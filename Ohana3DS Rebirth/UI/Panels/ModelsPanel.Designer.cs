namespace Ohana3DS_Rebirth.UI.Panels
{
    partial class ModelsPanel
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
            this.TopLayoutPanel = new System.Windows.Forms.TableLayoutPanel();
            this.ExportButton = new System.Windows.Forms.Button();
            this.ImportButton = new System.Windows.Forms.Button();
            this.DeleteButton = new System.Windows.Forms.Button();
            this.ClearButton = new System.Windows.Forms.Button();
            this.ModelListBox = new System.Windows.Forms.ListBox();
            this.TopLayoutPanel.SuspendLayout();
            this.SuspendLayout();
            // 
            // TopLayoutPanel
            // 
            this.TopLayoutPanel.AutoSize = true;
            this.TopLayoutPanel.ColumnCount = 4;
            this.TopLayoutPanel.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.TopLayoutPanel.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.TopLayoutPanel.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 71F));
            this.TopLayoutPanel.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 68F));
            this.TopLayoutPanel.Controls.Add(this.ExportButton, 0, 0);
            this.TopLayoutPanel.Controls.Add(this.ImportButton, 1, 0);
            this.TopLayoutPanel.Controls.Add(this.DeleteButton, 2, 0);
            this.TopLayoutPanel.Controls.Add(this.ClearButton, 3, 0);
            this.TopLayoutPanel.Dock = System.Windows.Forms.DockStyle.Top;
            this.TopLayoutPanel.Location = new System.Drawing.Point(0, 0);
            this.TopLayoutPanel.Name = "TopLayoutPanel";
            this.TopLayoutPanel.RowCount = 1;
            this.TopLayoutPanel.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.TopLayoutPanel.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.TopLayoutPanel.Size = new System.Drawing.Size(277, 68);
            this.TopLayoutPanel.TabIndex = 0;
            // 
            // ExportButton
            // 
            this.ExportButton.Dock = System.Windows.Forms.DockStyle.Fill;
            this.ExportButton.Location = new System.Drawing.Point(3, 3);
            this.ExportButton.Name = "ExportButton";
            this.ExportButton.Size = new System.Drawing.Size(63, 62);
            this.ExportButton.TabIndex = 0;
            this.ExportButton.Text = "Export";
            this.ExportButton.UseVisualStyleBackColor = true;
            this.ExportButton.Click += new System.EventHandler(this.ExportButton_Click);
            // 
            // ImportButton
            // 
            this.ImportButton.Dock = System.Windows.Forms.DockStyle.Fill;
            this.ImportButton.Location = new System.Drawing.Point(72, 3);
            this.ImportButton.Name = "ImportButton";
            this.ImportButton.Size = new System.Drawing.Size(63, 62);
            this.ImportButton.TabIndex = 1;
            this.ImportButton.Text = "Import";
            this.ImportButton.UseVisualStyleBackColor = true;
            this.ImportButton.Click += new System.EventHandler(this.ImportButton_Click);
            // 
            // DeleteButton
            // 
            this.DeleteButton.Dock = System.Windows.Forms.DockStyle.Fill;
            this.DeleteButton.Location = new System.Drawing.Point(141, 3);
            this.DeleteButton.Name = "DeleteButton";
            this.DeleteButton.Size = new System.Drawing.Size(65, 62);
            this.DeleteButton.TabIndex = 2;
            this.DeleteButton.Text = "Delete";
            this.DeleteButton.UseVisualStyleBackColor = true;
            this.DeleteButton.Click += new System.EventHandler(this.DeleteButton_Click);
            // 
            // ClearButton
            // 
            this.ClearButton.Dock = System.Windows.Forms.DockStyle.Fill;
            this.ClearButton.Location = new System.Drawing.Point(212, 3);
            this.ClearButton.Name = "ClearButton";
            this.ClearButton.Size = new System.Drawing.Size(62, 62);
            this.ClearButton.TabIndex = 3;
            this.ClearButton.Text = "Clear";
            this.ClearButton.UseVisualStyleBackColor = true;
            this.ClearButton.Click += new System.EventHandler(this.ClearButton_Click);
            // 
            // ModelListBox
            // 
            this.ModelListBox.Dock = System.Windows.Forms.DockStyle.Fill;
            this.ModelListBox.FormattingEnabled = true;
            this.ModelListBox.Location = new System.Drawing.Point(0, 68);
            this.ModelListBox.Name = "ModelListBox";
            this.ModelListBox.Size = new System.Drawing.Size(277, 181);
            this.ModelListBox.TabIndex = 1;
            this.ModelListBox.SelectedIndexChanged += new System.EventHandler(this.ModelListBox_SelectedIndexChanged);
            // 
            // ModelsPanel
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.ModelListBox);
            this.Controls.Add(this.TopLayoutPanel);
            this.Name = "ModelsPanel";
            this.Size = new System.Drawing.Size(277, 249);
            this.TopLayoutPanel.ResumeLayout(false);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.TableLayoutPanel TopLayoutPanel;
        private System.Windows.Forms.Button ExportButton;
        private System.Windows.Forms.Button ImportButton;
        private System.Windows.Forms.Button DeleteButton;
        private System.Windows.Forms.Button ClearButton;
        private System.Windows.Forms.ListBox ModelListBox;
    }
}
