using Fall2020_CSC403_Project.code;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Timers;
using System.Windows.Forms;
using System.Xml.Serialization;

namespace Fall2020_CSC403_Project
{
    public partial class FrmBattleScreen : Form
    {
        public static FrmBattleScreen instance = null;
        private Enemy enemy;
        private Player player;
        private List<Character> attackOrder;
        private FrmLevel form;

        private PictureBox UtilityPicture;
        private Label UtilityLabel;

        private Label playerHealthMax;
        private Label playerCurrentHealth;

        private Label enemyHealthMax;
        private Label enemyCurrentHealth;

        private Label partyZeroMax;
        private Label partyZeroCurrent;
        private Label partyOneMax;
        private Label partyOneCurrent;
        private Label partyTwoMax;
        private Label partyTwoCurrent;
        private Label partyThreeMax;
        private Label partyThreeCurrent;
        private Label partyZeroName;
        private Label partyOneName;
        private Label partyTwoName;
        private Label partyThreeName;

        private Button AttackButton;
        private Button FleeButton;
        private Button UseButton;


        private String[] BattleLog = new string[10];
        
        public static FrmLevel frmLevel;

        private Label backlog;
        private Label[] loglabels;
        private Label[] loggradients;


        private PictureBox picPlayer;
        private PictureBox picEnemy;
        private Label PlayerName;


        private int height = Screen.PrimaryScreen.Bounds.Height;
        private int width = Screen.PrimaryScreen.Bounds.Width;

        public FrmBattleScreen(FrmLevel level)
        {
            this.FormBorderStyle = FormBorderStyle.None;
            this.WindowState = FormWindowState.Maximized;
            this.Bounds = Screen.PrimaryScreen.Bounds;

            InitializeComponent();
            form = level;
            player = Game.player;
        }

        public static FrmBattleScreen GetInstance(FrmLevel level, Enemy enemy)
        {
            frmLevel = level;
            instance = new FrmBattleScreen(level);
            instance.enemy = enemy;
            instance.Setup();
            return instance;

        }

