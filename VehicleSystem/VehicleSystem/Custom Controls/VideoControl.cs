using System;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Drawing.Imaging;
using System.Windows.Forms;
using AForge.Controls;
using AForge.Video;
using AForge.Video.DirectShow;
using SimpleLPR2;
using VehicleSystem.LPR;
using VehicleSystem.MotionDetection;
using VehicleSystem.Properties;
using PictureBox = System.Windows.Forms.PictureBox;
using System.Reflection;
using System.Threading;
using VehicleSystem.Custom_Forms;
using System.IO;

namespace VehicleSystem.Custom_Controls
{
    internal class VideoControl : UserControl
    {
        //NOTES: videoSourcePlayer control in AForge.controls is horribly inefficient
        //a much faster way of displaying the video is simply making it the background of a panel
        //the image is cloned and bitmaps do not get released quick enough, so it slows down the system
        //but I manually call garbage collect every few seconds to elimiate this problem
        //the performance hit in calling GC so often is not yet known

        public Boolean robustChecking = true; //increases processing, but improves results
        public Boolean overrideDefaults = false;

        private LicencePlateRecognition _lpr;
        private MotionDetectorInstance _motionDetector;

        public FileVideoSource fileVideoSource;

        public int controlWidth = 500; //400
        public int controlHeight = 400; //300
        private Bitmap curFrame;

        private Form1 main;

        //feed details
        public String feedName;
        private Label lblFeedName;
        public String sourceAddress;
        public Boolean isEntrance;

        //image buttons
        private PictureBox btnInfo, btnSettings;
        private Boolean btnInfoClicked, btnSettingsClicked;
        private int pictureButtonsSize = 40;

        private Boolean plateDisplayed;
        private int framesCountedForDisposal;
        private PictureBox feedPicture;
        private Panel dummypanel;
        private Label lblConnectMessage;
        private int framesCountedForDetection;

        public VideoControl(String feedName, String sourceAddress, Boolean isEntrance, Form1 mainForm)
        {
            this.feedName = feedName;
            this.main = mainForm;
            this.sourceAddress = sourceAddress;
            this.isEntrance = isEntrance;

            initializeComponent();

            _lpr = new LicencePlateRecognition();
            _lpr.processingPlateFinished += processingPlateFinished;

            _motionDetector = new MotionDetectorInstance(_lpr);
            loadVideo(sourceAddress);
            startVideo();
        }

        private Candidate lastCandidate;

        private void processingPlateFinished(object sender, Candidate results, Bitmap b)
        {
            lastImage = b;
            if (!robustChecking)
            {
                main.HandleResults(results, b);
            }
            else
            {
                if (lastCandidate.text == null)
                    lastCandidate = results;

                //if latest detected one contains last detected -> replace with latest
                //eliminates when country code cannot be determined erros
                if (results.text.Replace(" ", "").Contains(lastCandidate.text.Replace(" ", "")))
                {
                    lastCandidate = results;
                    framesCountedForDetection = 0;
                    plateDisplayed = false;
                }
                //if last detected contains latest detection -> signal that the final plate
                //is ready for displaying
                else if (results.text.Replace(" ", "").Contains(lastCandidate.text.Replace(" ", "")))
                {
                    plateDisplayed = false;
                }
                //if last detected plate is totally different from latest detetion -> display
                //since it is more than likely a new car
                else
                {
                    framesCountedForDetection = 0;
                    lastCandidate = results;
                }
            }
        }

        
        public void initializeComponent()
        {
            
            InitializeComponent();
            this.Size = new Size(controlWidth, controlHeight);
            SuspendLayout();

            Font font = new Font("Arial", 12, FontStyle.Bold);
            this.BackColor = Color.Blue;
            //feedPicture.Size = new Size(controlWidth, controlHeight-lblFeedName.Height);

            //labels
            lblFeedName.Font = font;
            setFeedName(feedName);

            //Info Button
            btnInfo = new PictureBox
            {
                BackColor = Color.Transparent,
                Image = Resources.image_info,
                Size = new Size(pictureButtonsSize, pictureButtonsSize)
            };
            btnInfo.Location = new Point(feedPicture.Width - btnInfo.Width, 0);
            btnInfo.SizeMode = PictureBoxSizeMode.StretchImage;
            btnInfo.Click += btnInfo_Click;
            btnInfo.MouseDown += btnInfo_MouseDown;
            btnInfo.MouseUp += btnInfo_MouseUp;

            btnSettings = new PictureBox
            {
                BackColor = Color.Transparent,
                Image = Resources.settings_image,
                Size = new Size(pictureButtonsSize-4, pictureButtonsSize-4),
                Location = new Point(feedPicture.Width - btnInfo.Width - btnInfo.Width - 5, 2),
                SizeMode = PictureBoxSizeMode.StretchImage
            };
            btnSettings.MouseDown += btnSettings_MouseDown;
            btnSettings.MouseUp += btnSettings_MouseUp;
            btnSettings.Click += btnSettings_Click;

            feedPicture.Controls.Add(btnInfo);
            feedPicture.Controls.Add(btnSettings);
            //typeof (Panel).InvokeMember("DoubleBuffered",
            //    BindingFlags.SetProperty | BindingFlags.Instance | BindingFlags.NonPublic,
            //    null, panel1, new object[] {true});

            ResumeLayout(false);
        }

