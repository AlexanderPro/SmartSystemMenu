using System;
using System.Data;
using System.Linq;
using System.Windows.Forms;
using SmartSystemMenu.HotKeys;
using SmartSystemMenu.Extensions;
using SmartSystemMenu.Settings;

namespace SmartSystemMenu.Forms
{
    public partial class SettingsCloserForm : Form
    {
        public WindowCloserType CloserType { get; set; }

        public VirtualKeyModifier Key1 { get; private set; }

        public VirtualKeyModifier Key2 { get; private set; }

        public MouseButton MouseButton { get; private set; }

        public SettingsCloserForm(LanguageSettings settings, VirtualKeyModifier key1, VirtualKeyModifier key2, MouseButton mouseButton, WindowCloserType closerType)
        {
            InitializeComponent();
            InitializeControls(settings, key1, key2, mouseButton, closerType);
        }

        private void InitializeControls(LanguageSettings settings, VirtualKeyModifier key1, VirtualKeyModifier key2, MouseButton mouseButton, WindowCloserType closerType)
        {
            Text = settings.GetValue("closer_form");
            btnApply.Text = settings.GetValue("closer_btn_apply");
            btnCancel.Text = settings.GetValue("closer_btn_cancel");
            lblKey1.Text = settings.GetValue("closer_lbl_key1");
            lblKey2.Text = settings.GetValue("closer_lbl_key2");
            lblMouseButton.Text = settings.GetValue("closer_lbl_mouse_button");
            lblAction.Text = settings.GetValue("closer_lbl_action");

            cmbKey1.ValueMember = "Id";
            cmbKey1.DisplayMember = "Text";
            cmbKey1.DataSource = ((VirtualKeyModifier[])Enum.GetValues(typeof(VirtualKeyModifier))).Where(x => !string.IsNullOrEmpty(x.GetDescription())).Select(x => new { Id = x, Text = x.GetDescription() }).ToList();
            cmbKey1.SelectedValue = key1;

            cmbKey2.ValueMember = "Id";
            cmbKey2.DisplayMember = "Text";
            cmbKey2.DataSource = ((VirtualKeyModifier[])Enum.GetValues(typeof(VirtualKeyModifier))).Where(x => !string.IsNullOrEmpty(x.GetDescription())).Select(x => new { Id = x, Text = x.GetDescription() }).ToList();
            cmbKey2.SelectedValue = key2;

            cmMouseButton.ValueMember = "Id";
            cmMouseButton.DisplayMember = "Text";
            cmMouseButton.DataSource = ((MouseButton[])Enum.GetValues(typeof(MouseButton))).Where(x => !string.IsNullOrEmpty(x.GetDescription())).Select(x => new { Id = x, Text = x.GetDescription() }).ToList();
            cmMouseButton.SelectedValue = mouseButton;

            cmbAction.Items.Add(settings.GetValue("closer_close_foreground_window"));
            cmbAction.Items.Add(settings.GetValue("closer_close_window_under_cursor"));
            cmbAction.Items.Add(settings.GetValue("closer_kill_process_with_foreground_window"));
            cmbAction.Items.Add(settings.GetValue("closer_kill_process_with_window_under_cursor"));
            cmbAction.SelectedIndex = (int)closerType;
        }

        private void ButtonApplyClick(object sender, EventArgs e)
        {
            Key1 = (VirtualKeyModifier)cmbKey1.SelectedValue;
            Key2 = (VirtualKeyModifier)cmbKey2.SelectedValue;
            MouseButton = (MouseButton)cmMouseButton.SelectedValue;
            CloserType = (WindowCloserType)cmbAction.SelectedIndex;
            DialogResult = DialogResult.OK;
            Close();
        }

        private void ButtonCancelClick(object sender, EventArgs e)
        {
            Close();
        }

        private void KeyDownClick(object sender, KeyEventArgs e)
        {
            if (e.KeyValue == 13)
            {
                ButtonApplyClick(sender, e);
            }

            if (e.KeyValue == 27)
            {
                Close();
            }
        }
    }
}
