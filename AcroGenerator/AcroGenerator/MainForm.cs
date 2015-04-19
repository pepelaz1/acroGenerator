using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using AcroGenerator.Properties;
using System.IO;

namespace AcroGenerator
{
    public partial class MainForm : Form
    {
        public MainForm()
        {
            InitializeComponent();
        }

        private void btnSelectSource_Click(object sender, EventArgs e)
        {
            ofd.ShowDialog();
            if (String.IsNullOrEmpty(ofd.FileName) == false)
            {
                tbSourceFile.Text = ofd.FileName;
            }
        }

        private void btnSelectOutput_Click(object sender, EventArgs e)
        {
            sfd.ShowDialog();
            if (String.IsNullOrEmpty(sfd.FileName) == false)
            {
                tbOutputFile.Text = sfd.FileName;
            }

        }

        private void btnProcess_Click(object sender, EventArgs e)
        {
            if (String.IsNullOrEmpty(tbSourceFile.Text)
                || String.IsNullOrEmpty(tbOutputFile.Text))
            {
                MessageBox.Show("You should enter source and output files","Warning");
            }
            else
            {
                Process();
            }
        }

        private void Process()
        {
            try
            {
                using (TextReader tr = new StreamReader(tbSourceFile.Text))
                {
                    using (TextWriter tw = new StreamWriter(tbOutputFile.Text))
                    {
                        String srcline;
                        String outline;
                        for(;;)
                        {
                            srcline = tr.ReadLine();
                            if (String.IsNullOrEmpty(srcline) == true)
                                break;
                            outline = "";
                            String []parts = srcline.Split(" ".ToCharArray(), StringSplitOptions.RemoveEmptyEntries);
                            foreach (String part in parts)
                            {
                                outline += part.ToUpper()[0].ToString();
                            }
                            tw.WriteLine(outline);
                        }
                    }

                }
                MessageBox.Show("Completed!");
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Error");
            }
        }

        private void MainForm_Load(object sender, EventArgs e)
        {
            ofd.InitialDirectory = Environment.CurrentDirectory;
            sfd.InitialDirectory = Environment.CurrentDirectory;
            tbSourceFile.Text = Settings.Default.Srcfile;
            tbOutputFile.Text = Settings.Default.Outfile;
        }

        private void MainForm_FormClosing(object sender, FormClosingEventArgs e)
        {
            Settings.Default.Srcfile = tbSourceFile.Text;
            Settings.Default.Outfile = tbOutputFile.Text;
            Settings.Default.Save();
        }
    }
}
