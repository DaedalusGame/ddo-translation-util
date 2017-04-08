namespace TranslationUtil
{
    partial class Form1
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
            this.btn_open = new System.Windows.Forms.Button();
            this.progress_read = new System.Windows.Forms.ProgressBar();
            this.background_load = new System.ComponentModel.BackgroundWorker();
            this.button1 = new System.Windows.Forms.Button();
            this.btn_count = new System.Windows.Forms.Button();
            this.txt_output = new System.Windows.Forms.TextBox();
            this.btn_save = new System.Windows.Forms.Button();
            this.background_save = new System.ComponentModel.BackgroundWorker();
            this.SuspendLayout();
            // 
            // btn_open
            // 
            this.btn_open.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.btn_open.Location = new System.Drawing.Point(360, 12);
            this.btn_open.Name = "btn_open";
            this.btn_open.Size = new System.Drawing.Size(147, 23);
            this.btn_open.TabIndex = 1;
            this.btn_open.Text = "Select Folder";
            this.btn_open.UseVisualStyleBackColor = true;
            this.btn_open.Click += new System.EventHandler(this.btn_open_Click);
            // 
            // progress_read
            // 
            this.progress_read.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.progress_read.Location = new System.Drawing.Point(12, 290);
            this.progress_read.Name = "progress_read";
            this.progress_read.Size = new System.Drawing.Size(342, 23);
            this.progress_read.TabIndex = 2;
            // 
            // background_load
            // 
            this.background_load.WorkerReportsProgress = true;
            this.background_load.DoWork += new System.ComponentModel.DoWorkEventHandler(this.background_load_DoWork);
            this.background_load.ProgressChanged += new System.ComponentModel.ProgressChangedEventHandler(this.background_load_ProgressChanged);
            this.background_load.RunWorkerCompleted += new System.ComponentModel.RunWorkerCompletedEventHandler(this.background_load_RunWorkerCompleted);
            // 
            // button1
            // 
            this.button1.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.button1.Enabled = false;
            this.button1.Location = new System.Drawing.Point(360, 70);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(147, 23);
            this.button1.TabIndex = 3;
            this.button1.Text = "Start";
            this.button1.UseVisualStyleBackColor = true;
            this.button1.Click += new System.EventHandler(this.button1_Click);
            // 
            // btn_count
            // 
            this.btn_count.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.btn_count.Enabled = false;
            this.btn_count.Location = new System.Drawing.Point(360, 41);
            this.btn_count.Name = "btn_count";
            this.btn_count.Size = new System.Drawing.Size(147, 23);
            this.btn_count.TabIndex = 4;
            this.btn_count.Text = "Count";
            this.btn_count.UseVisualStyleBackColor = true;
            this.btn_count.Click += new System.EventHandler(this.btn_count_Click);
            // 
            // txt_output
            // 
            this.txt_output.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.txt_output.Location = new System.Drawing.Point(12, 12);
            this.txt_output.Multiline = true;
            this.txt_output.Name = "txt_output";
            this.txt_output.ReadOnly = true;
            this.txt_output.ScrollBars = System.Windows.Forms.ScrollBars.Vertical;
            this.txt_output.Size = new System.Drawing.Size(342, 272);
            this.txt_output.TabIndex = 5;
            // 
            // btn_save
            // 
            this.btn_save.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.btn_save.Enabled = false;
            this.btn_save.Location = new System.Drawing.Point(360, 99);
            this.btn_save.Name = "btn_save";
            this.btn_save.Size = new System.Drawing.Size(147, 23);
            this.btn_save.TabIndex = 6;
            this.btn_save.Text = "Save Result";
            this.btn_save.UseVisualStyleBackColor = true;
            this.btn_save.Click += new System.EventHandler(this.btn_save_Click);
            // 
            // background_save
            // 
            this.background_save.WorkerReportsProgress = true;
            this.background_save.DoWork += new System.ComponentModel.DoWorkEventHandler(this.background_save_DoWork);
            this.background_save.ProgressChanged += new System.ComponentModel.ProgressChangedEventHandler(this.background_save_ProgressChanged);
            this.background_save.RunWorkerCompleted += new System.ComponentModel.RunWorkerCompletedEventHandler(this.background_save_RunWorkerCompleted);
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(519, 325);
            this.Controls.Add(this.btn_save);
            this.Controls.Add(this.txt_output);
            this.Controls.Add(this.btn_count);
            this.Controls.Add(this.button1);
            this.Controls.Add(this.progress_read);
            this.Controls.Add(this.btn_open);
            this.Name = "Form1";
            this.Text = "TranslationUtil";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button btn_open;
        private System.Windows.Forms.ProgressBar progress_read;
        private System.ComponentModel.BackgroundWorker background_load;
        private System.Windows.Forms.Button button1;
        private System.Windows.Forms.Button btn_count;
        private System.Windows.Forms.TextBox txt_output;
        private System.Windows.Forms.Button btn_save;
        private System.ComponentModel.BackgroundWorker background_save;
    }
}

