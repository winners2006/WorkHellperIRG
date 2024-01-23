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
using static System.Net.Mime.MediaTypeNames;
using static System.Net.WebRequestMethods;
using static System.Windows.Forms.VisualStyles.VisualStyleElement;

namespace WorkHellperIRG
{
	public partial class Form1 : Form
	{

		string emailIS = Properties.Settings.Default.emailIS;
		string passwordIS = Properties.Settings.Default.passIS;
		string idUser = Properties.Settings.Default.userNameId;
		string urlISTasksMyTasks;
		string urlISTasksNewTasks;
		string urlISTasksLineTasks;
		int tasksType;
		int timerNum = 30;
		int coutnTasksMyTasks = 0;
		int coutnTasksNewTasks = 0;
		int coutnTasksLineTasks = 0;



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

			listView1.Show();
			listView2.Hide();
			listView3.Hide();
		}

		
			
		

		private void timer_Tick(object sender, EventArgs e)
		{
			UpdateTasks();
		}

		private void UpdateTasks()
		{
			listView1.Items.Clear();
			listView2.Items.Clear();
			listView3.Items.Clear();

			TasksToTableMyTasks(ConnectAndPushUrlMyTasks(emailIS, passwordIS));
			TasksToTableNewTasks(ConnectAndPushUrlNewTasks(emailIS, passwordIS));
			TasksToTableLineTasks(ConnectAndPushUrlLineTasks(emailIS, passwordIS));

			MessengeTasks(ConnectAndPushUrlNewTasks(emailIS, passwordIS));

			timerNum = Properties.Settings.Default.timer;
			DataEndTasksMyTasks();
			DataEndTasksNewTasks();
			DataEndTasksLineTasks();
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
			listView1.Show();
			listView2.Hide();
			listView3.Hide();
			label14.Text = $"Задач в работе: {coutnTasksMyTasks}";
		}

		private void buttonNewTasks(object sender, EventArgs e)
		{
			tasksType = 0;
			listView1.Hide();
			listView2.Show();
			listView3.Hide();
			label14.Text = $"Задач в работе: {coutnTasksNewTasks}";
		}

		private void button1Line(object sender, EventArgs e)
		{
			tasksType = 2;
			listView1.Hide();
			listView2.Hide();
			listView3.Show();
			label14.Text = $"Задач в работе: {coutnTasksLineTasks}";
		}

