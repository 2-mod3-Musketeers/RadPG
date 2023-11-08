using Fall2020_CSC403_Project.code;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Xml.Serialization;

namespace Fall2020_CSC403_Project
{
    public partial class FrmBattleScreen : Form
    {
        public static FrmBattleScreen instance = null;
        private Enemy enemy;
        private Player player;
        private FrmLevel form;

        private PictureBox UtilityPicture;
        private Label UtilityLabel;

        private Label playerHealthMax;
        private Label playerCurrentHealth;

        private Label enemyHealthMax;
        private Label enemyCurrentHealth;

        private String[] BattleLog = new string[10];

        public static FrmLevel frmLevel;

        public FrmBattleScreen(FrmLevel level)
        {
            this.WindowState = FormWindowState.Maximized;
            InitializeComponent();
            form = level;
            player = Game.player;
        }

        public void Setup()
        {

            this.BackColor = Color.SlateGray;
            //this.ControlBox = false;

            int height = Screen.PrimaryScreen.Bounds.Height;
            int width = Screen.PrimaryScreen.Bounds.Width;


            // Set up player
            PictureBox picPlayer = new PictureBox();
            picPlayer.Parent = this;
            picPlayer.Size = new Size(width / 5 + width / 128, 3 * width / 10 + 2 * Height / 32);
            picPlayer.SizeMode = PictureBoxSizeMode.StretchImage;
            picPlayer.Location = new Point(width / 7, 24 * height / 128);
            picPlayer.BackColor = this.BackColor;
            picPlayer.Image = player.Pic.Image;

            // Set up Enemy
            PictureBox picEnemy = new PictureBox();
            picEnemy.Parent= this;
            picEnemy.Size = new Size(width / 5 + width / 128, 3 * width / 10 + 2 * Height / 32);
            picEnemy.SizeMode = PictureBoxSizeMode.StretchImage;
            picEnemy.Location = new Point(3*width / 4 - picPlayer.Location.X/2, 24*height/128);
            picEnemy.BackColor = this.BackColor;
            picEnemy.Image = enemy.Pic.Image;

            // Set up attack button
            Button AttackButton = new Button();
            AttackButton.Parent = this;
            AttackButton.Size = new Size(picPlayer.Size.Width/3, AttackButton.Size.Width/2);
            AttackButton.Location = new Point(picPlayer.Location.X + picPlayer.Width/3, picPlayer.Location.Y + picPlayer.Size.Height + AttackButton.Size.Height/2);
            AttackButton.Font = new Font("NSimSun", AttackButton.Size.Height / 2);
            AttackButton.Text = "Attack";
            AttackButton.BackColor = Color.DarkRed;
            AttackButton.ForeColor = Color.White;
            AttackButton.FlatStyle = FlatStyle.Flat;
            AttackButton.FlatAppearance.BorderColor = Color.Black;
            AttackButton.FlatAppearance.BorderSize = 1;
            AttackButton.Click += AttackButton_Click;

            // Set up flee button
            Button FleeButton = new Button();
            FleeButton.Parent = this;
            FleeButton.Size = AttackButton.Size;
            FleeButton.Location = new Point(picPlayer.Location.X + picPlayer.Width / 3, AttackButton.Location.Y + AttackButton.Size.Height + AttackButton.Size.Height / 2);
            FleeButton.Font = new Font("NSimSun", AttackButton.Size.Height / 2);
            FleeButton.Text = "Flee";
            FleeButton.BackColor = Color.DarkGoldenrod;
            FleeButton.ForeColor = Color.White;
            FleeButton.FlatStyle = FlatStyle.Flat;
            FleeButton.FlatAppearance.BorderColor = Color.Black;
            FleeButton.FlatAppearance.BorderSize = 1;
            FleeButton.Click += FleeButton_Click;

            // Set up UseUtility button
            Button UseButton = new Button();
            UseButton.Parent = this;
            UseButton.Size = AttackButton.Size;
            UseButton.Location = new Point(picPlayer.Location.X + picPlayer.Width / 3, FleeButton.Location.Y + FleeButton.Size.Height + FleeButton.Size.Height / 2);
            UseButton.Font = new Font("NSimSun", AttackButton.Size.Height / 2);
            UseButton.Text = "Use Utility";
            UseButton.BackColor = Color.DarkBlue;
            UseButton.ForeColor = Color.White;
            UseButton.FlatStyle = FlatStyle.Flat;
            UseButton.FlatAppearance.BorderColor = Color.Black;
            UseButton.FlatAppearance.BorderSize = 1;
            UseButton.Click += UseButton_Click;

            // Add Utility Picture 
            UtilityPicture = new PictureBox();
            UtilityPicture.Parent = this;
            UtilityPicture.Size = new Size(width / 10, width / 10);
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
                UtilityLabel.Size = new Size(UtilityPicture.Width, UtilityPicture.Height / 5);
                UtilityLabel.Location = new Point(UtilityPicture.Location.X, UtilityPicture.Location.Y + UtilityPicture.Size.Height);
                UtilityLabel.Text = "Equipped Item";
                UtilityLabel.Font = new Font("NSimSun", UtilityLabel.Size.Height / 2);
                UtilityLabel.BackColor = this.BackColor;
                UtilityLabel.TextAlign = ContentAlignment.MiddleCenter;
            }

