using System;
using System.Collections.Generic;
using System.Diagnostics.Eventing.Reader;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Fall2020_CSC403_Project.code {
  public class Player : Character
    {
        public NPC[] Party;

        public Player(string name, PictureBox pic, Archetype archetype) : base(name, pic, archetype)
        {
            Party = new NPC[3];
        }

        public bool isPartyFull()
        {
            for (int i = 0; i < this.Party.Length; i++)
            {
                if (this.Party[i] == null)
                { return false; }
            }
            return true;
        }

        public int PartyCount()
        {
            int count = 0;
            for (int i = 0; i < this.Party.Length; i++)
            {
                if (this.Party[i] != null)
                { count++; }
            }
            return count;
        }

        public void addPartyMember(NPC newMember)
        {
            for (int i = 0; i < Party.Length; i++)
            {
                if (Party[i] == null)
                {
                    Party[i] = newMember;
                    return;
                }
                else
                {
                    continue;
                }
            }
        }

        public void removePartyMember(NPC member)
        {
            for (int i = 0; i < Party.Length; i++)
            {
                if (Party[i] == member)
                {
                    removePartyMember(i);
                    return;
                }
            }
        }

        public void removePartyMember(int index)
        {
            for (int i = 0; i < Party.Length - 1; i ++)
            {
                if (i >= index)
                {
                    Party[i] = Party[i + 1];
                }
            }
            Party[Party.Length - 1] = null;
        }
    }
}
