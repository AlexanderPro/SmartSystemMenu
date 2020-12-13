using System;
using System.Data;
using System.Linq;
using System.Windows.Forms;
using SmartSystemMenu.HotKeys;
using SmartSystemMenu.Extensions;
using SmartSystemMenu.Settings;

namespace SmartSystemMenu.Forms
{
    public partial class HotkeysForm : Form
    {
        public Settings.MenuItem MenuItem { get; set; }

        public HotkeysForm(SmartSystemMenuSettings settings, Settings.MenuItem menuItem)
        {
            InitializeComponent();
            MenuItem = menuItem;
            InitializeControls(settings, menuItem);
        }

        private void InitializeControls(SmartSystemMenuSettings settings, Settings.MenuItem menuItem)
        {
            Text = settings.LanguageSettings.GetValue("hotkeys_form");
            btnApply.Text = settings.LanguageSettings.GetValue("hotkeys_btn_apply");
            btnCancel.Text = settings.LanguageSettings.GetValue("hotkeys_btn_cancel");
            lblKey1.Text = settings.LanguageSettings.GetValue("hotkeys_lbl_key1");
            lblKey2.Text = settings.LanguageSettings.GetValue("hotkeys_lbl_key2");
            lblKey3.Text = settings.LanguageSettings.GetValue("hotkeys_lbl_key3");

            cmbKey1.ValueMember = "Id";
            cmbKey1.DisplayMember = "Text";
            cmbKey1.DataSource = ((VirtualKeyModifier[])Enum.GetValues(typeof(VirtualKeyModifier))).Where(x => !string.IsNullOrEmpty(x.GetDescription())).Select(x => new { Id = x, Text = x.GetDescription() }).ToList();
            cmbKey1.SelectedValue = menuItem.Key1;

            cmbKey2.ValueMember = "Id";
            cmbKey2.DisplayMember = "Text";
            cmbKey2.DataSource = ((VirtualKeyModifier[])Enum.GetValues(typeof(VirtualKeyModifier))).Where(x => !string.IsNullOrEmpty(x.GetDescription())).Select(x => new { Id = x, Text = x.GetDescription() }).ToList();
            cmbKey2.SelectedValue = menuItem.Key2;

            cmbKey3.ValueMember = "Id";
            cmbKey3.DisplayMember = "Text";
            cmbKey3.DataSource = ((VirtualKey[])Enum.GetValues(typeof(VirtualKey))).Where(x => !string.IsNullOrEmpty(x.GetDescription())).Select(x => new { Id = x, Text = x.GetDescription() }).ToList();
            cmbKey3.SelectedValue = menuItem.Key3;
        }

        private void ButtonApplyClick(object sender, EventArgs e)
        {
            var menuItem = new Settings.MenuItem();
            menuItem.Key1 = (VirtualKeyModifier)cmbKey1.SelectedValue;
            menuItem.Key2 = (VirtualKeyModifier)cmbKey2.SelectedValue;
            menuItem.Key3 = (VirtualKey)cmbKey3.SelectedValue;
            menuItem.Name = MenuItem.Name;
            MenuItem = menuItem;
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
