using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Fall2020_CSC403_Project.code
{
    public class Collider
    {
        public Rectangle rect;
        public bool Enabled { get; private set; }


        //public Collider(Rectangle rect)
        //{
        //    this.rect = rect;
        //    this.Enabled = true;
        //}

        [JsonConstructor]
        public Collider()
        {

        }


        public Collider(PictureBox pic, int padding = 7)
        {
            int n = pic.Location.X;
            this.rect = new Rectangle(pic.Location, new Size(pic.Size.Width - padding, pic.Size.Height - padding));
            this.Enabled = true;
        }

        //public Collider(Point point, Size size)
        //{
        //    this.rect = new Rectangle(point, size);
        //    this.Enabled = true;
        //}


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
            if (this.Enabled && c.Enabled)
            {
                return rect.IntersectsWith(c.rect);

            }
            return false;
        }


    }
}