        public void Setup()
        {

            this.BackColor = Color.SlateGray;
            //this.ControlBox = false;

            // Set up player
            picPlayer = new PictureBox();
            picPlayer.Parent = this;
            picPlayer.Size = new Size(width / 5 + width / 128, 3 * width / 10 + 2 * Height / 32);
            picPlayer.SizeMode = PictureBoxSizeMode.StretchImage;
            picPlayer.Location = new Point(width / 7, 24 * height / 128);
            picPlayer.BackColor = this.BackColor;
            picPlayer.Image = player.Pic.Image;

            // Set up Enemy
            picEnemy = new PictureBox();
            picEnemy.Parent= this;
            picEnemy.Size = new Size(width / 5 + width / 128, 3 * width / 10 + 2 * Height / 32);
            picEnemy.SizeMode = PictureBoxSizeMode.StretchImage;
            picEnemy.Location = new Point(3*width / 4 - picPlayer.Location.X/2, 24*height/128);
            picEnemy.BackColor = this.BackColor;
            picEnemy.Image = enemy.Pic.Image;


            // Add Utility Picture 
            UtilityPicture = new PictureBox();
            UtilityPicture.Parent = this;
            UtilityPicture.Size = new Size(width / 12, width / 12);
            UtilityPicture.SizeMode = PictureBoxSizeMode.StretchImage;
            UtilityPicture.BackColor = this.BackColor;
            UtilityPicture.Location = new Point(width / 32, width / 32);
            if (player.Inventory.Utility != null)
            {
                UtilityPicture.Image = player.Inventory.Utility.Pic.Image;
                UtilityPicture.BorderStyle = BorderStyle.FixedSingle;
            }

            // Add utility label if one is equipped
            if (player.Inventory.Utility != null)
            {
                UtilityLabel = new Label();
                UtilityLabel.Parent = this;
                UtilityLabel.AutoSize = false;
                UtilityLabel.Size = new Size(UtilityPicture.Width, UtilityPicture.Height / 6);
                UtilityLabel.Location = new Point(UtilityPicture.Location.X, UtilityPicture.Location.Y + UtilityPicture.Size.Height);
                UtilityLabel.Text = "Equipped Item";
                UtilityLabel.Font = new Font("NSimSun", UtilityLabel.Size.Height / 2);
                UtilityLabel.BackColor = this.BackColor;
                UtilityLabel.TextAlign = ContentAlignment.MiddleCenter;
            }

            // Add user name and enemy name
            Label EnemyName = new Label();
            PlayerName = new Label();

            // Set up enemy name label
            EnemyName.Parent = this;
            EnemyName.AutoSize = false;
            EnemyName.BackColor = this.BackColor;
            EnemyName.Text = enemy.Name.ToString();
            EnemyName.Size = new Size(picEnemy.Size.Width, EnemyName.Size.Width / 2);
            EnemyName.Location = new Point(picEnemy.Location.X, picEnemy.Location.Y - 2*EnemyName.Size.Height);
            EnemyName.TextAlign = ContentAlignment.MiddleCenter;
            EnemyName.Font = new Font("NSimSun", EnemyName.Size.Height / 2);

            // set up player name label
            PlayerName.Parent = this;
            PlayerName.AutoSize = false;
            PlayerName.BackColor = this.BackColor;
            PlayerName.Text = player.Name.ToString();
            PlayerName.Size = new Size(picPlayer.Size.Width, PlayerName.Size.Width / 2);
            PlayerName.Location = new Point(picPlayer.Location.X, picPlayer.Location.Y - 2 * PlayerName.Size.Height);
            PlayerName.TextAlign = ContentAlignment.MiddleCenter;
            PlayerName.Font = new Font("NSimSun", PlayerName.Size.Height / 2);

            // Add character Health Bar
            playerHealthMax = new Label();
            playerCurrentHealth = new Label();

            playerCurrentHealth.Size = new Size(picPlayer.Width, height / 32);
            playerCurrentHealth.Parent = this;
            playerCurrentHealth.Location = new Point(picPlayer.Location.X, picPlayer.Location.Y - 3 * height / 64);
            playerCurrentHealth.Font = new Font("NSimSun", 7*playerCurrentHealth.Size.Height / 8);
            playerCurrentHealth.TextAlign = ContentAlignment.MiddleCenter;
            playerCurrentHealth.BackColor = Color.Green;
            playerCurrentHealth.AutoSize = false;

            playerHealthMax.Size = new Size(picPlayer.Width, height / 32);
            playerHealthMax.Parent = this;
            playerHealthMax.Location = new Point(picPlayer.Location.X, picPlayer.Location.Y - 3 * height / 64);
            playerHealthMax.Font = new Font("NSimSun", 7*playerCurrentHealth.Size.Height / 8);
            playerHealthMax.BackColor = Color.Red;
            playerHealthMax.AutoSize = false;

            // Add Enemy Health Bar
            enemyHealthMax = new Label();
            enemyCurrentHealth = new Label();

            enemyCurrentHealth.Size = new Size(picEnemy.Width, height / 32);
            enemyCurrentHealth.Parent = this;
            enemyCurrentHealth.Location = new Point(picEnemy.Location.X, picEnemy.Location.Y - 3 * height / 64);
            enemyCurrentHealth.Font = new Font("NSimSun", 7*enemyCurrentHealth.Size.Height / 8);
            enemyCurrentHealth.TextAlign = ContentAlignment.MiddleCenter;
            enemyCurrentHealth.BackColor = Color.Green;
            enemyCurrentHealth.AutoSize = false;

            enemyHealthMax.Size = new Size(picEnemy.Width, height / 32);
            enemyHealthMax.Parent = this;
            enemyHealthMax.Location = new Point(picEnemy.Location.X, picEnemy.Location.Y - 3 * height / 64);
            enemyHealthMax.Font = new Font("NSimSun", 7*enemyCurrentHealth.Size.Height / 8);
            enemyHealthMax.BackColor = Color.Red;
            enemyHealthMax.AutoSize = false;

            if (player.PartyCount() > 0) 
            { 
                picPlayer.Hide(); playerCurrentHealth.Hide(); playerHealthMax.Hide(); PlayerName.Hide(); 
            }

            partyZeroMax = new Label();
            partyZeroCurrent = new Label();
            partyOneMax = new Label();
            partyOneCurrent = new Label();
            partyTwoMax = new Label();
            partyTwoCurrent = new Label();
            partyThreeMax = new Label();
            partyThreeCurrent = new Label();

            if (player.PartyCount() == 1)
            {
                GetPartyForm1();
            }
            else if (player.PartyCount() == 2)
            {
                GetPartyForm2();
            }
            else if (player.PartyCount() == 3)
            { 
                GetPartyForm3();
            }

            // Add log labels to screen
            backlog = new Label();
            loglabels = new Label[BattleLog.Count()];
            backlog.BackColor = Color.FromArgb(60, 70, 80);
            backlog.Size = new Size(picPlayer.Width,10 * height / 32);
            backlog.Parent = this;
            backlog.AutoSize = false;

            if(enemy.archetype.opener != null)
            {
                BattleLog[0] = enemy.archetype.opener;
            }

            this.attackOrder = new List<Character>();

            FillAttackOrder(player.Party, enemy);
            AddLogLabels();
            updateLog();

            backlog.Location = new Point(picEnemy.Left - picPlayer.Right / 2 - backlog.Width / 2, picPlayer.Location.Y + picPlayer.Height - 10*loglabels[0].Size.Height - picPlayer.Height/4);

            // Set up attack button
            AttackButton = new Button();
            AttackButton.Parent = this;
            AttackButton.Size = new Size(picPlayer.Size.Width / 3, AttackButton.Size.Width / 2);
            AttackButton.Location = new Point(backlog.Location.X + picPlayer.Width / 3, picPlayer.Location.Y + picPlayer.Size.Height + AttackButton.Size.Height / 2);
            AttackButton.Font = new Font("NSimSun", AttackButton.Size.Height / 2);
            AttackButton.Text = "Attack";
            AttackButton.BackColor = Color.DarkRed;
            AttackButton.ForeColor = Color.White;
            AttackButton.FlatStyle = FlatStyle.Flat;
            AttackButton.FlatAppearance.BorderColor = Color.Black;
            AttackButton.FlatAppearance.BorderSize = 1;
            AttackButton.Click += AttackButton_Click;

            // Set up flee button
            FleeButton = new Button();
            FleeButton.Parent = this;
            FleeButton.Size = AttackButton.Size;
            FleeButton.Location = new Point(backlog.Location.X + picPlayer.Width / 3, AttackButton.Location.Y + AttackButton.Size.Height + AttackButton.Size.Height / 2);
            FleeButton.Font = new Font("NSimSun", AttackButton.Size.Height / 2);
            FleeButton.Text = "Flee";
            FleeButton.BackColor = Color.DarkGoldenrod;
            FleeButton.ForeColor = Color.White;
            FleeButton.FlatStyle = FlatStyle.Flat;
            FleeButton.FlatAppearance.BorderColor = Color.Black;
            FleeButton.FlatAppearance.BorderSize = 1;
            FleeButton.Click += FleeButton_Click;

            // Set up UseUtility button
            UseButton = new Button();
            UseButton.Parent = this;
            UseButton.Size = AttackButton.Size;
            UseButton.Location = new Point(backlog.Location.X + picPlayer.Width / 3, FleeButton.Location.Y + FleeButton.Size.Height + FleeButton.Size.Height / 2);
            UseButton.Font = new Font("NSimSun", AttackButton.Size.Height / 2);
            UseButton.Text = "Use Utility";
            UseButton.BackColor = Color.DarkBlue;
            UseButton.ForeColor = Color.White;
            UseButton.FlatStyle = FlatStyle.Flat;
            UseButton.FlatAppearance.BorderColor = Color.Black;
            UseButton.FlatAppearance.BorderSize = 1;
            UseButton.Click += UseButton_Click;

            UpdateHealthBars();

            // Observer pattern
            for (int i = 0; i < this.attackOrder.Count(); i++)
            {
                this.attackOrder[i].AttackEvent += CharacterDamage;
            }
            
            UpdateHealthBars();
        }

