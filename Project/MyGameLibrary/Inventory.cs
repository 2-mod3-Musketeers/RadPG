using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Fall2020_CSC403_Project.code;
using MyGameLibrary;
using Fall2020_CSC403_Project;
using static Fall2020_CSC403_Project.code.Entity;
using System.Drawing;

namespace MyGameLibrary
{
    public class Inventory
    {
        public Item Weapon { get; private set; }
        public Item Armor { get; private set; }
        public Item Utility { get; private set; }
        public Item[] Backpack { get; private set; }

        
        public Inventory()
        {
            this.Backpack = new Item[9];
        }

        public Inventory(Item Weapon, Item Armor, Item Utility, Item[] Backpack)
        {
            this.Weapon = Weapon;
            this.Armor = Armor;
            this.Utility = Utility;
            if (Backpack != null)
            {
                if (Backpack.Length <= 9)
                {
                    this.Backpack = Backpack;
                } else
                {
                    throw new Exception("Backpack size limited to 6 Items");
                }
            } else
            {
                this.Backpack = new Item[9];
            }
        }

        public bool BackpackIsFull()
        {
            return this.Backpack[this.Backpack.Length - 1] != null;
        }

        public void UnEquipWeapon(Position position, Facing facing, Point PanOffset)
        {
            EquipWeapon(null, position, facing, PanOffset);
        }

        public void UnEquipArmor(Position position, Facing facing, Point PanOffset)
        {
            EquipArmor(null, position, facing, PanOffset);
        }

        public void UnEquipUtility(Position position, Facing facing, Point PanOffset)
        {
            EquipUtility(null, position, facing, PanOffset);
        }

        public void EquipWeapon(Item item, Position position, Facing facing, Point PanOffset)
        {
            Item currently_equipped = this.Weapon;
            if (item == null || item.Type == Item.ItemType.Weapon)
            {
                this.Weapon = item;
                RemoveFromBackpack(item);
                if (BackpackIsFull())
                {
                    DropItem(currently_equipped, position, facing, PanOffset);

                } else
                {
                    AddToBackpack(currently_equipped);
                }
            }
            else
            {
                Console.Error.WriteLine("Not a weapon");
            }
        }
        public void EquipArmor(Item item, Position position, Facing facing, Point PanOffset)
        {
            Item currently_equipped = this.Armor;
            if (item == null || item.Type == Item.ItemType.Armor)
            {
                this.Armor = item;
                RemoveFromBackpack(item);
                if (BackpackIsFull())
                {
                    DropItem(currently_equipped, position, facing, PanOffset);
                }
                else
                {
                    AddToBackpack(currently_equipped);
                }
            } else {
                Console.Error.WriteLine("Not armor");
            }
        }
        public void EquipUtility(Item item, Position position, Facing facing, Point PanOffset)
        {
            Item currently_equipped = this.Utility;
            if (item == null || item.Type == Item.ItemType.Utility)
            {
                this.Utility = item;
                RemoveFromBackpack(item);
                if (BackpackIsFull())
                {
                    DropItem(currently_equipped, position, facing, PanOffset);
                }
                else
                {
                    AddToBackpack(currently_equipped);
                }
            } else {
                Console.Error.WriteLine("Not a utility");
            }
        }

        public void AddToBackpack(Item item)
        {
            var inserted = false;
            for (int i = 0; i < this.Backpack.Length; i++)
            {
                if (this.Backpack[i] == null)
                {
                    this.Backpack[i] = item;
                    inserted = true;
                    break;
                }
            }
            if (!inserted)
            {
                Console.Error.Write("Could not insert. Array Limited to 9 items");
            }

        }

        public void RemoveFromBackpack(Item item)
        {
            int index = -1;
            if (this.Backpack[this.Backpack.Length - 1] == item)
            {
                index = this.Backpack.Length - 1;
            } else {
                for (int i = 0; i < this.Backpack.Length - 1; i++)
                {
                    if (this.Backpack[i] == item)
                    {
                        index = i;
                        break;
                    }
                }
            }
            if (index >= 0)
            {
                RemoveFromBackpack(index);
            }
        }

        public void RemoveFromBackpack(int index)
        {
            if (index == this.Backpack.Length - 1)
            {
                this.Backpack[index] = null;
                return;
            } 
            for (int i = index; i < this.Backpack.Length - 1; i++)
            {
                this.Backpack[i] = this.Backpack[i + 1];
            }
            this.Backpack[this.Backpack.Length - 1] = null;

            this.Backpack[this.Backpack.Length - 1] = null;

        }


        public void DropItem(Item item, Position position, Facing facing, Point PanOffset)
        {
            if (item == null)
            {
                return;
            }
            Position new_position = new Position(position.x, position.y);
            new_position.x = facing == Facing.Left ? new_position.x - item.Pic.Size.Width - 10 : new_position.x + 10;
            item.StartPos = new System.Drawing.Point((int)new_position.x - PanOffset.X, (int)new_position.y - PanOffset.Y);
            item.SetEntityPosition(new_position);
        }

        public void DropAll(Position position)
        {
            Random rnd = new Random();
            Position new_position = position;
            for (int i = 0; i < this.Backpack.Length; i++)
                if (this.Backpack[i] != null)
                {
                    new_position.x = rnd.Next(-this.Backpack[i].Pic.Size.Width - 20, this.Backpack[i].Pic.Size.Width + 20);
                    new_position.y = rnd.Next(-this.Backpack[i].Pic.Size.Width - 20, this.Backpack[i].Pic.Size.Width + 20);
                    this.Backpack[i].SetEntityPosition(new_position);
                    this.Backpack[i] = null;

                }

            new_position.x = rnd.Next(- this.Weapon.Pic.Size.Width - 20, this.Weapon.Pic.Size.Width  + 20);
            new_position.y = rnd.Next(-this.Weapon.Pic.Size.Width - 20, this.Weapon.Pic.Size.Width + 20);
            this.Weapon.SetEntityPosition(new_position);
            this.Weapon = null;

            new_position.x = rnd.Next(-this.Armor.Pic.Size.Width - 20, this.Armor.Pic.Size.Width + 20);
            new_position.y = rnd.Next(-this.Armor.Pic.Size.Width - 20, this.Armor.Pic.Size.Width + 20);
            this.Armor.SetEntityPosition(new_position);
            this.Armor = null;

            new_position.x = rnd.Next(-this.Utility.Pic.Size.Width - 20, this.Utility.Pic.Size.Width + 20);
            new_position.y = rnd.Next(-this.Utility.Pic.Size.Width - 20, this.Utility.Pic.Size.Width + 20);
            this.Utility.SetEntityPosition(new_position);
            this.Utility = null;

        }

        public void UseItem()
        {
            this.Utility = null;
        }

    }

}
