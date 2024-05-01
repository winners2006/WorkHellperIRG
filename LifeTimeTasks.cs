using System;
using System.Drawing;
using System.Windows.Forms;

namespace WorkHellperIRG
{
	public class LifeTimeTasks
	{
		public static void DataEndTasksMyTasks(ListView list)
		{
			foreach (ListViewItem item in list.Items)
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
					if (Convert.ToInt32(item.SubItems[3].Text) != Convert.ToInt32(Properties.Settings.Default.userNameId))
					{
						item.BackColor = Color.Green;
					}
				}
			}
		}
		public static void DataEndTasksNewTasks(ListView list)
		{
			foreach (ListViewItem item in list.Items)
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
		public static void DataEndTasksLineTasks(ListView list)
		{
			foreach (ListViewItem item in list.Items)
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
	}
}
