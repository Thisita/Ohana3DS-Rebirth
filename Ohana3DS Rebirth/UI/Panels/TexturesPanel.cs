using System;
using System.Collections.Generic;
using System.Windows.Forms;
using Ohana3DS_Rebirth.Ohana;

namespace Ohana3DS_Rebirth.UI.Panels
{
    public partial class TexturesPanel : UserControl, IRenderEnginePanel, IFileRenderPanel
    {
        private RenderEngine renderer;

        public TexturesPanel()
        {
            InitializeComponent();
        }

        public void Render(RenderEngine renderEngine)
        {
            renderer = renderEngine;
            UpdateTextureList(renderer.models.texture);
        }

        public void RenderFile(object data)
        {
            UpdateTextureList((List<RenderBase.OTexture>)data);
        }

        private void UpdateTextureList(List<RenderBase.OTexture> textures)
        {
            TexturesListView.Items.Clear();
            TexturesListView.LargeImageList.Images.Clear();
            TexturesListView.SmallImageList.Images.Clear();
            foreach (RenderBase.OTexture texture in textures)
            {
                TexturesListView.Items.Add(texture.name);
                TexturesListView.LargeImageList.Images.Add(texture.texture);
                TexturesListView.SmallImageList.Images.Add(texture.texture);
            }
            TexturesListView.Refresh();
        }

        private void ExportButton_Click(object sender, EventArgs e) => FileIO.export(FileIO.fileType.texture, renderer.models, TexturesListView.SelectedIndices[0]);

        private void ImportButton_Click(object sender, EventArgs e)
        {
            object importedData = FileIO.import(FileIO.fileType.texture);
            if (importedData != null)
            {
                if (renderer != null)
                {
                    renderer.addTextureRange((List<RenderBase.OTexture>)importedData);
                }

                foreach (RenderBase.OTexture texture in (List<RenderBase.OTexture>)importedData)
                {
                    TexturesListView.Items.Add(texture.name);
                    TexturesListView.LargeImageList.Images.Add(texture.texture);
                    TexturesListView.SmallImageList.Images.Add(texture.texture);
                }

                TexturesListView.Refresh();
            }
        }

        private void DeleteButton_Click(object sender, EventArgs e)
        {
            int index = TexturesListView.SelectedIndices[0];
            if (index == -1)
            {
                return;
            }

            if (renderer != null)
            {
                renderer.removeTexture(index);
            }

            //Note: The SelectedIndex will change after this is called, so don't add it before the removeTexture!
            TexturesListView.Items.RemoveAt(index);
            TexturesListView.LargeImageList.Images.RemoveAt(index);
            TexturesListView.SmallImageList.Images.RemoveAt(index);
        }

        private void ClearButton_Click(object sender, EventArgs e)
        {
            TexturesListView.Items.Clear();
            TexturesListView.SmallImageList.Images.Clear();
            TexturesListView.LargeImageList.Images.Clear();
            if (renderer != null)
            {
                renderer.removeAllTextures();
            }
        }
    }
}
