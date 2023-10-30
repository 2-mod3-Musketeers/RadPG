﻿using Fall2020_CSC403_Project.code;
using MyGameLibrary;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Drawing.Text;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Fall2020_CSC403_Project
{
    public partial class FrmInv : Form
    {
        public static FrmInv instance = null;
        public PictureBox[] InvSlots = new PictureBox[9];
        private Player player;
        public int selected;
        public PictureBox[] PictureBoxes;

        

        public FrmInv()
        {
            InitializeComponent();
            PictureBoxes = new PictureBox[] { Inv1, Inv2, Inv3, Inv4, Inv5, Inv6, Inv7, Inv8, Inv9, Weapon, Armor, Utility };
        }

        public static FrmInv GetInstance(Player player)
        {
            instance = new FrmInv();
            instance.Setup(player);
            return instance;
        }

       

        // set up for the inventory screen that places images in the correct place based on player inventory
        public void Setup(Player player)
        {

            // update for this Player
            this.player = player;
            PlayerPic.Image = player.Pic.Image;          // subject to change when new player types are added
            PlayerPic.Refresh();
            PlayerCurrentHealth.AutoSize = false;
            PlayerCurrentHealth.Height = playerHealthMax.Height;
            PlayerCurrentHealth.Location = playerHealthMax.Location;
            UpdateHealthBars();
            PictureBox[] show_inventory = { Inv1, Inv2, Inv3, Inv4, Inv5, Inv6, Inv7, Inv8, Inv9 };
            for (int i = 0; i < player.Inventory.Backpack.Length; i++)
            {
                if (player.Inventory.Backpack[i] != null)
                {
                    show_inventory[i].Image = player.Inventory.Backpack[i].Pic.Image;
                }
            }


            if (player.Inventory.Weapon != null)
            {
                Weapon.Image = player.Inventory.Weapon.Pic.Image;
            }
            if (player.Inventory.Armor != null)
            {
                Armor.Image = player.Inventory.Armor.Pic.Image;
            }
            if (player.Inventory.Utility != null)
            {
                Utility.Image = player.Inventory.Utility.Pic.Image;
            }


            // Sets up player stat locations


            Label AttackStat = new Label();
            Label DefStat = new Label();
            Label SpeedStat = new Label();

            AttackStat.Parent = this;
            DefStat.Parent = this;
            SpeedStat.Parent = this;

            AttackStat.BackColor = Color.Transparent;
            DefStat.BackColor = Color.Transparent;
            SpeedStat.BackColor = Color.Transparent;

            AttackStat.Text = ("Attack Stat = " + player.damage.ToString());
            DefStat.Text = ("Toughness Stat = " + player.defense.ToString());
            SpeedStat.Text = ("Speed Stat = " + player.speed.ToString());

            AttackStat.Location = new Point(Weapon.Location.X + 2 * PlayerPic.Width, Weapon.Location.Y + (Weapon.Height/2));
            DefStat.Location = new Point(Armor.Location.X + 2 * PlayerPic.Width, Armor.Location.Y + (Armor.Height / 2));
            SpeedStat.Location = new Point(Utility.Location.X + 2 * PlayerPic.Width, Utility.Location.Y + (Utility.Height / 2));





        }

        // since c# doesn't allow you to just refresh all picboxes, same as above
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
            this.Refresh();

        }

        private void FrmInv_Load(object sender, EventArgs e)
        {

        }


        private void UpdateHealthBars()
        {
            float playerHealthPer = player.Health / (float)player.MaxHealth;
            int MAX_HEALTHBAR_WIDTH = playerHealthMax.Width;
            PlayerCurrentHealth.BackColor = Color.Green;
            PlayerCurrentHealth.Width = (int)(MAX_HEALTHBAR_WIDTH * playerHealthPer);
            PlayerCurrentHealth.Text = player.Health.ToString();
        }



        // closes inv
        private void ExitButton_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        // will be settings, incomplete
        private void SettingButton_Click(object sender, EventArgs e)
        {
            FrmSettings frmsettings = new FrmSettings(this);
            frmsettings.FormClosed += (s, args) => this.Close(); // Handle closure of FrmLevel to close the application
            frmsettings.Show();
            this.Hide(); // Hide the FrmMain form
        }

        // removes a selected item from the character if it exists
        private void UnequipButton_Click(object sender, EventArgs e)
        {
            if (selected == 10)
            {
                player.Inventory.UnEquipWeapon(player.Position, player.facing);
                RefreshInv();
            }
            else if (selected == 11)
            {
                player.Inventory.UnEquipArmor(player.Position, player.facing);
                RefreshInv();
            }
            else if (selected == 12)
            {
                player.Inventory.UnEquipUtility(player.Position, player.facing);
                RefreshInv();
            }
            

            if (selected > 0)
            {
                PictureBoxes[selected - 1].BackColor = Color.DimGray;
            }
            selected = 0;

        }

        // moves a item from a backback slot to an equip slot
        private void EquipButton_Click(object sender, EventArgs e)
        {
            if (selected > 0 && selected < 10)
            {
                if (player.Inventory.Backpack[selected - 1] != null && player.Inventory.Backpack[selected - 1].Type == Item.ItemType.Weapon)
                {
                    player.Inventory.EquipWeapon(player.Inventory.Backpack[selected - 1], player.Position, player.facing);
                    player.UpdateStats();
                    RefreshInv();
                 
                }
                else if (player.Inventory.Backpack[selected - 1] != null && player.Inventory.Backpack[selected - 1].Type == Item.ItemType.Armor)
                {
                    player.Inventory.EquipArmor(player.Inventory.Backpack[selected - 1], player.Position, player.facing);
                    player.UpdateStats();
                    RefreshInv();
                }
                else if (player.Inventory.Backpack[selected - 1] != null && player.Inventory.Backpack[selected - 1].Type == Item.ItemType.Utility)
                {
                    player.Inventory.EquipUtility(player.Inventory.Backpack[selected - 1], player.Position, player.facing);
                    player.UpdateStats();
                    RefreshInv();
                }
                else { }

                if (selected > 0)
                {
                    PictureBoxes[selected - 1].BackColor = Color.DimGray;
                }
                selected = 0;


            }
        }

        // drops selected item
        private void DropButton_Click(object sender, EventArgs e) 
        {
            if (selected > 0 && selected < 10) { 
                player.Inventory.DropItem(player.Inventory.Backpack[selected - 1], player.Position, player.facing);
                player.Inventory.RemoveFromBackpack(selected - 1);
            }
            if (selected > 0)
            {
                PictureBoxes[selected - 1].BackColor = Color.DimGray;
            }
            selected = 0;
            RefreshInv();
        }

        // not functional, will let you use utility later
        private void UseItem_Click(object sender, EventArgs e)
        {
            if (selected == 12)
            {
                player.ApplyEffect(player.Inventory.Utility.Potion, player.Inventory.Utility.Stat);
                player.Inventory.UseItem();
                RefreshInv();
            }
            else
            {

            }

            if (selected > 0)
            {
                PictureBoxes[selected - 1].BackColor = Color.DimGray;
            }
            selected = 0;
        }


        // everything below here is just handling what happens when a user selects an item (color and recolor of background)
        private void Inv1_Click(object sender, EventArgs e)
        {
            if (selected > 0)
            {
                PictureBoxes[selected - 1].BackColor = Color.DimGray;
            }
            selected = 1;
            Inv1.BackColor = Color.WhiteSmoke;
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

        private void Inv3_Click(object sender, EventArgs e)
        {
            if (selected > 0)
            {
                PictureBoxes[selected - 1].BackColor = Color.DimGray;
            }
            selected = 3;
            Inv3.BackColor = Color.WhiteSmoke;
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

        private void Inv5_Click(object sender, EventArgs e)
        {
            if (selected > 0)
            {
                PictureBoxes[selected - 1].BackColor = Color.DimGray;
            }
            selected = 5;
            Inv5.BackColor = Color.WhiteSmoke   ;
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

        private void Inv7_Click(object sender, EventArgs e)
        {
            if (selected > 0)
            {
                PictureBoxes[selected - 1].BackColor = Color.DimGray;
            }
            selected = 7;
            Inv7.BackColor = Color.WhiteSmoke;
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

        private void Inv9_Click(object sender, EventArgs e)
        {
            if (selected > 0)
            {
                PictureBoxes[selected - 1].BackColor = Color.DimGray;
            }
            selected = 9;
            Inv9.BackColor = Color.WhiteSmoke;
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

        private void Armor_Click(object sender, EventArgs e)
        {
            if (selected > 0)
            {
                PictureBoxes[selected - 1].BackColor = Color.DimGray;
            }
            selected = 11;
            Armor.BackColor = Color.WhiteSmoke;
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

    }
}
