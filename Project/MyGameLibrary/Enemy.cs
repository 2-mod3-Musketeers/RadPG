using System.Drawing;
using System.Windows.Forms;
using System.Xml.XPath;

namespace Fall2020_CSC403_Project.code
{

	public class Enemy : Character
	{
		public Color Color { get; set; }
		public Enemy[] party { get; set; }

        public Enemy(string Name, PictureBox Pic, Archetype archetype) : base(Name, Pic, archetype)
		{
			party = new Enemy[3];
		}

        public void addToParty(Enemy newMember)
        {
            for (int i = 0; i < this.party.Length; i++)
            {
                if (this.party[i] == null)
                {
                    this.party[i] = newMember;
                }
                else
                {
                    continue;
                }
            }
        }

        public Enemy Clone()
        {
            //PictureBox ClonePic = new PictureBox();
            //ClonePic.Location = this.Location;
            //ClonePic.Size = this.Size;
            //ClonePic.Image = (Bitmap)this.Pic.Image;
            //ClonePic.SizeMode = PictureBoxSizeMode.StretchImage;
            //ClonePic.BackColor = this.BackColor;

            Enemy Clone = new Enemy(this.Name, this.Pic, this.archetype);
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
