//Ohana3DS 3D Rendering Engine by gdkchan
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Text;
using System.Windows.Forms;

using SharpDX;
using SharpDX.Direct3D9;
using SharpDX.Mathematics.Interop;
using Color = SharpDX.Color;
using Ohana3DS_Rebirth.Properties;

namespace Ohana3DS_Rebirth.Ohana
{
    public class RenderEngine : IDisposable
    {
        private bool disposed;

        private Effect fragmentShader;
        private PresentParameters pParams;
        private SharpDX.Direct3D9.Device device;
        private SharpDX.Direct3D9.Font infoHUD;

        public RenderBase.OModelGroup models;

        struct Vertex
        {
            public Vector4 Position;
            public ColorBGRA Color;
        }

        public struct customVertex
        {
            public float x, y, z;
            public float nx, ny, nz;
            public uint color;
            public float u0, v0;
            public float u1, v1;
            public float u2, v2;
        }

        private struct customTexture
        {
            public string name;
            public Texture texture;
            public int width;
            public int height;
        }

        private customVertex[][] meshes;
        private List<customTexture> textures = new List<customTexture>();

        private Vector2 translation;
        private Vector2 rotation;
        private float zoom;
        public bool lockCamera;

        public float Zoom
        {
            get { return zoom; }
        }

        const string infoHUDFontFamily = "Times New Roman";
        const int infoHUDFontSize = 14;

        public class animationControl
        {
            public RenderBase.OAnimationListBase animations;
            public float animationStep = 1;
            private int currentAnimation = -1;
            private float frame;
            public bool animate;
            public bool paused;

            public event EventHandler FrameChanged;
            public event EventHandler AnimationChanged;

            public float Frame
            {
                get
                {
                    return frame;
                }
                set
                {
                    frame = value;
                    FrameChanged?.Invoke(this, EventArgs.Empty);
                }
            }

            public int CurrentAnimation
            {
                get
                {
                    return currentAnimation;
                }
                set
                {
                    currentAnimation = value;
                    AnimationChanged?.Invoke(this, EventArgs.Empty);
                }
            }

            public string CurrentFrameInfo
            {
                get
                {
                    if (currentAnimation > -1)
                        return frame + " / " + animations.list[currentAnimation].frameSize;
                    else
                        return null;
                }
            }

            /// <summary>
            ///     Load the animation at the given index.
            /// </summary>
            /// <param name="animationIndex">The index where the animation is located</param>
            public bool load(int animationIndex)
            {
                currentAnimation = animationIndex;
                AnimationChanged?.Invoke(this, EventArgs.Empty);
                frame = 0;
                return true;
            }

            /// <summary>
            ///     Pauses the animation.
            /// </summary>
            public void pause()
            {
                paused = true;
            }

            /// <summary>
            ///     Stops the animation.
            /// </summary>
            public void stop()
            {
                animate = false;
                Frame = 0;
            }

            /// <summary>
            ///     Play the animation.
            ///     It will have no effect if no animations are loaded.
            /// </summary>
            public void play()
            {
                if (currentAnimation == -1) return;
                paused = false;
                animate = true;
            }

            /// <summary>
            ///     Advances the current animation Frame.
            /// </summary>
            /// <param name="frameSize">The total number of frames</param>
            public void advanceFrame()
            {
                if (!paused && animate)
                {
                    float frameSize = animations.list[currentAnimation].frameSize;
                    if (frame < frameSize) Frame += animationStep; else Frame = 0;
                }
            }
        }

        public animationControl ctrlSA = new animationControl();
        public animationControl ctrlMA = new animationControl();
        public animationControl ctrlVA = new animationControl();

        private int currentModel = -1;
        public int CurrentModel
        {
            get
            {
                return currentModel;
            }
            set
            {
                if (value != currentModel) resetCamera();
                currentModel = value;
                updateMeshes();
            }
        }

        private class OAnimationBone
        {
            public RenderBase.OMatrix transform;
            public bool hasTransform;

            public RenderBase.OVector3 translation;
            public Quaternion rotationQuaternion;
            public RenderBase.OVector3 scale;

            public short parentId;
            public string name = null;

            /// <summary>
            ///     Creates a new Bone.
            /// </summary>
            public OAnimationBone()
            {
                translation = new RenderBase.OVector3();
                rotationQuaternion = new Quaternion();
                scale = new RenderBase.OVector3();
            }
        }

        List<Vertex> gridBuffer;
        Timer animator;

