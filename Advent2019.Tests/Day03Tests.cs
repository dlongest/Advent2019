using Advent2019.Day03;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xunit;

namespace Advent2019.Tests
{
    public class Day03Tests
    {
        [Fact]
        public void Wire_GeneratesCurrentCoordinates_Up()
        {
            var expected = new[] { XY.Origin.Up(), XY.Origin.Up().Up() };

            var sut = new Wire().Move("U2");

            Assert.Equal(expected, sut.XYs);
        }

        [Fact]
        public void Wire_GeneratesCurrentCoordinates_Down()
        {
            var expected = new[] { XY.Origin.Down(), XY.Origin.Down().Down(), XY.Origin.Down().Down().Down() };

            var sut = new Wire().Move("D3");

            Assert.Equal(expected, sut.XYs);
        }

        [Fact]
        public void Wire_GeneratesCurrentCoordinates_Right()
        {
            var expected = new[] { XY.Origin.Right(), XY.Origin.Right().Right(), XY.Origin.Right().Right().Right() };

            var sut = new Wire().Move("R3");

            Assert.Equal(expected, sut.XYs);
        }

        [Fact]
        public void Wire_GeneratesCurrentCoordinates_Left()
        {
            var expected = new[] { XY.Origin.Left(), XY.Origin.Left().Left(), XY.Origin.Left().Left().Left() };

            var sut = new Wire().Move("L3");

            Assert.Equal(expected, sut.XYs);
        }

        [Fact]
        public void Wire_TracksMovesCorrectly()
        {
            var expected = new[] { new XY(1, 0), new XY(2, 0), new XY(3, 0), new XY(4, 0), new XY(5, 0), new XY(6, 0), new XY(7, 0), new XY(8,0),
                new XY(8, 1), new XY(8, 2), new XY(8, 3), new XY(8, 4), new XY(8, 5),
                new XY(7, 5), new XY(6, 5), new XY(5, 5), new XY(4, 5), new XY(3, 5),
                new XY(3, 4), new XY(3, 3), new XY(3, 2) };

            var sut = new Wire().Move("R8").Move("U5").Move("L5").Move("D3");

            Assert.Equal(expected, sut.XYs);
        }

        [Fact]
        public void Wire_IsCreatedCorrectly_FromCommaSeparatedList()
        {
            var expected = new[] { new XY(1, 0), new XY(2, 0), new XY(3, 0), new XY(4, 0), new XY(5, 0), new XY(6, 0), new XY(7, 0), new XY(8,0),
                new XY(8, 1), new XY(8, 2), new XY(8, 3), new XY(8, 4), new XY(8, 5),
                new XY(7, 5), new XY(6, 5), new XY(5, 5), new XY(4, 5), new XY(3, 5),
                new XY(3, 4), new XY(3, 3), new XY(3, 2) };

            var sut = Wire.FromCommaSeparated("R8,U5,L5,D3");

            Assert.Equal(expected, sut.XYs);
        }

        [Fact]
        public void Wire_Returns_SingleStepNumber_Correctly()
        {
            var sut = Wire.FromCommaSeparated("R8,U5,L5,D3");

            Assert.Equal(1, sut.StepNumber(new XY(1, 0)).Value);
            Assert.Equal(20, sut.StepNumber(new XY(3, 3)).Value);
            Assert.False(sut.StepNumber(XY.Origin).HasValue);
        }

        [Fact]
        public void Wire_Returns_MultipletepNumbers_Correctly()
        {
            var expected = new List<Tuple<XY, int?>>(new[] { Tuple.Create(new XY(1, 0), (int?)1), Tuple.Create(new XY(3, 3), (int?)20), Tuple.Create(XY.Origin, (int?)null) });
            var sut = Wire.FromCommaSeparated("R8,U5,L5,D3");

            var actual = sut.StepNumbers(new[] { new XY(1, 0), new XY(3, 3), XY.Origin });

            Assert.Equal(expected, actual);
        }


        [Theory]
        [InlineData("R8,U5,L5,D3", "U7,R6,D4,L4", 3, 3)]
        public void Wire_FindsClosestCrossedPath_ToOtherWire(string moves1, string moves2, int expectedX, int expectedY)
        {
            var expected = new XY(expectedX, expectedY);
            
            var sut = Wire.FromCommaSeparated(moves1);
            var other = Wire.FromCommaSeparated(moves2);

            var actual = sut.FindClosestCrossing(other);

            Assert.Equal(expected, actual);
        }

        [Theory]
        [InlineData("R8,U5,L5,D3", "U7,R6,D4,L4", 6)]
        [InlineData("R75,D30,R83,U83,L12,D49,R71,U7,L72", 
                    "U62,R66,U55,R34,D71,R55,D58,R83", 159)]
        [InlineData("R98,U47,R26,D63,R33,U87,L62,D20,R33,U53,R51",
                    "U98,R91,D20,R16,D67,R40,U7,R15,U6,R7", 135)]
        public void FindClosestCrossing_CompleteTest(string moves1, string moves2, int expected)
        {
            var sut = Wire.FromCommaSeparated(moves1);
            var other = Wire.FromCommaSeparated(moves2);

            var actual = sut.FindClosestCrossing(other).Manhattan(XY.Origin);

            Assert.Equal(expected, actual);
        }

        [Theory]
        [InlineData("R8,U5,L5,D3", "U7,R6,D4,L4", 30)]
        [InlineData("R75,D30,R83,U83,L12,D49,R71,U7,L72",
                  "U62,R66,U55,R34,D71,R55,D58,R83", 610)]
        [InlineData("R98,U47,R26,D63,R33,U87,L62,D20,R33,U53,R51",
                  "U98,R91,D20,R16,D67,R40,U7,R15,U6,R7", 410)]
        public void FindShortestSignalPath_CompleteTest(string moves1, string moves2, int expected)
        {
            var sut = Wire.FromCommaSeparated(moves1);
            var other = Wire.FromCommaSeparated(moves2);

            var actual = sut.FindShortestSignalDelay(other);

            Assert.Equal(expected, actual);
        }
    }

    public class XYTests
    {
        [Theory]
        [InlineData(0, 0, 3, 3, 6)]
        [InlineData(10, 10, 3, 3, 14)]
        [InlineData(-2, -6, 12, 14, 34)]
        public void Manhattan_ComputedCorrectly(int x1, int y1, int x2, int y2, int expected)
        {
            var actual = new XY(x1, y1).Manhattan(new XY(x2, y2));

            Assert.Equal(expected, actual);
        }
    }
}
