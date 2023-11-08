using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Fall2020_CSC403_Project
{
    public partial class FrmWarning : Form
    {
        private int height;
        private int width;
        private Form previousForm;
        private string warningType;
        public FrmWarning(Form previous, string warningType)
        {
            this.previousForm = previous;
            this.warningType = warningType;
            InitializeComponent();
            this.Setup();
        }

        private async void Setup()
        {
            height = Screen.PrimaryScreen.Bounds.Height;
            width = Screen.PrimaryScreen.Bounds.Width;

            Label WarningLabel = new Label()
            {
                Location = new Point(width / 2, height / 2),
                Parent = this,
                Size = new Size(9 * width / 16, height / 15),
                Font = new Font("NSimSun", height / 25),
                BackColor = Color.Transparent,
                ForeColor = Color.Black,
                Anchor = AnchorStyles.None,
            };

            // Add Buttons no
            Button NoButton = new Button();
            NoButton.Location = new Point(0 + (width / 18), 0 + (7 * height / 9));
            NoButton.Parent = this;
            NoButton.Size = new Size(width / 3, height / 10);
            NoButton.Text = ("No");
            NoButton.Font = new Font("NSimSun", NoButton.Size.Height / 2);
            NoButton.Click += NoButton_Click;

            // Create Button to clear the ranking
            Button YesButton = new Button();
            YesButton.Parent = this;
            YesButton.Size = new Size(width / 3, height / 10);
            YesButton.Location = new Point(17 * (width / 18) - YesButton.Size.Width, 0 + (7 * height / 9));
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
                    FrmPlayerSelect frmplayerselect = new FrmPlayerSelect(this);
                    frmplayerselect.FormClosed += (s, args) => this.Close(); // Handle closure of FrmPlayerSelect to close the application
                    frmplayerselect.Show();
                    previousForm.Hide(); // Hide the FrmNewGame form
                    this.Hide(); // Hide the FrmWarning form
                    break;
            }
        }
    }
}
