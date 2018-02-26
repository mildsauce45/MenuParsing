using System;
using System.Collections.Generic;

namespace MenuParsing.Data
{
    internal class MenuItem
    {
        public string DisplayName { get; }
        public string Path { get; }
        public IEnumerable<MenuItem> SubMenu { get; }
        public bool IsActive { get; internal set; }

        public MenuItem(string displayName, string path, IEnumerable<MenuItem> subMenu)
        {
            if (string.IsNullOrWhiteSpace(displayName))
                throw new ArgumentException("displayName must be provided");

            if (string.IsNullOrWhiteSpace(path))
                throw new ArgumentException("path must be provided");

            DisplayName = displayName;
            Path = path;
            SubMenu = subMenu;
        }

        public override string ToString() => $"{DisplayName}, {Path} {(IsActive ? "ACTIVE" : string.Empty)}".Trim();
    }
}
