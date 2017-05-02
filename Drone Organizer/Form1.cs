using System;
using System.Timers;
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
    public partial class Form1 : Form
    {
        public static int NumRows;
        public static int NumCols;
        public static int DefaultRed;
        public static int DefaultGreen;
        public static int DefaultBlue;

        private Graphics graphics;
        private int PixelsPerMeter;
        private double MetersBetweenDrones;
        private double DroneSizeInMeters;

        private bool Dragging;

        private Drone[,] drones;
        private  Drone SelectedDrone;

        private System.Timers.Timer RefreshTimer;

        private Point previousCursorPos;

        public Form1()
        {
            InitializeComponent();
            graphics = this.CreateGraphics();
            NumRows = 0;
            NumCols = 0;
            DefaultRed = 255;
            DefaultGreen = 255;
            DefaultBlue = 255;
            PixelsPerMeter = 100;
            MetersBetweenDrones = 1;
            DroneSizeInMeters = .5;
            Dragging = false;
            previousCursorPos = new Point();
            RefreshTimer = new System.Timers.Timer(16.0);
            RefreshTimer.Enabled = false;
            RefreshTimer.AutoReset = true;
            RefreshTimer.Elapsed += RefreshTimerElapsed;
        }

        private void newToolStripMenuItem_Click(object sender, EventArgs e)
        {
            NewArray newArray = new NewArray();
            if (newArray.ShowDialog() == DialogResult.OK)
                GenerateDrones();
        }

        public void GenerateDrones()
        {
            graphics.Clear(SystemColors.Control);
            graphics.SmoothingMode = System.Drawing.Drawing2D.SmoothingMode.HighQuality;
            drones = new Drone[NumRows, NumCols];

            for (int r = 0; r < NumRows; r++)
            {
                for (int c = 0; c < NumCols; c++)
                {
                    drones[r, c] = new Drone();
                    drones[r, c].Rect = new Rectangle( (int)(DroneSizeInMeters * PixelsPerMeter) + (int)(MetersBetweenDrones * PixelsPerMeter) * r
                                                     , (int)(DroneSizeInMeters * PixelsPerMeter) + (int)(MetersBetweenDrones * PixelsPerMeter) * c
                                                     , (int)(DroneSizeInMeters * PixelsPerMeter)
                                                     , (int)(DroneSizeInMeters * PixelsPerMeter) );

                    drones[r, c].Color = Color.FromArgb( DefaultRed, DefaultGreen, DefaultBlue );
                    DrawDrone( drones[r, c] );
                }
            }
        }

        public void RegenerateDrones()
        {
            for (int r = 0; r < NumRows; r++)
            {
                for (int c = 0; c < NumCols; c++)
                {
                    if (drones[r, c].Redraw)
                    {
                        EraseDrone(drones[r, c]);
                        DrawDrone(drones[r, c]);
                    }
                }
            }
        }

        public void RedrawAllDrones()
        {
            graphics.Clear(SystemColors.Control);
            for (int r = 0; r < NumRows; r++)
            {
                for (int c = 0; c < NumCols; c++)
                {
                    drones[r, c].IsDrawn = false;
                    DrawDrone(drones[r, c]);
                }
            }
        }

        public void DrawDrone(Drone drone)
        {
            //Verify the drone is not currently drawn
            if (drone.IsDrawn)
                return;

            //Create Pen and Brush
            Pen pen = new Pen(Color.Black);
            Brush brush = new SolidBrush(drone.Color);

            pen.Width = 2;

            drone.PrevX = drone.Rect.X;
            drone.PrevY = drone.Rect.Y;

            //Draw
            graphics.DrawEllipse(pen, drone.Rect);
            graphics.FillEllipse(brush, drone.Rect);

            drone.IsDrawn = true;
        }

        public void EraseDrone(Drone drone)
        {
            //Verify the drone is not currently errased
            if (!drone.IsDrawn)
                return;

            Pen pen = new Pen(SystemColors.Control);
            Brush brush = new SolidBrush(SystemColors.Control);

            pen.Width = 6;

            Rectangle eraseRect = new Rectangle(drone.PrevX, drone.PrevY, drone.Rect.Width, drone.Rect.Height);
            graphics.DrawEllipse(pen, eraseRect);
            graphics.FillEllipse(brush, eraseRect);

            drone.IsDrawn = false;
        }

        private void RefreshTimerElapsed(Object source, System.Timers.ElapsedEventArgs e)
        {
            if (Dragging)
            {
                RegenerateDrones();
            }
            else
            {
                graphics.SmoothingMode = System.Drawing.Drawing2D.SmoothingMode.HighQuality;
                RedrawAllDrones();
                RefreshTimer.Enabled = false;
            }
        }

        private void Form1_MouseMove(object sender, MouseEventArgs e)
        {
            if (Dragging)
            {
                SelectedDrone.X += Cursor.Position.X - previousCursorPos.X;
                SelectedDrone.Y += Cursor.Position.Y - previousCursorPos.Y;
                SelectedDrone.Redraw = true;
            }

            previousCursorPos = Cursor.Position;
        }

        private void Form1_MouseDown(object sender, MouseEventArgs e)
        {
            if (!Dragging)
            {
                Point cursorPos = Cursor.Position;
                for (int r = 0; r < NumRows; r++)
                {
                    for (int c = 0; c < NumCols; c++)
                    {
                        if (drones[r, c].CursorOnDrone(cursorPos.X, cursorPos.Y))
                        {
                            SelectedDrone = drones[r, c];
                            Dragging = true;
                            graphics.SmoothingMode = System.Drawing.Drawing2D.SmoothingMode.HighSpeed;
                            RefreshTimer.Enabled = true;
                        }
                    }
                }

                previousCursorPos = cursorPos;
            }
        }

        private void Form1_MouseUp(object sender, MouseEventArgs e)
        {
            if (Dragging)
            {
                Dragging = false;
            }
        }
    }
}
