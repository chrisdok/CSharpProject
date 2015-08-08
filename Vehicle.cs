using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Rent_a_Car
{
    class Vehicle
    {
        public int VehicleID { get; protected set; }

        public static void RegisterVehicle()
        {
            throw new NotImplementedException();
        }

        public static Vehicle GetVehicle(int VehicleID)
        {
            return new Vehicle();
        }

        public static void RentAVehicle()
        {
            throw new NotImplementedException();
        }
    }
}
