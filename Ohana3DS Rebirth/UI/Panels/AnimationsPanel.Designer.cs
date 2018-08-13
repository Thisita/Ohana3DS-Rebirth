namespace Ohana3DS_Rebirth.UI.Panels
{
    partial class AnimationsPanel
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
            this.ImportButton = new System.Windows.Forms.Button();
            this.DeleteButton = new System.Windows.Forms.Button();
            this.ClearButton = new System.Windows.Forms.Button();
            this.PlaybackControlsLayoutPanel = new System.Windows.Forms.TableLayoutPanel();
            this.PreviousButton = new System.Windows.Forms.Button();
            this.PlayPauseButton = new System.Windows.Forms.Button();
            this.StopButton = new System.Windows.Forms.Button();
            this.NextButton = new System.Windows.Forms.Button();
            this.Seeker = new System.Windows.Forms.NumericUpDown();
            this.Speed = new System.Windows.Forms.NumericUpDown();
            this.AnimationsListBox = new System.Windows.Forms.ListBox();
            this.TopButtonsLayoutPanel.SuspendLayout();
            this.PlaybackControlsLayoutPanel.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.Seeker)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.Speed)).BeginInit();
            this.SuspendLayout();
            // 
            // TopButtonsLayoutPanel
            // 
            this.TopButtonsLayoutPanel.AutoSize = true;
            this.TopButtonsLayoutPanel.ColumnCount = 4;
            this.TopButtonsLayoutPanel.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.TopButtonsLayoutPanel.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.TopButtonsLayoutPanel.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 66F));
            this.TopButtonsLayoutPanel.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 70F));
            this.TopButtonsLayoutPanel.Controls.Add(this.ExportButton, 0, 0);
            this.TopButtonsLayoutPanel.Controls.Add(this.ImportButton, 1, 0);
            this.TopButtonsLayoutPanel.Controls.Add(this.DeleteButton, 2, 0);
            this.TopButtonsLayoutPanel.Controls.Add(this.ClearButton, 3, 0);
            this.TopButtonsLayoutPanel.Dock = System.Windows.Forms.DockStyle.Top;
            this.TopButtonsLayoutPanel.Location = new System.Drawing.Point(0, 0);
            this.TopButtonsLayoutPanel.Name = "TopButtonsLayoutPanel";
            this.TopButtonsLayoutPanel.RowCount = 1;
            this.TopButtonsLayoutPanel.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.TopButtonsLayoutPanel.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.TopButtonsLayoutPanel.Size = new System.Drawing.Size(286, 29);
            this.TopButtonsLayoutPanel.TabIndex = 0;
            // 
            // ExportButton
            // 
            this.ExportButton.Dock = System.Windows.Forms.DockStyle.Fill;
            this.ExportButton.Location = new System.Drawing.Point(3, 3);
            this.ExportButton.Name = "ExportButton";
            this.ExportButton.Size = new System.Drawing.Size(69, 23);
            this.ExportButton.TabIndex = 0;
            this.ExportButton.Text = "Export";
            this.ExportButton.UseVisualStyleBackColor = true;
            // 
            // ImportButton
            // 
            this.ImportButton.Dock = System.Windows.Forms.DockStyle.Fill;
            this.ImportButton.Location = new System.Drawing.Point(78, 3);
            this.ImportButton.Name = "ImportButton";
            this.ImportButton.Size = new System.Drawing.Size(69, 23);
            this.ImportButton.TabIndex = 1;
            this.ImportButton.Text = "Import";
            this.ImportButton.UseVisualStyleBackColor = true;
            // 
            // DeleteButton
            // 
            this.DeleteButton.Dock = System.Windows.Forms.DockStyle.Fill;
            this.DeleteButton.Location = new System.Drawing.Point(153, 3);
            this.DeleteButton.Name = "DeleteButton";
            this.DeleteButton.Size = new System.Drawing.Size(60, 23);
            this.DeleteButton.TabIndex = 2;
            this.DeleteButton.Text = "Delete";
            this.DeleteButton.UseVisualStyleBackColor = true;
            // 
            // ClearButton
            // 
            this.ClearButton.Dock = System.Windows.Forms.DockStyle.Fill;
            this.ClearButton.Location = new System.Drawing.Point(219, 3);
            this.ClearButton.Name = "ClearButton";
            this.ClearButton.Size = new System.Drawing.Size(64, 23);
            this.ClearButton.TabIndex = 3;
            this.ClearButton.Text = "Clear";
            this.ClearButton.UseVisualStyleBackColor = true;
            // 
            // PlaybackControlsLayoutPanel
            // 
            this.PlaybackControlsLayoutPanel.AutoSize = true;
            this.PlaybackControlsLayoutPanel.ColumnCount = 4;
            this.PlaybackControlsLayoutPanel.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.PlaybackControlsLayoutPanel.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.PlaybackControlsLayoutPanel.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 64F));
            this.PlaybackControlsLayoutPanel.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 78F));
            this.PlaybackControlsLayoutPanel.Controls.Add(this.PreviousButton, 0, 0);
            this.PlaybackControlsLayoutPanel.Controls.Add(this.PlayPauseButton, 1, 0);
            this.PlaybackControlsLayoutPanel.Controls.Add(this.StopButton, 2, 0);
            this.PlaybackControlsLayoutPanel.Controls.Add(this.NextButton, 3, 0);
            this.PlaybackControlsLayoutPanel.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.PlaybackControlsLayoutPanel.Location = new System.Drawing.Point(0, 234);
            this.PlaybackControlsLayoutPanel.Name = "PlaybackControlsLayoutPanel";
            this.PlaybackControlsLayoutPanel.RowCount = 1;
            this.PlaybackControlsLayoutPanel.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.PlaybackControlsLayoutPanel.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.PlaybackControlsLayoutPanel.Size = new System.Drawing.Size(286, 29);
            this.PlaybackControlsLayoutPanel.TabIndex = 1;
            // 
            // PreviousButton
            // 
            this.PreviousButton.Dock = System.Windows.Forms.DockStyle.Fill;
            this.PreviousButton.Location = new System.Drawing.Point(3, 3);
            this.PreviousButton.Name = "PreviousButton";
            this.PreviousButton.Size = new System.Drawing.Size(66, 23);
            this.PreviousButton.TabIndex = 0;
            this.PreviousButton.Text = "Previous";
            this.PreviousButton.UseVisualStyleBackColor = true;
            // 
            // PlayPauseButton
            // 
            this.PlayPauseButton.Dock = System.Windows.Forms.DockStyle.Fill;
            this.PlayPauseButton.Location = new System.Drawing.Point(75, 3);
            this.PlayPauseButton.Name = "PlayPauseButton";
            this.PlayPauseButton.Size = new System.Drawing.Size(66, 23);
            this.PlayPauseButton.TabIndex = 1;
            this.PlayPauseButton.Text = "Play/Pause";
            this.PlayPauseButton.UseVisualStyleBackColor = true;
            // 
            // StopButton
            // 
            this.StopButton.Dock = System.Windows.Forms.DockStyle.Fill;
            this.StopButton.Location = new System.Drawing.Point(147, 3);
            this.StopButton.Name = "StopButton";
            this.StopButton.Size = new System.Drawing.Size(58, 23);
            this.StopButton.TabIndex = 2;
            this.StopButton.Text = "Stop";
            this.StopButton.UseVisualStyleBackColor = true;
            // 
            // NextButton
            // 
            this.NextButton.Dock = System.Windows.Forms.DockStyle.Fill;
            this.NextButton.Location = new System.Drawing.Point(211, 3);
            this.NextButton.Name = "NextButton";
            this.NextButton.Size = new System.Drawing.Size(72, 23);
            this.NextButton.TabIndex = 3;
            this.NextButton.Text = "Next";
            this.NextButton.UseVisualStyleBackColor = true;
            // 
            // Seeker
            // 
            this.Seeker.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.Seeker.Location = new System.Drawing.Point(0, 214);
            this.Seeker.Name = "Seeker";
            this.Seeker.Size = new System.Drawing.Size(286, 20);
            this.Seeker.TabIndex = 2;
            // 
            // Speed
            // 
            this.Speed.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.Speed.Location = new System.Drawing.Point(0, 194);
            this.Speed.Name = "Speed";
            this.Speed.Size = new System.Drawing.Size(286, 20);
            this.Speed.TabIndex = 3;
            this.Speed.Value = new decimal(new int[] {
            100,
            0,
            0,
            0});
            // 
            // AnimationsListBox
            // 
            this.AnimationsListBox.Dock = System.Windows.Forms.DockStyle.Fill;
            this.AnimationsListBox.FormattingEnabled = true;
            this.AnimationsListBox.Location = new System.Drawing.Point(0, 29);
            this.AnimationsListBox.Name = "AnimationsListBox";
            this.AnimationsListBox.Size = new System.Drawing.Size(286, 165);
            this.AnimationsListBox.TabIndex = 4;
            // 
            // AnimationsPanel
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.AnimationsListBox);
            this.Controls.Add(this.Speed);
            this.Controls.Add(this.Seeker);
            this.Controls.Add(this.PlaybackControlsLayoutPanel);
            this.Controls.Add(this.TopButtonsLayoutPanel);
            this.Name = "AnimationsPanel";
            this.Size = new System.Drawing.Size(286, 263);
            this.TopButtonsLayoutPanel.ResumeLayout(false);
            this.PlaybackControlsLayoutPanel.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.Seeker)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.Speed)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.TableLayoutPanel TopButtonsLayoutPanel;
        private System.Windows.Forms.Button ExportButton;
        private System.Windows.Forms.Button ImportButton;
        private System.Windows.Forms.Button DeleteButton;
        private System.Windows.Forms.Button ClearButton;
        private System.Windows.Forms.TableLayoutPanel PlaybackControlsLayoutPanel;
        private System.Windows.Forms.Button PreviousButton;
        private System.Windows.Forms.Button PlayPauseButton;
        private System.Windows.Forms.Button StopButton;
        private System.Windows.Forms.Button NextButton;
        private System.Windows.Forms.NumericUpDown Seeker;
        private System.Windows.Forms.NumericUpDown Speed;
        private System.Windows.Forms.ListBox AnimationsListBox;
    }
}
