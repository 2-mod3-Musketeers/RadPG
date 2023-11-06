using Fall2020_CSC403_Project.code;
using Fall2020_CSC403_Project.Properties;
using MyGameLibrary;
using System;
using System.Diagnostics;
using System.Drawing;
using System.Collections.Generic;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Windows.Input;
using System.Threading;
using System.Media;
using System.Text.Json;
using System.IO;
using System.Linq;
using System.Text;

namespace Fall2020_CSC403_Project
{
    public partial class FrmLevel : Form
    {
        private Player player;

        private Form MainMenu;

        public int Area { get; set; }
        public int score;

        public bool gameOver { get; set; }

        private DateTime timeBegin;
        private FrmBattleScreen frmBattleScreen;
        private FrmInventory frminventory;

        private Size itemSize = new Size(50, 50);
        private Size signSize = new Size(75, 50);

        public SoundPlayer gameAudio;

        public Area[] Areas;

        public Bitmap TerrainPic;

        private Point LastPanOffset = new Point(0, 0);
        private Point PanOffset = new Point(0, 0);
        private Thread panningThread;
        private Keys keyDown;
        private bool moving;

        private Direction TravelDirection = Direction.None;

        public Label ScoreLabel;

        public FrmLevel(Form MainMenu, Player player)
        {
            this.KeyPreview = true;
            this.DoubleBuffered = true;

            this.ScoreLabel = null;

            this.score = 0;

            this.gameAudio = new SoundPlayer(Resources.Game_audio);

            this.player = player;
            this.gameOver = false;
            this.WindowState = FormWindowState.Maximized;
            this.Area = 4;
            InitializeComponent();
            this.MainMenu = MainMenu;

            this.Areas = new Area[10];

            panningThread = new Thread(PanningThread);
            panningThread.IsBackground = true;
            panningThread.Start();

        }


        private void FrmLevel_Load(object sender, EventArgs e)
        {
            this.Areas[0] = new Area("Malek's Mountain", 12, 0.05);
            this.Areas[1] = new Area("Village Ruins", 901, 0.05);
            this.Areas[2] = new Area("Buddy Beachfront", 890, 0.05);
            this.Areas[3] = new Area("Uphill Hill", 789, 0.05);
            this.Areas[4] = new Area("Plainsfield", 678, 0.05);
            this.Areas[5] = new Area("Lower Harmony Village", 567, 0.05);
            this.Areas[6] = new Area("Windy Plateau", 456, 0.05);
            this.Areas[7] = new Area("Harmony Plains", 345, 0.05);
            this.Areas[8] = new Area("Harmony Village", 623, 0.15);
            this.Areas[9] = new Area("Dragon's Lair", 123, 0.2);


            for (int i = 0; i < 9; i++)
            {
                setAdjacency(i);
            }


            AreaSelect();

            InitializeAreaLayout();
            Game.player = player;
            timeBegin = DateTime.Now;

            this.ScoreLabel = new Label();
            this.Controls.Add(ScoreLabel);
            ScoreLabel.BackColor = Color.Black;
            ScoreLabel.ForeColor = Color.White;
            ScoreLabel.Text = "Score: " + score.ToString();
            ScoreLabel.Location = new Point(Screen.PrimaryScreen.Bounds.Width - ScoreLabel.Width - 20, 15);
            ScoreLabel.Font = new Font("Microsoft Sans Serif", 11);
            ScoreLabel.Visible = true;
            ScoreLabel.BringToFront();
            
            
            

            //Adding stop condition to mainMenu_music here
            FrmMain mainMenuPlayer = Application.OpenForms["FrmMain"] as FrmMain;
            FrmMain mainMenu = Application.OpenForms["FrmMain"] as FrmMain;
            if (mainMenu != null && mainMenu.mainMenuPlayer != null)
            {
                mainMenu.mainMenuPlayer.Stop();
                mainMenu.mainMenuPlayer.Dispose();
            }

            //Adding gameAudio here
            this.gameAudio.PlayLooping();

            SignPanel.Size = new Size(Screen.PrimaryScreen.Bounds.Width - 200, Screen.PrimaryScreen.Bounds.Height - 200);
            SignPanel.Location = new Point(Screen.PrimaryScreen.Bounds.Width / 2 - SignPanel.Width / 2,  50);

            TravelLabel.Size = new Size(SignPanel.Size.Width, SignPanel.Size.Height / 2);
            TravelLabel.Location = new Point(Screen.PrimaryScreen.Bounds.Width / 2 - TravelLabel.Width / 2 - 100, Screen.PrimaryScreen.Bounds.Height / 2 - TravelLabel.Height / 2 - 50);

            TravelButton.Size = new Size(SignPanel.Size.Width - 200, SignPanel.Size.Height / 4);
            TravelButton.Location = new Point(Screen.PrimaryScreen.Bounds.Width / 2 - SignPanel.Width / 2, Screen.PrimaryScreen.Bounds.Height / 2);


        }


