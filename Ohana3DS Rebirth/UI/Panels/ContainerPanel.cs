using System;
using System.Windows.Forms;
using Ohana3DS_Rebirth.Ohana.Containers;
using System.IO;
using Ohana3DS_Rebirth.Ohana.Compressions;

namespace Ohana3DS_Rebirth.UI.Panels
{
    public partial class ContainerPanel : UserControl, IFileRenderPanel
    {
        private OContainer container;

        public ContainerPanel()
        {
            InitializeComponent();
        }

        public void RenderFile(object data)
        {
            container = (OContainer)data;
            foreach (OContainer.fileEntry file in container.content)
            {
                uint length = file.loadFromDisk ? file.fileLength : (uint)file.data.Length;
                FileListBox.Items.Add($"{file.name} ({GetLength(length)})");
            }
            FileListBox.Refresh();
        }

        private void ExportAllButton_Click(object sender, EventArgs e)
        {
            using (FolderBrowserDialog fbd = new FolderBrowserDialog())
            {
                fbd.Description = "Export all files";
                if (fbd.ShowDialog() == DialogResult.OK)
                {
                    foreach (OContainer.fileEntry file in container.content)
                    {
                        string fileName = Path.Combine(fbd.SelectedPath, file.name);
                        string dir = Path.GetDirectoryName(fileName);
                        if (!Directory.Exists(dir)) Directory.CreateDirectory(dir);

                        byte[] buffer;

                        if (file.loadFromDisk)
                        {
                            buffer = new byte[file.fileLength];
                            container.data.Seek(file.fileOffset, SeekOrigin.Begin);
                            container.data.Read(buffer, 0, buffer.Length);
                        }
                        else
                            buffer = file.data;

                        if (file.doDecompression)
                        {
                            buffer = LZSS_Ninty.decompress(buffer);
                        }

                        File.WriteAllBytes(fileName, buffer);
                    }
                }
            }
        }

        private void ExportButton_Click(object sender, EventArgs e)
        {
            if (FileListBox.SelectedIndex == -1)
            {
                return;
            }

            using (SaveFileDialog sfd = new SaveFileDialog() { Title = "Export File", Filter = "All Files|*.*", FileName = container.content[FileListBox.SelectedIndex].name })
            {
                if (sfd.ShowDialog() == DialogResult.OK)
                {
                    OContainer.fileEntry file = container.content[FileListBox.SelectedIndex];

                    byte[] buffer;

                    if (file.loadFromDisk)
                    {
                        buffer = new byte[file.fileLength];
                        container.data.Seek(file.fileOffset, SeekOrigin.Begin);
                        container.data.Read(buffer, 0, buffer.Length);
                    }
                    else
                        buffer = file.data;

                    if (file.doDecompression)
                    {
                        buffer = LZSS_Ninty.decompress(buffer);
                    }

                    File.WriteAllBytes(sfd.FileName, buffer);
                }
            }
        }

        private readonly string[] lengthUnits = { "bytes", "kB", "MB", "GB", "TB" };
        private string GetLength(uint length)
        {
            int i = 0;
            while (length > 0x400)
            {
                length /= 0x400;
                i++;
            }

            return $"{length} {lengthUnits[i]}";
        }
    }
}
