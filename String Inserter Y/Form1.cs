using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.IO;


namespace String_Inserter_Y
{
    public partial class StringInserterY : Form
    {
        string dir;
        string input;
        string textInsert;
        public StringInserterY()
        {
            InitializeComponent();
            dir = "";
            input = "";
        }

        private void chooseFile_Click(object sender, EventArgs e)
        {
            openFileDialog1.Filter = "Text file | *.txt";
            if (openFileDialog1.ShowDialog() == DialogResult.OK)
            {
                dir = openFileDialog1.FileName;
                textBoxFile.Text = dir;
                input = File.ReadAllText(dir);
            }
        }

        private void buttonExecute_click(object sender, EventArgs e)
        { //Execute AKA beginning of run
            input = File.ReadAllText(dir);
            textInsert = textBoxInsert.Text;
            textInsert = textInsert.Replace(@"\n", "\n");
            textInsert = textInsert.Replace(@"\r", "\r");
            textInsert = textInsert.Replace(@"\t", "\t");
            buttonExecute.Enabled = false;
            int index = 0;
            while(input.Substring(index).Contains(textBoxStart.Text))
            {
                index = input.Substring(index).IndexOf(textBoxStart.Text) + textBoxStart.Text.Length + index;
                index += textBoxStart.Text.Length;
                string inputPastStart = input.Substring(index);
                int insertAt = inputPastStart.IndexOf(textBoxEnd.Text) + index;
                input = input.Insert(insertAt, textInsert);
                index += textInsert.Length;
            }
            //Console.WriteLine("starting write to file...");
            string writeToDir;
            if (checkBoxOverride.Checked)
            {
                writeToDir = dir;
            }
            else
                writeToDir = dir.Substring(0, dir.LastIndexOf("\\") + 1) + "output.txt";
            File.Delete(writeToDir);
            File.AppendAllText(writeToDir, input);
            MessageBox.Show("Completed.");
            buttonExecute.Enabled = true;
        }

    }
}
