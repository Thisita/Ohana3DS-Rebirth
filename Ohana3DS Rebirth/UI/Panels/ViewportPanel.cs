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
    public partial class ViewportPanel : UserControl, IFileRenderPanel
    {
        private RenderEngine renderer;

        public ViewportPanel()
        {
            InitializeComponent();
        }

        public void RenderFile(object data)
        {
            renderer = new RenderEngine
            {
                models = (RenderBase.OModelGroup)data
            };
            renderer.initialize(Screen.Handle, Screen.Width, Screen.Height);

            ModelsPagePanel.Render(renderer);
            //TexturesPanel.launch(renderer);
            //SkeletalAnimationsPanel.launch(renderer, FileIO.fileType.skeletalAnimation);
            //MaterialAnimationsPanel.launch(renderer, FileIO.fileType.materialAnimation);
            //VisibilityAnimationsPanel.launch(renderer, FileIO.fileType.visibilityAnimation);

            renderer.render();
        }
    }
}
