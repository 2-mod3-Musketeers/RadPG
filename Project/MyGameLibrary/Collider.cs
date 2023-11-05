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


        public Collider(Rectangle rect)
        {
            this.rect = rect;
        }

        public Collider(PictureBox pic, int padding = 7)
        {
            this.rect = new Rectangle(pic.Location, new Size(pic.Size.Width - padding, pic.Size.Height - padding));
        }

        public Collider(Point point, Size size)
        {
            this.rect = new Rectangle(point, size);
        }


        public void MovePosition(int x, int y)
        {
            rect.X = x;
            rect.Y = y;
        }

        public bool Intersects(Collider c)
        {
            return rect.IntersectsWith(c.rect);
        }

        public bool ContainsEntity(Entity e)
        {
            return rect.Contains((int)e.Position.x, (int)e.Position.y);
        }


    }
}
