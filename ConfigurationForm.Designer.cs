namespace Sleeper
{
    partial class ConfigurationForm
    {
        private System.ComponentModel.IContainer components = null;

        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region

        private void InitializeComponent()
        {
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(ConfigurationForm));
            this.InformationGroupBox = new System.Windows.Forms.GroupBox();
            this.InformationLabel = new System.Windows.Forms.Label();
            this.ActionAfterRadioButton = new System.Windows.Forms.RadioButton();
            this.ActionAfterComboBox = new System.Windows.Forms.ComboBox();
            this.HoursAfterComboBox = new System.Windows.Forms.ComboBox();
            this.FirstColonLabel = new System.Windows.Forms.Label();
            this.MinutesAfterComboBox = new System.Windows.Forms.ComboBox();
            this.ActionAtRadioButton = new System.Windows.Forms.RadioButton();
            this.ActionAtComboBox = new System.Windows.Forms.ComboBox();
            this.HoursAtComboBox = new System.Windows.Forms.ComboBox();
            this.SecondColonLabel = new System.Windows.Forms.Label();
            this.MinutesAtComboBox = new System.Windows.Forms.ComboBox();
            this.WarnCheckBox = new System.Windows.Forms.CheckBox();
            this.ActionButton = new System.Windows.Forms.Button();
            this.InformationGroupBox.SuspendLayout();
            this.SuspendLayout();
            // 
            // InformationGroupBox
            // 
            this.InformationGroupBox.Controls.Add(this.InformationLabel);
            this.InformationGroupBox.Location = new System.Drawing.Point(12, 12);
            this.InformationGroupBox.Name = "InformationGroupBox";
            this.InformationGroupBox.Size = new System.Drawing.Size(258, 47);
            this.InformationGroupBox.TabIndex = 0;
            this.InformationGroupBox.TabStop = false;
            // 
            // InformationLabel
            // 
            this.InformationLabel.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.InformationLabel.Location = new System.Drawing.Point(6, 16);
            this.InformationLabel.Name = "InformationLabel";
            this.InformationLabel.Size = new System.Drawing.Size(246, 18);
            this.InformationLabel.TabIndex = 1;
            this.InformationLabel.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // ActionAfterRadioButton
            // 
            this.ActionAfterRadioButton.AutoSize = true;
            this.ActionAfterRadioButton.Location = new System.Drawing.Point(22, 68);
            this.ActionAfterRadioButton.Name = "ActionAfterRadioButton";
            this.ActionAfterRadioButton.Size = new System.Drawing.Size(14, 13);
            this.ActionAfterRadioButton.TabIndex = 2;
            this.ActionAfterRadioButton.TabStop = true;
            this.ActionAfterRadioButton.UseVisualStyleBackColor = true;
            // 
            // ActionAfterComboBox
            // 
            this.ActionAfterComboBox.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.ActionAfterComboBox.FormattingEnabled = true;
            this.ActionAfterComboBox.Items.AddRange(new object[] {
            "Shutdown after",
            "Restart after",
            "Log off after",
            "Hibernate after",
            "Stand by after"});
            this.ActionAfterComboBox.Location = new System.Drawing.Point(45, 65);
            this.ActionAfterComboBox.Name = "ActionAfterComboBox";
            this.ActionAfterComboBox.Size = new System.Drawing.Size(121, 21);
            this.ActionAfterComboBox.TabIndex = 3;
            // 
            // HoursAfterComboBox
            // 
            this.HoursAfterComboBox.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.HoursAfterComboBox.FormattingEnabled = true;
            this.HoursAfterComboBox.Location = new System.Drawing.Point(172, 65);
            this.HoursAfterComboBox.Name = "HoursAfterComboBox";
            this.HoursAfterComboBox.Size = new System.Drawing.Size(38, 21);
            this.HoursAfterComboBox.TabIndex = 4;
            // 
            // FirstColonLabel
            // 
            this.FirstColonLabel.AutoSize = true;
            this.FirstColonLabel.Location = new System.Drawing.Point(216, 68);
            this.FirstColonLabel.Name = "FirstColonLabel";
            this.FirstColonLabel.Size = new System.Drawing.Size(10, 13);
            this.FirstColonLabel.TabIndex = 5;
            this.FirstColonLabel.Text = ":";
            // 
            // MinutesAfterComboBox
            // 
            this.MinutesAfterComboBox.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.MinutesAfterComboBox.FormattingEnabled = true;
            this.MinutesAfterComboBox.Location = new System.Drawing.Point(232, 65);
            this.MinutesAfterComboBox.Name = "MinutesAfterComboBox";
            this.MinutesAfterComboBox.Size = new System.Drawing.Size(38, 21);
            this.MinutesAfterComboBox.TabIndex = 6;
            // 
            // ActionAtRadioButton
            // 
            this.ActionAtRadioButton.AutoSize = true;
            this.ActionAtRadioButton.Location = new System.Drawing.Point(22, 95);
            this.ActionAtRadioButton.Name = "ActionAtRadioButton";
            this.ActionAtRadioButton.Size = new System.Drawing.Size(14, 13);
            this.ActionAtRadioButton.TabIndex = 7;
            this.ActionAtRadioButton.TabStop = true;
            this.ActionAtRadioButton.UseVisualStyleBackColor = true;
            // 
            // ActionAtComboBox
            // 
            this.ActionAtComboBox.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.ActionAtComboBox.FormattingEnabled = true;
            this.ActionAtComboBox.Items.AddRange(new object[] {
            "Shutdown at",
            "Restart at",
            "Log off at",
            "Hibernate at",
            "Stand by at"});
            this.ActionAtComboBox.Location = new System.Drawing.Point(45, 92);
            this.ActionAtComboBox.Name = "ActionAtComboBox";
            this.ActionAtComboBox.Size = new System.Drawing.Size(121, 21);
            this.ActionAtComboBox.TabIndex = 8;
            // 
            // HoursAtComboBox
            // 
            this.HoursAtComboBox.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.HoursAtComboBox.FormattingEnabled = true;
            this.HoursAtComboBox.Location = new System.Drawing.Point(172, 92);
            this.HoursAtComboBox.Name = "HoursAtComboBox";
            this.HoursAtComboBox.Size = new System.Drawing.Size(38, 21);
            this.HoursAtComboBox.TabIndex = 9;
            // 
            // SecondColonLabel
            // 
            this.SecondColonLabel.AutoSize = true;
            this.SecondColonLabel.Location = new System.Drawing.Point(216, 95);
            this.SecondColonLabel.Name = "SecondColonLabel";
            this.SecondColonLabel.Size = new System.Drawing.Size(10, 13);
            this.SecondColonLabel.TabIndex = 10;
            this.SecondColonLabel.Text = ":";
            // 
            // MinutesAtComboBox
            // 
            this.MinutesAtComboBox.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.MinutesAtComboBox.FormattingEnabled = true;
            this.MinutesAtComboBox.Location = new System.Drawing.Point(232, 92);
            this.MinutesAtComboBox.Name = "MinutesAtComboBox";
            this.MinutesAtComboBox.Size = new System.Drawing.Size(38, 21);
            this.MinutesAtComboBox.TabIndex = 11;
            // 
            // WarnCheckBox
            // 
            this.WarnCheckBox.AutoSize = true;
            this.WarnCheckBox.Location = new System.Drawing.Point(22, 122);
            this.WarnCheckBox.Name = "WarnCheckBox";
            this.WarnCheckBox.Size = new System.Drawing.Size(160, 17);
            this.WarnCheckBox.TabIndex = 12;
            this.WarnCheckBox.Text = "Warn 1 minute before action";
            this.WarnCheckBox.UseVisualStyleBackColor = true;
            // 
            // ActionButton
            // 
            this.ActionButton.Location = new System.Drawing.Point(196, 118);
            this.ActionButton.Name = "ActionButton";
            this.ActionButton.Size = new System.Drawing.Size(75, 23);
            this.ActionButton.TabIndex = 13;
            this.ActionButton.UseVisualStyleBackColor = true;
            this.ActionButton.Click += new System.EventHandler(this.ClickActionButton);
            // 
            // ConfigurationForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(282, 152);
            this.Controls.Add(this.InformationGroupBox);
            this.Controls.Add(this.ActionAfterRadioButton);
            this.Controls.Add(this.ActionAfterComboBox);
            this.Controls.Add(this.HoursAfterComboBox);
            this.Controls.Add(this.FirstColonLabel);
            this.Controls.Add(this.MinutesAfterComboBox);
            this.Controls.Add(this.ActionAtRadioButton);
            this.Controls.Add(this.ActionAtComboBox);
            this.Controls.Add(this.HoursAtComboBox);
            this.Controls.Add(this.SecondColonLabel);
            this.Controls.Add(this.MinutesAtComboBox);
            this.Controls.Add(this.WarnCheckBox);
            this.Controls.Add(this.ActionButton);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "ConfigurationForm";
            this.Text = "Sleeper - Configuration";
            this.Load += new System.EventHandler(this.LoadConfigurationForm);
            this.InformationGroupBox.ResumeLayout(false);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.GroupBox InformationGroupBox;
        private System.Windows.Forms.Label InformationLabel;
        private System.Windows.Forms.RadioButton ActionAfterRadioButton;
        private System.Windows.Forms.ComboBox ActionAfterComboBox;
        private System.Windows.Forms.ComboBox HoursAfterComboBox;
        private System.Windows.Forms.Label FirstColonLabel;
        private System.Windows.Forms.ComboBox MinutesAfterComboBox;
        private System.Windows.Forms.RadioButton ActionAtRadioButton;
        private System.Windows.Forms.ComboBox ActionAtComboBox;
        private System.Windows.Forms.ComboBox HoursAtComboBox;
        private System.Windows.Forms.Label SecondColonLabel;
        private System.Windows.Forms.ComboBox MinutesAtComboBox;
        private System.Windows.Forms.CheckBox WarnCheckBox;
        private System.Windows.Forms.Button ActionButton;
    }
}