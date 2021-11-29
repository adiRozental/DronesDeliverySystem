﻿using System;
using IBL.BO;
//using IDAL.DO;
//using DalObject;
using System.Linq;
using System.Collections.Generic;

namespace IBL
{
    public partial class BL/*: IBL.IBL*/
    {
        // public IDAL.IDal dl;
        public IDAL.IDal AccessIdal;
        List<DroneToList> DronesBL;
        internal static Random rand;//random
        internal static int chargeRate;

        public BL()
        {
            AccessIdal = new DalObject.DalObject();
            rand =  new Random(DateTime.Now.Millisecond);
            chargeRate = AccessIdal.GetChargeRate();
            DronesBL = (List<DroneToList>)(from item in AccessIdal.GetALLDrone()
                                           select new DroneToList()
                                           {
                                               Id = item.Id,
                                               Model = item.Model,
                                               MaxWeight = (WeightCategories)item.MaxWeight,
                                               Status = 0,
                                               //CurrentLocation = new Location() { Lattitude = item.Lattitude, Longitude = item.Longitude },
                                               // IdOfParcel=item.
                                               Battery = rand.Next(20, 41),
                                               IdOfParcel=-1
                                               

                                           });
            foreach(DroneToList item in DronesBL)
            {
                if(item.IdOfParcel!=-1)
                {
                    if(AccessIdal.CheckParcel(item.IdOfParcel))
                    {
                        item.Status = (DroneStatuses)2;
                        //battery status
                        IDAL.DO.Parcel p = AccessIdal.GetParcel(item.IdOfParcel);
                        DateTime d = new DateTime(0, 0, 0);
                        if(p.PickedUp==d)
                        {
                            //פונק של מרחק
                        }
                        else if(p.Delivered==d)
                        {
                            IDAL.DO.Customer c = AccessIdal.GetCustomer(p.SenderId);

                            item.CurrentLocation = new Location() { Lattitude = c.Lattitude, Longitude = c.Longitude };
                        }
                    }
                }
                if(item.Status!=DroneStatuses.Delivery)
                {
                    //avaitabal/maintains
                }
                if(item.Status == DroneStatuses.Maintenance)
                {
                    //הגרלה בין תחנות
                    item.Battery = rand.Next(20, 41);
                
                }
                if (item.Status == DroneStatuses.Available)
                {
                    //הגרלות הגרלות
                }

            }

        }

        private Station dis(double lon, double lat)
        {
            double max=0, d = 0;
            int ids=0;
            foreach (IDAL.DO.Station  item in AccessIdal.GetALLStation())
            {
                d = AccessIdal.StationDistance(lat, lon, item.Id);
                if(d<max)
                {
                    max = d;
                    ids = item.Id;
                }
            }

            return GetStation(ids);
        }

        private double amountOfbattery(Drone d,Location l)
        {
            double[] arr = AccessIdal.ElectricityUse();
            double s;
            s = getDistanceFromLatLonInKm(d.CurrentLocation.Lattitude, d.CurrentLocation.Longitude, l.Lattitude, l.Lattitude);

            if (d.Status == DroneStatuses.Available)
            {
                s *= arr[0];
            }
            else
            {
                switch (d.MaxWeight)
                {
                    case WeightCategories.Light:
                        s *= arr[1];
                        break;
                    case WeightCategories.Medium:
                        s *= arr[2];
                        break;
                    case WeightCategories.Heavy:
                        s *= arr[3];
                        break;
                    default:
                        break;
                }
            }
            return s;
        }

        private double amountOfbatteryFrom(Drone d, Location l,Location L2)
        {
            double[] arr = AccessIdal.ElectricityUse();
            double s;
            s = getDistanceFromLatLonInKm(l.Lattitude, l.Longitude, L2.Lattitude, L2.Lattitude);

            if (d.Status == DroneStatuses.Available)
            {
                s *= arr[0];
            }
            else
            {
                switch (d.MaxWeight)
                {
                    case WeightCategories.Light:
                        s *= arr[1];
                        break;
                    case WeightCategories.Medium:
                        s *= arr[2];
                        break;
                    case WeightCategories.Heavy:
                        s *= arr[3];
                        break;
                    default:
                        break;
                }
            }
            return s;
        }
        private double Deg2rad(double deg)
        {
            return deg * (Math.PI / 180);
        }
        public double getDistanceFromLatLonInKm(double lat1, double lon1, double lat2, double lon2)
        {
            double R = 6371; // Radius of the earth in km
            double dLat = Deg2rad(lat2 - lat1);  // deg2rad below
            double dLon = Deg2rad(lon2 - lon1);
            double a = Math.Sin(dLat / 2) * Math.Sin(dLat / 2) +
              Math.Cos(Deg2rad(lat1)) * Math.Cos(Deg2rad(lat2)) *
              Math.Sin(dLon / 2) * Math.Sin(dLon / 2);///calculating by the formula
            double c = 2 * Math.Atan2(Math.Sqrt(a), Math.Sqrt(1 - a));
            double d = R * c; // Distance in km
            return d;
        }
    }
}