        private void PanningThread()
        {
            int step = 3;
            while (true)
            {
                switch (this.keyDown) {
                    case Keys.A:
                        moving = true;
                        this.PanOffset.X += step;

                        break;

                    case Keys.D:
                        moving = true;
                        this.PanOffset.X -= step;

                        break;

                    case Keys.W:
                        moving = true;
                        this.PanOffset.Y += step;

                        break;

                    case Keys.S:
                        moving = true;
                        this.PanOffset.Y -= step;

                        break;

                    default:
                        moving = false;
                        this.keyDown = Keys.None;
                        break;
                }

                Thread.Sleep(10);
            }
        }

        protected override void OnPaint(PaintEventArgs e)
        {
            base.OnPaint(e);

            Graphics g = e.Graphics;

            g.DrawImage(this.TerrainPic, PanOffset.X, PanOffset.Y, Screen.PrimaryScreen.Bounds.Width * 2, Screen.PrimaryScreen.Bounds.Height * 2);
            

        }

        private void InitializeAreaLayout()
        {

            if (this.Areas[Area].Terrain != null)
            {

                this.TerrainPic = new Bitmap(Terrain.GridWidth * Terrain.TileSize.Width, Terrain.GridHeight * Terrain.TileSize.Width);
                using(Graphics g = Graphics.FromImage(this.TerrainPic))
                {
                    for (int col = 0; col < Terrain.GridWidth; col++)
                    {
                        for (int row = 0; row < Terrain.GridHeight; row++)
                        {

                            int imageIndex = col + Terrain.GridWidth * row;

                            Image image = this.Areas[this.Area].Terrain.Tiles[imageIndex].Pic;
                            g.DrawImage(image, new Point(col * Terrain.TileSize.Width, row * Terrain.TileSize.Width));
                        }
                    }
                }

                if (this.Areas[Area].TravelSigns != null)
                {
                    foreach (Direction direction in this.Areas[Area].TravelSigns.Keys)
                    {
                        Controls.Add(this.Areas[Area].TravelSigns[direction].Pic);
                    }
                }

                if (this.Areas[Area].Structures != null)
                {
                    for (int i = 0; i < this.Areas[Area].Structures.Count; i++)
                    {
                        this.Controls.Add(this.Areas[Area].Structures[i].Pic);
                    }
                }
                if (this.Areas[Area].Items != null)
                {
                    for (int i = 0; i < this.Areas[Area].Items.Count; i++)
                    {
                        this.Controls.Add(this.Areas[Area].Items[i].Pic);
                    }
                }
            }


            if (this.Areas[Area].Enemies != null)
            {
                for (int i = 0; i < this.Areas[Area].Enemies.Count; i++)
                {
                    this.Controls.Add(this.Areas[Area].Enemies[i].Pic);
                }
            }

            if(this.Areas[Area].npcs != null)
            {
                for (int i = 0; i < this.Areas[Area].npcs.Count; i++)
                {
                    this.Controls.Add(this.Areas[Area].npcs[i].Pic);
                    this.Areas[Area].npcs[i].Pic.BringToFront();
                }
            }

            if (player != null)
            {
                this.Controls.Add(player.Pic);
                player.Pic.BringToFront();
            }

        }

        public void AddItemToScreen(Item item)
        {
            Controls.Add(item.Pic);
            this.Areas[Area].Items.Add(item);
        }

        public PictureBox MakePictureBox(Bitmap pic, Point location, Size Size)
        {
            return new PictureBox
            {
                Size = Size,
                Location = location,
                Image = pic,
                SizeMode = PictureBoxSizeMode.StretchImage,
                BackColor = Color.Transparent,
            };
        }

