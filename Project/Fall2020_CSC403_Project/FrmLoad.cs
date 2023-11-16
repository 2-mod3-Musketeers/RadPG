using Fall2020_CSC403_Project.code;
using Fall2020_CSC403_Project.Properties;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.IO;
using System.Runtime;
using MyGameLibrary;
using Newtonsoft.Json;
using System.Security.AccessControl;
using System.Runtime.ConstrainedExecution;

namespace Fall2020_CSC403_Project
{
    public partial class FrmLoad : Form
    {
        private Form previousForm;
        private FrmLevel level;

        private Label Save1;
        private Label Save2;
        private Label Save3;
        private Label Save4;

        private PictureBox Slot1;
        private PictureBox Slot2;
        private PictureBox Slot3;
        private PictureBox Slot4;

        private Bitmap screenshot;
        public FrmLoad(Form previousForm)
        {
            this.WindowState = FormWindowState.Maximized;
            this.previousForm = previousForm;
            this.level = Application.OpenForms["FrmLevel"] as FrmLevel;
            InitializeComponent();
            this.Setup();
        }

        private void Setup()
        {

            this.BackColor = Color.SlateGray;

            int height = Screen.PrimaryScreen.Bounds.Height;
            int width = Screen.PrimaryScreen.Bounds.Width;

            

        screenshot = new Bitmap(width, height);

            Save1 = new Label
            {
                Location = new Point(width / 8, 0),
                Text = "Save Slot 1",
                Parent = this,
                Size = new Size(3 * width / 8, height / 16),
                Font = new Font("NSimSun", height / 20),
                BackColor = Color.Transparent,
                ForeColor = Color.Black,
                BorderStyle = BorderStyle.None,
            };

            Save2 = new Label
            {
                Location = new Point(5 * width / 8 + width / 32, 0),
                Text = "Save Slot 2",
                Parent = this,
                Size = new Size(3 * width / 8, height / 16),
                Font = new Font("NSimSun", height / 20),
                BackColor = Color.Transparent,
                ForeColor = Color.Black,
                BorderStyle = BorderStyle.None,
            };

            Save3 = new Label
            {
                Location = new Point(width / 8, height / 2 - height / 32),
                Text = "Save Slot 3",
                Parent = this,
                Size = new Size(3 * width / 8, height / 16),
                Font = new Font("NSimSun", height / 20),
                BackColor = Color.Transparent,
                ForeColor = Color.Black,
                BorderStyle = BorderStyle.None,
            };

            Save4 = new Label
            {
                Location = new Point(5 * width / 8 + width / 32, height / 2 - height / 32),
                Text = "Save Slot 4",
                Parent = this,
                Size = new Size(3 * width / 8, height / 16),
                Font = new Font("NSimSun", height / 20),
                BackColor = Color.Transparent,
                ForeColor = Color.Black,
                BorderStyle = BorderStyle.None,
            };

            Slot1 = new PictureBox
            {
                Parent = this,
                Location = new Point(0, height / 16),
                Size = new Size(width / 2 - width / 32, height / 2 - 3 * height / 32),
                SizeMode = PictureBoxSizeMode.StretchImage,
                Image = this.CreateBitmap("../../data/slot1.png"),
                BorderStyle = BorderStyle.None,
            };
            Slot1.Click += Slot1_Click;

            Slot2 = new PictureBox
            {
                Parent = this,
                Location = new Point(width / 2 + width / 32, height / 16),
                Size = new Size(width / 2 - width / 32, height / 2 - 3 * height / 32),
                SizeMode = PictureBoxSizeMode.StretchImage,
                Image = this.CreateBitmap("../../data/slot2.png"),
            };
            Slot2.Click += Slot2_Click;

            Slot3 = new PictureBox
            {
                Parent = this,
                Location = new Point(0, height / 2 + height / 32),
                Size = new Size(width / 2 - width / 32, height / 2 - 3 * height / 32),
                SizeMode = PictureBoxSizeMode.StretchImage,
                Image = this.CreateBitmap("../../data/slot3.png"),
            };
            Slot3.Click += Slot3_Click;

            Slot4 = new PictureBox
            {
                Parent = this,
                Location = new Point(width / 2 + width / 32, height / 2 + height / 32),
                Size = new Size(width / 2 - width / 32, height / 2 - 3 * height / 32),
                SizeMode = PictureBoxSizeMode.StretchImage,
                Image = this.CreateBitmap("../../data/slot4.png"),
            };
            Slot4.Click += Slot4_Click;

            
        }