        public void setFeedName(String n)
        {
            lblFeedName.Text = " " + n;
            while (lblFeedName.Width < feedPicture.Width)
                lblFeedName.Text = lblFeedName.Text + " ";
        }

        void btnSettings_Click(object sender, EventArgs e)
        {
            btnSettings_MouseUp(sender, null);
            FeedSettings settings = new FeedSettings();
            settings.setDetectorSettings(_motionDetector.detectionMethod, _motionDetector.detectionType,
                _motionDetector.detecionSpeed, _motionDetector.detectionSensitivity);
            settings.setFeedSettings(feedName,sourceAddress,isEntrance, robustChecking);
            settings.background = (Bitmap) curFrame.Clone();
            settings.resultRegion = _motionDetector.detectionArea;

            if (settings.ShowDialog() == DialogResult.OK)
            {
                this.feedName = settings.feedName;
                setFeedName(feedName);
                this.isEntrance = settings.isEntrance;
                this.robustChecking = settings.robustChecking;
                _motionDetector.updateMotionDetectorDetails(settings.resultRegion, settings.processor,settings.detector,settings.speed, settings.sensitivity);
                //loadVideo(sourceAddress);
                //startVideo();
                if (!sourceAddress.Equals(settings.feedURL))
                {
                    //must restart camera
                    this.sourceAddress = settings.feedURL;
                    loadVideo(sourceAddress);
                    startVideo();
                }
            }
        }

        public void updateMotionDetectorDetails(Rectangle[] detectionArea, MotionDetectorInstance.MOTION procrs, MotionDetectorInstance.DETECTIONTYPE dtctr,
            MotionDetectorInstance.DETECTIONSPEED spd, MotionDetectorInstance.DETECTIONSENSITIVITY sens)
        {
            _motionDetector.updateMotionDetectorDetails(detectionArea, procrs, dtctr, spd, sens);
        }

        void btnSettings_MouseUp(object sender, MouseEventArgs e)
        {
            if (btnSettingsClicked)
            {
                btnSettings.Size = new Size(pictureButtonsSize - 4, pictureButtonsSize - 4);
                btnSettings.Location = new Point(btnSettings.Location.X - 5, btnSettings.Location.Y - 5);
                btnSettingsClicked = false;
            }
        }

        void btnSettings_MouseDown(object sender, MouseEventArgs e)
        {
            btnSettingsClicked = true;
            btnSettings.Size = new Size(btnSettings.Width - 10, btnSettings.Height - 10);
            btnSettings.Location = new Point(btnSettings.Location.X + 5, btnSettings.Location.Y + 5);
        }

        private void btnInfo_MouseUp(object sender, MouseEventArgs e)
        {
            if (btnInfoClicked)
            {
                btnInfo.Size = new Size(pictureButtonsSize, pictureButtonsSize);
                btnInfo.Location = new Point(btnInfo.Location.X - 5, btnInfo.Location.Y - 5);
                btnInfoClicked = false;
            }
        }

