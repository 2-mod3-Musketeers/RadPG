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

namespace Fall2020_CSC403_Project
{
    public partial class FrmInventory : Form
    {

        public static FrmInventory instance = null;
        public PictureBox[] InvSlots = new PictureBox[9];
        private Player player;
        public int selected;
        public PictureBox[] PictureBoxes;

        // Pic Declarations for showing inventory
        private PictureBox Inv1;
        private PictureBox Inv2;
        private PictureBox Inv3;
        private PictureBox Inv4;
        private PictureBox Inv5;
        private PictureBox Inv6;
        private PictureBox Inv7;
        private PictureBox Inv8;
        private PictureBox Inv9;
        private PictureBox Weapon;
        private PictureBox Armor;
        private PictureBox Utility;

        // label declarations for showing health
        private Label HealthMax;
        private Label CurrentHealth;

        // label declarations for showing stats
        private Label AttackStat;
        private Label DefStat;
        private Label SpeedStat;

        private Point PanOffset;

        public FrmInventory()
        {
            this.WindowState = FormWindowState.Maximized;
            InitializeComponent();
            this.KeyPreview = true;
        }

        public static FrmInventory GetInstance(Player player, Point PanOffset)
        {
            instance = new FrmInventory();
            instance.Setup(player, PanOffset);
            return instance;
        }

