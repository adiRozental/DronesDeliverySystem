﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
//using IBAL.BO;
using IBL.BO;
//using IDAL.DO;

namespace IBL
{
    public partial class BL 
    {
        private Customer customerDoBoAdapter(IDAL.DO.Customer customerDO)
        {
            Customer customerBO = new Customer();
            int id = customerDO.Id;
            IDAL.DO.Customer s;
            try //???
            {
                s = AccessIdal.GetCustomer(id);
            }
            catch (IDAL.DO.BadIdException)
            {

                throw new BadIdException("station");
            }
            s.CopyPropertiesTo(customerBO);
            customerDO.CopyPropertiesTo(customerBO);
            //stationBO.DronesinCharge= from sic in AccessIdal.GetALLDrone(sic=> sic.Id==Id )
            //                          let 
            return customerBO;

        }
        public void AddCustomer(Customer newCustomer)
        {
            
            IDAL.DO.Customer newCus = new IDAL.DO.Customer()
            {
                Id = newCustomer.Id,
                Name = newCustomer.Name,
                Phone = newCustomer.Phone,
                Longitude = newCustomer.CustLocation.Longitude,
                Lattitude = newCustomer.CustLocation.Lattitude     
              
            };
            try
            {
                AccessIdal.AddCustomer(newCus);
            }
            catch (IDAL.DO.IDExistsException)
            {
               throw new IDExistsException("customer");
            }
        }
        public void UpdateCustomer(int cusId, string cusName, string cusPhone)
        {
            try
            {
                IDAL.DO.Customer cus = AccessIdal.GetCustomer(cusId);
                if (cusName != "")
                    cus.Name = cusName;
                if (cusPhone != "")
                    cus.Phone = cusPhone;
                //AccessIdal.
            }
            catch (IDAL.DO.BadIdException)
            {

                throw new BadIdException("customer");
            }
        }
        public IEnumerable<Customer> GetAllCustomers()
        {
            return from item in AccessIdal.GetALLCustomer()
                   select new Customer()
                   {
                       Id = item.Id,
                       Name = item.Name,
                       Phone = item.Phone,
                       CustLocation = new Location() { Longitude = item.Longitude, Lattitude = item.Lattitude },  //////??????????

                   };
        }


    }
}