        private void AddLogLabels()
        {
            for (int i = 0; i < BattleLog.Count(); i++)
            {
                loglabels[i] = new Label();
                loglabels[i].Parent = this;
                loglabels[i].Text = BattleLog[i]; // Assuming BattleLog is a list of strings
                loglabels[i].AutoSize = false;
                loglabels[i].TextAlign = ContentAlignment.MiddleCenter;
                loglabels[i].Size = new Size(picPlayer.Width, height / 32);
                loglabels[i].Font = new Font("NSimSun", 7 * loglabels[i].Size.Height / 8);
                loglabels[i].BackColor = Color.FromArgb(60, 70, 80);

                // Get the background color
                Color backgroundColor = loglabels[i].BackColor;

                // Interpolate between white and the background color to create a gradient
                double interpolationFactor = (double)(BattleLog.Length-i) / (BattleLog.Count()*(4/3));
                int red = (int)Math.Round(255 * interpolationFactor + (1 - interpolationFactor) * backgroundColor.R);
                int green = (int)Math.Round(255 * interpolationFactor + (1 - interpolationFactor) * backgroundColor.G);
                int blue = (int)Math.Round(255 * interpolationFactor + (1 - interpolationFactor) * backgroundColor.B);

                Color gradientColor = Color.FromArgb(red, green, blue);
                loglabels[i].ForeColor = gradientColor;

                if (i == 0)
                {
                    loglabels[i].Location = new Point(picEnemy.Left - picPlayer.Right / 2 - loglabels[i].Width / 2, picPlayer.Location.Y + picPlayer.Height - loglabels[i].Size.Height);
                }
                else
                {
                    loglabels[i].Location = new Point(loglabels[i - 1].Location.X, loglabels[i - 1].Location.Y - loglabels[i].Height);
                }
                if (BattleLog[i] != null)
                {
                    loglabels[i].Text = BattleLog[i];
                    Game.FontSizing(loglabels[i]);
                }
            }
        }

