using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Xml;

namespace XML_Viewer
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            XmlTextReader reader = new XmlTextReader("C:\\Users\\Fin\\Documents\\RR\\RN-EJ-412-1009-03.xml");
            while (reader.Read())
            {
                switch (reader.NodeType)
                {
                    case XmlNodeType.Element: // If the  nhe node is an element.
                        if (reader.Name == "featureDamage") //If XML line is a feature damage heading (e.g. "4.4 Heatshield")
                        {
                            Console.WriteLine(reader.GetAttribute("id").Substring(1)); //Print heading number "4.4" (minus the first character which is a F)
                            XmlReader inner = reader.ReadSubtree();
                            while (inner.Read())
                            {
                               if(inner.Name == "feature")
                                {

                                }
                            }

                                


                            reader.ReadToFollowing("feature"); //Jump to text element e.g. "Heatshield")
                            do //Jumps to and extracts heading text from "<para>< emphasis emph = "bold" > Heatshield </ emphasis ></ para >"
                            {
                                reader.ReadToDescendant("emphasis"); //Jump into 'para' to get to 'emphasis'
                                Console.WriteLine(reader.ReadElementContentAsString("emphasis", reader.NamespaceURI)); //Read the value
                            } while (reader.ReadToNextSibling("para")); //Sometimes multiple lines in different elements, this captures them all siblings


                            reader.ReadToFollowing("damageAndActions"); // Now jump to first sub-heading (e.g. "4.4.1 Minor damage, dents or distortion")
                            do
                            {
                                Console.WriteLine(reader.GetAttribute("id").Substring(1)); //Print sub-heading number "4.4.1" (minus the first character which is a D)

                                do
                                {
                                    reader.ReadToDescendant("para"); //Jump into 'para' to get to get sub-heading text
                                    Console.WriteLine(reader.ReadElementContentAsString("para", reader.NamespaceURI)); //Print text (e.g. "Minor damage, dents or distortion")
                                } while (reader.ReadToNextSibling("damage"));



                            } while (reader.ReadToNextSibling("damageAndActions")); //Multiple sub-headings

                            Console.WriteLine(); //New line before next heading


                        }

                        break;

                }
            }
        }






        private void button1_Click(object sender, EventArgs e)
        {

        }

    }
}
