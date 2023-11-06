using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.IO;
using System.Text.Json;
using System.Xml.Schema;
using Fall2020_CSC403_Project.code;
using Fall2020_CSC403_Project.Properties;

namespace Fall2020_CSC403_Project
{
    public partial class FrmLeaderboard : Form
    {
        public Form MainMenu;
        private Label PreviousRanking;
        private PictureBox PlayerPic;
        private PictureBox BackgroundImg;
        private PictureBox Weapon;
        private PictureBox Armor;
        private PictureBox Utility;
        private int height;
        private int width;

        private List<String> topPlayers;
        private List<String> topClasses;
        private List<int> topScores;
        private List<String> topWeapons;
        private List<String> topArmors;
        private List<String> topUtilities;
        public FrmLeaderboard()
        {
            this.WindowState = FormWindowState.Maximized;
            this.MainMenu = Application.OpenForms["FrmMain"];
            InitializeComponent();
            this.Setup();
        }

        private void Setup()
        {
            height = Screen.PrimaryScreen.Bounds.Height;
            width = Screen.PrimaryScreen.Bounds.Width;

            // Set up background
            BackgroundImg = new PictureBox();
            this.Controls.Add(BackgroundImg);
            BackgroundImg.Image = Resources.BackgroundForRPG3;
            BackgroundImg.SizeMode = PictureBoxSizeMode.StretchImage;
            BackgroundImg.Size = new Size(width, height);

            // Get leaderboard data from LeaderboardData.json and put the rankings into lists
            string filepath = "../../data/LeaderboardData.json";
            string[] text = File.ReadAllLines(filepath);

            string playerText = text[0];
            string classText = text[1];
            string scoresText = text[2];
            string weaponText = text[3];
            string armorText = text[4];
            string utilityText = text[5];

            topPlayers = JsonSerializer.Deserialize<List<String>>(playerText);
            topClasses = JsonSerializer.Deserialize<List<String>>(classText);
            topScores = JsonSerializer.Deserialize<List<int>>(scoresText);
            topWeapons = JsonSerializer.Deserialize<List<String>>(weaponText);
            topArmors = JsonSerializer.Deserialize<List<String>>(armorText);
            topUtilities = JsonSerializer.Deserialize<List<String>>(utilityText);

            // Create the headers for the leaderboard chart using a Label
            new Label
            {
                Location = new Point(0 + (7 * width / 16), 0),
                Text = String.Format("{0,-8} {1,-10} {2,-10} {3,-6}", "Rank", "Name", "Class", "Score"),
                Parent = BackgroundImg,
                Size = new Size(9 * width / 16, height / 15),
                Font = new Font("NSimSun", height / 25),
                BackColor = Color.FromArgb(64, Color.White),
                ForeColor = Color.Black,
            };

            // Create Labels to hold the rankings on the right side of the screen.
            Label Ranking;
            for (int i = 0; i < topPlayers.Count; i++)
            {
                if (topPlayers[i] != null)
                {
                    Ranking = new Label
                    {
                        Location = new Point(0 + (7 * width / 16), 0 + ((i + 1) * height / 15)),
                        Text = String.Format("{0,-8} {1,-10} {2,-10} {3,-6}", (i + 1), topPlayers[i], topClasses[i], topScores[i]),
                        Parent = BackgroundImg,
                        Size = new Size(9 * width / 16, height / 15),
                        Font = new Font("NSimSun", height / 25),
                        BackColor = Color.FromArgb(64, Color.White),
                        ForeColor = Color.Black,
                    };
                    Ranking.Click += Ranking_Click;
                }
            }

            // Create Label with instructions
            new Label
            {
                Location = new Point(0 + (width / 18), 0 + (8 * height / 32)),
                Text = "Click the rankings for more details on each playthrough",
                Parent = BackgroundImg,
                Size = new Size(3 * width / 8, 2 * height / 6),
                Font = new Font("NSimSun", height / 20),
                BackColor = Color.Transparent,
                ForeColor = Color.Black,
                BorderStyle = BorderStyle.None,
            };

            // Add Buttons For Start, Settings and Exit
            Button ExitButton = new Button();

            ExitButton.Location = new Point(0 + (width / 18), 0 + (7 * height / 9));
            ExitButton.Parent = BackgroundImg;
            ExitButton.Size = new Size(width / 3, height / 10);
            ExitButton.Text = ("Exit");
            ExitButton.Font = new Font("NSimSun", ExitButton.Size.Height / 2);
            ExitButton.Click += ExitButton_Click;


            //Button ClearLeaderboard = new Button
            //{
            //    Location = new Point(0 + (width / 18), 0 + (7 * height / 9)),
            //    Parent = BackgroundImg,
            //    Size = new Size(width / 3, height / 10),
            //    Text = ("Main Menu"),
            //    Font = new Font("NSimSun", Size.Height / 2),
            //};
            //MainMenuButton.Click += MainMenuButton_Click;
        }

