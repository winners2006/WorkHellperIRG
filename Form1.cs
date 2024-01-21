using Google.GData.Client;
using Google.GData.Extensions;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Management.Instrumentation;
using System.Net;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace WorkHellperIRG
{
	public partial class Form1 : Form
	{
		public static Form1 Instance;
		//List<string> ISListTasks = new List<string>();
		

		public Form1()
		{
			InitializeComponent();
			Instance = this;
		}

		private void buttonEnter(object sender, EventArgs e)
		{
			LoginForm dlg = new LoginForm();
			dlg.Show(this);
		}

		public void buttonUpdate(object sender, EventArgs e)
		{
			string emailIS = "Shchedlovskiy@inventive.ru"; // LoginForm.Instance.emailIS;
			string passwordIS = "Nw9e5E3DySm"; // LoginForm.Instance.passwordIS;
			string urlISTasks = "https://help.inventive.ru/api/task?fields=Id,Name,StatusId";

			TasksToTable(ConnectAndPushUrl(emailIS, passwordIS, urlISTasks));
		}

		public class Task
		{
			public int Id { get; set; }
			public string Name { get; set; }
		}
		public string ConnectAndPushUrl(string email, string pass, string url)
		{
			var result = string.Empty;
			try
			{
				var httpWebRequest = (HttpWebRequest)WebRequest.Create(url);

				httpWebRequest.Method = "Get";
				httpWebRequest.Headers["Authorization"] = "Basic " + Convert.ToBase64String(Encoding.Default.GetBytes($"{email}:{pass}"));
				httpWebRequest.KeepAlive = false;
				httpWebRequest.Accept = "text/json";
				httpWebRequest.ContentType = "application/json";
				var httpResponse = (HttpWebResponse)httpWebRequest.GetResponse();
				HttpWebResponse resp = httpWebRequest.GetResponse() as HttpWebResponse;
				if ((int)resp.StatusCode == 200) button2.BackColor = Color.Green;
				using (var streamReader = new StreamReader(httpResponse.GetResponseStream()))
				{
					result = streamReader.ReadToEnd();
				}
			}
			catch (WebException ex)
			{
				button2.BackColor = Color.Red;
				result = ex.Message;
			}
			return result;
		}
		public void TasksToTable(string jsonTasks)
		{
			JObject tasksIS = JObject.Parse(jsonTasks);

			IList<JToken> tasks = tasksIS["Tasks"].Children().ToList();
			IList<Task> task = new List<Task>();
			foreach (JToken taskToken in tasks)
			{
				Task task1 = JsonConvert.DeserializeObject<Task>(taskToken.ToString());
				task.Add(task1);
			}
			foreach (Task item in task)
			{
				ListViewItem item2 = new ListViewItem(new string[] { item.Id.ToString(), item.Name.ToString() });
				listView1.Items.Add(item2);
			}
		}
	}
}
