﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CS3280InvoiceSystem.Search
{
    class clsSearchLogic
    {
        /// <summary>
        /// Used for sql queries.
        /// </summary>
        clsSearchSQL sql;

        public clsSearchLogic()
        {
            sql = new clsSearchSQL();
        }
    }
}