        private void GetPartyForm1()
        {
            PictureBox partyZero = new PictureBox();
            PictureBox partyOne = new PictureBox();
            PictureBox partyTwo = new PictureBox();

            int h_scale = picPlayer.Height / 3;
            int w_scale = picPlayer.Width / 3;

            partyZero.Parent = this;
            partyZero.Size = new Size(2 * picPlayer.Width / 3, 2 * picPlayer.Height / 3);
            partyZero.SizeMode = PictureBoxSizeMode.StretchImage;
            partyZero.Location = new Point(picPlayer.Location.X + picPlayer.Width / 6, picPlayer.Location.Y);
            partyZero.BackColor = this.BackColor;
            partyZero.Image = player.Pic.Image;

            partyOne.Parent = this;
            partyOne.Size = new Size(w_scale, h_scale);
            partyOne.SizeMode = PictureBoxSizeMode.StretchImage;
            partyOne.Location = new Point(partyZero.Left + partyZero.Width / 2 - partyOne.Width/2, partyZero.Bottom + height / 64);
            partyOne.BackColor = this.BackColor;
            partyOne.Image = player.Party[0].Pic.Image;
            


            // Add health bars

            partyZeroCurrent.Size = new Size(partyZero.Width, height / 32);
            partyZeroCurrent.Parent = this;
            partyZeroCurrent.Location = new Point(partyZero.Location.X, partyZero.Location.Y - 3 * height / 64);
            partyZeroCurrent.Font = new Font("NSimSun", 7 * partyZeroCurrent.Size.Height / 8);
            partyZeroCurrent.TextAlign = ContentAlignment.MiddleCenter;
            partyZeroCurrent.BackColor = Color.Green;
            partyZeroCurrent.AutoSize = false;

            partyZeroMax.Size = new Size(partyZero.Width, height / 32);
            partyZeroMax.Parent = this;
            partyZeroMax.Location = new Point(partyZero.Location.X, partyZero.Location.Y - 3 * height / 64);
            partyZeroMax.Font = new Font("NSimSun", 7 * partyZeroCurrent.Size.Height / 8);
            partyZeroMax.BackColor = Color.Red;
            partyZeroMax.AutoSize = false;


            partyOneCurrent.Size = new Size(partyOne.Width, height / 32);
            partyOneCurrent.Parent = this;
            partyOneCurrent.Location = new Point(partyOne.Location.X, partyOne.Bottom);
            partyOneCurrent.Font = new Font("NSimSun", 7 * partyOneCurrent.Size.Height / 8);
            partyOneCurrent.TextAlign = ContentAlignment.MiddleCenter;
            partyOneCurrent.BackColor = Color.Green;
            partyOneCurrent.AutoSize = false;

            partyOneMax.Size = new Size(partyOne.Width, height / 32);
            partyOneMax.Parent = this;
            partyOneMax.Location = new Point(partyOne.Location.X, partyOne.Bottom);
            partyOneMax.Font = new Font("NSimSun", 7 * partyOneCurrent.Size.Height / 8);
            partyOneMax.BackColor = Color.Red;
            partyOneMax.AutoSize = false;

            // Set up names
            partyZeroName = new Label();
            partyOneName = new Label();

            partyZeroName.Parent = this;
            partyZeroName.AutoSize = false;
            partyZeroName.BackColor = this.BackColor;
            partyZeroName.Text = player.Name.ToString();
            partyZeroName.Size = new Size(partyZero.Size.Width, partyZeroName.Size.Width / 2);
            partyZeroName.Location = new Point(partyZero.Location.X, partyZero.Location.Y - 2 * partyZeroName.Size.Height);
            partyZeroName.TextAlign = ContentAlignment.MiddleCenter;
            partyZeroName.Font = new Font("NSimSun", partyZeroName.Size.Height / 2);

            partyOneName.Parent = this;
            partyOneName.AutoSize = false;
            partyOneName.BackColor = this.BackColor;
            partyOneName.Text = player.Party[0].Name.ToString();
            partyOneName.Size = new Size(partyOne.Size.Width, partyOneName.Size.Width / 3);
            partyOneName.Location = new Point(partyOne.Location.X, partyOne.Bottom + partyOneName.Size.Height);
            partyOneName.TextAlign = ContentAlignment.MiddleCenter;
            partyOneName.Font = new Font("NSimSun", partyOneName.Size.Height / 2);


        }
        private void GetPartyForm2()
        {
            PictureBox partyZero = new PictureBox();
            PictureBox partyOne = new PictureBox();
            PictureBox partyTwo = new PictureBox();

            int h_scale = picPlayer.Height / 3;
            int w_scale = picPlayer.Width / 3;

            partyZero.Parent = this;
            partyZero.Size = new Size(2 * picPlayer.Width / 3, 2 * picPlayer.Height / 3);
            partyZero.SizeMode = PictureBoxSizeMode.StretchImage;
            partyZero.Location = new Point(picPlayer.Location.X + picPlayer.Width / 6, picPlayer.Location.Y);
            partyZero.BackColor = this.BackColor;
            partyZero.Image = player.Pic.Image;

            partyOne.Parent = this;
            partyOne.Size = new Size(w_scale, h_scale);
            partyOne.SizeMode = PictureBoxSizeMode.StretchImage;
            partyOne.Location = new Point(partyZero.Left - partyOne.Width / 2, partyZero.Bottom + height / 64);
            partyOne.BackColor = this.BackColor;
            partyOne.Image = player.Party[0].Pic.Image;

            partyTwo.Parent = this;
            partyTwo.Size = new Size(w_scale, h_scale);
            partyTwo.SizeMode = PictureBoxSizeMode.StretchImage;
            partyTwo.Location = new Point(partyZero.Right - partyTwo.Width / 2, partyZero.Bottom + height / 64);
            partyTwo.BackColor = this.BackColor;
            partyTwo.Image = player.Party[1].Pic.Image;


            // Add health bars

            partyZeroCurrent.Size = new Size(partyZero.Width, height / 32);
            partyZeroCurrent.Parent = this;
            partyZeroCurrent.Location = new Point(partyZero.Location.X, partyZero.Location.Y - 3 * height / 64);
            partyZeroCurrent.Font = new Font("NSimSun", 7 * partyZeroCurrent.Size.Height / 8);
            partyZeroCurrent.TextAlign = ContentAlignment.MiddleCenter;
            partyZeroCurrent.BackColor = Color.Green;
            partyZeroCurrent.AutoSize = false;

            partyZeroMax.Size = new Size(partyZero.Width, height / 32);
            partyZeroMax.Parent = this;
            partyZeroMax.Location = new Point(partyZero.Location.X, partyZero.Location.Y - 3 * height / 64);
            partyZeroMax.Font = new Font("NSimSun", 7 * partyZeroCurrent.Size.Height / 8);
            partyZeroMax.BackColor = Color.Red;
            partyZeroMax.AutoSize = false;


            partyOneCurrent.Size = new Size(partyOne.Width, height / 32);
            partyOneCurrent.Parent = this;
            partyOneCurrent.Location = new Point(partyOne.Location.X, partyOne.Bottom);
            partyOneCurrent.Font = new Font("NSimSun", 7 * partyOneCurrent.Size.Height / 8);
            partyOneCurrent.TextAlign = ContentAlignment.MiddleCenter;
            partyOneCurrent.BackColor = Color.Green;
            partyOneCurrent.AutoSize = false;

            partyOneMax.Size = new Size(partyOne.Width, height / 32);
            partyOneMax.Parent = this;
            partyOneMax.Location = new Point(partyOne.Location.X, partyOne.Bottom);
            partyOneMax.Font = new Font("NSimSun", 7 * partyOneCurrent.Size.Height / 8);
            partyOneMax.BackColor = Color.Red;
            partyOneMax.AutoSize = false;

            partyTwoCurrent.Size = new Size(partyTwo.Width, height / 32);
            partyTwoCurrent.Parent = this;
            partyTwoCurrent.Location = new Point(partyTwo.Location.X, partyTwo.Bottom);
            partyTwoCurrent.Font = new Font("NSimSun", 7 * partyTwoCurrent.Size.Height / 8);
            partyTwoCurrent.TextAlign = ContentAlignment.MiddleCenter;
            partyTwoCurrent.BackColor = Color.Green;
            partyTwoCurrent.AutoSize = false;

            partyTwoMax.Size = new Size(partyTwo.Width, height / 32);
            partyTwoMax.Parent = this;
            partyTwoMax.Location = new Point(partyTwo.Location.X, partyTwo.Bottom);
            partyTwoMax.Font = new Font("NSimSun", 7 * partyTwoCurrent.Size.Height / 8);
            partyTwoMax.BackColor = Color.Red;
            partyTwoMax.AutoSize = false;

            // Set up names
            partyZeroName = new Label();
            partyOneName = new Label();
            partyTwoName = new Label();

            partyZeroName.Parent = this;
            partyZeroName.AutoSize = false;
            partyZeroName.BackColor = this.BackColor;
            partyZeroName.Text = player.Name.ToString();
            partyZeroName.Size = new Size(partyZero.Size.Width, partyZeroName.Size.Width / 2);
            partyZeroName.Location = new Point(partyZero.Location.X, partyZero.Location.Y - 2 * partyZeroName.Size.Height);
            partyZeroName.TextAlign = ContentAlignment.MiddleCenter;
            partyZeroName.Font = new Font("NSimSun", partyZeroName.Size.Height / 2);

            partyOneName.Parent = this;
            partyOneName.AutoSize = false;
            partyOneName.BackColor = this.BackColor;
            partyOneName.Text = player.Party[0].Name.ToString();
            partyOneName.Size = new Size(partyOne.Size.Width, partyOneName.Size.Width / 3);
            partyOneName.Location = new Point(partyOne.Location.X, partyOne.Bottom + partyOneName.Size.Height);
            partyOneName.TextAlign = ContentAlignment.MiddleCenter;
            partyOneName.Font = new Font("NSimSun", partyOneName.Size.Height / 2);

            partyTwoName.Parent = this;
            partyTwoName.AutoSize = false;
            partyTwoName.BackColor = this.BackColor;
            partyTwoName.Text = player.Party[1].Name.ToString();
            partyTwoName.Size = new Size(partyTwo.Size.Width, partyTwoName.Size.Width / 3);
            partyTwoName.Location = new Point(partyTwo.Location.X, partyTwo.Bottom + partyTwoName.Size.Height);
            partyTwoName.TextAlign = ContentAlignment.MiddleCenter;
            partyTwoName.Font = new Font("NSimSun", partyTwoName.Size.Height / 2);

        }
        private void GetPartyForm3()
        {
            PictureBox partyZero = new PictureBox();
            PictureBox partyOne = new PictureBox();
            PictureBox partyTwo = new PictureBox();
            PictureBox partyThree = new PictureBox();

            int h_scale = picPlayer.Height / 3;
            int w_scale = picPlayer.Width / 3;

            partyZero.Parent = this;
            partyZero.Size = new Size(2*picPlayer.Width/3, 2*picPlayer.Height/3);
            partyZero.SizeMode = PictureBoxSizeMode.StretchImage;
            partyZero.Location = new Point(picPlayer.Location.X+picPlayer.Width/6, picPlayer.Location.Y);
            partyZero.BackColor = this.BackColor;
            partyZero.Image = player.Pic.Image;

            partyOne.Parent = this;
            partyOne.Size = new Size(w_scale, h_scale);
            partyOne.SizeMode = PictureBoxSizeMode.StretchImage;
            partyOne.Location = new Point(partyZero.Left - partyOne.Width/2 - width/64, partyZero.Bottom + height/64);
            partyOne.BackColor = this.BackColor;
            partyOne.Image = player.Party[0].Pic.Image;

            partyTwo.Parent = this;
            partyTwo.Size = new Size(w_scale, h_scale);
            partyTwo.SizeMode = PictureBoxSizeMode.StretchImage;
            partyTwo.Location = new Point(partyZero.Left + partyZero.Width/2 - partyTwo.Width / 2, partyZero.Bottom + height / 64);
            partyTwo.BackColor = this.BackColor;
            partyTwo.Image = player.Party[1].Pic.Image;

            partyThree.Parent = this;
            partyThree.Size = new Size(w_scale, h_scale);
            partyThree.SizeMode = PictureBoxSizeMode.StretchImage;
            partyThree.Location = new Point(partyZero.Right - partyOne.Width / 2 + width/64, partyZero.Bottom + height / 64);
            partyThree.BackColor = this.BackColor;
            partyThree.Image = player.Party[2].Pic.Image;


            // Add health bars


            partyZeroCurrent.Size = new Size(partyZero.Width, height / 32);
            partyZeroCurrent.Parent = this;
            partyZeroCurrent.Location = new Point(partyZero.Location.X, partyZero.Location.Y - 3 * height / 64);
            partyZeroCurrent.Font = new Font("NSimSun", 7 * partyZeroCurrent.Size.Height / 8);
            partyZeroCurrent.TextAlign = ContentAlignment.MiddleCenter;
            partyZeroCurrent.BackColor = Color.Green;
            partyZeroCurrent.AutoSize = false;

            PlayerName.Size = new Size(partyZero.Size.Width, PlayerName.Size.Width / 2);
            PlayerName.Location = new Point(partyZero.Location.X, partyZero.Location.Y - 2 * PlayerName.Size.Height);

            partyZeroMax.Size = new Size(partyZero.Width, height / 32);
            partyZeroMax.Parent = this;
            partyZeroMax.Location = new Point(partyZero.Location.X, partyZero.Location.Y - 3 * height / 64);
            partyZeroMax.Font = new Font("NSimSun", 7 * partyZeroCurrent.Size.Height / 8);
            partyZeroMax.BackColor = Color.Red;
            partyZeroMax.AutoSize = false;          

            partyOneCurrent.Size = new Size(partyOne.Width, height / 32);
            partyOneCurrent.Parent = this;
            partyOneCurrent.Location = new Point(partyOne.Location.X, partyOne.Bottom);
            partyOneCurrent.Font = new Font("NSimSun", 7 * partyOneCurrent.Size.Height / 8);
            partyOneCurrent.TextAlign = ContentAlignment.MiddleCenter;
            partyOneCurrent.BackColor = Color.Green;
            partyOneCurrent.AutoSize = false;

            partyOneMax.Size = new Size(partyOne.Width, height / 32);
            partyOneMax.Parent = this;
            partyOneMax.Location = new Point(partyOne.Location.X, partyOne.Bottom);
            partyOneMax.Font = new Font("NSimSun", 7 * partyOneCurrent.Size.Height / 8);
            partyOneMax.BackColor = Color.Red;
            partyOneMax.AutoSize = false;

            partyTwoCurrent.Size = new Size(partyTwo.Width, height / 32);
            partyTwoCurrent.Parent = this;
            partyTwoCurrent.Location = new Point(partyTwo.Location.X, partyTwo.Bottom);
            partyTwoCurrent.Font = new Font("NSimSun", 7 * partyTwoCurrent.Size.Height / 8);
            partyTwoCurrent.TextAlign = ContentAlignment.MiddleCenter;
            partyTwoCurrent.BackColor = Color.Green;
            partyTwoCurrent.AutoSize = false;

            partyTwoMax.Size = new Size(partyTwo.Width, height / 32);
            partyTwoMax.Parent = this;
            partyTwoMax.Location = new Point(partyTwo.Location.X, partyTwo.Bottom);
            partyTwoMax.Font = new Font("NSimSun", 7 * partyTwoCurrent.Size.Height / 8);
            partyTwoMax.BackColor = Color.Red;
            partyTwoMax.AutoSize = false;

            partyThreeCurrent.Size = new Size(partyThree.Width, height / 32);
            partyThreeCurrent.Parent = this;
            partyThreeCurrent.Location = new Point(partyThree.Location.X, partyThree.Bottom);
            partyThreeCurrent.Font = new Font("NSimSun", 7 * partyThreeCurrent.Size.Height / 8);
            partyThreeCurrent.TextAlign = ContentAlignment.MiddleCenter;
            partyThreeCurrent.BackColor = Color.Green;
            partyThreeCurrent.AutoSize = false;

            partyThreeMax.Size = new Size(partyTwo.Width, height / 32);
            partyThreeMax.Parent = this;
            partyThreeMax.Location = new Point(partyThree.Location.X, partyThree.Bottom);
            partyThreeMax.Font = new Font("NSimSun", 7 * partyThreeCurrent.Size.Height / 8);
            partyThreeMax.BackColor = Color.Red;
            partyThreeMax.AutoSize = false;


            // Set up names
            partyZeroName = new Label();
            partyOneName = new Label();
            partyTwoName = new Label();
            partyThreeName = new Label();

            partyZeroName.Parent = this;
            partyZeroName.AutoSize = false;
            partyZeroName.BackColor = this.BackColor;
            partyZeroName.Text = player.Name.ToString();
            partyZeroName.Size = new Size(partyZero.Size.Width, partyZeroName.Size.Width / 2);
            partyZeroName.Location = new Point(partyZero.Location.X, partyZero.Location.Y - 2 * partyZeroName.Size.Height);
            partyZeroName.TextAlign = ContentAlignment.MiddleCenter;
            partyZeroName.Font = new Font("NSimSun", partyZeroName.Size.Height / 2);

            partyOneName.Parent = this;
            partyOneName.AutoSize = false;
            partyOneName.BackColor = this.BackColor;
            partyOneName.Text = player.Party[0].Name.ToString();
            partyOneName.Size = new Size(partyOne.Size.Width, partyOneName.Size.Width / 3);
            partyOneName.Location = new Point(partyOne.Location.X, partyOne.Bottom + partyOneName.Size.Height);
            partyOneName.TextAlign = ContentAlignment.MiddleCenter;
            partyOneName.Font = new Font("NSimSun", partyOneName.Size.Height / 2);

            partyTwoName.Parent = this;
            partyTwoName.AutoSize = false;
            partyTwoName.BackColor = this.BackColor;
            partyTwoName.Text = player.Party[1].Name.ToString();
            partyTwoName.Size = new Size(partyTwo.Size.Width, partyTwoName.Size.Width / 3);
            partyTwoName.Location = new Point(partyTwo.Location.X, partyTwo.Bottom + partyTwoName.Size.Height);
            partyTwoName.TextAlign = ContentAlignment.MiddleCenter;
            partyTwoName.Font = new Font("NSimSun", partyTwoName.Size.Height / 2);

            partyThreeName.Parent = this;
            partyThreeName.AutoSize = false;
            partyThreeName.BackColor = this.BackColor;
            partyThreeName.Text = player.Party[2].Name.ToString();
            partyThreeName.Size = new Size(partyThree.Size.Width, partyThreeName.Size.Width / 3);
            partyThreeName.Location = new Point(partyThree.Location.X, partyThree.Bottom + partyThreeName.Size.Height);
            partyThreeName.TextAlign = ContentAlignment.MiddleCenter;
            partyThreeName.Font = new Font("NSimSun", partyThreeName.Size.Height / 2);

        }


