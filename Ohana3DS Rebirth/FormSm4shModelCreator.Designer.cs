namespace Ohana3DS_Rebirth
{
    partial class FormSm4shModelCreator
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
            this.label1 = new System.Windows.Forms.Label();
            this.InputFileTextBox = new System.Windows.Forms.TextBox();
            this.InputOpenFileButton = new System.Windows.Forms.Button();
            this.label2 = new System.Windows.Forms.Label();
            this.OutputFileTextBox = new System.Windows.Forms.TextBox();
            this.OutputFileOpenButton = new System.Windows.Forms.Button();
            this.CreateButton = new System.Windows.Forms.Button();
            this.CloseButton = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(13, 13);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(65, 13);
            this.label1.TabIndex = 0;
            this.label1.Text = "Input model:";
            // 
            // InputFileTextBox
            // 
            this.InputFileTextBox.Location = new System.Drawing.Point(12, 29);
            this.InputFileTextBox.Name = "InputFileTextBox";
            this.InputFileTextBox.Size = new System.Drawing.Size(334, 20);
            this.InputFileTextBox.TabIndex = 1;
            this.InputFileTextBox.Text = "C:\\Input.obj";
            // 
            // InputOpenFileButton
            // 
            this.InputOpenFileButton.Location = new System.Drawing.Point(352, 29);
            this.InputOpenFileButton.Name = "InputOpenFileButton";
            this.InputOpenFileButton.Size = new System.Drawing.Size(25, 23);
            this.InputOpenFileButton.TabIndex = 2;
            this.InputOpenFileButton.Text = "...";
            this.InputOpenFileButton.UseVisualStyleBackColor = true;
            this.InputOpenFileButton.Click += new System.EventHandler(this.InputOpenFileButton_Click);
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(12, 52);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(103, 13);
            this.label2.TabIndex = 3;
            this.label2.Text = "Output *.mbn model:";
            // 
            // OutputFileTextBox
            // 
            this.OutputFileTextBox.Location = new System.Drawing.Point(12, 69);
            this.OutputFileTextBox.Name = "OutputFileTextBox";
            this.OutputFileTextBox.Size = new System.Drawing.Size(334, 20);
            this.OutputFileTextBox.TabIndex = 4;
            this.OutputFileTextBox.Text = "C:\\Output.mbn";
            // 
            // OutputFileOpenButton
            // 
            this.OutputFileOpenButton.Location = new System.Drawing.Point(352, 69);
            this.OutputFileOpenButton.Name = "OutputFileOpenButton";
            this.OutputFileOpenButton.Size = new System.Drawing.Size(25, 23);
            this.OutputFileOpenButton.TabIndex = 5;
            this.OutputFileOpenButton.Text = "...";
            this.OutputFileOpenButton.UseVisualStyleBackColor = true;
            this.OutputFileOpenButton.Click += new System.EventHandler(this.OutputFileOpenButton_Click);
            // 
            // CreateButton
            // 
            this.CreateButton.Location = new System.Drawing.Point(219, 98);
            this.CreateButton.Name = "CreateButton";
            this.CreateButton.Size = new System.Drawing.Size(75, 23);
            this.CreateButton.TabIndex = 6;
            this.CreateButton.Text = "Create";
            this.CreateButton.UseVisualStyleBackColor = true;
            this.CreateButton.Click += new System.EventHandler(this.CreateButton_Click);
            // 
            // CloseButton
            // 
            this.CloseButton.Location = new System.Drawing.Point(300, 98);
            this.CloseButton.Name = "CloseButton";
            this.CloseButton.Size = new System.Drawing.Size(75, 23);
            this.CloseButton.TabIndex = 7;
            this.CloseButton.Text = "Close";
            this.CloseButton.UseVisualStyleBackColor = true;
            this.CloseButton.Click += new System.EventHandler(this.CloseButton_Click);
            // 
            // FormSm4shModelCreator
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(387, 135);
            this.Controls.Add(this.CloseButton);
            this.Controls.Add(this.CreateButton);
            this.Controls.Add(this.OutputFileOpenButton);
            this.Controls.Add(this.OutputFileTextBox);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.InputOpenFileButton);
            this.Controls.Add(this.InputFileTextBox);
            this.Controls.Add(this.label1);
            this.Name = "FormSm4shModelCreator";
            this.Text = "FormSm4shModelCreator";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.TextBox InputFileTextBox;
        private System.Windows.Forms.Button InputOpenFileButton;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.TextBox OutputFileTextBox;
        private System.Windows.Forms.Button OutputFileOpenButton;
        private System.Windows.Forms.Button CreateButton;
        private System.Windows.Forms.Button CloseButton;
    }
}