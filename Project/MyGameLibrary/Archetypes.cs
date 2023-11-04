using System.Data;
using System.Drawing;
using System.Dynamic;
using System.Linq.Expressions;
using System.Runtime.CompilerServices;
using System.Runtime.Versioning;
using System.Security.Cryptography;
using System.Windows.Forms;
using Fall2020_CSC403_Project;
using MyGameLibrary.Properties;

namespace Fall2020_CSC403_Project.code
{
    public interface Archetype
    {
        int baseMaxHealth { get; }
        int baseDefense { get; }
        int baseDamage { get; }
        int baseSpeed { get; }
        int hitMod { get; }

        void specialMove(Character target);
    }

    public class ArchetypeHandler
    {
        private Archetype archetype;

        public ArchetypeHandler(Archetype archetype)
        {
            this.archetype = archetype;
        }
    }

    // Player Archetypes
    public class Tank : Archetype
    {
        public int baseMaxHealth { get; }
        public int baseDefense { get; }
        public int baseDamage { get; }
        public int baseSpeed { get; }
        public int hitMod { get; }

        public Tank()
        {
            baseMaxHealth = 100;
            baseDefense = 14;
            baseDamage = 2;
            baseSpeed = 0;
            hitMod = 2;
        }

        public void specialMove(Character target) {}
    }

    public class Rogue : Archetype
    {
        public int baseMaxHealth { get; }
        public int baseDefense { get; }
        public int baseDamage { get; }
        public int baseSpeed { get; }
        public int hitMod { get; }

        public Rogue()
        {
            baseMaxHealth = 20;
            baseDefense = 10;
            baseDamage = 10;
            baseSpeed = 5;
            hitMod = 5;
        }

        public void specialMove(Character target)
        {
        }
    }

    public class Swordsman : Archetype
    {
        public int baseMaxHealth { get; }
        public int baseDefense { get; }
        public int baseDamage { get; }
        public int baseSpeed { get; }
        public int hitMod { get; }

        public Swordsman()
        {
            baseMaxHealth = 50;
            baseDefense = 12;
            baseDamage = 8;
            baseSpeed = 2;
            hitMod = 3;
        }

        public void specialMove(Character target) { }
    }
    
    // NPC Archetypes
    public class Healer : Archetype
    {
        public int baseMaxHealth { get; }
        public int baseDefense { get; }
        public int baseDamage { get; }
        public int baseSpeed { get; }
        public int hitMod { get; }

        public Healer()
        {
            baseMaxHealth = 25;
            baseDefense = 10;
            baseDamage = 3;
            baseSpeed = 2;
            hitMod = 3;
        }

        public void specialMove(Character target)
        {
            target.GiveHealth(target.dice.Next(1, 8) + this.hitMod);
        }
    }

    // Enemy Archetypes
    public class Minion : Archetype
    {
        public int baseMaxHealth { get; }
        public int baseDefense { get; }
        public int baseDamage { get; }
        public int baseSpeed { get; }
        public int hitMod { get; }

        public Minion()
        {
            baseMaxHealth = 20;
            baseDefense = 10;
            baseDamage = 5;
            baseSpeed = 2;
            hitMod = 0;
        }

        public void specialMove(Character target) { }
    }

    public class Coward : Archetype
    {
        public int baseMaxHealth { get; }
        public int baseDefense { get; }
        public int baseDamage { get; }
        public int baseSpeed { get; }
        public int hitMod { get; }

        public Coward()
        {
            baseMaxHealth = 20;
            baseDefense = 10;
            baseDamage = 2;
            baseSpeed = 5;
            hitMod = 0;
        }

        public void specialMove(Character target) { }
    }

    public class Brute : Archetype
    {
        public int baseMaxHealth { get; }
        public int baseDefense { get; }
        public int baseDamage { get; }
        public int baseSpeed { get; }
        public int hitMod { get; }

        public Brute()
        {
            baseMaxHealth = 30;
            baseDefense = 12;
            baseDamage = 3;
            baseSpeed = 1;
            hitMod = 1;
        }

        public void specialMove(Character target) { }
    }

    public class Zombie : Archetype
    {
        public int baseMaxHealth { get; }
        public int baseDefense { get; }
        public int baseDamage { get; }
        public int baseSpeed { get; }
        public int hitMod { get; }

        public Zombie()
        {
            baseMaxHealth = 50;
            baseDefense = 7;
            baseDamage = 1;
            baseSpeed = 2;
            hitMod = 1;
        }

        public void specialMove(Character target) { }
    }

    public class Bees : Archetype
    {
        public int baseMaxHealth { get; }
        public int baseDefense { get; }
        public int baseDamage { get; }
        public int baseSpeed { get; }
        public int hitMod { get; }

        public Bees()
        {
            baseMaxHealth = 10;
            baseDefense = 18;
            baseDamage = 1;
            baseSpeed = 5;
            hitMod = 5;
        }

        public void specialMove(Character target) { }
    }

    public class Mage : Archetype
    {
        public int baseMaxHealth { get; }
        public int baseDefense { get; }
        public int baseDamage { get; }
        public int baseSpeed { get; }
        public int hitMod { get; }

        public Mage()
        {
            baseMaxHealth = 35;
            baseDefense = 10;
            baseDamage = 7;
            baseSpeed = 3;
            hitMod = 3;
        }

        public void specialMove(Character target) { }
    }

    public class Whelp : Archetype
    {
        public int baseMaxHealth { get; }
        public int baseDefense { get; }
        public int baseDamage { get; }
        public int baseSpeed { get; }
        public int hitMod { get; }

        public Whelp()
        {
            baseMaxHealth = 50;
            baseDefense = 7;
            baseDamage = 3;
            baseSpeed = 3;
            hitMod = 3;
        }

        public void specialMove(Character target) { }
    }

    public class Boss : Archetype
    {
        public int baseMaxHealth { get; }
        public int baseDefense { get; }
        public int baseDamage { get; }
        public int baseSpeed { get; }
        public int hitMod { get; }

        public Boss()
        {
            baseMaxHealth = 65;
            baseDefense = 15;
            baseDamage = 10;
            baseSpeed = 5;
            hitMod = 6;
        }

        public void specialMove(Character target) { }
    }

    public class Dragon : Archetype
    {
        public int baseMaxHealth { get; }
        public int baseDefense { get; }
        public int baseDamage { get; }
        public int baseSpeed { get; }
        public int hitMod { get; }

        public Dragon()
        {
            baseMaxHealth = 200;
            baseDefense = 20;
            baseDamage = 20;
            baseSpeed = 2;
            hitMod = 10;
        }

        public void specialMove(Character target) { }
    }
}