            // Add user name and enemy name
            Label EnemyName = new Label();
            Label PlayerName = new Label();

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

            UpdateHealthBars();

            //BackColor = enemy.Color;
            //picBossBattle.Visible = false;

            // Observer pattern
            enemy.AttackEvent += PlayerDamage;
            player.AttackEvent += EnemyDamage;

            // show health
            UpdateHealthBars();
        }

        private void UpdateHealthBars()
        {
            // for player
            float playerHealthPer = player.Health / (float)player.MaxHealth;
            int MAX_HEALTHBAR_WIDTH = playerHealthMax.Width;
            playerCurrentHealth.BackColor = Color.Green;
            playerCurrentHealth.Width = (int)(MAX_HEALTHBAR_WIDTH * playerHealthPer);
            playerCurrentHealth.Text = player.Health.ToString();

            // for enemy
            float enemyHealthPer = enemy.Health / (float)enemy.MaxHealth;
            int ENEMY_MAX_HEALTHBAR_WIDTH = enemyHealthMax.Width;
            enemyCurrentHealth.BackColor = Color.Green;
            enemyCurrentHealth.Width = (int)(ENEMY_MAX_HEALTHBAR_WIDTH * enemyHealthPer);
            enemyCurrentHealth.Text = enemy.Health.ToString();
        }

        private void UseButton_Click(object sender, EventArgs e)
        {
            player.ApplyEffect(player.Inventory.Utility.Potion, player.Inventory.Utility.Stat);
            player.Inventory.UseItem();
            UtilityPicture.Hide();
            UtilityLabel.Hide();
            UpdateHealthBars();
            
        }

        private void FleeButton_Click(object sender, EventArgs e)
        {
            frmLevel.UpdateHealthBars(frmLevel.playerCurrentHealth);
            enemy.OnAttack(player.defense);
            this.Close();
        }

        private void AttackButton_Click(object sender, EventArgs e)
        {
            if (player.speed >= enemy.speed)
            {
                player.OnAttack(enemy.defense);
                if (enemy.Health > 0)
                {
                    enemy.OnAttack(player.defense);
                }
            }
            else
            {
                if (enemy.Health > 0)
                {
                    enemy.OnAttack(player.defense);
                }
                player.OnAttack(enemy.defense);
            }

            UpdateHealthBars();
            if (player.Health <= 0)
            {
                instance = null;
                Close();
                form.GameOver();

            }
            else if (enemy.Health <= 0)
            {
                instance = null;
                form.RemoveEnemy(enemy);
                player.RemoveEffect();
                frmLevel.UpdateHealthBars(frmLevel.playerCurrentHealth);
                frmLevel.UpdateStatusBar(frmLevel.def_label, frmLevel.damage_label, frmLevel.speed_label);
                Close();
            }
        }

        public static FrmBattleScreen GetInstance(FrmLevel level, Enemy enemy)
        {
            frmLevel = level;
            instance = new FrmBattleScreen(level);
            instance.enemy = enemy;
            instance.Setup();
            return instance;
           
        }
        private void EnemyDamage(int amount)
        {
            enemy.TakeDamage(amount);
            UpdateHealthBars();
        }

        private void PlayerDamage(int amount)
        {
            player.TakeDamage(amount);
            UpdateHealthBars();
        }

    }
}
