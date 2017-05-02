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
    public partial class NewFrame : Form
    {
        public NewFrame()
        {
            InitializeComponent();
        }

        private void ux_ok_Click(object sender, EventArgs e)
        {
            Form1.FrameNumber = (int)ux_frame_number.Value;
        }
    }
}