        private void FrmLevel_KeyUp(object sender, KeyEventArgs e)
        {
            this.keyDown = Keys.None;
        }

        private void FrmLevel_KeyDown(object sender, KeyEventArgs e)
        {
            this.keyDown = e.KeyCode;
            switch (e.KeyCode)
            {
                case Keys.E:
                    moving = false;
                    player.ResetMoveSpeed();
                    frminventory = FrmInventory.GetInstance(player, this.Areas[this.Area], PanOffset);
                    frminventory.Show();
                    break;

                case Keys.Escape:
                    moving = false;
                    this.keyDown = Keys.None;
                    FrmSettings frmsettings = new FrmSettings(this);
                    frmsettings.FormClosed += (s, args) => this.Close(); // Handle closure of FrmLevel to close the application
                    frmsettings.Show();
                    this.Hide();
                    break;

                default:
                    break;
            }
        }


        private void tmrUpdateInGameTime_Tick(object sender, EventArgs e)
        {
            TimeSpan span = DateTime.Now - timeBegin;
            string time = span.ToString(@"hh\:mm\:ss");
            lblInGameTime.Text = "Time: " + time.ToString();
        }

        private void tmrPlayerMove_Tick(object sender, EventArgs e)
        {
            if (gameOver)
            {
                return;
            }


            // check collision with Structures
            if (HitAStructure(player))
            {
                this.keyDown = Keys.None;
                this.PanOffset = LastPanOffset;
            }

            if (moving)
            {
                this.LastPanOffset = this.PanOffset;
                for (int i = 0; i < this.Areas[Area].Enemies.Count; i++)
                {
                    this.Areas[Area].Enemies[i].Move(new Point((int)this.Areas[Area].Enemies[i].StartPos.X + this.PanOffset.X, (int)this.Areas[Area].Enemies[i].StartPos.Y + this.PanOffset.Y));
                }

                for (int i = 0; i < this.Areas[Area].Items.Count; i++)
                {
                    this.Areas[Area].Items[i].Move(new Point(this.Areas[Area].Items[i].StartPos.X + this.PanOffset.X, (int)this.Areas[Area].Items[i].StartPos.Y + this.PanOffset.Y));
                }

                for (int i = 0; i < this.Areas[Area].Structures.Count; i++)
                {
                    this.Areas[Area].Structures[i].Move(new Point((int)this.Areas[Area].Structures[i].Position.X + (int)this.PanOffset.X, (int)this.Areas[Area].Structures[i].Position.Y + (int)this.PanOffset.Y));
                }

                for (int i = 0; i < this.Areas[Area].npcs.Count; i++)
                {
                    this.Areas[Area].npcs[i].Move(new Point((int)this.Areas[Area].npcs[i].StartPos.X + (int)this.PanOffset.X, (int)this.Areas[Area].npcs[i].StartPos.Y + (int)this.PanOffset.Y));
                }

                this.Invalidate();
            }




            // check collision with enemies
            int x = hitEnemy(player);
            if (x >= 0)
            {
                this.keyDown = Keys.None;
                Fight(this.Areas[Area].Enemies[x]);


            }

            // check collision with NPCs
            int npcIndex = hitNPC(player);
            if (npcIndex >= 0)
            {
                Converse(this.Areas[Area].npcs[npcIndex]);
                Controls.Remove(this.Areas[Area].npcs[npcIndex].Pic);
                this.Areas[Area].npcs.Remove(this.Areas[Area].npcs[npcIndex]);
            }

            x = HitAnItem(player);
            if (x >= 0)
            {
                if (!player.Inventory.BackpackIsFull())
                {
                    Item item = this.Areas[this.Area].Items[x];
                    this.Areas[this.Area].Items.Remove(item);
                    player.Inventory.AddToBackpack(item);
                    item.HideEntity();
                }

            }

            x = InATile(player);
            if (x >= 0 && x < this.Areas[Area].Terrain.Tiles.Count)
            {
                player.SPEED = (int)this.Areas[Area].Terrain.Tiles[x].Effect;
            }

            this.TravelDirection = InASign(player);
            if (this.TravelDirection != Direction.None)
            {
                TravelLabel.Text = this.Areas[this.Area].TravelSigns[this.TravelDirection].Location;
                TravelButton.Enabled = true;
                SignPanel.Show();
                SignPanel.BringToFront();

            }
            else
            {
                SignPanel.Hide();
                TravelButton.Enabled = false;
            }

        }

