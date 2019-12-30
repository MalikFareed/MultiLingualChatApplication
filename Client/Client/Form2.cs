using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Client
{
    public partial class Form2 : Form
    {
        public Form2()
        {
            InitializeComponent();
            CheckForIllegalCrossThreadCalls = false;
        }

        private void Form2_Load(object sender, EventArgs e)
        {
            comboBox1.Items.Add("en");
            comboBox1.Items.Add("ur");
            comboBox1.Items.Add("hi");
            comboBox1.Items.Add("ru");
        }

        private void button1_Click(object sender, EventArgs e)
        {
            try
            {
                Form1 f1 = new Form1(comboBox1.SelectedItem.ToString());
                f1.Main();
                this.Hide();
                f1.Show();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }
      
    }
}
