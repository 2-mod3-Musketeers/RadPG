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
using Fall2020_CSC403_Project.Properties;
using Fall2020_CSC403_Project.code;

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
        private Label[] Rankings;

        private List<String> topPlayers;
        private List<String> topClasses;
        private List<int> topScores;
        private List<String> topWeapons;
        private List<String> topArmors;
        private List<String> topUtilities;
        public FrmLeaderboard()
        {
            this.FormBorderStyle = FormBorderStyle.None;
            this.WindowState = FormWindowState.Maximized;
            this.Bounds = Screen.PrimaryScreen.Bounds;

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
                Text = String.Format("{0,-12} {1,-12} {2,-12}", "Score", "Name", "Class"),
                Parent = BackgroundImg,
                Size = new Size(9 * width / 16, height / 15),
                Font = new Font("NSimSun", height / 25),
                BackColor = Color.FromArgb(64, Color.White),
                ForeColor = Color.Black,
            };

            // Create Labels to hold the rankings on the right side of the screen.
            Rankings = new Label[10];
            for (int i = 0; i < topPlayers.Count; i++)
            {
                if (topPlayers[i] != null)
                {
                    Rankings[i] = new Label
                    {
                        Location = new Point(0 + (7 * width / 16), 0 + ((i + 1) * height / 15)),
                        Text = String.Format("{0,-12} {1,-12} {2,-12}", topScores[i], topPlayers[i], topClasses[i]),
                        Parent = BackgroundImg,
                        Size = new Size(9 * width / 16, height / 15),
                        Font = new Font("NSimSun", height / 25),
                        BackColor = Color.FromArgb(64, Color.White),
                        ForeColor = Color.Black,
                    };
                    Rankings[i].Click += Ranking_Click;
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
            Button MainMenuButton = new Button();
            MainMenuButton.Location = new Point(0 + (width / 18), 0 + (7 * height / 9));
            MainMenuButton.Parent = BackgroundImg;
            MainMenuButton.Size = new Size(width / 3, height / 10);
            MainMenuButton.Text = ("Main Menu");
            MainMenuButton.Font = new Font("NSimSun", MainMenuButton.Size.Height / 2);
            MainMenuButton.Click += MainMenuButton_Click;

            // Create Button to clear the ranking
            Button ClearRankingsButton = new Button();
            ClearRankingsButton.Parent = BackgroundImg;
            ClearRankingsButton.Size = new Size(width / 3, height / 10);
            ClearRankingsButton.Location = new Point(17 * (width / 18) - ClearRankingsButton.Size.Width, 0 + (7 * height / 9));
            ClearRankingsButton.Text = ("Clear Rankings");
            ClearRankingsButton.Font = new Font("NSimSun", ClearRankingsButton.Size.Height / 2);
            ClearRankingsButton.Click += ClearRankingsButton_Click;
        }

        private void Ranking_Click(object sender, EventArgs e)
        {
            Label CurrentRanking = (Label)sender;

            // Turn the ranking more opaque if it is clicked
            // Change it back to original opaqueness if clicked again
            // Display playthrough info when clicked
            // Hide playthrough info when clicked again
            if (CurrentRanking != PreviousRanking)
            {
                CurrentRanking.BackColor = Color.FromArgb(128, Color.White);
                if (PreviousRanking != null)
                {
                    PreviousRanking.BackColor = Color.FromArgb(64, Color.White);
                    this.HidePlaythroughInfo();
                }
                PreviousRanking = CurrentRanking;
                // get which ranking it is based on its position on the page.
                int ranking = (int)(CurrentRanking.Location.Y * ((float)15 / Height)) - 1;
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
            // Get the class, weapon, armor, and utility used in the playthrough            
            string classType = topClasses[ranking];
            string weaponType = topWeapons[ranking];
            string armorType = topArmors[ranking];
            string utilityType = topUtilities[ranking];

            Controls.Add(Weapon);
            Controls.Add(Armor);
            Controls.Add(Utility);
            Controls.Add(PlayerPic);

            // Display a picture of the weapon
            Weapon = new PictureBox()
            {
                Location = new Point(0, 0),
                Size = new Size(11 * height / 45,  11 * height / 45),
                SizeMode = PictureBoxSizeMode.StretchImage,
                Parent = BackgroundImg,
                BackColor = Color.FromArgb(128, Color.DimGray),
                BorderStyle = BorderStyle.FixedSingle,
            };
            Weapon.BringToFront();

            // Display a picture of the armor
            Armor = new PictureBox
            {
                Location = new Point(0, Weapon.Location.Y + Weapon.Height),
                Size = new Size(11 * height / 45, 11 * height / 45),
                SizeMode = PictureBoxSizeMode.StretchImage,
                Parent = BackgroundImg,
                BackColor = Color.FromArgb(128, Color.DimGray),
                BorderStyle = BorderStyle.FixedSingle,
            };
            Armor.BringToFront();

            // Display a picture of the utility
            Utility = new PictureBox
            {
                Location = new Point(0, Armor.Location.Y + Armor.Height),
                Size = new Size(11 * height / 45, 11 * height / 45),
                SizeMode = PictureBoxSizeMode.StretchImage,
                Parent = BackgroundImg,
                BackColor = Color.FromArgb(128, Color.DimGray),
                BorderStyle = BorderStyle.FixedSingle,
            };
            Utility.BringToFront();

            // Display a picture of the class
            PlayerPic = new PictureBox
            {
                Parent = BackgroundImg,
                BackColor = Color.FromArgb(128, Color.DimGray),
                Location = new Point(Weapon.Location.X + Weapon.Width, Weapon.Location.Y),
                Size = new Size((7 * width / 16) - 11 * height / 45, 11 * height / 15),
                SizeMode = PictureBoxSizeMode.StretchImage,
            };
            PlayerPic.BringToFront();

            // Set the pictures for the class, weapon, armor, and utility
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


            if (weaponType != null)
            {
                Weapon.Image = Game.Items[weaponType].Pic.Image;

            }
            if (armorType != null)
            {
                Armor.Image = Game.Items[armorType].Pic.Image;
            }

            if (utilityType != null)
            {
                Utility.Image = Game.Items[utilityType].Pic.Image;
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

        private void MainMenuButton_Click(object sender, EventArgs e)
        {
            MainMenu.Show();
            this.Hide();
        }

        private void ClearRankingsButton_Click(object sender, EventArgs e)
        {
            string filepath = "../../data/LeaderboardData.json";

            // Set each item in the list to null or its equivalent
            for (int i = 0; i < topPlayers.Count; i++)
            {
                topPlayers[i] = null;
                topClasses[i] = null;
                topScores[i] = -1;
                topWeapons[i] = null;
                topArmors[i] = null;
                topUtilities[i] = null;
            }

            // Convert the lists to a JSON format
            string[] data = new string[6];
            data[0] = JsonSerializer.Serialize(topPlayers);
            data[1] = JsonSerializer.Serialize(topClasses);
            data[2] = JsonSerializer.Serialize(topScores);
            data[3] = JsonSerializer.Serialize(topWeapons);
            data[4] = JsonSerializer.Serialize(topArmors);
            data[5] = JsonSerializer.Serialize(topUtilities);

            // Overwrite the current leaderboard data with the new lists
            File.WriteAllLines(filepath, data);

            // Remove the pictures from the screen
            if (Weapon != null)
            {
                Weapon.Dispose();
                Armor.Dispose();
                Utility.Dispose();
                PlayerPic.Dispose();
            }

            // Remove the rankings from the screen
            foreach (Label Ranking in Rankings)
            {
                if (Ranking != null)
                {
                    Ranking.Dispose();
                }
            }
        }
    }
}