        private Direction InASign(Player you)
        {
            foreach (Direction direction in this.Areas[this.Area].TravelSigns.Keys)
            {
                if (you.Collider.Intersects(this.Areas[this.Area].TravelSigns[direction].Collider))
                {
                    return direction;
                }
            }
            return Direction.None;
        }

        private bool HitAStructure(Player c)
        {
            bool hitAWall = false;
            for (int w = 0; w < this.Areas[Area].Structures.Count; w++)
            {
                if (c.Collider.Intersects(this.Areas[Area].Structures[w].Collider))
                {
                    hitAWall = true;
                    break;
                }
            }
            return hitAWall;
        }

        private int hitEnemy(Player you)
        {
            if (this.Areas[Area].Enemies == null)
            {
                return -1;
            }
            int hitChar = -1;
            for (int i = 0; i < this.Areas[Area].Enemies.Count; i++)
            {
                if (this.Areas[Area].Enemies[i] != null)
                {
                    if (you.Collider.Intersects(this.Areas[Area].Enemies[i].Collider))
                    {
                        hitChar = i;
                        break;
                    }
                }
            }
            return hitChar;
        }

        private int hitNPC(Player you)
        {
            if(this.Areas[Area].npcs == null)
            {
                return -1;
            }
            int hitChar = -1;
            for (int i = 0; i < this.Areas[Area].npcs.Count; i++)
            {
                if (this.Areas[Area].npcs[i] != null)
                {
                    if (you.Collider.Intersects(this.Areas[Area].npcs[i].Collider))
                    {
                        hitChar = i;
                        break;
                    }
                }
            }
            return hitChar;
        }

        private int HitAnItem(Player you)
        {
            if (this.Areas[Area].Items == null)
            {
                return -1;
            }
            int hitItem = -1;
            for (int i = 0; i < this.Areas[Area].Items.Count; i++)
            {
                if (this.Areas[Area].Items[i] != null)
                {
                    if (you.Collider.Intersects(this.Areas[Area].Items[i].Collider))
                    {
                        hitItem = i;
                        break;
                    }
                }
            }
            return hitItem;
        }

        public int InATile(Player you)
        {
            int x = ((int)you.Position.x + you.Pic.Width / 2) / Terrain.TileSize.Width;
            int y = ((int)you.Position.y + you.Pic.Height) / Terrain.TileSize.Width;

            int a = (int)((x) + Math.Floor((double)y * Terrain.GridWidth));


            return a;
        }

        private void Fight(Enemy enemy)
        {
            player.ResetMoveSpeed();
            player.MoveBack();
            frmBattleScreen = FrmBattleScreen.GetInstance(this, enemy);
            frmBattleScreen.Show();
        }

        public void RemoveEnemy(Enemy enemy)
        {
            Controls.Remove(enemy.Pic);
            this.Areas[Area].Enemies.Remove(enemy);
            score += 100;
            ScoreLabel.Text = "Score: " + score.ToString();
        }

        private void Converse(NPC npc)
        {
            player.ResetMoveSpeed();
            player.MoveBack();

            // TODO: Insert conversation mechanic here

            if(player.isPartyFull())
            {
                Console.WriteLine("PARTY IS FULL NOTIFICATION");
            }
            else
            {
                player.addPartyMember(npc);
            }
        }