        public void Setup(Player player, Point PanOffset)
        {
            int height = Screen.PrimaryScreen.Bounds.Height;
            int width = Screen.PrimaryScreen.Bounds.Width;

            this.PanOffset = PanOffset;

            this.KeyDown += FrmInventory_KeyDown;
            this.BackColor = Color.DarkSlateGray;

            // create inv screen containers
            this.player = player;

             Inv1 = new PictureBox();
             Inv2 = new PictureBox();
             Inv3 = new PictureBox();
             Inv4 = new PictureBox();
             Inv5 = new PictureBox();
             Inv6 = new PictureBox();
             Inv7 = new PictureBox();
             Inv8 = new PictureBox();
             Inv9 = new PictureBox();
             Weapon = new PictureBox();
             Armor = new PictureBox();
             Utility = new PictureBox();

            PictureBoxes = new PictureBox[] { Inv1, Inv2, Inv3, Inv4, Inv5, Inv6, Inv7, Inv8, Inv9, Weapon, Armor, Utility };

            SetInvControls(width);

            // place inv buttons
            Inv1.Location = new Point(5 * width / 64, 3 * height / 16);
            Inv2.Location = new Point(Inv1.Location.X + Inv1.Width + width/128, 3 * height / 16);
            Inv3.Location = new Point(Inv2.Location.X + Inv2.Width + width / 128, 3 * height / 16);

            Inv4.Location = new Point(5 * width / 64, Inv1.Location.Y + Inv1.Height + Height / 32);
            Inv5.Location = new Point(Inv1.Location.X + Inv1.Width + width / 128, Inv2.Location.Y + Inv2.Height + Height / 32);
            Inv6.Location = new Point(Inv2.Location.X + Inv2.Width + width / 128, Inv3.Location.Y + Inv3.Height + Height / 32);

            Inv7.Location = new Point(5 * width / 64, Inv4.Location.Y + Inv4.Height + Height / 32);
            Inv8.Location = new Point(Inv1.Location.X + Inv1.Width + width / 128, Inv5.Location.Y + Inv5.Height + Height / 32);
            Inv9.Location = new Point(Inv2.Location.X + Inv2.Width + width / 128, Inv6.Location.Y + Inv6.Height + Height / 32);

            Weapon.Location = new Point(Inv3.Location.X + 2* Inv2.Width + 4 * width / 128, 3 * height / 16);
            Armor.Location = new Point(Inv3.Location.X + 2 * Inv2.Width + 4*width / 128, Inv2.Location.Y + Inv2.Height + Height / 32);
            Utility.Location = new Point(Inv3.Location.X + 2 * Inv2.Width + 4*width / 128, Inv6.Location.Y + Inv6.Height + Height / 32);

            // Set player Pic
            PictureBox PlayerPic = new PictureBox();
            PlayerPic.Parent = this;
            PlayerPic.BackColor = Color.DimGray;
            PlayerPic.Location = new Point(Weapon.Location.X + Weapon.Width + width/128, Weapon.Location.Y);
            PlayerPic.Size = new Size(width / 5 + width / 128, 3 * width / 10 + 2* Height/32);
            PlayerPic.SizeMode = PictureBoxSizeMode.StretchImage;
            PlayerPic.Image = player.Pic.Image;


            // add click controls
            Inv1.Click += Inv1_Click;
            Inv2.Click += Inv2_Click;
            Inv3.Click += Inv3_Click;
            Inv4.Click += Inv4_Click;
            Inv5.Click += Inv5_Click;
            Inv6.Click += Inv6_Click;
            Inv7.Click += Inv7_Click;
            Inv8.Click += Inv8_Click;
            Inv9.Click += Inv9_Click;
            Weapon.Click += Weapon_Click;
            Utility.Click += Utility_Click;
            Armor.Click += Armor_Click;

            // add buttons for Drop, equip, use, unequp, settings, and return
            Button UseButton = new Button();
            Button DropButton = new Button();
            Button EquipButton = new Button();
            Button UnequipButton = new Button();
            PictureBox SettingsButton = new PictureBox();
            Button ReturnButton = new Button();

            // everything for drop button
            DropButton.Size = new Size(Inv1.Width, Inv1.Height / 3);
            DropButton.Parent = this;
            DropButton.Location = new Point(Inv7.Location.X + Inv7.Width/2, Inv7.Location.Y+Inv7.Height+height/32);
            DropButton.Text = "Drop Item";
            DropButton.Font = new Font("NSimSun", DropButton.Size.Height / 4);
            DropButton.Click += DropButton_Click;

            // Everything for Equip Button
            EquipButton.Size = new Size(Inv1.Width, Inv1.Height / 3);
            EquipButton.Parent = this;
            EquipButton.Location = new Point(Inv8.Location.X + Inv8.Width / 2, Inv8.Location.Y + Inv8.Height + height / 32);
            EquipButton.Text = "Equip Item";
            EquipButton.Font = new Font("NSimSun", EquipButton.Size.Height /4);
            EquipButton.Click += EquipButton_Click;

            // Use button
            UseButton.Size = new Size(Inv1.Width, Inv1.Height / 3);
            UseButton.Parent = this;
            UseButton.Location = new Point(Utility.Location.X - Utility.Width / 2, Utility.Location.Y + Utility.Height + height / 32);
            UseButton.Text = "Use Item";
            UseButton.Font = new Font("NSimSun", UseButton.Size.Height / 4);
            UseButton.Click += UseButton_Click;

            // Unequip Button
            UnequipButton.Size = new Size(Inv1.Width, Inv1.Height / 3);
            UnequipButton.Parent = this;
            UnequipButton.Location = new Point(Utility.Location.X + Utility.Width / 2, Utility.Location.Y + Utility.Height + height / 32);
            UnequipButton.Text = "Unequip Item";
            UnequipButton.Font = new Font("NSimSun", UnequipButton.Size.Height / 4);
            UnequipButton.Click += UnequipButton_Click;

            // Return Button
            ReturnButton.Size = new Size(Inv1.Width, Inv1.Height / 3);
            ReturnButton.Parent = this;
            ReturnButton.Location = new Point(width-ReturnButton.Width-width/32, height/64);
            ReturnButton.Text = "Return";
            ReturnButton.Font = new Font("NSimSun", UnequipButton.Size.Height / 4);
            ReturnButton.Click += ReturnButton_Click;

            // Settings Button
            SettingsButton.Size = new Size(Inv1.Width/2, Inv1.Height / 2);
            SettingsButton.Parent = this;
            SettingsButton.Location = new Point(width - SettingsButton.Width - width / 32, 2*height / 64+ReturnButton.Height);
            SettingsButton.Image = Properties.Resources.Cog;
            SettingsButton.SizeMode = PictureBoxSizeMode.StretchImage;
            SettingsButton.Click += SettingsButton_Click;

            // Labels for armor, weapon, utility
            Label WeaponLabel = new Label();
            Label ArmorLabel = new Label();
            Label UtilityLabel = new Label();

            // Weapon Label
            WeaponLabel.Size = new Size(Inv1.Width, Inv1.Height / 3);
            WeaponLabel.Parent = this;
            WeaponLabel.Location = new Point(Inv3.Location.X + 7 *Inv3.Width/6, Inv3.Location.Y + Inv3.Height/2);
            WeaponLabel.TextAlign = ContentAlignment.MiddleRight;
            WeaponLabel.Text = "Weapon\nIncreases Damage";
            WeaponLabel.Font = new Font("NSimSun", WeaponLabel.Size.Height / 4);

            // Armor Label
            ArmorLabel.Size = new Size(Inv1.Width, Inv1.Height / 3);
            ArmorLabel.Parent = this;
            ArmorLabel.Location = new Point(Inv6.Location.X + 7 * Inv6.Width / 6, Inv6.Location.Y + Inv6.Height / 2);
            ArmorLabel.TextAlign = ContentAlignment.MiddleRight;
            ArmorLabel.Text = "Armor\nIncreases Defense";
            ArmorLabel.Font = new Font("NSimSun", ArmorLabel.Size.Height / 4);

            // Utility Label
            UtilityLabel.Size = new Size(Inv1.Width, Inv1.Height / 3);
            UtilityLabel.Parent = this;
            UtilityLabel.Location = new Point(Inv9.Location.X + 7 * Inv9.Width / 6, Inv9.Location.Y + Inv9.Height / 2);
            UtilityLabel.TextAlign = ContentAlignment.MiddleRight;
            UtilityLabel.Text = "Utility\nCauses an Effect";
            UtilityLabel.Font = new Font("NSimSun", UtilityLabel.Size.Height / 4);

            // Health Bar

            HealthMax = new Label(); 
            CurrentHealth = new Label();

            CurrentHealth.Size = new Size(PlayerPic.Width, height / 32);
            CurrentHealth.Parent = this;
            CurrentHealth.Location = new Point(PlayerPic.Location.X, PlayerPic.Location.Y - 3 * height / 64);
            CurrentHealth.Font = new Font("NSimSun", WeaponLabel.Size.Height / 3);
            CurrentHealth.TextAlign = ContentAlignment.MiddleCenter;
            CurrentHealth.BackColor = Color.Green;
            CurrentHealth.AutoSize = false;

            HealthMax.Size = new Size(PlayerPic.Width, height/32);
            HealthMax.Parent = this;
            HealthMax.Location = new Point(PlayerPic.Location.X, PlayerPic.Location.Y - 3*height/64);
            HealthMax.Font = new Font("NSimSun", WeaponLabel.Size.Height / 2);
            HealthMax.BackColor = Color.Red;
            HealthMax.AutoSize = false;

            UpdateHealthBars();

            // Labels for stats
            AttackStat = new Label();
            DefStat = new Label();
            SpeedStat = new Label();

            // Attack Stat Label
            AttackStat.Size = new Size(Inv1.Width, Inv1.Height / 3);
            AttackStat.AutoSize = false;
            AttackStat.Parent = this;
            AttackStat.Location = new Point(PlayerPic.Location.X + PlayerPic.Width + width / 32, Weapon.Location.Y + Weapon.Height / 3);
            AttackStat.TextAlign = ContentAlignment.MiddleLeft;
            AttackStat.Font = new Font("NSimSun", AttackStat.Size.Height / 4);

            // Defense Stat Label
            DefStat.Size = new Size(Inv1.Width, Inv1.Height / 3);
            DefStat.AutoSize = false;
            DefStat.Parent = this;
            DefStat.Location = new Point(PlayerPic.Location.X + PlayerPic.Width + width/32, Armor.Location.Y + Armor.Height/3);
            DefStat.TextAlign = ContentAlignment.MiddleLeft;
            DefStat.Font = new Font("NSimSun", DefStat.Size.Height / 4);

            // Speed Stat Label
            SpeedStat.Size = new Size(Inv1.Width, Inv1.Height / 3);
            SpeedStat.AutoSize = false;
            SpeedStat.Parent = this;
            SpeedStat.Location = new Point(PlayerPic.Location.X + PlayerPic.Width + width / 32, Utility.Location.Y + Utility.Height / 3);
            SpeedStat.TextAlign = ContentAlignment.MiddleLeft;
            SpeedStat.Font = new Font("NSimSun", SpeedStat.Size.Height / 4);

            UpdateStats();


            RefreshInv();

        }

