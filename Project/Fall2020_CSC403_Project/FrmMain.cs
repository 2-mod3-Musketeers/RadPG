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
            Button StartButton = new Button();
            Button SettingsButton = new Button();
            Button LeaderboardButton = new Button();
            Button ExitButton = new Button();

            StartButton.Location = new Point(0 + (width / 18), 0 + ((4 * height) / 9));
            SettingsButton.Location = new Point(0 + (width / 18), 0 + ((5 * height) / 9));
            LeaderboardButton.Location = new Point(0 + (width / 18), 0 + (6 * height / 9));
            ExitButton.Location = new Point(0 + (width / 18), 0 + (7 * height / 9));
            

            StartButton.Parent = BackgroundImg;
            SettingsButton.Parent = BackgroundImg;
            LeaderboardButton.Parent = BackgroundImg;
            ExitButton.Parent = BackgroundImg;

            StartButton.Size = new Size(width / 3, height / 10);
            SettingsButton.Size = new Size(width / 3, height / 10);
            LeaderboardButton.Size = new Size(width / 3, height / 10);
            ExitButton.Size = new Size(width / 3, height / 10);

            StartButton.Text = ("Start Game");
            SettingsButton.Text = ("Settings");
            LeaderboardButton.Text = ("Leaderboard");
            ExitButton.Text = ("Quit Game");

            StartButton.Font = new Font("NSimSun", StartButton.Size.Height / 2);
            SettingsButton.Font = new Font("NSimSun", SettingsButton.Size.Height / 2);
            LeaderboardButton.Font = new Font("NSimSun", ExitButton.Size.Height / 2);
            ExitButton.Font = new Font("NSimSun", ExitButton.Size.Height / 2);

            StartButton.Click += StartButton_Click;
            SettingsButton.Click += SettingsButton_Click;
            LeaderboardButton.Click += LeaderboardButton_Click;
            ExitButton.Click += ExitButton_Click;


            // Add Title
            PictureBox TitleImage = new PictureBox();
            TitleImage.Image = Properties.Resources.Title;
            TitleImage.Parent = BackgroundImg;
            TitleImage.Size = new Size(width / 3, height / 3);
            TitleImage.SizeMode = PictureBoxSizeMode.StretchImage;
            TitleImage.SizeMode = PictureBoxSizeMode.StretchImage;
            TitleImage.Location = new Point(0 + (width / 18), 0 + (height / 8));
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
        
        private void ExitButton_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void SettingsButton_Click(object sender, EventArgs e)
        {
            FrmSettings frmsettings = new FrmSettings(this);
            frmsettings.FormClosed += (s, args) => this.Close(); 
            frmsettings.Show();
            this.Hide(); // Hide the FrmMain form
        }

        private void StartButton_Click(object sender, EventArgs e)
        {
            FrmPlayerSelect frmplayerselect = new FrmPlayerSelect(this);
            frmplayerselect.FormClosed += (s, args) => this.Close(); // Handle closure of FrmLevel to close the application
            frmplayerselect.Show();
            this.Hide(); // Hide the FrmMain form
        }

        private void LeaderboardButton_Click(object sender, EventArgs e)
        {
            FrmLeaderboard leaderboard = new FrmLeaderboard();
            leaderboard.FormClosed += (s, args) => this.Close(); // Handle closure of FrmLeaderboard to close the application
            leaderboard.Show();
            this.Hide(); // Hide the FrmMain form
        }

    }
}
