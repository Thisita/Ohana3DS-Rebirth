using Ohana3DS_Rebirth.Ohana;
using Ohana3DS_Rebirth.Ohana.Models;
using Ohana3DS_Rebirth.Ohana.Models.GenericFormats;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Ohana3DS_Rebirth
{
    public partial class FormSm4shModelCreator : Form
    {
        public FormSm4shModelCreator()
        {
            InitializeComponent();
        }

        private void InputOpenFileButton_Click(object sender, EventArgs e)
        {
            using (OpenFileDialog ofd = new OpenFileDialog() { Filter = "Supported formats|*.obj;*.smd" })
            {
                if (ofd.ShowDialog() == DialogResult.OK)
                {
                    InputFileTextBox.Text = ofd.FileName;
                }
            }
        }

        private void OutputFileOpenButton_Click(object sender, EventArgs e)
        {
            using (OpenFileDialog ofd = new OpenFileDialog() { Filter = "Sm4sh model|*.mbn" })
            {
                if (ofd.ShowDialog() == DialogResult.OK)
                {
                    OutputFileTextBox.Text = ofd.FileName;
                }
            }
        }

        private void CreateButton_Click(object sender, EventArgs e)
        {
            if(Create(InputFileTextBox.Text, OutputFileTextBox.Text))
            {
                MessageBox.Show(this, "Done!", "Information", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
        }

        private void CloseButton_Click(object sender, EventArgs e)
        {
            Close();
        }

        private bool Create(string inputFilename, string outputFilename)
        {
            if (!File.Exists(inputFilename)) return false;

            RenderBase.OModelGroup models;
            switch (Path.GetExtension(inputFilename).ToLower())
            {
                case ".obj": models = OBJ.import(inputFilename); break;
                case ".smd": models = SMD.import(inputFilename); break;
                default:
                    MessageBox.Show(this, "Unsupported model format!", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return false;
            }

            RenderBase.OModel model = models.model[0];

            using (MemoryStream output = new MemoryStream())
            {
                BinaryWriter writer = new BinaryWriter(output);

                writer.Write((ushort)5);
                writer.Write((ushort)0xffff);
                writer.Write((uint)4);
                writer.Write((uint)0);
                writer.Write(model.mesh.Count);

                List<Mesh> meshes = new List<Mesh>();
                foreach (RenderBase.OMesh mesh in model.mesh) meshes.Add(CreateMesh(mesh));

                foreach (Mesh m in meshes)
                {
                    //Face
                    writer.Write((uint)1); //Faces count
                    writer.Write(m.descriptor.nodes.Count);
                    foreach (uint n in m.descriptor.nodes) writer.Write(n);
                    writer.Write(m.indexBuffer.Length / 2);

                    //Vertex
                    writer.Write(m.descriptor.attributes.Count);
                    foreach (AttributeDescriptor att in m.descriptor.attributes)
                    {
                        writer.Write(att.type);
                        writer.Write(att.format);
                        writer.Write(att.scale);
                    }
                    writer.Write(m.vertexBuffer.Length);
                }

                Align(output);
                foreach (Mesh m in meshes)
                {
                    writer.Write(m.vertexBuffer);
                    Align(output);

                    writer.Write(m.indexBuffer);
                    Align(output);
                }

                File.WriteAllBytes(outputFilename, output.ToArray());
            }

            return true;
        }

        private void Align(Stream data)
        {
            while ((data.Position & 0x1f) != 0) data.WriteByte(0xff);
        }

        private class AttributeDescriptor
        {
            public uint type;
            public uint format;
            public float scale;

            public AttributeDescriptor(uint _type, uint _format, float _scale)
            {
                type = _type;
                format = _format;
                scale = _scale;
            }
        }

        private struct MeshDescriptor
        {
            public List<uint> nodes;
            public List<AttributeDescriptor> attributes;
        }

        private struct Mesh
        {
            public MeshDescriptor descriptor;
            public byte[] vertexBuffer;
            public byte[] indexBuffer;
        }

        private Mesh CreateMesh(RenderBase.OMesh input)
        {
            Mesh output;

            output.descriptor.nodes = new List<uint>();
            output.descriptor.attributes = new List<AttributeDescriptor>
            {
                new AttributeDescriptor(0, 0, 1f) //Position
            };
            if (input.hasNormal) output.descriptor.attributes.Add(new AttributeDescriptor(1, 0, 1f));
            if (input.texUVCount > 0) output.descriptor.attributes.Add(new AttributeDescriptor(3, 0, 1f));
            if (input.hasNode) output.descriptor.attributes.Add(new AttributeDescriptor(5, 1, 1f));
            if (input.hasWeight) output.descriptor.attributes.Add(new AttributeDescriptor(6, 1, 0.00392156862f));

            MeshUtils.optimizedMesh optimized = MeshUtils.optimizeMesh(input);
            using (MemoryStream vertexStream = new MemoryStream())
            {
                BinaryWriter writer = new BinaryWriter(vertexStream);

                foreach (RenderBase.OVertex vtx in optimized.vertices)
                {
                    writer.Write(vtx.position.x);
                    writer.Write(vtx.position.y);
                    writer.Write(vtx.position.z);

                    if (optimized.hasNormal)
                    {
                        writer.Write(vtx.normal.x);
                        writer.Write(vtx.normal.y);
                        writer.Write(vtx.normal.z);
                    }

                    if (optimized.texUVCount > 0)
                    {
                        writer.Write(vtx.texture0.x);
                        writer.Write(vtx.texture0.y);
                    }

                    if (optimized.hasNode)
                    {
                        for (int i = 0; i < 2; i++)
                        {
                            if (i < vtx.node.Count)
                            {
                                int nodeIndex = output.descriptor.nodes.IndexOf((uint)vtx.node[i]);
                                if (nodeIndex == -1)
                                {
                                    writer.Write((byte)output.descriptor.nodes.Count);
                                    output.descriptor.nodes.Add((uint)vtx.node[i]);
                                }
                                else
                                    writer.Write((byte)nodeIndex);
                            }
                            else
                                writer.Write((byte)0);
                        }
                    }

                    if (optimized.hasWeight)
                    {
                        for (int i = 0; i < 2; i++)
                        {
                            if (i < vtx.weight.Count)
                                writer.Write((byte)(vtx.weight[i] * byte.MaxValue));
                            else
                                writer.Write((byte)0);
                        }
                    }
                }

                output.vertexBuffer = vertexStream.ToArray();
            }

            using (MemoryStream indexStream = new MemoryStream())
            {
                BinaryWriter writer = new BinaryWriter(indexStream);
                foreach (uint index in optimized.indices) writer.Write((ushort)index);
                output.indexBuffer = indexStream.ToArray();
            }

            return output;
        }
    }
}
