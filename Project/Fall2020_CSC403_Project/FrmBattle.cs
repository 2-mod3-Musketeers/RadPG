using Fall2020_CSC403_Project.code;
using System;
using System.Windows.Forms;

namespace Fall2020_CSC403_Project
{
    public partial class FrmBattle : Form
    {
        public static FrmBattle instance = null;
        private Enemy enemy;
        private Player player;
        private FrmLevel form;

        private FrmBattle(FrmLevel level) {
            InitializeComponent();
            player = Game.player;
            form = level;
        }

        public void Setup()
        {
            // update for this enemy
            picEnemy.BackgroundImage = enemy.Pic.Image;
            picEnemy.Refresh();
            BackColor = enemy.Color;
            picBossBattle.Visible = false;

            picPlayer.BackgroundImage = player.Pic.Image;
            picPlayer.Refresh();

            // Observer pattern
            enemy.AttackEvent += PlayerDamage;
            player.AttackEvent += EnemyDamage;

            // show health
            UpdateHealthBars();
        }

        public void SetupForBossBattle()
        {

            tmrFinalBattle.Enabled = true;
        }

        public static FrmBattle GetInstance(FrmLevel level, Enemy enemy) {
            if (instance == null) {
                instance = new FrmBattle(level);
                instance.enemy = enemy;
                instance.Setup();
            }
            return instance;
        }

        private void UpdateHealthBars()
        {
            float playerHealthPer = player.Health / (float)player.MaxHealth;
            float enemyHealthPer = enemy.Health / (float)enemy.MaxHealth;

            const int MAX_HEALTHBAR_WIDTH = 226;
            lblPlayerHealthFull.Width = (int)(MAX_HEALTHBAR_WIDTH * playerHealthPer);
            lblEnemyHealthFull.Width = (int)(MAX_HEALTHBAR_WIDTH * enemyHealthPer);

            lblPlayerHealthFull.Text = player.Health.ToString();
            lblEnemyHealthFull.Text = enemy.Health.ToString();
        }

        private void btnAttack_Click(object sender, EventArgs e)
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

            } else if (enemy.Health <= 0)
            {
                form.RemoveEnemy(enemy);
                instance = null;
                player.RemoveEffect();
                Close();
            }

            //Adding attack sound here
            //Thread attackThread = new Thread(() =>
            //{
            //    SoundPlayer attackAudio = new SoundPlayer(Resources.punch_1);
            //    attackAudio.PlaySync(); // Play the attack sound synchronously
            //});

            //attackThread.Start(); // Start the attack sound thread
        }

        private void EnemyDamage(int amount)
        {
            enemy.TakeDamage(amount);
        }

        private void PlayerDamage(int amount)
        { 
            player.TakeDamage(amount); 
        }

        private void tmrFinalBattle_Tick(object sender, EventArgs e)
        {
            picBossBattle.Visible = false;
            tmrFinalBattle.Enabled = false;
        }
    }
}
