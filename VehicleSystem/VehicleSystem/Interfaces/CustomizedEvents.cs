using System.Drawing;
using SimpleLPR2;

namespace VehicleSystem.Interfaces
{
    public class CustomizedEvents
    {
        public delegate void finishedProcessingPlate(object sender, Candidate results, Bitmap image);
    }
}
