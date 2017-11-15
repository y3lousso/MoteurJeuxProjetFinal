using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml;
using System.IO;

namespace MoteurJeuxProjetFinal
{
    class XML_Manager
    {

        /// <summary>
        /// Create xml file (allow us to create game without editor, you can go directly inside the xml file if you want)
        /// </summary>
        public void CreateGameContentFromXMLFile(string GameName)
        {
            XmlTextWriter writer = new XmlTextWriter(GameName, System.Text.Encoding.UTF8);
            writer.WriteStartDocument(true);
            writer.Formatting = Formatting.Indented;
            writer.Indentation = 2;
            writer.WriteStartElement(GameName);
            AddScene("Scene 1", writer);
            AddScene("Scene 2", writer);
            writer.WriteEndElement();
            writer.WriteEndDocument();
            writer.Close();
            //MessageBox.Show("XML File created ! ");
        }


        /// <summary>
        /// Load xml file (allow us to create game without editor, you can go directly inside the xml file if you want)
        ///xml parsing
        ///for each entities
        ///create entities
        ///for each components in entities
        ///add components to entities
        ///for each components
        ///set component values
        ///end for
        ///end for
        ///end for
        /// </summary>
        public void LoadGameContentFromXMLFile(string GameName)
        {
            XmlDataDocument xmldoc = new XmlDataDocument();
            XmlNodeList xmlnode;
            int i = 0;
            string str = null;
            FileStream fs = new FileStream(GameName, FileMode.Open, FileAccess.Read);
            xmldoc.Load(fs);
            xmlnode = xmldoc.GetElementsByTagName("Scene");
            for (i = 0; i <= xmlnode.Count - 1; i++)
            {
                xmlnode[i].ChildNodes.Item(0).InnerText.Trim();
                str = xmlnode[i].ChildNodes.Item(0).InnerText.Trim() + "  " + xmlnode[i].ChildNodes.Item(1).InnerText.Trim() + "  " + xmlnode[i].ChildNodes.Item(2).InnerText.Trim();
                //MessageBox.Show(str);
            }
        }

        private void AddScene(string sceneName, XmlTextWriter writer)
        {
            writer.WriteStartElement("Scene");
            writer.WriteString(sceneName);
            writer.WriteEndElement();
        }

        /*private void AddEntityToScene(string entity, string sceneName, XmlTextWriter writer)
        {
            writer.WriteStartAttribute(sceneName, entity, writer);
            writer.WriteString(entity);
            writer.WriteEndElement();
        }*/
    }
}
