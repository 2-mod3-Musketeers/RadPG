using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Runtime.CompilerServices;
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

        public static Dictionary<string, bool> Objectives = new Dictionary<string, bool>();

        public static void PopulateWorld()
        {
            // Create Items
            Items["Sting"] = new Item(
                "Sting",
                MakePictureBox(Resources.common_dagger, new Point(500, 300), itemSize),
                5,
                Item.ItemType.Weapon,
                Item.WeaponType.Dagger,
                "This small dagger feels sturdy in your hands. As you look at the hilt, you see a name etched into the handle by a previous owner: \"Garn Thalia\"");


            Items["Dagger of Mischief"] = new Item(
               "Dagger of Mischief",
               MakePictureBox(Resources.rare_dagger, new Point(Terrain.TileSize.Width * 6, Terrain.TileSize.Width * 5), itemSize),
               10,
               Item.ItemType.Weapon,
               Item.WeaponType.Dagger,
               "This dagger has been involved in many misdeeds. You grow slightly weary of what the previous owner used this for. The dagger has no name engraved on it... Odd.");


            Items["Shabby Armor"] = new Item(
                "Shabby Armor",
                MakePictureBox(Resources.common_armor, new Point(880, 800), itemSize),
                5, Item.ItemType.Armor,
                "This armor looks thin and flexible. Putting it on, there seems to be a gap in the armor along the spine. Since when did people have ridges on their back?");

            Items["Potion of Speed"] = new Item(
                "Potion of Speed",
                MakePictureBox(Resources.speed_potion, new Point(Screen.PrimaryScreen.Bounds.Width * 5 / 8, Terrain.TileSize.Width * 3), itemSize),
                10,
                Item.ItemType.Utility,
                Item.EffectType.Speed,
                "This bottle acts unnatural as a simple tremor will cause the liquid inside to splash about the bottle. What was Marissa thinking when she made this one?");

            Items["Small Health Potion"] = new Item(
                "Small Health Potion",
                MakePictureBox(Resources.lesser_health_potion, new Point(20, 400), itemSize),
                5,
                Item.ItemType.Utility,
                Item.EffectType.Healing,
                "This small bottle holds a liquid that swirls with crimson liquid. A brand on the cork bears the logo of Marissa's Cauldron");

            Items["Potion of Strength"] = new Item(
                "Potion of Strength",
                MakePictureBox(Resources.strength_potion, new Point(Screen.PrimaryScreen.Bounds.Width - Terrain.TileSize.Width * 6, Terrain.TileSize.Width * 5), itemSize),
                5,
                Item.ItemType.Utility,
                Item.EffectType.Strength,
                "The orange liquid inside has a consistency thicker than honey. It looks safe, but you don't trust drinking anything made by Marissa's Rival.");

            Items["Carpenter's Hammer"] = new Item(
                "Carptener's Hammer",
                MakePictureBox(Resources.common_hammer, new Point(700, 600), itemSize),
                5,
                Item.ItemType.Weapon,
                Item.WeaponType.War,
                "This hammer has a sturdy handle and a large head. Must have been used to build houses in a nearby village.");

            Items["Lumberjack's Axe"] = new Item(
                "Lumberjack's Axe",
                MakePictureBox(Resources.common_axe, new Point(Screen.PrimaryScreen.Bounds.Width / 2 + 30, Screen.PrimaryScreen.Bounds.Height / 2 + 10), itemSize),
                10,
                Item.ItemType.Weapon,
                Item.WeaponType.War,
                "This axe has seen better days. There's a few chips in the edge, but it will get the job done.");

            Items["Rusty Sword"] = new Item(
                "Rusty Sword",
                MakePictureBox(Resources.common_sword, new Point(300, 500), itemSize),
                5,
                Item.ItemType.Weapon,
                Item.WeaponType.Sword,
                "As you hold the half rusted blade in your hand, there is no doubt about it. This sword was made by Jorubus, the best blacksmith in the land.");

            Items["Thalian Sword"] = new Item(
                "Thalian Sword",
                MakePictureBox(Resources.rare_sword, new Point(550, 850), itemSize),
                10,
                Item.ItemType.Weapon,
                Item.WeaponType.Sword,
                "The blade seems to hum in your hand as you feel the strength of the sword. On the pommel is the Thalia Family seal.");

           
            Items["Potion of Accuracy"] = new Item(
                "Potion of Accuracy",
                MakePictureBox(Resources.accuracy_potion, new Point(Terrain.TileSize.Width * 5, Terrain.TileSize.Width * 8), itemSize),
                3,
                Item.ItemType.Utility,
                Item.EffectType.Accuracy,
                "This liquid seems rigid in this bottle. The cork on top indicates to you that the Allegiance uses this potion as standard issue.");

            Items["Large Health Potion"] = new Item(
                "Greather Health Potion",
                MakePictureBox(Resources.greather_health_potion, new Point(Terrain.TileSize.Width * 10, Terrain.TileSize.Width * 15), itemSize),
                15,
                Item.ItemType.Utility,
                Item.EffectType.Healing,
                "This large bottle holds a liquid that swirls with crimson liquid. The handiwork of its contents are the expertise of Cassandra.");


            // Create Enemies
            Enemies["Minion1"] = new Enemy(
                "Lizard Minion",
                MakePictureBox(Resources.minion, new Point(Screen.PrimaryScreen.Bounds.Width / 10, Screen.PrimaryScreen.Bounds.Height * 4 / 5), new Size(100, 100)),
                new Minion());

            Enemies["Minion2"] = new Enemy(
                "Lizard Minion",
                MakePictureBox(Resources.minion, new Point(Screen.PrimaryScreen.Bounds.Width * 3 / 5, Screen.PrimaryScreen.Bounds.Height / 2), new Size(100, 100)),
                new Minion());

            Enemies["Minion3"] = new Enemy(
                "Lizard Minion",
                MakePictureBox(Resources.minion, new Point(Screen.PrimaryScreen.Bounds.Width / 2, Screen.PrimaryScreen.Bounds.Height * 4 / 5), new Size(100, 100)),
                new Minion());

            Enemies["Minion4"] = new Enemy(
                "Lizard Minion",
                MakePictureBox(Resources.minion, new Point(Screen.PrimaryScreen.Bounds.Width * 3 / 5, Screen.PrimaryScreen.Bounds.Height / 2), new Size(100, 100)),
                new Minion());

            Enemies["Minion5"] = new Enemy(
                "Lizard Minion",
                MakePictureBox(Resources.minion, new Point(Screen.PrimaryScreen.Bounds.Width * 2 / 5, Screen.PrimaryScreen.Bounds.Height * 1 / 3), new Size(100, 100)),
                new Minion());

            Enemies["Coward"] = new Enemy(
                "Coward",
                MakePictureBox(Resources.coward, new Point(Screen.PrimaryScreen.Bounds.Width / 2, Screen.PrimaryScreen.Bounds.Height * 3 /5), new Size(75, 125)),
                new Coward());

            Enemies["Brute"] = new Enemy(
                "Brute",
                MakePictureBox(Resources.brute, new Point(Screen.PrimaryScreen.Bounds.Width * 4 / 5, Screen.PrimaryScreen.Bounds.Height / 10), new Size(150, 150)),
                new Brute());

            Enemies["Brute1"] = new Enemy(
                "Brute",
                MakePictureBox(Resources.brute, new Point(Screen.PrimaryScreen.Bounds.Width * 3 / 5, Screen.PrimaryScreen.Bounds.Height * 3 / 5), new Size(150, 150)),
                new Brute());

            Enemies["Brute2"] = new Enemy(
                "Brute",
                MakePictureBox(Resources.brute, new Point(Screen.PrimaryScreen.Bounds.Width * 4/5, Screen.PrimaryScreen.Bounds.Height / 10), new Size(150, 150)),
                new Brute());

            Enemies["Brute3"] = new Enemy(
                "Brute",
                MakePictureBox(Resources.brute, new Point(Screen.PrimaryScreen.Bounds.Width / 10, Screen.PrimaryScreen.Bounds.Height / 2), new Size(150, 150)),
                new Brute());

            Enemies["Brute4"] = new Enemy(
                "Brute",
                MakePictureBox(Resources.brute, new Point(Screen.PrimaryScreen.Bounds.Width / 2, Screen.PrimaryScreen.Bounds.Height * 3 / 5), new Size(150, 150)),
                new Brute());

            Enemies["Brute5"] = new Enemy(
                "Brute",
                MakePictureBox(Resources.brute, new Point(Screen.PrimaryScreen.Bounds.Width * 9 / 10, Screen.PrimaryScreen.Bounds.Height * 2 / 3), new Size(150, 150)),
                new Brute());

            Enemies["Zombie"] = new Enemy(
                "Zombie",
                MakePictureBox(Resources.zombie, new Point(Screen.PrimaryScreen.Bounds.Width / 2, Screen.PrimaryScreen.Bounds.Height / 2), new Size(400, 150)),
                new Zombie());


            Enemies["Lizard Wizard"] = new Enemy(
                "Lizard Wizard",
                MakePictureBox(Resources.wizardlizard, new Point(Screen.PrimaryScreen.Bounds.Width / 2, Screen.PrimaryScreen.Bounds.Height / 2), new Size(150, 150)),
                new Mage());

            Enemies["Lizard Wizard1"] = new Enemy(
                "Lizard Wizard",
                MakePictureBox(Resources.wizardlizard, new Point(Screen.PrimaryScreen.Bounds.Width / 10, Screen.PrimaryScreen.Bounds.Height * 4 / 5), new Size(150, 150)),
                new Mage());
            
            Enemies["Lizard Wizard2"] = new Enemy(
                "Lizard Wizard",
                MakePictureBox(Resources.wizardlizard, new Point(Screen.PrimaryScreen.Bounds.Width * 4 / 5, Screen.PrimaryScreen.Bounds.Height * 4 / 5), new Size(150, 150)),
                new Mage());
            
            Enemies["Lizard Wizard3"] = new Enemy(
                "Lizard Wizard",
                MakePictureBox(Resources.wizardlizard, new Point(Screen.PrimaryScreen.Bounds.Width / 5 + 20, Screen.PrimaryScreen.Bounds.Height / 2), new Size(150, 150)),
                new Mage());
            
            Enemies["Lizard Wizard4"] = new Enemy(
                "Lizard Wizard",
                MakePictureBox(Resources.wizardlizard, new Point(Screen.PrimaryScreen.Bounds.Width / 10, Screen.PrimaryScreen.Bounds.Height / 10), new Size(150, 150)),
                new Mage());

            Enemies["Lizard Wizard5"] = new Enemy(
                "Lizard Wizard",
                MakePictureBox(Resources.wizardlizard, new Point(Screen.PrimaryScreen.Bounds.Width / 2, Screen.PrimaryScreen.Bounds.Height / 2), new Size(150, 150)),
                new Mage());

            Enemies["Lizard Wizard6"] = new Enemy(
                "Lizard Wizard",
                MakePictureBox(Resources.wizardlizard, new Point(Screen.PrimaryScreen.Bounds.Width / 10, Screen.PrimaryScreen.Bounds.Height * 4 / 5), new Size(150, 150)),
                new Mage());

            Enemies["Whelp1"] = new Enemy(
                "Whelp",
                MakePictureBox(Resources.whelp, new Point(Screen.PrimaryScreen.Bounds.Width / 2, Screen.PrimaryScreen.Bounds.Height / 2), new Size(100, 100)),
                new Whelp());
            
            Enemies["Whelp2"] = new Enemy(
                "Whelp",
                MakePictureBox(Resources.whelp, new Point(Screen.PrimaryScreen.Bounds.Width / 8, Screen.PrimaryScreen.Bounds.Height * 2 / 5), new Size(100, 100)),
                new Whelp());
            
            Enemies["Whelp3"] = new Enemy(
                "Whelp",
                MakePictureBox(Resources.whelp, new Point(Screen.PrimaryScreen.Bounds.Width / 10, Screen.PrimaryScreen.Bounds.Height * 4 / 5), new Size(100, 100)),
                new Whelp());

            Size dragonSize = new Size(Screen.PrimaryScreen.Bounds.Width * 1 / 4, Screen.PrimaryScreen.Bounds.Height * 1 / 4);
            Enemies["Dragon"] = new Enemy(
                "Malek",
                MakePictureBox(Resources.dragonboss, new Point(Screen.PrimaryScreen.Bounds.Width / 2 - dragonSize.Width / 2, Screen.PrimaryScreen.Bounds.Height * 1/12 + 30), dragonSize),
                new Dragon());
            Enemies["Dragon"].canFlee = false;

            // Create NPCs
            NPCs["Harold"] = new NPC(
                "Harold",
                MakePictureBox(Resources.harold, new Point(Screen.PrimaryScreen.Bounds.Width * 9 /10, Screen.PrimaryScreen.Bounds.Height / 10), new Size(75, 100)),
                new Healer()
            );
            NPCs["Harold"].Dialog = "How's it going? Name's Harold. If you go kill some lizards, let me know. I want in.";
            NPCs["Harold"].ConverseImage = Resources.harold_converse;

            NPCs["Tombstone"] = new NPC(
                "Tombstone",
                MakePictureBox(Resources.tombstone, new Point(Screen.PrimaryScreen.Bounds.Width * 5 / 7, Screen.PrimaryScreen.Bounds.Height -  Terrain.TileSize.Width * 8), new Size(75, 100)),
                new Tombstone());
            NPCs["Tombstone"].CanJoinParty = false;
            NPCs["Tombstone"].InviteRejection = "Woah there, pal, I'm not sure we're that tight yet.";
            NPCs["Tombstone"].ConverseImage = Resources.tombstone_converse;

            NPCs["Gerald"] = new NPC(
                "Gerald",
                MakePictureBox(Resources.gerald, new Point(Screen.PrimaryScreen.Bounds.Width / 2, Screen.PrimaryScreen.Bounds.Height / 2), new Size(150, 100)),
                new Gerald());
            NPCs["Gerald"].Dialog = "Greetings, I am Gerald. \nI wish to vanquish the lizards and dragon that plague our land. We cannot endure this much longer. \nI wish to aid in the vanquish of our foes.";
            NPCs["Gerald"].ConverseImage = Resources.gerald_converse;

            NPCs["Reginald"] = new NPC(
                "Reginald",
                MakePictureBox(Resources.reginald, new Point(Screen.PrimaryScreen.Bounds.Width - Terrain.TileSize.Width * 4, Terrain.TileSize.Width * 7 ), new Size(75, 100)),
                new Guy());
            NPCs["Reginald"].CanJoinParty = false;
            NPCs["Reginald"].Dialog = "It's a me, Reginald. I make-a the best food in the village.\nIf you want-a a taste its gonna cost you though. People like me don't-a work for free!";
            NPCs["Reginald"].InviteRejection = "What? Fight? I can't-a do that! Who's gonna cook all the food around-a here?";
            NPCs["Reginald"].ConverseImage = Resources.reginald_converse;

            NPCs["Bobby"] = new NPC(
                "Bobby",
                MakePictureBox(Resources.bobby, new Point(Terrain.TileSize.Width * 16, Terrain.TileSize.Width * 4), new Size(75, 100)),
                new Guy());
            NPCs["Bobby"].CanJoinParty = false;
            NPCs["Bobby"].Dialog = "Eugene is so cool! I like talking with him! In fact, when I grow up I wanna be just like him! He's just so cool, I wanna go talk with him again!";
            NPCs["Bobby"].InviteRejection = "Sorry, I would but I'd rather go talk to Eugene...";
            NPCs["Bobby"].ConverseImage = Resources.bobby_converse;

            NPCs["Eugene"] = new NPC(
                "Eugene",
                MakePictureBox(Resources.eugene, new Point(Terrain.TileSize.Width * 23, Terrain.TileSize.Width * 7), new Size(75, 100)),
                new Guy());
            NPCs["Eugene"].CanJoinParty = false;
            NPCs["Eugene"].Dialog = "I hate Bobby.";
            NPCs["Eugene"].InviteRejection = "Eh, seems like a lot of work. Plus, I don't wanna risk that Bobby kid seeing me.";
            NPCs["Eugene"].ConverseImage = Resources.eugene_converse;

            NPCs["Hank"] = new NPC(
                "Hank",
                MakePictureBox(Resources.hank, new Point(Screen.PrimaryScreen.Bounds.Width - Terrain.TileSize.Width * 8, Terrain.TileSize.Width * 4), new Size(75, 100)),
                new Guy());
            NPCs["Hank"].CanJoinParty = false;
            NPCs["Hank"].Dialog = "I'll tell ya what, dang it, these dadgum lizards just won't quit lurkin' 'round my dang ol' village.\nI mean, I work hard, sell propane and propane accessories, and I come home to find these dang lizards causin' a ruckus.\r\n\r\nBobby, I swear, it's like they got no respect for personal space. I've tried talkin' to 'em, but they just stare at me with those beady eyes.\nI don't know what they want, but they need to git gone and let me enjoy my propane-filled peace.\nIt's downright frustratin', I'll tell you what.";
            NPCs["Hank"].InviteRejection = "Now hold on there, I appreciate the enthusiasm for takin' care of them lizards,\nbut I just heard there might be a dragon lurkin' 'round these parts.\nNow, I ain't one to back down from a challenge,\nbut dragons are a whole different kettle of fish, I'll tell you what.";
            NPCs["Hank"].ConverseImage = Resources.hank_converse;

            NPCs["Bartholomew"] = new NPC(
                "Bartholomew",
                MakePictureBox(Resources.bartholomew, new Point(300, 300), new Size(75, 100)),
                new Guy());
            NPCs["Bartholomew"].CanJoinParty = false;
            NPCs["Bartholomew"].InviteRejection = "I definitely can't fight anyone, but I did hear of some good loot washed up on the beach from a sunken pirate ship";
            NPCs["Bartholomew"].ConverseImage = Resources.bartholomew_converse;

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
                MakePictureBox(house_long, new Point(Screen.PrimaryScreen.Bounds.Width / 2, Terrain.TileSize.Width * 10), longSize));

            Structures["house_L_1"] = new Structure(
                MakePictureBox(house_L, new Point(Screen.PrimaryScreen.Bounds.Width * 3 / 4, Terrain.TileSize.Width * 7), L_Size));

            Structures["house_L_rot180_1"] = new Structure(
                MakePictureBox(house_L_rot180, new Point(Screen.PrimaryScreen.Bounds.Width * 1 / 4, Terrain.TileSize.Width * 2), L_Size));


            Structures["house_L_rot_90_1"] = new Structure(
                MakePictureBox(house_L_rot90, new Point(Screen.PrimaryScreen.Bounds.Width * 3 / 4, Screen.PrimaryScreen.Bounds.Height * 1/6), L_Size_rot90));





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





            Objectives["spoke_to_tombstone"] = false;

            Objectives["learned_of_dragon1"] = false;
            Objectives["learned_of_dragon2"] = false;

            Objectives["tombstone_ready"] = false;
            Objectives["tombstone_leading"] = false;
            Objectives["cave_unlocked"] = false;

            Objectives["cleared_harmony_plains"] = false;
            Objectives["cleared_windy"] = false;
            Objectives["cleared_ruined_village"] = false;

            Objectives["killed_dragon"] = false;

            Objectives["tombstone_killed"] = false;
            Objectives["tombstone_revived"] = false;

            Objectives["visited_leader_tombstone"] = false;

            Game.Objectives["spoke_to_tm_after_learn_dragon"] = false;


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

        public static void CheckObjectives()
        {

            if (Objectives["cleared_harmony_plains"] == false && Game.Areas[7].Enemies.Count == 0 && Game.Areas[7].Visited)
            {
                Objectives["cleared_harmony_plains"] = true;
            }
            if (Objectives["cleared_windy"] == false && Game.Areas[6].Enemies.Count == 0 && Game.Areas[6].Visited)
            {
                Objectives["cleared_windy"] = true;

            }
            if (Objectives["cleared_ruined_village"] == false && Game.Areas[1].Enemies.Count == 0 && Game.Areas[1].Visited)
            {
                Objectives["cleared_ruined_village"] = true;
            }


            if (Objectives["tombstone_ready"] && Game.Objectives["spoke_to_tm_after_learn_dragon"] && !Game.Objectives["killed_dragon"])
            {
                if (!Game.Areas[0].npcs.Any(guy => guy.Name == "Tombstone"))
                {
                    Game.Areas[0].npcs.Add(NPCs["Tombstone"]);
                }

                Objectives["cave_unlocked"] = true;
                Objectives["tombstone_ready"] = false;

            }
            else if (Objectives["tombstone_killed"] && !Objectives["tombstone_revived"])
            {
                NPCs["Tombstone"].Dialog = "Here lies, Tombstone";
                NPCs["Tombstone"].Pic.Image = Resources.tombstone_tombstone;
                NPCs["Tombstone"].ConverseImage = Resources.tombstone_tombstone_converse;
                NPCs["Tombstone"].InviteRejection = "RIP I'm literally a tombstone";

            }
            else if (Objectives["killed_dragon"] && Objectives["tombstone_revived"] && !Objectives["visited_leader_tombstone"])
            {
                Game.CurrentArea.TravelSigns[Direction.Right].Collider.Enable();
                NPCs["Tombstone"].Dialog = "Why am I alive again? I was cursed to be resurrected as long as dragon was alive.\nI guess I came to life just in time! Thanks for freeing me.\nHey, let's not mention all of the previous adventurer's I've brought here... thanks.";
                NPCs["Tombstone"].Pic.Image = Resources.tombstone;
                NPCs["Tombstone"].ConverseImage = Resources.tombstone_converse;
                NPCs["Tombstone"].InviteRejection = "Hey, I'll think about it now! Come back later!";

            }
            else if ((!Objectives["learned_of_dragon1"] || !Objectives["learned_of_dragon2"]) && !Objectives["spoke_to_tombstone"])
            {
                NPCs["Tombstone"].Dialog = "Hey, name's Tombstone.\nWhy am I not attacking you? I don't like those other lizards...\nbut don't go tell them that.\nJust run away from me and pretend I attacked you";
            }
            else if (Objectives["learned_of_dragon1"] && Objectives["learned_of_dragon2"] && !Objectives["spoke_to_tombstone"])
            {
                NPCs["Tombstone"].Dialog = "Hey, name's Tombstone.\nWhy am I not attacking you? I don't like those other lizards...\nbut don't go tell them that.\nHey! I know where that Dragon is... why don't I show you the way? Meet you at Malek's Mountain";
                Objectives["tombstone_ready"] = true;
            }
            else if ((!Objectives["learned_of_dragon1"] || !Objectives["learned_of_dragon2"]) && Objectives["spoke_to_tombstone"] && !Objectives["killed_dragon"] && !Objectives["tombstone_ready"])
            {
                NPCs["Tombstone"].Dialog = "Hey there again, back for more?\nHa, I'll let you go again this time";

            }
            else if (Game.CurrentArea.AreaName == "Malek's Mountain" && Objectives["cave_unlocked"])
            {

                Game.Areas[2].npcs.Remove(NPCs["Tombstone"]);

                Game.Areas[0].TravelSigns[Direction.Left].Pic.Image = Resources.cave_entrance_open;
                Game.Areas[0].TravelSigns[Direction.Left].Collider.Enable();
                NPCs["Tombstone"].Dialog = "Hey, I moved that big stone in front of the cave for you, it's right over there";

            }
            else if (Objectives["learned_of_dragon1"] && Objectives["learned_of_dragon2"] && Objectives["spoke_to_tombstone"] && !Objectives["killed_dragon"] && Game.CurrentArea.AreaName != "Malek's Lair")
            {
                NPCs["Tombstone"].Dialog = "Hey, I know where that Dragon is ... why don't I show you the way? Meet you at Malek's Mountain";
                Objectives["tombstone_ready"] = true;

            }
            else if (!Objectives["killed_dragon"] && Game.CurrentArea.AreaName == "Malek's Lair" && !Objectives["tombstone_killed"])
            {
                NPCs["Tombstone"].Dialog = "I'm sorry I have to do this to you pal, but a lizard's gotta pay the bills.";
            }
            else if (Objectives["killed_dragon"] && Objectives["tombstone_revived"] && Objectives["visited_leader_tombstone"])
            {
                NPCs["Tombstone"].Dialog = "I am tombstone, newly declared owner of this cave and its gold... don't worry friend. I won't forget you.";
                NPCs["Tombstone"].CanJoinParty = true;
            }
            else
            {
                NPCs["Tombstone"].Dialog = "Hey there!";
            }


            if (!Objectives["cleared_harmony_plains"] && !Objectives["cleared_ruined_village"] && !Objectives["cleared_windy"])
            {
                NPCs["Bartholomew"].Dialog = "Those darn pesky lizard people have made it to harmony plains! Some adventurers are fighting them, but I'm not sure they have what it takes.";
            }
            else if (Objectives["cleared_harmony_plains"] && !Objectives["cleared_windy"])
            {
                NPCs["Bartholomew"].Dialog = "Great job clearing Harmony Plains.. \nNow we need to drive them further away and out of the Windy Plateau!";
            }
            else if (!Objectives["cleared_harmony_plains"] && (Objectives["cleared_ruined_village"] || Objectives["cleared_windy"]))
            {
                NPCs["Bartholomew"].Dialog = "Great work! I wish those lizards would leave Harmony Plains though";
            }
            else if (!Objectives["cleared_ruined_village"] && (Objectives["cleared_windy"] || Objectives["cleared_harmony_plains"]))
            {
                NPCs["Bartholomew"].Dialog = "Wonderful work over there, now if only someone would get rid of the lizards in the old village... They moved in after the dragon destroyed it.";
                Objectives["learned_of_dragon1"] = true;
            }
            else if (!Objectives["cleared_harmony_plains"] && Objectives["cleared_ruined_village"] && Objectives["cleared_windy"] && Objectives["killed_dragon"])
            {
                NPCs["Bartholomew"].Dialog = "Fantatic work getting rid of the Dragon! Without their leader, it should be easy to wipe out the rest of the reptiles from the surrounding land";
            }
            else if (Objectives["cleared_harmony_plains"] && Objectives["cleared_ruined_village"] && Objectives["cleared_windy"] && !Objectives["killed_dragon"])
            {
                NPCs["Bartholomew"].Dialog = "You're doing great. We should attack them at the source, the dragon.";
                Objectives["learned_of_dragon1"] = true;

            }
            else if (Objectives["cleared_harmony_plains"] && Objectives["cleared_ruined_village"] && Objectives["cleared_windy"] && Objectives["killed_dragon"])
            {
                NPCs["Bartholomew"].Dialog = "You've done great work for this land.\nWe thank you";
            } else
            {
                NPCs["Bartholomew"].Dialog = "Hey there, nothing for me to complain about today yet.";
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
