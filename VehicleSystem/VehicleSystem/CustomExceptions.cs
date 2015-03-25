using System;

namespace VehicleSystem
{
    public class NoEventHandler : Exception
    {
        public NoEventHandler() : 
            base("LPR has no event handler attached, attach a finishedProcessingPlate event") { }
    }
}
