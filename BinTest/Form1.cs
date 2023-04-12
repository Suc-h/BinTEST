using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace BinTest
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }
        FileStream data = new FileStream("seznam.dat", FileMode.OpenOrCreate, FileAccess.ReadWrite);
        int klik = 0;
        private void button1_Click(object sender, EventArgs e)
        {
            int pocet = (int)numericUpDown3.Value-klik;
            
            BinaryWriter Writer = new BinaryWriter(data, Encoding.GetEncoding("windows-1250"));

            if(klik==0)
            {
                Writer.Write((int)numericUpDown3.Value);

                listBox1.Items.Add((int)numericUpDown3.Value);
            }

            if (pocet > 0)
            {
                Writer.Write((int)numericUpDown2.Value);
                Writer.Write((int)numericUpDown1.Value);

                listBox1.Items.Add((int)numericUpDown2.Value);
                listBox1.Items.Add((int)numericUpDown1.Value);
                
            }

            if(pocet==0)
            {
               // button1.Enabled = false;
                Writer.Write(textBox1.Text);
               

                listBox1.Items.Add(textBox1.Text);
                klik = -1;
            }

          
            klik++;
           
        }

    
       
        private void button3_Click(object sender, EventArgs e)
        {
            int pocet = (int)numericUpDown3.Value;
            BinaryReader Reader = new BinaryReader(data, Encoding.GetEncoding("windows-1250"));
            
                Reader.BaseStream.Seek(+sizeof(Int32), SeekOrigin.Begin);
            
            
           
            for(int i=0;i<pocet;i++)
            {
                listBox2.Items.Add(Reader.ReadInt32()+"       "+Reader.ReadInt32());
            }
            string text = Reader.ReadString();
            textBox3.Text = text;

        }

        private void button2_Click(object sender, EventArgs e)
        {
            BinaryReader Reader = new BinaryReader(data, Encoding.GetEncoding("windows-1250"));
            double pocet = (int)numericUpDown3.Value;
            double znamky = 0;
            Reader.BaseStream.Seek(+sizeof(Int32), SeekOrigin.Begin);
            for (int i = 0; i <= pocet; i++)
            {
                Reader.BaseStream.Seek(+sizeof(Int32), SeekOrigin.Begin);
                znamky = znamky+Reader.ReadInt32();

            }
            double prumer = pocet / znamky;
            MessageBox.Show("Průměr žáka je:" + Convert.ToDouble(znamky));
        }
    }
}
