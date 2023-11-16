using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MyGameLibrary;
using Fall2020_CSC403_Project.code;
using System.Drawing;
using System.Windows.Forms;

namespace MyGameLibrary
{
    public class Item : Entity
    {
        public int Stat { get; private set; }
        public string Desc { get; private set; }
        public ItemType Type { get; private set; }

        public PotionTypes Potion { get; private set; }


        //public Item(string Name, PictureBox Pic, int Stat, ItemType Type, string desc) : base(Name, Pic)
        //{
        //    this.Stat = Stat;
        //    this.Type = Type;
        //    this.Desc = desc;
        //    Pic.Size = new Size(50, 50);
        //}

        public Item(string Name, PictureBox Pic, int Stat, ItemType Type, string desc, PotionTypes Potion = PotionTypes.None) : base(Name, Pic)
        {
            this.Stat = Stat;
            this.Type = Type;
            this.Potion = Potion;
            this.Desc = desc;
            this.Pic.Size = new Size(50, 50);
        }


        public enum ItemType
        {
            Weapon,
            Armor,
            Utility
        }

        public enum PotionTypes
        {
            None,
            Healing,
            Speed,
            Strength,
            Accuracy
        }

        public Item Clone()
        {
            Item Clone = new Item(this.Name, this.Pic, this.Stat, this.Type, this.Desc);
            return Clone;
        }

    }



}