        private void UpdateStats()
        {
            SpeedStat.Text = "Speed: "+player.speed.ToString();
            DefStat.Text = "Defense: "+player.defense.ToString();
            AttackStat.Text = "Attack: "+player.damage.ToString();
        }

        private void UpdateHealthBars()
        {
            float playerHealthPer = player.Health / (float)player.MaxHealth;
            int MAX_HEALTHBAR_WIDTH = HealthMax.Width;
            CurrentHealth.BackColor = Color.Green;
            CurrentHealth.Width = (int)(MAX_HEALTHBAR_WIDTH * playerHealthPer);
            CurrentHealth.Text = player.Health.ToString();
        }

        private void SettingsButton_Click(object sender, EventArgs e)
        {
            FrmSettings frmsettings = new FrmSettings(this);
            frmsettings.FormClosed += (s, args) => this.Close(); // Handle closure of FrmLevel to close the application
            frmsettings.Show();
            this.Hide(); // Hide the FrmMain form
        }

        private void ReturnButton_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void FrmInventory_KeyDown(object sender, KeyEventArgs e)
        {
            switch (e.KeyCode)
            {

                case Keys.E:
                    this.Close();
                    break;

                case Keys.Escape:
                    FrmSettings frmsettings = new FrmSettings(this);
                    frmsettings.FormClosed += (s, args) => this.Close(); // Handle closure of FrmLevel to close the application
                    frmsettings.Show();
                    this.Hide();
                    break;

                default:
                    break;
            }
        }

