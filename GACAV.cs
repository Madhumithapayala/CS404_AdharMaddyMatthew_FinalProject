﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WindowsFormsApp3
{
    class GACAV
    {
        public string select;
        public string from;
        public string where;
        public string logic;
        public string value;

        public string generate_SQL()
        {

            return "SELECT " + select + " FROM " + from + " WHERE " + where + " " + logic + " " + value;
        }
    }
}
