using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using System.Configuration;
using System.IO;

namespace stockassistant
{
    public partial class FrmSetting : Form
    {
        public FrmSetting()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            SaveSetting();
            //Close();
        }

        private void FrmSetting_Load(object sender, EventArgs e)
        {
            LoadSetting();
        }

        private void LoadSetting()
        {
            try
            {
                string file = ConfigurationSettings.AppSettings["path"].ToString();
                if (File.Exists(file))
                {
                    txtPath.Text = file;
                }
                txtlogincaption.Text = ConfigurationSettings.AppSettings["loginname"].ToString();
                txtmaincaption.Text = ConfigurationSettings.AppSettings["mainname"].ToString();
                txtAccount.Text = ConfigurationSettings.AppSettings["account"].ToString();
                string pwd = ConfigurationSettings.AppSettings["password"].ToString();
                if (!string.IsNullOrEmpty(pwd))
                {
                    txtPWD.Text = Utility.Decrypt(pwd);
                }
                decimal i = 5000;
                decimal.TryParse(ConfigurationSettings.AppSettings["overflow"].ToString(), out i);
                upoverflow.Value = i;
                txtWave.Text = ConfigurationSettings.AppSettings["wave"] != null ? ConfigurationSettings.AppSettings["wave"].ToString() : "0.03"; 
            }
            catch(Exception ex)
            {
                MessageBox.Show("the configuration setting is missing!" + " " + ex.Message);
            }
        }

        private void SaveSetting()
        {
            if (txtPath.Text != string.Empty)
            {
                UpdateAppConfig("path", txtPath.Text);
            }
            if (txtlogincaption.Text != string.Empty)
            {
                UpdateAppConfig("loginname", txtlogincaption.Text);
            }
            if (txtmaincaption.Text != string.Empty)
            {
                UpdateAppConfig("mainname", txtmaincaption.Text);
            }
            if (txtAccount.Text != string.Empty)
            {
                UpdateAppConfig("account", txtAccount.Text);
            }
            if (txtPWD.Text != string.Empty)
            {
                UpdateAppConfig("password", Utility.Encrypt(txtPWD.Text));
            }
            UpdateAppConfig("overflow", upoverflow.Value.ToString("f0"));
            if (txtWave.Text != string.Empty)
            {
                UpdateAppConfig("wave", txtWave.Text);
            }
        }

        ///<summary>
        ///在＊.exe.config文件中appSettings配置节增加一对键、值对
        ///</summary>
        ///<param ></param>
        ///<param ></param>
        private static void UpdateAppConfig(string newKey, string newValue)
        {
            bool isModified = false;
            foreach (string key in ConfigurationManager.AppSettings)
            {
                if (key == newKey)
                {
                    isModified = true;
                }
            }
            // Open App.Config of executable
            Configuration config =
                ConfigurationManager.OpenExeConfiguration(ConfigurationUserLevel.None);
            // You need to remove the old settings object before you can replace it
            if (isModified)
            {
                config.AppSettings.Settings.Remove(newKey);
            }
            // Add an Application Setting.
            config.AppSettings.Settings.Add(newKey, newValue);
            // Save the changes in App.config file.
            config.Save(ConfigurationSaveMode.Modified);
            // Force a reload of a changed section.
            ConfigurationManager.RefreshSection("appSettings");            
        }

        private void button3_Click(object sender, EventArgs e)
        {
            if (openFileDialog1.ShowDialog() != DialogResult.No)
            {
                if(File.Exists(openFileDialog1.FileName))
                {
                    txtPath.Text = openFileDialog1.FileName;
                }
            }
        }

        private void txtlogincaption_DoubleClick(object sender, EventArgs e)
        {
            (sender as TextBox).Text = string.Empty;
        }

        private void txtAccount_DoubleClick(object sender, EventArgs e)
        {
            (sender as TextBox).Text = string.Empty;
        }

        private void txtPWD_DoubleClick(object sender, EventArgs e)
        {
            (sender as TextBox).Text = string.Empty;
        }

        private void textBox1_DoubleClick(object sender, EventArgs e)
        {
            (sender as TextBox).Text = string.Empty;
        }
    }
}
