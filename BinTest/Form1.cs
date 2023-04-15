namespace BinTest
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            button2.Enabled = false;
            File.WriteAllText("seznam.dat", string.Empty);
        }
        List <int> vaha = new List<int>();
        List<int> znamka = new List<int>();
        
        private void button1_Click(object sender, EventArgs e)
        {
            vaha.Add(Convert.ToInt32(numericUpDown1.Value));
            znamka.Add(Convert.ToInt32(numericUpDown2.Value));

            label6.Text += "\n" + Convert.ToInt32(numericUpDown1.Value) +"       "+ Convert.ToInt32(numericUpDown2.Value);


            if(textBox1.Text!=""&& textBox2.Text!="")
            {
            button2.Enabled = true;
            }
        }

        private void button2_Click(object sender, EventArgs e)
        {
            FileStream fileStream = new FileStream("seznam.dat", FileMode.OpenOrCreate, FileAccess.ReadWrite);
            BinaryWriter Writer = new BinaryWriter(fileStream);
            try
            {
                Writer.BaseStream.Position = Writer.BaseStream.Length; // posledn� z�pis
                Writer.Write(vaha.Count);
                for(int i = 0; i < vaha.Count; i++) // znamka.count
                {
                    Writer.Write(vaha[i]);
                    Writer.Write(znamka[i]);
                }
                Writer.Write(textBox1.Text);
                Writer.Write(textBox2.Text);
                znamka.Clear();
                vaha.Clear();
                textBox1.ResetText();
                textBox2.ResetText();
                Writer.Close();
                fileStream.Close();
                label6.Text = "";
                button2.Enabled=false;

            }
            catch
            {
                MessageBox.Show("Zkontroluj �e m� zadan� v�echy �daje");
                Writer.Close();
                fileStream.Close();
            }
        }
        List<string> Jmeno = new List<string>();
        private void button3_Click(object sender, EventArgs e)
        {
            textBox3.Text = "";
            listBox1.Items.Clear();
            listBox2.Items.Clear();
            FileStream fileStream = new FileStream("seznam.dat", FileMode.OpenOrCreate, FileAccess.Read);
            BinaryReader Reader = new BinaryReader(fileStream);
            Reader.BaseStream.Position = 0;
            int znamkek = 0;
            int vahik = 0;
            int pocetznamek = 0;
            double soucet = 0;
            double prumer = 0;
            bool ano = false;
            while (Reader.BaseStream.Position < Reader.BaseStream.Length)
            {
                ano = false;
                pocetznamek = Reader.ReadInt32();
                listBox1.Items.Add(" ");
                listBox1.Items.Add("Po�et zn�mek: "+pocetznamek);
                listBox2.Items.Add(" ");
                listBox2.Items.Add("Po�et zn�mek: " + pocetznamek);
                for (int i = 0; i < pocetznamek; i++)
                {
                    vahik = Reader.ReadInt32();
                   znamkek = Reader.ReadInt32();
                    soucet = soucet + (znamkek * vahik);
                    listBox1.Items.Add("v�ha: "+vahik);
                    listBox1.Items.Add("Zn�mka: "+znamkek);
                    listBox2.Items.Add("v�ha: " + vahik);
                    listBox2.Items.Add("Zn�mka: " + znamkek);

                }
                string jmeno = Reader.ReadString();
                string prijmeni = Reader.ReadString();

                
                prumer = soucet / Convert.ToDouble(pocetznamek);
                listBox2.Items.Add("Pr�m�r je: " + prumer);
                if (Math.Round(prumer)==4)
                {
                    MessageBox.Show("Pozor ��kovi: " + jmeno + " " + prijmeni + " vych�z� 4!");
                }
                else
                {
                    if(Math.Round(prumer) >= 5)
                    {
                        ano = true;
                       
                        MessageBox.Show("Pozor ��kovi: " + jmeno + " " + prijmeni + " vych�z� 5!");
                    }
                    else
                    {
                        if(Math.Round(prumer) <= 1)
                        {

                            MessageBox.Show("��kovi: " + jmeno + " " + prijmeni + " vych�z� 1!");
                        }
                    }
                }
                if (ano == true)
                {
                    textBox4.AppendText("John" + " " + "Doe" + "\r\n");
                }
                else
                {
                    textBox4.AppendText(jmeno + " " + prijmeni + "\r\n");
                }
                textBox3.AppendText(jmeno + " " + prijmeni + "\r\n");
                soucet = 0;
            }
            Reader.Close();
            fileStream.Close();
        }
    }
}