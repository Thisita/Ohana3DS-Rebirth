namespace Ohana3DS_Rebirth.UI.Panels
{
    partial class ViewportPanel
    {
        /// <summary> 
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary> 
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Component Designer generated code

        /// <summary> 
        /// Required method for Designer support - do not modify 
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.SplitContainer = new System.Windows.Forms.SplitContainer();
            this.OperationsTabControl = new System.Windows.Forms.TabControl();
            this.ModelsTabPage = new System.Windows.Forms.TabPage();
            this.TexturesTabPage = new System.Windows.Forms.TabPage();
            this.SkeletalAnimationsTabPage = new System.Windows.Forms.TabPage();
            this.MaterialAnimationsTabPage = new System.Windows.Forms.TabPage();
            this.VisibilityAnimationsTabPage = new System.Windows.Forms.TabPage();
            this.Screen = new System.Windows.Forms.PictureBox();
            this.ModelsPagePanel = new Ohana3DS_Rebirth.UI.Panels.ModelsPanel();
            this.TexturesPagePanel = new Ohana3DS_Rebirth.UI.Panels.TexturesPanel();
            ((System.ComponentModel.ISupportInitialize)(this.SplitContainer)).BeginInit();
            this.SplitContainer.Panel1.SuspendLayout();
            this.SplitContainer.Panel2.SuspendLayout();
            this.SplitContainer.SuspendLayout();
            this.OperationsTabControl.SuspendLayout();
            this.ModelsTabPage.SuspendLayout();
            this.TexturesTabPage.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.Screen)).BeginInit();
            this.SuspendLayout();
            // 
            // SplitContainer
            // 
            this.SplitContainer.Dock = System.Windows.Forms.DockStyle.Fill;
            this.SplitContainer.Location = new System.Drawing.Point(0, 0);
            this.SplitContainer.Name = "SplitContainer";
            // 
            // SplitContainer.Panel1
            // 
            this.SplitContainer.Panel1.Controls.Add(this.OperationsTabControl);
            // 
            // SplitContainer.Panel2
            // 
            this.SplitContainer.Panel2.Controls.Add(this.Screen);
            this.SplitContainer.Size = new System.Drawing.Size(742, 488);
            this.SplitContainer.SplitterDistance = 246;
            this.SplitContainer.TabIndex = 0;
            // 
            // OperationsTabControl
            // 
            this.OperationsTabControl.Alignment = System.Windows.Forms.TabAlignment.Left;
            this.OperationsTabControl.Controls.Add(this.ModelsTabPage);
            this.OperationsTabControl.Controls.Add(this.TexturesTabPage);
            this.OperationsTabControl.Controls.Add(this.SkeletalAnimationsTabPage);
            this.OperationsTabControl.Controls.Add(this.MaterialAnimationsTabPage);
            this.OperationsTabControl.Controls.Add(this.VisibilityAnimationsTabPage);
            this.OperationsTabControl.Dock = System.Windows.Forms.DockStyle.Fill;
            this.OperationsTabControl.Location = new System.Drawing.Point(0, 0);
            this.OperationsTabControl.Multiline = true;
            this.OperationsTabControl.Name = "OperationsTabControl";
            this.OperationsTabControl.SelectedIndex = 0;
            this.OperationsTabControl.Size = new System.Drawing.Size(246, 488);
            this.OperationsTabControl.TabIndex = 0;
            // 
            // ModelsTabPage
            // 
            this.ModelsTabPage.Controls.Add(this.ModelsPagePanel);
            this.ModelsTabPage.Location = new System.Drawing.Point(23, 4);
            this.ModelsTabPage.Name = "ModelsTabPage";
            this.ModelsTabPage.Padding = new System.Windows.Forms.Padding(3);
            this.ModelsTabPage.Size = new System.Drawing.Size(219, 480);
            this.ModelsTabPage.TabIndex = 0;
            this.ModelsTabPage.Text = "Models";
            this.ModelsTabPage.UseVisualStyleBackColor = true;
            // 
            // TexturesTabPage
            // 
            this.TexturesTabPage.Controls.Add(this.TexturesPagePanel);
            this.TexturesTabPage.Location = new System.Drawing.Point(23, 4);
            this.TexturesTabPage.Name = "TexturesTabPage";
            this.TexturesTabPage.Padding = new System.Windows.Forms.Padding(3);
            this.TexturesTabPage.Size = new System.Drawing.Size(219, 480);
            this.TexturesTabPage.TabIndex = 1;
            this.TexturesTabPage.Text = "Textures";
            this.TexturesTabPage.UseVisualStyleBackColor = true;
            // 
            // SkeletalAnimationsTabPage
            // 
            this.SkeletalAnimationsTabPage.Location = new System.Drawing.Point(23, 4);
            this.SkeletalAnimationsTabPage.Name = "SkeletalAnimationsTabPage";
            this.SkeletalAnimationsTabPage.Padding = new System.Windows.Forms.Padding(3);
            this.SkeletalAnimationsTabPage.Size = new System.Drawing.Size(219, 480);
            this.SkeletalAnimationsTabPage.TabIndex = 2;
            this.SkeletalAnimationsTabPage.Text = "Skeletal Animations";
            this.SkeletalAnimationsTabPage.UseVisualStyleBackColor = true;
            // 
            // MaterialAnimationsTabPage
            // 
            this.MaterialAnimationsTabPage.Location = new System.Drawing.Point(23, 4);
            this.MaterialAnimationsTabPage.Name = "MaterialAnimationsTabPage";
            this.MaterialAnimationsTabPage.Padding = new System.Windows.Forms.Padding(3);
            this.MaterialAnimationsTabPage.Size = new System.Drawing.Size(219, 480);
            this.MaterialAnimationsTabPage.TabIndex = 3;
            this.MaterialAnimationsTabPage.Text = "Material Animations";
            this.MaterialAnimationsTabPage.UseVisualStyleBackColor = true;
            // 
            // VisibilityAnimationsTabPage
            // 
            this.VisibilityAnimationsTabPage.Location = new System.Drawing.Point(23, 4);
            this.VisibilityAnimationsTabPage.Name = "VisibilityAnimationsTabPage";
            this.VisibilityAnimationsTabPage.Padding = new System.Windows.Forms.Padding(3);
            this.VisibilityAnimationsTabPage.Size = new System.Drawing.Size(219, 480);
            this.VisibilityAnimationsTabPage.TabIndex = 4;
            this.VisibilityAnimationsTabPage.Text = "Visibility Animations";
            this.VisibilityAnimationsTabPage.UseVisualStyleBackColor = true;
            // 
            // Screen
            // 
            this.Screen.Dock = System.Windows.Forms.DockStyle.Fill;
            this.Screen.Location = new System.Drawing.Point(0, 0);
            this.Screen.Name = "Screen";
            this.Screen.Size = new System.Drawing.Size(492, 488);
            this.Screen.TabIndex = 0;
            this.Screen.TabStop = false;
            // 
            // ModelsPagePanel
            // 
            this.ModelsPagePanel.Dock = System.Windows.Forms.DockStyle.Fill;
            this.ModelsPagePanel.Location = new System.Drawing.Point(3, 3);
            this.ModelsPagePanel.Name = "ModelsPagePanel";
            this.ModelsPagePanel.Size = new System.Drawing.Size(213, 474);
            this.ModelsPagePanel.TabIndex = 0;
            // 
            // TexturesPagePanel
            // 
            this.TexturesPagePanel.Dock = System.Windows.Forms.DockStyle.Fill;
            this.TexturesPagePanel.Location = new System.Drawing.Point(3, 3);
            this.TexturesPagePanel.Name = "TexturesPagePanel";
            this.TexturesPagePanel.Size = new System.Drawing.Size(213, 474);
            this.TexturesPagePanel.TabIndex = 0;
            // 
            // ViewportPanel
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.SplitContainer);
            this.Name = "ViewportPanel";
            this.Size = new System.Drawing.Size(742, 488);
            this.SplitContainer.Panel1.ResumeLayout(false);
            this.SplitContainer.Panel2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.SplitContainer)).EndInit();
            this.SplitContainer.ResumeLayout(false);
            this.OperationsTabControl.ResumeLayout(false);
            this.ModelsTabPage.ResumeLayout(false);
            this.TexturesTabPage.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.Screen)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.SplitContainer SplitContainer;
        private System.Windows.Forms.PictureBox Screen;
        private System.Windows.Forms.TabControl OperationsTabControl;
        private System.Windows.Forms.TabPage ModelsTabPage;
        private System.Windows.Forms.TabPage TexturesTabPage;
        private System.Windows.Forms.TabPage SkeletalAnimationsTabPage;
        private System.Windows.Forms.TabPage MaterialAnimationsTabPage;
        private System.Windows.Forms.TabPage VisibilityAnimationsTabPage;
        private ModelsPanel ModelsPagePanel;
        private TexturesPanel TexturesPagePanel;
    }
}
