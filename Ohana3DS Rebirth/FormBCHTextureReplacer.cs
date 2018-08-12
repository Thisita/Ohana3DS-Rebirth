﻿using Ohana3DS_Rebirth.Ohana;
using Ohana3DS_Rebirth.Ohana.Models.PICA200;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Windows.Forms;

namespace Ohana3DS_Rebirth
{
    public partial class FormBCHTextureReplacer : Form
    {
        private string CurrentFilename;

        private struct LoadedTexture
        {
            public bool modified;
            public uint gpuCommandsOffset;
            public uint gpuCommandsWordCount;
            public uint offset;
            public int length;
            public RenderBase.OTexture texture;
        }

        private struct LoadedMaterial
        {
            public string texture0;
            public string texture1;
            public string texture2;
            public uint gpuCommandsOffset;
            public uint gpuCommandsWordCount;
        }

        private class LoadedBCH
        {
            public uint mainHeaderOffset;
            public uint gpuCommandsOffset;
            public uint dataOffset;
            public uint relocationTableOffset;
            public uint relocationTableLength;
            public List<LoadedTexture> textures;
            public List<LoadedMaterial> materials;

            public LoadedBCH()
            {
                textures = new List<LoadedTexture>();
                materials = new List<LoadedMaterial>();
            }
        }

        LoadedBCH bch;

        public FormBCHTextureReplacer()
        {
            InitializeComponent();
        }

        private void OpenToolStripMenuItem_Click(object sender, EventArgs e)
        {
            using (OpenFileDialog ofd = new OpenFileDialog() { Filter = "All supported files|*bch", CheckFileExists = true })
            {
                if (ofd.ShowDialog() == DialogResult.OK)
                {
                    if (!Open(ofd.FileName))
                    {
                        MessageBox.Show(this, "Unsupported file format!", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }
                }
            }
        }

        private void SaveToolStripMenuItem_Click(object sender, EventArgs e) => Save();

        private void SaveAndPreviewToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if(bch != null)
            {
                Save();
                ((FormMain)Owner).OpenFile(CurrentFilename);
            }
        }

        private void CloseToolStripMenuItem_Click(object sender, EventArgs e) => Close();

        private void ExportButton_Click(object sender, EventArgs e)
        {
            if (TextureList.SelectedIndex == -1) return;

            using (SaveFileDialog saveDlg = new SaveFileDialog())
            {
                saveDlg.Filter = "Image|*.png";
                if (saveDlg.ShowDialog() == DialogResult.OK)
                {
                    bch.textures[TextureList.SelectedIndex].texture.texture.Save(saveDlg.FileName);
                }
            }
        }

        private void ExportAllButton_Click(object sender, EventArgs e)
        {
            if (TextureList.SelectedIndex == -1) return;

            using (FolderBrowserDialog browserDlg = new FolderBrowserDialog())
            {
                if (browserDlg.ShowDialog() == DialogResult.OK)
                {
                    foreach (LoadedTexture tex in bch.textures)
                    {
                        string outFile = Path.Combine(browserDlg.SelectedPath, tex.texture.name);
                        tex.texture.texture.Save(outFile + ".png");
                    }
                }
            }
        }

        private void ReplaceButton_Click(object sender, EventArgs e)
        {
            if (TextureList.SelectedIndex == -1) return;

            using (OpenFileDialog openDlg = new OpenFileDialog())
            {
                openDlg.Filter = "Image|*.png";
                if (openDlg.ShowDialog() == DialogResult.OK)
                {
                    LoadedTexture tex = bch.textures[TextureList.SelectedIndex];
                    bch.textures.RemoveAt(TextureList.SelectedIndex);
                    Bitmap newTexture = new Bitmap(openDlg.FileName);
                    tex.texture.texture = newTexture;
                    tex.modified = true;
                    bch.textures.Insert(TextureList.SelectedIndex, tex);
                    PreviewPictureBox.Image = newTexture;
                }
            }
        }

        private void ReplaceAllButton_Click(object sender, EventArgs e)
        {
            if (bch == null) return;

            using (FolderBrowserDialog browserDlg = new FolderBrowserDialog())
            {
                if (browserDlg.ShowDialog() == DialogResult.OK)
                {
                    string[] files = Directory.GetFiles(browserDlg.SelectedPath);
                    for (int i = 0; i < bch.textures.Count; i++)
                    {
                        LoadedTexture tex = bch.textures[i];

                        foreach (string file in files)
                        {
                            string name = Path.GetFileNameWithoutExtension(file);

                            if (string.Compare(name, tex.texture.name, StringComparison.InvariantCultureIgnoreCase) == 0)
                            {
                                LoadedTexture newTex = tex;
                                bch.textures.RemoveAt(i);
                                Bitmap newTexture = new Bitmap(file);
                                tex.texture.texture = newTexture;
                                tex.modified = true;
                                bch.textures.Insert(i, tex);
                                if (TextureList.SelectedIndex == i) PreviewPictureBox.Image = newTexture;
                            }
                        }
                    }
                }
            }
        }

