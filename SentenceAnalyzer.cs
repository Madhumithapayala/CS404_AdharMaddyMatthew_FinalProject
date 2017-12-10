using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;
using System.Xml;
using System.Windows.Forms;



namespace WindowsFormsApp3
{
    public class SentenceAnalyzer
    { // the lists of words used in the analysis are declared here and initialized in the constructor

        public List<string> list_of_tables;
        public List<string> list_of_attributes;
        public List<List<string>> relationship;
        public List<List<string>> synonyms;
        public List<string> condition;
        public List<string> get;
        public List<string> over1;
        public List<string> greater;
        public List<string> less;

        // this is the path to the foler with the xml files
        public string general_xml_path = System.IO.File.ReadAllText(Directory.GetCurrentDirectory() + "\\general_xml_path.txt");

        public SentenceAnalyzer()
        {
            try
            {

                XmlDocument scheme = new XmlDocument();

                list_of_tables = new List<string>();
                list_of_attributes = new List<string>();
                relationship = new List<List<string>>();
                synonyms = new List<List<string>>();
                condition = new List<string>();
                get = new List<string>();
                over1 = new List<string>();
                greater = new List<string>();
                less = new List<string>();

// this code will store values in the list_of_tables, list_of_attributes, relationship lists

                FileStream fs = new FileStream(general_xml_path + "\\Scheme.xml", FileMode.Open, FileAccess.Read);
                scheme.Load(fs);

                XmlNodeList root_node = scheme.GetElementsByTagName("Scheme");


                XmlNodeList table_nodes = root_node[0].ChildNodes;



                for (int j = 0; j < table_nodes.Count; j++)
                {

                    string table_name = table_nodes[j].Name;
                    List<string> attribute_list_of_each_table = new List<string>();

                    list_of_tables.Add(table_name);

                    XmlNodeList attribute_nodes = table_nodes[j].ChildNodes;


                    for (int k = 0; k < attribute_nodes.Count; k++)
                    {
                        string attribute_name = attribute_nodes[k].InnerText;
                        list_of_attributes.Add(attribute_name);

                        attribute_list_of_each_table.Add(attribute_name);

                        //MessageBox.Show(attribute_node[k].InnerText);

                    }

                    relationship.Add(attribute_list_of_each_table);
                }
                
                // this code will store the values in the synonym list

                XmlDocument synDoc = new XmlDocument();

                for (int k = 0; k < list_of_attributes.Count; k++)
                {

                    fs = new FileStream(general_xml_path + "\\" + list_of_attributes[k] + ".xml", FileMode.Open, FileAccess.Read);

                    synDoc.Load(fs);

                    XmlNodeList sub_root_node = synDoc.GetElementsByTagName(list_of_attributes[k]);

                    XmlNodeList current_synonyms = sub_root_node[0].ChildNodes;

                    List<string> temp_syns = new List<string>();

                    for (int a = 0; a < current_synonyms.Count; a++)
                    {

                        temp_syns.Add(current_synonyms[a].InnerText);
                    }

                    synonyms.Add(temp_syns);

                }

                // this code stores values in the get, condition, over1, greater, and less lists

                populate_list("Condition", ref condition);
                populate_list("Get", ref get);
                populate_list("over1", ref over1);
                populate_list("greater", ref greater);
                populate_list("less", ref less);






                // the lists of table names, attribute names, and relationship have been tested and are correct
                // print_list(list_of_attributes);
                //print_list(list_of_tables);

                //print_nested_list(synonyms);



            }
            catch (FileNotFoundException ex) { MessageBox.Show(ex.Message); }

        }

        public void populate_list(string name, ref List<string> list)
        {
            XmlDocument xfile = new XmlDocument();

            FileStream fs = new FileStream(general_xml_path + "\\" + name + ".xml", FileMode.Open, FileAccess.Read);

            xfile.Load(fs);

            XmlNodeList sub_root_node = xfile.GetElementsByTagName(name);

            XmlNodeList current_values = sub_root_node[0].ChildNodes;

       

            for (int a = 0; a < current_values.Count; a++)
            {

                list.Add(current_values[a].InnerText);
            }



        }

    }
}
