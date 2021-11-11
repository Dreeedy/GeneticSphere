
namespace GeneticSphere
{
    partial class Form1
    {
        /// <summary>
        ///  Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        ///  Clean up any resources being used.
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
        ///  Required method for Designer support - do not modify
        ///  the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.components = new System.ComponentModel.Container();
            this.timer1 = new System.Windows.Forms.Timer(this.components);
            this.splitContainer1 = new System.Windows.Forms.SplitContainer();
            this.button_pause = new System.Windows.Forms.Button();
            this.button_resume = new System.Windows.Forms.Button();
            this.cb_RenderToggle = new System.Windows.Forms.CheckBox();
            this.label_Frogs = new System.Windows.Forms.Label();
            this.label_CountTurns = new System.Windows.Forms.Label();
            this.label_GenerationNumber = new System.Windows.Forms.Label();
            this.label_FrogsHelfPoints = new System.Windows.Forms.Label();
            this.button_StartNewWorld = new System.Windows.Forms.Button();
            this.label1 = new System.Windows.Forms.Label();
            this.numResolution = new System.Windows.Forms.NumericUpDown();
            this.pictureBox1 = new System.Windows.Forms.PictureBox();
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer1)).BeginInit();
            this.splitContainer1.Panel1.SuspendLayout();
            this.splitContainer1.Panel2.SuspendLayout();
            this.splitContainer1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.numResolution)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).BeginInit();
            this.SuspendLayout();
            // 
            // timer1
            // 
            this.timer1.Interval = 1;
            this.timer1.Tick += new System.EventHandler(this.timer1_Tick);
            // 
            // splitContainer1
            // 
            this.splitContainer1.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.splitContainer1.Cursor = System.Windows.Forms.Cursors.Default;
            this.splitContainer1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.splitContainer1.FixedPanel = System.Windows.Forms.FixedPanel.Panel1;
            this.splitContainer1.Location = new System.Drawing.Point(0, 0);
            this.splitContainer1.Name = "splitContainer1";
            // 
            // splitContainer1.Panel1
            // 
            this.splitContainer1.Panel1.Controls.Add(this.button_pause);
            this.splitContainer1.Panel1.Controls.Add(this.button_resume);
            this.splitContainer1.Panel1.Controls.Add(this.cb_RenderToggle);
            this.splitContainer1.Panel1.Controls.Add(this.label_Frogs);
            this.splitContainer1.Panel1.Controls.Add(this.label_CountTurns);
            this.splitContainer1.Panel1.Controls.Add(this.label_GenerationNumber);
            this.splitContainer1.Panel1.Controls.Add(this.label_FrogsHelfPoints);
            this.splitContainer1.Panel1.Controls.Add(this.button_StartNewWorld);
            this.splitContainer1.Panel1.Controls.Add(this.label1);
            this.splitContainer1.Panel1.Controls.Add(this.numResolution);
            // 
            // splitContainer1.Panel2
            // 
            this.splitContainer1.Panel2.Controls.Add(this.pictureBox1);
            this.splitContainer1.Size = new System.Drawing.Size(1336, 507);
            this.splitContainer1.SplitterDistance = 412;
            this.splitContainer1.TabIndex = 0;
            // 
            // button_pause
            // 
            this.button_pause.Location = new System.Drawing.Point(12, 158);
            this.button_pause.Name = "button_pause";
            this.button_pause.Size = new System.Drawing.Size(150, 23);
            this.button_pause.TabIndex = 25;
            this.button_pause.Text = "Пауза";
            this.button_pause.UseVisualStyleBackColor = true;
            this.button_pause.Click += new System.EventHandler(this.button_pause_Click);
            // 
            // button_resume
            // 
            this.button_resume.Location = new System.Drawing.Point(12, 187);
            this.button_resume.Name = "button_resume";
            this.button_resume.Size = new System.Drawing.Size(150, 23);
            this.button_resume.TabIndex = 24;
            this.button_resume.Text = "Продолжить";
            this.button_resume.UseVisualStyleBackColor = true;
            this.button_resume.Click += new System.EventHandler(this.button_resume_Click);
            // 
            // cb_RenderToggle
            // 
            this.cb_RenderToggle.AutoSize = true;
            this.cb_RenderToggle.Checked = true;
            this.cb_RenderToggle.CheckState = System.Windows.Forms.CheckState.Checked;
            this.cb_RenderToggle.Location = new System.Drawing.Point(12, 56);
            this.cb_RenderToggle.Name = "cb_RenderToggle";
            this.cb_RenderToggle.Size = new System.Drawing.Size(102, 19);
            this.cb_RenderToggle.TabIndex = 23;
            this.cb_RenderToggle.Text = "Визуализация";
            this.cb_RenderToggle.UseVisualStyleBackColor = true;
            // 
            // label_Frogs
            // 
            this.label_Frogs.AutoSize = true;
            this.label_Frogs.Location = new System.Drawing.Point(168, 29);
            this.label_Frogs.Name = "label_Frogs";
            this.label_Frogs.Size = new System.Drawing.Size(32, 15);
            this.label_Frogs.TabIndex = 22;
            this.label_Frogs.Text = "Учет";
            // 
            // label_CountTurns
            // 
            this.label_CountTurns.AutoSize = true;
            this.label_CountTurns.Location = new System.Drawing.Point(267, 9);
            this.label_CountTurns.Name = "label_CountTurns";
            this.label_CountTurns.Size = new System.Drawing.Size(73, 15);
            this.label_CountTurns.TabIndex = 21;
            this.label_CountTurns.Text = "Номер хода";
            // 
            // label_GenerationNumber
            // 
            this.label_GenerationNumber.AutoSize = true;
            this.label_GenerationNumber.Location = new System.Drawing.Point(168, 9);
            this.label_GenerationNumber.Name = "label_GenerationNumber";
            this.label_GenerationNumber.Size = new System.Drawing.Size(69, 15);
            this.label_GenerationNumber.TabIndex = 20;
            this.label_GenerationNumber.Text = "Поколение";
            // 
            // label_FrogsHelfPoints
            // 
            this.label_FrogsHelfPoints.AutoSize = true;
            this.label_FrogsHelfPoints.Location = new System.Drawing.Point(168, 74);
            this.label_FrogsHelfPoints.Name = "label_FrogsHelfPoints";
            this.label_FrogsHelfPoints.Size = new System.Drawing.Size(48, 15);
            this.label_FrogsHelfPoints.TabIndex = 19;
            this.label_FrogsHelfPoints.Text = "HP жаб";
            // 
            // button_StartNewWorld
            // 
            this.button_StartNewWorld.Location = new System.Drawing.Point(12, 100);
            this.button_StartNewWorld.Name = "button_StartNewWorld";
            this.button_StartNewWorld.Size = new System.Drawing.Size(150, 23);
            this.button_StartNewWorld.TabIndex = 12;
            this.button_StartNewWorld.Text = "Старт нового мира";
            this.button_StartNewWorld.UseVisualStyleBackColor = true;
            this.button_StartNewWorld.Click += new System.EventHandler(this.button_StartNewWorld_Click);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(12, 9);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(141, 15);
            this.label1.TabIndex = 13;
            this.label1.Text = "Размер одной клетки PX";
            // 
            // numResolution
            // 
            this.numResolution.Increment = new decimal(new int[] {
            3,
            0,
            0,
            0});
            this.numResolution.Location = new System.Drawing.Point(12, 27);
            this.numResolution.Maximum = new decimal(new int[] {
            15,
            0,
            0,
            0});
            this.numResolution.Minimum = new decimal(new int[] {
            3,
            0,
            0,
            0});
            this.numResolution.Name = "numResolution";
            this.numResolution.Size = new System.Drawing.Size(141, 23);
            this.numResolution.TabIndex = 15;
            this.numResolution.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            this.numResolution.Value = new decimal(new int[] {
            3,
            0,
            0,
            0});
            // 
            // pictureBox1
            // 
            this.pictureBox1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.pictureBox1.Location = new System.Drawing.Point(0, 0);
            this.pictureBox1.Name = "pictureBox1";
            this.pictureBox1.Size = new System.Drawing.Size(916, 503);
            this.pictureBox1.SizeMode = System.Windows.Forms.PictureBoxSizeMode.CenterImage;
            this.pictureBox1.TabIndex = 14;
            this.pictureBox1.TabStop = false;
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 15F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1336, 507);
            this.Controls.Add(this.splitContainer1);
            this.Name = "Form1";
            this.Text = "GeneticSphere";
            this.WindowState = System.Windows.Forms.FormWindowState.Maximized;
            this.splitContainer1.Panel1.ResumeLayout(false);
            this.splitContainer1.Panel1.PerformLayout();
            this.splitContainer1.Panel2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer1)).EndInit();
            this.splitContainer1.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.numResolution)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion
        private System.Windows.Forms.Timer timer1;
        private System.Windows.Forms.SplitContainer splitContainer1;
        private System.Windows.Forms.Button button_StartNewWorld;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.NumericUpDown numResolution;
        private System.Windows.Forms.PictureBox pictureBox1;
        private System.Windows.Forms.Label label_FrogsHelfPoints;
        private System.Windows.Forms.Label label_CountTurns;
        private System.Windows.Forms.Label label_GenerationNumber;
        private System.Windows.Forms.Label label_Frogs;
        private System.Windows.Forms.CheckBox cb_RenderToggle;
        private System.Windows.Forms.Button button_pause;
        private System.Windows.Forms.Button button_resume;
    }
}

