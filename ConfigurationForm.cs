using System;
using System.Windows.Forms;

namespace Sleeper
{
    public partial class ConfigurationForm : Form
    {
        private Configuration FormConfiguration;
        // Delegates
        public delegate void SwitchTimer(bool Start = true);
        public event SwitchTimer ChangeAction;

        public ConfigurationForm(ref Configuration Config)
        {
            FormConfiguration = Config;
            InitializeComponent();
        }

        private void LoadConfigurationForm(object sender, EventArgs e)
        {
            UpdateInformationLabel();

            if (FormConfiguration.IsEnabled)
            {
                ActionButton.Text = "Reset";
                ActionAfterRadioButton.Enabled = ActionAfterComboBox.Enabled = HoursAfterComboBox.Enabled = MinutesAfterComboBox.Enabled = ActionAtRadioButton.Enabled = ActionAtComboBox.Enabled = HoursAtComboBox.Enabled = MinutesAtComboBox.Enabled = WarnCheckBox.Enabled = false;
            }
            else
            {
                ActionButton.Text = "Set";
            }

            if (FormConfiguration.SelectedOperation == Configuration.SelectionOperation.After)
            {
                ActionAfterRadioButton.Checked = true;
            }
            else
            {
                ActionAtRadioButton.Checked = true;
            }

            ActionAfterComboBox.SelectedIndex = (int)FormConfiguration.AfterOperation;
            ActionAtComboBox.SelectedIndex = (int)FormConfiguration.AtOperation;

            for (byte h = 0; h < 24; ++h)
            {
                HoursAfterComboBox.Items.Add(String.Format("{0:D2}", h));
                HoursAtComboBox.Items.Add(String.Format("{0:D2}", h));
            }

            for (byte h = 0; h < 60; ++h)
            {
                MinutesAfterComboBox.Items.Add(String.Format("{0:D2}", h));
                MinutesAtComboBox.Items.Add(String.Format("{0:D2}", h));
            }

            TimeSpan TimeSpan = TimeSpan.FromMinutes(FormConfiguration.TimeAfterOperation);
            HoursAfterComboBox.Text = TimeSpan.ToString("hh");
            MinutesAfterComboBox.Text = TimeSpan.ToString("mm");
            TimeSpan = TimeSpan.FromMinutes(FormConfiguration.TimeAtOperation);
            HoursAtComboBox.Text = TimeSpan.ToString("hh");
            MinutesAtComboBox.Text = TimeSpan.ToString("mm");

            if (FormConfiguration.IsWarnEnabled)
            {
                WarnCheckBox.Checked = true;
            }

            ActionAfterRadioButton.CheckedChanged  += ConfigurationChanged;
            ActionAfterComboBox.SelectionChangeCommitted += ConfigurationChanged;
            HoursAfterComboBox.SelectionChangeCommitted += ConfigurationChanged;
            MinutesAfterComboBox.SelectionChangeCommitted += ConfigurationChanged;
            ActionAtRadioButton.CheckedChanged += ConfigurationChanged;
            ActionAtComboBox.SelectionChangeCommitted += ConfigurationChanged;
            HoursAtComboBox.SelectionChangeCommitted += ConfigurationChanged;
            MinutesAtComboBox.SelectionChangeCommitted += ConfigurationChanged;
            WarnCheckBox.CheckedChanged += ConfigurationChanged;
        }

        private void ConfigurationChanged(object sender, EventArgs e)
        {
            // Lazy solution
            FormConfiguration.SelectedOperation = (ActionAfterRadioButton.Checked ? Configuration.SelectionOperation.After : Configuration.SelectionOperation.At);
            FormConfiguration.AfterOperation = (Configuration.OperationTypes)ActionAfterComboBox.SelectedIndex;
            FormConfiguration.TimeAfterOperation = (ushort)TimeSpan.Parse(HoursAfterComboBox.GetItemText(HoursAfterComboBox.SelectedItem) + ":" + MinutesAfterComboBox.GetItemText(MinutesAfterComboBox.SelectedItem)).TotalMinutes;
            FormConfiguration.AtOperation = (Configuration.OperationTypes)ActionAtComboBox.SelectedIndex;
            FormConfiguration.TimeAtOperation = (ushort)TimeSpan.Parse(HoursAtComboBox.GetItemText(HoursAtComboBox.SelectedItem) + ":" + MinutesAtComboBox.GetItemText(MinutesAtComboBox.SelectedItem)).TotalMinutes;
            FormConfiguration.IsWarnEnabled = WarnCheckBox.Checked;
        }

        private void UpdateInformationLabel()
        {
            if (FormConfiguration.IsEnabled)
            {
                string Label;

                if (FormConfiguration.SelectedOperation == Configuration.SelectionOperation.After)
                {
                    Label = Enum.GetName(typeof(Configuration.OperationTypes), FormConfiguration.AfterOperation) + " after " + TimeSpan.FromMinutes(FormConfiguration.TimeAfterOperation).ToString(@"hh\:mm");
                }
                else
                {
                    Label = Enum.GetName(typeof(Configuration.OperationTypes), FormConfiguration.AtOperation) + " at " + TimeSpan.FromMinutes(FormConfiguration.TimeAtOperation).ToString(@"hh\:mm");
                }

                InformationLabel.Text = Label.Replace('_', ' ') + " - warning " + (FormConfiguration.IsWarnEnabled ? "enabled" : "disabled");
            }
            else
            {
                InformationLabel.Text = "Currently no action scheduled";
            }
        }

        private void ClickActionButton(object sender, EventArgs e)
        {
            FormConfiguration.IsEnabled = !FormConfiguration.IsEnabled;
            UpdateInformationLabel();

            if (FormConfiguration.IsEnabled)
            {
                ChangeAction();
                ActionAfterRadioButton.Enabled = ActionAfterComboBox.Enabled = HoursAfterComboBox.Enabled = MinutesAfterComboBox.Enabled = ActionAtRadioButton.Enabled = ActionAtComboBox.Enabled = HoursAtComboBox.Enabled = MinutesAtComboBox.Enabled = WarnCheckBox.Enabled = false;
                ActionButton.Text = "Reset";
            }
            else
            {
                ChangeAction(false);
                ActionAfterRadioButton.Enabled = ActionAfterComboBox.Enabled = HoursAfterComboBox.Enabled = MinutesAfterComboBox.Enabled = ActionAtRadioButton.Enabled = ActionAtComboBox.Enabled = HoursAtComboBox.Enabled = MinutesAtComboBox.Enabled = WarnCheckBox.Enabled = true;
                ActionButton.Text = "Set";
            }
        }
    }
}