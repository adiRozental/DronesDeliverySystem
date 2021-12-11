﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IDAL
{
    public interface IDal
    {
        #region Customer
        void AddCustomer(DO.Customer cus);
        public void UpdateCustomer(DO.Customer newD);
        public DO.Customer GetCustomer(int id);
        public bool CheckCustomer(int id);
        double CustomerDistance(double lat, double lon1, int id);
        DO.Customer ShowOneCustomer(int _id);
        IEnumerable<DO.Customer> ListCustomer();
        public IEnumerable<DO.Customer> GetALLCustomer();
        #endregion

        #region Drone
        void AddDrone(DO.Drone d);
        public DO.Drone GetDrone(int id);
        public bool CheckDrone(int id);
        void LinkParcelToDrone(int parcelId, int droneId);
        void DroneToCharge(int droneId, int stationId);
        void EndingCharge(int droneId);
        public  int GetChargeRate();

        DO.Drone ShowOneDrone(int _id);
        public void UpdateDrone(DO.Drone newD);
        double[] ElectricityUse();
        IEnumerable<DO.Drone> ListDrone();
        public IEnumerable<DO.Drone> GetALLDrone();

        #endregion

        #region Parcel
        public IEnumerable<DO.Parcel> GetAllUnMachedParcel();
        void AddParcel(DO.Parcel per);
        public void UpdateParcel(DO.Parcel newD);
        public DO.Parcel GetParcel(int id);
        public bool CheckParcel(int id);
        void PickParcel(int parcelId);
        void DeliveringParcel(int parcelId);
        DO.Parcel ShowOneParcel(int _id);
        IEnumerable<DO.Parcel> ListParcel();
        public IEnumerable<DO.Parcel> GetALLParcel();
        #endregion

        #region Station
        double StationDistance(double lat, double lon1, int id);
        public void UpdateStation(DO.Station newD);
        void AddStation(DO.Station s);
        public DO.Station GetStation(int id);
        public bool CheckStation(int id);
        DO.Station ShowOneStation(int _id);
        IEnumerable<DO.Station> ListStation();
        public IEnumerable<DO.Station> GetALLStation();
        int NumOfChargingNow(int x);
        double getDistanceFromLatLonInKm(double lat, double long1, double lat2, double long2 );

        #endregion

        #region DroneCharge
        public IEnumerable<DO.DroneCharge> GetALLDroneCharges();
        DO.DroneCharge GetDroneCharge(int id);
        public void DeleteDroneCharge(int id);
        public void AddDroneCharge(DO.DroneCharge dc);
        public void UpdateDroneCharge(DO.DroneCharge dc);
        public bool CheckDC(int id);

        #endregion

    }
}
