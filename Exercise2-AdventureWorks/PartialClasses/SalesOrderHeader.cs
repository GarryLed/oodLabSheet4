﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Exercise2_AdventureWorks
{
    public partial class SalesOrderHeader
    {
        // override the to string method 
        public override string ToString()
        {
            return string.Format("{0}:{1} {2:c}",
                OrderDate.ToShortDateString(),
                SalesOrderID,
                TotalDue);
        }
    }
}
