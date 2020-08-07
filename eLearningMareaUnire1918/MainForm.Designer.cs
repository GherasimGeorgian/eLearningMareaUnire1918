namespace eLearningMareaUnire1918
{
    partial class MainForm
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
            this.start_app = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // start_app
            // 
            this.start_app.Location = new System.Drawing.Point(350, 161);
            this.start_app.Name = "start_app";
            this.start_app.Size = new System.Drawing.Size(129, 42);
            this.start_app.TabIndex = 0;
            this.start_app.Text = "Start";
            this.start_app.UseVisualStyleBackColor = true;
            this.start_app.Click += new System.EventHandler(this.start_app_Click);
            // 
            // MainForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(800, 450);
            this.Controls.Add(this.start_app);
            this.Name = "MainForm";
            this.Text = "MainForm";
            this.Load += new System.EventHandler(this.MainForm_Load);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Button start_app;
    }
}

