using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Fall2020_CSC403_Project.code;

namespace MyGameLibrary
{
    public class TravelSign
    {
        public string Location;
        public PictureBox Pic;
        public Collider Collider;

        public TravelSign(string Location, PictureBox Pic)
        {
            this.Location = Location;
            this.Pic = Pic;
            this.Collider = new Collider(Pic);
        }
    }
}
