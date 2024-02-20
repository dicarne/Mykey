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
            playAudioCheckBox = new CheckBox();
            tabControl1 = new TabControl();
            keyPage = new TabPage();
            scriptPanel = new TabPage();
            ScriptList = new CheckedListBox();
            apiPage = new TabPage();
            label7 = new Label();
            apiStatueText = new Label();
            apiServerButton = new Button();
            label6 = new Label();
            tabControl1.SuspendLayout();
            keyPage.SuspendLayout();
            scriptPanel.SuspendLayout();
            apiPage.SuspendLayout();
            SuspendLayout();
            // 
            // label1
            // 
            label1.AutoSize = true;
            label1.Location = new Point(24, 28);
            label1.Margin = new Padding(4, 0, 4, 0);
            label1.Name = "label1";
            label1.Size = new Size(84, 20);
            label1.TabIndex = 0;
            label1.Text = "运行状态：";
            // 
            // StatueLabel
            // 
            StatueLabel.AutoSize = true;
            StatueLabel.Location = new Point(122, 28);
            StatueLabel.Margin = new Padding(4, 0, 4, 0);
            StatueLabel.Name = "StatueLabel";
            StatueLabel.Size = new Size(39, 20);
            StatueLabel.TabIndex = 1;
            StatueLabel.Text = "停止";
            // 
            // HelpButton
            // 
            HelpButton.Location = new Point(6, 314);
            HelpButton.Margin = new Padding(4);
            HelpButton.Name = "HelpButton";
            HelpButton.Size = new Size(39, 31);
            HelpButton.TabIndex = 2;
            HelpButton.Text = "?";
            HelpButton.UseVisualStyleBackColor = true;
            HelpButton.Click += HelpButton_Click;
            // 
            // label2
            // 
            label2.AutoSize = true;
            label2.Location = new Point(25, 65);
            label2.Margin = new Padding(4, 0, 4, 0);
            label2.Name = "label2";
            label2.Size = new Size(90, 20);
            label2.TabIndex = 3;
            label2.Text = "开始/停止：";
            // 
            // label3
            // 
            label3.AutoSize = true;
            label3.Location = new Point(1, 51);
            label3.Margin = new Padding(4, 0, 4, 0);
            label3.Name = "label3";
            label3.Size = new Size(84, 20);
            label3.TabIndex = 5;
            label3.Text = "按键列表：";
            // 
            // label4
            // 
            label4.AutoSize = true;
            label4.Location = new Point(1, 84);
            label4.Margin = new Padding(4, 0, 4, 0);
            label4.Name = "label4";
            label4.Size = new Size(94, 20);
            label4.TabIndex = 7;
            label4.Text = "间隔(毫秒)：";
            // 
            // label5
            // 
            label5.AutoSize = true;
            label5.Location = new Point(4, 12);
            label5.Margin = new Padding(4, 0, 4, 0);
            label5.Name = "label5";
            label5.Size = new Size(84, 20);
            label5.TabIndex = 9;
            label5.Text = "方案名称：";
            label5.Click += label5_Click;
            // 
            // Plans
            // 
            Plans.FormattingEnabled = true;
            Plans.Location = new Point(4, 117);
            Plans.Margin = new Padding(4);
            Plans.Name = "Plans";
            Plans.Size = new Size(334, 180);
            Plans.TabIndex = 11;
            Plans.SelectedIndexChanged += checkedListBox1_SelectedIndexChanged;
            // 
            // ModifyHotkeyButton
            // 
            ModifyHotkeyButton.Location = new Point(122, 63);
            ModifyHotkeyButton.Margin = new Padding(4);
            ModifyHotkeyButton.Name = "ModifyHotkeyButton";
            ModifyHotkeyButton.Size = new Size(249, 27);
            ModifyHotkeyButton.TabIndex = 12;
            ModifyHotkeyButton.Text = "修改";
            ModifyHotkeyButton.UseVisualStyleBackColor = true;
            ModifyHotkeyButton.Click += ModifyHotkeyButton_Click;
            // 
            // AdminTest
            // 
            AdminTest.AutoSize = true;
            AdminTest.Location = new Point(24, 10);
            AdminTest.Margin = new Padding(4, 0, 4, 0);
            AdminTest.Name = "AdminTest";
            AdminTest.Size = new Size(0, 20);
            AdminTest.TabIndex = 13;
            // 
            // NameTextBox
            // 
            NameTextBox.Location = new Point(101, 8);
            NameTextBox.Margin = new Padding(3, 4, 3, 4);
            NameTextBox.Name = "NameTextBox";
            NameTextBox.Size = new Size(235, 27);
            NameTextBox.TabIndex = 14;
            NameTextBox.TextChanged += NameTextBox_TextChanged;
            // 
            // PressKeyTextBox
            // 
            PressKeyTextBox.Location = new Point(98, 47);
            PressKeyTextBox.Margin = new Padding(3, 4, 3, 4);
            PressKeyTextBox.Name = "PressKeyTextBox";
            PressKeyTextBox.Size = new Size(238, 27);
            PressKeyTextBox.TabIndex = 15;
            PressKeyTextBox.TextChanged += PressKeyTextBox_TextChanged;
            // 
            // IntervalTextBox
            // 
            IntervalTextBox.Location = new Point(98, 82);
            IntervalTextBox.Margin = new Padding(3, 4, 3, 4);
            IntervalTextBox.Name = "IntervalTextBox";
            IntervalTextBox.Size = new Size(238, 27);
            IntervalTextBox.TabIndex = 16;
            IntervalTextBox.TextChanged += IntervalTextBox_TextChanged;
            // 
            // CreateButton
            // 
            CreateButton.Location = new Point(141, 315);
            CreateButton.Margin = new Padding(3, 4, 3, 4);
            CreateButton.Name = "CreateButton";
            CreateButton.Size = new Size(94, 29);
            CreateButton.TabIndex = 17;
            CreateButton.Text = "新建方案";
            CreateButton.UseVisualStyleBackColor = true;
            CreateButton.Click += CreateButton_Click;
            // 
            // DeleteButton
            // 
            DeleteButton.Location = new Point(240, 315);
            DeleteButton.Margin = new Padding(3, 4, 3, 4);
            DeleteButton.Name = "DeleteButton";
            DeleteButton.Size = new Size(94, 29);
            DeleteButton.TabIndex = 18;
            DeleteButton.Text = "删除方案";
            DeleteButton.UseVisualStyleBackColor = true;
            DeleteButton.Click += DeleteButton_Click;
            // 
            // GitHubButton
            // 
            GitHubButton.Location = new Point(51, 315);
            GitHubButton.Margin = new Padding(4);
            GitHubButton.Name = "GitHubButton";
            GitHubButton.Size = new Size(84, 29);
            GitHubButton.TabIndex = 19;
            GitHubButton.Text = "GitHub";
            GitHubButton.UseVisualStyleBackColor = true;
            GitHubButton.Click += GitHubButton_Click;
            // 
            // playAudioCheckBox
            // 
            playAudioCheckBox.AutoSize = true;
            playAudioCheckBox.Location = new Point(295, 24);
            playAudioCheckBox.Name = "playAudioCheckBox";
            playAudioCheckBox.Size = new Size(76, 24);
            playAudioCheckBox.TabIndex = 20;
            playAudioCheckBox.Text = "提示音";
            playAudioCheckBox.UseVisualStyleBackColor = true;
            playAudioCheckBox.CheckedChanged += playAudioCheckBox_CheckedChanged;
            // 
            // tabControl1
            // 
            tabControl1.Controls.Add(keyPage);
            tabControl1.Controls.Add(scriptPanel);
            tabControl1.Controls.Add(apiPage);
            tabControl1.Location = new Point(24, 97);
            tabControl1.Name = "tabControl1";
            tabControl1.SelectedIndex = 0;
            tabControl1.Size = new Size(353, 394);
            tabControl1.TabIndex = 21;
            // 
            // keyPage
            // 
            keyPage.Controls.Add(label3);
            keyPage.Controls.Add(HelpButton);
            keyPage.Controls.Add(NameTextBox);
            keyPage.Controls.Add(GitHubButton);
            keyPage.Controls.Add(label4);
            keyPage.Controls.Add(DeleteButton);
            keyPage.Controls.Add(label5);
            keyPage.Controls.Add(Plans);
            keyPage.Controls.Add(CreateButton);
            keyPage.Controls.Add(PressKeyTextBox);
            keyPage.Controls.Add(IntervalTextBox);
            keyPage.Location = new Point(4, 29);
            keyPage.Name = "keyPage";
            keyPage.Padding = new Padding(3);
            keyPage.Size = new Size(345, 361);
            keyPage.TabIndex = 0;
            keyPage.Text = "按键";
            keyPage.UseVisualStyleBackColor = true;
            // 
            // scriptPanel
            // 
            scriptPanel.Controls.Add(ScriptList);
            scriptPanel.Location = new Point(4, 29);
            scriptPanel.Name = "scriptPanel";
            scriptPanel.Padding = new Padding(3);
            scriptPanel.Size = new Size(345, 361);
            scriptPanel.TabIndex = 1;
            scriptPanel.Text = "脚本";
            scriptPanel.UseVisualStyleBackColor = true;
            // 
            // ScriptList
            // 
            ScriptList.FormattingEnabled = true;
            ScriptList.Location = new Point(0, 2);
            ScriptList.Name = "ScriptList";
            ScriptList.Size = new Size(345, 356);
            ScriptList.TabIndex = 0;
            ScriptList.SelectedIndexChanged += ScriptList_SelectedIndexChanged;
            // 
            // apiPage
            // 
            apiPage.Controls.Add(label7);
            apiPage.Controls.Add(apiStatueText);
            apiPage.Controls.Add(apiServerButton);
            apiPage.Controls.Add(label6);
            apiPage.Location = new Point(4, 29);
            apiPage.Name = "apiPage";
            apiPage.Size = new Size(345, 361);
            apiPage.TabIndex = 2;
            apiPage.Text = "API";
            apiPage.UseVisualStyleBackColor = true;
            // 
            // label7
            // 
            label7.AutoSize = true;
            label7.Location = new Point(16, 92);
            label7.Name = "label7";
            label7.Size = new Size(129, 20);
            label7.TabIndex = 3;
            label7.Text = "服务端口：15692";
            // 
            // apiStatueText
            // 
            apiStatueText.AutoSize = true;
            apiStatueText.Location = new Point(119, 55);
            apiStatueText.Name = "apiStatueText";
            apiStatueText.Size = new Size(69, 20);
            apiStatueText.TabIndex = 2;
            apiStatueText.Text = "未启动。";
            // 
            // apiServerButton
            // 
            apiServerButton.Location = new Point(13, 52);
            apiServerButton.Name = "apiServerButton";
            apiServerButton.Size = new Size(94, 29);
            apiServerButton.TabIndex = 1;
            apiServerButton.Text = "开启";
            apiServerButton.UseVisualStyleBackColor = true;
            apiServerButton.Click += apiServerButton_Click;
            // 
            // label6
            // 
            label6.AutoSize = true;
            label6.Location = new Point(13, 18);
            label6.Name = "label6";
            label6.Size = new Size(277, 20);
            label6.TabIndex = 0;
            label6.Text = "API 服务用于提供给其他应用程序使用。";
            // 
            // Mykey
            // 
            AutoScaleDimensions = new SizeF(9F, 20F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(397, 497);
            Controls.Add(tabControl1);
            Controls.Add(playAudioCheckBox);
            Controls.Add(AdminTest);
            Controls.Add(ModifyHotkeyButton);
            Controls.Add(label2);
            Controls.Add(StatueLabel);
            Controls.Add(label1);
            FormBorderStyle = FormBorderStyle.FixedSingle;
            KeyPreview = true;
            Margin = new Padding(4);
            MaximizeBox = false;
            Name = "Mykey";
            Text = "自动按键";
            Load += Form1_Load;
            KeyDown += Mykey_KeyDown;
            tabControl1.ResumeLayout(false);
            keyPage.ResumeLayout(false);
            keyPage.PerformLayout();
            scriptPanel.ResumeLayout(false);
            apiPage.ResumeLayout(false);
            apiPage.PerformLayout();
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
        private CheckBox playAudioCheckBox;
        private TabControl tabControl1;
        private TabPage keyPage;
        private TabPage scriptPanel;
        private CheckedListBox ScriptList;
        private TabPage apiPage;
        private Button apiServerButton;
        private Label label6;
        private Label apiStatueText;
        private Label label7;
    }
}