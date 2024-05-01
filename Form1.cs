using System;

using System.Windows.Forms;

namespace WorkHellperIRG
{
	public partial class Form1 : Form
	{

		static string emailIS = Properties.Settings.Default.emailIS;
		static string passwordIS = Properties.Settings.Default.passIS;
		static string idUser = Properties.Settings.Default.userNameId;
		int coutnTasksMyTasks = 0;
		int coutnTasksNewTasks = 0;
		int coutnTasksLineTasks = 0;
		public int countWorkDay;
		public string test;
		
		public int countidMess = 0;
		int tasksType = 1;


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
			UpdateCloseTasks();
		}

		//Таймер обновления списка заявок
		public void TimerTasks()
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
		//Таймер обновления кол-ва выполненых заявок
		public void TimerCloseTasks()
		{
			Timer timer = new Timer();
			timer.Interval = (3600 * 1000);
			timer.Tick += new EventHandler(TimerCloseTasksTick);
			timer.Start();
		}
		private void TimerCloseTasksTick(object sender, EventArgs e)
		{
			UpdateCloseTasks();
		}

		//Обновление списка заявок
		public void UpdateTasks()
		{
			if (emailIS != null && emailIS != "" && passwordIS != null && passwordIS != "")
			{
				listView1.Items.Clear();
				listView2.Items.Clear();
				listView3.Items.Clear();

				TasksToTable.TasksToList(listView1);
				TasksToTable.TasksToList(listView2);
				TasksToTable.TasksToList(listView3);

				MessengeTasks.MessTasks(listView2);
			}
		}
		//Обновление выполненых заявок
		private void UpdateCloseTasks()
		{
			TasksToTable.CountTasksWeekJson(label2); //week
			TasksToTable.CountTasksDayJson(label7);  //day
		}


		private void buttonEnter(object sender, EventArgs e)
		{
			LoginForm dlg = new LoginForm();
			dlg.Show(this);
		}
		public void buttonUpdate(object sender, EventArgs e)
		{
			UpdateTasks();
			UpdateCloseTasks();
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

		//Обработка нажатия на заявку
		private void listView1_DoubleClick(object sender, EventArgs e)
		{
			try
			{
				if (listView1.SelectedItems.Count == 0)
					return;
				ListViewItem item = listView1.SelectedItems[0];
				string temp = item.ToString().Remove(0, 15);
				string temp2 = temp.TrimEnd(new char[] { '}' });
				System.Diagnostics.Process.Start($"https://intraservice.ru/Task/View/{temp2}");
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
				System.Diagnostics.Process.Start($"https://intraservice.ru/Task/View/{temp2}");
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
				System.Diagnostics.Process.Start($"https://intraservice.ru/Task/View/{temp2}");
			}
			catch (Exception ex)
			{
				MessageBox.Show(ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
			}
		}
	}
}
