using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Fall2020_CSC403_Project.code
{
    public class NPC : Character
    {

        public NPC(string name, PictureBox pic, Archetype archetype) : base(name, pic, archetype)
        { 
        }

        public NPC Clone()
        {
            NPC Clone = new NPC(this.Name, this.Pic, this.archetype);
            Clone.Inventory = this.Inventory.Clone();
            Clone.Inventory.SaveNames();
            Clone.defense = this.defense;
            Clone.damage = this.damage;
            Clone.speed = this.speed;
            Clone.dice = this.dice;
            Clone.Health = this.Health;
            Clone.MaxHealth = this.MaxHealth;
            return Clone;
        }
    }
}
