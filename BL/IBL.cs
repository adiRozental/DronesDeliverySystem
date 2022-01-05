﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BlApi
{
    public interface IBL
    {
        #region Drone
        public BO.Drone GetDrone(int id);
         IEnumerable<BO.Drone> GetAllDrones();
        public void AddDrone(BO.Drone d, int x);
        public void UpdateDrone(int id, string m);
        public void DroneToCharge(int id);
        public void EndCharging(int id/*, int timeI*/);
        public void LinkDroneToParcel(int id);
        public IEnumerable<BO.DroneToList> ListDrone();
        public IEnumerable<BO.Drone> GetDroneBy(Predicate<BO.Drone> P);

        #endregion

        #region Customer
        public IEnumerable<BO.Customer> GetCustomerBy(Predicate<BO.Customer> P);

        public void AddCustomer(BO.Customer d);
        public void UpdateCustomer(int cusid, string cusName, string cusPhone);
        public IEnumerable<BO.Customer> GetAllCustomers();

        public IEnumerable<BO.Customer> GetAllCustomersThatHasDeliveredParcels();
        public BO.Customer GetCustomer(int id);
        public IEnumerable<BO.Customer> GetAllCusromerRecived();

        #endregion

        #region Parcel
        public void RemoveParcel(int id);
        public IEnumerable<BO.Parcel> GetParcelBy(Predicate<BO.Parcel> P);
        public void AddParcel(BO.Parcel p);
        public IEnumerable<BO.Parcel> GetAllParcels();
        public BO.Parcel GetParcel(int id);
        public void UpdateParcel(BO.Parcel p);
        public void PickParcel(int id);
        public int GetNextParcel();
        public void DeliveringParcel(int id);
        public IEnumerable<BO.Parcel> GetAllUnmachedParcels();

        #endregion

        #region Station
         IEnumerable<BO.Station> GetStationsforNoEmpty();
        public BO.Station GetStation(int id);

        public void AddStation(BO.Station s);
        public void Updatestation(int id, string name, int numOfChargingSlots);
        public IEnumerable<BO.Station> GetAllStations();
            #endregion

        }
}
