using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.IO.MemoryMappedFiles;
using System.Resources;
using System.IO;
using System.Xml;
using System.Web;
using MySql.Data.MySqlClient;





namespace WindowsFormsApp3
{
    public partial class Form1 : Form
    {
        
        string general_xml_path = System.IO.File.ReadAllText(Directory.GetCurrentDirectory() + "\\general_xml_path.txt");
    
        public struct Input_Metadata
        {
            public string type;
            public List<string> values;
        }

        public Form1()
        {
            InitializeComponent();
        }

        private void Form1_Load(object sender, EventArgs e)
        {

            
        }
        public void BindData(string query)
        {
            MySqlConnection con = new MySqlConnection("server=localhost;user id=root;persistsecurityinfo=True;database=databaseproject;password=admin");
            con.Open();


            MySqlCommand cmd = new MySqlCommand(query, con);
            MySqlDataAdapter adp = new MySqlDataAdapter(cmd);
            DataTable ds = new DataTable();
            adp.Fill(ds);

            dataGridView1.DataSource = ds;
            dataGridView1.DataMember = ds.TableName;
            cmd.Dispose();
            con.Close();
        }

        private void dataGridView1_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

        }

        private void textBox1_TextChanged(object sender, EventArgs e)
        {

        }

        private void button1_Click(object sender, EventArgs e)
        {

            //string text = System.IO.File.ReadAllText(@"C:\Users\Matthew\Documents\Life\School\UofI\CS360\projTest.txt");
            //MessageBox.Show(text);

            string[] input = parse_Sentence(textBox1.Text);

            

            SentenceAnalyzer words = new SentenceAnalyzer();

            Input_Metadata data = find_sentence_type(input, ref words);

            string sql_code = getSQL(data, input, ref words);

            MessageBox.Show(sql_code);

            BindData(sql_code);


            //print_list(test2.get);
           // print_list(test2.greater);
           // print_list(test2.less);
           // print_list(test2.over1);
           // print_list(test2.condition);
           // print_list(test2.list_of_attributes);
           // print_list(test2.list_of_tables);
           // print_nested_list(test2.relationship);
           // print_nested_list(test2.synonyms);
        }

        private void label1_Click(object sender, EventArgs e)
        {

        }

        public void print_list(List<string> list)
        {

            for (int i = 0; i < list.Count; i++)
                MessageBox.Show(list[i]);
        }

        public void print_nested_list(List<List<string>> list)
        {

            for (int a = 0; a < list.Count; a++)
            {
                for (int b = 0; b < list[a].Count; b++)
                    MessageBox.Show("table" + a.ToString() + " " + list[a][b]);
            }
        }

        public string[] parse_Sentence(string arg)
        {
            char[] letters = {' ', '\'', 'a', 'b', 'c', 'd', 'e', 'f', 'g', 'h', 'i', 'j', 'k', 'l', 'm', 'n', 'o', 'p', 'q', 'r', 's', 't', 'u', 'v', 'w', 'x', 'y', 'z', '0', '1', '2', '3', '4', '5', '6', '7', '8', '9'};

            string input = NormalizeWhiteSpace(arg);

            input = input.ToLower();

            for (int i = 0; i < input.Length; i++)
            {
                if (!(letters.Contains<char>(input[i])))
                {
                    input = input.Remove(i, 1);
                    i--;
                }

            }

           

            //MessageBox.Show(input);

            string[] words = input.Split(' ');


            //for (int i = 0; i < words.Length; i++)
               // MessageBox.Show(words[i]);


            return words;

        }

        public static string NormalizeWhiteSpace(string S)
        {
            string s = S.Trim();
            bool iswhite = false;
            
            int sLength = s.Length;
            StringBuilder sb = new StringBuilder(sLength);
            foreach (char c in s.ToCharArray())
            {
                if (Char.IsWhiteSpace(c))
                {
                    if (iswhite)
                    {
                        //Continuing whitespace ignore it.
                        continue;
                    }
                    else
                    {
                        //New WhiteSpace

                        //Replace whitespace with a single space.
                        sb.Append(" ");
                        //Set iswhite to True and any following whitespace will be ignored
                        iswhite = true;
                    }
                }
                else
                {
                    sb.Append(c.ToString());
                    //reset iswhitespace to false
                    iswhite = false;
                }
            }
            return sb.ToString();
        }
        // get word = G
        // condition word = C
        // attribute word = A
        // table word = T
        // over1 word = 1

