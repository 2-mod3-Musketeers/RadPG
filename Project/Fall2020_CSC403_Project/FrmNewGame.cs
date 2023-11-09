using Fall2020_CSC403_Project.code;
using Fall2020_CSC403_Project.Properties;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.IO;
using System.Threading;

namespace Fall2020_CSC403_Project
{
    public partial class FrmNewGame : Form
    {
        private Form previousForm;
        private FrmLevel level;

        private Label Save1;
        private Label Save2;
        private Label Save3;
        private Label Save4;

        public PictureBox Slot1;
        public PictureBox Slot2;
        public PictureBox Slot3;
        public PictureBox Slot4;

        private Bitmap screenshot;
        public FrmNewGame(Form previousForm)
        {
            this.WindowState = FormWindowState.Maximized;
            this.previousForm = previousForm;
            this.level = Application.OpenForms["FrmLevel"] as FrmLevel;
            InitializeComponent();
            this.Setup();
        }

        private void Setup()
        {

            this.BackColor = Color.SlateGray;

            int height = Screen.PrimaryScreen.Bounds.Height;
            int width = Screen.PrimaryScreen.Bounds.Width;

            screenshot = new Bitmap(width, height);

            Save1 = new Label
            {
                Location = new Point(width / 8, 0),
                Text = "Save Slot 1",
                Parent = this,
                Size = new Size(3 * width / 8, height / 16),
                Font = new Font("NSimSun", height / 20),
                BackColor = Color.Transparent,
                ForeColor = Color.Black,
                BorderStyle = BorderStyle.None,
            };

            Save2 = new Label
            {
                Location = new Point(5 * width / 8 + width / 32, 0),
                Text = "Save Slot 2",
                Parent = this,
                Size = new Size(3 * width / 8, height / 16),
                Font = new Font("NSimSun", height / 20),
                BackColor = Color.Transparent,
                ForeColor = Color.Black,
                BorderStyle = BorderStyle.None,
            };

            Save3 = new Label
            {
                Location = new Point(width / 8, height / 2 - height / 32),
                Text = "Save Slot 3",
                Parent = this,
                Size = new Size(3 * width / 8, height / 16),
                Font = new Font("NSimSun", height / 20),
                BackColor = Color.Transparent,
                ForeColor = Color.Black,
                BorderStyle = BorderStyle.None,
            };

            Save4 = new Label
            {
                Location = new Point(5 * width / 8 + width / 32, height / 2 - height / 32),
                Text = "Save Slot 4",
                Parent = this,
                Size = new Size(3 * width / 8, height / 16),
                Font = new Font("NSimSun", height / 20),
                BackColor = Color.Transparent,
                ForeColor = Color.Black,
                BorderStyle = BorderStyle.None,
            };

            Slot1 = new PictureBox
            {
                Parent = this,
                Location = new Point(0, height / 16),
                Size = new Size(width / 2 - width / 32, height / 2 - 3 * height / 32),
                SizeMode = PictureBoxSizeMode.StretchImage,
                Image = CreateBitmap("../../data/slot1.png"),
                BorderStyle = BorderStyle.None,
            };
            Slot1.Click += Slot1_Click;

            Slot2 = new PictureBox
            {
                Parent = this,
                Location = new Point(width / 2 + width / 32, height / 16),
                Size = new Size(width / 2 - width / 32, height / 2 - 3 * height / 32),
                SizeMode = PictureBoxSizeMode.StretchImage,
                Image = CreateBitmap("../../data/slot2.png"),
            };
            Slot2.Click += Slot2_Click;

            Slot3 = new PictureBox
            {
                Parent = this,
                Location = new Point(0, height / 2 + height / 32),
                Size = new Size(width / 2 - width / 32, height / 2 - 3 * height / 32),
                SizeMode = PictureBoxSizeMode.StretchImage,
                Image = CreateBitmap("../../data/slot3.png"),
            };
            Slot3.Click += Slot3_Click;

            Slot4 = new PictureBox
            {
                Parent = this,
                Location = new Point(width / 2 + width / 32, height / 2 + height / 32),
                Size = new Size(width / 2 - width / 32, height / 2 - 3 * height / 32),
                SizeMode = PictureBoxSizeMode.StretchImage,
                Image = CreateBitmap("../../data/slot4.png"),
            };
            Slot4.Click += Slot4_Click;

            
        }
        private Bitmap CreateBitmap(string filepath)
        {
            using (FileStream stream = new FileStream(filepath, FileMode.Open, FileAccess.Read))
            // get a binary reader for the file stream
            using (BinaryReader reader = new BinaryReader(stream))
            {
                // copy the content of the file into a memory stream
                var memoryStream = new MemoryStream(reader.ReadBytes((int)stream.Length));
                // make a new Bitmap object the owner of the MemoryStream
                return new Bitmap(memoryStream);
            }
        }

        private void Slot1_Click(object sender, EventArgs e)
        {
            if (Slot1.Image != null)
            {
                FrmWarning frmwarning = new FrmWarning(this, "Slot1");
                frmwarning.Show();
            }
            else
            { 
                FrmPlayerSelect frmplayerselect = new FrmPlayerSelect(this);
                frmplayerselect.FormClosed += (s, args) => this.Close(); // Handle closure of FrmPlayerSelect to close the application
                frmplayerselect.Show();
                this.Hide(); // Hide the FrmMain form
            }
        }

        private void Slot2_Click(object sender, EventArgs e)
        {
            if (Slot2.Image != null)
            {
                FrmWarning frmwarning = new FrmWarning(this, "Slot2");
                frmwarning.Show();
            }
            else
            {
                FrmPlayerSelect frmplayerselect = new FrmPlayerSelect(this);
                frmplayerselect.FormClosed += (s, args) => this.Close(); // Handle closure of FrmPlayerSelect to close the application
                frmplayerselect.Show();
                this.Hide(); // Hide the FrmMain form
            }
        }

        private void Slot3_Click(object sender, EventArgs e)
        {
            if (Slot3.Image != null)
            {
                FrmWarning frmwarning = new FrmWarning(this, "Slot3");
                frmwarning.Show();
            }
            else
            {
                FrmPlayerSelect frmplayerselect = new FrmPlayerSelect(this);
                frmplayerselect.FormClosed += (s, args) => this.Close(); // Handle closure of FrmPlayerSelect to close the application
                frmplayerselect.Show();
                this.Hide(); // Hide the FrmMain form
            }
        }

        private void Slot4_Click(object sender, EventArgs e)
        {
            if (Slot4.Image != null)
            {
                FrmWarning frmwarning = new FrmWarning(this, "Slot4");
                frmwarning.Show();
            }
            else
            {
                FrmPlayerSelect frmplayerselect = new FrmPlayerSelect(this);
                frmplayerselect.FormClosed += (s, args) => this.Close(); // Handle closure of FrmPlayerSelect to close the application
                frmplayerselect.Show();
                this.Hide(); // Hide the FrmMain form
            }
        }
    }
}
