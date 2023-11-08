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

namespace Fall2020_CSC403_Project
{
    public partial class FrmSave : Form
    {
        private Form previousForm;
        private FrmLevel level;

        private Label Save1;
        private Label Save2;
        private Label Save3;
        private Label Save4;

        private PictureBox Slot1;
        private PictureBox Slot2;
        private PictureBox Slot3;
        private PictureBox Slot4;

        private Bitmap screenshot;
        public FrmSave(Form previousForm)
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
                Image = Image.FromFile("../../data/slot1.png"),
                BorderStyle = BorderStyle.None,
            };
            Slot1.Click += Slot1_Click;

            Slot2 = new PictureBox
            {
                Parent = this,
                Location = new Point(width / 2 + width / 32, height / 16),
                Size = new Size(width / 2 - width / 32, height / 2 - 3 * height / 32),
                SizeMode = PictureBoxSizeMode.StretchImage,
                Image = Image.FromFile("../../data/slot2.png"),
            };
            Slot2.Click += Slot2_Click;

            Slot3 = new PictureBox
            {
                Parent = this,
                Location = new Point(0, height / 2 + height / 32),
                Size = new Size(width / 2 - width / 32, height / 2 - 3 * height / 32),
                SizeMode = PictureBoxSizeMode.StretchImage,
                Image = Image.FromFile("../../data/slot3.png"),
            };
            Slot3.Click += Slot3_Click;

            Slot4 = new PictureBox
            {
                Parent = this,
                Location = new Point(width / 2 + width / 32, height / 2 + height / 32),
                Size = new Size(width / 2 - width / 32, height / 2 - 3 * height / 32),
                SizeMode = PictureBoxSizeMode.StretchImage,
                Image = Image.FromFile("../../data/slot4.png"),
            };
            Slot4.Click += Slot4_Click;

            
        }

        private void Slot1_Click(object sender, EventArgs e)
        {
            string filepath = "../../data/Save1Data.Json";

            level.DrawToBitmap(screenshot, new Rectangle(new Point(0, 0), screenshot.Size));

            Rectangle cropArea = new Rectangle(new Point(0, 40), level.ClientSize);

            screenshot = screenshot.Clone(cropArea, screenshot.PixelFormat);

            Slot1.Image.Dispose();

            screenshot.Save("../../data/slot1.png");

            screenshot.Dispose();

            string[] data = new string[7];

            data[0] = JsonSerializer.Serialize(Game.player);
            data[1] = JsonSerializer.Serialize(Game.Areas);
            data[2] = JsonSerializer.Serialize(Game.CurrentArea);
            data[3] = JsonSerializer.Serialize(Game.Items);
            data[4] = JsonSerializer.Serialize(Game.Enemies);
            data[5] = JsonSerializer.Serialize(Game.NPCs);
            data[6] = JsonSerializer.Serialize(Game.TravelSigns);

            File.WriteAllLines(filepath, data);

            this.Hide();
            previousForm.Show();
        }

        private void Slot2_Click(object sender, EventArgs e)
        {
            string filepath = "../../data/Save2Data.Json";

            level.DrawToBitmap(screenshot, new Rectangle(new Point(0, 0), screenshot.Size));

            Rectangle cropArea = new Rectangle(new Point(0, 40), level.ClientSize);

            screenshot = screenshot.Clone(cropArea, screenshot.PixelFormat);

            Slot2.Image.Dispose();

            screenshot.Save("../../data/slot2.png");

            screenshot.Dispose();

            string[] data = new string[7];

            data[0] = JsonSerializer.Serialize(Game.player);
            data[1] = JsonSerializer.Serialize(Game.Areas);
            data[2] = JsonSerializer.Serialize(Game.CurrentArea);
            data[3] = JsonSerializer.Serialize(Game.Items);
            data[4] = JsonSerializer.Serialize(Game.Enemies);
            data[5] = JsonSerializer.Serialize(Game.NPCs);
            data[6] = JsonSerializer.Serialize(Game.TravelSigns);

            File.WriteAllLines(filepath, data);

            this.Hide();
            previousForm.Show();
        }

        private void Slot3_Click(object sender, EventArgs e)
        {
            string filepath = "../../data/Save3Data.Json";

            level.DrawToBitmap(screenshot, new Rectangle(new Point(0, 0), screenshot.Size));

            Rectangle cropArea = new Rectangle(new Point(0, 40), level.ClientSize);

            screenshot = screenshot.Clone(cropArea, screenshot.PixelFormat);

            Slot3.Image.Dispose();

            screenshot.Save("../../data/slot3.png");

            screenshot.Dispose();

            string[] data = new string[7];

            data[0] = JsonSerializer.Serialize(Game.player);
            data[1] = JsonSerializer.Serialize(Game.Areas);
            data[2] = JsonSerializer.Serialize(Game.CurrentArea);
            data[3] = JsonSerializer.Serialize(Game.Items);
            data[4] = JsonSerializer.Serialize(Game.Enemies);
            data[5] = JsonSerializer.Serialize(Game.NPCs);
            data[6] = JsonSerializer.Serialize(Game.TravelSigns);

            File.WriteAllLines(filepath, data);

            this.Hide();
            previousForm.Show();
        }

        private void Slot4_Click(object sender, EventArgs e)
        {
            string filepath = "../../data/Save4Data.Json";

            level.DrawToBitmap(screenshot, new Rectangle(new Point(0, 0), screenshot.Size));

            Rectangle cropArea = new Rectangle(new Point(0, 40), level.ClientSize);

            screenshot = screenshot.Clone(cropArea, screenshot.PixelFormat);

            Slot4.Image.Dispose();

            screenshot.Save("../../data/slot4.png");

            screenshot.Dispose();

            string[] data = new string[7];

            data[0] = JsonSerializer.Serialize(Game.player);
            data[1] = JsonSerializer.Serialize(Game.Areas);
            data[2] = JsonSerializer.Serialize(Game.CurrentArea);
            data[3] = JsonSerializer.Serialize(Game.Items);
            data[4] = JsonSerializer.Serialize(Game.Enemies);
            data[5] = JsonSerializer.Serialize(Game.NPCs);
            data[6] = JsonSerializer.Serialize(Game.TravelSigns);

            File.WriteAllLines(filepath, data);

            this.Hide();
            previousForm.Show();
        }
    }
}
