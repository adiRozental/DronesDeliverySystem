﻿using System;
using IBL.BO;
//using IDAL.DO;
//using DalObject;
using System.Linq;
using System.Collections.Generic;

namespace IBL
{
    public partial class BL: IBL.IBL
    {
        // public IDAL.IDal dl;
        public IDAL.IDal AccessIdal;
        List<DroneToList> DronesBL;
        internal static Random rand;//random


        public BL()
        {
            AccessIdal = new DalObject.DalObject();
            rand =  new Random(DateTime.Now.Millisecond);
        }
    }
}