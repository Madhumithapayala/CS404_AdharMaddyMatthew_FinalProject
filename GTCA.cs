using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WindowsFormsApp3
{
    class GTCA
    {

        public string from;
        public string where_attribute;
        public string value;
        public string logic;

        public string generate_SQL()
        {

            return ("SELECT *" + " FROM " + from + " WHERE " + where_attribute + " " + logic + " " + value);
        }
    }
}
