using System;
using System.IO;
using System.Collections.Generic;
using System.Drawing;
using System.Windows.Forms;

namespace fsm
{
	public partial class MainForm : Form
	{
		int i, j;
		FileStream file_in, file_out;
		string[] file_in_name = new string[255];
		int pos_dot;
		string file_out_name;
				
		public MainForm()
		{
			InitializeComponent();
			//AddOwnedForm(form1);
		}
		
		void Button1Click(object sender, EventArgs e)
		{
			for (j = 0; j < 254; j++) file_in_name[j] = null;
			openFileDialog1.ShowDialog();
		}

		
		void OpenFileDialog1FileOk(object sender, System.ComponentModel.CancelEventArgs e)
		{
			//выбор файлов для объединения
			j = 0;
			do {
				file_in_name[j] = openFileDialog1.FileNames[j];
				j++;
			} while (j != openFileDialog1.FileNames.Length);
			//определение результирующего файла
			if (file_in_name[0].EndsWith("_000"))
			   file_out_name = file_in_name[0].Substring(0, file_in_name[0].Length-4);
			   else {
				pos_dot = file_in_name[0].LastIndexOf(".");
				if (pos_dot != -1) file_out_name = file_in_name[0].Insert(pos_dot, "_merge");
					else file_out_name = file_in_name[0] + "_merge";
				}
				saveFileDialog1.FileName = file_out_name;
				saveFileDialog1.ShowDialog();
		}
		
		void SaveFileDialog1FileOk(object sender, System.ComponentModel.CancelEventArgs e)
		{
			//объединение
			try {
				file_out = new FileStream(saveFileDialog1.FileName, FileMode.Create);
			} catch 
				{MessageBox.Show("Ошибка записи файла");
				return;
			}
			j = 0;
			try {
				do {
					file_in = new FileStream(file_in_name[j], FileMode.Open, FileAccess.Read);
					do	{
						i = file_in.ReadByte();
						if(i != -1) file_out.WriteByte((byte)i);
					} while (i != -1);
					file_in.Close();
					j++;
				} while (file_in_name[j] != null);
			} catch 
				{MessageBox.Show("Ошибка чтения файла");
				return;
			}
			file_out.Close();
		}
		
		void Button3Click(object sender, EventArgs e)
		{
			Close();
		}
		
		void Button2Click(object sender, EventArgs e)
		{
			openFileDialog2.ShowDialog();
		}

		
		void OpenFileDialog2FileOk(object sender, System.ComponentModel.CancelEventArgs e)
		{
			Form1 form1 = new Form1(this.openFileDialog2.FileName);
			form1.ShowDialog();
		}
	}
}
