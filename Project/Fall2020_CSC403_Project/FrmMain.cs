using Fall2020_CSC403_Project.Properties;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Drawing.Text;
using System.Linq;
using System.Media;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Fall2020_CSC403_Project
{
    public partial class FrmMain : Form
    {
        public SoundPlayer mainMenuPlayer;
        public FrmMain()
        {
            this.WindowState = FormWindowState.Maximized;
            InitializeComponent();
            this.setup();
        }

        private void setup()
        {
            int height = Screen.PrimaryScreen.Bounds.Height;
            int width = Screen.PrimaryScreen.Bounds.Width;
            // Set up background
            PictureBox BackgroundImg = new PictureBox();
            this.Controls.Add(BackgroundImg);
            BackgroundImg.Image = Properties.Resources.BackgroundForRPG3;
            BackgroundImg.SizeMode = PictureBoxSizeMode.StretchImage;
            BackgroundImg.Size = new Size(width, height);

            // Add Buttons For Start, Settings and Exit
            Button NewGameButton = new Button();
            Button LoadGameButton = new Button();
            Button SettingsButton = new Button();
            Button LeaderboardButton = new Button();
            Button ExitButton = new Button();

            NewGameButton.Location = new Point(0 + (width / 18), 0 + ((4 * height) / 10));
            LoadGameButton.Location = new Point(0 + (width / 18), 0 + ((5 * height) / 10));
            LeaderboardButton.Location = new Point(0 + (width / 18), 0 + (6 * height / 10));
            SettingsButton.Location = new Point(0 + (width / 18), 0 + ((7 * height) / 10));
            ExitButton.Location = new Point(0 + (width / 18), 0 + (8 * height / 10));
            

            NewGameButton.Parent = BackgroundImg;
            LoadGameButton.Parent = BackgroundImg;
            LeaderboardButton.Parent = BackgroundImg;
            SettingsButton.Parent = BackgroundImg;
            ExitButton.Parent = BackgroundImg;

            NewGameButton.Size = new Size(width / 3, height / 11);
            LoadGameButton.Size = new Size(width / 3, height / 11);
            LeaderboardButton.Size = new Size(width / 3, height / 11);
            SettingsButton.Size = new Size(width / 3, height / 11);
            ExitButton.Size = new Size(width / 3, height / 11);

            NewGameButton.Text = ("New Game");
            LoadGameButton.Text = ("Load Game");
            LeaderboardButton.Text = ("Leaderboard");
            SettingsButton.Text = ("Settings");
            ExitButton.Text = ("Quit Game");

            NewGameButton.Font = new Font("NSimSun", NewGameButton.Size.Height / 2);
            LoadGameButton.Font = new Font("NSimSun", LoadGameButton.Size.Height / 2);
            LeaderboardButton.Font = new Font("NSimSun", LeaderboardButton.Size.Height / 2);
            SettingsButton.Font = new Font("NSimSun", SettingsButton.Size.Height / 2);
            ExitButton.Font = new Font("NSimSun", ExitButton.Size.Height / 2);

            NewGameButton.Click += NewGameButton_Click;
            LoadGameButton.Click += LoadGameButton_Click;
            LeaderboardButton.Click += LeaderboardButton_Click;
            SettingsButton.Click += SettingsButton_Click;
            ExitButton.Click += ExitButton_Click;


            // Add Title
            PictureBox TitleImage = new PictureBox();
            TitleImage.Image = Properties.Resources.Title;
            TitleImage.Parent = BackgroundImg;
            TitleImage.Size = new Size(width / 3, height / 3);
            TitleImage.SizeMode = PictureBoxSizeMode.StretchImage;
            TitleImage.SizeMode = PictureBoxSizeMode.StretchImage;
            TitleImage.Location = new Point(0 + (width / 18), 0 + (height / 11));
            TitleImage.BackColor = Color.Transparent;

            // Add Instructions
            PictureBox InstructionImage = new PictureBox();
            InstructionImage.Image = Properties.Resources.Instrucitons;
            InstructionImage.Parent = BackgroundImg;
            InstructionImage.Size = new Size(width / 3, (6 * height) / 8);
            InstructionImage.Location = new Point(0 + (width / 2), 0 + (height / 8));
            InstructionImage.BackColor = Color.Transparent;
            InstructionImage.SizeMode = PictureBoxSizeMode.StretchImage;

            //Playing Mainmenu_audio here
            SoundPlayer mainMenuPlayer = new SoundPlayer(Resources.Mainmenu_audio);
            mainMenuPlayer.PlayLooping();
        }

        private void NewGameButton_Click(object sender, EventArgs e)
        {
            FrmPlayerSelect frmplayerselect = new FrmPlayerSelect(this);
            frmplayerselect.FormClosed += (s, args) => this.Close(); // Handle closure of FrmLevel to close the application
            frmplayerselect.Show();
            this.Hide(); // Hide the FrmMain form
        }
        private void LoadGameButton_Click(object sender, EventArgs e)
        {
            FrmLoad frmload = new FrmLoad(this);
            frmload.FormClosed += (s, args) => this.Close(); // Handle closure of FrmLoad to close the application
            frmload.Show();
            this.Hide(); // Hide the FrmMain form
        }
        private void LeaderboardButton_Click(object sender, EventArgs e)
        {
            FrmLeaderboard leaderboard = new FrmLeaderboard();
            leaderboard.FormClosed += (s, args) => this.Close(); // Handle closure of FrmLeaderboard to close the application
            leaderboard.Show();
            this.Hide(); // Hide the FrmMain form
        }
        private void SettingsButton_Click(object sender, EventArgs e)
        {
            FrmSettings frmsettings = new FrmSettings(this);
            frmsettings.FormClosed += (s, args) => this.Close(); 
            frmsettings.Show();
            this.Hide(); // Hide the FrmMain form
        }
        private void ExitButton_Click(object sender, EventArgs e)
        {
            this.Close();
        }

    }
}
