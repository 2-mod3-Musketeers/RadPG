using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Fall2020_CSC403_Project.code
{
    public class Structure
    {
        public PictureBox Pic { get; set; }
        public Collider Collider { get; set; }

        public Point Position { get; set; }

        public Structure(PictureBox Pic)
        {
            this.Pic = Pic;
            this.Collider = new Collider(Pic);
            Position = Pic.Location;
        }

        public void Move(Point point)
        {
            this.Pic.Location = point;
            this.Collider.MovePosition((int)point.X, (int)point.Y);
        }
    }
}
