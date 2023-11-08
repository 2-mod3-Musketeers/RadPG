using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Drawing;
using System.Threading.Tasks;
using System.Windows.Forms;
using MyGameLibrary;
using System.Runtime.Remoting.Messaging;
using static MyGameLibrary.Item;
using static System.Windows.Forms.VisualStyles.VisualStyleElement.Rebar;


#pragma warning disable 1591 // use this to disable comment warnings

namespace Fall2020_CSC403_Project.code
{
    public class Character : Entity
    {

        public event Action<int> AttackEvent;

		public Inventory Inventory { get; set; }

		public Archetype archetype;
		
		public int defense;

		public int damage;

		public int speed;

		public Random dice;

		public int Health { get; private set; }
		public int MaxHealth { get; private set; }


		public Character(string Name, PictureBox Pic, Archetype archetype) : base(Name, Pic)
		{
            this.archetype = archetype;
			this.MaxHealth = archetype.baseMaxHealth;
            this.damage = archetype.baseDamage;
            this.defense = archetype.baseDefense;
            this.speed = archetype.baseSpeed;
            this.Health = MaxHealth;
			this.Inventory = new Inventory();
			this.dice = new Random();
		}
		
		public string OnAttack(int defense)
		{
            int hit = this.dice.Next(1, 21);
			string log;
			int damage;
			log = this.Name + " hit : " + hit;
            if (hit == 20)
			{
				damage = this.damage * 2 + this.dice.Next(1, this.archetype.baseDamage + 1);
                AttackEvent(damage);
				log = this.Name + " criticaly hit for " + damage + "!";
			}
            else if (hit + this.archetype.hitMod >= defense)
			{
				damage = this.damage + this.dice.Next(1, this.archetype.baseDamage + 1);
                AttackEvent(damage);
                log = this.Name + " hits for " + damage;
            }
			else
			{
				log = this.Name + " missed!";
			}
			return log;
        }

        public void TakeDamage(int amount)
		{
			Health -= amount;
		}

		public void GiveHealth(int amount)
        {
            Health += amount;
        }

        public void RestoreHealth()
        {
            this.Health = this.MaxHealth;
        }

		public void UpdateStats()
		{
			if (this.Inventory.Armor != null)
			{
                this.defense = this.archetype.baseDefense + this.Inventory.Armor.Stat;

            }
			if (this.Inventory.Weapon != null)
			{
                this.damage = this.archetype.baseDamage + this.Inventory.Weapon.Stat;

            }
        }

		public void ApplyEffect(PotionTypes Potion, int stat)
		{
			switch (Potion)
			{
				case PotionTypes.Healing:
					this.Health += stat;
					if (this.Health > this.MaxHealth)
					{
						this.Health = this.MaxHealth;
					}
					break;
				case PotionTypes.Strength:
					this.damage += stat;
					break;
				case PotionTypes.Speed:
					this.speed += stat;
					break;
				case PotionTypes.Accuracy:
					this.archetype.hitMod += stat;
					break;
				default:
					break;
			}
		}

		public void RemoveEffect()
		{
			this.damage = this.archetype.baseDamage;
			this.speed = this.archetype.baseSpeed;
		}
        
    }
}