        public void GameOver()
        {
            this.gameOver = true;
            this.player.SetEntityPosition(new Position(-100, -100));


            DisposeLevel();
            DisposeGame();



            // set the location and size of the square 
            // to the location and size of the form
            BlackSquare.Location = this.Location;
            BlackSquare.Size = this.Size;

            BlackSquare.Visible = true;
            BlackSquare.BringToFront();

            // find the x-coordinate to perfectly center the GameOverText
            int centerGameOverText = (this.Width / 2) - (GameOverText.Width / 2);

            GameOverText.Location = new Point(centerGameOverText, 100);
            GameOverText.Visible = true;
            GameOverText.BringToFront();

            // find the x-coordinate to offset the RestartButton from the center so that it's symmetrical
            int centerRestartButton = (this.Width / 2) - (RestartButton.Width / 2);


            RestartButton.Enabled = true;
            RestartButton.Location = new Point(centerRestartButton - 150, 400);
            RestartButton.Size = new Size(100, 30);
            RestartButton.Visible = true;
            RestartButton.BringToFront();

            // find the x-coordinate to offset the ExitButton from the center so that it's symmetrical
            int centerExitButton = (this.Width / 2) - (ExitButton.Width / 2);

            ExitButton.Enabled = true;
            ExitButton.Location = new Point(centerExitButton + 150, 400);
            ExitButton.Size = new Size(100, 30);
            ExitButton.Visible = true;
            ExitButton.BringToFront();

            // find the x-coordinate to put the MainMenuButton in the center
            int centerMainMenuButton = (this.Width / 2) - (MainMenuButton.Width / 2);

            MainMenuButton.Enabled = true;
            MainMenuButton.Location = new Point(centerMainMenuButton, 400);
            MainMenuButton.Size = new Size(100, 30);
            MainMenuButton.Visible = true;
            MainMenuButton.BringToFront();

            RecordLeaderboardData();

            gameAudio.Stop();
        }

        private void RecordLeaderboardData()
        {
            string filepath = "../../data/LeaderboardData.json";
            string[] text = File.ReadAllLines(filepath);

            string playerText = text[0];
            string classText = text[1];
            string scoresText = text[2];
            string weaponText = text[3];
            string armorText = text[4];
            string utilityText = text[5];

            List<String> topPlayers = JsonSerializer.Deserialize<List<String>>(playerText);
            List<String> topClasses = JsonSerializer.Deserialize<List<String>>(classText);
            List<int> topScores = JsonSerializer.Deserialize<List<int>>(scoresText);
            List<String> topWeapons = JsonSerializer.Deserialize<List<String>>(weaponText);
            List<String> topArmors = JsonSerializer.Deserialize<List<String>>(armorText);
            List<String> topUtilities = JsonSerializer.Deserialize<List<String>>(utilityText);


            for (int i = 0; i < topScores.Count; i++)
            {
                if (score > topScores[i])
                {
                    topPlayers.Insert(i, player.Name);
                    topPlayers.RemoveAt(topPlayers.Count - 1);
                    topClasses.Insert(i, player.archetype.name);
                    topClasses.RemoveAt(topClasses.Count - 1);
                    topScores.Insert(i, score);
                    topScores.RemoveAt(topScores.Count - 1);
                    if (player.Inventory.Weapon != null)
                    {
                        topWeapons.Insert(i, player.Inventory.Weapon.Name);
                        topWeapons.RemoveAt(topWeapons.Count - 1);
                    }
                    if (player.Inventory.Armor != null)
                    {
                        topArmors.Insert(i, player.Inventory.Armor.Name);
                        topArmors.RemoveAt(topArmors.Count - 1);
                    }
                    if (player.Inventory.Utility != null)
                    {
                        topUtilities.Insert(i, player.Inventory.Utility.Name);
                        topUtilities.RemoveAt(topUtilities.Count - 1);
                    }
                    break;
                }
            }
            string[] data = new string[9];
            data[0] = JsonSerializer.Serialize(topPlayers);
            data[1] = JsonSerializer.Serialize(topClasses);
            data[2] = JsonSerializer.Serialize(topScores);
            data[3] = JsonSerializer.Serialize(topWeapons);
            data[4] = JsonSerializer.Serialize(topArmors);
            data[5] = JsonSerializer.Serialize(topUtilities);

            File.WriteAllLines(filepath, data);
        }


        private void DisposeLevel()
        {

            foreach (Direction direction in this.Areas[Area].TravelSigns.Keys)
            {
                this.Controls.Remove(this.Areas[Area].TravelSigns[direction].Pic);
            }

            for (int i = 0; i < this.Areas[Area].Enemies.Count; i++)
            {
                this.Controls.Remove(this.Areas[Area].Enemies[i].Pic);
            }

            for (int i = 0; i < this.Areas[Area].npcs.Count; i++)
            {
                this.Controls.Remove(this.Areas[Area].npcs[i].Pic);
            }
            this.Areas[Area].npcs = new List<NPC> { };

            //remove terrain here

            for (int i = 0; i < this.Areas[Area].Items.Count; i++)
            {
                this.Controls.Remove(this.Areas[Area].Items[i].Pic);
            }
            for (int i = 0; i < this.Areas[Area].Structures.Count; i++)
            {
                this.Controls.Remove(this.Areas[Area].Structures[i].Pic);
            }

            GC.Collect();

        }

