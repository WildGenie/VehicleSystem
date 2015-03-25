using System;
using System.Collections.Generic;
using System.Data.OleDb;
using System.Drawing;
using System.Runtime.InteropServices;
using System.Windows.Forms;
using java.lang;
using java.net;
using SimpleLPR2;
using VehicleSystem.Custom_Controls;
using VehicleSystem.Custom_Forms;
using VehicleSystem.LPR;
using VehicleSystem.MotionDetection;
using VehicleSystem.ServerListener;
using Boolean = System.Boolean;
using Exception = System.Exception;
using Object = System.Object;
using String = System.String;
using VehicleSystem.Properties;

namespace VehicleSystem
{
    public partial class Form1 : Form
    {
        #region global vars

        private List<VideoControl> _videoControls = new List<VideoControl>();

        private OleDbConnection _conn;

        public VehicleSystemServer _server;

        private int _vehicleCount;

        public Size GetFeedSize
        {
            get { return pnlVideoFeed.Size; }
        }

        #endregion

        //to display console
        [DllImport("kernel32.dll", SetLastError = true)]
        [return: MarshalAs(UnmanagedType.Bool)]
        private static extern bool AllocConsole();


        public Form1()
        {
#if DEBUG
            //show console
            AllocConsole();
            Console.WriteLine("Working in DEBUG mode");
#endif
            this.StartPosition = FormStartPosition.CenterScreen;
            InitializeComponent();
            this.FormClosing += Form1_FormClosing;

            pnlVideoFeed.AutoScroll = true;

            lvResults.View = View.Details;
            lvResults.Scrollable = true;

            //StartJavaServer();
            ConnectToDb();
            SetupStatsList();
        }

        //SETUP UI

        #region setup UI

        public void SetupStatsList()
        {
            statsDataGrid.Rows.Add("Vehicle Count", 0);
            statsDataGrid.Rows.Add("Registered Vehicle Count", 0);
            statsDataGrid.Rows.Add("Unregistered Vehicle Count", 0);
        }

        #endregion

        //ADDING OF VIDEO FEEDS

        #region add video feed

        private void btnAddFeed_Click(object sender, EventArgs e)
        {
            FeedSettings settings = new FeedSettings();
            //have some default settings
            settings.setDetectorSettings(MotionDetectorInstance.MOTION.MOTION_BORDER_HIGHLIGHTING, MotionDetectorInstance.DETECTIONTYPE.TWO_FRAMES_DIFFERENCE, 
                MotionDetectorInstance.DETECTIONSPEED.SLOW, MotionDetectorInstance.DETECTIONSENSITIVITY.HIGH);
            settings.setFeedSettings("", @"C:\Users\s212227122\Desktop\VehicleSystem\My Movie.wmv", true, true);
            settings.background = Resources.preview_frame;

            if (settings.ShowDialog() == DialogResult.OK)
            {
                //VideoControl newControl = new VideoControl(settings.feedName, @"C:\Users\Matthew\Desktop\Front Edited.wmv", true, this) { Margin = new Padding(0) };
                //C:\Users\Matthew\Desktop\Front Edited.wmv
                VideoControl newControl = new VideoControl(settings.feedName, settings.feedURL, settings.isEntrance,
                    this) {Margin = new Padding(0), robustChecking = settings.robustChecking};
                newControl.updateMotionDetectorDetails(settings.resultRegion, settings.processor, settings.detector, settings.speed, settings.sensitivity);
                _videoControls.Add(newControl);
                pnlVideoFeed.Controls.Add(newControl);
            }

            Console.WriteLine("Stream added");
        }

        #endregion

        //SETTING UP ANDROID SERVER

        #region setting up android server

        public void StartJavaServer()
        {
            Console.WriteLine("Starting server");

            URL url = new URL("file:VehicleSystemServer.jar");
            URLClassLoader loader = new URLClassLoader(new[] {url});
            try
            {
                Class cl = Class.forName("VehicleSystemServer", true, loader);
                object obj = cl.newInstance();
                _server = (VehicleSystemServer) obj;

                String userName = Globals.GCM_SENDER_ID + "@gcm.googleapis.com";
                String password = Globals.GCM_SERVER_KEY;

                _server.connect(userName, password);

                MessageListener listener = new MessageListener(this);

                _server.addListener(listener);

                _server.testListener();

                Console.WriteLine("Server started successfully");
            }
            catch (Exception e)
            {
                Console.WriteLine("Error in starting server");
                Console.WriteLine(e.Message);
            }
        }

        #endregion