        private Bitmap CreateBitmap(string filepath)
        {
            using (FileStream stream = new FileStream(filepath, FileMode.Open, FileAccess.Read))
            // get a binary reader for the file stream
            using (BinaryReader reader = new BinaryReader(stream))
            {
                // copy the content of the file into a memory stream
                var memoryStream = new MemoryStream(reader.ReadBytes((int)stream.Length));
                // make a new Bitmap object the owner of the MemoryStream
                return new Bitmap(memoryStream);
            }
        }

        private void Slot1_Click(object sender, EventArgs e)
        {
            string filepath = "../../data/Save1Data.Json";

            string[] data = File.ReadAllLines(filepath);

            var settings = new JsonSerializerSettings
            {
                TypeNameHandling = TypeNameHandling.All,
                ReferenceLoopHandling = ReferenceLoopHandling.Ignore,
                ObjectCreationHandling = ObjectCreationHandling.Reuse,
            };


            List<Enemy>[] Enemies = new List<Enemy>[Game.Areas.Length];
            List<Item>[] Items = new List<Item>[Game.Areas.Length];
            List<NPC>[] Npcs = new List<NPC>[Game.Areas.Length];
            bool[] Visited = new bool[Game.Areas.Length];
            int CurrentArea;

            Game.player = JsonConvert.DeserializeObject<Player>(data[0], settings);
            Enemies = JsonConvert.DeserializeObject<List<Enemy>[]>(data[1], settings);
            Items = JsonConvert.DeserializeObject<List<Item>[]>(data[2], settings);
            Visited = JsonConvert.DeserializeObject<bool[]>(data[3], settings);
            CurrentArea = JsonConvert.DeserializeObject<int>(data[4], settings);
            Npcs = JsonConvert.DeserializeObject<List<NPC>[]>(data[5], settings);




            Game.AreaNum = CurrentArea;
            Game.CurrentArea = Game.Areas[CurrentArea];

            for (int i = 0; i < Game.Areas.Length; i++)
            {           
                Game.Areas[i].Enemies = Enemies[i];            
                Game.Areas[i].Items = Items[i];               
                Game.Areas[i].Visited = Visited[i];
            }


            

            for (int i = 0; i < Game.Areas.Length; i++)
            {
                for (int j = 0; j < Game.Areas[i].Enemies.Count; j++)
                {
                    Game.Areas[i].Enemies[j].RecreateEntity();
                    Game.Areas[i].Enemies[j].RecreateArchetype();
                }
                for (int j = 0; j < Game.Areas[i].Items.Count; j++)
                {
                    Game.Areas[i].Items[j].RecreateEntity();
                }
                for (int j = 0; j < Game.Areas[i].npcs.Count; j++)
                {
                    Console.WriteLine("HELLO");
                    Console.WriteLine(Game.Areas[i].npcs[j].Name);
                    Game.Areas[i].npcs[j].RecreateEntity();
                    Console.WriteLine(Game.Areas[i].npcs[j].Name);
                    Game.Areas[i].npcs[j].RecreateArchetype();
                    Game.Areas[i].npcs[j].Inventory.RecreateInventory();
                }
            }
            Game.player.RecreateEntity();
            Game.player.RecreateArchetype();
            Game.player.Inventory.RecreateInventory();

            for (int i = 0; i < Game.player.party.Length; i++)
            {
                if (Game.player.party[i] != null)
                {
                    Game.player.party[i].RecreateEntity();
                    Game.player.party[i].RecreateArchetype();
                    Game.player.party[i].Inventory.RecreateInventory();
                }
            }

            FrmLevel frmlevel = new FrmLevel(previousForm);

            frmlevel.FormClosed += (s, args) => this.Close();
            frmlevel.Show();
            this.Hide();
        }

