namespace RounderSample
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
            components = new System.ComponentModel.Container();
            label1 = new Label();
            label2 = new Label();
            label3 = new Label();
            txtX = new TextBox();
            txtY = new TextBox();
            txtZ = new TextBox();
            btnConnect = new Button();
            btnRef = new Button();
            timerRead = new System.Windows.Forms.Timer(components);
            SuspendLayout();
            // 
            // label1
            // 
            label1.AutoSize = true;
            label1.Location = new Point(12, 9);
            label1.Name = "label1";
            label1.Size = new Size(25, 20);
            label1.TabIndex = 0;
            label1.Text = "X: ";
            // 
            // label2
            // 
            label2.AutoSize = true;
            label2.Location = new Point(12, 44);
            label2.Name = "label2";
            label2.Size = new Size(24, 20);
            label2.TabIndex = 1;
            label2.Text = "Y: ";
            // 
            // label3
            // 
            label3.AutoSize = true;
            label3.Location = new Point(12, 79);
            label3.Name = "label3";
            label3.Size = new Size(21, 20);
            label3.TabIndex = 2;
            label3.Text = "Z:";
            // 
            // txtX
            // 
            txtX.Location = new Point(43, 9);
            txtX.Name = "txtX";
            txtX.ReadOnly = true;
            txtX.Size = new Size(125, 27);
            txtX.TabIndex = 3;
            // 
            // txtY
            // 
            txtY.Location = new Point(43, 42);
            txtY.Name = "txtY";
            txtY.ReadOnly = true;
            txtY.Size = new Size(125, 27);
            txtY.TabIndex = 4;
            // 
            // txtZ
            // 
            txtZ.Location = new Point(43, 76);
            txtZ.Name = "txtZ";
            txtZ.ReadOnly = true;
            txtZ.Size = new Size(125, 27);
            txtZ.TabIndex = 5;
            // 
            // btnConnect
            // 
            btnConnect.Location = new Point(190, 8);
            btnConnect.Name = "btnConnect";
            btnConnect.Size = new Size(94, 29);
            btnConnect.TabIndex = 6;
            btnConnect.Text = "Connect";
            btnConnect.UseVisualStyleBackColor = true;
            btnConnect.Click += btnConnect_Click;
            // 
            // btnRef
            // 
            btnRef.Location = new Point(190, 40);
            btnRef.Name = "btnRef";
            btnRef.Size = new Size(94, 29);
            btnRef.TabIndex = 7;
            btnRef.Text = "Reference";
            btnRef.UseVisualStyleBackColor = true;
            btnRef.Click += btnRef_Click;
            // 
            // timerRead
            // 
            timerRead.Tick += timerRead_Tick;
            // 
            // Form1
            // 
            AutoScaleDimensions = new SizeF(8F, 20F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(300, 117);
            Controls.Add(btnRef);
            Controls.Add(btnConnect);
            Controls.Add(txtZ);
            Controls.Add(txtY);
            Controls.Add(txtX);
            Controls.Add(label3);
            Controls.Add(label2);
            Controls.Add(label1);
            FormBorderStyle = FormBorderStyle.FixedSingle;
            MaximizeBox = false;
            MinimizeBox = false;
            Name = "Form1";
            Text = "Rounder Sample";
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion

        private Label label1;
        private Label label2;
        private Label label3;
        private TextBox txtX;
        private TextBox txtY;
        private TextBox txtZ;
        private Button btnConnect;
        private Button btnRef;
        private System.Windows.Forms.Timer timerRead;
    }
}
