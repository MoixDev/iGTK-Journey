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

            string dynamic_pattern = @"XYZ='\w+'".Replace("'", $"\"").Replace("XYZ", tag);

            string finalConfig = Regex.Replace(configFile, dynamic_pattern, dynamic_pattern.
                Replace(@"\w+", text.ToLower()));

            write_config(configPath_, finalConfig);

            this.Text = dynamic_pattern + $"({text.ToLower()})";
        }

        private void SettingCustomNumber_ValueChanged(object sender, EventArgs e)
        {
            NumericUpDown thisButton = (NumericUpDown)sender;
            string tag = (string)thisButton.Tag;
            string text = thisButton.Value.ToString();
            string configPath_ = configPath.Text;

            string configFile = File.ReadAllText(configPath_, Encoding.Default);

            string dynamic_pattern = @"XYZ='\d+'".Replace("'", $"\"").Replace("XYZ", tag);

            string finalConfig = Regex
                .Replace(configFile, dynamic_pattern, dynamic_pattern
                .Replace(@"\d+", text));

            write_config(configPath_, finalConfig);

            this.Text = dynamic_pattern + $"({text})";
        }

        private void SettingButton_Click(object sender, EventArgs e)
        {
            Button thisButton = (Button)sender;
            string tag = (string)thisButton.Tag;
            string text = thisButton.Text;
            string configPath_ = configPath.Text;
            try
            {
                string configFile = File.ReadAllText(configPath_, Encoding.Default);
                string dynamic_pattern = @"XYZ='\d+'".Replace("'", $"\"").Replace("XYZ", tag);

                string finalConfig = Regex.Replace(configFile, dynamic_pattern, dynamic_pattern
                    .Replace(@"\d+", text)
                    .Replace(".*", ""));

                write_config(configPath_, finalConfig);

                this.Text = dynamic_pattern + $"({text})";
            }
            catch (Exception)
            {
                error_message();
            }
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
                Title = "Select game main .exe File",

                CheckFileExists = true,
                CheckPathExists = true,

                FilterIndex = 2,
                RestoreDirectory = true,

                ReadOnlyChecked = true,
            };

            if (openFileDialog1.ShowDialog() == DialogResult.OK)
            {
                exePath.Text = openFileDialog1.FileName;
            }
        }

        void write_config(string configPath_, string finalConfig)
        {
            try
            {
                StreamWriter sw = new StreamWriter(configPath_);
                sw.Write("");
                sw.Write(finalConfig);
                sw.Close();
            }
            catch (Exception)
            {
                error_message();
            }
        }

        void error_message()
        {
            MessageBox.Show("Firt open the [Jouney.cfg] file hosted on \n\n...\n\n To modify the game graphics"
                .Replace("...",

                @"C:\Users\[[YOUR USER NAME]]]\AppData\Local\Annapurna Interactive\Journey\Steam\Jouney.cfg"
            ));
        }

        private void OpenConfigBtn_Click(object sender, EventArgs e)
        {
            OpenFileDialog openFileDialog1 = new OpenFileDialog
            {
                InitialDirectory =Environment.GetFolderPath(Environment.SpecialFolder.UserProfile),
                Title = @"Browse to C:\Users\  YOUR USER NAME  \AppData\Local\Annapurna Interactive\Journey\Steam\Jouney.cfg",

                CheckFileExists = true,
                CheckPathExists = true,

                FilterIndex = 2,
                RestoreDirectory = true,

                ReadOnlyChecked = true,
                ShowReadOnly = false
            };

            if (openFileDialog1.ShowDialog() == DialogResult.OK)
            {
                configPath.Text = openFileDialog1.FileName;
            }
        }

        private void OpenGithubPage(object sender, LinkLabelLinkClickedEventArgs e)
        {
            Process.Start("https://moissesa.blogspot.com/2022/07/igtk-integrated-graphics-toolkit.html");
        }
    }
}