        private bool Open(string filename)
        {
            using (FileStream data = new FileStream(filename, FileMode.Open))
            {
                BinaryReader input = new BinaryReader(data);

                string magic = IOUtils.readString(input, 0);
                if (magic != "BCH") return false;
                CurrentFilename = filename;
                data.Seek(4, SeekOrigin.Current);
                byte backwardCompatibility = input.ReadByte();
                byte forwardCompatibility = input.ReadByte();
                ushort version = input.ReadUInt16();

                uint mainHeaderOffset = input.ReadUInt32();
                uint stringTableOffset = input.ReadUInt32();
                uint gpuCommandsOffset = input.ReadUInt32();
                uint dataOffset = input.ReadUInt32();
                uint dataExtendedOffset = backwardCompatibility > 0x20 ? input.ReadUInt32() : 0;
                uint relocationTableOffset = input.ReadUInt32();

                uint mainHeaderLength = input.ReadUInt32();
                uint stringTableLength = input.ReadUInt32();
                uint gpuCommandsLength = input.ReadUInt32();
                uint dataLength = input.ReadUInt32();
                uint dataExtendedLength = backwardCompatibility > 0x20 ? input.ReadUInt32() : 0;
                uint relocationTableLength = input.ReadUInt32();

                data.Seek(mainHeaderOffset, SeekOrigin.Begin);
                uint modelsPointerTableOffset = input.ReadUInt32() + mainHeaderOffset;
                uint modelsPointerTableEntries = input.ReadUInt32();

                data.Seek(mainHeaderOffset + 0x24, SeekOrigin.Begin);
                uint texturesPointerTableOffset = input.ReadUInt32() + mainHeaderOffset;
                uint texturesPointerTableEntries = input.ReadUInt32();

                bch = new LoadedBCH();

                //Textures
                for (int index = 0; index < texturesPointerTableEntries; index++)
                {
                    data.Seek(texturesPointerTableOffset + (index * 4), SeekOrigin.Begin);
                    data.Seek(input.ReadUInt32() + mainHeaderOffset, SeekOrigin.Begin);

                    LoadedTexture tex;
                    tex.modified = false;
                    tex.gpuCommandsOffset = input.ReadUInt32() + gpuCommandsOffset;
                    tex.gpuCommandsWordCount = input.ReadUInt32();
                    data.Seek(0x14, SeekOrigin.Current);
                    uint textureNameOffset = input.ReadUInt32();
                    string textureName = IOUtils.readString(input, textureNameOffset + stringTableOffset);

                    data.Seek(tex.gpuCommandsOffset, SeekOrigin.Begin);
                    PICACommandReader textureCommands = new PICACommandReader(data, tex.gpuCommandsWordCount);

                    tex.offset = textureCommands.getTexUnit0Address() + dataOffset;
                    RenderBase.OTextureFormat fmt = textureCommands.getTexUnit0Format();
                    Size textureSize = textureCommands.getTexUnit0Size();
                    switch (fmt)
                    {
                        case RenderBase.OTextureFormat.rgba8: tex.length = (textureSize.Width * textureSize.Height) * 4; break;
                        case RenderBase.OTextureFormat.rgb8: tex.length = (textureSize.Width * textureSize.Height) * 3; break;
                        case RenderBase.OTextureFormat.rgba5551: tex.length = (textureSize.Width * textureSize.Height) * 2; break;
                        case RenderBase.OTextureFormat.rgb565: tex.length = (textureSize.Width * textureSize.Height) * 2; break;
                        case RenderBase.OTextureFormat.rgba4: tex.length = (textureSize.Width * textureSize.Height) * 2; break;
                        case RenderBase.OTextureFormat.la8: tex.length = (textureSize.Width * textureSize.Height) * 2; break;
                        case RenderBase.OTextureFormat.hilo8: tex.length = (textureSize.Width * textureSize.Height) * 2; break;
                        case RenderBase.OTextureFormat.l8: tex.length = textureSize.Width * textureSize.Height; break;
                        case RenderBase.OTextureFormat.a8: tex.length = textureSize.Width * textureSize.Height; break;
                        case RenderBase.OTextureFormat.la4: tex.length = textureSize.Width * textureSize.Height; break;
                        case RenderBase.OTextureFormat.l4: tex.length = (textureSize.Width * textureSize.Height) >> 1; break;
                        case RenderBase.OTextureFormat.a4: tex.length = (textureSize.Width * textureSize.Height) >> 1; break;
                        case RenderBase.OTextureFormat.etc1: tex.length = (textureSize.Width * textureSize.Height) >> 1; break;
                        case RenderBase.OTextureFormat.etc1a4: tex.length = textureSize.Width * textureSize.Height; break;
                        default: throw new Exception("OBCHTextureReplacer: Invalid texture format on BCH!");
                    }

                    while ((tex.length & 0x7f) > 0) tex.length++;

                    data.Seek(tex.offset, SeekOrigin.Begin);
                    byte[] buffer = new byte[textureSize.Width * textureSize.Height * 4];
                    input.Read(buffer, 0, buffer.Length);
                    Bitmap texture = TextureCodec.decode(
                        buffer,
                        textureSize.Width,
                        textureSize.Height,
                        fmt);

                    tex.texture = new RenderBase.OTexture(texture, textureName);

                    bch.textures.Add(tex);
                }

                //Materials
                for (int mdlIndex = 0; mdlIndex < modelsPointerTableEntries; mdlIndex++)
                {
                    data.Seek(modelsPointerTableOffset + (mdlIndex * 4), SeekOrigin.Begin);
                    data.Seek(input.ReadUInt32() + mainHeaderOffset, SeekOrigin.Begin);
                    data.Seek(0x34, SeekOrigin.Current);

                    uint materialsTableOffset = input.ReadUInt32() + mainHeaderOffset;
                    uint materialsTableEntries = input.ReadUInt32();

                    for (int index = 0; index < materialsTableEntries; index++)
                    {
                        if (backwardCompatibility < 0x21)
                            data.Seek(materialsTableOffset + (index * 0x58), SeekOrigin.Begin);
                        else
                            data.Seek(materialsTableOffset + (index * 0x2c), SeekOrigin.Begin);

                        LoadedMaterial mat;

                        data.Seek(0x10, SeekOrigin.Current);
                        mat.gpuCommandsOffset = input.ReadUInt32() + gpuCommandsOffset;
                        mat.gpuCommandsWordCount = input.ReadUInt32();

                        if (backwardCompatibility < 0x21)
                            data.Seek(0x30, SeekOrigin.Current);
                        else
                            data.Seek(4, SeekOrigin.Current);

                        uint texture0Offset = input.ReadUInt32() + stringTableOffset;
                        uint texture1Offset = input.ReadUInt32() + stringTableOffset;
                        uint texture2Offset = input.ReadUInt32() + stringTableOffset;

                        mat.texture0 = IOUtils.readString(input, texture0Offset);
                        mat.texture1 = IOUtils.readString(input, texture1Offset);
                        mat.texture2 = IOUtils.readString(input, texture2Offset);

                        bch.materials.Add(mat);
                    }
                }

                bch.mainHeaderOffset = mainHeaderOffset;
                bch.gpuCommandsOffset = gpuCommandsOffset;
                bch.dataOffset = dataOffset;
                bch.relocationTableOffset = relocationTableOffset;
                bch.relocationTableLength = relocationTableLength;
            }

            UpdateTexturesList();
            return true;
        }

