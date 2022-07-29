using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Windows.Forms;
namespace GTK
{
    public partial class GTKForm : Form
    {
        public GTKForm()
        {
            InitializeComponent();

            for (int i = 0; i < tabControl1.TabPages.Count; i++)
            {
                for (int i2 = 0; i2 < tabControl1.TabPages[i].Controls.Count; i2++)
                {
                    if (tabControl1.TabPages[i].Controls[i2] is Button)
                    {
                        Button settingButton = (Button)tabControl1.TabPages[i].Controls[i2];

                        settingButton.Click += SettingButton_Click;
                    }
                    if (tabControl1.TabPages[i].Controls[i2] is NumericUpDown)
                    {
                        NumericUpDown SettingCustomNumber = (NumericUpDown)tabControl1.TabPages[i].Controls[i2];

                        SettingCustomNumber.ValueChanged += SettingCustomNumber_ValueChanged; 
                    }
                    if (tabControl1.TabPages[i].Controls[i2] is CheckBox)
                    {
                        CheckBox SettingCustomBool = (CheckBox)tabControl1.TabPages[i].Controls[i2];

                        SettingCustomBool.CheckedChanged += SettingCustomBool_CheckedChanged; ;
                    }
                }
            }
        }

        private void SettingCustomBool_CheckedChanged(object sender, EventArgs e)
        {
            CheckBox thisButton = (CheckBox)sender;
            string tag = (string)thisButton.Tag;
            string text = thisButton.Checked.ToString();
            string configPath_ = configPath.Text;

            string configFile = File.ReadAllText(configPath_, Encoding.Default);

            string dynamic_pattern = @"<Low XYZ='\w+'".Replace("'", $"\"").Replace("XYZ", tag);

            string finalConfig = Regex.Replace(configFile, dynamic_pattern, dynamic_pattern.Replace(@"\w+", text.ToLower()));

            this.Text = dynamic_pattern + $"({text.ToLower()})";
        }

        private void SettingCustomNumber_ValueChanged(object sender, EventArgs e)
        {
            NumericUpDown thisButton = (NumericUpDown)sender;
            string tag = (string)thisButton.Tag;
            string text = thisButton.Value.ToString();
            string configPath_ = configPath.Text;

            string configFile = File.ReadAllText(configPath_, Encoding.Default);

            string dynamic_pattern = @"<Low XYZ='\d+'".Replace("'", $"\"").Replace("XYZ", tag);

            string finalConfig = Regex.Replace(configFile, dynamic_pattern, dynamic_pattern.Replace(@"\d+", text));

            this.Text = dynamic_pattern + $"({text})";
        }

        private void SettingButton_Click(object sender, EventArgs e)
        {
            Button thisButton = (Button)sender;
            string tag = (string)thisButton.Tag;
            string text = thisButton.Text;
            string configPath_ = configPath.Text;

            string configFile = File.ReadAllText(configPath_, Encoding.Default);

            string dynamic_pattern = @"<Low XYZ='\d+'".Replace("'", $"\"").Replace("XYZ", tag);

            string finalConfig = Regex.Replace(configFile, dynamic_pattern, dynamic_pattern.Replace(@"\d+", text));

            this.Text = dynamic_pattern + $"({text})";
        }

        private void RunBtn_Click(object sender, EventArgs e)
        {
            Process.Start(exePath.Text);
        }

        private void findExe(object sender, EventArgs e)
        {
            OpenFileDialog openFileDialog1 = new OpenFileDialog
            {
                InitialDirectory =Environment.GetFolderPath(Environment.SpecialFolder.Desktop),
                Title = "Browse .exe Files",

                CheckFileExists = true,
                CheckPathExists = true,

                FilterIndex = 2,
                RestoreDirectory = true,

                ReadOnlyChecked = true,
                ShowReadOnly = true
            };

            if (openFileDialog1.ShowDialog() == DialogResult.OK)
            {
                exePath.Text = openFileDialog1.FileName;
            }
        }
        //:))
    }
}
