using System;
using System.Windows.Forms;
using Ohana3DS_Rebirth.Ohana;

namespace Ohana3DS_Rebirth.UI.Panels
{
    public partial class ImagePanel : UserControl, IFileRenderPanel
    {
        private string TextureName;
        public ImagePanel()
        {
            InitializeComponent();
        }

        public void RenderFile(object data)
        {
            RenderBase.OTexture texture = (RenderBase.OTexture)data;
            TextureName = texture.name;
            TexturePreview.BackgroundImage = texture.texture;
        }

        private void ExportButton_Click(object sender, EventArgs e)
        {
            using (SaveFileDialog saveDlg = new SaveFileDialog() { Title = "Export Texture", FileName = TextureName, Filter = "PNG Image|*.png" })
            {
                if (saveDlg.ShowDialog() == DialogResult.OK)
                {
                    TexturePreview.BackgroundImage.Save(saveDlg.FileName);
                }
            }
        }
    }
}
