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
        private Enemy enemy;
        private Player player;
        public int selected;

        public FrmInv()
        {
            InitializeComponent();
        }


        public static FrmInv GetInstance(Player player)
        {
            instance = new FrmInv();
            instance.Setup(player);
            return instance;
        }

        public void Setup(Player player)
        {
            
            // update for this Player
            PlayerPic.Image = Fall2020_CSC403_Project.Properties.Resources.player;
            PlayerPic.Refresh();
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
                Armor.Image = player.Inventory.Weapon.Pic.Image;
            }
            if (player.Inventory.Utility != null)
            {
                Utility.Image = player.Inventory.Weapon.Pic.Image;
            }

            

        }

        private void FrmInv_Load(object sender, EventArgs e)
        {

        }


        private void ExitButton_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void SettingButton_Click(object sender, EventArgs e)
        {

        }

        private void UnequipButton_Click(object sender, EventArgs e)
        {

        }

        private void EquipButton_Click(object sender, EventArgs e)
        {

        }

        private void DropButton_Click(object sender, EventArgs e)
        {

        }
        private void Inv1_Click(object sender, EventArgs e)
        {
            selected = 1;
        }
        private void Inv2_Click(object sender, EventArgs e)
        {
            selected = 2;
        }

        private void Inv3_Click(object sender, EventArgs e)
        {
            selected = 3;
        }

        private void Inv4_Click(object sender, EventArgs e)
        {
            selected = 4;
        }

        private void Inv5_Click(object sender, EventArgs e)
        {
            selected = 5;
        }

        private void Inv6_Click(object sender, EventArgs e)
        {
            selected = 6;
        }

        private void Inv7_Click(object sender, EventArgs e)
        {
            selected = 7;
        }

        private void Inv8_Click(object sender, EventArgs e)
        {
            selected = 8;
        }

        private void Inv9_Click(object sender, EventArgs e)
        {
            selected = 9; 
        }

        private void Weapon_Click(object sender, EventArgs e)
        {
           
        }

        private void Armor_Click(object sender, EventArgs e)
        {

        }

        private void Utility_Click(object sender, EventArgs e)
        {

        }
    }
}
