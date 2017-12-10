using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WindowsFormsApp3
{
    class GA1
    {
        public string select_attribute;
        public string from;
        public string where_attribute;
        public string value;
        public string logic;

        public string generate_SQL()
        {

            return ("SELECT " + select_attribute + " FROM " + from + " WHERE " + where_attribute + " " + logic + " " + value);
        }
    }
}
