using System;

namespace VehicleSystem.LPR
{
    public class LicencePlate
    {
        public String plateNumber;
        public Boolean isRegistered;

        public LicencePlate(String plateNumber, Boolean isRegistered)
        {
            this.plateNumber = plateNumber;
            this.isRegistered = isRegistered;
        }

        public String[] toArray()
        {
            return new String[] { plateNumber, isRegistered.ToString()};
        }
    }
}
