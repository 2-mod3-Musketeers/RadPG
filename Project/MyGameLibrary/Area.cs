using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Fall2020_CSC403_Project.code;
using MyGameLibrary.Properties;

namespace MyGameLibrary
{
    public class Area
    {

        public List<Enemy> Enemies { get; set; }
        public List<NPC> npcs { get; set; }
        public List<Item> Items { get; set; }
        public List<Structure> Structures { get; set; }
        


        public bool Visited { get; set; }

        public String AreaName { get; set; }


        public Dictionary<Direction, TravelSign> TravelSigns { get; set; }

        public Dictionary<Direction, int> AdjacentAreas { get; set; }

        public Terrain Terrain;


        public Area(String AreaName, int Seed, double SeedAmp)
        {
            Terrain = new Terrain(Seed, SeedAmp);
            Enemies = new List<Enemy>();
            npcs = new List<NPC>();
            Items = new List<Item>();
            Structures = new List<Structure>();
            AdjacentAreas = new Dictionary<Direction, int>();
            TravelSigns = new Dictionary<Direction, TravelSign>();
            this.AreaName = AreaName;
            this.Visited = false;
        }
        public void AddStructure(Structure structure)
        {
            this.Structures.Add(structure);

        }

        public void AddItem(Item item)
        {
            this.Items.Add(item);
        }

        public void AddEnemy(Enemy enemy)
        {
            this.Enemies.Add(enemy);
        }

        public void AddNPC(NPC npc)
        {
            this.npcs.Add(npc);
        }

        public void SetAdjacentArea(Direction direction, int area)
        {
            this.AdjacentAreas[direction] = area;
        }

        public void SetTravelSign(Direction direction, TravelSign sign)
        {
            this.TravelSigns[direction] = sign;
        }


    }

    public enum Direction
    {
        None,
        Left,
        Right,
        Up,
        Down,
    }
}
