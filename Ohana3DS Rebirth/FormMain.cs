using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Ohana3DS_Rebirth
{
    public partial class FormMain : Form
    {
        public FormMain()
        {
            InitializeComponent();
        }

        private void aboutToolStripMenuItem_Click(object sender, EventArgs e) => MessageBox.Show("Ohana3DS Rebirth made by gdkchan and edited by thisita", "About", MessageBoxButtons.OK, MessageBoxIcon.Information);

        private void openToolStripMenuItem_Click(object sender, EventArgs e)
        {
            using(OpenFileDialog ofn = new OpenFileDialog() { Filter = "All Files (*.*)|*.*"})
            {
                if(ofn.ShowDialog() == DialogResult.OK)
                {
                    OpenFile(ofn.FileName);
                }
            }
        }

        private void OpenFile(string filename)
        {

        }

        private void exitToolStripMenuItem_Click(object sender, EventArgs e) => Application.Exit();

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
            Properties.Settings.Default.reBackgroundColor = value);
            Properties.Settings.Default.Save();
        }

        private void FormMain_DragDrop(object sender, DragEventArgs e)
        {
            string[] files = (string[])e.Data.GetData(DataFormats.FileDrop, false);
            if(files.Length == 1)
            {
                OpenFile(files[0]);
            } else if(files.Length > 1)
            {
                MessageBox.Show("Multi-file load is not supported", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            } else
            {
                MessageBox.Show("Unsupported operation", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
    }
}