        private void DisposeGame()
        {
            for (int i = 0; i < this.Areas.Length; i++)
            {
                this.Areas[i].Enemies = new List<Enemy> { };
                this.Areas[i].TravelSigns.Clear();
                this.Areas[i].AdjacentAreas.Clear();
                this.Areas[i].Terrain.Tiles.Clear();
                this.Areas[i].Items.Clear();
                this.Areas[i].Structures.Clear();
            }
            this.Areas = new Area[10];
            GC.Collect();

        }


        private void Menu_Click(object sender, EventArgs e)
        {
            frminventory = FrmInventory.GetInstance(player, this.Areas[this.Area], this.PanOffset);
            frminventory.Show();
        }


        private void RestartButton_Click(object sender, EventArgs e)
        {
            this.score = 0;
            gameAudio.PlayLooping();

            this.Areas[0] = new Area("Malek's Mountain", 12, 0.05);
            this.Areas[1] = new Area("Village Ruins", 901, 0.05);
            this.Areas[2] = new Area("Buddy Beachfront", 890, 0.05);
            this.Areas[3] = new Area("Uphill Hill", 789, 0.05);
            this.Areas[4] = new Area("Plainsfield", 678, 0.05);
            this.Areas[5] = new Area("Lower Harmony Village", 567, 0.05);
            this.Areas[6] = new Area("Windy Plateau", 456, 0.05);
            this.Areas[7] = new Area("Harmony Plains", 345, 0.05);
            this.Areas[8] = new Area("Harmony Village", 234, 0.05);
            this.Areas[9] = new Area("Dragon's Lair", 123, 0.2);

            this.gameOver = false;
            this.player = new Player(player.Name, player.Pic, player.archetype);
            Game.player = this.player;

            BlackSquare.Visible = false;
            GameOverText.Visible = false;
            RestartButton.Visible = false;
            ExitButton.Visible = false;
            MainMenuButton.Visible = false;

            RestartButton.Enabled = false;
            ExitButton.Enabled = false;
            MainMenuButton.Enabled = false;

            this.Area = 4;
            AreaSelect();
            InitializeAreaLayout();
        }

        private void MainMenuButton_Click(object sender, EventArgs e)
        {
            Game.player = null;
            this.player = null;
            MainMenu.Show();
            FrmMain mainMenu = Application.OpenForms["FrmMain"] as FrmMain;
            mainMenu.mainMenuPlayer = new SoundPlayer(Resources.Mainmenu_audio);
            mainMenu.mainMenuPlayer.PlayLooping();
            this.gameAudio.Dispose();
            this.Hide();
        }

        private void ExitButton_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void AreaSelect()
        {
            switch (this.Area)
            {
                case 0:
                    Area0();
                    break;
                case 1:
                    Area1();
                    break;
                case 2:
                    Area2();
                    break;
                case 3:
                    Area3();
                    break;
                case 4:
                    Area4();
                    break;
                case 5:
                    Area5();
                    break;
                case 6:
                    Area6();
                    break;
                case 7:
                    Area7();
                    break;
                case 8:
                    Area8();
                    break;
                case 9:
                    AreaBoss();
                    break;
                default:
                    Area4();
                    break;
            }
        }

        private void AreaBoss()
        {

            if (this.Areas[9].Visited)
            {
                return;
            }
            this.Areas[this.Area].Visited = true;



            player.Pic.Visible = true;
        }

