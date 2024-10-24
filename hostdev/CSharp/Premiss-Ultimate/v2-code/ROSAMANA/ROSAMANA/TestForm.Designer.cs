namespace ROSAMANA
{
    partial class TestForm
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
            this.mediaFilesBlk1 = new ROSAMANA.MediaFilesBlk();
            this.SuspendLayout();
            // 
            // mediaFilesBlk1
            // 
            this.mediaFilesBlk1._FONT = new System.Drawing.Font("微软雅黑", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.mediaFilesBlk1._LblForeColor = System.Drawing.SystemColors.ControlText;
            this.mediaFilesBlk1._Transparency = 255;
            this.mediaFilesBlk1.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(192)))), ((int)(((byte)(192)))));
            this.mediaFilesBlk1.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.mediaFilesBlk1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.mediaFilesBlk1.Location = new System.Drawing.Point(0, 0);
            this.mediaFilesBlk1.Name = "mediaFilesBlk1";
            this.mediaFilesBlk1.Size = new System.Drawing.Size(622, 536);
            this.mediaFilesBlk1.TabIndex = 0;

            // 
            // TestForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 15F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(622, 536);
            this.Controls.Add(this.mediaFilesBlk1);
            this.DoubleBuffered = true;
            this.Name = "TestForm";
            this.Text = "TestForm";
            this.Load += new System.EventHandler(this.TestForm_Load);
            this.ResumeLayout(false);

        }

        #endregion

        private MediaFilesBlk mediaFilesBlk1;


    }
}