        public Input_Metadata find_sentence_type(string[] input, ref SentenceAnalyzer words)
        {
           
            string type = "";
            List<string> values = new List<string>();

            for (int i = 0; i < input.Length; i++)
            {

                if (input[i][0] == '\'' && input[i][input[i].Length - 1] == '\'')
                {

                    type += "V";
                    values.Add(input[i]);
                    continue;
                }

                for (int j = 0; j < words.synonyms.Count; j++)
                {
                    if (words.synonyms[j].Contains(input[i]))
                    {

                        type += "A";
                        values.Add(words.list_of_attributes[j]);
                    }
                    
                }

                

                for (int j = 0; j < words.list_of_tables.Count; j++)
                {

                    if (input[i] == words.list_of_tables[j])
                    {
                        type += "T";
                        values.Add(words.list_of_tables[j]);
                    }
                }

                

                for (int j = 0; j < words.get.Count; j++)
                {

                    if (input[i] == words.get[j])
                    {
                        type += "G";
                        values.Add(words.get[j]);
                    }
                }

                

                //for (int j = 0; j < words.over1.Count; j++)
                //{

                   // if (input[i] == words.over1[j])
                   // {
                       // type += "1";
                       // values.Add(words.over1[j]);
                   // }
               // }

                

                for (int j = 0; j < words.condition.Count; j++)
                {

                    if (input[i] == words.condition[j])
                    {
                        type += "C";
                        values.Add(words.condition[j]);
                    }
                }

                for (int j = 0; j < words.greater.Count; j++)
                {

                    if (input[i] == words.greater[j])
                    {
                        type += "L";
                        values.Add(words.greater[j]);
                    }
                }

                for (int j = 0; j < words.less.Count; j++)
                {

                    if (input[i] == words.less[j])
                    {
                        type += "L";
                        values.Add(words.less[j]);
                    }
                }
            }

            Input_Metadata data = new Input_Metadata();

            data.type = type;
            data.values = values;

            //MessageBox.Show(data.type);




            return data;

        }

