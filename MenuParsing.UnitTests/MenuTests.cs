using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xunit;
using FluentAssertions;

namespace MenuParsing.UnitTests
{
    public class MenuTests
    {
        [Theory, MemberData(nameof(SetActiveData))]
        public void Menu_SetActive_ProperlySetsValue(string xml, string path, bool expectedResult, string reason)
        {
            var menu = MenuParser.ParseFromXml(xml);

            menu.Should().NotBeNull();

            menu.SetActive(path);

            // The call would have been successful if at least one of the top level elements is set to true
            bool result = false;

            foreach (var mi in menu.Items)
                result |= mi.IsActive;

            result.ShouldBeEquivalentTo(expectedResult, reason);
        }

        [Theory, MemberData(nameof(SetActiveFileData))]
        public void Menu_FromTestFile_ProperlySetsValue(string pathToFile, string path, bool expectedResult, string reason)
        {
            var menu = MenuParser.ParseFromFile(pathToFile);

            menu.Should().NotBeNull();

            menu.SetActive(path);

            // The call would have been successful if at least one of the top level elements is set to true
            bool result = false;

            foreach (var mi in menu.Items)
                result |= mi.IsActive;

            result.ShouldBeEquivalentTo(expectedResult, reason);
        }

        public static IEnumerable<object[]> SetActiveData
        {
            get
            {
                return new[]
                {
                    new object[] { "<menu></menu>", "/foo", false, "becuase no children were provided" },
                    new object[] { "<menu><item><displayName>Home</displayName><path value=\"/bar\"/></item></menu>", "/foo", false, "because there is no match" },
                    new object[] { "<menu><item><displayName>Home</displayName><path value=\"/bar\"/></item></menu>", "/bar", true, "because there should be a match" }
                };
            }
        }

        public static IEnumerable<object[]> SetActiveFileData
        {
            get
            {
                return new[]
                {
                    new object[] { "./TestFiles/SchedAeroMenu.txt", "/Requests/Trips/ScheduledTrips.aspx", true, "because this should match." },
                    new object[] { "./TestFiles/SchedAeroMenu.txt", "/Trips/ScheduledTrips.aspx", false, "because this is an invalid path." },
                };
            }
        }
    }
}
