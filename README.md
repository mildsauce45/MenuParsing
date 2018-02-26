# MenuParsing
Generating a menu data structure from an xml file

## The Problem
Parse an XML structure containing the menu information. Using any framework classes you choose, write a C# console application that does all of the following:
 1. Accepts two arguments: first a path to a menu file ("C:\schedaeromenu.xml"). second an active path to match ("/default.aspx")
 2. Parse the xml document, ignoring any content not required for this application
 3. Identify currently active menu items - a menu item is active if it or one of it's children has a path matching the second argument
 4. Write the parsed menu to the console
    * Show the display name and path structure for each menu item
    * Indent submenu items
    * Print the word "ACTIVE" next to any active menu items
