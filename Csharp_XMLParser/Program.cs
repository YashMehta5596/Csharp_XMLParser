using System;
using System.Collections.Generic;
using System.IO;
using System.Xml;

namespace XMLParser_CSharp
{
    class Program
    {

        /// <summary>
        /// For storing global members
        /// </summary>
        public class GlobalData
        {
            public static string outputFilePath = Path.GetFullPath(@"..\..\Data\SampleFileOutput.txt");
            public static List<string> childNodes = new List<string>();
        };

        /// <summary>
        /// Main method/ Driver method
        /// </summary>
        public static void Main()
        {
            //Create the XmlDocument
            XmlDocument mFile = new XmlDocument();
            XmlNode mCurrentNode;

            //Load the document with the sample file
            string inputPath = Path.GetFullPath(@"..\..\Data\Sample.xml");


            mFile.Load(inputPath);
            mCurrentNode = mFile.DocumentElement;

            //Select the node from the file
            XmlNodeList nodeList = mCurrentNode.SelectNodes("/parent/child/PLACES/place");


            DisplayListofChildNodes(nodeList);

            string[] result = GlobalData.childNodes.ToArray();
            //Adding result to file
            File.WriteAllLines(GlobalData.outputFilePath, Array.ConvertAll(result, x => x.ToString()));

        }


        /// <summary>
        /// Gives the list of child nodes present in the input file
        /// </summary>
        /// <param name="nodeList">list of nodes from the file</param>
        static void DisplayListofChildNodes(XmlNodeList nodeList)
        {
            foreach (XmlNode node in nodeList)
            {
                TraverseXmlFileNoSiblings(node);
            }
        }

        /// <summary>
        /// Traverse the Child elements
        /// </summary>
        /// <param name="rootElement">root element of the file</param>
        static void TraverseXmlFileNoSiblings(XmlNode rootElement)
        {
            if (rootElement is XmlElement)
            {
                //Console.WriteLine(rootElement.Name);
                if (rootElement.HasChildNodes)
                    TraverseXmlFile(rootElement.LastChild);
            }
        }

        /// <summary>
        /// Traverse the root element along with its siblings and finds the child element
        /// </summary>
        /// <param name="rootElement">root element of the file</param>
        static void TraverseXmlFile(XmlNode rootElement)
        {
            if (rootElement is XmlElement)
            {
                //Console.WriteLine(rootElement.Name);
                if (rootElement.HasChildNodes)
                    TraverseXmlFile(rootElement.FirstChild);
                if (rootElement.NextSibling != null)
                    TraverseXmlFile(rootElement.NextSibling);
                if (rootElement.NextSibling == null && rootElement.LastChild.Value != null)
                {
                    //Adding childnode in list
                    GlobalData.childNodes.Add(rootElement.LastChild.Value);

                    //Also displaying same response on console
                    Console.WriteLine(rootElement.LastChild.Value);
                }
            }
        }
    }
}