        private void Slot2_Click(object sender, EventArgs e)
        {
            string filepath = "../../data/Save2Data.Json";

            string[] data = File.ReadAllLines(filepath);

            var settings = new JsonSerializerSettings
            {
                TypeNameHandling = TypeNameHandling.All,
                ReferenceLoopHandling = ReferenceLoopHandling.Ignore,
                ObjectCreationHandling = ObjectCreationHandling.Reuse,
            };


            List<Enemy>[] Enemies = new List<Enemy>[Game.Areas.Length];
            List<Item>[] Items = new List<Item>[Game.Areas.Length];
            bool[] Visited = new bool[Game.Areas.Length];
            int CurrentArea;

            Game.player = JsonConvert.DeserializeObject<Player>(data[0], settings);
            Enemies = JsonConvert.DeserializeObject<List<Enemy>[]>(data[1], settings);
            Items = JsonConvert.DeserializeObject<List<Item>[]>(data[2], settings);
            Visited = JsonConvert.DeserializeObject<bool[]>(data[3], settings);
            CurrentArea = JsonConvert.DeserializeObject<int>(data[4], settings);




            Game.AreaNum = CurrentArea;
            Game.CurrentArea = Game.Areas[CurrentArea];

            for (int i = 0; i < Game.Areas.Length; i++)
            {
                Game.Areas[i].Enemies = Enemies[i];
                Game.Areas[i].Items = Items[i];
                Game.Areas[i].Visited = Visited[i];
            }




            for (int i = 0; i < Game.Areas.Length; i++)
            {
                for (int j = 0; j < Game.Areas[i].Enemies.Count; j++)
                {
                    Game.Areas[i].Enemies[j].RecreateEntity();
                    Game.Areas[i].Enemies[j].RecreateArchetype();
                }
                for (int j = 0; j < Game.Areas[i].Items.Count; j++)
                {
                    Game.Areas[i].Items[j].RecreateEntity();
                }
            }
            Game.player.RecreateEntity();
            Game.player.RecreateArchetype();
            Game.player.Inventory.RecreateInventory();
            Game.player.Pic.BringToFront();

            for (int i = 0; i < Game.player.party.Length; i++)
            {
                Game.player.party[i].RecreateEntity();
                Game.player.party[i].RecreateArchetype();
                Game.player.party[i].Inventory.RecreateInventory();
            }

            FrmLevel frmlevel = new FrmLevel(previousForm);

            frmlevel.FormClosed += (s, args) => this.Close();
            frmlevel.Show();
            this.Hide();
        }