        private void Area8()
        {

            if (this.Areas[8].Visited)
            {
                return;
            }
            this.Areas[this.Area].Visited = true;

            player.Pic.Visible = true;


            Area currentArea = this.Areas[this.Area];

            Bitmap house_long = Resources.house_long;

            Bitmap house_long_rot = Resources.house_long;
            house_long_rot.RotateFlip(RotateFlipType.Rotate90FlipNone);

            Bitmap house_L = Resources.house_L;

            Bitmap house_L_rot90 = Resources.house_L;
            house_L_rot90.RotateFlip(RotateFlipType.Rotate90FlipNone);

            Bitmap house_L_rot180 = Resources.house_L;
            house_L_rot180.RotateFlip(RotateFlipType.Rotate180FlipNone);

            Size longSize = new Size(Terrain.TileSize.Width * 6, Terrain.TileSize.Height * 3);
            Size longSize_rot = new Size(longSize.Height, longSize.Width);
            Size L_Size = new Size(Terrain.TileSize.Width * 6, Terrain.TileSize.Height * 4);
            Size L_Size_rot90 = new Size(L_Size.Height, L_Size.Width);

            currentArea.AddStructure(new Structure(MakePictureBox(house_long_rot, new Point(Screen.PrimaryScreen.Bounds.Width * 3 / 4 - 100 , - 100), longSize_rot)));
            currentArea.AddStructure(new Structure(MakePictureBox(house_long, new Point(Screen.PrimaryScreen.Bounds.Width / 2, 400), longSize)));
            currentArea.AddStructure(new Structure(MakePictureBox(house_L, new Point(Screen.PrimaryScreen.Bounds.Width * 3 / 4, 300), L_Size)));
            currentArea.AddStructure(new Structure(MakePictureBox(house_L_rot180, new Point(Screen.PrimaryScreen.Bounds.Width * 1 / 4, 80), L_Size)));


        }

        private void Area7()
        {

            if (this.Areas[7].Visited)
            {
                return;
            }
            this.Areas[this.Area].Visited = true;



            player.Pic.Visible = true;
        }

        private void Area6()
        {

            if (this.Areas[6].Visited)
            {
                return;
            }
            this.Areas[this.Area].Visited = true;



            player.Pic.Visible = true;
        }

        private void Area5()
        {

            if (this.Areas[5].Visited)
            {
                return;
            }
            this.Areas[this.Area].Visited = true;



            player.Pic.Visible = true;
        }



        private void Area4()
        {
            // leave this here, default case when this.Area is unset.
            this.Area = 4;

            if (this.Areas[4].Visited)
            {
                return;
            }
            this.Areas[this.Area].Visited = true;



            Area currentArea = this.Areas[4];


            player.SetEntityPosition(new Position(Screen.PrimaryScreen.Bounds.Width / 2 - player.Pic.Width / 2, Screen.PrimaryScreen.Bounds.Height / 2 - player.Pic.Height));
            player.Pic.Visible = true;


            currentArea.AddItem(new Item("Sting", MakePictureBox(Resources.common_dagger, new Point(300, 200), itemSize), 5, Item.ItemType.Weapon));
            currentArea.AddItem(new Item("Lesser Heal", MakePictureBox(Resources.lesser_health_potion, new Point(500, 300), itemSize), 5, Item.ItemType.Utility, Item.PotionTypes.Healing));
            currentArea.AddItem(new Item("Armor of Noob", MakePictureBox(Resources.common_armor, new Point(880, 800), itemSize), 5, Item.ItemType.Armor));
            currentArea.AddItem(new Item("Potion of Speed", MakePictureBox(Resources.speed_potion, new Point(20, 400), itemSize), 10, Item.ItemType.Utility, Item.PotionTypes.Speed));

            currentArea.AddEnemy(new Enemy("Poison Packet", MakePictureBox(Resources.enemy_poisonpacket, new Point(200, 500), new Size(100, 100)), new Minion()));
            currentArea.AddEnemy(new Enemy("Cheeto", MakePictureBox(Resources.enemy_cheetos, new Point(600, 200), new Size(75, 125)), new Minion()));
            currentArea.AddEnemy(new Enemy("BossKoolAid", MakePictureBox(Resources.enemy_koolaid, new Point(this.Width - 200, 100), new Size(150, 150)), new Boss()));

            currentArea.AddNPC(new NPC("Harold", MakePictureBox(Resources.harold, new Point(150, 150), new Size(75, 100)), new Healer()));
            currentArea.AddStructure(new Structure(MakePictureBox(Resources.wall_bricks, new Point(500, 500), new Size(20, 100))));
            currentArea.Structures[0].Pic.Image.RotateFlip(RotateFlipType.Rotate90FlipNone);
        }

