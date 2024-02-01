namespace WorkHellperIRG
{
	partial class LoginForm
	{
		/// <summary>
		/// Required designer variable.
		/// </summary>
		private System.ComponentModel.IContainer components = null;

		/// <summary>
		/// Clean up any resources being used.
		/// </summary>
		/// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
		protected override void Dispose(bool disposing)
		{
			if (disposing && (components != null))
			{
				components.Dispose();
			}
			base.Dispose(disposing);
		}

		#region Windows Form Designer generated code

		/// <summary>
		/// Required method for Designer support - do not modify
		/// the contents of this method with the code editor.
		/// </summary>
		private void InitializeComponent()
		{
			this.label1 = new System.Windows.Forms.Label();
			this.label2 = new System.Windows.Forms.Label();
			this.button2 = new System.Windows.Forms.Button();
			this.textBox3 = new System.Windows.Forms.TextBox();
			this.textBox4 = new System.Windows.Forms.TextBox();
			this.textBox1 = new System.Windows.Forms.TextBox();
			this.textBox2 = new System.Windows.Forms.TextBox();
			this.button1 = new System.Windows.Forms.Button();
			this.label3 = new System.Windows.Forms.Label();
			this.button3 = new System.Windows.Forms.Button();
			this.button4 = new System.Windows.Forms.Button();
			this.SuspendLayout();
			// 
			// label1
			// 
			this.label1.AutoSize = true;
			this.label1.Location = new System.Drawing.Point(12, 9);
			this.label1.Name = "label1";
			this.label1.Size = new System.Drawing.Size(465, 16);
			this.label1.TabIndex = 3;
			this.label1.Text = "Для корректного отображения данных необходимо авторизироваться";
			// 
			// label2
			// 
			this.label2.AutoSize = true;
			this.label2.Location = new System.Drawing.Point(158, 43);
			this.label2.Name = "label2";
			this.label2.Size = new System.Drawing.Size(168, 16);
			this.label2.TabIndex = 4;
			this.label2.Text = "Авторизация IntraService";
			// 
			// button2
			// 
			this.button2.Location = new System.Drawing.Point(352, 149);
			this.button2.Name = "button2";
			this.button2.Size = new System.Drawing.Size(90, 50);
			this.button2.TabIndex = 7;
			this.button2.Text = "Вход";
			this.button2.UseVisualStyleBackColor = true;
			this.button2.Click += new System.EventHandler(this.buttonEnterGoogle);
			// 
			// textBox3
			// 
			this.textBox3.Location = new System.Drawing.Point(58, 177);
			this.textBox3.Name = "textBox3";
			this.textBox3.Size = new System.Drawing.Size(277, 22);
			this.textBox3.TabIndex = 6;
			// 
			// textBox4
			// 
			this.textBox4.Location = new System.Drawing.Point(58, 149);
			this.textBox4.Name = "textBox4";
			this.textBox4.Size = new System.Drawing.Size(277, 22);
			this.textBox4.TabIndex = 5;
			// 
			// textBox1
			// 
			this.textBox1.Location = new System.Drawing.Point(58, 62);
			this.textBox1.Name = "textBox1";
			this.textBox1.Size = new System.Drawing.Size(277, 22);
			this.textBox1.TabIndex = 0;
			// 
			// textBox2
			// 
			this.textBox2.Location = new System.Drawing.Point(58, 90);
			this.textBox2.Name = "textBox2";
			this.textBox2.PasswordChar = '*';
			this.textBox2.Size = new System.Drawing.Size(277, 22);
			this.textBox2.TabIndex = 1;
			// 
			// button1
			// 
			this.button1.Location = new System.Drawing.Point(352, 62);
			this.button1.Name = "button1";
			this.button1.Size = new System.Drawing.Size(90, 50);
			this.button1.TabIndex = 2;
			this.button1.Text = "Вход";
			this.button1.UseVisualStyleBackColor = true;
			this.button1.Click += new System.EventHandler(this.buttonEnterIS);
			// 
			// label3
			// 
			this.label3.AutoSize = true;
			this.label3.Location = new System.Drawing.Point(158, 130);
			this.label3.Name = "label3";
			this.label3.Size = new System.Drawing.Size(142, 16);
			this.label3.TabIndex = 8;
			this.label3.Text = "Авторизация Google";
			// 
			// button3
			// 
			this.button3.Location = new System.Drawing.Point(720, 177);
			this.button3.Name = "button3";
			this.button3.Size = new System.Drawing.Size(104, 42);
			this.button3.TabIndex = 13;
			this.button3.Text = "Сохранить";
			this.button3.UseVisualStyleBackColor = true;
			this.button3.Click += new System.EventHandler(this.buttonSaveSettings);
			// 
			// button4
			// 
			this.button4.Location = new System.Drawing.Point(548, 177);
			this.button4.Name = "button4";
			this.button4.Size = new System.Drawing.Size(104, 42);
			this.button4.TabIndex = 14;
			this.button4.Text = "Сбросить";
			this.button4.UseVisualStyleBackColor = true;
			this.button4.Click += new System.EventHandler(this.buttonResetSettings);
			// 
			// LoginForm
			// 
			this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			this.ClientSize = new System.Drawing.Size(836, 224);
			this.Controls.Add(this.button4);
			this.Controls.Add(this.button3);
			this.Controls.Add(this.label3);
			this.Controls.Add(this.button2);
			this.Controls.Add(this.textBox3);
			this.Controls.Add(this.textBox4);
			this.Controls.Add(this.label2);
			this.Controls.Add(this.label1);
			this.Controls.Add(this.button1);
			this.Controls.Add(this.textBox2);
			this.Controls.Add(this.textBox1);
			this.Name = "LoginForm";
			this.Text = "LoginForm";
			this.ResumeLayout(false);
			this.PerformLayout();

		}

		#endregion
		public System.Windows.Forms.Label label1;
		public System.Windows.Forms.Label label2;
		public System.Windows.Forms.Button button2;
		public System.Windows.Forms.TextBox textBox3;
		public System.Windows.Forms.TextBox textBox4;
		public System.Windows.Forms.TextBox textBox1;
		public System.Windows.Forms.TextBox textBox2;
		public System.Windows.Forms.Button button1;
		public System.Windows.Forms.Label label3;
		private System.Windows.Forms.Button button3;
		private System.Windows.Forms.Button button4;
	}
}