using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WindowsFormsApp3
{
    class GA
    {
        public List<string> from = new List<string>();

        public List<string> select = new List<string>();
     
        public string generate_SQL()
        {
            string from_list = "";

            for (int i = 0; i < from.Count - 1; i++)
            {
                from_list += from[i] + " natural join ";
            }

            from_list += from[from.Count - 1];

            string select_list = "";

            for (int i = 0; i < select.Count - 1; i++)
            {
                select_list += select[i] + ",";

            }

            select_list += select[select.Count - 1];

            return "SELECT " + select_list + " FROM " + from_list;
        }
    }

}
