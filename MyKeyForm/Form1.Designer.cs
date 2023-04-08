namespace MyKeyForm
{
    partial class Mykey
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
            label1 = new Label();
            StatueLabel = new Label();
            HelpButton = new Button();
            label2 = new Label();
            StartStopLabel = new Label();
            label3 = new Label();
            PressKeyLabel = new Label();
            label4 = new Label();
            IntervalLabel = new Label();
            label5 = new Label();
            NameLabel = new Label();
            Plans = new CheckedListBox();
            SuspendLayout();
            // 
            // label1
            // 
            label1.AutoSize = true;
            label1.Location = new Point(24, 25);
            label1.Name = "label1";
            label1.Size = new Size(44, 17);
            label1.TabIndex = 0;
            label1.Text = "状态：";
            // 
            // StatueLabel
            // 
            StatueLabel.AutoSize = true;
            StatueLabel.Location = new Point(65, 26);
            StatueLabel.Name = "StatueLabel";
            StatueLabel.Size = new Size(32, 17);
            StatueLabel.TabIndex = 1;
            StatueLabel.Text = "停止";
            // 
            // HelpButton
            // 
            HelpButton.Location = new Point(19, 322);
            HelpButton.Name = "HelpButton";
            HelpButton.Size = new Size(75, 23);
            HelpButton.TabIndex = 2;
            HelpButton.Text = "说明";
            HelpButton.UseVisualStyleBackColor = true;
            HelpButton.Click += HelpButton_Click;
            // 
            // label2
            // 
            label2.AutoSize = true;
            label2.Location = new Point(24, 60);
            label2.Name = "label2";
            label2.Size = new Size(73, 17);
            label2.TabIndex = 3;
            label2.Text = "开启/停止：";
            // 
            // StartStopLabel
            // 
            StartStopLabel.AutoSize = true;
            StartStopLabel.Location = new Point(111, 60);
            StartStopLabel.Name = "StartStopLabel";
            StartStopLabel.Size = new Size(0, 17);
            StartStopLabel.TabIndex = 4;
            // 
            // label3
            // 
            label3.AutoSize = true;
            label3.Location = new Point(25, 115);
            label3.Name = "label3";
            label3.Size = new Size(44, 17);
            label3.TabIndex = 5;
            label3.Text = "按键：";
            // 
            // PressKeyLabel
            // 
            PressKeyLabel.AutoSize = true;
            PressKeyLabel.Location = new Point(96, 115);
            PressKeyLabel.Name = "PressKeyLabel";
            PressKeyLabel.Size = new Size(0, 17);
            PressKeyLabel.TabIndex = 6;
            // 
            // label4
            // 
            label4.AutoSize = true;
            label4.Location = new Point(25, 143);
            label4.Name = "label4";
            label4.Size = new Size(44, 17);
            label4.TabIndex = 7;
            label4.Text = "间隔：";
            // 
            // IntervalLabel
            // 
            IntervalLabel.AutoSize = true;
            IntervalLabel.Location = new Point(96, 143);
            IntervalLabel.Name = "IntervalLabel";
            IntervalLabel.Size = new Size(15, 17);
            IntervalLabel.TabIndex = 8;
            IntervalLabel.Text = "0";
            // 
            // label5
            // 
            label5.AutoSize = true;
            label5.Location = new Point(24, 87);
            label5.Name = "label5";
            label5.Size = new Size(44, 17);
            label5.TabIndex = 9;
            label5.Text = "名称：";
            // 
            // NameLabel
            // 
            NameLabel.AutoSize = true;
            NameLabel.Location = new Point(96, 87);
            NameLabel.Name = "NameLabel";
            NameLabel.Size = new Size(0, 17);
            NameLabel.TabIndex = 10;
            // 
            // Plans
            // 
            Plans.FormattingEnabled = true;
            Plans.Location = new Point(25, 175);
            Plans.Name = "Plans";
            Plans.Size = new Size(270, 130);
            Plans.TabIndex = 11;
            Plans.SelectedIndexChanged += checkedListBox1_SelectedIndexChanged;
            // 
            // Mykey
            // 
            AutoScaleDimensions = new SizeF(7F, 17F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(327, 353);
            Controls.Add(Plans);
            Controls.Add(NameLabel);
            Controls.Add(label5);
            Controls.Add(IntervalLabel);
            Controls.Add(label4);
            Controls.Add(PressKeyLabel);
            Controls.Add(label3);
            Controls.Add(StartStopLabel);
            Controls.Add(label2);
            Controls.Add(HelpButton);
            Controls.Add(StatueLabel);
            Controls.Add(label1);
            Name = "Mykey";
            Text = "Mykey";
            Load += Form1_Load;
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion

        private Label label1;
        private Label StatueLabel;
        private Button HelpButton;
        private Label label2;
        private Label StartStopLabel;
        private Label label3;
        private Label PressKeyLabel;
        private Label label4;
        private Label IntervalLabel;
        private Label label5;
        private Label NameLabel;
        private CheckedListBox Plans;
    }
}