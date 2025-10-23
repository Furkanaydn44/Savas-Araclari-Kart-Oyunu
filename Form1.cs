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
    

    public partial class Form1 : Form
    {
        bool Button1bool = false;
        bool Button2bool = false;
        int maxtur = 5;
        int kartpuan = 0;
        string name;

        public Form1()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {

            bool Valid = false;
            

            while (!Valid)
            {
                string a = textBox1.Text;

                if (string.IsNullOrEmpty(a))
                {
                    MessageBox.Show("Giriş iptal edildi.");
                    return; 
                }

                if (int.TryParse(textBox1.Text, out int result))
                {
                    kartpuan = int.Parse(textBox1.Text);
                    if (kartpuan < 0)
                    {
                        MessageBox.Show("Hatalı giriş. Pozitif Sayı giriniz ! ");
                        textBox1.Clear();
                        return;
                    }
                    else
                    {
                        Valid = true;
                        MessageBox.Show("Giriş kaydedildi !");
                        Button1bool = true;
                    }
                }
                else
                {
                    MessageBox.Show("Hatalı giriş. Sayı giriniz ! ");
                    textBox1.Clear();
                    return;
                }

            }
        }

        private void button2_Click(object sender, EventArgs e)
        {
            bool Valid = false;
            

            while (!Valid)
            {
                string a = textBox2.Text;

                if (string.IsNullOrEmpty(a))
                {
                    MessageBox.Show("Giriş iptal edildi.");
                    return;
                }

                if (int.TryParse(textBox2.Text, out int result))
                {
                    maxtur = int.Parse(textBox2.Text);

                    if (maxtur < 1)
                    {
                        MessageBox.Show("Hatalı giriş. \"0\" dan büyük bir sayı giriniz ! ");
                        textBox2.Clear();
                        return;
                    }
                    else 
                    { 
                    Valid = true;
                    MessageBox.Show("Giriş kaydedildi !");
                    Button2bool = true;
                    }
                }
                else
                {
                    MessageBox.Show("Hatalı giriş. Sayı giriniz ! ");
                    textBox2.Clear();
                    return;
                }

            }
        }

        private void label1_Click(object sender, EventArgs e)
        {

        }

        private void textBox1_TextChanged(object sender, EventArgs e)
        {

        }

        private void label2_Click(object sender, EventArgs e)
        {

        }

        private void textBox2_TextChanged(object sender, EventArgs e)
        {

        }

        private void label3_Click(object sender, EventArgs e)
        {

        }

        private void button3_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(textBox3.Text))
            {
                MessageBox.Show("Kullanıcı adını girin !!");
                return;
            }
            else
            {
                name = textBox3.Text;

                if (Button1bool == true && Button2bool == true)
                {

                    Form form2 = new Form2(kartpuan, maxtur,name);
                    form2.Show();
                    this.Hide();

                }
                else if (Button1bool == false && Button2bool == false)
                    MessageBox.Show("Kartpuanını ve Maksimum tur sayısını giriniz !");

                else if (Button2bool == false)
                    MessageBox.Show("Maksimum tur sayısını giriniz !");
                else
                {
                    MessageBox.Show("Kartpuanını giriniz !");

                }
            }
        }

        private void label5_Click(object sender, EventArgs e)
        {

        }
    }
}