        public string getSQL(Input_Metadata data, string[] input, ref SentenceAnalyzer words)
        {
            string sql = "";

            
            // checks for type GAAAA...

            if (isGA(data.type))
            {
                GA command_segment = new GA();

                for (int j = 1; j < data.values.Count; j++)
                {

                    for (int i = 0; i < words.relationship.Count; i++)
                    {
                        if (words.relationship[i].Contains(data.values[j]) && !command_segment.from.Contains(words.list_of_tables[i]))
                        {
                            command_segment.from.Add(words.list_of_tables[i]);
                        }
                    }

                    command_segment.select.Add(data.values[j]);

                }

                

                return command_segment.generate_SQL();

            }


            // checks for type GATAAT...

            if (isGTA(data.type))
            {
               
                GT command_segment = new GT();

                for (int j = 1; j < data.values.Count; j++)
                {

                    for (int i = 0; i < words.relationship.Count; i++)
                    {
                        if (words.relationship[i].Contains(data.values[j]) && !command_segment.from.Contains(words.list_of_tables[i]))
                        {
                            command_segment.from.Add(words.list_of_tables[i]);
                        }

                        if (words.list_of_tables.Contains(data.values[j]) && !command_segment.from.Contains(data.values[j]))
                            command_segment.from.Add(data.values[j]);
                    }

                }

                return command_segment.generate_SQL();

            }

            if (data.type == "GACALV")
            {
                // the attribute that is being projected is the 2nd element of the values list
                GACALV command_segment = new GACALV();

                // the value of the SELECT statement is found

                command_segment.select = data.values[1];

                // the value of the FROM statement is found
                for (int i = 0; i < words.relationship.Count; i++)
                {
                    // the attribute is checked against the double list of attributes and their tables. If the attribute is found in the ith list, then it belongs to the ith table, so that is where the query is called from

                    if (words.relationship[i].Contains(data.values[1]))
                        command_segment.from = words.list_of_tables[i];
                }

                // the value of the WHERE statement is found

                // the value of the logical operator in the sql statement is found

                if (check_Greater(input, ref words))
                    command_segment.logic = ">";

                else if (check_Less(input, ref words))
                    command_segment.logic = "<";

                else
                    command_segment.logic = "=";

                // the name of the attribute used in the condition is found

                command_segment.where = data.values[3];

                // the value that the attribute is being checked against is found
                //for (int j = 0; j < input.Length; j++)
                //{
                //if (words.over1.Contains(input[j]))
                // {
                // command_segment.value = input[j + 1];

                //MessageBox.Show(command_segment.generate_SQL());
                // }


                //} 

                command_segment.value = data.values[5];

                return command_segment.generate_SQL();
            }

            if (data.type == "GACAV")
            {
                // the attribute that is being projected is the 2nd element of the values list
                GACAV command_segment = new GACAV();

                // the value of the SELECT statement is found

                command_segment.select = data.values[1];

                // the value of the FROM statement is found
                for (int i = 0; i < words.relationship.Count; i++)
                {
                    // the attribute is checked against the double list of attributes and their tables. If the attribute is found in the ith list, then it belongs to the ith table, so that is where the query is called from

                    if (words.relationship[i].Contains(data.values[1]))
                        command_segment.from = words.list_of_tables[i];
                }

                // the value of the WHERE statement is found

                // the value of the logical operator in the sql statement is found

               
                command_segment.logic = "=";

                // the name of the attribute used in the condition is found

                command_segment.where = data.values[3];

                // the value that the attribute is being checked against is found
                //for (int j = 0; j < input.Length; j++)
                //{
                //if (words.over1.Contains(input[j]))
                // {
                // command_segment.value = input[j + 1];

                //MessageBox.Show(command_segment.generate_SQL());
                // }


                //} 

                command_segment.value = data.values[4];

                return command_segment.generate_SQL();
            }


            if (data.type == "GTCALV")
            {

                GTCALV command_segment = new GTCALV();

                command_segment.from = data.values[1];

                command_segment.where = data.values[3];

                if (check_Greater(input, ref words))
                    command_segment.logic = ">";

                else if (check_Less(input, ref words))
                    command_segment.logic = "<";

                else
                    command_segment.logic = "=";

                command_segment.value = data.values[5];

                return command_segment.generate_SQL();

            }

            if (data.type == "GTCAV")
            {

                GTCAV command_segment = new GTCAV();

                command_segment.from = data.values[1];

                command_segment.where = data.values[3];

                command_segment.logic = "=";

                command_segment.value = data.values[4];

                return command_segment.generate_SQL();

            }

            if (data.type == "CALVGA")
            {

                // the attribute that is being projected is the 2nd element of the values list
                CALVGA command_segment = new CALVGA();

                // the value of the SELECT statement is found

                command_segment.select = data.values[5];

                // the value of the FROM statement is found
                for (int i = 0; i < words.relationship.Count; i++)
                {
                    // the attribute is checked against the double list of attributes and their tables. If the attribute is found in the ith list, then it belongs to the ith table, so that is where the query is called from

                    if (words.relationship[i].Contains(data.values[5]))
                        command_segment.from = words.list_of_tables[i];
                }

                // the value of the WHERE statement is found

                // the value of the logical operator in the sql statement is found

                if (check_Greater(input, ref words))
                    command_segment.logic = ">";

                else if (check_Less(input, ref words))
                    command_segment.logic = "<";

                else
                    command_segment.logic = "=";

                // the name of the attribute used in the condition is found

                command_segment.where = data.values[1];

               

                command_segment.value = data.values[3];

                return command_segment.generate_SQL();

            }

            if (data.type == "CAVGA")
            {

                // the attribute that is being projected is the 2nd element of the values list
                CAVGA command_segment = new CAVGA();

                // the value of the SELECT statement is found

                command_segment.select = data.values[4];

                // the value of the FROM statement is found
                for (int i = 0; i < words.relationship.Count; i++)
                {
                    // the attribute is checked against the double list of attributes and their tables. If the attribute is found in the ith list, then it belongs to the ith table, so that is where the query is called from

                    if (words.relationship[i].Contains(data.values[4]))
                        command_segment.from = words.list_of_tables[i];
                }

                // the value of the WHERE statement is found

                // the value of the logical operator in the sql statement is found
                command_segment.logic = "=";

                // the name of the attribute used in the condition is found

                command_segment.where = data.values[1];



                command_segment.value = data.values[2];

                return command_segment.generate_SQL();
            }

            if (data.type == "CALVGT")
            {

                // the attribute that is being projected is the 2nd element of the values list
                CALVGT command_segment = new CALVGT();


                // the value of the FROM statement is found

                command_segment.from = data.values[5];
                

                // the value of the WHERE statement is found

                // the value of the logical operator in the sql statement is found

                if (check_Greater(input, ref words))
                    command_segment.logic = ">";

                else if (check_Less(input, ref words))
                    command_segment.logic = "<";

                else
                    command_segment.logic = "=";

                // the name of the attribute used in the condition is found

                command_segment.where = data.values[1];



                command_segment.value = data.values[3];

                return command_segment.generate_SQL();

            }

            if (data.type == "CAVGT")
            {

                // the attribute that is being projected is the 2nd element of the values list
                CAVGT command_segment = new CAVGT();


                // the value of the FROM statement is found

                command_segment.from = data.values[4];


                // the value of the WHERE statement is found

                // the value of the logical operator in the sql statement is found
                
                command_segment.logic = "=";

                // the name of the attribute used in the condition is found

                command_segment.where = data.values[1];



                command_segment.value = data.values[2];

                return command_segment.generate_SQL();

            }

            return sql;
        }

        public bool check_Greater(string[] input, ref SentenceAnalyzer words)
        {

            for (int i = 0; i < input.Length; i++)
            {
                if (words.greater.Contains(input[i]))
                    return true;
            }

            return false;
        }

        public bool check_Less(string[] input, ref SentenceAnalyzer words)
        {

            for (int i = 0; i < input.Length; i++)
            {
                if (words.less.Contains(input[i]))
                    return true;
            }

            return false;
        }

        public bool isGA(string type)
        {

            bool flag = true;

            if (type[0] == 'G')
            {

                for (int i = 1; i < type.Length; i++)
                {
                    if (type[i] != 'A')
                        flag = false;
                }
            }
            else
                flag = false;

            return flag;
        }

        public bool isGTA(string type)
        {

            bool flag = true;

            if (type[0] == 'G')
            {

                for (int i = 1; i < type.Length; i++)
                {

                    //MessageBox.Show(type[i].ToString());

                    if ((type[i] != 'T') && (type[i] != 'A'))
                    {
                        //MessageBox.Show("sfsd");
                        flag = false;
                    }
                }
            }
            else
                flag = false;
            
            return flag;
        }



    }
}
