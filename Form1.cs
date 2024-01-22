using GDataDB.Impl;
using Google.GData.Client;
using Google.GData.Extensions;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Management.Instrumentation;
using System.Net;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using static System.Net.WebRequestMethods;
using static System.Windows.Forms.VisualStyles.VisualStyleElement;

namespace WorkHellperIRG
{
	public partial class Form1 : Form
	{

		string emailIS = Properties.Settings.Default.emailIS;
		string passwordIS = Properties.Settings.Default.passIS;
		string idUser = Properties.Settings.Default.userNameId;
		string urlISTasks;
		int tasksType;
		int timerNum;



		public static Form1 Instance;

		public Form1()
		{
			InitializeComponent();
			Instance = this;

			Timer timer = new Timer();
			timer.Interval = (timerNum * 1000); 
			timer.Tick += new EventHandler(timer_Tick);
			timer.Start();

			label15.Text = Properties.Settings.Default.userName;
		}

		
			
		

		private void timer_Tick(object sender, EventArgs e)
		{
			UpdateTasks();
		}

		private void UpdateTasks()
		{
			listView1.Items.Clear();
			if (tasksType == 1)
			{
				urlISTasks = $"https://help.inventive.ru/api/task?fields=Id,Name,Deadline&ExecutorIds={idUser}&StatusIDs=31,95";
			}
			else if (tasksType == 0)
			{
				urlISTasks = $"https://help.inventive.ru/api/task?fields=Id,Name,Deadline&filterid=3211&StatusIDs=31,95";
			}
			else if (tasksType == 2)
			{
				urlISTasks = $"https://help.inventive.ru/api/task?fields=Id,Name,Deadline&filterid=3213&StatusIDs=31,95";
			}
			timerNum = Properties.Settings.Default.timer;
			TasksToTable(ConnectAndPushUrl(emailIS, passwordIS, urlISTasks));
			DataEndTasks();
			CountTasksWeek();
			CountTasksDay();
		}

		private void buttonEnter(object sender, EventArgs e)
		{
			LoginForm dlg = new LoginForm();
			dlg.Show(this);
		}

		public void buttonUpdate(object sender, EventArgs e)
		{
			UpdateTasks();
		}

		private void buttonMyTasks(object sender, EventArgs e)
		{
			tasksType = 1;
			UpdateTasks();
		}

		private void buttonNewTasks(object sender, EventArgs e)
		{
			tasksType = 0;
			UpdateTasks();
		}

		private void button1Line(object sender, EventArgs e)
		{
			tasksType = 2;
			UpdateTasks();
		}

		private void listView_DoubleClick(object sender, EventArgs e)
		{
			try
			{
				if (listView1.SelectedItems.Count == 0)
					return;
				ListViewItem item = listView1.SelectedItems[0];
				string temp = item.ToString().Remove(0,15);
				string temp2 = temp.TrimEnd(new char[] { '}' });
				System.Diagnostics.Process.Start($"https://help.inventive.ru/Task/View/{temp2}");

			}
			catch (Exception ex)
			{
				MessageBox.Show(ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
			}
		}

		public void CountTasksWeek()
		{
			DateTime dt = DateTime.Now;
			DateTime startOfWeek = dt.AddDays((((int)(dt.DayOfWeek) + 6) % 7) * -1);
			DateTime dtstart = DateTime.Parse(startOfWeek.ToString());
			string startW = dtstart.ToString("yyyy-MM-dd", CultureInfo.InvariantCulture);
			string urlCountTasks = $"https://help.inventive.ru/api/task?fields=Id&ResolutionDateFactMoreThan={startW}&ExecutorIds=5273&PageSize=300";

			label2.Text = $"Выполненые: {CountTasksWeekJson(ConnectAndPushUrl(emailIS, passwordIS, urlCountTasks))}";
		}

		public void CountTasksDay()
		{
			DateTime dt = DateTime.Now;
			string dayNow = dt.ToString("yyyy-MM-dd", CultureInfo.InvariantCulture);
			string urlCountTasks = $"https://help.inventive.ru/api/task?fields=Id&ResolutionDateFactMoreThan={dayNow}&ExecutorIds=5273&PageSize=100";

			label7.Text = $"Выполненые: { CountTasksDayJson(ConnectAndPushUrl(emailIS, passwordIS, urlCountTasks))}";
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
			int coutnTasks = 0;

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
				coutnTasks++;
				if (item.Deadline != null && item.Deadline.ToString() != "")
				{
					string temp = item.Deadline.ToString();
					string dateEnd = temp.Replace("T", " ");
					ListViewItem item2 = new ListViewItem(new string[] { item.Id.ToString(), item.Name, dateEnd });
					listView1.Items.Add(item2);
				}
				else
				{
					ListViewItem item2 = new ListViewItem(new string[] { item.Id.ToString(), item.Name, item.Deadline });
					listView1.Items.Add(item2);
				}
			}
			label14.Text = $"Задач в работе: {coutnTasks}";
		}

		public int CountTasksWeekJson(string jsonTasks)
		{
			int countWeek = 0;

			JObject tasksIS = JObject.Parse(jsonTasks);

			IList<JToken> tasks = tasksIS["Tasks"].Children().ToList();
			IList<CountTask> task = new List<CountTask>();
			foreach (JToken taskToken in tasks)
			{
				CountTask task1 = JsonConvert.DeserializeObject<CountTask>(taskToken.ToString());
				task.Add(task1);
			}
			foreach (CountTask item in task)
			{
					countWeek++;
			}
			return countWeek;
			
		}

		public int CountTasksDayJson(string jsonTasks)
		{
			int countDay = 0;

			JObject tasksIS = JObject.Parse(jsonTasks);

			IList<JToken> tasks = tasksIS["Tasks"].Children().ToList();
			IList<CountTask> task = new List<CountTask>();
			foreach (JToken taskToken in tasks)
			{
				CountTask task1 = JsonConvert.DeserializeObject<CountTask>(taskToken.ToString());
				task.Add(task1);
			}
			foreach (CountTask item in task)
			{
				countDay++;
			}
			return countDay;

		}

		public void DataEndTasks()
		{
			foreach (ListViewItem item in listView1.Items)
			{
				if (item.SubItems[2] != null && item.SubItems[2].Text != "")
				{
					string tempDateTime = item.SubItems[2].Text;
					DateTime dateTimeEnd = DateTime.Parse(tempDateTime);
					if (dateTimeEnd.Subtract(DateTime.Now).TotalMinutes < 30.0)
					{
						item.BackColor = Color.Red;
					}
					else if (dateTimeEnd.Subtract(DateTime.Now).TotalMinutes < 1440)
					{
						item.BackColor = Color.Orange;
					}
				}
			}
		}

		public class Task
		{
			public int Id { get; set; }
			public string Name { get; set; }
			public string Deadline { get; set; }
		}

		public class CountTask
		{
			public int Id { get; set; }
			public string ResolutionDateFact { get; set; }
		}

	}
}