        //HANDLING OF NUMBER PLATE RESULTS METHODS

        #region handling on number plate results

        private int vehicleCount = 0;
        public void HandleResults(Candidate results, Bitmap b)
        {
            System.Threading.Thread t = new System.Threading.Thread(() =>
            {
                DateTime date = DateTime.Now;
                //b.Save(@"C:\FRAMES\" + ++vehicleCount + ".jpg");
                String absolutePath = String.Format("{0}-{1}-{2}-{3}.jpg", date.Day, date.Month, date.Year, results.text);
                String filepath = String.Format(@"C:\FRAMES\{0}", absolutePath);
                b.Save(filepath);
                String URL = absolutePath;
                CheckDatabase(results.text, URL);
                b.Dispose();
            }); 
            t.Start();
        }

        #endregion

        //DISPLAYING ON UI THREAD

        #region displayng on UI thread

        public void DisplayPlate(LicencePlate plate)
        {
            MethodInvoker invoker = delegate
            {
                DisplayVehicleCount(plate);

                //lvResults.Items.Add(new ListViewItem(plate.toArray()));
                lvResults.Items.Insert(0, new ListViewItem(plate.toArray()));
                lvResults.Items[0].BackColor = plate.plateNumber.Contains("error") ? Color.Red : Color.LawnGreen;
            };

            BeginInvoke(invoker);
        }

        public void DisplayVehicleCount(LicencePlate plate)
        {
            MethodInvoker invoker = delegate
            {
                statsDataGrid.Rows[0].Cells[1].Value = _vehicleCount;
                if (plate.isRegistered)
                {
                    int count = Convert.ToInt32(statsDataGrid.Rows[1].Cells[1].Value.ToString());
                    statsDataGrid.Rows[1].Cells[1].Value = ++count;
                }
                else
                {
                    int count = Convert.ToInt32(statsDataGrid.Rows[2].Cells[1].Value.ToString());
                    statsDataGrid.Rows[2].Cells[1].Value = ++count;
                }
            };

            BeginInvoke(invoker);
        }

        #endregion

        //DATABASE

        #region database methods

        private void CheckDatabase(String plateNumber, String picURL)
        {
            plateNumber = plateNumber.Replace(" ", "").ToUpper();
            String qry = "SELECT Person.PersonName, Person.PersonSurname, Car.IsRegistered FROM Car, Person WHERE Car.CarNumberPlate = '"+plateNumber
                +"' AND Car.PersonID = Person.IDNumber";

            _vehicleCount++;

            OleDbCommand command = new OleDbCommand(qry, _conn);
            OleDbDataReader reader = command.ExecuteReader();

            if (reader != null && !reader.HasRows)
            {
                DisplayPlate(new LicencePlate("error-" + plateNumber, false));
                sendSingleCarInfoToAll(plateNumber, "<UNKNOWN>", "UNKNOWN", picURL);
            }
                
            while (reader != null && reader.Read())
            {
                String fname = reader.GetString(0);
                String sname = reader.GetString(1);
                Boolean isRegistered = reader.GetBoolean(2);

                DisplayPlate(new LicencePlate(plateNumber, isRegistered));

                sendSingleCarInfoToAll(fname, sname, plateNumber, picURL);
            }

            if (reader != null)
                reader.Close();
        }

        public void sendSingleCarInfoToAll(String plate, String fname, String sname, String URL)
        {
            if (_server != null)
                _server.sendSingleCarInfoToAll(plate,fname,sname,URL);
        }

        private void ConnectToDb()
        {
            Console.WriteLine("Connecting to database");

            string strSqlConn = "Provider=Microsoft.ACE.OLEDB.12.0;Data Source=LPDatabase.accdb";
            _conn = new OleDbConnection(strSqlConn);

            _conn.Open();

            Console.WriteLine("Successfully connected to database");
        }


        public void runQuery(String qry)
        {
            //TODO have it return a dataset for that qry
        }

        #endregion

        //ANIMATIONS

        #region animation methods

        

        #endregion

        //FORM CLOSING (releasing of all resources)

        #region form closing

        private void Form1_FormClosing(object sender, FormClosingEventArgs eventArgs)
        {
            try
            {
                foreach (var control in _videoControls)
                    control.CloseVideoSource();

                if (_conn != null)
                    _conn.Close();
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
            }
            finally
            {
                if (_server != null)
                    _server.disconnectServer();
            }
        }

        #endregion

        private void Form1_Load(Object sender, EventArgs e)
        {
            Console.WriteLine("HIT");
        }
    }
}