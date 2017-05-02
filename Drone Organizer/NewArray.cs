using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Drone_Organizer
{
    public partial class NewArray : Form
    {
        public NewArray()
        {
            InitializeComponent();
        }

        private void ux_rows_field_ValueChanged(object sender, EventArgs e)
        {
            ux_total_field.Text = (ux_rows_field.Value * ux_cols_field.Value).ToString();
            Form1.NumRows = (int)ux_rows_field.Value;
        }

        private void ux_cols_field_ValueChanged(object sender, EventArgs e)
        {
            ux_total_field.Text = (ux_rows_field.Value * ux_cols_field.Value).ToString();
            Form1.NumCols = (int)ux_cols_field.Value;
        }

        private void ux_red_field_ValueChanged(object sender, EventArgs e)
        {
            Form1.DefaultRed = (int)ux_red_field.Value;
        }

        private void ux_green_field_ValueChanged(object sender, EventArgs e)
        {
            Form1.DefaultGreen = (int)ux_green_field.Value;
        }

        private void ux_blue_field_ValueChanged(object sender, EventArgs e)
        {
            Form1.DefaultBlue = (int)ux_blue_field.Value;
        }
    }
}
