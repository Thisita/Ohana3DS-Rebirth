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
    public partial class AnimationsPanel : UserControl, IRenderEnginePanel
    {
        private RenderEngine renderer;
        private RenderEngine.animationControl control;
        private RenderBase.OAnimationListBase animations;
        protected FileIO.fileType type;

        public AnimationsPanel(FileIO.fileType fileType)
        {
            type = fileType;
            InitializeComponent();
        }

        public void Render(RenderEngine renderEngine)
        {
            renderer = renderEngine;
            switch (type)
            {
                case FileIO.fileType.skeletalAnimation:
                    control = renderer.ctrlSA;
                    animations = renderer.models.skeletalAnimation;
                    break;
                case FileIO.fileType.materialAnimation:
                    control = renderer.ctrlMA;
                    animations = renderer.models.materialAnimation;
                    break;
                case FileIO.fileType.visibilityAnimation:
                    control = renderer.ctrlVA;
                    animations = renderer.models.visibilityAnimation;
                    break;
            }

            //control.FrameChanged += Control_FrameChanged;
            UpdateAnimationsList();
        }

        private void UpdateAnimationsList()
        {
            AnimationsListBox.Items.Clear();
            if (control != null)
            {
                control.load(-1);

                foreach (RenderBase.OAnimationBase animation in animations.list)
                {
                    AnimationsListBox.Items.Add(animation.name);
                }
            }
            AnimationsListBox.Refresh();
        }
    }
}
