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
            label3 = new Label();
            label4 = new Label();
            label5 = new Label();
            Plans = new CheckedListBox();
            ModifyHotkeyButton = new Button();
            AdminTest = new Label();
            NameTextBox = new TextBox();
            PressKeyTextBox = new TextBox();
            IntervalTextBox = new TextBox();
            CreateButton = new Button();
            DeleteButton = new Button();
            GitHubButton = new Button();
            SuspendLayout();
            // 
            // label1
            // 
            label1.AutoSize = true;
            label1.Location = new Point(24, 25);
            label1.Name = "label1";
            label1.Size = new Size(68, 17);
            label1.TabIndex = 0;
            label1.Text = "运行状态：";
            // 
            // StatueLabel
            // 
            StatueLabel.AutoSize = true;
            StatueLabel.Location = new Point(100, 25);
            StatueLabel.Name = "StatueLabel";
            StatueLabel.Size = new Size(32, 17);
            StatueLabel.TabIndex = 1;
            StatueLabel.Text = "停止";
            // 
            // HelpButton
            // 
            HelpButton.Location = new Point(25, 317);
            HelpButton.Name = "HelpButton";
            HelpButton.Size = new Size(30, 26);
            HelpButton.TabIndex = 2;
            HelpButton.Text = "?";
            HelpButton.UseVisualStyleBackColor = true;
            HelpButton.Click += HelpButton_Click;
            // 
            // label2
            // 
            label2.AutoSize = true;
            label2.Location = new Point(25, 56);
            label2.Name = "label2";
            label2.Size = new Size(73, 17);
            label2.TabIndex = 3;
            label2.Text = "开始/停止：";
            // 
            // label3
            // 
            label3.AutoSize = true;
            label3.Location = new Point(25, 113);
            label3.Name = "label3";
            label3.Size = new Size(68, 17);
            label3.TabIndex = 5;
            label3.Text = "按键列表：";
            // 
            // label4
            // 
            label4.AutoSize = true;
            label4.Location = new Point(25, 141);
            label4.Name = "label4";
            label4.Size = new Size(68, 17);
            label4.TabIndex = 7;
            label4.Text = "按键间隔：";
            // 
            // label5
            // 
            label5.AutoSize = true;
            label5.Location = new Point(25, 85);
            label5.Name = "label5";
            label5.Size = new Size(68, 17);
            label5.TabIndex = 9;
            label5.Text = "方案名称：";
            // 
            // Plans
            // 
            Plans.FormattingEnabled = true;
            Plans.Location = new Point(25, 175);
            Plans.Name = "Plans";
            Plans.Size = new Size(261, 130);
            Plans.TabIndex = 11;
            Plans.SelectedIndexChanged += checkedListBox1_SelectedIndexChanged;
            // 
            // ModifyHotkeyButton
            // 
            ModifyHotkeyButton.Location = new Point(100, 54);
            ModifyHotkeyButton.Name = "ModifyHotkeyButton";
            ModifyHotkeyButton.Size = new Size(185, 23);
            ModifyHotkeyButton.TabIndex = 12;
            ModifyHotkeyButton.Text = "修改";
            ModifyHotkeyButton.UseVisualStyleBackColor = true;
            ModifyHotkeyButton.Click += ModifyHotkeyButton_Click;
            // 
            // AdminTest
            // 
            AdminTest.AutoSize = true;
            AdminTest.Location = new Point(24, 9);
            AdminTest.Name = "AdminTest";
            AdminTest.Size = new Size(0, 17);
            AdminTest.TabIndex = 13;
            // 
            // NameTextBox
            // 
            NameTextBox.Location = new Point(100, 82);
            NameTextBox.Margin = new Padding(2, 3, 2, 3);
            NameTextBox.Name = "NameTextBox";
            NameTextBox.Size = new Size(186, 23);
            NameTextBox.TabIndex = 14;
            NameTextBox.TextChanged += NameTextBox_TextChanged;
            // 
            // PressKeyTextBox
            // 
            PressKeyTextBox.Location = new Point(100, 110);
            PressKeyTextBox.Margin = new Padding(2, 3, 2, 3);
            PressKeyTextBox.Name = "PressKeyTextBox";
            PressKeyTextBox.Size = new Size(186, 23);
            PressKeyTextBox.TabIndex = 15;
            PressKeyTextBox.TextChanged += PressKeyTextBox_TextChanged;
            // 
            // IntervalTextBox
            // 
            IntervalTextBox.Location = new Point(100, 139);
            IntervalTextBox.Margin = new Padding(2, 3, 2, 3);
            IntervalTextBox.Name = "IntervalTextBox";
            IntervalTextBox.Size = new Size(186, 23);
            IntervalTextBox.TabIndex = 16;
            IntervalTextBox.TextChanged += IntervalTextBox_TextChanged;
            // 
            // CreateButton
            // 
            CreateButton.Location = new Point(130, 318);
            CreateButton.Margin = new Padding(2, 3, 2, 3);
            CreateButton.Name = "CreateButton";
            CreateButton.Size = new Size(73, 25);
            CreateButton.TabIndex = 17;
            CreateButton.Text = "新建方案";
            CreateButton.UseVisualStyleBackColor = true;
            CreateButton.Click += CreateButton_Click;
            // 
            // DeleteButton
            // 
            DeleteButton.Location = new Point(207, 318);
            DeleteButton.Margin = new Padding(2, 3, 2, 3);
            DeleteButton.Name = "DeleteButton";
            DeleteButton.Size = new Size(73, 25);
            DeleteButton.TabIndex = 18;
            DeleteButton.Text = "删除方案";
            DeleteButton.UseVisualStyleBackColor = true;
            DeleteButton.Click += DeleteButton_Click;
            // 
            // GitHubButton
            // 
            GitHubButton.Location = new Point(60, 318);
            GitHubButton.Name = "GitHubButton";
            GitHubButton.Size = new Size(65, 25);
            GitHubButton.TabIndex = 19;
            GitHubButton.Text = "GitHub";
            GitHubButton.UseVisualStyleBackColor = true;
            GitHubButton.Click += GitHubButton_Click;
            // 
            // Mykey
            // 
            AutoScaleDimensions = new SizeF(7F, 17F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(315, 359);
            Controls.Add(GitHubButton);
            Controls.Add(DeleteButton);
            Controls.Add(CreateButton);
            Controls.Add(IntervalTextBox);
            Controls.Add(PressKeyTextBox);
            Controls.Add(NameTextBox);
            Controls.Add(AdminTest);
            Controls.Add(ModifyHotkeyButton);
            Controls.Add(Plans);
            Controls.Add(label5);
            Controls.Add(label4);
            Controls.Add(label3);
            Controls.Add(label2);
            Controls.Add(HelpButton);
            Controls.Add(StatueLabel);
            Controls.Add(label1);
            FormBorderStyle = FormBorderStyle.FixedSingle;
            KeyPreview = true;
            MaximizeBox = false;
            Name = "Mykey";
            Text = "MyKey";
            Load += Form1_Load;
            KeyDown += Mykey_KeyDown;
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion

        private Label label1;
        private Label StatueLabel;
        private Button HelpButton;
        private Label label2;
        private Label label3;
        private Label label4;
        private Label label5;
        private CheckedListBox Plans;
        private Button ModifyHotkeyButton;
        private Label AdminTest;
        private TextBox NameTextBox;
        private TextBox PressKeyTextBox;
        private TextBox IntervalTextBox;
        private Button CreateButton;
        private Button DeleteButton;
        private Button GitHubButton;
    }
}