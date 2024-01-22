using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Google.GData.Client;
using Google.GData.Extensions;
using Google.GData.Spreadsheets;
using Newtonsoft.Json;
using static System.Windows.Forms.VisualStyles.VisualStyleElement;



namespace WorkHellperIRG
{
	public partial class LoginForm : Form
	{

		public string emailIS;
		public string passwordIS;
		string emailGoogle;
		string passwordGoogle;

		public static LoginForm Instance;

		public LoginForm()
		{
			InitializeComponent();
			Instance = this;

			textBox1.Text = Properties.Settings.Default.emailIS;
			textBox2.Text = Properties.Settings.Default.passIS;
			textBox5.Text = Properties.Settings.Default.timer.ToString();
		}


		public void buttonEnterIS(object sender, EventArgs e)
		{
			emailIS = textBox1.Text;
			passwordIS = textBox2.Text;
			if (emailIS == "") { MessageBox.Show("Введите Ваш логин!", "Ошибка", MessageBoxButtons.OK); return; }
			if (passwordIS == "") { MessageBox.Show("Введите Ваш пароль!", "Ошибка", MessageBoxButtons.OK); return; }
			var result = string.Empty;
			try
			{
				var httpWebRequest = (HttpWebRequest)WebRequest.Create("https://help.inventive.ru/api/user?getcurrentuserinfo=true");

				httpWebRequest.Method = "Get";
				httpWebRequest.Headers["Authorization"] = "Basic " + Convert.ToBase64String(Encoding.Default.GetBytes($"{emailIS}:{passwordIS}"));
				httpWebRequest.KeepAlive = false;
				httpWebRequest.Accept = "text/json";
				httpWebRequest.ContentType = "application/json";
				var httpResponse = (HttpWebResponse)httpWebRequest.GetResponse();
				HttpWebResponse resp = httpWebRequest.GetResponse() as HttpWebResponse;
				if ((int)resp.StatusCode == 200) button1.BackColor = Color.Green;
				using (var streamReader = new StreamReader(httpResponse.GetResponseStream()))
				{
					result = streamReader.ReadToEnd();
				}
			}
			catch (WebException ex)
			{
				button1.BackColor = Color.Red;
				result = ex.Message;
			}

			User jsonUser = JsonConvert.DeserializeObject<User>(result);
			Properties.Settings.Default.userName = jsonUser.Name;
			Properties.Settings.Default.userNameId = jsonUser.Id;
			Properties.Settings.Default.emailIS = emailIS;
			Properties.Settings.Default.passIS = passwordIS;
			Properties.Settings.Default.Save();
		}

		public void buttonEnterGoogle(object sender, EventArgs e)
		{
			emailGoogle = textBox3.Text;
			passwordGoogle = textBox4.Text;
			SpreadsheetsService myService = new SpreadsheetsService("https://docs.google.com/spreadsheets/d/1SXNJzlA_YgcgwcUkcOncy0MJT93xa0v-N16DIf1Lmcc");
			myService.setUserCredentials(emailGoogle, passwordGoogle);
		}

		private void buttonSaveSettings(object sender, EventArgs e)
		{
			Properties.Settings.Default.timer = Convert.ToInt32(textBox5.Text);
			Properties.Settings.Default.Save();
			this.Close();
		}
	}
}
