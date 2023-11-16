﻿using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using MyGameLibrary;
using MyGameLibrary.Properties;

namespace Fall2020_CSC403_Project.code
{
	public static class Game
	{
		public static Player player = null;

		public static Area[] Areas = new Area[10];

		public static Area CurrentArea;

        private static Size itemSize = new Size(50, 50);

        // default items
        public static Dictionary<string, Item> Items = new Dictionary<string, Item>();
        public static Dictionary<string, Enemy> Enemies = new Dictionary<string, Enemy>();
        public static Dictionary<string, NPC> NPCs = new Dictionary<string, NPC>();
        public static Dictionary<string, Structure> Structures = new Dictionary<string, Structure>();

        public static void PopulateWorld()
        {
            // Create Items
            Items["Sting"] = new Item(
                "Sting",
                MakePictureBox(Resources.common_dagger, new Point(500, 300), itemSize),
                5,
                Item.ItemType.Weapon,
                "This small dagger feels sturdy in your hands. As you look at the hilt, you see a name etched into the handle by a previous owner: \"Garn Thalia\"");

            Items["Shabby Armor"] = new Item(
                "Shabby Armor",
                MakePictureBox(Resources.common_armor, new Point(880, 800), itemSize),
                5, Item.ItemType.Armor,
                "This armor looks thin and flexible. Putting it on, there seems to be a gap in the armor along the spine. Since when did people have ridges on their back?");

            Items["Speed Potion"] = new Item(
                "Potion of Speed",
                MakePictureBox(Resources.speed_potion, new Point(69, 420), itemSize),
                10,
                Item.ItemType.Utility,
                Item.PotionTypes.Speed,
                "This bottle acts unnatural as a simple tremor will cause the liquid inside to splash about the bottle. What was Marissa thinking when she made this one?");

            Items["Lesser Health Potion"] = new Item(
                "Small Health Potion",
                MakePictureBox(Resources.lesser_health_potion, new Point(20, 400), itemSize),
                5,
                Item.ItemType.Utility,
                Item.PotionTypes.Healing,
                "This small bottle holds a liquid that swirls with crimson liquid. A brand on the cork bears the logo of Marissa's Cauldron®");

            Items["Strength Potion"] = new Item(
                "Potion of Strength",
                MakePictureBox(Resources.strength_potion, new Point(400, 400), itemSize),
                5,
                Item.ItemType.Utility,
                Item.PotionTypes.Strength,
                "The orange liquid inside has a consistency thicker than honey. It looks safe, but you don't trust drinking anything made by Marissa's Rival.");

            Items["Carpenter Hammer"] = new Item(
                "Carptener's Hammer",
                MakePictureBox(Resources.common_hammer, new Point(500, 650), itemSize),
                5,
                Item.ItemType.Weapon,
                "This hammer has a sturdy handle and a large head. Must have been used to build houses in a nearby village.");

            Items["Lumberjack Axe"] = new Item(
                "Lumberjack's Axe",
                MakePictureBox(Resources.common_axe, new Point(880, 880), itemSize),
                5,
                Item.ItemType.Weapon,
                "This axe has seen better days. There's a few chips in the edge, but it will get the job done.");

            Items["Rusty Sword"] = new Item(
                "Rusty Sword",
                MakePictureBox(Resources.common_sword, new Point(300, 500), itemSize),
                5,
                Item.ItemType.Weapon,
                "As you hold the half rusted blade in your hand, there is no doubt about it. This sword was made by Jorubus, the best blacksmith in the land.");

            Items["Thalian Sword"] = new Item(
                "Thalian Sword",
                MakePictureBox(Resources.rare_sword, new Point(550, 850), itemSize),
                10,
                Item.ItemType.Weapon,
                "The blade seems to hum in your hand as you feel the strength of the sword. On the pommel is the Thalia Family seal.");

            Items["Dagger of Mischief"] = new Item(
                "Dagger of Mischief",
                MakePictureBox(Resources.rare_dagger, new Point(800, 100), itemSize),
                10,
                Item.ItemType.Weapon,
                "This dagger has been involved in many misdeeds. You grow slightly weary of what the previous owner used this for. The dagger has no name engraved on it... Odd.");

            Items["Accuracy Potion"] = new Item(
                "Potion of Accuracy",
                MakePictureBox(Resources.accuracy_potion, new Point(800, 100), itemSize),
                3,
                Item.ItemType.Utility,
                Item.PotionTypes.Accuracy,
                "This liquid seems rigid in this bottle. The cork on top indicates to you that the Allegiance uses this potion as standard issue.");

            // Create Enemies
            Enemies["Minion1"] = new Enemy(
                "Lizard Minion",
                MakePictureBox(Resources.minion, new Point(200, 500), new Size(100, 100)),
                new Minion());

            Enemies["Minion2"] = new Enemy(
                "Lizard Minion",
                MakePictureBox(Resources.minion, new Point(500, 200), new Size(100, 100)),
                new Minion());

            Enemies["Coward"] = new Enemy(
                "Coward",
                MakePictureBox(Resources.coward, new Point(600, 200), new Size(75, 125)),
                new Coward());

            Enemies["Brute"] = new Enemy(
                "Brute",
                MakePictureBox(Resources.brute, new Point(1000, 100), new Size(150, 150)),
                new Brute());

            Enemies["Brute1"] = new Enemy(
                "Brute",
                MakePictureBox(Resources.brute, new Point(1000, 800), new Size(150, 150)),
                new Brute());

            Enemies["Brute2"] = new Enemy(
                "Brute",
                MakePictureBox(Resources.brute, new Point(500, 840), new Size(150, 150)),
                new Brute());

            Enemies["Zombie"] = new Enemy(
                "Zombie",
                MakePictureBox(Resources.zombie, new Point(1000, 100), new Size(400, 150)),
                new Zombie());

            //Enemies["Bees"] = new Enemy(
            //    "Bees",
            //    MakePictureBox(Resources.bees, new Point(1000, 100), new Size(400, 150)),
            //    new Bees());

            Enemies["Lizard Wizard"] = new Enemy(
                "Lizard Wizard",
                MakePictureBox(Resources.wizardlizard, new Point(677, 697), new Size(150, 150)),
                new Mage());

            Enemies["Lizard Wizard1"] = new Enemy(
                "Lizard Wizard",
                MakePictureBox(Resources.wizardlizard, new Point(845, 540), new Size(150, 150)),
                new Mage());

            Enemies["Whelp"] = new Enemy(
                "Whelp",
                MakePictureBox(Resources.whelp, new Point(473, 347), new Size(400, 150)),
                new Whelp());

            Size dragonSize = new Size(Screen.PrimaryScreen.Bounds.Width * 1 / 4, Screen.PrimaryScreen.Bounds.Height * 1 / 4);
            Enemies["Dragon"] = new Enemy(
                "Malek",
                MakePictureBox(Resources.dragonboss, new Point(Screen.PrimaryScreen.Bounds.Width / 2 - dragonSize.Width / 2, Screen.PrimaryScreen.Bounds.Height * 1/12 + 30), dragonSize),
                new Dragon());

            // Create NPCs
            NPCs["Harold"] = new NPC(
                "Harold",
                MakePictureBox(Resources.harold, new Point(150, 150), new Size(75, 100)),
                new Healer());

            NPCs["Tombstone"] = new NPC(
                "Tombstone",
                MakePictureBox(Resources.tombstone, new Point(150, 150), new Size(75, 100)),
                new Tombstone());

            NPCs["Gerald"] = new NPC(
                "Gerald",
                MakePictureBox(Resources.gerald, new Point(300, 300), new Size(100, 100)),
                new Gerald());

            NPCs["Bartholomew"] = new NPC(
                "Bartholomew",
                MakePictureBox(Resources.bartholomew, new Point(300, 300), new Size(75, 100)),
                new Guy());

            // Create Structures
            Structures["wall_bricks"] = new Structure(
                MakePictureBox(Resources.wall_bricks, new Point(500, 500), new Size(20, 100)));

            Bitmap house_long_rot = Resources.house_long;
            house_long_rot.RotateFlip(RotateFlipType.Rotate90FlipNone);

            Bitmap house_long = Resources.house_long;
            Bitmap house_L = Resources.house_L;

            Bitmap house_L_rot90 = Resources.house_L;
            house_L_rot90.RotateFlip(RotateFlipType.Rotate90FlipNone);

            Bitmap house_L_rot180 = Resources.house_L;
            house_L_rot180.RotateFlip(RotateFlipType.Rotate180FlipNone);

            Size longSize = new Size(Terrain.TileSize.Width * 6, Terrain.TileSize.Height * 3);
            Size L_Size = new Size(Terrain.TileSize.Width * 6, Terrain.TileSize.Height * 4);
            Size L_Size_rot90 = new Size(L_Size.Height, L_Size.Width);

            Structures["house_long_rot_1"] = new Structure(
                MakePictureBox(
                    house_long_rot,
                    new Point(Screen.PrimaryScreen.Bounds.Width * 3 / 4 - 100, -100),
                    new Size(longSize.Height, longSize.Width)));

            Structures["house_long_1"] = new Structure(
                MakePictureBox(house_long, new Point(Screen.PrimaryScreen.Bounds.Width / 2, 400), longSize));

            Structures["house_L_1"] = new Structure(
                MakePictureBox(house_L, new Point(Screen.PrimaryScreen.Bounds.Width * 3 / 4, 300), L_Size));

            Structures["house_L_rot180_1"] = new Structure(
                MakePictureBox(house_L_rot180, new Point(Screen.PrimaryScreen.Bounds.Width * 1 / 4, 80), L_Size));


            Structures["VillageWall1"] = new Structure(
                MakePictureBox(Resources.Transparency, new Point((int)Screen.PrimaryScreen.Bounds.Width * 2 / 10, 0), new Size(Resources.wall_bricks.Height, Terrain.TileSize.Width * 7)));
            Structures["VillageWall1"].Pic.BackgroundImage = Resources.wall_bricks;
            Structures["VillageWall1"].Pic.BackgroundImage.RotateFlip(RotateFlipType.Rotate90FlipNone);
            Structures["VillageWall1"].Pic.BackgroundImageLayout = ImageLayout.Tile;

            Structures["VillageWall2"] = new Structure(
                MakePictureBox(Resources.Transparency, new Point((int)Screen.PrimaryScreen.Bounds.Width * 2 / 10, Terrain.TileSize.Width * 12), new Size(Resources.wall_bricks.Height, Screen.PrimaryScreen.Bounds.Height)));
            Structures["VillageWall2"].Pic.BackgroundImage = Resources.wall_bricks;
            Structures["VillageWall2"].Pic.BackgroundImage.RotateFlip(RotateFlipType.Rotate90FlipNone);
            Structures["VillageWall2"].Pic.BackgroundImageLayout = ImageLayout.Tile;


            Structures["LowerVillageWall1"] = new Structure(
    MakePictureBox(Resources.Transparency, new Point((int)Screen.PrimaryScreen.Bounds.Width * 1/ 5 - 10, 0), new Size(Resources.wall_bricks.Height, Terrain.TileSize.Width * 7)));
            Structures["LowerVillageWall1"].Pic.BackgroundImage = Resources.wall_bricks;
            Structures["LowerVillageWall1"].Pic.BackgroundImage.RotateFlip(RotateFlipType.Rotate90FlipNone);
            Structures["LowerVillageWall1"].Pic.BackgroundImageLayout = ImageLayout.Tile;

            Structures["LowerVillageWall2"] = new Structure(
                MakePictureBox(Resources.Transparency, new Point((int)Screen.PrimaryScreen.Bounds.Width * 1/ 5  - 10, Terrain.TileSize.Width * 12), new Size(Resources.wall_bricks.Height, Terrain.TileSize.Width * 2)));
            Structures["LowerVillageWall2"].Pic.BackgroundImage = Resources.wall_bricks;
            Structures["LowerVillageWall2"].Pic.BackgroundImage.RotateFlip(RotateFlipType.Rotate90FlipNone);
            Structures["LowerVillageWall2"].Pic.BackgroundImageLayout = ImageLayout.Tile;


            int x = Structures["LowerVillageWall2"].Pic.Location.X;
            int y = Terrain.TileSize.Width * 14;
            Structures["LowerVillageWall3"] = new Structure(
                MakePictureBox(Resources.Transparency, new Point(x, y), new Size(12 * Terrain.TileSize.Width, Resources.wall_bricks.Height)));
            Structures["LowerVillageWall3"].Pic.BackgroundImage = Resources.wall_bricks;
            Structures["LowerVillageWall3"].Pic.BackgroundImageLayout = ImageLayout.Tile;

            int x_2 = Terrain.TileSize.Width * 4 + Structures["LowerVillageWall3"].Pic.Location.X + Structures["LowerVillageWall3"].Pic.Size.Width;
            int y_2 = Structures["LowerVillageWall3"].Pic.Location.Y;
            Structures["LowerVillageWall4"] = new Structure(
                MakePictureBox(Resources.Transparency, new Point(x_2, y-2), new Size(Screen.PrimaryScreen.Bounds.Width, Resources.wall_bricks.Height)));
            Structures["LowerVillageWall4"].Pic.BackgroundImage = Resources.wall_bricks;
            Structures["LowerVillageWall4"].Pic.BackgroundImageLayout = ImageLayout.Tile;


            int x_3 = 10 * Terrain.TileSize.Width;
            int y_3 = Screen.PrimaryScreen.Bounds.Height * 1 / 4 - 2 * Terrain.TileSize.Width - 40 + Screen.PrimaryScreen.Bounds.Height * 1 / 12;
            Structures["Pillar1"] = new Structure(
                MakePictureBox(Resources.pillar, new Point(x_3, y_3), new Size(Terrain.TileSize.Width * 2, Terrain.TileSize.Width * 2)));

            x_3 = 10 * Terrain.TileSize.Width;
            y_3 = Screen.PrimaryScreen.Bounds.Height * 1 / 2 - 2 * Terrain.TileSize.Width + Screen.PrimaryScreen.Bounds.Height * 1 / 12;
            Structures["Pillar2"] = new Structure(
                MakePictureBox(Resources.pillar, new Point(x_3, y_3), new Size(Terrain.TileSize.Width * 2, Terrain.TileSize.Width * 2)));

            x_3 = 10 * Terrain.TileSize.Width;
            y_3 = Screen.PrimaryScreen.Bounds.Height * 3 / 4 - 2 * Terrain.TileSize.Width + Screen.PrimaryScreen.Bounds.Height * 1 / 12;
            Structures["Pillar3"] = new Structure(
                MakePictureBox(Resources.pillar, new Point(x_3, y_3), new Size(Terrain.TileSize.Width * 2, Terrain.TileSize.Width * 2)));

            x_3 = Terrain.TileSize.Width * 24;
            y_3 = Screen.PrimaryScreen.Bounds.Height * 1 / 4 - 2 * Terrain.TileSize.Width - 40 + Screen.PrimaryScreen.Bounds.Height * 1 / 12;
            Structures["Pillar4"] = new Structure(
                MakePictureBox(Resources.pillar, new Point(x_3, y_3), new Size(Terrain.TileSize.Width * 2, Terrain.TileSize.Width * 2)));

            x_3 = Terrain.TileSize.Width * 24;
            y_3 = Screen.PrimaryScreen.Bounds.Height * 1 / 2 - 2 * Terrain.TileSize.Width + Screen.PrimaryScreen.Bounds.Height * 1 / 12;
            Structures["Pillar5"] = new Structure(
                MakePictureBox(Resources.pillar, new Point(x_3, y_3), new Size(Terrain.TileSize.Width * 2, Terrain.TileSize.Width * 2)));

            x_3 = Terrain.TileSize.Width * 24;
            y_3 = Screen.PrimaryScreen.Bounds.Height * 3 / 4 - 2 * Terrain.TileSize.Width + Screen.PrimaryScreen.Bounds.Height * 1 / 12;
            Structures["Pillar6"] = new Structure(
                MakePictureBox(Resources.pillar, new Point(x_3, y_3), new Size(Terrain.TileSize.Width * 2, Terrain.TileSize.Width * 2)));


            int x_4 = Terrain.TileSize.Width;
            int y_4 = Screen.PrimaryScreen.Bounds.Height * 1 / 6;
            Structures["Gold1"] = new Structure(
                MakePictureBox(Resources.gold_pile1, new Point(x_4, y_4), new Size(Terrain.TileSize.Width * 8, Terrain.TileSize.Width * 4)));

            x_4 = Terrain.TileSize.Width * 2;
            y_4 = Screen.PrimaryScreen.Bounds.Height * 1/2;
            Structures["Gold3"] = new Structure(
                MakePictureBox(Resources.gold_pile3, new Point(x_4, y_4), new Size(Terrain.TileSize.Width * 6, Terrain.TileSize.Width * 5)));

            x_4 = Screen.PrimaryScreen.Bounds.Width - Terrain.TileSize.Width - Terrain.TileSize.Width * 8; ;
            y_4 = Screen.PrimaryScreen.Bounds.Height * 1 / 6 + 100;
            Structures["Gold2"] = new Structure(
                MakePictureBox(Resources.gold_pile2, new Point(x_4, y_4), new Size(Terrain.TileSize.Width * 8, Terrain.TileSize.Width * 4)));

            x_4 = Screen.PrimaryScreen.Bounds.Width - Terrain.TileSize.Width - Terrain.TileSize.Width * 7;
            y_4 = Screen.PrimaryScreen.Bounds.Height * 5 / 8;
            Structures["Gold4"] = new Structure(
                MakePictureBox(Resources.gold_pile4, new Point(x_4, y_4), new Size(Terrain.TileSize.Width * 7, Terrain.TileSize.Width * 4)));

        }

