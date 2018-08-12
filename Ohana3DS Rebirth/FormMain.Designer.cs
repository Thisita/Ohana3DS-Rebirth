namespace Ohana3DS_Rebirth
{
    partial class FormMain
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

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.menuStrip1 = new System.Windows.Forms.MenuStrip();
            this.toolStripMenuItem1 = new System.Windows.Forms.ToolStripMenuItem();
            this.fileToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.openToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripSeparator1 = new System.Windows.Forms.ToolStripSeparator();
            this.exitToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.optionsToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.viewportToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.antialiasingToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.AA2xToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.AA4xToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.AA8xToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.AA16xToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripSeparator3 = new System.Windows.Forms.ToolStripSeparator();
            this.AANoneToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.backgroundToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.BackgroundBlackToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.BackgroundGrayToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.BackgroundWhiteToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripSeparator4 = new System.Windows.Forms.ToolStripSeparator();
            this.BackgroundCustomToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.showHideToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.guidelinesToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.informationToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.allMeshesToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.sidebarToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.wireframeToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.texturingToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.fragmentShaderToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripSeparator2 = new System.Windows.Forms.ToolStripSeparator();
            this.toolStripMenuItem2 = new System.Windows.Forms.ToolStripMenuItem();
            this.useLastShaderToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.toolsToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.bCHTextureReplacerToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.sm4shModelCreatorToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.helpToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.aboutToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.menuStrip1.SuspendLayout();
            this.SuspendLayout();
            // 
            // menuStrip1
            // 
            this.menuStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.toolStripMenuItem1,
            this.fileToolStripMenuItem,
            this.optionsToolStripMenuItem,
            this.toolsToolStripMenuItem,
            this.helpToolStripMenuItem});
            this.menuStrip1.Location = new System.Drawing.Point(0, 0);
            this.menuStrip1.Name = "menuStrip1";
            this.menuStrip1.Size = new System.Drawing.Size(800, 24);
            this.menuStrip1.TabIndex = 0;
            this.menuStrip1.Text = "menuStrip1";
            // 
            // toolStripMenuItem1
            // 
            this.toolStripMenuItem1.Name = "toolStripMenuItem1";
            this.toolStripMenuItem1.Size = new System.Drawing.Size(12, 20);
            // 
            // fileToolStripMenuItem
            // 
            this.fileToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.openToolStripMenuItem,
            this.toolStripSeparator1,
            this.exitToolStripMenuItem});
            this.fileToolStripMenuItem.Name = "fileToolStripMenuItem";
            this.fileToolStripMenuItem.Size = new System.Drawing.Size(37, 20);
            this.fileToolStripMenuItem.Text = "&File";
            // 
            // openToolStripMenuItem
            // 
            this.openToolStripMenuItem.Name = "openToolStripMenuItem";
            this.openToolStripMenuItem.Size = new System.Drawing.Size(103, 22);
            this.openToolStripMenuItem.Text = "&Open";
            this.openToolStripMenuItem.Click += new System.EventHandler(this.openToolStripMenuItem_Click);
            // 
            // toolStripSeparator1
            // 
            this.toolStripSeparator1.Name = "toolStripSeparator1";
            this.toolStripSeparator1.Size = new System.Drawing.Size(100, 6);
            // 
            // exitToolStripMenuItem
            // 
            this.exitToolStripMenuItem.Name = "exitToolStripMenuItem";
            this.exitToolStripMenuItem.Size = new System.Drawing.Size(103, 22);
            this.exitToolStripMenuItem.Text = "&Exit";
            this.exitToolStripMenuItem.Click += new System.EventHandler(this.exitToolStripMenuItem_Click);
            // 
            // optionsToolStripMenuItem
            // 
            this.optionsToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.viewportToolStripMenuItem});
            this.optionsToolStripMenuItem.Name = "optionsToolStripMenuItem";
            this.optionsToolStripMenuItem.Size = new System.Drawing.Size(61, 20);
            this.optionsToolStripMenuItem.Text = "&Options";
            // 
            // viewportToolStripMenuItem
            // 
            this.viewportToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.antialiasingToolStripMenuItem,
            this.backgroundToolStripMenuItem,
            this.showHideToolStripMenuItem,
            this.texturingToolStripMenuItem});
            this.viewportToolStripMenuItem.Name = "viewportToolStripMenuItem";
            this.viewportToolStripMenuItem.Size = new System.Drawing.Size(180, 22);
            this.viewportToolStripMenuItem.Text = "&Viewport";
            // 
            // antialiasingToolStripMenuItem
            // 
            this.antialiasingToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.AA2xToolStripMenuItem,
            this.AA4xToolStripMenuItem,
            this.AA8xToolStripMenuItem,
            this.AA16xToolStripMenuItem,
            this.toolStripSeparator3,
            this.AANoneToolStripMenuItem});
            this.antialiasingToolStripMenuItem.Name = "antialiasingToolStripMenuItem";
            this.antialiasingToolStripMenuItem.Size = new System.Drawing.Size(180, 22);
            this.antialiasingToolStripMenuItem.Text = "&Anti-aliasing";
            // 
            // AA2xToolStripMenuItem
            // 
            this.AA2xToolStripMenuItem.Name = "AA2xToolStripMenuItem";
            this.AA2xToolStripMenuItem.Size = new System.Drawing.Size(180, 22);
            this.AA2xToolStripMenuItem.Text = "&2x";
            this.AA2xToolStripMenuItem.Click += new System.EventHandler(this.AA2xToolStripMenuItem_Click);
            // 
            // AA4xToolStripMenuItem
            // 
            this.AA4xToolStripMenuItem.Name = "AA4xToolStripMenuItem";
            this.AA4xToolStripMenuItem.Size = new System.Drawing.Size(180, 22);
            this.AA4xToolStripMenuItem.Text = "&4x";
            this.AA4xToolStripMenuItem.Click += new System.EventHandler(this.AA4xToolStripMenuItem_Click);
            // 
            // AA8xToolStripMenuItem
            // 
            this.AA8xToolStripMenuItem.Name = "AA8xToolStripMenuItem";
            this.AA8xToolStripMenuItem.Size = new System.Drawing.Size(180, 22);
            this.AA8xToolStripMenuItem.Text = "&8x";
            this.AA8xToolStripMenuItem.Click += new System.EventHandler(this.AA8xToolStripMenuItem_Click);
            // 
            // AA16xToolStripMenuItem
            // 
            this.AA16xToolStripMenuItem.Name = "AA16xToolStripMenuItem";
            this.AA16xToolStripMenuItem.Size = new System.Drawing.Size(180, 22);
            this.AA16xToolStripMenuItem.Text = "&16x";
            this.AA16xToolStripMenuItem.Click += new System.EventHandler(this.AA16xToolStripMenuItem_Click);
            // 
            // toolStripSeparator3
            // 
            this.toolStripSeparator3.Name = "toolStripSeparator3";
            this.toolStripSeparator3.Size = new System.Drawing.Size(177, 6);
            // 
            // AANoneToolStripMenuItem
            // 
            this.AANoneToolStripMenuItem.Checked = true;
            this.AANoneToolStripMenuItem.CheckState = System.Windows.Forms.CheckState.Checked;
            this.AANoneToolStripMenuItem.Name = "AANoneToolStripMenuItem";
            this.AANoneToolStripMenuItem.Size = new System.Drawing.Size(180, 22);
            this.AANoneToolStripMenuItem.Text = "&None";
            this.AANoneToolStripMenuItem.Click += new System.EventHandler(this.AANoneToolStripMenuItem_Click);
            // 
            // backgroundToolStripMenuItem
            // 
            this.backgroundToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.BackgroundBlackToolStripMenuItem,
            this.BackgroundGrayToolStripMenuItem,
            this.BackgroundWhiteToolStripMenuItem,
            this.toolStripSeparator4,
            this.BackgroundCustomToolStripMenuItem});
            this.backgroundToolStripMenuItem.Name = "backgroundToolStripMenuItem";
            this.backgroundToolStripMenuItem.Size = new System.Drawing.Size(180, 22);
            this.backgroundToolStripMenuItem.Text = "&Background";
            // 
            // BackgroundBlackToolStripMenuItem
            // 
            this.BackgroundBlackToolStripMenuItem.Checked = true;
            this.BackgroundBlackToolStripMenuItem.CheckState = System.Windows.Forms.CheckState.Checked;
            this.BackgroundBlackToolStripMenuItem.Name = "BackgroundBlackToolStripMenuItem";
            this.BackgroundBlackToolStripMenuItem.Size = new System.Drawing.Size(180, 22);
            this.BackgroundBlackToolStripMenuItem.Text = "&Black";
            this.BackgroundBlackToolStripMenuItem.Click += new System.EventHandler(this.BackgroundBlackToolStripMenuItem_Click);
            // 
            // BackgroundGrayToolStripMenuItem
            // 
            this.BackgroundGrayToolStripMenuItem.Name = "BackgroundGrayToolStripMenuItem";
            this.BackgroundGrayToolStripMenuItem.Size = new System.Drawing.Size(180, 22);
            this.BackgroundGrayToolStripMenuItem.Text = "&Gray";
            this.BackgroundGrayToolStripMenuItem.Click += new System.EventHandler(this.BackgroundGrayToolStripMenuItem_Click);
            // 
            // BackgroundWhiteToolStripMenuItem
            // 
            this.BackgroundWhiteToolStripMenuItem.Name = "BackgroundWhiteToolStripMenuItem";
            this.BackgroundWhiteToolStripMenuItem.Size = new System.Drawing.Size(180, 22);
            this.BackgroundWhiteToolStripMenuItem.Text = "&White";
            this.BackgroundWhiteToolStripMenuItem.Click += new System.EventHandler(this.BackgroundWhiteToolStripMenuItem_Click);
            // 
            // toolStripSeparator4
            // 
            this.toolStripSeparator4.Name = "toolStripSeparator4";
            this.toolStripSeparator4.Size = new System.Drawing.Size(177, 6);
            // 
            // BackgroundCustomToolStripMenuItem
            // 
            this.BackgroundCustomToolStripMenuItem.Name = "BackgroundCustomToolStripMenuItem";
            this.BackgroundCustomToolStripMenuItem.Size = new System.Drawing.Size(180, 22);
            this.BackgroundCustomToolStripMenuItem.Text = "&Custom";
            this.BackgroundCustomToolStripMenuItem.Click += new System.EventHandler(this.BackgroundCustomToolStripMenuItem_Click);
            // 
            // showHideToolStripMenuItem
            // 
            this.showHideToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.guidelinesToolStripMenuItem,
            this.informationToolStripMenuItem,
            this.allMeshesToolStripMenuItem,
            this.sidebarToolStripMenuItem,
            this.wireframeToolStripMenuItem});
            this.showHideToolStripMenuItem.Name = "showHideToolStripMenuItem";
            this.showHideToolStripMenuItem.Size = new System.Drawing.Size(180, 22);
            this.showHideToolStripMenuItem.Text = "&Show/Hide";
            // 
            // guidelinesToolStripMenuItem
            // 
            this.guidelinesToolStripMenuItem.CheckOnClick = true;
            this.guidelinesToolStripMenuItem.Name = "guidelinesToolStripMenuItem";
            this.guidelinesToolStripMenuItem.Size = new System.Drawing.Size(180, 22);
            this.guidelinesToolStripMenuItem.Text = "&Guidelines";
            // 
            // informationToolStripMenuItem
            // 
            this.informationToolStripMenuItem.CheckOnClick = true;
            this.informationToolStripMenuItem.Name = "informationToolStripMenuItem";
            this.informationToolStripMenuItem.Size = new System.Drawing.Size(180, 22);
            this.informationToolStripMenuItem.Text = "&Information";
            // 
            // allMeshesToolStripMenuItem
            // 
            this.allMeshesToolStripMenuItem.CheckOnClick = true;
            this.allMeshesToolStripMenuItem.Name = "allMeshesToolStripMenuItem";
            this.allMeshesToolStripMenuItem.Size = new System.Drawing.Size(180, 22);
            this.allMeshesToolStripMenuItem.Text = "&All Meshes";
            // 
            // sidebarToolStripMenuItem
            // 
            this.sidebarToolStripMenuItem.CheckOnClick = true;
            this.sidebarToolStripMenuItem.Name = "sidebarToolStripMenuItem";
            this.sidebarToolStripMenuItem.Size = new System.Drawing.Size(180, 22);
            this.sidebarToolStripMenuItem.Text = "&Sidebar";
            // 
            // wireframeToolStripMenuItem
            // 
            this.wireframeToolStripMenuItem.CheckOnClick = true;
            this.wireframeToolStripMenuItem.Name = "wireframeToolStripMenuItem";
            this.wireframeToolStripMenuItem.Size = new System.Drawing.Size(180, 22);
            this.wireframeToolStripMenuItem.Text = "&Wireframe";
            // 
            // texturingToolStripMenuItem
            // 
            this.texturingToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.fragmentShaderToolStripMenuItem,
            this.toolStripSeparator2,
            this.toolStripMenuItem2,
            this.useLastShaderToolStripMenuItem});
            this.texturingToolStripMenuItem.Name = "texturingToolStripMenuItem";
            this.texturingToolStripMenuItem.Size = new System.Drawing.Size(180, 22);
            this.texturingToolStripMenuItem.Text = "&Texturing";
            // 
            // fragmentShaderToolStripMenuItem
            // 
            this.fragmentShaderToolStripMenuItem.CheckOnClick = true;
            this.fragmentShaderToolStripMenuItem.Name = "fragmentShaderToolStripMenuItem";
            this.fragmentShaderToolStripMenuItem.Size = new System.Drawing.Size(202, 22);
            this.fragmentShaderToolStripMenuItem.Text = "&Enable Fragment Shader";
            // 
            // toolStripSeparator2
            // 
            this.toolStripSeparator2.Name = "toolStripSeparator2";
            this.toolStripSeparator2.Size = new System.Drawing.Size(199, 6);
            // 
            // toolStripMenuItem2
            // 
            this.toolStripMenuItem2.Name = "toolStripMenuItem2";
            this.toolStripMenuItem2.Size = new System.Drawing.Size(202, 22);
            this.toolStripMenuItem2.Text = "Use &First Shader";
            // 
            // useLastShaderToolStripMenuItem
            // 
            this.useLastShaderToolStripMenuItem.Name = "useLastShaderToolStripMenuItem";
            this.useLastShaderToolStripMenuItem.Size = new System.Drawing.Size(202, 22);
            this.useLastShaderToolStripMenuItem.Text = "Use &Last Shader";
            // 
            // toolsToolStripMenuItem
            // 
            this.toolsToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.bCHTextureReplacerToolStripMenuItem,
            this.sm4shModelCreatorToolStripMenuItem});
            this.toolsToolStripMenuItem.Name = "toolsToolStripMenuItem";
            this.toolsToolStripMenuItem.Size = new System.Drawing.Size(47, 20);
            this.toolsToolStripMenuItem.Text = "&Tools";
            // 
            // bCHTextureReplacerToolStripMenuItem
            // 
            this.bCHTextureReplacerToolStripMenuItem.Name = "bCHTextureReplacerToolStripMenuItem";
            this.bCHTextureReplacerToolStripMenuItem.Size = new System.Drawing.Size(188, 22);
            this.bCHTextureReplacerToolStripMenuItem.Text = "&BCH Texture Replacer";
            // 
            // sm4shModelCreatorToolStripMenuItem
            // 
            this.sm4shModelCreatorToolStripMenuItem.Name = "sm4shModelCreatorToolStripMenuItem";
            this.sm4shModelCreatorToolStripMenuItem.Size = new System.Drawing.Size(188, 22);
            this.sm4shModelCreatorToolStripMenuItem.Text = "&Sm4sh Model Creator";
            // 
            // helpToolStripMenuItem
            // 
            this.helpToolStripMenuItem.Alignment = System.Windows.Forms.ToolStripItemAlignment.Right;
            this.helpToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.aboutToolStripMenuItem});
            this.helpToolStripMenuItem.Name = "helpToolStripMenuItem";
            this.helpToolStripMenuItem.Size = new System.Drawing.Size(44, 20);
            this.helpToolStripMenuItem.Text = "&Help";
            // 
            // aboutToolStripMenuItem
            // 
            this.aboutToolStripMenuItem.Name = "aboutToolStripMenuItem";
            this.aboutToolStripMenuItem.Size = new System.Drawing.Size(107, 22);
            this.aboutToolStripMenuItem.Text = "&About";
            this.aboutToolStripMenuItem.Click += new System.EventHandler(this.aboutToolStripMenuItem_Click);
            // 
            // FormMain
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(800, 450);
            this.Controls.Add(this.menuStrip1);
            this.MainMenuStrip = this.menuStrip1;
            this.Name = "FormMain";
            this.Text = "Ohana3DS";
            this.DragDrop += new System.Windows.Forms.DragEventHandler(this.FormMain_DragDrop);
            this.menuStrip1.ResumeLayout(false);
            this.menuStrip1.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.MenuStrip menuStrip1;
        private System.Windows.Forms.ToolStripMenuItem toolStripMenuItem1;
        private System.Windows.Forms.ToolStripMenuItem fileToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem openToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem exitToolStripMenuItem;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator1;
        private System.Windows.Forms.ToolStripMenuItem optionsToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem viewportToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem antialiasingToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem backgroundToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem showHideToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem texturingToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem AANoneToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem AA2xToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem AA4xToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem AA8xToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem AA16xToolStripMenuItem;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator3;
        private System.Windows.Forms.ToolStripMenuItem BackgroundBlackToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem BackgroundGrayToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem BackgroundWhiteToolStripMenuItem;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator4;
        private System.Windows.Forms.ToolStripMenuItem BackgroundCustomToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem guidelinesToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem informationToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem allMeshesToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem sidebarToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem wireframeToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem fragmentShaderToolStripMenuItem;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator2;
        private System.Windows.Forms.ToolStripMenuItem toolStripMenuItem2;
        private System.Windows.Forms.ToolStripMenuItem useLastShaderToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem toolsToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem bCHTextureReplacerToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem sm4shModelCreatorToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem helpToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem aboutToolStripMenuItem;
    }
}