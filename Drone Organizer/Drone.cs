using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Drone_Organizer
{
    public class Drone
    {
        private System.Drawing.Rectangle rect;
        private System.Drawing.Rectangle prevRect;
        private System.Drawing.Color color;
        private bool isDrawn;
        private bool redraw;

        public System.Drawing.Rectangle Rect
        {
            get
            {
                return rect;
            }
            set
            {
                rect = value;
            }
        }

        public int X
        {
            get
            {
                return rect.X;
            }
            set
            {
                rect.X = value;
            }
        }

        public int Y
        {
            get
            {
                return rect.Y;
            }
            set
            {
                rect.Y = value;
            }
        }

        public int PrevX
        {
            get
            {
                return prevRect.X;
            }
            set
            {
                prevRect.X = value;
            }
        }

        public int PrevY
        {
            get
            {
                return prevRect.Y;
            }
            set
            {
                prevRect.Y = value;
            }
        }

        public System.Drawing.Color Color
        {
            get
            {
                return color;
            }
            set
            {
                color = value;
            }
        }

        public bool IsDrawn
        {
            get
            {
                return isDrawn;
            }
            set
            {
                isDrawn = value;
            }
        }

        public bool Redraw
        {
            get
            {
                return redraw;
            }
            set
            {
                redraw = value;
            }
        }

        public Drone()
        {
            rect = new System.Drawing.Rectangle();
            prevRect = new System.Drawing.Rectangle();
            color = new System.Drawing.Color();
            isDrawn = false;
            redraw = false;
        }

        public bool CursorOnDrone(int x, int y)
        {
            y -= 25;
            if (x > this.Rect.X
              && x < this.Rect.X + this.Rect.Width
              && y > this.Rect.Y
              && y < this.Rect.Y + this.Rect.Height )
            {
                return true;
            }

            return false;
        }

    }
}
