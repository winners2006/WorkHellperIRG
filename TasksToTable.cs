using Newtonsoft.Json.Linq;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace WorkHellperIRG
{
	public class TasksToTable
	{
		public static async void TasksToList(ListView listView)
		{
			try
			{
				var jsonTasks = await ConnectAndPushUrlTasks.ConnectAndPushUrlMyTasks();
				int coutnTasksMyTasks = 0;
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
						listView.Items.Add(item2);
					}
					else
					{
						ListViewItem item2 = new ListViewItem(new string[] { item.Id.ToString(), item.Name, item.Deadline, item.EditorId });
						listView.Items.Add(item2);
					}
				}
			}
			catch {  }
			LifeTimeTasks.DataEndTasksMyTasks(listView);
		}

		//подсчет выполненых заявок
		public static async void CountTasksWeekJson(Label label)
		{

			var jsonTasks = await ConnectAndPushUrlTasks.CountTasksWeek();
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
				label.Text = $"Выполненые: {countWeek}";
			}
			catch {  }
		}
		public static async void CountTasksDayJson(Label label)
		{
			var jsonTasks = await ConnectAndPushUrlTasks.CountTasksDay();
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
				label.Text = $"Выполненые: {countDay}";
			}
			catch {  }
		}
	}
}
