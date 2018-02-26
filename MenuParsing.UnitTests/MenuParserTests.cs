using FluentAssertions;
using System;
using System.IO;
using Xunit;

namespace MenuParsing.UnitTests
{
    public class MenuParserTests
    {
        [Fact]
        public void MenuParser_ParseFromFile_ThrowsOnNullOrEmpty()
        {
            Action parseAction = () => MenuParser.ParseFromFile(null);

            parseAction.ShouldThrow<ArgumentException>("because we don't accept null for the path");

            parseAction = () => MenuParser.ParseFromFile(string.Empty);

            parseAction.ShouldThrow<ArgumentException>("because we don't accept an empty string for the path");
        }

        [Fact]
        public void MenuParser_ParseFromFile_ThrowsOnFileNotFound()
        {
            Action parseAction = () => MenuParser.ParseFromFile(@"C:\thisdirectorydoesntexist\filenotfound.abc");

            parseAction.ShouldThrow<FileNotFoundException>();
        }

        [Fact]
        public void MenuParser_ParseFromXml_HandlesEmptyContent()
        {
            Action parseAction = () => MenuParser.ParseFromXml(null);

            parseAction.ShouldThrow<ArgumentException>();
        }

        [Fact]
        public void MenuParser_ParseFromXml_HandlesTopLevelElement()
        {
            var menu = MenuParser.ParseFromXml("<menu></menu>");

            menu.Should().NotBeNull();
            menu.Items.Should().HaveCount(0, "becuase there are no child elements in the xml");
        }
    }
}