        private void updateLog()
        {
            for (int i = 0; i < BattleLog.Count(); i++)
            {
                if (BattleLog[i] != null)
                {
                    loglabels[i].Text = BattleLog[i];
                    Game.FontSizing(loglabels[i]);
                }
                loglabels[i].BringToFront();
            }
        }


        private void UpdateHealthBars()
        {
            // for party sizes:
            if (player.PartyCount() == 0)
            {
                // for player
                float playerHealthPer = player.Health / (float)player.MaxHealth;
                int MAX_HEALTHBAR_WIDTH = playerHealthMax.Width;
                playerCurrentHealth.BackColor = Color.Green;
                playerCurrentHealth.Width = (int)(MAX_HEALTHBAR_WIDTH * playerHealthPer);
                playerCurrentHealth.Text = player.Health.ToString();
            }
            if (player.PartyCount() == 1)
            {
                float partyZeroHealth = player.Health / (float)player.MaxHealth;
                int MAX_PARTY_ZERO = partyZeroMax.Width;
                partyZeroCurrent.BackColor = Color.Green;
                partyZeroCurrent.Width = (int)(MAX_PARTY_ZERO * partyZeroHealth);
                partyZeroCurrent.Text = player.Health.ToString();

                float partyOneHealth = player.Party[0].Health / (float)player.Party[0].MaxHealth;
                int MAX_PARTY_ONE = partyOneMax.Width;
                partyOneCurrent.BackColor = Color.Green;
                partyOneCurrent.Width = (int)(MAX_PARTY_ONE * partyOneHealth);
                partyOneCurrent.Text = player.Party[0].Health.ToString();
            }
            else if (player.PartyCount() == 2)
            {
                float partyZeroHealth = player.Health / (float)player.MaxHealth;
                int MAX_PARTY_ZERO = partyZeroMax.Width;
                partyZeroCurrent.BackColor = Color.Green;
                partyZeroCurrent.Width = (int)(MAX_PARTY_ZERO * partyZeroHealth);
                partyZeroCurrent.Text = player.Health.ToString();

                float partyOneHealth = player.Party[0].Health / (float)player.Party[0].MaxHealth;
                int MAX_PARTY_ONE = partyOneMax.Width;
                partyOneCurrent.BackColor = Color.Green;
                partyOneCurrent.Width = (int)(MAX_PARTY_ONE * partyOneHealth);
                partyOneCurrent.Text = player.Party[0].Health.ToString();

                float partyTwoHealth = player.Party[1].Health / (float)player.Party[1].MaxHealth;
                int MAX_PARTY_TWO = partyTwoMax.Width;
                partyTwoCurrent.BackColor = Color.Green;
                partyTwoCurrent.Width = (int)(MAX_PARTY_TWO * partyTwoHealth);
                partyTwoCurrent.Text = player.Party[1].Health.ToString();
            }
            else if (player.PartyCount() == 3)
            {
                float partyZeroHealth = player.Health / (float)player.MaxHealth;
                int MAX_PARTY_ZERO = partyZeroMax.Width;
                partyZeroCurrent.BackColor = Color.Green;
                partyZeroCurrent.Width = (int)(MAX_PARTY_ZERO * partyZeroHealth);
                partyZeroCurrent.Text = player.Health.ToString();

                float partyOneHealth = player.Party[0].Health / (float)player.Party[0].MaxHealth;
                int MAX_PARTY_ONE = partyOneMax.Width;
                partyOneCurrent.BackColor = Color.Green;
                partyOneCurrent.Width = (int)(MAX_PARTY_ONE * partyOneHealth);
                partyOneCurrent.Text = player.Party[0].Health.ToString();

                float partyTwoHealth = player.Party[1].Health / (float)player.Party[1].MaxHealth;
                int MAX_PARTY_TWO = partyTwoMax.Width;
                partyTwoCurrent.BackColor = Color.Green;
                partyTwoCurrent.Width = (int)(MAX_PARTY_TWO * partyTwoHealth);
                partyTwoCurrent.Text = player.Party[1].Health.ToString();

                float partyThreeHealth = player.Party[2].Health / (float)player.Party[2].MaxHealth;
                int MAX_PARTY_THREE = partyThreeMax.Width;
                partyThreeCurrent.BackColor = Color.Green;
                partyThreeCurrent.Width = (int)(MAX_PARTY_THREE * partyThreeHealth);
                partyThreeCurrent.Text = player.Party[2].Health.ToString();
            }
            
            // for enemy
            float enemyHealthPer = enemy.Health / (float)enemy.MaxHealth;
            int ENEMY_MAX_HEALTHBAR_WIDTH = enemyHealthMax.Width;
            enemyCurrentHealth.BackColor = Color.Green;
            enemyCurrentHealth.Width = (int)(ENEMY_MAX_HEALTHBAR_WIDTH * enemyHealthPer);
            enemyCurrentHealth.Text = enemy.Health.ToString();
        }

