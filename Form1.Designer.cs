using System.Security.Cryptography.X509Certificates;

namespace WorkHellperIRG
{
	 partial class Form1
	 {


		private System.ComponentModel.IContainer components = null;

		protected override void Dispose(bool disposing)
		{
			if (disposing && (components != null))
			{
				components.Dispose();
			}
			base.Dispose(disposing);
		}

		#region Код, автоматически созданный конструктором форм Windows

		/// <summary>
		/// Требуемый метод для поддержки конструктора — не изменяйте 
		/// содержимое этого метода с помощью редактора кода.
		/// </summary>
		private void InitializeComponent()
		{
			this.button1 = new System.Windows.Forms.Button();
			this.monthCalendar1 = new System.Windows.Forms.MonthCalendar();
			this.panel1 = new System.Windows.Forms.Panel();
			this.label4 = new System.Windows.Forms.Label();
			this.label3 = new System.Windows.Forms.Label();
			this.label2 = new System.Windows.Forms.Label();
			this.label1 = new System.Windows.Forms.Label();
			this.panel3 = new System.Windows.Forms.Panel();
			this.label13 = new System.Windows.Forms.Label();
			this.label12 = new System.Windows.Forms.Label();
			this.label11 = new System.Windows.Forms.Label();
			this.label10 = new System.Windows.Forms.Label();
			this.label9 = new System.Windows.Forms.Label();
			this.panel2 = new System.Windows.Forms.Panel();
			this.label5 = new System.Windows.Forms.Label();
			this.label6 = new System.Windows.Forms.Label();
			this.label7 = new System.Windows.Forms.Label();
			this.label8 = new System.Windows.Forms.Label();
			this.label14 = new System.Windows.Forms.Label();
			this.label15 = new System.Windows.Forms.Label();
			this.button2 = new System.Windows.Forms.Button();
			this.listView1 = new System.Windows.Forms.ListView();
			this.Id = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
			this.name = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
			this.DataEnd = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
			this.panel1.SuspendLayout();
			this.panel3.SuspendLayout();
			this.panel2.SuspendLayout();
			this.SuspendLayout();
			// 
			// button1
			// 
			this.button1.Location = new System.Drawing.Point(785, 5);
			this.button1.Name = "button1";
			this.button1.Size = new System.Drawing.Size(91, 36);
			this.button1.TabIndex = 1;
			this.button1.Text = "Вход";
			this.button1.UseVisualStyleBackColor = true;
			this.button1.Click += new System.EventHandler(this.buttonEnter);
			// 
			// monthCalendar1
			// 
			this.monthCalendar1.Location = new System.Drawing.Point(785, 79);
			this.monthCalendar1.Name = "monthCalendar1";
			this.monthCalendar1.TabIndex = 4;
			// 
			// panel1
			// 
			this.panel1.Controls.Add(this.label4);
			this.panel1.Controls.Add(this.label3);
			this.panel1.Controls.Add(this.label2);
			this.panel1.Controls.Add(this.label1);
			this.panel1.Location = new System.Drawing.Point(548, 79);
			this.panel1.Name = "panel1";
			this.panel1.Size = new System.Drawing.Size(225, 107);
			this.panel1.TabIndex = 5;
			// 
			// label4
			// 
			this.label4.AutoSize = true;
			this.label4.Location = new System.Drawing.Point(3, 67);
			this.label4.Name = "label4";
			this.label4.Size = new System.Drawing.Size(103, 16);
			this.label4.TabIndex = 8;
			this.label4.Text = "Просроченые: ";
			// 
			// label3
			// 
			this.label3.AutoSize = true;
			this.label3.Location = new System.Drawing.Point(3, 51);
			this.label3.Name = "label3";
			this.label3.Size = new System.Drawing.Size(88, 16);
			this.label3.TabIndex = 8;
			this.label3.Text = "Отмененые: ";
			// 
			// label2
			// 
			this.label2.AutoSize = true;
			this.label2.Location = new System.Drawing.Point(3, 35);
			this.label2.Name = "label2";
			this.label2.Size = new System.Drawing.Size(96, 16);
			this.label2.TabIndex = 8;
			this.label2.Text = "Выполненые: ";
			// 
			// label1
			// 
			this.label1.AutoSize = true;
			this.label1.Location = new System.Drawing.Point(3, 9);
			this.label1.Name = "label1";
			this.label1.Size = new System.Drawing.Size(217, 16);
			this.label1.TabIndex = 0;
			this.label1.Text = "Выполненые заявки за неделю: ";
			// 
			// panel3
			// 
			this.panel3.Controls.Add(this.label13);
			this.panel3.Controls.Add(this.label12);
			this.panel3.Controls.Add(this.label11);
			this.panel3.Controls.Add(this.label10);
			this.panel3.Controls.Add(this.label9);
			this.panel3.Location = new System.Drawing.Point(548, 305);
			this.panel3.Name = "panel3";
			this.panel3.Size = new System.Drawing.Size(225, 99);
			this.panel3.TabIndex = 7;
			// 
			// label13
			// 
			this.label13.AutoSize = true;
			this.label13.Location = new System.Drawing.Point(3, 75);
			this.label13.Name = "label13";
			this.label13.Size = new System.Drawing.Size(110, 16);
			this.label13.TabIndex = 12;
			this.label13.Text = "Ожидаемая ЗП: ";
			// 
			// label12
			// 
			this.label12.AutoSize = true;
			this.label12.Location = new System.Drawing.Point(3, 59);
			this.label12.Name = "label12";
			this.label12.Size = new System.Drawing.Size(160, 16);
			this.label12.TabIndex = 11;
			this.label12.Text = "Дополнительные часы: ";
			// 
			// label11
			// 
			this.label11.AutoSize = true;
			this.label11.Location = new System.Drawing.Point(3, 43);
			this.label11.Name = "label11";
			this.label11.Size = new System.Drawing.Size(190, 16);
			this.label11.TabIndex = 10;
			this.label11.Text = "Количество рабочих часов: ";
			// 
			// label10
			// 
			this.label10.AutoSize = true;
			this.label10.Location = new System.Drawing.Point(3, 27);
			this.label10.Name = "label10";
			this.label10.Size = new System.Drawing.Size(182, 16);
			this.label10.TabIndex = 9;
			this.label10.Text = "количество рабочих дней: ";
			// 
			// label9
			// 
			this.label9.AutoSize = true;
			this.label9.Location = new System.Drawing.Point(3, 11);
			this.label9.Name = "label9";
			this.label9.Size = new System.Drawing.Size(144, 16);
			this.label9.TabIndex = 8;
			this.label9.Text = "Статистика за месяц";
			// 
			// panel2
			// 
			this.panel2.Controls.Add(this.label5);
			this.panel2.Controls.Add(this.label6);
			this.panel2.Controls.Add(this.label7);
			this.panel2.Controls.Add(this.label8);
			this.panel2.Location = new System.Drawing.Point(548, 192);
			this.panel2.Name = "panel2";
			this.panel2.Size = new System.Drawing.Size(225, 107);
			this.panel2.TabIndex = 6;
			// 
			// label5
			// 
			this.label5.AutoSize = true;
			this.label5.Location = new System.Drawing.Point(3, 67);
			this.label5.Name = "label5";
			this.label5.Size = new System.Drawing.Size(103, 16);
			this.label5.TabIndex = 8;
			this.label5.Text = "Просроченые: ";
			// 
			// label6
			// 
			this.label6.AutoSize = true;
			this.label6.Location = new System.Drawing.Point(3, 51);
			this.label6.Name = "label6";
			this.label6.Size = new System.Drawing.Size(88, 16);
			this.label6.TabIndex = 8;
			this.label6.Text = "Отмененые: ";
			// 
			// label7
			// 
			this.label7.AutoSize = true;
			this.label7.Location = new System.Drawing.Point(3, 35);
			this.label7.Name = "label7";
			this.label7.Size = new System.Drawing.Size(96, 16);
			this.label7.TabIndex = 8;
			this.label7.Text = "Выполненые: ";
			// 
			// label8
			// 
			this.label8.AutoSize = true;
			this.label8.Location = new System.Drawing.Point(3, 9);
			this.label8.Name = "label8";
			this.label8.Size = new System.Drawing.Size(198, 16);
			this.label8.TabIndex = 0;
			this.label8.Text = "Выполненые заявки за день: ";
			// 
			// label14
			// 
			this.label14.AutoSize = true;
			this.label14.Location = new System.Drawing.Point(112, 51);
			this.label14.Name = "label14";
			this.label14.Size = new System.Drawing.Size(164, 16);
			this.label14.TabIndex = 8;
			this.label14.Text = "Список задач в работе: ";
			// 
			// label15
			// 
			this.label15.AutoSize = true;
			this.label15.Location = new System.Drawing.Point(571, 15);
			this.label15.Name = "label15";
			this.label15.Size = new System.Drawing.Size(168, 16);
			this.label15.TabIndex = 9;
			this.label15.Text = "Иванов Иван Сергеевич";
			// 
			// button2
			// 
			this.button2.Location = new System.Drawing.Point(12, 18);
			this.button2.Name = "button2";
			this.button2.Size = new System.Drawing.Size(75, 23);
			this.button2.TabIndex = 10;
			this.button2.Text = "Обновить";
			this.button2.UseVisualStyleBackColor = true;
			this.button2.Click += new System.EventHandler(this.buttonUpdate);
			// 
			// listView1
			// 
			this.listView1.AllowColumnReorder = true;
			this.listView1.Columns.AddRange(new System.Windows.Forms.ColumnHeader[] {
            this.Id,
            this.name,
            this.DataEnd});
			this.listView1.FullRowSelect = true;
			this.listView1.HideSelection = false;
			this.listView1.Location = new System.Drawing.Point(12, 79);
			this.listView1.Name = "listView1";
			this.listView1.Size = new System.Drawing.Size(530, 325);
			this.listView1.TabIndex = 11;
			this.listView1.UseCompatibleStateImageBehavior = false;
			this.listView1.View = System.Windows.Forms.View.Details;
			// 
			// Id
			// 
			this.Id.Text = "Id";
			// 
			// name
			// 
			this.name.Text = "Name";
			this.name.Width = 291;
			// 
			// DataEnd
			// 
			this.DataEnd.Text = "DataEnd";
			this.DataEnd.Width = 107;
			// 
			// Form1
			// 
			this.ClientSize = new System.Drawing.Size(990, 412);
			this.Controls.Add(this.listView1);
			this.Controls.Add(this.button2);
			this.Controls.Add(this.label15);
			this.Controls.Add(this.label14);
			this.Controls.Add(this.panel2);
			this.Controls.Add(this.panel3);
			this.Controls.Add(this.panel1);
			this.Controls.Add(this.monthCalendar1);
			this.Controls.Add(this.button1);
			this.Name = "Form1";
			this.panel1.ResumeLayout(false);
			this.panel1.PerformLayout();
			this.panel3.ResumeLayout(false);
			this.panel3.PerformLayout();
			this.panel2.ResumeLayout(false);
			this.panel2.PerformLayout();
			this.ResumeLayout(false);
			this.PerformLayout();

		}