        private void Area3()
        {

            if (this.Areas[3].Visited)
            {
                return;
            }
            this.Areas[this.Area].Visited = true;



            player.Pic.Visible = true;


        }

        private void Area2()
        {
            if (this.Areas[this.Area].Visited)
            {
                return;
            }
            this.Areas[this.Area].Visited = true;

            player.Pic.Visible = true;
        }

        private void Area1()
        {

            if (this.Areas[this.Area].Visited)
            {
                return;
            }

            this.Areas[this.Area].Visited = true;


            player.Pic.Visible = true;
        }

        private void Area0()
        {

            if (this.Areas[this.Area].Visited)
            {
                return;
            }
            this.Areas[this.Area].Visited = true;


            player.Pic.Visible = true;
        }


        private void setAdjacency(int area)
        {
            int up = area > 5 ? -1 : area + 3;
            int down = area < 3 ? -1 : area - 3;
            int left = area % 3 == 0 ? -1 : area - 1;
            int right = area % 3 == 2 ? -1 : area + 1;

            if (up >= 0)
            {
                this.Areas[area].SetAdjacentArea(Direction.Up, up);
                this.Areas[area].SetTravelSign(Direction.Up, new TravelSign(this.Areas[up].AreaName, MakePictureBox(Resources.travel_sign, new Point(Screen.PrimaryScreen.Bounds.Width / 2 - this.signSize.Width / 2, 10), this.signSize)));
            }
            if (down >= 0)
            {
                this.Areas[area].SetAdjacentArea(Direction.Down, down);
                this.Areas[area].SetTravelSign(Direction.Down, new TravelSign(this.Areas[down].AreaName, MakePictureBox(Resources.travel_sign, new Point(Screen.PrimaryScreen.Bounds.Width / 2 - this.signSize.Width / 2, Screen.PrimaryScreen.Bounds.Height - 80 - this.signSize.Height), this.signSize)));

            }
            if (left >= 0)
            {
                this.Areas[area].SetAdjacentArea(Direction.Left, left);
                this.Areas[area].SetTravelSign(Direction.Left, new TravelSign(this.Areas[left].AreaName, MakePictureBox(Resources.travel_sign, new Point(10, Screen.PrimaryScreen.Bounds.Height / 2 - this.signSize.Height / 2), this.signSize)));

            }
            if (right >= 0)
            {
                this.Areas[area].SetAdjacentArea(Direction.Right, right);
                this.Areas[area].SetTravelSign(Direction.Right, new TravelSign(this.Areas[right].AreaName, MakePictureBox(Resources.travel_sign, new Point(Screen.PrimaryScreen.Bounds.Width - 10 - this.signSize.Width, Screen.PrimaryScreen.Bounds.Height / 2 - this.signSize.Height / 2), this.signSize)));

            }
        }

        private void TravelButton_Click(object sender, EventArgs e)
        {
            DisposeLevel();
            this.Area = this.Areas[this.Area].AdjacentAreas[this.TravelDirection];

            switch (this.TravelDirection)
            {
                case Direction.Up:
                    player.SetEntityPosition(new Position(Screen.PrimaryScreen.Bounds.Width / 2 - player.Pic.Width / 2, Screen.PrimaryScreen.Bounds.Height - signSize.Height - player.Pic.Height  - 100));
                    break;
                case Direction.Down:
                    player.SetEntityPosition(new Position(Screen.PrimaryScreen.Bounds.Width / 2 - player.Pic.Width / 2, signSize.Height + 20));
                    break;
                case Direction.Right:
                    player.SetEntityPosition(new Position(signSize.Width + 10, Screen.PrimaryScreen.Bounds.Height / 2 - player.Pic.Height / 2));
                    break;
                case Direction.Left:
                    player.SetEntityPosition(new Position(Screen.PrimaryScreen.Bounds.Width - signSize.Width - 20 - player.Pic.Width, Screen.PrimaryScreen.Bounds.Height / 2 - player.Pic.Height / 2));
                    break;

                default:
                    player.SetEntityPosition(new Position(Screen.PrimaryScreen.Bounds.Width / 2 - player.Pic.Width, Screen.PrimaryScreen.Bounds.Height - player.Pic.Height));
                    break;
            }

            AreaSelect();


            InitializeAreaLayout();
            Game.player = player;

        }
    }
}
