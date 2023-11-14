using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.Json;
using System.Text.Json.Serialization;
using System.Threading.Tasks;
using System.Windows.Forms;
using Fall2020_CSC403_Project.code;
using Fall2020_CSC403_Project.Properties;
using System.Drawing;

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
            Game.PopulateWorld();
            Archetype archetype = new Rogue();
            PictureBox PlayerPic = Game.MakePictureBox
            (
                Resources.rogue,
                new Point(0, 0),
                new Size(100,100)
            );
            //{
            //    BackColor = Color.FromArgb(128, Color.DimGray),
            //    Location = new Point(0, 0),
            //    Size = new Size(100, 100),
            //    SizeMode = PictureBoxSizeMode.StretchImage,
            //    Image = Resources.rogue,
            //};

            //var settings = new JsonSerializerSettings
            //{
            //    TypeNameHandling = TypeNameHandling.All,
            //    ReferenceLoopHandling = ReferenceLoopHandling.Ignore,
            //};

            JsonSerializerOptions settings = new JsonSerializerOptions
            {
                IncludeFields = true,
            };

            Player player1 = new Player("Joe", PlayerPic, archetype);
            player1.Pic = null;
            player1.archetype = null;
            var data = JsonSerializer.Serialize(player1, settings);
            Console.WriteLine(data);
            Console.WriteLine("HI");
            Player player2 = JsonSerializer.Deserialize<Player>(data, settings);
            Console.WriteLine(player2.Health);


            //Application.EnableVisualStyles();
            //Application.SetCompatibleTextRenderingDefault(false);
            //Game.PopulateWorld();
            //Application.Run(new FrmMain());
        }
    }
}
