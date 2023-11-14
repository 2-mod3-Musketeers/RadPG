using Fall2020_CSC403_Project.code;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Drawing;
using System.Windows.Forms;

namespace MyGameLibrary
{
    public class PictureBox
    {
        public Size Size;
        public Point Location;
        public Bitmap Image;
        public PictureBoxSizeMode SizeMode = PictureBoxSizeMode.StretchImage;
        public Color BackColor;
        public PictureBox(Size Size, Point Location, Bitmap Image, Color Backcolor)
        {
            this.Size = Size;
            this.Location = Location;
            this.Image = Image;
            this.BackColor = Backcolor;
        }

        // convert the fake PictureBox to a real one
        // necessary because PictureBoxes can't be saved to json
        public System.Windows.Forms.PictureBox Convert()
        {
            return new System.Windows.Forms.PictureBox
            {
                Size = this.Size,
                Location = this.Location,
                Image = this.Image,
                SizeMode = PictureBoxSizeMode.StretchImage,
                BackColor = this.BackColor,
            };
        }
    }
}
