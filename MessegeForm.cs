using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace WorkHellperIRG
{
	public partial class MessegeForm : Form
	{
		int id;
		public int idCount;
		public static MessegeForm Instance;

		public MessegeForm(int id, string mess)
		{
			InitializeComponent();
			Instance = this;
			this.id = id;
			textBox1.Text = mess;
			Form1.Instance.countid = id;

			System.Media.SoundPlayer player = new System.Media.SoundPlayer();
			player.SoundLocation = "signal.wav";
			player.Play();
		}

		private void button1_Click(object sender, EventArgs e)
		{
			System.Diagnostics.Process.Start($"https://help.inventive.ru/Task/View/{id}");
			this.Close();
		}

		private void button2_Click(object sender, EventArgs e)
		{
			this.Close();
		}
	}
}
