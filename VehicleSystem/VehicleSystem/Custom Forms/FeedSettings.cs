using System;
using System.Drawing;
using System.Linq;
using System.Windows.Forms;
using AForge.Vision.Motion;
using VehicleSystem.MotionDetection;

namespace VehicleSystem.Custom_Forms
{
    public partial class FeedSettings : Form
    {
        public Rectangle[] resultRegion;
        public String feedName, feedURL;
        public Boolean isEntrance;
        public Bitmap background;
        public Boolean robustChecking;

        public MotionDetectorInstance.MOTION processor;
        public MotionDetectorInstance.DETECTIONTYPE detector;
        public MotionDetectorInstance.DETECTIONSPEED speed;
        public MotionDetectorInstance.DETECTIONSENSITIVITY sensitivity;

        public FeedSettings()
        {
            InitializeComponent();
        }

        public void setDetectorSettings(MotionDetectorInstance.MOTION procrs, MotionDetectorInstance.DETECTIONTYPE dtctr,
            MotionDetectorInstance.DETECTIONSPEED spd, MotionDetectorInstance.DETECTIONSENSITIVITY sens)
        {
            switch (sens)
            {
                case MotionDetectorInstance.DETECTIONSENSITIVITY.LOW:
                {
                    cbxSensitivity.SelectedIndex = 0;
                    break;
                }
                case MotionDetectorInstance.DETECTIONSENSITIVITY.MEDIUM:
                {
                    cbxSensitivity.SelectedIndex = 1;
                    break;
                }
                case MotionDetectorInstance.DETECTIONSENSITIVITY.HIGH:
                {
                    cbxSensitivity.SelectedIndex = 2;
                    break;
                }
            }

            switch (procrs)
            {
                case MotionDetectorInstance.MOTION.MOTION_AREA_HIGHLIGHTING:
                {
                    cbxProcessor.SelectedIndex = 0;
                    break;
                }
                case MotionDetectorInstance.MOTION.MOTION_GRID_AREA_HIGHLIGHTING:
                {
                    cbxProcessor.SelectedIndex = 1;
                    break;
                }
                case MotionDetectorInstance.MOTION.MOTION_BORDER_HIGHLIGHTING:
                {
                    cbxProcessor.SelectedIndex = 2;
                    break;
                }
            }

            switch (spd)
            {
                case MotionDetectorInstance.DETECTIONSPEED.FAST:
                {
                    cbxSpeed.SelectedIndex = 2;
                    break;
                }
                case MotionDetectorInstance.DETECTIONSPEED.MEDIUM:
                {
                    cbxSpeed.SelectedIndex = 1;
                    break;
                }
                case MotionDetectorInstance.DETECTIONSPEED.SLOW:
                {
                    cbxSpeed.SelectedIndex = 0;
                    break;
                }
            }

            if (dtctr == MotionDetectorInstance.DETECTIONTYPE.TWO_FRAMES_DIFFERENCE)
                cbxDetector.SelectedIndex = 0;
            else
                cbxDetector.SelectedIndex = 1;
        }

        public void setFeedSettings(String feedname, String URL, Boolean isentrance, Boolean robustchecking)
        {
            txtFeedName.Text = feedname;
            txtFeelURL.Text = URL;
            cbxDirection.SelectedIndex = isentrance ? 0 : 1;
            if (robustchecking)
                cbxRobustChecking.SelectedIndex = 0;
            else
                cbxRobustChecking.SelectedIndex = 1;
            this.robustChecking = robustchecking;
        }

        private void btnCancel_Click(object sender, EventArgs e)
        {
            DialogResult = DialogResult.Cancel;
        }

        private void btnOK_Click(object sender, EventArgs e)
        {
            feedName = txtFeedName.Text;
            feedURL = txtFeelURL.Text;
            isEntrance = (cbxDirection.SelectedIndex == 0) ? true : false;

            detector = (cbxDetector.SelectedIndex == 0)
                ? MotionDetectorInstance.DETECTIONTYPE.TWO_FRAMES_DIFFERENCE
                : MotionDetectorInstance.DETECTIONTYPE.BACKGROUND_MODELLING;

            switch (cbxProcessor.SelectedIndex)
            {
                case 0:
                {
                    processor = MotionDetectorInstance.MOTION.MOTION_AREA_HIGHLIGHTING;
                    break;
                }
                case 1:
                {
                    processor = MotionDetectorInstance.MOTION.MOTION_GRID_AREA_HIGHLIGHTING;
                    break;
                }
                case 2:
                {
                    processor = MotionDetectorInstance.MOTION.MOTION_BORDER_HIGHLIGHTING;
                    break;
                }
            }

            switch (cbxSpeed.SelectedIndex)
            {
                case 0:
                {
                    speed = MotionDetectorInstance.DETECTIONSPEED.SLOW;
                    break;
                }
                case 1:
                {
                    speed = MotionDetectorInstance.DETECTIONSPEED.MEDIUM;
                    break;
                }
                case 2:
                {
                    speed = MotionDetectorInstance.DETECTIONSPEED.FAST;
                    break;
                }
            }

            switch (cbxSensitivity.SelectedIndex)
            {
                case 0:
                {
                    sensitivity = MotionDetectorInstance.DETECTIONSENSITIVITY.LOW;
                    break;
                }
                case 1:
                {
                    sensitivity = MotionDetectorInstance.DETECTIONSENSITIVITY.MEDIUM;
                    break;
                }
                case 2:
                {
                    sensitivity = MotionDetectorInstance.DETECTIONSENSITIVITY.HIGH;
                    break;
                }
            }

            if (cbxRobustChecking.SelectedIndex == 0)
                robustChecking = true;
            else
                robustChecking = false;

            DialogResult = DialogResult.OK;
        }

        private void btnDefineRegion_Click(object sender, EventArgs e)
        {
            BoundaryChooser choose = new BoundaryChooser();
            choose.VideoFrame = background;
            choose.MotionRectangles = resultRegion;
            if (choose.ShowDialog() == DialogResult.OK)
            {
                resultRegion = choose.MotionRectangles;
                if (resultRegion.Count() == 0)
                    resultRegion = null;
            }
        }
    }
}