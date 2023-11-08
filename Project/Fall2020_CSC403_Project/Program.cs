using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Forms;
using Fall2020_CSC403_Project.code;

namespace Fall2020_CSC403_Project
{
	static class Program
	{
		/// <summary>
		/// The main entry point for the application.
		/// </summary>
		[STAThread]
		static void Main()
		{
			Application.EnableVisualStyles();
			Application.SetCompatibleTextRenderingDefault(false);
			Game.PopulateWorld();
			Application.Run(new FrmMain());
		}
	}
}