        private void UseButton_Click(object sender, EventArgs e)
        {
            if (player.Inventory.Utility != null)
            {
                player.ApplyEffect(player.Inventory.Utility.Potion, player.Inventory.Utility.Stat);
                player.Inventory.UseItem();
                UtilityPicture.Hide();
                UtilityLabel.Hide();
                UpdateHealthBars();
            }            
        }

        private void FleeButton_Click(object sender, EventArgs e)
        {
            frmLevel.UpdateHealthBars(frmLevel.playerCurrentHealth);
            frmLevel.fighting = false;
            AddToLog(enemy.OnAttack(player));
            foreach (NPC npc in player.Party)
            {
                if (npc == null)
                {
                    continue;
                }

                if (npc.Health <= 0)
                {
                    player.removePartyMember(npc);
                }
            }
            if (player.Health <= 0)
            {
                AddToLog(enemy.Name + " defeated " + player.Name + "!");
                instance = null;
                this.Close();
                form.GameOver();
                return;
            } else
            {
                this.Close();
            }
        }

        private void ExitButton_Click(object sender, EventArgs e)
        {
            frmLevel.UpdateHealthBars(frmLevel.playerCurrentHealth);
            frmLevel.fighting = false;
            Close();
        }

        private void AttackButton_Click(object sender, EventArgs e)
        {
            do
            {
                if (this.attackOrder[0] == enemy)
                {
                    int target = enemy.dice.Next(1, player.PartyCount() + 1);
                    AddToLog(this.attackOrder[0].OnAttack(this.attackOrder[target]));
                }
                else
                    AddToLog(this.attackOrder[0].OnAttack(enemy));

                RotateOrder();
                UpdateHealthBars();

                foreach (NPC npc in player.Party)
                {
                    if (npc == null)
                    {
                        continue;
                    }

                    if (npc.Health <= 0)
                    {
                        player.removePartyMember(npc);
                        attackOrder.Remove(npc);
                    }
                }

                if (player.Health <= 0)
                {
                    AddToLog(enemy.Name + " defeated " + player.Name + "!");
                    instance = null;
                    this.Close();
                    form.GameOver();
                    return;
                }
                else if (enemy.Health <= 0)
                {
                    AddToLog(this.attackOrder[this.attackOrder.Count - 1].Name + " deafeated " + enemy.Name + "!");
                    instance = null;
                    form.RemoveEnemy(enemy);
                    player.RemoveEffect();
                    frmLevel.UpdateHealthBars(frmLevel.playerCurrentHealth);
                    frmLevel.UpdateStatusBar(frmLevel.def_label, frmLevel.damage_label, frmLevel.speed_label);

                    Button ExitButton = new Button();
                    ExitButton.Parent = this;
                    ExitButton.Location = new Point(width/6, this.AttackButton.Top);
                    ExitButton.Size = new Size(2 * width / 3, height - this.AttackButton.Top - 10);
                    ExitButton.Text = "You Won! Click to Exit";
                    ExitButton.Font = new Font("NSimSun", ExitButton.Size.Height / 2);
                    Game.FontSizing(ExitButton);
                    ExitButton.BackColor = Color.DarkGreen;
                    ExitButton.ForeColor = Color.White;
                    ExitButton.FlatStyle = FlatStyle.Flat;
                    ExitButton.FlatAppearance.BorderColor = Color.Black;
                    ExitButton.FlatAppearance.BorderSize = 1;
                    ExitButton.Click += ExitButton_Click;
                    ExitButton.BringToFront();
                    UseButton.Hide();
                    AttackButton.Hide();
                    FleeButton.Hide();

                    return;
                }
            }
            while (this.attackOrder[attackOrder.Count - 1] != player);
        }