        private void btnInfo_MouseDown(object sender, MouseEventArgs e)
        {
            btnInfoClicked = true;
            btnInfo.Size = new Size(pictureButtonsSize - 10, pictureButtonsSize - 10);
            btnInfo.Location = new Point(btnInfo.Location.X + 5, btnInfo.Location.Y + 5);
        }

        private void btnInfo_Click(object sender, EventArgs e)
        {
            btnInfo_MouseUp(sender, null);
            MessageBox.Show("VideoFeedName = " + feedName, "Info for this box", MessageBoxButtons.OK,
                MessageBoxIcon.Information);
        }


        //private void videoPlayer_NewFrame(object sender, ref Bitmap image)
        //{
        //    if (robustChecking)
        //    {
        //        framesCountedForDetection++;

        //        if (framesCountedForDetection > 5)
        //        {
        //            if (!plateDisplayed && lastCandidate.text != null)
        //            {
        //                if (lastDisplayedPlate.text == null)
        //                {
        //                    main.HandleResults(lastCandidate);
        //                    framesCountedForDetection = 0;
        //                    lastDisplayedPlate = lastCandidate;
        //                    plateDisplayed = false;
        //                }
        //                else if (!lastDisplayedPlate.text.Equals(lastCandidate.text))
        //                {
        //                    main.HandleResults(lastCandidate);
        //                    framesCountedForDetection = 0;
        //                    lastDisplayedPlate = lastCandidate;
        //                    plateDisplayed = false;
        //                }
        //            }
        //        }
        //    }
 
        //    _motionDetector.ProcessFrame(image);
        //    //force garbage collection since we are using Bitmap's and we 
        //    //cant dispose them because of the ref to the video player 
        //    //and for some reason it doesn't let go off the resources properly
        //    framesCountedForDisposal++;
        //    if (framesCountedForDisposal == 10)
        //    {
        //        GC.Collect();
        //        framesCountedForDisposal = 0;
        //    }
        //}

        private void loadVideo(String filename)
        {
            lblConnectMessage.Show();
            if (fileVideoSource != null)
            { 
                if (fileVideoSource.IsRunning)
                {
                    CloseVideoSource();
                    fileVideoSource.NewFrame += null;
                    setPicture(null);
                    lblConnectMessage.Text = "Connecting to feed...";
                }  
            }

            if (File.Exists(filename))
            {
                fileVideoSource = new FileVideoSource(filename);
                fileVideoSource.NewFrame += fileVideoSource_NewFrame;
                lblConnectMessage.Hide();
            }
            else
            {
                lblConnectMessage.Text = "Error connecting to feed";
            }
        }

        public void startVideo()
        {
            fileVideoSource.Start();
        }

        public void updateVideoStream(String url)
        {
            loadVideo(url);
        }

        private Candidate lastDisplayedPlate;
        private Bitmap lastImage;
        void fileVideoSource_NewFrame(object sender, NewFrameEventArgs eventArgs)
        {
            if (robustChecking)
            {
                framesCountedForDetection++;

                if (framesCountedForDetection > 5)
                {
                    if (!plateDisplayed && lastCandidate.text != null)
                    {
                        if (lastDisplayedPlate.text == null)
                        {
                            main.HandleResults(lastCandidate, lastImage);
                            framesCountedForDetection = 0;
                            lastDisplayedPlate = lastCandidate;
                            plateDisplayed = false;
                        }
                        else if (!lastDisplayedPlate.text.Equals(lastCandidate.text))
                        {
                            main.HandleResults(lastCandidate, lastImage);
                            framesCountedForDetection = 0;
                            lastDisplayedPlate = lastCandidate;
                            plateDisplayed = false;
                        }
                    }
                }
            }
            
            _motionDetector.ProcessFrame(eventArgs.Frame);

            curFrame = (Bitmap)eventArgs.Frame.Clone();
            setPicture(curFrame);
            
            framesCountedForDisposal++;
            if (framesCountedForDisposal == 60)
            {
                GC.Collect();
                framesCountedForDisposal = 0;
            }
        }