        private void Save()
        {
            if(bch == null)
            {
                return;
            }
            using (FileStream data = new FileStream(CurrentFilename, FileMode.Open))
            {
                BinaryReader input = new BinaryReader(data);
                BinaryWriter output = new BinaryWriter(data);

                for (int i = 0; i < bch.textures.Count; i++)
                {
                    LoadedTexture tex = bch.textures[i];

                    if (tex.modified)
                    {
                        byte[] buffer = Align(TextureCodec.encode(tex.texture.texture, RenderBase.OTextureFormat.rgba8));
                        int diff = buffer.Length - tex.length;

                        ReplaceData(data, tex.offset, tex.length, buffer);

                        //Update offsets of next textures
                        tex.length = buffer.Length;
                        tex.modified = false;
                        UpdateTexture(i, tex);
                        for (int j = i; j < bch.textures.Count; j++)
                        {
                            LoadedTexture next = bch.textures[j];
                            next.offset = (uint)(next.offset + diff);
                            UpdateTexture(j, next);
                        }

                        //Update all addresses poiting after the replaced data
                        bch.relocationTableOffset = (uint)(bch.relocationTableOffset + diff);
                        for (int index = 0; index < bch.relocationTableLength; index += 4)
                        {
                            data.Seek(bch.relocationTableOffset + index, SeekOrigin.Begin);
                            uint value = input.ReadUInt32();
                            uint offset = value & 0x1ffffff;
                            byte flags = (byte)(value >> 25);

                            if ((flags & 0x20) > 0 || flags == 7 || flags == 0xc)
                            {
                                if ((flags & 0x20) > 0)
                                    data.Seek((offset * 4) + bch.gpuCommandsOffset, SeekOrigin.Begin);
                                else
                                    data.Seek((offset * 4) + bch.mainHeaderOffset, SeekOrigin.Begin);

                                uint address = input.ReadUInt32();
                                if (address + bch.dataOffset > tex.offset)
                                {
                                    address = (uint)(address + diff);
                                    data.Seek(-4, SeekOrigin.Current);
                                    output.Write(address);
                                }
                            }
                        }

                        uint newSize = (uint)((tex.texture.texture.Width << 16) | tex.texture.texture.Height);

                        //Update texture format
                        data.Seek(tex.gpuCommandsOffset, SeekOrigin.Begin);
                        for (int index = 0; index < tex.gpuCommandsWordCount * 3; index++)
                        {
                            uint command = input.ReadUInt32();

                            switch (command)
                            {
                                case 0xf008e:
                                case 0xf0096:
                                case 0xf009e:
                                    ReplaceCommand(data, output, 0); //Set texture format to 0 = RGBA8888
                                    break;
                                case 0xf0082:
                                case 0xf0092:
                                case 0xf009a:
                                    ReplaceCommand(data, output, newSize); //Set new texture size
                                    break;
                            }
                        }

                        //Update material texture format
                        foreach (LoadedMaterial mat in bch.materials)
                        {
                            data.Seek(mat.gpuCommandsOffset, SeekOrigin.Begin);
                            for (int index = 0; index < mat.gpuCommandsWordCount; index++)
                            {
                                uint command = input.ReadUInt32();

                                switch (command)
                                {
                                    case 0xf008e: if (mat.texture0 == tex.texture.name || mat.texture0 == "") ReplaceCommand(data, output, 0); break;
                                    case 0xf0096: if (mat.texture1 == tex.texture.name || mat.texture1 == "") ReplaceCommand(data, output, 0); break;
                                    case 0xf009e: if (mat.texture2 == tex.texture.name || mat.texture2 == "") ReplaceCommand(data, output, 0); break;
                                }
                            }
                        }

                        //Patch up BCH header for new offsets and lengths
                        data.Seek(4, SeekOrigin.Begin);
                        byte backwardCompatibility = input.ReadByte();
                        byte forwardCompatibility = input.ReadByte();

                        //Update Data Extended and Relocation Table offsets
                        data.Seek(18, SeekOrigin.Current);
                        if (backwardCompatibility > 0x20) UpdateAddress(data, input, output, diff);
                        UpdateAddress(data, input, output, diff);

                        //Update data length
                        data.Seek(12, SeekOrigin.Current);
                        UpdateAddress(data, input, output, diff);
                    }
                }
            }

            MessageBox.Show(this, "Done!", "Information", MessageBoxButtons.OK, MessageBoxIcon.Information);
        }

