using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Drawing;
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

		public Position MoveSpeed { get; private set; }
		public Position LastPosition { get; private set; }
		public Collider Collider { get; private set; }


        public Rectangle Size { get; private set; }
        public Position Position { get; private set; }

        public string Name { get; private set; }

        public Facing facing { get; set; }

        public Point StartPos { get; private set; }


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
            this.Position = new Position(this.Pic);
            this.Collider = new Collider(this.Pic);
            this.StartPos = Pic.Location;
        }
        
        public void Move(Point point)
        {
            this.LastPosition = Position;
            this.Position = new Position(Position.x + MoveSpeed.x, Position.y + MoveSpeed.y);
            this.Pic.Location = point;
            this.Collider.MovePosition((int)point.X, (int)point.Y);
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

		public void RemoveEntity()
		{
            this.LastPosition = Position;;
            this.Position = new Position(-100, -100);
            Collider.MovePosition((int)Position.x, (int)Position.y);
			this.Pic.Visible = false;
            this.Pic.Location = new Point((int)Position.x, (int)Position.y);
        }

		public void RestoreEntity()
		{
			this.Position = LastPosition;
            this.Pic.Location = new Point((int)Position.x, (int)Position.y);
            this.LastPosition = Position;
            Collider.MovePosition((int)Position.x, (int)Position.y);
			this.Pic.Visible = true;
        }

        public void SetEntityPosition(Position pos)
        {
            this.LastPosition = Position;
            this.Position = pos;
            Collider.MovePosition((int)Position.x, (int)Position.y);
            this.Pic.Visible = true;
            this.Pic.Location = new Point((int)Position.x, (int)Position.y);
        }


    }
}
