﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IBL
{
    namespace BO
    {
        public class StationsToList
        {
            public int Id { get; set; }
            public string Name { get; set; }
            public int EmptyChargeSlots { get; set; }
            public int FullChargeSlots { get; set; }

        }
    }
}