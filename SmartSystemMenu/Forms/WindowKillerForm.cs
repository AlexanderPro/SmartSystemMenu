using System;
using System.Data;
using System.Linq;
using System.Windows.Forms;
using SmartSystemMenu.HotKeys;
using SmartSystemMenu.Extensions;
using SmartSystemMenu.Settings;

namespace SmartSystemMenu.Forms
{
    public partial class WindowKillerForm : Form
    {
        public WindowKillerType WindowKillerType { get; set; }

        public VirtualKeyModifier Key1 { get; private set; }

        public VirtualKeyModifier Key2 { get; private set; }

        public MouseButton MouseButton { get; private set; }

        public WindowKillerForm(LanguageSettings settings, VirtualKeyModifier key1, VirtualKeyModifier key2, MouseButton mouseButton, WindowKillerType windowKillerType)
        {
            InitializeComponent();
            InitializeControls(settings, key1, key2, mouseButton, windowKillerType);
        }

        private void InitializeControls(LanguageSettings settings, VirtualKeyModifier key1, VirtualKeyModifier key2, MouseButton mouseButton, WindowKillerType windowKillerType)
        {
            Text = settings.GetValue("window_killer_form");
            btnApply.Text = settings.GetValue("window_killer_btn_apply");
            btnCancel.Text = settings.GetValue("window_killer_btn_cancel");
            lblKey1.Text = settings.GetValue("window_killer_lbl_key1");
            lblKey2.Text = settings.GetValue("window_killer_lbl_key2");
            lblMouseButton.Text = settings.GetValue("window_killer_lbl_mouse_button");
            lblAction.Text = settings.GetValue("window_killer_lbl_action");

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

            cmbAction.Items.Add(settings.GetValue("window_killer_close_window"));
            cmbAction.Items.Add(settings.GetValue("window_killer_kill_process"));
            cmbAction.SelectedIndex = (int)windowKillerType;
        }

        private void ButtonApplyClick(object sender, EventArgs e)
        {
            Key1 = (VirtualKeyModifier)cmbKey1.SelectedValue;
            Key2 = (VirtualKeyModifier)cmbKey2.SelectedValue;
            MouseButton = (MouseButton)cmMouseButton.SelectedValue;
            WindowKillerType = (WindowKillerType)cmbAction.SelectedIndex;
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
