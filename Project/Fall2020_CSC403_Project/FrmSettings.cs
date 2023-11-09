﻿using Fall2020_CSC403_Project.code;
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
        private FrmLevel level;


        public FrmSettings(Form previous)
        {
            this.previousForm = previous;
            this.level = Application.OpenForms["FrmLevel"] as FrmLevel;
            this.WindowState = FormWindowState.Maximized;
            InitializeComponent();
            this.setup();
            this.KeyPreview = true;
        }
        private void setup()
        {
            this.KeyDown += FrmSettings_KeyDown;
            this.BackColor = Color.DimGray;

            int height = Screen.PrimaryScreen.Bounds.Height;
            int width = Screen.PrimaryScreen.Bounds.Width;

            // Add Buttons For Return and Exit
            Button ReturnButton = new Button();
            Button ExitButton = new Button();

            ReturnButton.Location = new Point((width / 3), (height / 8));
            ExitButton.Location = new Point((width / 3), (2 * height / 8));

            ReturnButton.Size = new Size(width / 3, height / 10);
            ExitButton.Size = new Size(width / 3, height / 10);

            ReturnButton.Text = ("Return");
            ExitButton.Text = ("Quit Game");

            ReturnButton.Font = new Font("NSimSun", ReturnButton.Size.Height / 2);
            ExitButton.Font = new Font("NSimSun", ExitButton.Size.Height / 2);

            ReturnButton.Parent = this;
            ExitButton.Parent = this;

            ReturnButton.Click += ReturnButton_Click;
            ExitButton.Click += ExitButton_Click;

            if (this.previousForm.Name != "FrmMain")
            {

                Button SaveButton = new Button();
                SaveButton.Location = new Point((width / 3), (2 * height / 8));
                SaveButton.Size = new Size(width / 3, height / 10);
                SaveButton.Text = ("Save");
                SaveButton.Font = new Font("NSimSun", ReturnButton.Size.Height / 2);
                SaveButton.Parent = this;
                SaveButton.Click += SaveButton_Click;
                SaveButton.TabIndex = 2;

                ExitButton.Location = new Point((width / 3), (3 * height / 8));
                ExitButton.TabIndex = 3;
            }
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
        private void SaveButton_Click(object sender, EventArgs e)
        {
            level.Save();
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
