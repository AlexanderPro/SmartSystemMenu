using System;
using System.Linq;
using System.Windows.Forms;
using SmartSystemMenu.Settings;
using SmartSystemMenu.HotKeys;
using SmartSystemMenu.Extensions;

namespace SmartSystemMenu.Forms
{
    partial class SettingsSizeForm : Form
    {
        public WindowSizeMenuItem MenuItem { get; private set; }

        public SettingsSizeForm(LanguageSettings settings, WindowSizeMenuItem menuItem)
        {
            InitializeComponent();
            InitializeControls(settings, menuItem);
            MenuItem = menuItem;
        }

        private void InitializeControls(LanguageSettings settings, WindowSizeMenuItem menuItem)
        {
            lblTitle.Text = settings.GetValue("lbl_window_size_title");
            lblLeft.Text = settings.GetValue("lbl_window_size_left");
            lblTop.Text = settings.GetValue("lbl_window_size_top");
            lblWidth.Text = settings.GetValue("lbl_window_size_width");
            lblHeight.Text = settings.GetValue("lbl_window_size_height");
            lblKey1.Text = settings.GetValue("lbl_window_size_key1");
            lblKey2.Text = settings.GetValue("lbl_window_size_key2");
            lblKey3.Text = settings.GetValue("lbl_window_size_key3");
            btnApply.Text = settings.GetValue("window_size_btn_apply");
            btnCancel.Text = settings.GetValue("window_size_btn_cancel");
            Text = settings.GetValue("window_size_form");

            txtTitle.Text = menuItem.Title;
            txtLeft.Text = menuItem.Left == null ? "" : menuItem.Left.Value.ToString();
            txtTop.Text = menuItem.Top == null ? "" : menuItem.Top.Value.ToString();
            txtWidth.Text = menuItem.Width == null ? "" : menuItem.Width.ToString();
            txtHeight.Text = menuItem.Height == null ? "" : menuItem.Height.ToString();

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

        protected override void OnLoad(EventArgs e)
        {
            base.OnLoad(e);

            txtTitle.SelectAll();
            txtTitle.Focus();
        }

        private void ButtonApplyClick(object sender, EventArgs e)
        {
            if (string.IsNullOrWhiteSpace(txtTitle.Text))
            {
                txtTitle.SelectAll();
                txtTitle.Focus();
                return;
            }

            var menuItem = new Settings.WindowSizeMenuItem();
            menuItem.Title = txtTitle.Text;
            menuItem.Key1 = (VirtualKeyModifier)cmbKey1.SelectedValue;
            menuItem.Key2 = (VirtualKeyModifier)cmbKey2.SelectedValue;
            menuItem.Key3 = (VirtualKey)cmbKey3.SelectedValue;

            menuItem.Width = int.TryParse(txtWidth.Text, out var width) ? width : null;
            menuItem.Height = int.TryParse(txtHeight.Text, out var height) ? height : null;
            menuItem.Left = int.TryParse(txtLeft.Text, out var left) ? left : null;
            menuItem.Top = int.TryParse(txtTop.Text, out var top) ? top : null;

            MenuItem = menuItem;
            DialogResult = DialogResult.OK;
            Close();
        }

        private void ButtonCancelClick(object sender, EventArgs e)
        {
            DialogResult = DialogResult.Cancel;
            Close();
        }

        private void FormKeyDown(object sender, KeyEventArgs e)
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
