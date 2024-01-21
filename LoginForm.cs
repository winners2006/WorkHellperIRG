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



namespace WorkHellperIRG
{
	public partial class LoginForm : Form
	{

		public string emailIS;
		public string passwordIS;
		string emailGoogle;
		string passwordGoogle;
		public string resultUser;

		public static LoginForm Instance;

		public LoginForm()
		{
			InitializeComponent();
			Instance = this;
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
			resultUser = jsonUser.Name;
		}

		public void buttonEnterGoogle(object sender, EventArgs e)
		{
			emailGoogle = textBox3.Text;
			passwordGoogle = textBox4.Text;
			SpreadsheetsService myService = new SpreadsheetsService("https://docs.google.com/spreadsheets/d/1SXNJzlA_YgcgwcUkcOncy0MJT93xa0v-N16DIf1Lmcc");
			myService.setUserCredentials(emailGoogle, passwordGoogle);
		}


	}
}