		#endregion
		private System.Windows.Forms.Button button1;
		private System.Windows.Forms.MonthCalendar monthCalendar1;
		private System.Windows.Forms.Panel panel1;
		private System.Windows.Forms.Panel panel3;
		private System.Windows.Forms.Label label1;
		private System.Windows.Forms.Label label2;
		private System.Windows.Forms.Label label3;
		private System.Windows.Forms.Label label4;
		private System.Windows.Forms.Panel panel2;
		private System.Windows.Forms.Label label5;
		private System.Windows.Forms.Label label6;
		private System.Windows.Forms.Label label7;
		private System.Windows.Forms.Label label8;
		private System.Windows.Forms.Label label11;
		private System.Windows.Forms.Label label10;
		private System.Windows.Forms.Label label9;
		private System.Windows.Forms.Label label13;
		private System.Windows.Forms.Label label12;
		private System.Windows.Forms.Label label14;
		public System.Windows.Forms.Label label15;
		private System.Windows.Forms.Button button2;
		private System.Windows.Forms.ListView listView1;
		private System.Windows.Forms.ColumnHeader Id;
		private System.Windows.Forms.ColumnHeader name;
		private System.Windows.Forms.ColumnHeader DataEnd;
	}
	//public Label Label15 { get { return this.Label15.Text; } set { this.Label15.Text = value; } }

	
}

