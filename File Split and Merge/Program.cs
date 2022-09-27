/*
 * Сделано в SharpDevelop.
 * Пользователь: pedop7_3
 * Дата: 11.05.2011
 * Время: 15:41
 * 
 * Для изменения этого шаблона используйте Сервис | Настройка | Кодирование | Правка стандартных заголовков.
 */
using System;
using System.Windows.Forms;

namespace fsm
{
	/// <summary>
	/// Class with program entry point.
	/// </summary>
	
	internal sealed class Program
	{
		/// <summary>
		/// Program entry point.
		/// </summary>
				
		[STAThread]
		private static void Main(string[] args)
		{
			Application.EnableVisualStyles();
			Application.SetCompatibleTextRenderingDefault(false);
			Application.Run(new MainForm());
		}
		
	}
}
