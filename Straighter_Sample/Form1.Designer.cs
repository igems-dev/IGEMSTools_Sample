namespace StraighterSample
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
            btnRef = new Button();
            btnConnect = new Button();
            txtY = new TextBox();
            txtX = new TextBox();
            label2 = new Label();
            label1 = new Label();
            timer1 = new System.Windows.Forms.Timer(components);
            SuspendLayout();
            // 
            // btnRef
            // 
            btnRef.Location = new Point(186, 44);
            btnRef.Name = "btnRef";
            btnRef.Size = new Size(94, 29);
            btnRef.TabIndex = 13;
            btnRef.Text = "Reference";
            btnRef.UseVisualStyleBackColor = true;
            btnRef.Click += btnRef_Click;
            // 
            // btnConnect
            // 
            btnConnect.Location = new Point(186, 12);
            btnConnect.Name = "btnConnect";
            btnConnect.Size = new Size(94, 29);
            btnConnect.TabIndex = 12;
            btnConnect.Text = "Connect";
            btnConnect.UseVisualStyleBackColor = true;
            btnConnect.Click += btnConnect_Click;
            // 
            // txtY
            // 
            txtY.Location = new Point(39, 46);
            txtY.Name = "txtY";
            txtY.ReadOnly = true;
            txtY.Size = new Size(125, 27);
            txtY.TabIndex = 11;
            // 
            // txtX
            // 
            txtX.Location = new Point(39, 13);
            txtX.Name = "txtX";
            txtX.ReadOnly = true;
            txtX.Size = new Size(125, 27);
            txtX.TabIndex = 10;
            // 
            // label2
            // 
            label2.AutoSize = true;
            label2.Location = new Point(8, 48);
            label2.Name = "label2";
            label2.Size = new Size(24, 20);
            label2.TabIndex = 9;
            label2.Text = "Y: ";
            // 
            // label1
            // 
            label1.AutoSize = true;
            label1.Location = new Point(8, 13);
            label1.Name = "label1";
            label1.Size = new Size(25, 20);
            label1.TabIndex = 8;
            label1.Text = "X: ";
            // 
            // timer1
            // 
            timer1.Tick += timer1_Tick;
            // 
            // Form1
            // 
            AutoScaleDimensions = new SizeF(8F, 20F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(293, 96);
            Controls.Add(btnRef);
            Controls.Add(btnConnect);
            Controls.Add(txtY);
            Controls.Add(txtX);
            Controls.Add(label2);
            Controls.Add(label1);
            Name = "Form1";
            Text = "Straighter_Sample";
            FormClosing += Form1_FormClosing;
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion

        private Button btnRef;
        private Button btnConnect;
        private TextBox txtY;
        private TextBox txtX;
        private Label label2;
        private Label label1;
        private System.Windows.Forms.Timer timer1;
    }
}