        private void UnequipButton_Click(object sender, EventArgs e)
        {
            if (selected == 10)
            {
                player.Inventory.UnEquipWeapon(player.Position, player.facing, this.PanOffset);
                RefreshInv();
            }
            else if (selected == 11)
            {
                player.Inventory.UnEquipArmor(player.Position, player.facing, this.PanOffset);
                RefreshInv();
            }
            else if (selected == 12)
            {
                player.Inventory.UnEquipUtility(player.Position, player.facing, this.PanOffset);
                RefreshInv();
            }
            else { }
            if (selected > 0)
            {
                PictureBoxes[selected - 1].BackColor = Color.DimGray;
            }
            selected = 0;

        }

        private void UseButton_Click(object sender, EventArgs e)
        {
            if (selected == 12)
            {
                player.ApplyEffect(player.Inventory.Utility.Potion, player.Inventory.Utility.Stat);
                player.Inventory.UseItem();
                RefreshInv();
            }


            if (selected > 0)
            {
                PictureBoxes[selected - 1].BackColor = Color.DimGray;
            }
            selected = 0;
        }

        private void EquipButton_Click(object sender, EventArgs e)
        {
            if (selected > 0 && selected < 10)
            {
                if (player.Inventory.Backpack[selected - 1] != null && player.Inventory.Backpack[selected - 1].Type == MyGameLibrary.Item.ItemType.Weapon)
                {
                    player.Inventory.EquipWeapon(player.Inventory.Backpack[selected - 1], player.Position, player.facing, this.PanOffset);

                }
                else if (player.Inventory.Backpack[selected - 1] != null && player.Inventory.Backpack[selected - 1].Type == MyGameLibrary.Item.ItemType.Armor)
                {
                    player.Inventory.EquipArmor(player.Inventory.Backpack[selected - 1], player.Position, player.facing, this.PanOffset);
                }
                else if (player.Inventory.Backpack[selected - 1] != null && player.Inventory.Backpack[selected - 1].Type == MyGameLibrary.Item.ItemType.Utility)
                {
                    player.Inventory.EquipUtility(player.Inventory.Backpack[selected - 1], player.Position, player.facing, this.PanOffset);
                }
                else { }

                if (selected > 0)
                {
                    PictureBoxes[selected - 1].BackColor = Color.DimGray;
                }
                selected = 0;

                player.UpdateStats();
                RefreshInv();


            }
        }

        private void DropButton_Click(object sender, EventArgs e)
        {
            if (selected > 0 && selected < 10)
            {
                player.Inventory.DropItem(player.Inventory.Backpack[selected - 1], player.Position, player.facing, this.PanOffset);
                player.Inventory.RemoveFromBackpack(selected - 1);
            }
            if (selected > 0)
            {
                PictureBoxes[selected - 1].BackColor = Color.DimGray;
            }
            selected = 0;
            RefreshInv();
        }

        public void SetInvControls(int width)
        {
            for (int i = 0; i < PictureBoxes.Length; i++)
            {
                PictureBoxes[i].Size = new Size(width / 10, width / 10);
                PictureBoxes[i].SizeMode = PictureBoxSizeMode.StretchImage; 
                PictureBoxes[i].Parent = this;
                PictureBoxes[i].BackColor = Color.DimGray;
            }
        }


