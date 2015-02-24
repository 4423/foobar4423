using System;
using System.Windows.Forms;

namespace foobar4423
{
    public partial class Form_Help : Form
    {
        public Form_Help()
        {
            InitializeComponent();
        }

        private void Form_Help_Load(object sender, EventArgs e)
        {
            textBox1.ReadOnly = true;
            textBox1.SelectionStart = 0;
        }

    }
}
