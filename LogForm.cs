using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace WindowsFormsApp2
{
    public partial class LogForm : Form
    {
        public LogForm(string logs)
        {
            
                InitializeComponent();
                
                richTextBox1.Text = logs; 
                richTextBox1.ReadOnly = true; 
           
        }

        private void LogForm_Load(object sender, EventArgs e)
        {

        }

        private void LogForm_FormClosed(object sender, FormClosedEventArgs e)
        {
            Application.Exit();
        }
    }
}