		private void listView_DoubleClick(object sender, EventArgs e)
		{
			try
			{
				if(tasksType == 0) 
				{
					if (listView1.SelectedItems.Count == 0)
						return;
					ListViewItem item = listView1.SelectedItems[0];
					string temp = item.ToString().Remove(0, 15);
					string temp2 = temp.TrimEnd(new char[] { '}' });
					System.Diagnostics.Process.Start($"https://help.inventive.ru/Task/View/{temp2}");
				}

				else if (tasksType == 1) {
					if (tasksType == 0)
					{
						if (listView2.SelectedItems.Count == 0)
							return;
						ListViewItem item = listView2.SelectedItems[0];
						string temp = item.ToString().Remove(0, 15);
						string temp2 = temp.TrimEnd(new char[] { '}' });
						System.Diagnostics.Process.Start($"https://help.inventive.ru/Task/View/{temp2}");
					}
				}

				else if (tasksType == 2)
				{
					if (tasksType == 0)
					{
						if (listView3.SelectedItems.Count == 0)
							return;
						ListViewItem item = listView3.SelectedItems[0];
						string temp = item.ToString().Remove(0, 15);
						string temp2 = temp.TrimEnd(new char[] { '}' });
						System.Diagnostics.Process.Start($"https://help.inventive.ru/Task/View/{temp2}");
					}
				}
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

		public string ConnectAndPushUrlMyTasks(string email, string pass)
		{
			urlISTasksMyTasks = $"https://help.inventive.ru/api/task?fields=Id,Name,Deadline&ExecutorIds={idUser}&StatusIDs=31,95";

			var result = string.Empty;
			try
			{
				var httpWebRequest = (HttpWebRequest)WebRequest.Create(urlISTasksMyTasks);

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
		public string ConnectAndPushUrlNewTasks(string email, string pass)
		{
			urlISTasksNewTasks = $"https://help.inventive.ru/api/task?fields=Id,Name,Deadline,PriorityId,Created&filterid=3211&StatusIDs=31,95";

			var result = string.Empty;
			try
			{
				var httpWebRequest = (HttpWebRequest)WebRequest.Create(urlISTasksNewTasks);

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
		public string ConnectAndPushUrlLineTasks(string email, string pass)
		{
			urlISTasksLineTasks = $"https://help.inventive.ru/api/task?fields=Id,Name,Deadline&filterid=3213&StatusIDs=31,95";

			var result = string.Empty;
			try
			{
				var httpWebRequest = (HttpWebRequest)WebRequest.Create(urlISTasksLineTasks);

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
		public string ConnectAndPushUrl(string email, string pass,string url)
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

		public void TasksToTableMyTasks(string jsonTasks)
		{
			coutnTasksMyTasks = 0;
			JObject tasksIS = JObject.Parse(jsonTasks);

			IList<JToken> tasks = tasksIS["Tasks"].Children().ToList();
			IList<MyTask> task = new List<MyTask>();
			foreach (JToken taskToken in tasks)
			{
				MyTask task1 = JsonConvert.DeserializeObject<MyTask>(taskToken.ToString());
				task.Add(task1);
			}
			foreach (MyTask item in task)
			{
				coutnTasksMyTasks++;
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
		}

		public void TasksToTableNewTasks(string jsonTasks)
		{
			coutnTasksNewTasks = 0;
			JObject tasksIS = JObject.Parse(jsonTasks);

			IList<JToken> tasks = tasksIS["Tasks"].Children().ToList();
			IList<NewTask> task = new List<NewTask>();
			foreach (JToken taskToken in tasks)
			{
				NewTask task1 = JsonConvert.DeserializeObject<NewTask>(taskToken.ToString());
				task.Add(task1);
			}
			foreach (NewTask item in task)
			{
				coutnTasksNewTasks++;
				if (item.Deadline != null && item.Deadline.ToString() != "")
				{
					string temp = item.Deadline.ToString();
					string dateEnd = temp.Replace("T", " ");
					ListViewItem item2 = new ListViewItem(new string[] { item.Id.ToString(), item.Name, dateEnd });
					listView2.Items.Add(item2);
				}
				else
				{
					ListViewItem item2 = new ListViewItem(new string[] { item.Id.ToString(), item.Name, item.Deadline });
					listView2.Items.Add(item2);
				}
			}
		}

		public void TasksToTableLineTasks(string jsonTasks)
		{
			coutnTasksLineTasks = 0;
			JObject tasksIS = JObject.Parse(jsonTasks);

			IList<JToken> tasks = tasksIS["Tasks"].Children().ToList();
			IList<LineTask> task = new List<LineTask>();
			foreach (JToken taskToken in tasks)
			{
				LineTask task1 = JsonConvert.DeserializeObject<LineTask>(taskToken.ToString());
				task.Add(task1);
			}
			foreach (LineTask item in task)
			{
				coutnTasksLineTasks++;
				if (item.Deadline != null && item.Deadline.ToString() != "")
				{
					string temp = item.Deadline.ToString();
					string dateEnd = temp.Replace("T", " ");
					ListViewItem item2 = new ListViewItem(new string[] { item.Id.ToString(), item.Name, dateEnd });
					listView3.Items.Add(item2);
				}
				else
				{
					ListViewItem item2 = new ListViewItem(new string[] { item.Id.ToString(), item.Name, item.Deadline });
					listView3.Items.Add(item2);
				}
			}
		}

		public void MessengeTasks(string jsonTasks)
		{
			JObject tasksIS = JObject.Parse(jsonTasks);

			IList<JToken> tasks = tasksIS["Tasks"].Children().ToList();
			IList<NewTask> task = new List<NewTask>();
			foreach (JToken taskToken in tasks)
			{
				NewTask task1 = JsonConvert.DeserializeObject<NewTask>(taskToken.ToString());
				task.Add(task1);
			}

			foreach (NewTask i in task)
			{
				MessageBoxButtons buttons = MessageBoxButtons.YesNo;
				MessageBoxIcon iconMess = MessageBoxIcon.Warning;
				DialogResult resultClicMessege;
				DateTime dtNow = DateTime.Now;
				string NameMess = "Новая заявка";
				string tempDateStart = i.Created;
				DateTime dateTimeStart = DateTime.Parse(tempDateStart.ToString());
				if (dtNow.Subtract(dateTimeStart).TotalMinutes < 2 & Convert.ToInt32(i.PriorityId) == 12)
				{
					string message = $"Приоритет КРИТИЧНЫЙ заявка № {i.Id}. Необходимо взять в работу в течении 15 минут. Выполнить в течении 4 часов! Открыть заявку?";
					resultClicMessege = MessageBox.Show(message, NameMess, buttons, iconMess);
					if (resultClicMessege == DialogResult.Yes) System.Diagnostics.Process.Start($"https://help.inventive.ru/Task/View/{i.Id}");
				}
				else if (dtNow.Subtract(dateTimeStart).TotalMinutes < 2 & Convert.ToInt32(i.PriorityId) == 10)
				{
					string message = $"Приоритет ВЫСОКИЙ заявка № {i.Id}. Необходимо взять в работу в течении 40 минут. Выполнить в течении 8 часов! Открыть заявку?";
					resultClicMessege = MessageBox.Show(message, NameMess, buttons, iconMess);
					if (resultClicMessege == DialogResult.Yes) System.Diagnostics.Process.Start($"https://help.inventive.ru/Task/View/{i.Id}");
				}

				else if (dtNow.Subtract(dateTimeStart).TotalMinutes < 2 & Convert.ToInt32(i.PriorityId) == 9)
				{
					string message = $"Приоритет СРЕДНИЙ заявка № {i.Id}. Необходимо взять в работу в течении 4 часов. Выполнить в течении 48 часов! Открыть заявку?";
					resultClicMessege = MessageBox.Show(message, NameMess, buttons, iconMess);
					if (resultClicMessege == DialogResult.Yes) System.Diagnostics.Process.Start($"https://help.inventive.ru/Task/View/{i.Id}");
				}
				else if (dtNow.Subtract(dateTimeStart).TotalMinutes < 2 & Convert.ToInt32(i.PriorityId) == 11)
				{
					string message = $"Приоритет НИЗКИЙ заявка № {i.Id}. Необходимо взять в работу в течении 16 часов. Выполнить в течении 80 часов! Открыть заявку?";
					resultClicMessege = MessageBox.Show(message, NameMess, buttons, iconMess);
					if (resultClicMessege == DialogResult.Yes) System.Diagnostics.Process.Start($"https://help.inventive.ru/Task/View/{i.Id}");
				}
				else if (dtNow.Subtract(dateTimeStart).TotalMinutes > 10 & dtNow.Subtract(dateTimeStart).TotalMinutes < 12 & Convert.ToInt32(i.PriorityId) == 12)
				{
					string message = $"Приоритет КРИТИЧНЫЙ заявка № {i.Id}. Необходимо срочно взять в работу! Открыть заявку?";
					resultClicMessege = MessageBox.Show(message, NameMess, buttons, iconMess);
					if (resultClicMessege == DialogResult.Yes) System.Diagnostics.Process.Start($"https://help.inventive.ru/Task/View/{i.Id}");
				}
			}
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

		public void DataEndTasksMyTasks()
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

		public void DataEndTasksNewTasks()
		{
			foreach (ListViewItem item in listView2.Items)
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

		public void DataEndTasksLineTasks()
		{
			foreach (ListViewItem item in listView3.Items)
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

		public class MyTask
		{
			public int Id { get; set; }
			public string Name { get; set; }
			public string Deadline { get; set; }
			public string PriorityId { get; set; }
			public string Created {  get; set; }
		}

		public class NewTask
		{
			public int Id { get; set; }
			public string Name { get; set; }
			public string Deadline { get; set; }
			public string PriorityId { get; set; }
			public string Created { get; set; }
		}

		public class LineTask
		{
			public int Id { get; set; }
			public string Name { get; set; }
			public string Deadline { get; set; }
			public string PriorityId { get; set; }
			public string Created { get; set; }
		}

		public class CountTask
		{
			public int Id { get; set; }
			public string ResolutionDateFact { get; set; }
		}
	}
}
