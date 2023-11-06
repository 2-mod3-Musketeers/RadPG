using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Fall2020_CSC403_Project.code {
  public class Player : Character
    {
        public NPC[] party;

        public Player(string name, PictureBox pic, Archetype archetype) : base(name, pic, archetype)
        {
            party = new NPC[3];

        }

        public bool isPartyFull()
        {
            for (int i = 0; i < this.party.Length; i++)
            {
                if (this.party[i] == null)
                { return false; }
            }
            return true;
        }

        public void addPartyMember(NPC newMember)
        {
            for (int i = 0; i < party.Length; i++)
            {
                if (party[i] == null)
                {
                    party[i] = newMember;
                }
                else
                {
                    continue;
                }
            }
        }
    }
}