        public void setPicture(Bitmap b)
        {
            if (feedPicture != null && feedPicture.IsHandleCreated)
                if (feedPicture.InvokeRequired)
                {
                    Invoke((MethodInvoker)delegate
                    {
                        if (feedPicture != null)
                            feedPicture.Image = b;
                    }); 
                }
                else
                {
                    feedPicture.Image = b;
                }
        }

        public void CloseVideoSource()
        {
            //reset motion detection
            fileVideoSource.SignalToStop();
            _motionDetector.resetMotionDetector();
        }

        //private void OpenVideoSource(IVideoSource source)
        //{
        //    //close previous video source
        //    CloseVideoSource();
        //    // start new video source on Async thread
        //    _videoPlayer.VideoSource = new AsyncVideoSource(source);
        //    //start video playback on Async thread
        //    _videoPlayer.Start();
        //}

        private void InitializeComponent()
        {
            this.lblFeedName = new System.Windows.Forms.Label();
            this.feedPicture = new System.Windows.Forms.PictureBox();
            this.dummypanel = new System.Windows.Forms.Panel();
            this.lblConnectMessage = new System.Windows.Forms.Label();
            ((System.ComponentModel.ISupportInitialize)(this.feedPicture)).BeginInit();
            this.dummypanel.SuspendLayout();
            this.SuspendLayout();
            // 
            // lblFeedName
            // 
            this.lblFeedName.AutoSize = true;
            this.lblFeedName.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(192)))), ((int)(((byte)(0)))));
            this.lblFeedName.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.lblFeedName.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblFeedName.Location = new System.Drawing.Point(0, 421);
            this.lblFeedName.Margin = new System.Windows.Forms.Padding(0);
            this.lblFeedName.Name = "lblFeedName";
            this.lblFeedName.Size = new System.Drawing.Size(398, 20);
            this.lblFeedName.TabIndex = 5;
            this.lblFeedName.Text = "asdaddddddddddddddddddddddddddddddddddd";
            this.lblFeedName.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // feedPicture
            // 
            this.feedPicture.Dock = System.Windows.Forms.DockStyle.Fill;
            this.feedPicture.Location = new System.Drawing.Point(0, 0);
            this.feedPicture.Margin = new System.Windows.Forms.Padding(0);
            this.feedPicture.Name = "feedPicture";
            this.feedPicture.Size = new System.Drawing.Size(604, 421);
            this.feedPicture.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage;
            this.feedPicture.TabIndex = 6;
            this.feedPicture.TabStop = false;
            // 
            // dummypanel
            // 
            this.dummypanel.Controls.Add(this.lblConnectMessage);
            this.dummypanel.Controls.Add(this.feedPicture);
            this.dummypanel.Controls.Add(this.lblFeedName);
            this.dummypanel.Dock = System.Windows.Forms.DockStyle.Fill;
            this.dummypanel.Location = new System.Drawing.Point(0, 0);
            this.dummypanel.Name = "dummypanel";
            this.dummypanel.Size = new System.Drawing.Size(604, 441);
            this.dummypanel.TabIndex = 7;
            // 
            // lblConnectMessage
            // 
            this.lblConnectMessage.AutoSize = true;
            this.lblConnectMessage.Location = new System.Drawing.Point(3, 0);
            this.lblConnectMessage.Name = "lblConnectMessage";
            this.lblConnectMessage.Size = new System.Drawing.Size(106, 13);
            this.lblConnectMessage.TabIndex = 7;
            this.lblConnectMessage.Text = "Connecting to feed...";
            // 
            // VideoControl
            // 
            this.Controls.Add(this.dummypanel);
            this.Margin = new System.Windows.Forms.Padding(0);
            this.Name = "VideoControl";
            this.Size = new System.Drawing.Size(604, 441);
            ((System.ComponentModel.ISupportInitialize)(this.feedPicture)).EndInit();
            this.dummypanel.ResumeLayout(false);
            this.dummypanel.PerformLayout();
            this.ResumeLayout(false);

        }
    }
}