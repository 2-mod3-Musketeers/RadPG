using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Resources;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using MyGameLibrary;

namespace Fall2020_CSC403_Project.code
{
    public class Entity
    {
        public int SPEED { get; set; }

        
		public PictureBox Pic;
        public Point Location;
        public Size Size;
        public Color BackColor;

        public string ImageFilepath;

		public Position MoveSpeed { get; private set; }
		public Position LastPosition { get; private set; }
		public Collider Collider { get; private set; }
        public Position Position { get; private set; }

        public string Name { get; private set; }

        public Facing facing { get; set; }


        public enum Facing
        {
            Left,
            Right
        }

        public Entity(string Name, PictureBox Pic)
        {
            this.SPEED = 3;
            this.Name = Name;
            this.Pic = Pic;
            this.ImageFilepath = string.Format("../../data/Pictures/{0}.png", this.Name);

            if (this.Pic == null)
            {
                Console.WriteLine(this.Location.X);
                this.Pic = new PictureBox
                {
                    Size = this.Size,
                    Location = new Point(this.Location.X, this.Location.Y),
                    Image = Image.FromFile(this.ImageFilepath),
                    SizeMode = PictureBoxSizeMode.StretchImage,
                    BackColor = this.BackColor,
                };
            }
            else
            {
                //Bitmap clone = (Bitmap)this.Pic.Image.Clone();
                //clone.Save(ImageFilepath);
                //clone.Dispose();
            }


            this.Position = new Position(this.Pic);
            this.Collider = new Collider(this.Pic);

            this.Location = this.Pic.Location;
            this.Size = this.Pic.Size;
            
            
            this.BackColor = this.Pic.BackColor;

            Console.WriteLine("picture");
            Console.WriteLine(this.Pic.Location.Y);
        }

        public void RecreateEntity()
        {
            this.Pic.Location = this.Location;
            this.Pic.Size = this.Size;
            this.Pic.Image = Image.FromFile(this.ImageFilepath);
            this.Pic.SizeMode = PictureBoxSizeMode.StretchImage;
            this.Pic.BackColor = this.BackColor;

            this.Position = new Position(this.Pic);
            this.Collider = new Collider(this.Pic);

            this.Location = this.Pic.Location;
            this.Size = this.Pic.Size;


            this.BackColor = this.Pic.BackColor;
        }
        
        public void Move()
        {
            this.LastPosition = Position;
            this.Position = new Position(Position.x + MoveSpeed.x, Position.y + MoveSpeed.y);
            this.Pic.Location = new Point((int)this.Position.x, (int)this.Position.y);
            this.Collider.MovePosition((int)Position.x, (int)Position.y);
        }


        public void MoveBack()
        {
            this.Position = LastPosition;
        }

        public void GoLeft()
        {
            this.MoveSpeed = new Position(-SPEED, 0);
            this.facing = Facing.Left;

        }
        public void GoRight()
        {
            this.MoveSpeed = new Position(+SPEED, 0);
            this.facing = Facing.Right;
        }
        public void GoUp()
        {
            this.MoveSpeed = new Position(0, -SPEED);
        }
        public void GoDown()
        {
            this.MoveSpeed = new Position(0, +SPEED);
        }

		public void ResetMoveSpeed()
		{
			MoveSpeed = new Position(0, 0);
		}

		public void HideEntity()
		{
            Collider.Disable();
			this.Pic.Visible = false;
        }

        public void SetEntityPosition(Position pos)
        {
            this.LastPosition = Position;
            this.Position = pos;
            Collider.MovePosition((int)Position.x, (int)Position.y);
            this.Pic.Visible = true;
            this.Pic.Location = new Point((int)Position.x, (int)Position.y);
            Collider.Enable();
            this.Pic.BringToFront();
        }
    }
}
