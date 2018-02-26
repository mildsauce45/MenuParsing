using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace MenuParsing.Data
{
    internal class Menu
    {
        private IEnumerable<MenuItem> items;

        public Menu(IEnumerable<MenuItem> items)
        {
            this.items = items;
        }

        public void SetActive(string path)
        {
            if (items.IsNullOrEmpty())
                return;

            foreach (var menuItem in items)
                SetActiveInternal(menuItem, path);
        }

        private bool SetActiveInternal(MenuItem menuItem, string path)
        {
            // Does this menu item have children
            var recurseChildren = !menuItem.SubMenu.IsNullOrEmpty();

            // A menu item is active if:
            // 1) It's own path is equal to the given path
            // 2) One of it's children is
            if (string.Equals(menuItem.Path, path) || (recurseChildren && menuItem.SubMenu.Any(mi => SetActiveInternal(mi, path))))
                menuItem.IsActive = true;

            return menuItem.IsActive;
        }

        public override string ToString()
        {
            var sb = new StringBuilder();

            foreach (var menuItem in items)
                sb.Append(ToStringInternal(menuItem, 0));

            return sb.ToString();
        }

        private string ToStringInternal(MenuItem mi, int tabs)
        {
            var sb = new StringBuilder();

            for (int i = 0; i < tabs; i++)
                sb.Append("\t");

            sb.AppendLine(mi.ToString());

            if (!mi.SubMenu.IsNullOrEmpty())
            {
                foreach (var child in mi.SubMenu)
                    sb.Append(ToStringInternal(child, tabs + 1));
            }

            return sb.ToString();
        }
    }
}
