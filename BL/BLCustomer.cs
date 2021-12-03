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
        /// <summary>
        /// adapter from dal to bl object
        /// </summary>
        /// <param name="customerDO"></param> DO object
        /// <returns></returns> BO object
        private Customer customerDoBoAdapter(IDAL.DO.Customer customerDO)
        {
            Customer customerBO = new Customer();
            int id = customerDO.Id;
            IDAL.DO.Customer s;
            try 
            {
                s = AccessIdal.GetCustomer(id);
            }
            catch (IDAL.DO.BadIdException)
            {

                throw new BadIdException("customer");
            }
            s.CopyPropertiesTo(customerBO);
            customerDO.CopyPropertiesTo(customerBO);
 
            return customerBO;

        }
        /// <summary>
        /// creating a bl customer and adding it to the dal too
        /// </summary>
        /// <param name="newCustomer"></param>
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
               throw new IDExistsException(newCus.Id,"this customer already exists");//#########################
            }
        }
        /// <summary>
        /// updating the bl customer: name and phone, by its id, and sending to change on the dal level 
        /// </summary>
        /// <param name="cusId"></param>
        /// <param name="cusName"></param>
        /// <param name="cusPhone"></param>
        public void UpdateCustomer(int cusId, string cusName, string cusPhone)
        {
            try
            {
                if (!AccessIdal.CheckCustomer(cusId))
                    throw new IDAL.DO.BadIdException(cusId, "this customer doesn't exist");
                IDAL.DO.Customer cus = AccessIdal.GetCustomer(cusId);
                if (cusName != "")
                    cus.Name = cusName;
                if (cusPhone != "")
                    cus.Phone = cusPhone;
                AccessIdal.UpdateCustomer(cus);
            }
            catch (IDAL.DO.BadIdException)
            {

                throw new BadIdException(cusId,"this customer doesn't exist");
            }
        }
        /// <summary>
        /// thre function returns all the customers
        /// </summary>
        /// <returns></returns>
        public IEnumerable<Customer> GetAllCustomers()
        {
            return from item in AccessIdal.GetALLCustomer()
                   orderby item.Id
                   select GetCustomer(item.Id);                  
        }
        /// <summary>
        /// the function returns all the customers that have deievered parcels
        /// </summary>
        /// <returns></returns>
        public IEnumerable<Customer> GetAllCustomersThatHasDeliveredParcels()
        {
            return from item in AccessIdal.GetALLCustomer()
                   orderby item.Id
                   select GetCustomer(item.Id);
        }

        /// <summary>
        /// the functions gets an id and returs the customer the id belongs to
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public Customer GetCustomer(int id)
        {
            if (!AccessIdal.CheckCustomer(id))
                throw new BadIdException(id, "this customer doesn't"); 
            IDAL.DO.Customer c = AccessIdal.GetCustomer(id);
            
            Customer cb = new Customer()
            {
                Id = id,
                CustLocation = new Location() { Lattitude = c.Lattitude, Longitude = c.Longitude },
                Name = c.Name,
                Phone = c.Phone,     
            };
            cb.parcelToCustomer = new List<Parcel>();
            cb.parcelFromCustomer = new List<Parcel>();
            foreach (Parcel item in GetAllParcels())
            {
                if (item.Sender.Id == c.Id)
                    cb.parcelFromCustomer.Add(item);
            }
            foreach (Parcel item in GetAllParcels())
            {
                if (item.Target.Id == c.Id)
                    cb.parcelToCustomer.Add(item);
            }
            return cb;
        }
        /// <summary>
        /// the functions gets an id and returns the information of customer the id belongs to
        /// </summary>
        /// <param name="_id"></param>
        /// <returns></returns>
        public string ShowOneCustomer(int _id)
        {
            Customer s = GetCustomer(_id); //finding the station by its id
            return s.ToString();
        }
        /// <summary>
        /// the funxtion returns all the customers that has received parcels
        /// </summary>
        /// <returns></returns> list of customer
        public IEnumerable<Customer> GetAllCusromerRecived()
        {
            return from item in GetAllCustomers()
                   where item.parcelToCustomer.Count > 0
                   select GetCustomer(item.Id);
        }
    }
}
