﻿using System;
using System.Drawing;
using System.IO;
using System.Net;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Google.Apis.Sheets.v4.Data;
using Newtonsoft.Json;
using static System.Net.Mime.MediaTypeNames;
using Color = System.Drawing.Color;



namespace WorkHellperIRG
{
	public partial class LoginForm : Form
	{


		string emailIS = Properties.Settings.Default.emailIS;
		string passwordIS = Properties.Settings.Default.passIS;

		public static LoginForm Instance;

		public LoginForm()
		{
			InitializeComponent();
			Instance = this;

			textBox1.Text = Properties.Settings.Default.emailIS;
			textBox2.Text = Properties.Settings.Default.passIS;
			textBox5.Text = Properties.Settings.Default.timer.ToString();
			
		}
		public static HttpClient сlient = new HttpClient()
		{

			BaseAddress = new Uri("https://help.inventive.ru/api/"),

		};
		
		
		public async void buttonEnterIS(object sender, EventArgs e)
		{
			emailIS = textBox1.Text;
			passwordIS = textBox2.Text;
			if (emailIS == "") { MessageBox.Show("Введите Ваш логин!", "Ошибка", MessageBoxButtons.OK); return; }
			if (passwordIS == "") { MessageBox.Show("Введите Ваш пароль!", "Ошибка", MessageBoxButtons.OK); return; }
			string urlISUser = "user?getcurrentuserinfo=true";
			var result = string.Empty;
			сlient.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
			var hesh = Convert.ToBase64String(Encoding.Default.GetBytes($"{emailIS}:{passwordIS}"));

			using (HttpRequestMessage request = new HttpRequestMessage(HttpMethod.Get, urlISUser))
			{
				сlient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Basic", hesh);
				var response = await сlient.GetAsync(urlISUser);
				response.EnsureSuccessStatusCode();
				if ((int)response.StatusCode == 200) button1.BackColor = Color.Green;
				else button1.BackColor = Color.Red;
				var tempresult = await response.Content.ReadAsStringAsync();
				User jsonUser = JsonConvert.DeserializeObject<User>(tempresult);
				Properties.Settings.Default.userName = jsonUser.Name;
				Properties.Settings.Default.userNameId = jsonUser.Id;
				Properties.Settings.Default.emailIS = emailIS;
				Properties.Settings.Default.passIS = passwordIS;
				Properties.Settings.Default.Save();
			}
		}

		public void buttonEnterGoogle(object sender, EventArgs e)
		{
			
		}

		private void buttonSaveSettings(object sender, EventArgs e)
		{
			Properties.Settings.Default.timer = Convert.ToInt32(textBox5.Text);
			Properties.Settings.Default.Save();
			
			this.Close();
		}

		private void buttonResetSettings(object sender, EventArgs e)
		{
			Properties.Settings.Default.Reset();
			Properties.Settings.Default.Save();
		}
	}
}
