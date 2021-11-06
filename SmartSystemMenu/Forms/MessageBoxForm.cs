using System.Windows.Forms;

namespace SmartSystemMenu.Forms
{
    public partial class MessageBoxForm : Form
    {
        public string Message 
        { 
            get
            {
                return txtMessage.Text;
            }
            set
            {
                txtMessage.Text = value;
            }
        }

        public MessageBoxForm()
        {
            InitializeComponent();
        }

        private void OkClick(object sender, System.EventArgs e)
        {
            Close();
        }

        private void FormKeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyValue == 13 || e.KeyValue == 27)
            {
                Close();
            }
        }
    }
}
