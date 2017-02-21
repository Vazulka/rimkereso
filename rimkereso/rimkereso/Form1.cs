using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.IO;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace rimkereso
{
    public partial class Form1 : Form
    {
        string file = @"E:\oplay\Suli\teszt\magyarszavak5.txt";     //ebből olvas
        List<string> szavak = new List<string>();
        List<Table> osszes = new List<Table>();
        List<int> szamolo = new List<int>();
        List<int> vszveg = new List<int>();
        bool torles = false;
        int szam = 0;
        public class Table : IEquatable<Table>
        {
            public string szo { get; set; }
            public string maganh { get; set; }
            public string kod { get; set; }

            public override string ToString()
            {
                return szo;
            }
            public override bool Equals(object obj)
            {
                if (obj == null) return false;
                Table objAsPart = obj as Table;
                if (objAsPart == null) return false;
                else return Equals(objAsPart);
            }

            public override int GetHashCode()
            {
                return base.GetHashCode();
            }
            public bool Equals(Table other)
            {
                if (other == null) return false;
                return (this.maganh.Equals(other.maganh));
            }
        }
        static bool mgn(char x)
        {
            if (x == 'A' || x == 'Á' || x == 'E' || x == 'É' || x == 'U' || x == 'Ú'
                || x == 'Ü' || x == 'Ű' || x == 'O' || x == 'Ó' || x == 'Ö'
                || x == 'Ő' || x == 'I' || x == 'Í')
            {
                return true;
            }
            return false;


        }
        static string codemake(string code)
        {
            string pr;
            char[] x = new char[code.Length];
            for (int j = 0; j < code.Length; j++)
            {
                x[j] = betu(code[j], code, j);
                
            }
            pr = new string(x);
            pr = pr.Replace("4300", "100");
            pr = pr.Replace("430", "20");
            pr = pr.Replace("4", "1");
            pr = pr.Replace("3", "");
            pr = pr.Replace("0", "");
            return pr;

        }
        static string maganbetu(string magan)
        {
            string listbe;
            int szamolo = 0;
            for (int i = 0; i < magan.Length; i++)
            {
                if (mgn(magan[i])==true)
                {
                    if (szamolo == 2)
                        break;
                    szamolo++;
                }
          

            }
            int l = 0;
            char[] y = new char[szamolo];
            for (int j = magan.Length - 1; j > -1; j--)
            {

                if (mgn(magan[j]) == true)
                {
                    y[l] = magan[j];
                    l++;
                    if (szamolo == l)
                    {
                        break;
                    }

                }


            }
            Array.Reverse(y);
            listbe = new string(y);
            return listbe;
        }
        static string tisztit(string nyers)
        {
            nyers = nyers.Replace("!", "");
            nyers = nyers.Replace("?", "");
            nyers = nyers.Replace(".", "");
            nyers = nyers.Replace(",", "");
            nyers = nyers.Replace("(", "");
            nyers = nyers.Replace(")", "");
            nyers = nyers.Replace("{", "");
            nyers = nyers.Replace("}", "");
            nyers = nyers.Replace("'", "");
            nyers = nyers.Replace(":", "");
            nyers = nyers.Replace(";", "");
            nyers = nyers.Replace(" ", "");
            nyers = nyers.Replace("«", "");
            nyers = nyers.Replace("»", "");
            if (nyers.StartsWith("-"))
                nyers = nyers.Remove(0, 1);


            return nyers;

        }
        static char betu(char x, string szo, int i)
        {
            List<char> zarHang = new List<char>() { 'P', 'T', 'K', 'B', 'D', 'G' };
            List<char> likvida = new List<char>() { 'L', 'R' };
            char y;
            if (x == 'A' || x == 'E' || x == 'U' || x == 'Ü' || x == 'O' || x == 'Ö' || x == 'I')
            {
                if (i < szo.Length - 2)
                {
                    if (mgn(szo[i + 1]) == false && mgn(szo[i + 2]) == false)
                    {

                        if (zarHang.Contains(szo[i + 1]))
                        {
                            if (likvida.Contains(szo[i + 2]))
                            {
                                y = '2';
                                return y;
                            }
                            y = '4';
                            return y;
                        }
                        y = '4';
                        return y;
                    }
                }
                y = '2';
                return y;
            }
            if (x == 'Á' || x == 'É' || x == 'Ú' || x == 'Ű' || x == 'Ó' || x == 'Ő' || x == 'Í')
            {
                y = '1';
                return y;
            }
            if (x == 'S')
            {
                if (i != szo.Length - 1)
                {


                    if (szo[i + 1] == 'Z')
                    {
                        y = '3';
                        return y;

                    }
                    else
                        y = '0';
                }
                y = '0';
                return y;
            }
            if (x == 'Z')
            {
                if (i != szo.Length - 1)
                {
                    if (szo[i + 1] == 'S')
                    {
                        y = '3';
                        return y;

                    }
                }
                y = '0';
                return y;
            }
            if (x == 'N' || x == 'L' || x == 'G' || x == 'T')
            {
                if (i != szo.Length - 1)
                {
                    if (szo[i + 1] == 'Y')
                    {
                        y = '3';
                        return y;

                    }
                }
                y = '0';

                return y;
            }

            if (x == 'C')
            {
                if (i != szo.Length - 1)
                {
                    if (szo[i + 1] == 'S')
                    {
                        y = '3';
                        return y;
                    }
                }
                y = '0';

                return y;
            }
            y = '0';
            return y;


        }
        static Random r = new Random();
        public Form1()
        {
            StreamReader olvaso = new StreamReader(file, Encoding.Default);
            while (!olvaso.EndOfStream)
            {
                szavak.Add(olvaso.ReadLine().ToUpper());
            }
            olvaso.Close();
            for (int i = 0; i < szavak.Count; i++)              //part lista elkészítése
            {
                osszes.Add(new Table() { szo = szavak[i], maganh = maganbetu(szavak[i])
                    , kod =codemake( szavak[i]) });
            }
            InitializeComponent();

        }

        private void Form1_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                button1.PerformClick();
            }
        }
        private void button1_Click(object sender, EventArgs e)
        {
            customRTB2.Clear();
            List<string> rimek = new List<string>();
            List<string> maganTemp = new List<string>();
            List<string> szavaktemp = new List<string>();
            List<string> codetemp = new List<string>();
            List<string> kesz = new List<string>();
            List<int> szoszam = new List<int>();
            List<string> library = new List<string>();
            int coder = 0;
            string line;
            for (int i = 0; i < customRTB1.Lines.Length; i++)
            {
                line = customRTB1.Lines[i];
                string[] kozolo = line.Split(' ');
                for (int j = 0; j < kozolo.Length; j++)
                {
                    int sorSzamolo = 0;
                    kozolo[j] = tisztit(kozolo[j]);
                    if (kozolo[j] != "")
                    {
                        szavaktemp.Add(kozolo[j].ToUpper());
                        codetemp.Add(codemake(kozolo[j].ToUpper()));        //a betöltött szavakat kódolja
                        maganTemp.Add(maganbetu(kozolo[j].ToUpper()));
                        szam++;
                    }
                    else
                    {  
                        vszveg.Add(szam);
                    }
                    if (j == kozolo.Length - 1 && j != 0)
                    {       
                            rimek.Add(kozolo[j]);
                            szamolo.Add(szam);                      
                    }
                    if (radioButton1.Checked)
                    {
                        sorSzamolo += codemake(kozolo[i]).Length;                    //szavak hossza kód
                        szoszam.Add(sorSzamolo);
                        coder += 1;
                    }
                }
                if (radioButton1.Checked)
                {
                    szoszam.Add(500);
                    line = line.Replace(" ", "");                                            //összefűz                                           
                    library.Add(codemake(line));
                }
            }
            for (int i = 0; i < szavaktemp.Count; i++)                          //szavak kiválasztása 
            {                                                                   //táblából

                List<string> temp = new List<string>();
                for (int j = 0; j < osszes.Count; j++)
                {
                    if (szamolo.Contains(i+1))
                    {
                        if (osszes[j].kod == (codetemp[i]) && osszes[j].maganh == (maganTemp[i]))
                        {
                            temp.Add(osszes[j].ToString());
                        }
                    }
                    else
                    {
                        if (osszes[j].kod == (codetemp[i]))
                        {
                            temp.Add(osszes[j].ToString());
                        }
                    }

                }
                kesz.Add(temp[r.Next(temp.Count)]);
            }

            for (int i = 0; i < kesz.Count; i++)
            {
                if (szamolo.Contains(i))
                {
                    if (vszveg.Contains(i))
                    {
                        customRTB2.Text = customRTB2.Text + "\n";
                    }
                    customRTB2.Text = customRTB2.Text + "\n";
                }

                customRTB2.Text = customRTB2.Text + " " + kesz[i];
            }
            for (int i = 0; i < vszveg.Count; i++)
            {
                customRTB2.Text = customRTB2.Text + vszveg[i];
            }

            //if (radioButton1.Checked)
            //{
            //    coder += 1;
            //}
            //if (radioButton2.Checked)
            //{
            //    coder += 2;
            //}
            //if (radioButton3.Checked)
            //{
            //    coder += 10;
            //}
            //if (radioButton4.Checked)
            //{
            //    coder += 20;
            //}
            //if (radioButton5.Checked)
            //{
            //    coder += 30;
            //}

            //switch (coder)
            //{


            //}


        }

        private void text_click(object sender, MouseEventArgs e)
        {
            if (torles == false)
            {
                customRTB1.Clear();
                torles = true;
            }

        }

        private void button2_Click(object sender, EventArgs e)
        {
            customRTB2.Clear();
            szamolo.Clear();
            szam = 0;
            
        }
    }

}
              
