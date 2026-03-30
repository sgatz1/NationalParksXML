using Newtonsoft.Json;
using System;
using System.Xml;
using System.Xml.Schema;
using static System.Net.WebRequestMethods;

namespace ConsoleApp1
{
    public class Submission
    {
        // 🔗 Replace these with your GitHub RAW URLs
        public static string xmlURL = "https://raw.githubusercontent.com/sgatz1/NationalParksXML/refs/heads/main/Nationalparks.xml";
        public static string xmlErrorURL = "https://raw.githubusercontent.com/sgatz1/NationalParksXML/refs/heads/main/NationalParksErrors.cml";
        public static string xsdURL = "https://raw.githubusercontent.com/sgatz1/NationalParksXML/refs/heads/main/NationalParks.xsd";

        public static void Main(string[] args)
        {
            // Q3.1 - Validate correct XML
            string result = Verification(xmlURL, xsdURL);
            Console.WriteLine(result);

            // Q3.2 - Validate error XML
            result = Verification(xmlErrorURL, xsdURL);
            Console.WriteLine(result);

            // Q3.3 - Convert XML to JSON
            result = Xml2Json(xmlURL);
            Console.WriteLine(result);
        }

        // Q2.1 - XML Validation
        public static string Verification(string xmlUrl, string xsdUrl)
        {
            try
            {
                XmlSchemaSet schemas = new XmlSchemaSet();
                schemas.Add("", xsdUrl);

                XmlReaderSettings settings = new XmlReaderSettings();
                settings.ValidationType = ValidationType.Schema;
                settings.Schemas = schemas;

                string errors = "";

                settings.ValidationEventHandler += (sender, e) =>
                {
                    errors += e.Message + "\n";
                };

                XmlReader reader = XmlReader.Create(xmlUrl, settings);

                while (reader.Read()) { }

                if (errors == "")
                    return "No errors are found";
                else
                    return errors;
            }
            catch (Exception ex)
            {
                return ex.Message;
            }
        }

        // Q2.2 - Convert XML to JSON
        public static string Xml2Json(string xmlUrl)
        {
            try
            {
                XmlDocument doc = new XmlDocument();
                doc.Load(xmlUrl);

                string jsonText = JsonConvert.SerializeXmlNode(doc, Newtonsoft.Json.Formatting.Indented);

                return jsonText;
            }
            catch (Exception ex)
            {
                return ex.Message;
            }
        }
    }
}