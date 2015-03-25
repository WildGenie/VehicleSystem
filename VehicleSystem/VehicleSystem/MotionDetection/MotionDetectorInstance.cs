using System;
using System.Drawing;
using AForge.Vision.Motion;
using VehicleSystem.LPR;

namespace VehicleSystem.MotionDetection
{
    public class MotionDetectorInstance
    {
        public enum MOTION
        {
            MOTION_AREA_HIGHLIGHTING,
            MOTION_GRID_AREA_HIGHLIGHTING,
            MOTION_BORDER_HIGHLIGHTING,
            MOTION_BLOB_COUNTER
        };

        public enum DETECTIONTYPE
        {
            TWO_FRAMES_DIFFERENCE,
            BACKGROUND_MODELLING
        };

        public enum DETECTIONSPEED
        {
            SLOW,
            MEDIUM,
            FAST
        };

        public enum DETECTIONSENSITIVITY
        {
            LOW,
            MEDIUM,
            HIGH
        };

        public MOTION detectionMethod = MOTION.MOTION_AREA_HIGHLIGHTING;
        public DETECTIONTYPE detectionType = DETECTIONTYPE.TWO_FRAMES_DIFFERENCE;
        public DETECTIONSPEED detecionSpeed = DETECTIONSPEED.MEDIUM;
        public DETECTIONSENSITIVITY detectionSensitivity = DETECTIONSENSITIVITY.MEDIUM;

        private LicencePlateRecognition LPR;
        private MotionDetector motionDetector;
        private IMotionDetector detector;
        private IMotionProcessing motionProcessor;
        public Rectangle[] detectionArea;

        //used for skipping frames, to increase performance but decrease reliability
        private int processedFrames;
        private int skipFrame = 1;
        private double sensitivityFactor;

        public MotionDetectorInstance(LicencePlateRecognition lpr)
        {
            this.LPR = lpr;

            //LPR.processingPlateFinished += LPR_processingPlateFinished;

            createMotionDetector();
        }

        public void createMotionDetector()
        {
            switch (detectionSensitivity)
            {
                case DETECTIONSENSITIVITY.LOW:
                {
                    sensitivityFactor = 0.15;
                    break;
                }
                case DETECTIONSENSITIVITY.MEDIUM:
                {
                    sensitivityFactor = 0.1;
                    break;
                }
                case DETECTIONSENSITIVITY.HIGH:
                {
                    sensitivityFactor = 0.01;
                    break;
                }
            }

            switch (detectionMethod)
            {
                case MOTION.MOTION_AREA_HIGHLIGHTING:
                {
                    motionProcessor = new MotionAreaHighlighting();
                    break;
                }
                case MOTION.MOTION_GRID_AREA_HIGHLIGHTING:
                {
                    motionProcessor = new GridMotionAreaProcessing(5, 5);
                    break;
                }
                case MOTION.MOTION_BORDER_HIGHLIGHTING:
                {
                    motionProcessor = new MotionBorderHighlighting();
                    break;
                }
                case MOTION.MOTION_BLOB_COUNTER:
                        {
                            motionProcessor = new BlobCountingObjectsProcessing();
                            break;
                        }
            }

            switch (detecionSpeed)
            {
                case DETECTIONSPEED.FAST:
                {
                    skipFrame = 3;
                    break;
                }
                case DETECTIONSPEED.MEDIUM:
                {
                    skipFrame = 2;
                    break;
                }
                case DETECTIONSPEED.SLOW:
                {
                    skipFrame = 1;
                    break;
                }
            }

            if (detectionType == DETECTIONTYPE.TWO_FRAMES_DIFFERENCE)
                detector = new TwoFramesDifferenceDetector(true);
            else
                detector = new SimpleBackgroundModelingDetector(true, true);


            motionDetector = new MotionDetector(detector, motionProcessor);

            motionDetector.MotionZones = detectionArea;
        }

        public void updateMotionDetectorDetails(Rectangle[] dA, MOTION procrs, DETECTIONTYPE dtctr,
            DETECTIONSPEED spd, DETECTIONSENSITIVITY sens)
        {
            this.detectionArea = dA;
            this.detectionMethod = procrs;
            this.detecionSpeed = spd;
            this.detectionSensitivity = sens;
            this.detectionType = dtctr;
            createMotionDetector();
        }

        //void LPR_processingPlateFinished(object sender, SimpleLPR2.Candidate results)
        //{
        //    processNextFrame = false;
        //}

        public void resetMotionDetector()
        {
            motionDetector.Reset();
        }

        /// <summary>
        ///     image must be a clone of the original image (un-proccessed by motion proccessor)
        /// </summary>
        /// <param name="image"></param>
        public void ProcessFrame(Bitmap image)
        {
            if (++processedFrames%skipFrame == 0)
            {
                processedFrames = 0;
                Bitmap img = image.Clone(new Rectangle(0, 0, image.Width, image.Height), image.PixelFormat);
                Boolean disposedImage = false;
                var motionLevel = motionDetector.ProcessFrame(image);
                //Console.WriteLine(motionLevel);
                switch (detectionMethod)
                {
                    case MOTION.MOTION_GRID_AREA_HIGHLIGHTING:
                    {
                        // ReSharper disable once PossibleNullReferenceException
                        if (motionLevel >= sensitivityFactor)
                        {
                            LPR.analyze(img);
                            disposedImage = true;
                        }
                        break;
                    }
                    case MOTION.MOTION_AREA_HIGHLIGHTING:
                    {
                        if (motionLevel >= sensitivityFactor)
                        {
                            LPR.analyze(img);
                            disposedImage = true;
                        }
                        break;
                    }
                    case MOTION.MOTION_BORDER_HIGHLIGHTING:
                    {
                        if (motionLevel >= sensitivityFactor)
                        {
                            LPR.analyze(img);
                            disposedImage = true;
                        }
                        break;
                    }
                }

                if (!disposedImage)
                    img.Dispose();
            }
        }
    }
}