        private byte[] Align(byte[] input)
        {
            int length = input.Length;
            while ((length & 0x7f) > 0) length++;
            byte[] output = new byte[length];
            Buffer.BlockCopy(input, 0, output, 0, input.Length);
            return output;
        }

        private void ReplaceCommand(Stream data, BinaryWriter output, uint newVal)
        {
            data.Seek(-8, SeekOrigin.Current);
            output.Write(newVal);
            data.Seek(4, SeekOrigin.Current);
        }

        private void ReplaceData(Stream data, uint offset, int length, byte[] newData)
        {
            data.Seek(offset + length, SeekOrigin.Begin);
            byte[] after = new byte[data.Length - data.Position];
            data.Read(after, 0, after.Length);
            data.SetLength(offset);
            data.Seek(offset, SeekOrigin.Begin);
            data.Write(newData, 0, newData.Length);
            data.Write(after, 0, after.Length);
        }

        private void UpdateAddress(Stream data, BinaryReader input, BinaryWriter output, int diff)
        {
            uint offset = input.ReadUInt32();
            offset = (uint)(offset + diff);
            data.Seek(-4, SeekOrigin.Current);
            output.Write(offset);
        }

        private void UpdateTexture(int index, LoadedTexture newTex)
        {
            bch.textures.RemoveAt(index);
            bch.textures.Insert(index, newTex);
        }

        private void UpdateTexturesList()
        {
            TextureList.Items.Clear();
            PreviewPictureBox.Image = null;
            foreach (LoadedTexture tex in bch.textures)
            {
                TextureList.Items.Add(tex.texture.name);
            }

            TextureList.Refresh();
        }

        private void TextureList_SelectedIndexChanged(object sender, EventArgs e)
        {

            if (TextureList.SelectedIndex == -1)
            {
                return;
            }

            PreviewPictureBox.Image = bch.textures[TextureList.SelectedIndex].texture.texture;
        }
    }
}
