using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.IO;
using Fall2020_CSC403_Project.code;

namespace Fall2020_CSC403_Project
{
    public partial class FrmWarning : Form
    {
        private int height;
        private int width;
        private Form previousForm;
        private string warningType;
        private Label WarningLabel;
        private FrmPlayerSelect frmplayerselect;
        public FrmWarning(Form previous, string warningType)
        {
            this.previousForm = previous;
            this.warningType = warningType;
            InitializeComponent();
            this.Setup();
        }

        private void Setup()
        {
            height = Screen.PrimaryScreen.Bounds.Height;
            width = Screen.PrimaryScreen.Bounds.Width;
            this.Size = new Size(width / 3 + 3 * width / 256, height / 3 - height / 128);

            WarningLabel = new Label()
            {
                Location = new Point(0, 0),
                Parent = this,
                Size = new Size(width / 3, height / 3 - height / 8),
                Font = new Font("NSimSun", height / 32),
                BackColor = Color.White,
                ForeColor = Color.Black,
                TextAlign = ContentAlignment.MiddleCenter,
            };

            switch (warningType)
            {
                case "Slot1":
                    WarningLabel.Text = "Are you sure you want to overwrite save slot 1?";
                    break;
                case "Slot2":
                    WarningLabel.Text = "Are you sure you want to overwrite save slot 2?";
                    break;
                case "Slot3":
                    WarningLabel.Text = "Are you sure you want to overwrite save slot 3?";
                    break;
                case "Slot4":
                    WarningLabel.Text = "Are you sure you want to overwrite save slot 4?";
                    break;
            }

            // Add no Button
            Button NoButton = new Button();
            NoButton.Parent = this;
            NoButton.Size = new Size(width / 6, height / 16);
            NoButton.Location = new Point(width / 6, height / 3 - 2 * NoButton.Size.Height);
            NoButton.Text = ("No");
            NoButton.Font = new Font("NSimSun", NoButton.Size.Height / 2);
            NoButton.Click += NoButton_Click;

            // Add yes Button
            Button YesButton = new Button();
            YesButton.Parent = this;
            YesButton.Size = new Size(width / 6, height / 16);
            YesButton.Location = new Point(0, height / 3 - 2 * YesButton.Size.Height);
            YesButton.Text = ("Yes");
            YesButton.Font = new Font("NSimSun", YesButton.Size.Height / 2);
            YesButton.Click += YesButton_Click;
        }

        private void NoButton_Click(object sender, EventArgs e)
        {
            this.Hide();
        }

        private void YesButton_Click(object sender, EventArgs e)
        {
            this.Hide();

            switch (warningType)
            {
                case "Slot1":
                    Game.saveSlot = 1;
                    frmplayerselect = new FrmPlayerSelect(this);
                    frmplayerselect.FormClosed += (s, args) => this.Close(); // Handle closure of FrmPlayerSelect to close the application
                    frmplayerselect.Show();
                    if (File.Exists("../../data/Save1Data.json"))
                    {
                        File.Delete("../../data/Save1Data.json");
                        File.Delete("../../data/slot1.png");
                    }
                    previousForm.Hide(); // Hide the FrmNewGame form
                    this.Hide(); // Hide the FrmWarning form
                    break;
                case "Slot2":
                    Game.saveSlot = 2;
                    frmplayerselect = new FrmPlayerSelect(this);
                    frmplayerselect.FormClosed += (s, args) => this.Close(); // Handle closure of FrmPlayerSelect to close the application
                    frmplayerselect.Show();
                    if (File.Exists("../../data/Save2Data.json"))
                    {
                        File.Delete("../../data/Save2Data.json");
                        File.Delete("../../data/slot2.png");
                    }
                    previousForm.Hide(); // Hide the FrmNewGame form
                    this.Hide(); // Hide the FrmWarning form
                    break;
                case "Slot3":
                    Game.saveSlot = 3;
                    frmplayerselect = new FrmPlayerSelect(this);
                    frmplayerselect.FormClosed += (s, args) => this.Close(); // Handle closure of FrmPlayerSelect to close the application
                    frmplayerselect.Show();
                    if (File.Exists("../../data/Save3Data.json"))
                    {
                        File.Delete("../../data/Save3Data.json");
                        File.Delete("../../data/slot3.png");
                    }
                    previousForm.Hide(); // Hide the FrmNewGame form
                    this.Hide(); // Hide the FrmWarning form
                    break;
                case "Slot4":
                    Game.saveSlot = 4;
                    frmplayerselect = new FrmPlayerSelect(this);
                    frmplayerselect.FormClosed += (s, args) => this.Close(); // Handle closure of FrmPlayerSelect to close the application
                    frmplayerselect.Show();
                    if (File.Exists("../../data/4Data.json"))
                    {
                        File.Delete("../../data/Save4Data.json");
                        File.Delete("../../data/slot4.png");
                    }
                    previousForm.Hide(); // Hide the FrmNewGame form
                    this.Hide(); // Hide the FrmWarning form
                    break;
            }
        }
    }
}
