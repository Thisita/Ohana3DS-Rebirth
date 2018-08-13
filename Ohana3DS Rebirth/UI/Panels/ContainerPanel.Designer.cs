namespace Ohana3DS_Rebirth.UI.Panels
{
    partial class ContainerPanel
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
            this.ExportAllButton = new System.Windows.Forms.Button();
            this.ExportButton = new System.Windows.Forms.Button();
            this.FileListBox = new System.Windows.Forms.ListBox();
            this.TopButtonsLayoutPanel.SuspendLayout();
            this.SuspendLayout();
            // 
            // TopButtonsLayoutPanel
            // 
            this.TopButtonsLayoutPanel.AutoSize = true;
            this.TopButtonsLayoutPanel.ColumnCount = 2;
            this.TopButtonsLayoutPanel.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.TopButtonsLayoutPanel.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.TopButtonsLayoutPanel.Controls.Add(this.ExportAllButton, 0, 0);
            this.TopButtonsLayoutPanel.Controls.Add(this.ExportButton, 1, 0);
            this.TopButtonsLayoutPanel.Dock = System.Windows.Forms.DockStyle.Top;
            this.TopButtonsLayoutPanel.Location = new System.Drawing.Point(0, 0);
            this.TopButtonsLayoutPanel.Name = "TopButtonsLayoutPanel";
            this.TopButtonsLayoutPanel.RowCount = 1;
            this.TopButtonsLayoutPanel.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.TopButtonsLayoutPanel.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.TopButtonsLayoutPanel.Size = new System.Drawing.Size(332, 29);
            this.TopButtonsLayoutPanel.TabIndex = 0;
            // 
            // ExportAllButton
            // 
            this.ExportAllButton.Dock = System.Windows.Forms.DockStyle.Fill;
            this.ExportAllButton.Location = new System.Drawing.Point(3, 3);
            this.ExportAllButton.Name = "ExportAllButton";
            this.ExportAllButton.Size = new System.Drawing.Size(160, 23);
            this.ExportAllButton.TabIndex = 0;
            this.ExportAllButton.Text = "Export All";
            this.ExportAllButton.UseVisualStyleBackColor = true;
            this.ExportAllButton.Click += new System.EventHandler(this.ExportAllButton_Click);
            // 
            // ExportButton
            // 
            this.ExportButton.Dock = System.Windows.Forms.DockStyle.Fill;
            this.ExportButton.Location = new System.Drawing.Point(169, 3);
            this.ExportButton.Name = "ExportButton";
            this.ExportButton.Size = new System.Drawing.Size(160, 23);
            this.ExportButton.TabIndex = 1;
            this.ExportButton.Text = "Export";
            this.ExportButton.UseVisualStyleBackColor = true;
            this.ExportButton.Click += new System.EventHandler(this.ExportButton_Click);
            // 
            // FileListBox
            // 
            this.FileListBox.Dock = System.Windows.Forms.DockStyle.Fill;
            this.FileListBox.FormattingEnabled = true;
            this.FileListBox.Location = new System.Drawing.Point(0, 29);
            this.FileListBox.Name = "FileListBox";
            this.FileListBox.Size = new System.Drawing.Size(332, 265);
            this.FileListBox.TabIndex = 1;
            // 
            // ContainerPanel
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.FileListBox);
            this.Controls.Add(this.TopButtonsLayoutPanel);
            this.Name = "ContainerPanel";
            this.Size = new System.Drawing.Size(332, 294);
            this.TopButtonsLayoutPanel.ResumeLayout(false);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.TableLayoutPanel TopButtonsLayoutPanel;
        private System.Windows.Forms.Button ExportAllButton;
        private System.Windows.Forms.Button ExportButton;
        private System.Windows.Forms.ListBox FileListBox;
    }
}