        private void Ranking_Click(object sender, EventArgs e)
        {
            Label CurrentRanking = (Label)sender;

            if (CurrentRanking != PreviousRanking)
            {
                CurrentRanking.BackColor = Color.FromArgb(128, Color.White);
                if (PreviousRanking != null)
                {
                    PreviousRanking.BackColor = Color.FromArgb(64, Color.White);
                    this.HidePlaythroughInfo();
                }
                PreviousRanking = CurrentRanking;
                string[] rankingInfo = CurrentRanking.Text.Split(' ');
                int ranking = int.Parse(rankingInfo[0]) - 1;
                this.ShowPlaythroughInfo(ranking);
            }else
            {
                PreviousRanking.BackColor = Color.FromArgb(64, Color.White);
                PreviousRanking = null;
                this.HidePlaythroughInfo();
            }
        }

        private void ShowPlaythroughInfo(int ranking)
        {
            
            string classType = topClasses[ranking];
            string weaponType = topWeapons[ranking];
            string armorType = topArmors[ranking];
            string utilityType = topUtilities[ranking];

            Controls.Add(Weapon);
            Controls.Add(Armor);
            Controls.Add(Utility);
            Controls.Add(PlayerPic);

            Weapon = new PictureBox()
            {
                Location = new Point(0, 0),
                Size = new Size(3 * Height / 128 + width / 8,  3 * Height / 128 + width / 8),
                SizeMode = PictureBoxSizeMode.StretchImage,
                Parent = BackgroundImg,
                BackColor = Color.FromArgb(128, Color.DimGray),
                BorderStyle = BorderStyle.FixedSingle,
            };
            Weapon.BringToFront();
            
            Armor = new PictureBox
            {
                Location = new Point(0, Weapon.Location.Y + Weapon.Height),
                Size = new Size(3 * Height / 128 + width / 8, 3 * Height / 128 +  width / 8),
                SizeMode = PictureBoxSizeMode.StretchImage,
                Parent = BackgroundImg,
                BackColor = Color.FromArgb(128, Color.DimGray),
                BorderStyle = BorderStyle.FixedSingle,
            };
            Armor.BringToFront();

            Utility = new PictureBox
            {
                Location = new Point(0, Armor.Location.Y + Armor.Height),
                Size = new Size(3 * Height / 128 + width / 8, 3 * Height / 128 + width / 8),
                SizeMode = PictureBoxSizeMode.StretchImage,
                Parent = BackgroundImg,
                BackColor = Color.FromArgb(128, Color.DimGray),
                BorderStyle = BorderStyle.FixedSingle,
            };
            Utility.BringToFront();

            PlayerPic = new PictureBox
            {
                Parent = BackgroundImg,
                BackColor = Color.FromArgb(128, Color.DimGray),
                Location = new Point(Weapon.Location.X + Weapon.Width, Weapon.Location.Y),
                Size = new Size(4 + 19 * width / 64, 3 * (3 * Height / 128 + width / 8)),
                SizeMode = PictureBoxSizeMode.StretchImage,
            };
            PlayerPic.BringToFront();

            switch (classType)
            {
                case "Tank":
                    PlayerPic.Image = Resources.tank;
                    break;
                case "Rogue":
                    PlayerPic.Image = Resources.rogue;
                    break;
                case "Swordsman":
                    PlayerPic.Image = Resources.swordsman;
                    break;
                default:
                    break;
            }

            switch (weaponType)
            {
                case "Sting":
                    Weapon.Image = Resources.common_dagger;
                    break;
                default:
                    break;
            }

            switch (armorType)
            {
                case "Armor of Noob":
                    Armor.Image = Resources.common_armor;
                    break;
                default:
                    break;
            }

            switch (utilityType)
            {
                case "Health Potion":
                    Utility.Image = Resources.lesser_health_potion;
                    break;
                case "Potion of Speed":
                    Utility.Image = Resources.speed_potion;
                    break;
                default:
                    break;
            }
        }

        private void HidePlaythroughInfo()
        {
            Controls.Remove(Weapon);
            Controls.Remove(Armor);
            Controls.Remove(Utility);
            Controls.Remove(PlayerPic);
            Weapon.Dispose();
            Armor.Dispose();
            Utility.Dispose();
            PlayerPic.Dispose();
        }

        private void ExitButton_Click(object sender, EventArgs e)
        {
            MainMenu.Show();
            this.Hide();
        }
    }
}