        public void RefreshInv()
        {
            PictureBox[] show_inventory = { Inv1, Inv2, Inv3, Inv4, Inv5, Inv6, Inv7, Inv8, Inv9 };
            for (int i = 0; i < player.Inventory.Backpack.Length; i++)
            {
                if (player.Inventory.Backpack[i] != null)
                {
                    show_inventory[i].Image = player.Inventory.Backpack[i].Pic.Image;
                }
                else
                {
                    show_inventory[i].Image = null;
                }
            }
            if (player.Inventory.Weapon != null)
            {
                Weapon.Image = player.Inventory.Weapon.Pic.Image;
            }
            else
            {
                Weapon.Image = null;
            }
            if (player.Inventory.Armor != null)
            {
                Armor.Image = player.Inventory.Armor.Pic.Image;
            }
            else
            {
                Armor.Image = null;
            }
            if (player.Inventory.Utility != null)
            {
                Utility.Image = player.Inventory.Utility.Pic.Image;
            }
            else
            {
                Utility.Image = null;
            }


            UpdateHealthBars();
            UpdateStats();
            this.Refresh();

        }




        //handle clicking inventory objects
        private void Armor_Click(object sender, EventArgs e)
        {
            if (selected > 0)
            {
                PictureBoxes[selected - 1].BackColor = Color.DimGray;
            }
            selected = 11;
            Armor.BackColor = Color.WhiteSmoke; ;
        }

        private void Utility_Click(object sender, EventArgs e)
        {
            if (selected > 0)
            {
                PictureBoxes[selected - 1].BackColor = Color.DimGray;
            }
            selected = 12;
            Utility.BackColor = Color.WhiteSmoke;
        }

        private void Weapon_Click(object sender, EventArgs e)
        {
            if (selected > 0)
            {
                PictureBoxes[selected - 1].BackColor = Color.DimGray;
            }
            selected = 10;
            Weapon.BackColor = Color.WhiteSmoke;
        }

        private void Inv9_Click(object sender, EventArgs e)
        {
            if (selected > 0)
            {
                PictureBoxes[selected - 1].BackColor = Color.DimGray;
            }
            selected = 9;
            Inv9.BackColor = Color.WhiteSmoke;
        }

        private void Inv8_Click(object sender, EventArgs e)
        {
            if (selected > 0)
            {
                PictureBoxes[selected - 1].BackColor = Color.DimGray;
            }
            selected = 8;
            Inv8.BackColor = Color.WhiteSmoke;
        }

        private void Inv7_Click(object sender, EventArgs e)
        {
            if (selected > 0)
            {
                PictureBoxes[selected - 1].BackColor = Color.DimGray;
            }
            selected = 7;
            Inv7.BackColor = Color.WhiteSmoke;
        }

        private void Inv6_Click(object sender, EventArgs e)
        {
            if (selected > 0)
            {
                PictureBoxes[selected - 1].BackColor = Color.DimGray;
            }
            selected = 6;
            Inv6.BackColor = Color.WhiteSmoke;
        }

        private void Inv5_Click(object sender, EventArgs e)
        {
            if (selected > 0)
            {
                PictureBoxes[selected - 1].BackColor = Color.DimGray;
            }
            selected = 5;
            Inv5.BackColor = Color.WhiteSmoke;
        }

        private void Inv4_Click(object sender, EventArgs e)
        {
            if (selected > 0)
            {
                PictureBoxes[selected - 1].BackColor = Color.DimGray;
            }
            selected = 4;
            Inv4.BackColor = Color.WhiteSmoke;
        }

        private void Inv3_Click(object sender, EventArgs e)
        {
            if (selected > 0)
            {
                PictureBoxes[selected - 1].BackColor = Color.DimGray;
            }
            selected = 3;
            Inv3.BackColor = Color.WhiteSmoke;
        }

        private void Inv2_Click(object sender, EventArgs e)
        {
            if (selected > 0)
            {
                PictureBoxes[selected - 1].BackColor = Color.DimGray;
            }
            selected = 2;
            Inv2.BackColor = Color.WhiteSmoke;
        }

        private void Inv1_Click(object sender, EventArgs e)
        {
            if (selected > 0)
            {
                PictureBoxes[selected - 1].BackColor = Color.DimGray;
            }
            selected = 1;
            Inv1.BackColor = Color.WhiteSmoke;
        }





    }
}
