
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
            this.lab_Frogs = new System.Windows.Forms.Label();
            this.lab_LifeDuration = new System.Windows.Forms.Label();
            this.lab_GenerationNumber = new System.Windows.Forms.Label();
            this.lab_FrogsHelfPoints = new System.Windows.Forms.Label();
            this.stopBut = new System.Windows.Forms.Button();
            this.startBut = new System.Windows.Forms.Button();
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
            this.splitContainer1.Cursor = System.Windows.Forms.Cursors.VSplit;
            this.splitContainer1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.splitContainer1.FixedPanel = System.Windows.Forms.FixedPanel.Panel1;
            this.splitContainer1.Location = new System.Drawing.Point(0, 0);
            this.splitContainer1.Name = "splitContainer1";
            // 
            // splitContainer1.Panel1
            // 
            this.splitContainer1.Panel1.Controls.Add(this.lab_Frogs);
            this.splitContainer1.Panel1.Controls.Add(this.lab_LifeDuration);
            this.splitContainer1.Panel1.Controls.Add(this.lab_GenerationNumber);
            this.splitContainer1.Panel1.Controls.Add(this.lab_FrogsHelfPoints);
            this.splitContainer1.Panel1.Controls.Add(this.stopBut);
            this.splitContainer1.Panel1.Controls.Add(this.startBut);
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
            // lab_Frogs
            // 
            this.lab_Frogs.AutoSize = true;
            this.lab_Frogs.Location = new System.Drawing.Point(206, 27);
            this.lab_Frogs.Name = "lab_Frogs";
            this.lab_Frogs.Size = new System.Drawing.Size(38, 15);
            this.lab_Frogs.TabIndex = 22;
            this.lab_Frogs.Text = "label2";
            // 
            // lab_LifeDuration
            // 
            this.lab_LifeDuration.AutoSize = true;
            this.lab_LifeDuration.Location = new System.Drawing.Point(138, 42);
            this.lab_LifeDuration.Name = "lab_LifeDuration";
            this.lab_LifeDuration.Size = new System.Drawing.Size(38, 15);
            this.lab_LifeDuration.TabIndex = 21;
            this.lab_LifeDuration.Text = "label2";
            // 
            // lab_GenerationNumber
            // 
            this.lab_GenerationNumber.AutoSize = true;
            this.lab_GenerationNumber.Location = new System.Drawing.Point(138, 27);
            this.lab_GenerationNumber.Name = "lab_GenerationNumber";
            this.lab_GenerationNumber.Size = new System.Drawing.Size(38, 15);
            this.lab_GenerationNumber.TabIndex = 20;
            this.lab_GenerationNumber.Text = "label2";
            // 
            // lab_FrogsHelfPoints
            // 
            this.lab_FrogsHelfPoints.AutoSize = true;
            this.lab_FrogsHelfPoints.Location = new System.Drawing.Point(10, 155);
            this.lab_FrogsHelfPoints.Name = "lab_FrogsHelfPoints";
            this.lab_FrogsHelfPoints.Size = new System.Drawing.Size(38, 15);
            this.lab_FrogsHelfPoints.TabIndex = 19;
            this.lab_FrogsHelfPoints.Text = "label2";
            // 
            // stopBut
            // 
            this.stopBut.Location = new System.Drawing.Point(12, 129);
            this.stopBut.Name = "stopBut";
            this.stopBut.Size = new System.Drawing.Size(75, 23);
            this.stopBut.TabIndex = 18;
            this.stopBut.Text = "Stop";
            this.stopBut.UseVisualStyleBackColor = true;
            this.stopBut.Click += new System.EventHandler(this.stopBut_Click);
            // 
            // startBut
            // 
            this.startBut.Location = new System.Drawing.Point(12, 100);
            this.startBut.Name = "startBut";
            this.startBut.Size = new System.Drawing.Size(75, 23);
            this.startBut.TabIndex = 12;
            this.startBut.Text = "Start";
            this.startBut.UseVisualStyleBackColor = true;
            this.startBut.Click += new System.EventHandler(this.startBut_Click);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(12, 9);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(63, 15);
            this.label1.TabIndex = 13;
            this.label1.Text = "Resolution";
            // 
            // numResolution
            // 
            this.numResolution.Location = new System.Drawing.Point(12, 27);
            this.numResolution.Maximum = new decimal(new int[] {
            25,
            0,
            0,
            0});
            this.numResolution.Minimum = new decimal(new int[] {
            1,
            0,
            0,
            0});
            this.numResolution.Name = "numResolution";
            this.numResolution.Size = new System.Drawing.Size(120, 23);
            this.numResolution.TabIndex = 15;
            this.numResolution.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
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
            this.Text = "Form1";
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
        private System.Windows.Forms.Button stopBut;
        private System.Windows.Forms.Button startBut;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.NumericUpDown numResolution;
        private System.Windows.Forms.PictureBox pictureBox1;
        private System.Windows.Forms.Label lab_FrogsHelfPoints;
        private System.Windows.Forms.Label lab_LifeDuration;
        private System.Windows.Forms.Label lab_GenerationNumber;
        private System.Windows.Forms.Label lab_Frogs;
    }
}

