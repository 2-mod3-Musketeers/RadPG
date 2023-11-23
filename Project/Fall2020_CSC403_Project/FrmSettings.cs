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
    public partial class FrmSettings : Form
    {
        private Form previousForm;


        public FrmSettings(Form previous)
        {
            this.FormBorderStyle = FormBorderStyle.None;
            this.WindowState = FormWindowState.Maximized;
            this.Bounds = Screen.PrimaryScreen.Bounds;

            InitializeComponent();
            this.setup();
            this.KeyPreview = true;
            previousForm = previous;
        }
        private void setup()
        {
            this.KeyDown += FrmSettings_KeyDown;
            this.BackColor = Color.DimGray;

            int height = Screen.PrimaryScreen.Bounds.Height;
            int width = Screen.PrimaryScreen.Bounds.Width;

            // Add Buttons For Return, Exit and FAQ
            Button ReturnButton = new Button();
            Button ExitButton = new Button();
            Button FAQButton = new Button();

            ReturnButton.Location = new Point((width / 3), (height / 8));
            ExitButton.Location = new Point((width / 3), (3 * height / 8));
            FAQButton.Location = new Point((width / 3), (2 * height / 8));

            ReturnButton.Size = new Size(width / 3, height / 10);
            ExitButton.Size = new Size(width / 3, height / 10);
            FAQButton.Size = new Size(width / 3, height / 10);

            ReturnButton.Text = ("Return");
            ExitButton.Text = ("Quit Game");
            FAQButton.Text = ("FAQs");

            ReturnButton.Font = new Font("NSimSun", ReturnButton.Size.Height / 2);
            ExitButton.Font = new Font("NSimSun", ExitButton.Size.Height / 2);
            FAQButton.Font = new Font("NSimSun", ExitButton.Size.Height / 2);

            ReturnButton.Parent = this;
            ExitButton.Parent = this;
            FAQButton.Parent = this;

            ReturnButton.Click += ReturnButton_Click;
            ExitButton.Click += ExitButton_Click;
            FAQButton.Click += FAQButton_Click;


        }

        private void FAQButton_Click(object sender, EventArgs e)
        {
            FrmFAQ frmfaq = new FrmFAQ(this);
            frmfaq.FormClosed += (s, args) => this.Close();
            frmfaq.Show();
            this.Hide();
        }

        private void FrmSettings_KeyDown(object sender, KeyEventArgs e)
        {
            switch (e.KeyCode)
            {

                case Keys.Escape:
                    previousForm.Show();
                    this.Hide();
                    break;

                default:
                    break;
            }
        }


        private void ExitButton_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }

        private void ReturnButton_Click(object sender, EventArgs e)
        {
            previousForm.Show();
            this.Hide();
        }
    }
}