        /// <summary>
        ///     Initialize the renderer at a given target.
        /// </summary>
        /// <param name="handle">Memory pointer to the control rendering buffer</param>
        /// <param name="width">Render width</param>
        /// <param name="height">Render height</param>
        public void initialize(IntPtr handle, int width, int height)
        {
            if (!Manager.CheckDeviceMultiSampleType(0, DeviceType.Hardware, Format.D16, true, (MultiSampleType)Properties.Settings.Default.reAntiAlias))
            {
                MessageBox.Show("MSAA level not supported by GPU!", "Warning", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                Properties.Settings.Default.reAntiAlias = 0;
            }

            pParams = new PresentParameters
            {
                BackBufferCount = 1,
                BackBufferFormat = Manager.Adapters[0].CurrentDisplayMode.Format,
                BackBufferWidth = width,
                BackBufferHeight = height,
                Windowed = true,
                SwapEffect = SwapEffect.Discard,
                EnableAutoDepthStencil = true,
                AutoDepthStencilFormat = Format.D24S8,
                MultiSampleType = (MultisampleType)Properties.Settings.Default.reAntiAlias
            };

            try
            {
                device = new Device(new Direct3D(), 0, DeviceType.Hardware, handle, CreateFlags.HardwareVertexProcessing, pParams);
            }
            catch
            {
                //Some crap GPUs only works with Software vertex processing
                device = new Device(new Direct3D(), 0, DeviceType.Hardware, handle, CreateFlags.SoftwareVertexProcessing, pParams);
            }

            //Information font setup
            FontDescription HUDFont = new FontDescription()
            {
                FaceName = infoHUDFontFamily,
                Height = infoHUDFontSize,
                Italic = false,
                CharacterSet = FontCharacterSet.Ansi,
                MipLevels = 0,
                OutputPrecision = FontPrecision.TrueType,
                PitchAndFamily = FontPitchAndFamily.Default,
                Quality = FontQuality.ClearType,
                Weight = FontWeight.Regular
            };
            infoHUD = new SharpDX.Direct3D9.Font(device, HUDFont);

            //Compile the Fragment Shader
            if (Properties.Settings.Default.reFragmentShader)
            {
                try
                {

                    fragmentShader = Effect.FromString(device, Resources.OFragmentShader, ShaderFlags.SkipOptimization);
                    fragmentShader.Technique = "Combiner";
                }
                catch (Exception)
                {
                    MessageBox.Show("Failed to compile Fragment Shader!", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }

            #region "Grid buffer creation"
            gridBuffer = new List<Vertex>();
            for (int i = -50; i <= 50; i += 2)
            {
                if (i == 0)
                {
                    // TODO: Check why i is ref
                    gridBuffer.AddRange(new[] {
                        new Vertex() { Color = Color.White, Position = new Vector4(-50f, 0, i, 1.0f)},
                        new Vertex() { Color = Color.White, Position = new Vector4(0, 0, i, 1.0f)},
                        new Vertex() { Color = Color.White, Position = new Vector4(5f, 0, i, 1.0f)},
                        new Vertex() { Color = Color.White, Position = new Vector4(50f, 0, i, 1.0f)},

                        new Vertex() { Color = Color.White, Position = new Vector4(i, 0, -50f, 1.0f)},
                        new Vertex() { Color = Color.White, Position = new Vector4(i, 0, -5f, 1.0f)},
                        new Vertex() { Color = Color.White, Position = new Vector4(i, 0, 0, 1.0f)},
                        new Vertex() { Color = Color.White, Position = new Vector4(i, 0, 50f, 1.0f)}
                    });
                }
                else
                {
                    Color lColor;
                    if ((i % 10) == 0)
                        lColor = Color.White;
                    else
                        lColor = Color.DarkGray;

                    gridBuffer.AddRange(new[]
                    {
                        new Vertex() { Color = lColor, Position = new Vector4(-50f, 0, i, 1.0f)},
                        new Vertex() { Color = lColor, Position = new Vector4(50f, 0, i, 1.0f)},
                        new Vertex() { Color = lColor, Position = new Vector4(i, 0, -50f, 1.0f)},
                        new Vertex() { Color = lColor, Position = new Vector4(i, 0, 50f, 1.0f)},
                    });
                }
            }

            gridBuffer.AddRange(new[]
            {
                new Vertex() { Color = Color.Red, Position = new Vector4(0, 0, 0, 1.0f) },
                new Vertex() { Color = Color.Red, Position = new Vector4(5f, 0, 0, 1.0f) },
                new Vertex() { Color = Color.Green, Position = new Vector4(0, 0, 0, 1.0f) },
                new Vertex() { Color = Color.Green, Position = new Vector4(0, 5f, 0, 1.0f) },
                new Vertex() { Color = Color.Blue, Position = new Vector4(0, 0, 0, 1.0f) },
                new Vertex() { Color = Color.Blue, Position = new Vector4(0, 0, -5f, 1.0f) },
            });
            #endregion

            ctrlSA.animations = models.skeletalAnimation;
            ctrlMA.animations = models.materialAnimation;
            ctrlVA.animations = models.visibilityAnimation;

            updateMeshes();
            updateTextures();

            animator = new Timer();
            animator.Interval = 1;
            animator.Tick += refresh;

            animator.Enabled = true;
        }

        private void refresh(object sender, EventArgs e)
        {
            render();
        }

        /// <summary>
        ///     Resizes the Back Buffer.
        /// </summary>
        /// <param name="width">New width</param>
        /// <param name="height">New height</param>
        public void resize(int width, int height)
        {
            if (width == 0 || height == 0) return;
            
            pParams.BackBufferWidth = width;
            pParams.BackBufferHeight = height;
            device.Reset(pParams);
        }

        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }

        protected virtual void Dispose(bool disposing)
        {
            if (disposed) return;

            foreach (customTexture texture in textures) texture.texture.Dispose();
            foreach (RenderBase.OTexture texture in models.texture) texture.texture.Dispose();

            if (fragmentShader != null) fragmentShader.Dispose();
            device.Dispose();
            infoHUD.Dispose();
            animator.Dispose();

            disposed = true;
        }

        /// <summary>
        ///     Updates the internal meshes buffer used for rendering.
        ///     Call this if you change the current model or the meshes on the current model.
        /// </summary>
        public void updateMeshes()
        {
            if (currentModel > -1)
            {
                meshes = new customVertex[models.model[currentModel].mesh.Count][];
                for (int i = 0; i < models.model[currentModel].mesh.Count; i++)
                {
                    RenderBase.OMesh mesh = models.model[currentModel].mesh[i];
                    meshes[i] = new customVertex[mesh.vertices.Count];

                    for (int j = 0; j < mesh.vertices.Count; j++)
                    {
                        customVertex vertex;

                        //Position
                        vertex.x = mesh.vertices[j].position.x;
                        vertex.y = mesh.vertices[j].position.y;
                        vertex.z = mesh.vertices[j].position.z;

                        //Normal
                        vertex.nx = mesh.vertices[j].normal.x;
                        vertex.ny = mesh.vertices[j].normal.y;
                        vertex.nz = mesh.vertices[j].normal.z;

                        //Texture
                        vertex.u0 = mesh.vertices[j].texture0.x;
                        vertex.v0 = mesh.vertices[j].texture0.y;

                        vertex.u1 = mesh.vertices[j].texture1.x;
                        vertex.v1 = mesh.vertices[j].texture1.y;

                        vertex.u2 = mesh.vertices[j].texture2.x;
                        vertex.v2 = mesh.vertices[j].texture2.y;

                        //Color
                        vertex.color = mesh.vertices[j].diffuseColor;

                        //Set it to the buffer
                        meshes[i][j] = vertex;
                    }
                }
            }
        }

        /// <summary>
        ///     Forces all textures on the model to be cached.
        /// </summary>
        public void updateTextures()
        {
            foreach (RenderBase.OTexture texture in models.texture) cacheTexture(texture);
        }

        /// <summary>
        ///     Adds a list of textures to the model and the cache.
        /// </summary>
        /// <param name="textures">The list of textures to be added</param>
        public void addTextureRange(List<RenderBase.OTexture> textures)
        {
            foreach (RenderBase.OTexture texture in textures)
            {
                models.texture.Add(texture);
                cacheTexture(texture);
            }
        }

        /// <summary>
        ///     Adds a single texture to the model and the cache.
        /// </summary>
        /// <param name="texture">The texture to be added</param>
        public void addTexture(RenderBase.OTexture texture)
        {
            models.texture.Add(texture);
            cacheTexture(texture);
        }

        /// <summary>
        ///     Adds a texture to the texture cache.
        /// </summary>
        /// <param name="texture"></param>
        private void cacheTexture(RenderBase.OTexture texture)
        {
            customTexture tex;
            tex.name = texture.name;
            Bitmap bmp = new Bitmap(texture.texture);
            bmp.RotateFlip(RotateFlipType.RotateNoneFlipY);
            tex.texture = new Texture(device, bmp, Usage.None, Pool.Managed);
            tex.width = bmp.Width;
            tex.height = bmp.Height;
            textures.Add(tex);
            bmp.Dispose();
        }

        /// <summary>
        ///     Removes all textures from the model and the cache.
        /// </summary>
        public void removeAllTextures()
        {
            foreach (RenderBase.OTexture texture in models.texture) texture.texture.Dispose();
            foreach (customTexture texture in textures) texture.texture.Dispose();

            models.texture.Clear();
            textures.Clear();
        }

        /// <summary>
        ///     Removes a texture at the given index.
        /// </summary>
        /// <param name="index">The index of the texture</param>
        public void removeTexture(int index)
        {
            if (index < 0 || index > textures.Count - 1) return;

            models.texture[index].texture.Dispose();
            textures[index].texture.Dispose();

            models.texture.RemoveAt(index);
            textures.RemoveAt(index);
        }

        public Color FromDrawingColor(System.Drawing.Color color)
        {
            return new Color(color.R, color.G, color.B, color.A);
        }

        /// <summary>
        ///     Renders a single frame of the scene.
        /// </summary>
        public void render()
        {
            RawColorBGRA backgroundColor = new RawColorBGRA(0, 0, 0, 0xFF);
            device.Clear(ClearFlags.Stencil | ClearFlags.Target | ClearFlags.ZBuffer, backgroundColor, 1f, 15);
            device.SetTexture(0, null);
            device.BeginScene();

            float fovy = (float)Math.PI / 4;
            float aspectRatio = (float)pParams.BackBufferWidth / pParams.BackBufferHeight;
            device.SetTransform(TransformState.Projection, Matrix.PerspectiveFovLH(fovy, aspectRatio, 0.01f, 1000f));
            device.SetTransform(TransformState.View, Matrix.LookAtLH(
                new Vector3(0f, 0f, 20f),
                new Vector3(0f, 0f, 0f),
                new Vector3(0f, 1f, 0f)));

            //View
            RenderBase.OVector3 minVector = new RenderBase.OVector3();
            RenderBase.OVector3 maxVector = new RenderBase.OVector3();

            if (currentModel > -1)
            {
                minVector = models.model[currentModel].minVector;
                maxVector = models.model[currentModel].maxVector;
            }

            float minSize = Math.Min(Math.Min(minVector.x, minVector.y), minVector.z);
            float maxSize = Math.Max(Math.Max(maxVector.x, maxVector.y), maxVector.z);
            float scale = (10f / (maxSize - minSize)); //Try to adjust to screen
            if (maxSize - minSize == 0) scale = 1;

            Matrix centerMatrix = Matrix.Translation(
                -(minVector.x + maxVector.x) / 2,
                -(minVector.y + maxVector.y) / 2,
                -(minVector.z + maxVector.z) / 2);
            Matrix translationMatrix = Matrix.Translation(
                (-translation.X / 50) / scale,
                (translation.Y / 50) / scale,
                zoom / scale);

            Matrix baseTransform = Matrix.Identity;
            baseTransform *= Matrix.RotationY(rotation.Y) * Matrix.RotationX(rotation.X);
            baseTransform *= centerMatrix * translationMatrix * Matrix.Scaling(-scale, scale, scale);

            //Grid
            if (Properties.Settings.Default.reShowGuidelines)
            {
                resetRenderState();
                device.SetTransform(TransformState.World, baseTransform);
                //device.VertexFormat = CustomVertex.PositionColored.Format;

                VertexBuffer buffer = new VertexBuffer(device, gridBuffer.Count * 20, Usage.WriteOnly, VertexFormat.None, Pool.Managed);
                buffer.Lock(0, 0, LockFlags.None).WriteRange(gridBuffer.ToArray());
                buffer.Unlock();
                device.SetStreamSource(0, buffer, 0, 20);
                device.DrawPrimitives(PrimitiveType.LineList, 0, gridBuffer.Count / 2);
            }

            if (Properties.Settings.Default.reFragmentShader)
            {
                fragmentShader.Begin(0);

                fragmentShader.SetValue("world", device.GetTransform(TransformState.World));
                fragmentShader.SetValue("view", device.GetTransform(TransformState.View));
                fragmentShader.SetValue("projection", device.GetTransform(TransformState.Projection));

                fragmentShader.SetValue("lights[0].pos", new Vector4(0, -10, -10, 0));
                fragmentShader.SetValue("lights[0].ambient", new Vector4(0.1f, 0.1f, 0.1f, 1));
                fragmentShader.SetValue("lights[0].diffuse", new Vector4(1, 1, 1, 1));
                fragmentShader.SetValue("lights[0].specular", new Vector4(1, 1, 1, 1));
                fragmentShader.SetValue("numLights", 1);
            }

            if (Properties.Settings.Default.reWireframeMode)
                device.SetRenderState(RenderState.FillMode, FillMode.Wireframe);
            else
                device.SetRenderState(RenderState.FillMode, FillMode.Solid);

            if (currentModel > -1)
            {
                RenderBase.OModel mdl = models.model[currentModel];
                device.SetTransform(TransformState.World, getMatrix(mdl.transform) * baseTransform);

                #region "Skeletal Animation"
                Matrix[] animationSkeletonTransform = new Matrix[mdl.skeleton.Count];
                if (ctrlSA.animate)
                {
                    Matrix[] skeletonTransform = new Matrix[mdl.skeleton.Count];

                    for (int index = 0; index < mdl.skeleton.Count; index++)
                    {
                        skeletonTransform[index] = Matrix.Identity;
                        transformSkeleton(mdl.skeleton, index, ref skeletonTransform[index]);
                    }

                    List<RenderBase.OSkeletalAnimationBone> bone = ((RenderBase.OSkeletalAnimation)models.skeletalAnimation.list[ctrlSA.CurrentAnimation]).bone;
                    List<OAnimationBone> frameAnimationSkeleton = new List<OAnimationBone>();
                    for (int index = 0; index < mdl.skeleton.Count; index++)
                    {
                        OAnimationBone newBone = new OAnimationBone();
                        newBone.parentId = mdl.skeleton[index].parentId;
                        newBone.rotationQuaternion = getQuaternion(mdl.skeleton[index].rotation, false);
                        newBone.translation = new RenderBase.OVector3(mdl.skeleton[index].translation);
                        newBone.scale = new RenderBase.OVector3(1, 1, 1);
                        foreach (RenderBase.OSkeletalAnimationBone b in bone)
                        {
                            if (b.name == mdl.skeleton[index].name)
                            {
                                if (b.isFullBakedFormat)
                                {
                                    newBone.hasTransform = true;
                                    newBone.transform = b.transform[(int)ctrlSA.Frame % b.transform.Count];
                                }
                                else if (b.isFrameFormat)
                                {
                                    float fl = (float)Math.Floor(ctrlSA.Frame);
                                    float fr = (float)Math.Ceiling(ctrlSA.Frame);
                                    float mu = ctrlSA.Frame - fl;

                                    if (b.scale.exists)
                                    {
                                        int il = Math.Min((int)fl, b.scale.vector.Count - 1);
                                        int ir = Math.Min((int)fr, b.scale.vector.Count - 1);

                                        RenderBase.OVector4 sl = b.scale.vector[il];
                                        RenderBase.OVector4 sr = b.scale.vector[ir];
                                        RenderBase.OVector4 s = AnimationUtils.interpolateLinear(sl, sr, mu);

                                        newBone.scale = new RenderBase.OVector3(s.x, s.y, s.z);
                                    }

                                    if (b.rotationQuaternion.exists)
                                    {
                                        int il = Math.Min((int)fl, b.rotationQuaternion.vector.Count - 1);
                                        int ir = Math.Min((int)fr, b.rotationQuaternion.vector.Count - 1);

                                        Quaternion ql = getQuaternion(b.rotationQuaternion.vector[il]);
                                        Quaternion qr = getQuaternion(b.rotationQuaternion.vector[ir]);

                                        newBone.rotationQuaternion = Quaternion.Slerp(ql, qr, mu);
                                    }

                                    if (b.translation.exists)
                                    {
                                        int il = Math.Min((int)fl, b.translation.vector.Count - 1);
                                        int ir = Math.Min((int)fr, b.translation.vector.Count - 1);

                                        RenderBase.OVector4 tl = b.translation.vector[il];
                                        RenderBase.OVector4 tr = b.translation.vector[ir];
                                        RenderBase.OVector4 t = AnimationUtils.interpolateLinear(tl, tr, mu);

                                        newBone.translation = new RenderBase.OVector3(t.x, t.y, t.z);

                                        newBone.translation.x *= mdl.skeleton[index].absoluteScale.x;
                                        newBone.translation.y *= mdl.skeleton[index].absoluteScale.y;
                                        newBone.translation.z *= mdl.skeleton[index].absoluteScale.z;
                                    }
                                }
                                else
                                {
                                    //Scale
                                    if (b.scaleX.exists) newBone.scale.x = AnimationUtils.getKey(b.scaleX, ctrlSA.Frame);
                                    if (b.scaleY.exists) newBone.scale.y = AnimationUtils.getKey(b.scaleY, ctrlSA.Frame);
                                    if (b.scaleZ.exists) newBone.scale.z = AnimationUtils.getKey(b.scaleZ, ctrlSA.Frame);

                                    //Rotation
                                    float fl = (float)Math.Floor(ctrlSA.Frame);
                                    float fr = (float)Math.Ceiling(ctrlSA.Frame);
                                    float mu = ctrlSA.Frame - fl;

                                    RenderBase.OVector3 vl = new RenderBase.OVector3(mdl.skeleton[index].rotation);
                                    RenderBase.OVector3 vr = new RenderBase.OVector3(mdl.skeleton[index].rotation);

                                    if (b.rotationX.exists)
                                    {
                                        vl.x = AnimationUtils.getKey(b.rotationX, fl);
                                        vr.x = AnimationUtils.getKey(b.rotationX, fr);
                                    }

                                    if (b.rotationY.exists)
                                    {
                                        vl.y = AnimationUtils.getKey(b.rotationY, fl);
                                        vr.y = AnimationUtils.getKey(b.rotationY, fr);
                                    }

                                    if (b.rotationZ.exists)
                                    {
                                        vl.z = AnimationUtils.getKey(b.rotationZ, fl);
                                        vr.z = AnimationUtils.getKey(b.rotationZ, fr);
                                    }

                                    Quaternion ql = getQuaternion(vl, b.isAxisAngle);
                                    Quaternion qr = getQuaternion(vr, b.isAxisAngle);
                                    newBone.rotationQuaternion = Quaternion.Slerp(ql, qr, mu);

                                    //Translation
                                    if (b.translationX.exists)
                                    {
                                        newBone.translation.x = AnimationUtils.getKey(b.translationX, ctrlSA.Frame);
                                        newBone.translation.x *= mdl.skeleton[index].absoluteScale.x;
                                    }

                                    if (b.translationY.exists)
                                    {
                                        newBone.translation.y = AnimationUtils.getKey(b.translationY, ctrlSA.Frame);
                                        newBone.translation.y *= mdl.skeleton[index].absoluteScale.y;
                                    }

                                    if (b.translationZ.exists)
                                    {
                                        newBone.translation.z = AnimationUtils.getKey(b.translationZ, ctrlSA.Frame);
                                        newBone.translation.z *= mdl.skeleton[index].absoluteScale.z;
                                    }
                                }

                                break;
                            }
                        }

                        frameAnimationSkeleton.Add(newBone);
                    }

                    for (int index = 0; index < mdl.skeleton.Count; index++)
                    {
                        animationSkeletonTransform[index] = Matrix.Identity;

                        if (frameAnimationSkeleton[index].hasTransform)
                            animationSkeletonTransform[index] = getMatrix(frameAnimationSkeleton[index].transform);
                        else
                        {
                            animationSkeletonTransform[index] *= Matrix.Scaling(
                                frameAnimationSkeleton[index].scale.x,
                                frameAnimationSkeleton[index].scale.y,
                                frameAnimationSkeleton[index].scale.z);

                            int id = index;
                            while (id > -1)
                            {
                                RenderBase.OVector3 t = new RenderBase.OVector3(frameAnimationSkeleton[id].translation);
                                if (frameAnimationSkeleton[id].parentId > -1) t *= frameAnimationSkeleton[frameAnimationSkeleton[id].parentId].scale;

                                animationSkeletonTransform[index] *= Matrix.RotationQuaternion(frameAnimationSkeleton[id].rotationQuaternion);
                                animationSkeletonTransform[index] *= Matrix.Translation(t.x, t.y, t.z);

                                id = frameAnimationSkeleton[id].parentId;
                            }
                        }

                        animationSkeletonTransform[index] = Matrix.Invert(skeletonTransform[index]) * animationSkeletonTransform[index];
                    }
                }
                #endregion

                for (int objectIndex = 0; objectIndex < mdl.mesh.Count; objectIndex++)
                {
                    RenderBase.OMesh obj = mdl.mesh[objectIndex];
                    bool isVisible = obj.isVisible;

                    if (ctrlVA.animate)
                    {
                        foreach (RenderBase.OVisibilityAnimationData data in ((RenderBase.OVisibilityAnimation)models.visibilityAnimation.list[ctrlVA.CurrentAnimation]).data)
                        {
                            RenderBase.OAnimationKeyFrame frame = AnimationUtils.getLeftFrame(data.visibilityList.keyFrames, ctrlVA.Frame);
                            if (data.name == obj.name) isVisible = frame.bValue;
                        }
                    }

                    if (!(isVisible || Properties.Settings.Default.reShowAllMeshes)) continue;

                    RenderBase.OMaterial material = mdl.material[obj.materialId];

                    #region "Material Animation"
                    int[] textureId = { -1, -1, -1 };
                    Color blendColor = FromDrawingColor(material.fragmentOperation.blend.blendColor);
                    Color[] borderColor = new Color[3];
                    borderColor[0] = FromDrawingColor(material.textureMapper[0].borderColor);
                    borderColor[1] = FromDrawingColor(material.textureMapper[1].borderColor);
                    borderColor[2] = FromDrawingColor(material.textureMapper[2].borderColor);
                    if (ctrlMA.animate)
                    {
                        foreach (RenderBase.OMaterialAnimationData data in ((RenderBase.OMaterialAnimation)models.materialAnimation.list[ctrlMA.CurrentAnimation]).data)
                        {
                            if (data.name == material.name)
                            {
                                switch (data.type)
                                {
                                    case RenderBase.OMaterialAnimationType.textureMapper0: getMaterialAnimationInt(data, ref textureId[0]); break;
                                    case RenderBase.OMaterialAnimationType.textureMapper1: getMaterialAnimationInt(data, ref textureId[1]); break;
                                    case RenderBase.OMaterialAnimationType.textureMapper2: getMaterialAnimationInt(data, ref textureId[2]); break;
                                    case RenderBase.OMaterialAnimationType.borderColorMapper0: getMaterialAnimationColor(data, ref borderColor[0]); break;
                                    case RenderBase.OMaterialAnimationType.borderColorMapper1: getMaterialAnimationColor(data, ref borderColor[1]); break;
                                    case RenderBase.OMaterialAnimationType.borderColorMapper2: getMaterialAnimationColor(data, ref borderColor[2]); break;
                                    case RenderBase.OMaterialAnimationType.blendColor: getMaterialAnimationColor(data, ref blendColor); break;
                                }
                            }
                        }
                    }
                    #endregion

                    if (Properties.Settings.Default.reFragmentShader)
                    {
                        #region "Shader combiner parameters"
                        RenderBase.OMaterialColor materialColor = new RenderBase.OMaterialColor();
                        materialColor = material.materialColor;

                        if (ctrlMA.animate)
                        {
                            foreach (RenderBase.OMaterialAnimationData data in ((RenderBase.OMaterialAnimation)models.materialAnimation.list[ctrlMA.CurrentAnimation]).data)
                            {
                                if (data.name == material.name)
                                {
                                    switch (data.type)
                                    {
                                        case RenderBase.OMaterialAnimationType.constant0: getMaterialAnimationColor(data, ref materialColor.constant0); break;
                                        case RenderBase.OMaterialAnimationType.constant1: getMaterialAnimationColor(data, ref materialColor.constant1); break;
                                        case RenderBase.OMaterialAnimationType.constant2: getMaterialAnimationColor(data, ref materialColor.constant2); break;
                                        case RenderBase.OMaterialAnimationType.constant3: getMaterialAnimationColor(data, ref materialColor.constant3); break;
                                        case RenderBase.OMaterialAnimationType.constant4: getMaterialAnimationColor(data, ref materialColor.constant4); break;
                                        case RenderBase.OMaterialAnimationType.constant5: getMaterialAnimationColor(data, ref materialColor.constant5); break;
                                        case RenderBase.OMaterialAnimationType.diffuse: getMaterialAnimationColor(data, ref materialColor.diffuse); break;
                                        case RenderBase.OMaterialAnimationType.specular0: getMaterialAnimationColor(data, ref materialColor.specular0); break;
                                        case RenderBase.OMaterialAnimationType.specular1: getMaterialAnimationColor(data, ref materialColor.specular1); ; break;
                                        case RenderBase.OMaterialAnimationType.ambient: getMaterialAnimationColor(data, ref materialColor.ambient); break;
                                    }
                                }
                            }
                        }

                        fragmentShader.SetValue("hasTextures", textures.Count > 0);
                        fragmentShader.SetValue("uvCount", obj.texUVCount);

                        fragmentShader.SetValue("isD0Enabled", material.fragmentShader.lighting.isDistribution0Enabled);
                        fragmentShader.SetValue("isD1Enabled", material.fragmentShader.lighting.isDistribution1Enabled);
                        fragmentShader.SetValue("isG0Enabled", material.fragmentShader.lighting.isGeometryFactor0Enabled);
                        fragmentShader.SetValue("isG1Enabled", material.fragmentShader.lighting.isGeometryFactor1Enabled);
                        fragmentShader.SetValue("isREnabled", material.fragmentShader.lighting.isReflectionEnabled);

                        fragmentShader.SetValue("bumpIndex", (int)material.fragmentShader.bump.texture);
                        fragmentShader.SetValue("bumpMode", (int)material.fragmentShader.bump.mode);

                        fragmentShader.SetValue("mEmissive", getColor(materialColor.emission));
                        fragmentShader.SetValue("mAmbient", getColor(materialColor.ambient));
                        fragmentShader.SetValue("mDiffuse", getColor(materialColor.diffuse));
                        fragmentShader.SetValue("mSpecular", getColor(materialColor.specular0));

                        fragmentShader.SetValue("hasNormal", obj.hasNormal);

                        for (int i = 0; i < 6; i++)
                        {
                            RenderBase.OTextureCombiner textureCombiner = material.fragmentShader.textureCombiner[i];

                            fragmentShader.SetValue(string.Format("combiners[{0}].colorCombine", i), (int)textureCombiner.combineRgb);
                            fragmentShader.SetValue(string.Format("combiners[{0}].alphaCombine", i), (int)textureCombiner.combineAlpha);

                            fragmentShader.SetValue(string.Format("combiners[{0}].colorScale", i), (float)textureCombiner.rgbScale);
                            fragmentShader.SetValue(string.Format("combiners[{0}].alphaScale", i), (float)textureCombiner.alphaScale);

                            for (int j = 0; j < 3; j++)
                            {
                                fragmentShader.SetValue(string.Format("combiners[{0}].colorArg[{1}]", i, j), (int)textureCombiner.rgbSource[j]);
                                fragmentShader.SetValue(string.Format("combiners[{0}].colorOp[{1}]", i, j), (int)textureCombiner.rgbOperand[j]);
                                fragmentShader.SetValue(string.Format("combiners[{0}].alphaArg[{1}]", i, j), (int)textureCombiner.alphaSource[j]);
                                fragmentShader.SetValue(string.Format("combiners[{0}].alphaOp[{1}]", i, j), (int)textureCombiner.alphaOperand[j]);
                            }

                            Color constantColor = Color.White;
                            switch (textureCombiner.constantColor)
                            {
                                case RenderBase.OConstantColor.ambient: constantColor = materialColor.ambient; break;
                                case RenderBase.OConstantColor.constant0: constantColor = materialColor.constant0; break;
                                case RenderBase.OConstantColor.constant1: constantColor = materialColor.constant1; break;
                                case RenderBase.OConstantColor.constant2: constantColor = materialColor.constant2; break;
                                case RenderBase.OConstantColor.constant3: constantColor = materialColor.constant3; break;
                                case RenderBase.OConstantColor.constant4: constantColor = materialColor.constant4; break;
                                case RenderBase.OConstantColor.constant5: constantColor = materialColor.constant5; break;
                                case RenderBase.OConstantColor.diffuse: constantColor = materialColor.diffuse; break;
                                case RenderBase.OConstantColor.emission: constantColor = materialColor.emission; break;
                                case RenderBase.OConstantColor.specular0: constantColor = materialColor.specular0; break;
                                case RenderBase.OConstantColor.specular1: constantColor = materialColor.specular1; break;
                            }

                            fragmentShader.SetValue(string.Format("combiners[{0}].constant", i), new Vector4(
                                (float)constantColor.R / 0xff,
                                (float)constantColor.G / 0xff,
                                (float)constantColor.B / 0xff,
                                (float)constantColor.A / 0xff));
                        }
                        #endregion
                    }

                    int legacyTextureIndex = -1;
                    int legacyTextureUnit = -1;
                    for (int i = 0; i < textures.Count; i++)
                    {
                        string[] name = new string[3];
                        name[0] = material.name0;
                        name[1] = material.name1;
                        name[2] = material.name2;

                        if (ctrlMA.animate)
                        {
                            RenderBase.OMaterialAnimation ma = (RenderBase.OMaterialAnimation)models.materialAnimation.list[ctrlMA.CurrentAnimation];
                            for (int j = 0; j < 3; j++) if (textureId[j] > -1) name[j] = ma.textureName[textureId[j]];
                        }

                        if (Properties.Settings.Default.reLegacyTexturingMode == 0 && !Properties.Settings.Default.reFragmentShader)
                        {
                            if (textures[i].name == name[0])
                            {
                                legacyTextureIndex = i;
                                legacyTextureUnit = 0;
                                break;
                            }
                        }
                        else
                        {
                            for (int j = 0; j < 3; j++)
                            {
                                if (Properties.Settings.Default.reFragmentShader)
                                {
                                    if (textures[i].name == name[j])
                                    {
                                        string shaderTexture = string.Format("texture{0}", j);
                                        fragmentShader.SetValue(shaderTexture, textures[i].texture);
                                    }
                                }
                                else
                                {
                                    if (textures[i].name == name[j] && legacyTextureUnit < j)
                                    {
                                        legacyTextureIndex = i;
                                        legacyTextureUnit = j;
                                    }
                                }
                            }
                        }
                    }

                    if (!Properties.Settings.Default.reFragmentShader)
                    {
                        if (legacyTextureIndex > -1)
                            device.SetTexture(0, textures[legacyTextureIndex].texture);
                        else
                            device.SetTexture(0, null);
                    }

                    #region "Texture Filtering/Addressing Setup"
                    //Filtering
                    for (int s = 0; s < 3; s++)
                    {
                        RenderBase.OTextureCoordinator coordinator = material.textureCoordinator[s];

                        Vector2 translate = new Vector2(coordinator.translateU, coordinator.translateV);
                        Vector2 scaling = new Vector2(coordinator.scaleU, coordinator.scaleV);
                        if (scaling == Vector2.Zero) scaling = new Vector2(1, 1);
                        float rotate = coordinator.rotate;
                        #region "Material Animation"
                        if (ctrlMA.animate)
                        {
                            foreach (RenderBase.OMaterialAnimationData data in ((RenderBase.OMaterialAnimation)models.materialAnimation.list[ctrlMA.CurrentAnimation]).data)
                            {
                                if (data.name == material.name)
                                {
                                    switch (data.type)
                                    {
                                        case RenderBase.OMaterialAnimationType.translateCoordinator0: if (s == 0) getMaterialAnimationVector2(data, ref translate); break; //Translation
                                        case RenderBase.OMaterialAnimationType.translateCoordinator1: if (s == 1) getMaterialAnimationVector2(data, ref translate); break;
                                        case RenderBase.OMaterialAnimationType.translateCoordinator2: if (s == 2) getMaterialAnimationVector2(data, ref translate); break;
                                        case RenderBase.OMaterialAnimationType.scaleCoordinator0: if (s == 0) getMaterialAnimationVector2(data, ref scaling); break; //Scaling
                                        case RenderBase.OMaterialAnimationType.scaleCoordinator1: if (s == 1) getMaterialAnimationVector2(data, ref scaling); break;
                                        case RenderBase.OMaterialAnimationType.scaleCoordinator2: if (s == 2) getMaterialAnimationVector2(data, ref scaling); break;
                                        case RenderBase.OMaterialAnimationType.rotateCoordinator0: if (s == 0) getMaterialAnimationFloat(data, ref rotate); break; //Rotation
                                        case RenderBase.OMaterialAnimationType.rotateCoordinator1: if (s == 1) getMaterialAnimationFloat(data, ref rotate); break;
                                        case RenderBase.OMaterialAnimationType.rotateCoordinator2: if (s == 2) getMaterialAnimationFloat(data, ref rotate); break;
                                    }
                                }
                            }
                        }
                        #endregion

                        if (Properties.Settings.Default.reFragmentShader)
                        {
                            fragmentShader.SetValue(string.Format("uvTranslate[{0}]", s), new Vector4(-translate.X, -translate.Y, 0, 0));
                            fragmentShader.SetValue(string.Format("uvScale[{0}]", s), new Vector4(scaling.X, scaling.Y, 0, 0));
                            fragmentShader.SetValue(string.Format("uvTransform[{0}]", s), Matrix.RotationZ(rotate));
                        }
                        else
                        {
                            device.SetTextureStageState(s, TextureStage.TextureTransformFlags, (int)TextureTransform.Count2);
                            Matrix uvTransform = rotateCenter2D(rotate) * Matrix.Scaling(scaling.X, scaling.Y, 1) * translate2D(-translate);
                            if (s == legacyTextureUnit) device.SetTransform(TransformState.Texture0, uvTransform);
                        }

                        device.SetSamplerState(s, SamplerState.MinFilter, (int)TextureFilter.Linear);
                        switch (material.textureMapper[s].magFilter)
                        {
                            case RenderBase.OTextureMagFilter.nearest: device.SetSamplerState(s, SamplerState.MagFilter, (int)TextureFilter.None); break;
                            case RenderBase.OTextureMagFilter.linear: device.SetSamplerState(s, SamplerState.MagFilter, (int)TextureFilter.Linear); break;
                        }

                        //Addressing
                        device.SetSamplerState(s, SamplerState.BorderColor, borderColor[s].ToArgb());
                        switch (material.textureMapper[s].wrapU)
                        {
                            case RenderBase.OTextureWrap.repeat: device.SetSamplerState(s, SamplerState.AddressU, (int)TextureAddress.Wrap); break;
                            case RenderBase.OTextureWrap.mirroredRepeat: device.SetSamplerState(s, SamplerState.AddressU, (int)TextureAddress.Mirror); break;
                            case RenderBase.OTextureWrap.clampToEdge: device.SetSamplerState(s, SamplerState.AddressU, (int)TextureAddress.Clamp); break;
                            case RenderBase.OTextureWrap.clampToBorder: device.SetSamplerState(s, SamplerState.AddressU, (int)TextureAddress.Border); break;
                        }
                        switch (material.textureMapper[s].wrapV)
                        {
                            case RenderBase.OTextureWrap.repeat: device.SetSamplerState(s, SamplerState.AddressV, (int)TextureAddress.Wrap); break;
                            case RenderBase.OTextureWrap.mirroredRepeat: device.SetSamplerState(s, SamplerState.AddressV, (int)TextureAddress.Mirror); break;
                            case RenderBase.OTextureWrap.clampToEdge: device.SetSamplerState(s, SamplerState.AddressV, (int)TextureAddress.Clamp); break;
                            case RenderBase.OTextureWrap.clampToBorder: device.SetSamplerState(s, SamplerState.AddressV, (int)TextureAddress.Border); break;
                        }
                    }
                    #endregion

                    #region "Culling/Alpha/Depth/Stencil testing/blending stuff Setup"
                    //Culling
                    switch (material.rasterization.cullMode)
                    {
                        case RenderBase.OCullMode.backFace: device.RenderState.CullMode = Cull.Clockwise; break;
                        case RenderBase.OCullMode.frontFace: device.RenderState.CullMode = Cull.CounterClockwise; break;
                        case RenderBase.OCullMode.never: device.RenderState.CullMode = Cull.None; break;
                    }

                    //Alpha testing
                    RenderBase.OAlphaTest alpha = material.fragmentShader.alphaTest;
                    device.RenderState.AlphaTestEnable = alpha.isTestEnabled;
                    device.RenderState.AlphaFunction = getCompare(alpha.testFunction);
                    device.RenderState.ReferenceAlpha = (int)alpha.testReference;

                    //Depth testing
                    RenderBase.ODepthOperation depth = material.fragmentOperation.depth;
                    device.RenderState.ZBufferEnable = depth.isTestEnabled;
                    device.RenderState.ZBufferFunction = getCompare(depth.testFunction);
                    device.RenderState.ZBufferWriteEnable = depth.isMaskEnabled;

                    //Alpha blending
                    RenderBase.OBlendOperation blend = material.fragmentOperation.blend;
                    device.RenderState.AlphaBlendEnable = blend.mode == RenderBase.OBlendMode.blend;
                    device.RenderState.SeparateAlphaBlendEnabled = true;
                    device.RenderState.SourceBlend = getBlend(blend.rgbFunctionSource);
                    device.RenderState.DestinationBlend = getBlend(blend.rgbFunctionDestination);
                    device.RenderState.BlendOperation = getBlendOperation(blend.rgbBlendEquation);
                    device.RenderState.AlphaSourceBlend = getBlend(blend.alphaFunctionSource);
                    device.RenderState.AlphaDestinationBlend = getBlend(blend.alphaFunctionDestination);
                    device.RenderState.AlphaBlendOperation = getBlendOperation(blend.alphaBlendEquation);
                    device.RenderState.BlendFactorColor = blendColor.ToArgb();

                    //Stencil testing
                    RenderBase.OStencilOperation stencil = material.fragmentOperation.stencil;
                    device.RenderState.StencilEnable = stencil.isTestEnabled;
                    device.RenderState.StencilFunction = getCompare(stencil.testFunction);
                    device.RenderState.ReferenceStencil = (int)stencil.testReference;
                    device.RenderState.StencilWriteMask = (int)stencil.testMask;
                    device.RenderState.StencilFail = getStencilOperation(stencil.failOperation);
                    device.RenderState.StencilZBufferFail = getStencilOperation(stencil.zFailOperation);
                    device.RenderState.StencilPass = getStencilOperation(stencil.passOperation);
                    #endregion

                    #region "Rendering"
                    //Vertex rendering
                    VertexFormat vertexFormat = VertexFormat.Position | VertexFormat.Normal | VertexFormat.Texture3 | VertexFormat.Diffuse;
                    device.VertexFormat = vertexFormat;

                    if (Properties.Settings.Default.reFragmentShader) fragmentShader.BeginPass(0);

                    if (meshes[objectIndex].Length > 0)
                    {
                        customVertex[] buffer = meshes[objectIndex];

                        if (ctrlSA.animate)
                        {
                            buffer = new customVertex[meshes[objectIndex].Length];

                            for (int vertex = 0; vertex < buffer.Length; vertex++)
                            {
                                buffer[vertex] = meshes[objectIndex][vertex];
                                RenderBase.OVertex input = obj.vertices[vertex];
                                Vector3 position = new Vector3(input.position.x, input.position.y, input.position.z);
                                Vector4 p = new Vector4();

                                int weightIndex = 0;
                                float weightSum = 0;
                                foreach (int boneIndex in input.node)
                                {
                                    float weight = 0;
                                    if (weightIndex < input.weight.Count) weight = input.weight[weightIndex++];
                                    weightSum += weight;
                                    p += Vector3.Transform(position, animationSkeletonTransform[boneIndex]) * weight;
                                }
                                if (weightSum < 1) p += new Vector4(position.X, position.Y, position.Z, 0) * (1 - weightSum);

                                buffer[vertex].x = p.X;
                                buffer[vertex].y = p.Y;
                                buffer[vertex].z = p.Z;
                            }
                        }

                        using (VertexBuffer vertexBuffer = new VertexBuffer(typeof(customVertex), buffer.Length, device, Usage.None, vertexFormat, Pool.Managed))
                        {
                            vertexBuffer.(buffer, 0, LockFlags.None);
                            device.SetStreamSource(0, vertexBuffer, 0, 0);

                            device.DrawPrimitives(PrimitiveType.TriangleList, 0, buffer.Length / 3);
                        }
                    }

                    if (Properties.Settings.Default.reFragmentShader) fragmentShader.EndPass();
                    #endregion
                }
            }

            if (Properties.Settings.Default.reFragmentShader) fragmentShader.End();

            //HUD
            if (Properties.Settings.Default.reShowInformation && currentModel > -1)
            {
                resetRenderState();
                RenderBase.OModel mdl = models.model[currentModel];

                StringBuilder info = new StringBuilder();
                info.AppendLine("Meshes: " + mdl.mesh.Count);
                info.AppendLine("Triangles: " + (models.model[currentModel].verticesCount / 3));
                info.AppendLine("Bones: " + mdl.skeleton.Count);
                info.AppendLine("Materials: " + mdl.material.Count);
                info.AppendLine("Textures: " + models.texture.Count);
                if (ctrlSA.CurrentAnimation > -1) info.AppendLine("S. Frame: " + ctrlSA.CurrentFrameInfo);
                if (ctrlMA.CurrentAnimation > -1) info.AppendLine("M. Frame: " + ctrlMA.CurrentFrameInfo);
                if (ctrlVA.CurrentAnimation > -1) info.AppendLine("V. Frame: " + ctrlVA.CurrentFrameInfo);

                drawText(info.ToString(), 128, 128);
            }

            device.EndScene();
            device.Present();

            ctrlSA.advanceFrame(); //TODO Fix me. StackOverflow occurs here!
            ctrlMA.advanceFrame();
            ctrlVA.advanceFrame();
        }

        /// <summary>
        ///     Resets the Render State to the default values.
        /// </summary>
        private void resetRenderState()
        {
            device.SetRenderState(RenderState.FillMode, FillMode.Solid);
            device.SetRenderState(RenderState.AlphaTestEnable, false);
            device.SetRenderState(RenderState.ZEnable, true);
            device.SetRenderState(RenderState.ZFunc, Compare.LessEqual);
            device.SetRenderState(RenderState.ZWriteEnable, true);
            device.SetRenderState(RenderState.AlphaBlendEnable, false);
            device.SetRenderState(RenderState.SeparateAlphaBlendEnable, false);
            device.SetRenderState(RenderState.StencilEnable, false);
            device.SetRenderState(RenderState.CullMode, Cull.None);
            device.SetRenderState(RenderState.Lighting, false);
        }

        /// <summary>
        ///     Draws a text inside a box on the Screen.
        /// </summary>
        /// <param name="text">The text to be draw</param>
        /// <param name="width">The width of the box</param>
        /// <param name="height">The height of the box</param>
        private void drawText(string text, int width, int height)
        {
            int x = 16;
            int y = 16;
            int c = 0x7f000000;

            CustomVertex.TransformedColored[] boxBuffer = new CustomVertex.TransformedColored[4];
            boxBuffer[0] = new CustomVertex.TransformedColored(x, y, 0, 1, c);
            boxBuffer[1] = new CustomVertex.TransformedColored(x + width, y, 0, 1, c);
            boxBuffer[2] = new CustomVertex.TransformedColored(x, y + height, 0, 1, c);
            boxBuffer[3] = new CustomVertex.TransformedColored(x + width, y + height, 0, 1, c);

            using (VertexBuffer vertexBuffer = new VertexBuffer(typeof(CustomVertex.TransformedColored), 4, device, Usage.None, CustomVertex.TransformedColored.Format, Pool.Managed))
            {
                vertexBuffer.SetData(boxBuffer, 0, LockFlags.None);

                device.SetTexture(0, null);
                device.SetRenderState(RenderState.AlphaBlendEnable, true);
                device.SetRenderState(RenderState.SourceBlend, Blend.SourceAlpha);
                device.SetRenderState(RenderState.DestinationBlend, Blend.InverseSourceAlpha);
                device.SetRenderState(RenderState.BlendOperationAlpha, BlendOperation.Add);
                device.VertexFormat = CustomVertex.TransformedColored.Format;

                device.SetStreamSource(0, vertexBuffer, 0, 0);
            }

            device.DrawPrimitives(PrimitiveType.TriangleStrip, 0, 2);
            infoHUD.DrawText(null, text, x + 8, y + 8, new RawColorBGRA(0xFF, 0xFF, 0xFF, 0xFF));
        }

        /// <summary>
        ///     Set X/Y angles to rotate the Mesh.
        /// </summary>
        /// <param name="y"></param>
        /// <param name="x"></param>
        public void setRotation(float y, float x)
        {
            if (lockCamera) return;
            rotation.X = wrap(rotation.X + x);
            rotation.Y = wrap(rotation.Y + y);

            render();
        }

        /// <summary>
        ///     Set translation of the Mesh.
        /// </summary>
        /// <param name="x"></param>
        /// <param name="y"></param>
        public void setTranslation(float x, float y)
        {
            if (lockCamera) return;
            translation.X = x;
            translation.Y = y;

            render();
        }

        /// <summary>
        ///     Set Zoom (distance of the Mesh to the Camera).
        /// </summary>
        /// <param name="z"></param>
        public void setZoom(float z)
        {
            if (lockCamera) return;
            zoom = z;

            render();
        }

        /// <summary>
        ///     Set Camera back to zero position.
        /// </summary>
        public void resetCamera()
        {
            translation = Vector2.Zero;
            rotation = Vector2.Zero;
            zoom = 0;

            render();
        }

        /// <summary>
        ///     Wrap a Rotation Angle between -PI and PI.
        /// </summary>
        /// <param name="value">The angle in PI radians</param>
        /// <returns></returns>
        private float wrap(float value)
        {
            return (float)(value - 2 * Math.PI * Math.Floor(value / (2 * Math.PI) + 0.5f));
        }

        /// <summary>
        ///     Rotates a 2-D UV Coordinate around the center.
        /// </summary>
        /// <param name="rotation">The Rotation angle in radians</param>
        /// <returns></returns>
        private Matrix rotateCenter2D(float rotation)
        {
            Matrix output = Matrix.Identity;

            output *= translate2D(new Vector2(-0.5f, -0.5f));
            output *= Matrix.RotationZ(rotation);
            output *= translate2D(new Vector2(0.5f, 0.5f));

            return output;
        }
        /// <summary>
        ///     Builds a 2-D translation Matrix.
        /// </summary>
        /// <param name="translation">Translation vector</param>
        /// <returns>Translation matrix</returns>
        private Matrix translate2D(Vector2 translation)
        {
            Matrix output = Matrix.Identity;

            output.M31 = translation.X;
            output.M32 = translation.Y;

            return output;
        }

        /// <summary>
        ///     Gets a MDX Matrix from a RenderBase Matrix.
        /// </summary>
        /// <param name="mtx">RenderBase matrix</param>
        /// <returns></returns>
        private Matrix getMatrix(RenderBase.OMatrix mtx)
        {
            Matrix output = Matrix.Identity;

            output.M11 = mtx.M11;
            output.M12 = mtx.M12;
            output.M13 = mtx.M13;
            output.M14 = mtx.M14;

            output.M21 = mtx.M21;
            output.M22 = mtx.M22;
            output.M23 = mtx.M23;
            output.M24 = mtx.M24;

            output.M31 = mtx.M31;
            output.M32 = mtx.M32;
            output.M33 = mtx.M33;
            output.M34 = mtx.M34;

            output.M41 = mtx.M41;
            output.M42 = mtx.M42;
            output.M43 = mtx.M43;
            output.M44 = mtx.M44;

            return output;
        }

        /// <summary>
        ///     Creates a Quaternion from a Rotation 3-D Vector.
        /// </summary>
        /// <param name="vector">The rotation vector</param>
        /// <returns></returns>
        private Quaternion getQuaternion(RenderBase.OVector3 vector, bool isAxisAngle)
        {
            Quaternion output = Quaternion.Identity;

            if (isAxisAngle)
            {
                Vector3 v = new Vector3(vector.x, vector.y, vector.z);
                float angle = v.Length();
                v.Normalize();
                output *= Quaternion.RotationAxis(v, angle);
            }
            else
            {
                output *= Quaternion.RotationAxis(new Vector3(1, 0, 0), vector.x);
                output *= Quaternion.RotationAxis(new Vector3(0, 1, 0), vector.y);
                output *= Quaternion.RotationAxis(new Vector3(0, 0, 1), vector.z);
            }

            return output;
        }

        /// <summary>
        ///     Creates a Quaternion from a Quaternion Vector4.
        /// </summary>
        /// <param name="vector">The quaternion vector</param>
        /// <returns></returns>
        private Quaternion getQuaternion(RenderBase.OVector4 vector)
        {
            return new Quaternion(vector.x, vector.y, vector.z, vector.w);
        }

        /// <summary>
        ///     Gets a DirectX Compare from a RenderBase Compare.
        /// </summary>
        /// <param name="function">The compare test function</param>
        /// <returns></returns>
        private Compare getCompare(RenderBase.OTestFunction function)
        {
            switch (function)
            {
                case RenderBase.OTestFunction.always: return Compare.Always;
                case RenderBase.OTestFunction.equal: return Compare.Equal;
                case RenderBase.OTestFunction.greater: return Compare.Greater;
                case RenderBase.OTestFunction.greaterOrEqual: return Compare.GreaterEqual;
                case RenderBase.OTestFunction.less: return Compare.Less;
                case RenderBase.OTestFunction.lessOrEqual: return Compare.LessEqual;
                case RenderBase.OTestFunction.never: return Compare.Never;
                case RenderBase.OTestFunction.notEqual: return Compare.NotEqual;
            }

            return 0;
        }

        /// <summary>
        ///     Gets a DirectX Blend from a RenderBase Blend.
        /// </summary>
        /// <param name="function">The blend test function</param>
        /// <returns></returns>
        private Blend getBlend(RenderBase.OBlendFunction function)
        {
            switch (function)
            {
                case RenderBase.OBlendFunction.constantAlpha: return Blend.Bothsrcalpha;
                case RenderBase.OBlendFunction.constantColor: return Blend.BlendFactor;
                case RenderBase.OBlendFunction.destinationAlpha: return Blend.DestinationAlpha;
                case RenderBase.OBlendFunction.destinationColor: return Blend.DestinationColor;
                case RenderBase.OBlendFunction.one: return Blend.One;
                case RenderBase.OBlendFunction.oneMinusConstantAlpha: return Blend.BothInverseSourceAlpha;
                case RenderBase.OBlendFunction.oneMinusConstantColor: return Blend.InverseBlendFactor;
                case RenderBase.OBlendFunction.oneMinusDestinationAlpha: return Blend.InverseDestinationAlpha;
                case RenderBase.OBlendFunction.oneMinusDestinationColor: return Blend.InverseDestinationColor;
                case RenderBase.OBlendFunction.oneMinusSourceAlpha: return Blend.InverseSourceAlpha;
                case RenderBase.OBlendFunction.oneMinusSourceColor: return Blend.InverseSourceColor;
                case RenderBase.OBlendFunction.sourceAlpha: return Blend.SourceAlpha;
                case RenderBase.OBlendFunction.sourceAlphaSaturate: return Blend.SourceAlphaSaturated;
                case RenderBase.OBlendFunction.sourceColor: return Blend.SourceColor;
                case RenderBase.OBlendFunction.zero: return Blend.Zero;
            }

            return 0;
        }

        /// <summary>
        ///     Gets a DirectX Blend operation from a RenderBase Blend operation.
        /// </summary>
        /// <param name="function">The blend operation</param>
        /// <returns></returns>
        private BlendOperation getBlendOperation(RenderBase.OBlendEquation equation)
        {
            switch (equation)
            {
                case RenderBase.OBlendEquation.add: return BlendOperation.Add;
                case RenderBase.OBlendEquation.max: return BlendOperation.Maximum;
                case RenderBase.OBlendEquation.min: return BlendOperation.Minimum;
                case RenderBase.OBlendEquation.reverseSubtract: return BlendOperation.ReverseSubtract;
                case RenderBase.OBlendEquation.subtract: return BlendOperation.Subtract;
            }

            return 0;
        }

        /// <summary>
        ///     Gets a DirectX Stencil operation from a RenderBase Stencil operation.
        /// </summary>
        /// <param name="function">The stencil operation</param>
        /// <returns></returns>
        private StencilOperation getStencilOperation(RenderBase.OStencilOp operation)
        {
            switch (operation)
            {
                case RenderBase.OStencilOp.decrease: return StencilOperation.Decrement;
                case RenderBase.OStencilOp.decreaseWrap: return StencilOperation.DecrementSaturate;
                case RenderBase.OStencilOp.increase: return StencilOperation.Increment;
                case RenderBase.OStencilOp.increaseWrap: return StencilOperation.IncrementSaturate;
                case RenderBase.OStencilOp.keep: return StencilOperation.Keep;
                case RenderBase.OStencilOp.replace: return StencilOperation.Replace;
                case RenderBase.OStencilOp.zero: return StencilOperation.Zero;
            }

            return 0;
        }

        /// <summary>
        ///     Transforms a Color into a Vector4.
        /// </summary>
        /// <param name="input">The color to be converted</param>
        /// <returns></returns>
        private Vector4 getColor(System.Drawing.Color input)
        {
            return new Vector4((float)input.R / 0xff, (float)input.G / 0xff, (float)input.B / 0xff, (float)input.A / 0xff);
        }

        /// <summary>
        ///     Transforms a Skeleton from relative to absolute positions.
        /// </summary>
        /// <param name="skeleton">The skeleton</param>
        /// <param name="index">Index of the bone to convert</param>
        /// <param name="target">Target matrix to save bone transformation</param>
        private void transformSkeleton(List<RenderBase.OBone> skeleton, int index, ref Matrix target)
        {
            target *= Matrix.RotationX(skeleton[index].rotation.x);
            target *= Matrix.RotationY(skeleton[index].rotation.y);
            target *= Matrix.RotationZ(skeleton[index].rotation.z);
            target *= Matrix.Translation(
                skeleton[index].translation.x,
                skeleton[index].translation.y,
                skeleton[index].translation.z);

            if (skeleton[index].parentId > -1) transformSkeleton(skeleton, skeleton[index].parentId, ref target);
        }

        /// <summary>
        ///     Gets the current frame of a Material Animation color.
        /// </summary>
        /// <param name="data">The animation data</param>
        /// <param name="color">The color where the animation will be applied</param>
        private void getMaterialAnimationColor(RenderBase.OMaterialAnimationData data, ref System.Drawing.Color color)
        {
            float r = AnimationUtils.getKey(data.frameList[0], ctrlMA.Frame);
            float g = AnimationUtils.getKey(data.frameList[1], ctrlMA.Frame);
            float b = AnimationUtils.getKey(data.frameList[2], ctrlMA.Frame);
            float a = AnimationUtils.getKey(data.frameList[3], ctrlMA.Frame);

            byte R = data.frameList[0].exists ? (byte)(r * 0xff) : color.R;
            byte G = data.frameList[1].exists ? (byte)(g * 0xff) : color.G;
            byte B = data.frameList[2].exists ? (byte)(b * 0xff) : color.B;
            byte A = data.frameList[3].exists ? (byte)(a * 0xff) : color.A;

            color = System.Drawing.Color.FromArgb(A, R, G, B);
        }

        /// <summary>
        ///     Gets the current frame of a Material Animation 2-D vector.
        /// </summary>
        /// <param name="data">The animation data</param>
        /// <param name="color">The vector where the animation will be applied</param>
        private void getMaterialAnimationVector2(RenderBase.OMaterialAnimationData data, ref Vector2 vector)
        {
            if (data.frameList[0].exists) vector.X = AnimationUtils.getKey(data.frameList[0], ctrlMA.Frame);
            if (data.frameList[1].exists) vector.Y = AnimationUtils.getKey(data.frameList[1], ctrlMA.Frame);
        }

        /// <summary>
        ///     Gets the current frame of a Material Animation float value.
        /// </summary>
        /// <param name="data">The animation data</param>
        /// <param name="color">The float value where the animation will be applied</param>
        private void getMaterialAnimationFloat(RenderBase.OMaterialAnimationData data, ref float value)
        {
            if (data.frameList[0].exists) value = AnimationUtils.getKey(data.frameList[0], ctrlMA.Frame);
        }

        /// <summary>
        ///     Gets the current frame of a Material Animation integer value.
        /// </summary>
        /// <param name="data">The animation data</param>
        /// <param name="color">The integer value where the animation will be applied</param>
        private void getMaterialAnimationInt(RenderBase.OMaterialAnimationData data, ref int value)
        {
            if (data.frameList[0].exists) value = (int)AnimationUtils.getKey(data.frameList[0], ctrlMA.Frame);
        }
    }
}
