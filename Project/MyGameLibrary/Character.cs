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

		public string archetypeName;
		
		public int defense;

		public int damage;

		public int speed;

		public Random dice;

		public int Health { get; private set; }
		public int MaxHealth { get; private set; }


		public Character(string Name, PictureBox Pic, Archetype archetype) : base(Name, Pic)
		{
			if (archetype == null)
			{
				switch (this.archetypeName)
				{
					case "Tank":
						this.archetype = new Tank();
						break;
					case "Rogue":
                        this.archetype = new Rogue();
                        break;
                    case "Swordsman":
                        this.archetype = new Swordsman();
                        break;
                    case "Healer":
                        this.archetype = new Healer();
                        break;
                    case "Minion":
                        this.archetype = new Minion();
                        break;
                    case "Coward":
                        this.archetype = new Coward();
                        break;
                    case "Brute":
                        this.archetype = new Brute();
                        break;
                    case "Zombie":
                        this.archetype = new Zombie();
                        break;
                    case "Bees":
                        this.archetype = new Bees();
                        break;
                    case "Mage":
                        this.archetype = new Mage();
                        break;
                    case "Whelp":
                        this.archetype = new Whelp();
                        break;
                    case "Boss":
                        this.archetype = new Boss();
                        break;
                    case "Dragon":
                        this.archetype = new Dragon();
                        break;
					default:
						this.archetype = new Rogue();
						break;
                }

			}
			else
			{
                this.archetype = archetype;
            }			
			this.MaxHealth = this.archetype.baseMaxHealth;
            this.damage = this.archetype.baseDamage;
            this.defense = this.archetype.baseDefense;
            this.speed = this.archetype.baseSpeed;
            this.Health = this.MaxHealth;
			this.Inventory = new Inventory();
			this.dice = new Random();
			this.archetypeName = this.archetype.name;

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
