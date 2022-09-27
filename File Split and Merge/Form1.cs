using System;
using System.Drawing;
using System.Windows.Forms;
using System.IO;
using System.Text;

namespace fsm
{
	/// <summary>
	/// Description of Form1.
	/// </summary>
	public partial class Form1 : Form
	{
		int i, j, file_out_size, n;
		FileStream file_in, file_out;
		string[] file_out_name = new string[255];
		string file_in_name, bat_str;
				
		public Form1(string file_for_split)
		{
			InitializeComponent();
			file_in_name = file_for_split;
		}
						
		void Button1Click(object sender, EventArgs e)
		{
			try {
				file_in = new FileStream(file_in_name, FileMode.Open, FileAccess.Read);
			} catch {
				MessageBox.Show("Ошибка открытия файла");
				return;
			}
			System.IO.FileInfo file = new System.IO.FileInfo(file_in_name);
			bat_str = @"copy /b ";
			i = 0;
			j = 0;
			if (radioButton1.Checked == true) file_out_size = (int) ((int) file.Length / numericUpDown1.Value)+1;
			if (radioButton2.Checked == true)	{
				file_out_size = (int) numericUpDown2.Value;
				if (comboBox1.Text == "КБ") file_out_size = file_out_size * 1024;
				if (comboBox1.Text == "МБ") file_out_size = file_out_size * 1024 * 1024;
			}
			if ((file.Length / file_out_size) < 101) 
			try {
				do {
					file_out_name[j] = file_in_name + "_" + j.ToString("D3");
					file_out = new FileStream(file_out_name[j], FileMode.Create);
					if (j > 0) bat_str = bat_str + "+";
					n = 1;
					do {
						i = file_in.ReadByte();
						if(i != -1) file_out.WriteByte((byte)i);
						n++;
						if (n > file_out_size) break;
					} while (i != -1);
					file_out.Close();
					bat_str = bat_str + "\"" + System.IO.Path.GetFileName(file_out_name[j]) + "\"";
					j++;
				} while (i != -1);
			} catch 
				{MessageBox.Show("Ошибка записи файла");
				return;
			}
			else MessageBox.Show("Ошибка. Много файлов.");
			bat_str = bat_str + " \"" + System.IO.Path.GetFileName(file_in_name) + "\"";
			file_in.Close();
			
			//запись в bat-файл
			try {
				if (checkBox1.Checked) {
					System.IO.StreamWriter st = 
						new System.IO.StreamWriter(file_in_name + ".bat", false, 
						                           System.Text.Encoding.GetEncoding(866));
					st.WriteLine(bat_str);
					st.Close();
				}
			} catch 
				{MessageBox.Show("Ошибка записи bat-файла");
				return;
			}
			Close();
		}
		
		void NumericUpDown2Enter(object sender, EventArgs e)
		{
			radioButton2.Checked = true;
		}
		
		void ComboBox1Enter(object sender, EventArgs e)
		{
			radioButton2.Checked = true;
		}
		
		void NumericUpDown1Enter(object sender, EventArgs e)
		{
			radioButton1.Checked = true;
		}
		
		void Form1Load(object sender, EventArgs e)
		{
			radioButton1.Checked = true;
		}
	}
}
