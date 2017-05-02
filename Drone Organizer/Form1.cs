using System;
using System.Drawing;
using System.Windows.Forms;



namespace Drone_Organizer
{
    public partial class Form1 : Form
    {
        /********************************************************************
        Variables
        ********************************************************************/
        public static int NumRows;
        public static int NumCols;
        public static int DefaultRed;
        public static int DefaultGreen;
        public static int DefaultBlue;
        public static int FrameNumber;

        private Graphics graphics;
        private int PixelsPerMeter;
        private double MetersBetweenDrones;
        private double DroneSizeInMeters;

        private bool Dragging;

        private Drone[,] drones;
        private  Drone SelectedDrone;

        private System.Timers.Timer RefreshTimer;

        private Point previousCursorPos;

        /********************************************************************
        Initialization
        ********************************************************************/
        public Form1()
        {
            InitializeComponent();
            graphics = this.CreateGraphics();
            NumRows = 0;
            NumCols = 0;
            DefaultRed = 255;
            DefaultGreen = 255;
            DefaultBlue = 255;
            FrameNumber = 0;

            PixelsPerMeter = 100;
            MetersBetweenDrones = 1;
            DroneSizeInMeters = .5;
            Dragging = false;
            previousCursorPos = new Point();
            RefreshTimer = new System.Timers.Timer(16.0);
            RefreshTimer.Enabled = false;
            RefreshTimer.AutoReset = true;
            RefreshTimer.Elapsed += RefreshTimerElapsed;

            XMLGenerator.init();
        }

        /********************************************************************
        Functions that draw drones
        ********************************************************************/
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

        /********************************************************************
        Timer Event Handlers
        ********************************************************************/
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

        /********************************************************************
        Mouse Event Handlers
        ********************************************************************/
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

        /********************************************************************
        Menu Item Event Handlers
        ********************************************************************/
        private void NewArrayMenuItem_Click(object sender, EventArgs e)
        {
            NewArray newArray = new NewArray();
            if (newArray.ShowDialog() == DialogResult.OK)
                GenerateDrones();
        }

        private void NewFrameMenuItem_Click(object sender, EventArgs e)
        {
            NewFrame newFrame = new NewFrame();
            if (newFrame.ShowDialog() == DialogResult.OK)
            {
                XMLGenerator.StoreFrame(FrameNumber, drones, NumRows, NumCols);
            }
        }

        private void GenerateXMLMenuItem_Click(object sender, EventArgs e)
        {
            XMLGenerator.StoreFrame(FrameNumber, drones, NumRows, NumCols);
            XMLGenerator.CreateXml();
        }

        private void SaveMenuItem_Click(object sender, EventArgs e)
        {
            XMLGenerator.StoreFrame(FrameNumber, drones, NumRows, NumCols);
        }
    }
}
