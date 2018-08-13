namespace Ohana3DS_Rebirth.UI.Panels
{
    partial class ImagePanel
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
            this.TopButtonsLayoutPanel = new System.Windows.Forms.TableLayoutPanel();
            this.ExportButton = new System.Windows.Forms.Button();
            this.TexturePreview = new System.Windows.Forms.PictureBox();
            this.TopButtonsLayoutPanel.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.TexturePreview)).BeginInit();
            this.SuspendLayout();
            // 
            // TopButtonsLayoutPanel
            // 
            this.TopButtonsLayoutPanel.AutoSize = true;
            this.TopButtonsLayoutPanel.ColumnCount = 1;
            this.TopButtonsLayoutPanel.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.TopButtonsLayoutPanel.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.TopButtonsLayoutPanel.Controls.Add(this.ExportButton, 0, 0);
            this.TopButtonsLayoutPanel.Dock = System.Windows.Forms.DockStyle.Top;
            this.TopButtonsLayoutPanel.Location = new System.Drawing.Point(0, 0);
            this.TopButtonsLayoutPanel.Name = "TopButtonsLayoutPanel";
            this.TopButtonsLayoutPanel.RowCount = 1;
            this.TopButtonsLayoutPanel.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.TopButtonsLayoutPanel.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.TopButtonsLayoutPanel.Size = new System.Drawing.Size(298, 29);
            this.TopButtonsLayoutPanel.TabIndex = 0;
            // 
            // ExportButton
            // 
            this.ExportButton.Dock = System.Windows.Forms.DockStyle.Fill;
            this.ExportButton.Location = new System.Drawing.Point(3, 3);
            this.ExportButton.Name = "ExportButton";
            this.ExportButton.Size = new System.Drawing.Size(292, 23);
            this.ExportButton.TabIndex = 0;
            this.ExportButton.Text = "Export";
            this.ExportButton.UseVisualStyleBackColor = true;
            this.ExportButton.Click += new System.EventHandler(this.ExportButton_Click);
            // 
            // TexturePreview
            // 
            this.TexturePreview.Dock = System.Windows.Forms.DockStyle.Fill;
            this.TexturePreview.Location = new System.Drawing.Point(0, 29);
            this.TexturePreview.Name = "TexturePreview";
            this.TexturePreview.Size = new System.Drawing.Size(298, 278);
            this.TexturePreview.TabIndex = 1;
            this.TexturePreview.TabStop = false;
            // 
            // ImagePanel
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.TexturePreview);
            this.Controls.Add(this.TopButtonsLayoutPanel);
            this.Name = "ImagePanel";
            this.Size = new System.Drawing.Size(298, 307);
            this.TopButtonsLayoutPanel.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.TexturePreview)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.TableLayoutPanel TopButtonsLayoutPanel;
        private System.Windows.Forms.Button ExportButton;
        private System.Windows.Forms.PictureBox TexturePreview;
    }
}
