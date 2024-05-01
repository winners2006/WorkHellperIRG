using Google.GData.Extensions;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Net.Http.Headers;
using System.Net.Http;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using System.Drawing;
using System.Globalization;

namespace WorkHellperIRG
{
	public class ConnectAndPushUrlTasks
	{
		static string emailIS = Properties.Settings.Default.emailIS;
		static string passwordIS = Properties.Settings.Default.passIS;
		static string idUser = Properties.Settings.Default.userNameId;

		//Адрес подключения
		public static HttpClient httpClient = new HttpClient()
		{

			BaseAddress = new Uri("https://intraservice.ru/api/"),

		};

		//Получение списка заявок назначенных на пользователя
		public static async Task<string> ConnectAndPushUrlMyTasks()
		{
			string urlISTasksMyTasks = $"task?fields=Id,Name,Deadline,EditorId&ExecutorIds={idUser}&StatusIDs=31,95,27";

			var result = string.Empty;
			httpClient.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
			var hesh = Convert.ToBase64String(Encoding.Default.GetBytes($"{emailIS}:{passwordIS}"));

			using (HttpRequestMessage request = new HttpRequestMessage(HttpMethod.Get, urlISTasksMyTasks))
			{
				httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Basic", hesh);
				var response = await httpClient.GetAsync(urlISTasksMyTasks);
				result = await response.Content.ReadAsStringAsync();
				if (response.StatusCode == HttpStatusCode.OK) ; // Form1.button2.BackColor = Color.Green;
				else result = string.Empty;
			}
			return await Task.FromResult(result);
		}
		//Получение списка заявок по Фильтрам
		public static async Task<string> ConnectAndPushUrlNewTasks()
		{
			string urlISTasksNewTasks = $"task?fields=Id,Name,Deadline,PriorityId,Created&filterid=3211&StatusIDs=31,95&PageSize=300";

			var result = string.Empty;
			httpClient.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
			var hesh = Convert.ToBase64String(Encoding.Default.GetBytes($"{emailIS}:{passwordIS}"));

			using (HttpRequestMessage request = new HttpRequestMessage(HttpMethod.Get, urlISTasksNewTasks))
			{
				httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Basic", hesh);
				var response = await httpClient.GetAsync(urlISTasksNewTasks);
				result = await response.Content.ReadAsStringAsync();
				if (response.StatusCode == HttpStatusCode.OK) ; // Form1.button2.BackColor = Color.Green;
				else result = string.Empty;
			}
			return await Task.FromResult(result);
		}
		//Получение списка заявок по Фильтрам
		public static async Task<string> ConnectAndPushUrlLineTasks()
		{
			string urlISTasksLineTasks = $"task?fields=Id,Name,Deadline&filterid=3213&StatusIDs=31,95&PageSize=300";

			var result = string.Empty;
			httpClient.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
			var hesh = Convert.ToBase64String(Encoding.Default.GetBytes($"{emailIS}:{passwordIS}"));

			using (HttpRequestMessage request = new HttpRequestMessage(HttpMethod.Get, urlISTasksLineTasks))
			{
				httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Basic", hesh);
				var response = await httpClient.GetAsync(urlISTasksLineTasks);
				result = await response.Content.ReadAsStringAsync();
				if (response.StatusCode == HttpStatusCode.OK) ; // Form1.button2.BackColor = Color.Green;
				else result = string.Empty;
			}
			return result;
		}

		//Получение списка выполненых заявок
		public static async Task<string> CountTasksWeek()
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
		public static async Task<string> CountTasksDay()
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
	}

}
