using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Globalization;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

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
		int coutnTasksMyTasks = 0;
		int coutnTasksNewTasks = 0;
		int coutnTasksLineTasks = 0;
		public int countWorkDay;
		public string test;
		public int countid = 0;
		public int countidMess = 0;


		public static Form1 Instance;

		public Form1()
		{
			InitializeComponent();
			Instance = this;
			

			

			label15.Text = Properties.Settings.Default.userName;

			listView1.Show();
			listView2.Hide();
			listView3.Hide();

			Update();
		}

		public void TimerTasks()
		{
			Timer timer = new Timer();
			timer.Interval = (90 * 1000);
			timer.Tick += new EventHandler(TimerTasksTick);
			timer.Start();
		}
		public void TimerCloseTasks()
		{
			Timer timer = new Timer();
			timer.Interval = (90 * 1000);
			timer.Tick += new EventHandler(TimerTasksTick);
			timer.Start();
		}

		private void TimerTasksTick(object sender, EventArgs e)
		{
			UpdateTasks();
		}

		public void UpdateTasks()
		{
			if (emailIS != null && emailIS != "" && passwordIS != null && passwordIS != "")
			{
				listView1.Items.Clear();
				listView2.Items.Clear();
				listView3.Items.Clear();

				TasksToTableMyTasks();
				TasksToTableNewTasks();
				TasksToTableLineTasks();

				MessengeTasks();

				CountTasksWeekJson();
				CountTasksDayJson();
			}
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

		private void listView1_DoubleClick(object sender, EventArgs e)
		{
			try
			{
				if (listView1.SelectedItems.Count == 0)
					return;
				ListViewItem item = listView1.SelectedItems[0];
				string temp = item.ToString().Remove(0, 15);
				string temp2 = temp.TrimEnd(new char[] { '}' });
				System.Diagnostics.Process.Start($"https://help.inventive.ru/Task/View/{temp2}");
			}
			catch (Exception ex)
			{
				MessageBox.Show(ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
			}
		}
		private void listView2_DoubleClick(object sender, EventArgs e)
		{
			try
			{
				if (listView2.SelectedItems.Count == 0)
					return;
				ListViewItem item = listView2.SelectedItems[0];
				string temp = item.ToString().Remove(0, 15);
				string temp2 = temp.TrimEnd(new char[] { '}' });
				System.Diagnostics.Process.Start($"https://help.inventive.ru/Task/View/{temp2}");
			}
			catch (Exception ex)
			{
				MessageBox.Show(ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
			}
		}
		private void listView3_DoubleClick(object sender, EventArgs e)
		{
			try
			{
				if (listView3.SelectedItems.Count == 0)
					return;
				ListViewItem item = listView3.SelectedItems[0];
				string temp = item.ToString().Remove(0, 15);
				string temp2 = temp.TrimEnd(new char[] { '}' });
				System.Diagnostics.Process.Start($"https://help.inventive.ru/Task/View/{temp2}");
			}
			catch (Exception ex)
			{
				MessageBox.Show(ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
			}
		}

		public static HttpClient httpClient = new HttpClient()
		{
			
			BaseAddress = new Uri("https://help.inventive.ru/api/"),

		};

		public async Task<string> ConnectAndPushUrlMyTasks()
		{
			urlISTasksMyTasks = $"task?fields=Id,Name,Deadline,EditorId&ExecutorIds={idUser}&StatusIDs=31,95,27";
			
			var result = string.Empty;
			httpClient.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
			var hesh = Convert.ToBase64String(Encoding.Default.GetBytes($"{emailIS}:{passwordIS}"));

			using (HttpRequestMessage request = new HttpRequestMessage(HttpMethod.Get, urlISTasksMyTasks))
			{
				httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Basic", hesh);
				var response = await httpClient.GetAsync(urlISTasksMyTasks);
				result = await response.Content.ReadAsStringAsync();
				if (response.StatusCode == HttpStatusCode.OK) button2.BackColor = Color.Green;
				else result = string.Empty;
			}
			return await Task.FromResult(result);
		}
		public async Task<string> ConnectAndPushUrlNewTasks()
		{
			urlISTasksNewTasks = $"task?fields=Id,Name,Deadline,PriorityId,Created&filterid=3211&StatusIDs=31,95&PageSize=300";

			var result = string.Empty;
			httpClient.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
			var hesh = Convert.ToBase64String(Encoding.Default.GetBytes($"{emailIS}:{passwordIS}"));

			using (HttpRequestMessage request = new HttpRequestMessage(HttpMethod.Get, urlISTasksNewTasks))
			{
				httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Basic", hesh);
				var response = await httpClient.GetAsync(urlISTasksNewTasks);
				result = await response.Content.ReadAsStringAsync();
				if (response.StatusCode == HttpStatusCode.OK) button2.BackColor = Color.Green;
				else result = string.Empty;
			}
			return await Task.FromResult(result);
		}
		public async Task<string> ConnectAndPushUrlLineTasks()
		{
			urlISTasksLineTasks = $"task?fields=Id,Name,Deadline&filterid=3213&StatusIDs=31,95&PageSize=300";

			var result = string.Empty;
			httpClient.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
			var hesh = Convert.ToBase64String(Encoding.Default.GetBytes($"{emailIS}:{passwordIS}"));

			using (HttpRequestMessage request = new HttpRequestMessage(HttpMethod.Get, urlISTasksLineTasks))
			{
				httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Basic", hesh);
				var response = await httpClient.GetAsync(urlISTasksLineTasks);
				result = await response.Content.ReadAsStringAsync();
				if (response.StatusCode == HttpStatusCode.OK) button2.BackColor = Color.Green;
				else result = string.Empty;
			}
			return result;
		}
		public async Task<string> CountTasksWeek()
		{
			DateTime dt = DateTime.Now;
			DateTime startOfWeek = dt.AddDays((((int)(dt.DayOfWeek) + 6) % 7) * -1);
			DateTime dtstart = DateTime.Parse(startOfWeek.ToString());
			string startW = dtstart.ToString("yyyy-MM-dd", CultureInfo.InvariantCulture);
			string urlCountTasks = $"task?fields=Id&ResolutionDateFactMoreThan={startW}&ExecutorIds={idUser}&PageSize=300";
			var result = string.Empty;
			httpClient.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
			var hesh = Convert.ToBase64String(Encoding.Default.GetBytes($"{emailIS}:{passwordIS}"));

			using (HttpRequestMessage request = new HttpRequestMessage(HttpMethod.Get, urlCountTasks))
			{
				httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Basic", hesh);
				var response = await httpClient.GetAsync(urlCountTasks);
				result = await response.Content.ReadAsStringAsync();
			}
			return result;
		}
		public async Task<string> CountTasksDay()
		{
			DateTime dt = DateTime.Now;
			string dayNow = dt.ToString("yyyy-MM-dd", CultureInfo.InvariantCulture);
			string urlCountTasks = $"task?fields=Id&ResolutionDateFactMoreThan={dayNow}&ExecutorIds={idUser}&PageSize=100";
			var result = string.Empty;
			httpClient.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
			var hesh = Convert.ToBase64String(Encoding.Default.GetBytes($"{emailIS}:{passwordIS}"));

			using (HttpRequestMessage request = new HttpRequestMessage(HttpMethod.Get, urlCountTasks))
			{
				httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Basic", hesh);
				var response = await httpClient.GetAsync(urlCountTasks);
				result = await response.Content.ReadAsStringAsync();
			}
			return result;
		}

		public async void TasksToTableMyTasks()
		{
			try
			{
				var jsonTasks = await ConnectAndPushUrlMyTasks();
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
						ListViewItem item2 = new ListViewItem(new string[] { item.Id.ToString(), item.Name, dateEnd, item.EditorId });
						listView1.Items.Add(item2);
					}
					else
					{
						ListViewItem item2 = new ListViewItem(new string[] { item.Id.ToString(), item.Name, item.Deadline, item.EditorId });
						listView1.Items.Add(item2);
					}
				}
			}
			catch { Application.Restart(); }
			DataEndTasksMyTasks();
		}
		public async void TasksToTableNewTasks()
		{
			try
			{
				string jsonTasks = await ConnectAndPushUrlNewTasks();
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
			} catch { Application.Restart(); }
			DataEndTasksNewTasks();
		}
		public async void TasksToTableLineTasks()
		{
			try
			{
				string jsonTasks = await ConnectAndPushUrlLineTasks();
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
			catch { Application.Restart(); }
			DataEndTasksLineTasks();
		}

		public void MessengeTasks()
		{
			foreach (ListViewItem i in listView2.Items)
			{
				int tempID = Convert.ToInt32(i.SubItems[0].Text);
				int tempPriority = Convert.ToInt32(i.SubItems[3].Text);
				if (tempID == countid) return;
				DateTime dtNow = DateTime.Now;
				string tempDateStart = i.SubItems[2].Text;
				DateTime dateTimeStart = DateTime.Parse(tempDateStart.ToString());
				if (dtNow.Subtract(dateTimeStart).TotalMinutes < 2 & tempPriority == 12)
				{
					string message = $"Приоритет КРИТИЧНЫЙ заявка № {tempID}. Необходимо взять в работу в течении 15 минут. Выполнить в течении 4 часов! Открыть заявку?";
					MessegeForm mf = new MessegeForm(tempID, message);
					mf.Show(this);
				}
				else if (dtNow.Subtract(dateTimeStart).TotalMinutes < 2 & tempPriority == 10)
				{
					string message = $"Приоритет ВЫСОКИЙ заявка № {tempID}. Необходимо взять в работу в течении 40 минут. Выполнить в течении 8 часов! Открыть заявку?";
					MessegeForm mf = new MessegeForm(tempID, message);
					mf.Show(this);
					
				}
				else if (dtNow.Subtract(dateTimeStart).TotalMinutes < 2 & tempPriority == 9)
				{
					string message = $"Приоритет СРЕДНИЙ заявка № {tempID}. Необходимо взять в работу в течении 4 часов. Выполнить в течении 48 часов! Открыть заявку?";
					MessegeForm mf = new MessegeForm(tempID, message);
					mf.Show(this);
				}
				else if (dtNow.Subtract(dateTimeStart).TotalMinutes < 2 & tempPriority == 11)
				{
					string message = $"Приоритет НИЗКИЙ заявка № {tempID}. Необходимо взять в работу в течении 16 часов. Выполнить в течении 80 часов! Открыть заявку?";
					MessegeForm mf = new MessegeForm(tempID, message);
					mf.Show(this);
				}
				else if (dtNow.Subtract(dateTimeStart).TotalMinutes > 10 & dtNow.Subtract(dateTimeStart).TotalMinutes < 12 & tempPriority == 12)
				{
					string message = $"Приоритет КРИТИЧНЫЙ заявка № {tempID}. Необходимо срочно взять в работу! Открыть заявку?";
					MessegeForm mf = new MessegeForm(tempID, message);
					mf.Show(this);
				}
			}
		}

		public async void CountTasksWeekJson()
		{

			var jsonTasks = await CountTasksWeek();
			int countWeek = 0;
			try
			{
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
				label2.Text = $"Выполненые: {countWeek}";
			}
			catch { Application.Restart(); }
		}
		public async void CountTasksDayJson()
		{
			var jsonTasks = await CountTasksDay();
			int countDay = 0;
			try
			{
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
				label7.Text = $"Выполненые: {countDay}";
			} 
			catch { Application.Restart(); }
		}

		public void DataEndTasksMyTasks()
		{
			foreach (ListViewItem item in listView1.Items)
			{
				int idTaskTemp = Convert.ToInt32(item.SubItems[0].Text);
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
				
				if (item.SubItems[3] != null && item.SubItems[3].Text != "")
				{
					if (Convert.ToInt32(item.SubItems[3].Text) != Convert.ToInt32(idUser))
					{
						item.BackColor = Color.Green;
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
			public string EditorId {  get; set; }
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
