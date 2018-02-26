using MenuParsing.Data;
using System;
using System.Collections.Generic;
using System.IO;
using System.Xml.Linq;

namespace MenuParsing
{
    internal static class MenuParser
    {
        public static Menu ParseFromFile(string path)
        {
            if (string.IsNullOrWhiteSpace(path))
                throw new ArgumentException("path cannot be null or empty");

            if (!File.Exists(path))
                throw new FileNotFoundException($"Path: {path} does not exist.");

            return ParseFromXml(File.ReadAllText(path));
        }

        public static Menu ParseFromXml(string contents)
        {
            if (string.IsNullOrWhiteSpace(contents))
                throw new ArgumentException("contents cannot be null or empty");

            return new Menu(ParseFromElement(XElement.Parse(contents)));
        }

        private static IEnumerable<MenuItem> ParseFromElement(XElement node)
        {
            var results = new List<MenuItem>();

            foreach (var menuItemElement in node.Elements("item"))
            {
                var displayNameElement = menuItemElement.Element("displayName");
                var pathElement = menuItemElement.Element("path");
                var subMenuElement = menuItemElement.Element("subMenu");

                var displayName = displayNameElement?.Value;
                var path = pathElement?.Value;
                IEnumerable<MenuItem> subMenu = null;

                if (subMenuElement != null)
                    subMenu = ParseFromElement(subMenuElement);

                // For this example, I'll let the MenuItem constructor do validation assuming that if we received in invalid XML
                // document it's better not parse it then to parse and display something that is potentially invalid/insecure.
                // Another option would be to not allow invalid entries in the result set
                results.Add(new MenuItem(
                    displayNameElement?.Value, 
                    pathElement?.Attribute("value")?.Value, 
                    subMenu));
            }

            return results;
        }
    }
}