        public static PictureBox MakePictureBox(Bitmap pic, Point location, Size Size)
        {
            return new PictureBox
            {
                Size = Size,
                Location = location,
                Image = pic,
                SizeMode = PictureBoxSizeMode.StretchImage,
                BackColor = Color.Transparent,  
            };
        }

        public static void FontSizing(Label lbl)
        {
            using (Graphics g = lbl.CreateGraphics())
            {
                Size proposedSize = new Size(int.MaxValue, int.MaxValue);
                for (int fontSize = 100; fontSize >= 8; fontSize--)
                {
                    Font testFont = new Font("NSimSun", fontSize);
                    Size textSize = TextRenderer.MeasureText(g, lbl.Text, testFont, proposedSize, TextFormatFlags.WordBreak);

                    if (textSize.Width <= lbl.Width && textSize.Height <= lbl.Height)
                    {
                        lbl.Font = testFont; // Set the font size that fits
                        break;
                    }
                }
            }
        }

        public static void FontSizing(Button but)
        {
            using (Graphics g = but.CreateGraphics())
            {
                Size proposedSize = new Size(int.MaxValue, int.MaxValue);
                for (int fontSize = 100; fontSize >= 8; fontSize--)
                {
                    Font testFont = new Font("NSimSun", fontSize);
                    Size textSize = TextRenderer.MeasureText(g, but.Text, testFont, proposedSize, TextFormatFlags.WordBreak);

                    if (textSize.Width <= but.Width && textSize.Height <= but.Height)
                    {
                        but.Font = testFont; // Set the font size that fits
                        break;
                    }
                }
            }
        }
    }
}
