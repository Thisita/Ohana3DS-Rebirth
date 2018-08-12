using Ohana3DS_Rebirth.GUI;
using Ohana3DS_Rebirth.Ohana;
using Ohana3DS_Rebirth.UI.Panels;
using System;
using System.Drawing;
using System.Windows.Forms;

namespace Ohana3DS_Rebirth
{
    public partial class FormMain : Form
    {
        FileIO.formatType currentFormat;

        public FormMain()
        {
            InitializeComponent();
        }

        private void AboutToolStripMenuItem_Click(object sender, EventArgs e) => MessageBox.Show(this, "Ohana3DS Rebirth made by gdkchan and edited by thisita", "About", MessageBoxButtons.OK, MessageBoxIcon.Information);

        private void OpenToolStripMenuItem_Click(object sender, EventArgs e)
        {
            using(OpenFileDialog ofn = new OpenFileDialog() { Filter = "All Files (*.*)|*.*"})
            {
                if(ofn.ShowDialog() == DialogResult.OK)
                {
                    OpenFile(ofn.FileName);
                }
            }
        }

        private void FormMain_DragDrop(object sender, DragEventArgs e)
        {
            string[] files = (string[])e.Data.GetData(DataFormats.FileDrop, false);
            if (files.Length == 1)
            {
                OpenFile(files[0]);
            }
            else if (files.Length > 1)
            {
                MessageBox.Show(this, "Multi-file load is not supported", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            else
            {
                MessageBox.Show(this, "Unsupported operation", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        public void OpenFile(string filename)
        {
            if(ContentContainer.Controls.Count > 0)
            {
                ContentContainer.Controls.Clear();
            }

            try
            {
                FileIO.file file = FileIO.load(filename);
                currentFormat = file.type;

                if (currentFormat != FileIO.formatType.unsupported)
                {
                    switch (currentFormat)
                    {
                        case FileIO.formatType.container:
                            ContentContainer.Controls.Add(new OContainerPanel());
                            break;
                        case FileIO.formatType.image:
                            ContentContainer.Controls.Add(new OImagePanel());
                            break;
                        case FileIO.formatType.model:
                            ContentContainer.Controls.Add(new ViewportPanel());
                            break;
                        case FileIO.formatType.texture:
                            ContentContainer.Controls.Add(new TexturesPanel());
                            break;
                    }

                    ContentContainer.Controls[0].Dock = DockStyle.Fill;
                    ((IFileRenderPanel)ContentContainer.Controls[0]).RenderFile(file.data);
                }
                else
                    MessageBox.Show(this, "Unsupported file format!", "Error", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);

            }
            catch (Exception)
            {
                MessageBox.Show(this, "Unsupported file format!", "Error", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);

                if (ContentContainer.Controls.Count > 0)
                {
                    ContentContainer.Controls.Clear();
                }
            }
        }

        private void ExitToolStripMenuItem_Click(object sender, EventArgs e) => Close();

        private void BCHTextureReplacerToolStripMenuItem_Click(object sender, EventArgs e) => new FormBCHTextureReplacer().Show(this);
        private void Sm4shModelCreatorToolStripMenuItem_Click(object sender, EventArgs e) => new FormSm4shModelCreator().Show(this);

        private void AANoneToolStripMenuItem_Click(object sender, EventArgs e) => UpdateAA((ToolStripMenuItem)sender, 0);
        private void AA2xToolStripMenuItem_Click(object sender, EventArgs e) => UpdateAA((ToolStripMenuItem)sender, 2);
        private void AA4xToolStripMenuItem_Click(object sender, EventArgs e) => UpdateAA((ToolStripMenuItem)sender, 4);
        private void AA8xToolStripMenuItem_Click(object sender, EventArgs e) => UpdateAA((ToolStripMenuItem)sender, 8);
        private void AA16xToolStripMenuItem_Click(object sender, EventArgs e) => UpdateAA((ToolStripMenuItem)sender, 16);

        private void UpdateAA(ToolStripMenuItem sender, int value)
        {
            AANoneToolStripMenuItem.Checked = AA2xToolStripMenuItem.Checked = AA4xToolStripMenuItem.Checked = AA8xToolStripMenuItem.Checked = AA16xToolStripMenuItem.Checked = false;
            sender.Checked = true;
            Properties.Settings.Default.reAntiAlias = value;
            Properties.Settings.Default.Save();
            menuStrip1.Invalidate();
        }

        private void BackgroundBlackToolStripMenuItem_Click(object sender, EventArgs e) => UpdateBackgound((ToolStripMenuItem)sender, Color.Black.ToArgb());
        private void BackgroundGrayToolStripMenuItem_Click(object sender, EventArgs e) => UpdateBackgound((ToolStripMenuItem)sender, Color.DimGray.ToArgb());
        private void BackgroundWhiteToolStripMenuItem_Click(object sender, EventArgs e) => UpdateBackgound((ToolStripMenuItem)sender, Color.White.ToArgb());

        private void BackgroundCustomToolStripMenuItem_Click(object sender, EventArgs e)
        {
            using (ColorDialog colorDialog = new ColorDialog())
            {
                if(colorDialog.ShowDialog() == DialogResult.OK)
                {
                    UpdateBackgound((ToolStripMenuItem)sender, colorDialog.Color.ToArgb());
                }
            }
        }

        private void UpdateBackgound(ToolStripMenuItem sender, int value)
        {
            BackgroundBlackToolStripMenuItem.Checked = BackgroundGrayToolStripMenuItem.Checked = BackgroundWhiteToolStripMenuItem.Checked = BackgroundCustomToolStripMenuItem.Checked = false;
            sender.Checked = true;
            Properties.Settings.Default.reBackgroundColor = value;
            Properties.Settings.Default.Save();
            menuStrip1.Invalidate();
        }
    }
}
