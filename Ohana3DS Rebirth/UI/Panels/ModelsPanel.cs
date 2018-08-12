using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Ohana3DS_Rebirth.Ohana;

namespace Ohana3DS_Rebirth.UI.Panels
{
    public partial class ModelsPanel : UserControl, IRenderEnginePanel
    {
        private RenderEngine renderer;

        public ModelsPanel()
        {
            InitializeComponent();
        }

        public void Render(RenderEngine renderEngine)
        {
            renderer = renderEngine;
            UpdateModelList();
        }

        private void UpdateModelList()
        {
            ModelListBox.Items.Clear();
            foreach (RenderBase.OModel model in renderer.models.model)
            {
                ModelListBox.Items.Add(model.name);
            }

            if (ModelListBox.Items.Count > 0)
            {
                ModelListBox.SelectedIndex = 0;
            }

            ModelListBox.Refresh();
        }

        private void ModelListBox_SelectedIndexChanged(object sender, EventArgs e)
        {
            renderer.CurrentModel = ModelListBox.SelectedIndex;
        }

        private void ExportButton_Click(object sender, EventArgs e) => FileIO.export(FileIO.fileType.model, renderer.models, ModelListBox.SelectedIndex);

        private void ImportButton_Click(object sender, EventArgs e)
        {
            object importedData = FileIO.import(FileIO.fileType.model);
            if (importedData != null)
            {
                renderer.models.model.AddRange((List<RenderBase.OModel>)importedData);
                foreach (RenderBase.OModel model in (List<RenderBase.OModel>)importedData)
                {
                    ModelListBox.Items.Add(model.name);
                }

                ModelListBox.Refresh();
            }
        }

        private void DeleteButton_Click(object sender, EventArgs e)
        {
            if (ModelListBox.SelectedIndex == -1) return;

            renderer.models.model.RemoveAt(ModelListBox.SelectedIndex);
            renderer.CurrentModel = ModelListBox.SelectedIndex;

            ModelListBox.Items.RemoveAt(ModelListBox.SelectedIndex);
        }

        private void ClearButton_Click(object sender, EventArgs e)
        {
            renderer.CurrentModel = -1;
            renderer.models.model.Clear();
            UpdateModelList();
        }
    }
}