        private void CharacterDamage(Character target, int amount)
        {
            target.TakeDamage(amount);
            UpdateHealthBars();
        }

        private void AddToLog(string log)
        {
            for (int i = BattleLog.Length - 1; i > 0; i--)
            {
                BattleLog[i] = BattleLog[i - 1];
            }
            BattleLog[0] = log;

            updateLog();
        }

        private void FillAttackOrder(NPC[] party, Enemy enemy)
        {
            
            for (int i = 0; i < party.Length; i++)
            {
                if (party[i] != null && this.attackOrder.Count() != 0)
                {
                    for (int j = 0; j < this.attackOrder.Count(); j++)
                    {

                        if (party[i].speed > this.attackOrder[j].speed)
                        {
                            this.attackOrder.Insert(j, party[i]);
                            break;
                        }
                    }
                    if (!this.attackOrder.Contains(party[i]))
                        this.attackOrder.Add(party[i]);
                }
                else if (party[i] != null)
                {
                    this.attackOrder.Add(party[i]);
                }
            }

            for (int i = 0; i < this.attackOrder.Count(); i++)
            {
                if (enemy.speed > this.attackOrder[i].speed)
                {
                    this.attackOrder.Insert(i, enemy);
                    break;
                }
            }
            if (!this.attackOrder.Contains(enemy))
                this.attackOrder.Add(enemy);
            
            for (int i = 0; i < this.attackOrder.Count(); i++)
            {
                if (player.speed > this.attackOrder[i].speed)
                {
                    this.attackOrder.Insert(i, player);
                    break;
                }
            }
            if (!this.attackOrder.Contains(player))
                this.attackOrder.Add(player);
        }

        private void RotateOrder()
        {
            Character move = this.attackOrder[0];
            this.attackOrder.Remove(this.attackOrder[0]);
            this.attackOrder.Add(move);
        }

    }
}
