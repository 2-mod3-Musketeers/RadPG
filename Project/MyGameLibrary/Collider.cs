using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Fall2020_CSC403_Project.code
{
    public class Collider
    {
        private Rectangle rect;
        public bool Enabled { get; private set; }


        public Collider(Rectangle rect)
        {
            this.rect = rect;
            this.Enabled = true;
        }


        public Collider(PictureBox pic, int padding = 7)
        {
            this.rect = new Rectangle(pic.Location, new Size(pic.Size.Width - padding, pic.Size.Height - padding));
            this.Enabled = true;
        }

        public Collider(Point point, Size size)
        {
            this.rect = new Rectangle(point, size);
            this.Enabled = true;
        }

        public void Enable()
        {
            this.Enabled = true;
        }

        public void Disable()
        {
            this.Enabled = false;
        }


        public void MovePosition(int x, int y)
        {
            rect.X = x;
            rect.Y = y;
        }

        public bool Intersects(Collider c)
        {
            Console.WriteLine(this.Enabled + ", " + c.Enabled);
            if (this.Enabled && c.Enabled)
            {
                return rect.IntersectsWith(c.rect);

            }
            return false;
        }

    }
}
