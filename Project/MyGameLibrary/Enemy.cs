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
    }
}
