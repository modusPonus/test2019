using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Collections;

namespace TestTask
{
    public partial class Form1 : Form
    {
        private char[] massOfChar = {'а','б', 'в', 'г', 'д', 'е', 'ё', 'ж', 'з', 'и', 'й', 'к', 'л', 'м', 'н', 'о', 'п', 'р', 'с', 'т', 'у', 'ф', 'х', 'ц', 'ч', 'ш', 'щ', 'ъ', 'ы', 'ь', 'э', 'ю', 'я', };
        private Hashtable hashtableText = new Hashtable();
        private Hashtable hashtableFinValue = new Hashtable();

        public Form1()
        {
            InitializeComponent();
        }

        private void Form1_Load(object sender, EventArgs e)
        {
        }

        private void button1_Click(object sender, EventArgs e)
        {
            hashtableText = GetHashtableWithNumOfText(richTextBox1.Text);
            hashtableFinValue = FreqCalculation(hashtableText, GetNumOfText(richTextBox1.Text));
            int shift = FindShift(hashtableFinValue);
            richTextBox2.Text = BuildText(shift);

            hashtableText.Clear();
            hashtableFinValue.Clear();
        }

        private Hashtable GetHashtableWithNumOfText(String text) 
        {
            Hashtable hashtable = new Hashtable();
            text = text.ToLower();
            for (int i = 0; i < text.Length; i++)
            {
                if (char.IsLetter(text[i]))
                {
                    if (hashtable.ContainsKey(text[i]))
                    {
                        hashtable[text[i]] = (int)hashtable[text[i]] + 1;
                    }
                    else
                    {
                        hashtable.Add(text[i], 1);
                    }
                }
            }
            return hashtable;
        }

        private int GetNumOfText(String text)
        {
            String textFin = "";
            text = text.ToLower();
            for (int i = 0; i < text.Length; i++)
            {
                if (char.IsLetter(text[i]))
                {
                    textFin = textFin + text[i];
                }
            }
            return textFin.Length;
        }

        private Hashtable FreqCalculation(Hashtable hashtable, int textNum)
        {
            Hashtable hashtableValue = new Hashtable();
            ICollection keys = hashtable.Keys;
            foreach (char Text in keys)
            {
                double num = (int)hashtable[Text];
                double freq = Math.Round((num / textNum * 100), 2);
                if (hashtableValue.ContainsKey(Text))
                {
                    hashtableValue[Text] = Math.Round(freq, 2);
                }
                else
                {
                    hashtableValue.Add(Text, Math.Round(freq, 2));
                }
            }
            return hashtableValue;
        }

        private int FindShift(Hashtable hashtable)
        {
            ICollection keys = hashtable.Keys;
            double dif = Double.MinValue;
            String finch = "";
            foreach (char text in keys)
            {
                textBox1.Text = textBox1.Text + (hashtable[text]);
                double count = Convert.ToDouble(textBox1.Text);
                textBox1.Text = "";
                if (count > dif)
                {
                    dif = count;
                    textBox1.Text = textBox1.Text + text;
                    finch = textBox1.Text;
                    textBox1.Text = "";
                }
            }
            char[] shiftChar = finch.ToCharArray();
            int shift = Array.IndexOf(massOfChar, 'о') - Array.IndexOf(massOfChar, shiftChar[0]);

            return shift;
        }

        private String BuildText(int shift)
        {
            String textFin = "";
            String text = richTextBox1.Text;
            int ind;
            for (int i = 0; i < text.Length; i++)
            {
                if (char.IsLetter(text[i]))
                {
                    if (char.IsUpper(text[i]) == true)
                    {
                        if (shift > 0) { ind = Array.IndexOf(massOfChar, char.ToLower(text[i])) - shift; }
                        else { ind = Array.IndexOf(massOfChar, char.ToLower(text[i])) + shift; }
                        if (ind < 0)
                        {
                            textFin = textFin + massOfChar[massOfChar.Length + ind];
                        }
                        else { textFin = textFin + char.ToUpper(massOfChar[ind]); }
                    }
                    else
                    {
                        if (shift > 0) { ind = Array.IndexOf(massOfChar, text[i]) - shift; }
                        else { ind = Array.IndexOf(massOfChar, text[i]) + shift; }
                        if (ind < 0)
                        {
                            textFin = textFin + massOfChar[massOfChar.Length + ind];
                        }
                        else { textFin = textFin + massOfChar[ind]; }
                    }
                }
                else { textFin = textFin + text[i]; }
            }
            return textFin;
        }

        private void clearToolStripMenuItem_Click(object sender, EventArgs e)
        {
            richTextBox1.Text = "";
            richTextBox2.Text = "";
        }

        private void richTextBox1_TextChanged(object sender, EventArgs e)
        {
            if (richTextBox1.Text.Length < 1000)
            {
                button1.Enabled = false;
            } else { button1.Enabled = true; }
        }
    }
}
