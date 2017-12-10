using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WindowsFormsApp3
{
    class GT
    {
        public List<string> from = new List<string>();

        public string generate_SQL()
        {

            string from_list = "";
            
            for (int i = 0; i < from.Count - 1; i++)
            {
                from_list += from[i] + " natural join ";
            }

            from_list += from[from.Count - 1];

            return "SELECT * FROM " + from_list;
        }
    }
}

