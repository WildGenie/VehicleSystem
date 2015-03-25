using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using VehicleSystem.Custom_Controls;

namespace VehicleSystem.Custom_Forms
{
    public partial class BoundaryChooser : Form
    {
        DefineRegionsControl defineRegionsControl = new DefineRegionsControl();
        public BoundaryChooser()
        {
            InitializeComponent();
            //defineRegionsControl.Dock = DockStyle.Fill;

            VideoPlayer.Controls.Add(defineRegionsControl);
            defineRegionsControl.OnNewRectangle += new NewRectangleHandler(defineRegionsControl_NewRectangleHandler);
            rectangleButton.Click+=rectangleButton_Click;
            clearButton.Click+=clearButton_Click;
        }

        // Video frame sample to show
        public Bitmap VideoFrame
        {
            set
            { 
                defineRegionsControl.BackgroundImage = value;
            }
        }

        // Motion rectangles
        public Rectangle[] MotionRectangles
        {
            get { return defineRegionsControl.Rectangles; }
            set { defineRegionsControl.Rectangles = value; }
        }

        // On first displaying of the form
        protected override void OnLoad(EventArgs e)
        {
            // get video frame dimension
            if (defineRegionsControl.BackgroundImage != null)
            {
                int imageWidth = defineRegionsControl.BackgroundImage.Width;
                int imageHeight = defineRegionsControl.BackgroundImage.Height;

                // resize region definition control
                defineRegionsControl.Size = new Size(imageWidth + 2, imageHeight + 2);
                VideoPlayer.Size = new Size(imageWidth + 2, imageHeight + 2);
                // resize window
                //this.Size = new Size(imageWidth + 2 + 26 +100, imageHeight + 2 + 118);
                this.Size = new Size(imageWidth + +12 +2*VideoPlayer.Location.X, imageHeight + 2 + 118);
            }

            base.OnLoad(e);
        }

        // On rectangle button click
        private void rectangleButton_Click(object sender, EventArgs e)
        {
            DrawingMode currentMode = defineRegionsControl.DrawingMode;

            // change current mode
            currentMode = (currentMode == DrawingMode.Rectangular) ? DrawingMode.None : DrawingMode.Rectangular;
            // update current mode
            defineRegionsControl.DrawingMode = currentMode;
            // change button status
            rectangleButton.Checked = (currentMode == DrawingMode.Rectangular);
        }

        // New rectangle definition was finished
        private void defineRegionsControl_NewRectangleHandler(object sender, Rectangle rect)
        {
            rectangleButton.Checked = false;
        }

        // On clear button click
        private void clearButton_Click(object sender, EventArgs e)
        {
            defineRegionsControl.RemoveAllRegions();
        }
    }
}
