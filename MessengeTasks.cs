using System;
using System.Windows.Forms;

namespace WorkHellperIRG
{
	public class MessengeTasks
	{
		public int countid = 0;

		public static void MessTasks(ListView list)
		{
			foreach (ListViewItem i in list.Items)
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
					mf.Show();
				}
				else if (dtNow.Subtract(dateTimeStart).TotalMinutes < 2 & tempPriority == 10)
				{
					string message = $"Приоритет ВЫСОКИЙ заявка № {tempID}. Необходимо взять в работу в течении 40 минут. Выполнить в течении 8 часов! Открыть заявку?";
					MessegeForm mf = new MessegeForm(tempID, message);
					mf.Show();

				}
				else if (dtNow.Subtract(dateTimeStart).TotalMinutes < 2 & tempPriority == 9)
				{
					string message = $"Приоритет СРЕДНИЙ заявка № {tempID}. Необходимо взять в работу в течении 4 часов. Выполнить в течении 48 часов! Открыть заявку?";
					MessegeForm mf = new MessegeForm(tempID, message);
					mf.Show();
				}
				else if (dtNow.Subtract(dateTimeStart).TotalMinutes < 2 & tempPriority == 11)
				{
					string message = $"Приоритет НИЗКИЙ заявка № {tempID}. Необходимо взять в работу в течении 16 часов. Выполнить в течении 80 часов! Открыть заявку?";
					MessegeForm mf = new MessegeForm(tempID, message);
					mf.Show();
				}
				else if (dtNow.Subtract(dateTimeStart).TotalMinutes > 10 & dtNow.Subtract(dateTimeStart).TotalMinutes < 12 & tempPriority == 12)
				{
					string message = $"Приоритет КРИТИЧНЫЙ заявка № {tempID}. Необходимо срочно взять в работу! Открыть заявку?";
					MessegeForm mf = new MessegeForm(tempID, message);
					mf.Show();
				}
			}
		}
	}
}