        private void Slot3_Click(object sender, EventArgs e)
        {
            string filepath = "../../data/Save3Data.Json";

            string[] data = File.ReadAllLines(filepath);

            var settings = new JsonSerializerSettings
            {
                TypeNameHandling = TypeNameHandling.All,
                ReferenceLoopHandling = ReferenceLoopHandling.Ignore,
                ObjectCreationHandling = ObjectCreationHandling.Reuse,
            };


            List<Enemy>[] Enemies = new List<Enemy>[Game.Areas.Length];
            List<Item>[] Items = new List<Item>[Game.Areas.Length];
            bool[] Visited = new bool[Game.Areas.Length];
            int CurrentArea;

            Game.player = JsonConvert.DeserializeObject<Player>(data[0], settings);
            Enemies = JsonConvert.DeserializeObject<List<Enemy>[]>(data[1], settings);
            Items = JsonConvert.DeserializeObject<List<Item>[]>(data[2], settings);
            Visited = JsonConvert.DeserializeObject<bool[]>(data[3], settings);
            CurrentArea = JsonConvert.DeserializeObject<int>(data[4], settings);




            Game.AreaNum = CurrentArea;
            Game.CurrentArea = Game.Areas[CurrentArea];

            for (int i = 0; i < Game.Areas.Length; i++)
            {
                Game.Areas[i].Enemies = Enemies[i];
                Game.Areas[i].Items = Items[i];
                Game.Areas[i].Visited = Visited[i];
            }




            for (int i = 0; i < Game.Areas.Length; i++)
            {
                for (int j = 0; j < Game.Areas[i].Enemies.Count; j++)
                {
                    Game.Areas[i].Enemies[j].RecreateEntity();
                    Game.Areas[i].Enemies[j].RecreateArchetype();
                }
                for (int j = 0; j < Game.Areas[i].Items.Count; j++)
                {
                    Game.Areas[i].Items[j].RecreateEntity();
                }
            }
            Game.player.RecreateEntity();
            Game.player.RecreateArchetype();
            Game.player.Inventory.RecreateInventory();
            Game.player.Pic.BringToFront();

            for (int i = 0; i < Game.player.party.Length; i++)
            {
                if (Game.player.party[i] == null)
                {
                    Game.player.party[i].RecreateEntity();
                    Game.player.party[i].RecreateArchetype();
                    Game.player.party[i].Inventory.RecreateInventory();
                }
            }

            FrmLevel frmlevel = new FrmLevel(previousForm);

            frmlevel.FormClosed += (s, args) => this.Close();
            frmlevel.Show();
            this.Hide();
        }

        private void Slot4_Click(object sender, EventArgs e)
        {
            string filepath = "../../data/Save4Data.Json";

            string[] data = File.ReadAllLines(filepath);

            var settings = new JsonSerializerSettings
            {
                TypeNameHandling = TypeNameHandling.All,
                ReferenceLoopHandling = ReferenceLoopHandling.Ignore,
                ObjectCreationHandling = ObjectCreationHandling.Reuse,
            };


            List<Enemy>[] Enemies = new List<Enemy>[Game.Areas.Length];
            List<Item>[] Items = new List<Item>[Game.Areas.Length];
            bool[] Visited = new bool[Game.Areas.Length];
            int CurrentArea;

            Game.player = JsonConvert.DeserializeObject<Player>(data[0], settings);
            Enemies = JsonConvert.DeserializeObject<List<Enemy>[]>(data[1], settings);
            Items = JsonConvert.DeserializeObject<List<Item>[]>(data[2], settings);
            Visited = JsonConvert.DeserializeObject<bool[]>(data[3], settings);
            CurrentArea = JsonConvert.DeserializeObject<int>(data[4], settings);




            Game.AreaNum = CurrentArea;
            Game.CurrentArea = Game.Areas[CurrentArea];

            for (int i = 0; i < Game.Areas.Length; i++)
            {
                Game.Areas[i].Enemies = Enemies[i];
                Game.Areas[i].Items = Items[i];
                Game.Areas[i].Visited = Visited[i];
            }




            for (int i = 0; i < Game.Areas.Length; i++)
            {
                for (int j = 0; j < Game.Areas[i].Enemies.Count; j++)
                {
                    Game.Areas[i].Enemies[j].RecreateEntity();
                    Game.Areas[i].Enemies[j].RecreateArchetype();
                }
                for (int j = 0; j < Game.Areas[i].Items.Count; j++)
                {
                    Game.Areas[i].Items[j].RecreateEntity();
                }
            }
            Game.player.RecreateEntity();
            Game.player.RecreateArchetype();
            Game.player.Inventory.RecreateInventory();
            Game.player.Pic.BringToFront();

            for (int i = 0; i < Game.player.party.Length; i++)
            {
                Game.player.party[i].RecreateEntity();
                Game.player.party[i].RecreateArchetype();
                Game.player.party[i].Inventory.RecreateInventory();
            }

            FrmLevel frmlevel = new FrmLevel(previousForm);

            frmlevel.FormClosed += (s, args) => this.Close();
            frmlevel.Show();
            this.Hide();
        }
    }
}
