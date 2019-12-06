using Advent2019.Day06;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xunit;

namespace Advent2019.Tests
{
    public class Day06Tests
    {
        [Fact]
        public void COM_CheckSum_WithNoSatellites_Equals_0()
        {
            var sut = new OrbitMap();

            Assert.Equal(0, sut.Checksum("COM"));
        }

        [Fact]
        public void COM_CheckSum_WithSatellite_Equals_0()
        {
            var sut = new OrbitMap();
            sut.AddOrbit("COM", "AAA");

            Assert.Equal(0, sut.Checksum("COM"));
        }

        [Fact]
        public void Direct_Satellite_To_COM_Equals_1()
        {
            var sut = new OrbitMap();
            sut.AddOrbit("COM", "AAA");

            Assert.Equal(1, sut.Checksum("AAA"));
        }

        [Fact]
        public void One_LevelIndirect_To_COM_Equals_2()
        {
            var sut = new OrbitMap();
            sut.AddOrbit("COM", "AAA");
            sut.AddOrbit("AAA", "BBB");

            Assert.Equal(2, sut.Checksum("BBB"));
        }

        [Fact]
        public void Two_Level_Indirect_To_COM_Equals_3()
        {
            var sut = new OrbitMap();
            sut.AddOrbit("COM", "AAA");
            sut.AddOrbit("AAA", "BBB");
            sut.AddOrbit("BBB", "CCC");

            Assert.Equal(3, sut.Checksum("CCC"));
        }

        [Fact]
        public void Order_Of_Adding_Orbits_Doesnt_Affect_Checksum()
        {
            var sut = new OrbitMap();
            sut.AddOrbit("BBB", "CCC");
            sut.AddOrbit("AAA", "BBB");
            sut.AddOrbit("COM", "AAA");

            Assert.Equal(3, sut.Checksum("CCC"));
        }

        [Fact]
        public void Checksum_ComputesOrbitChecksum_ForAllOrbits()
        {
            var sut = new OrbitMap().AddOrbit("COM", "B")
                                          .AddOrbit("B", "C")
                                          .AddOrbit("C", "D")
                                          .AddOrbit("D", "E")
                                          .AddOrbit("E", "F")
                                          .AddOrbit("B", "G")
                                          .AddOrbit("G", "H")
                                          .AddOrbit("D", "I")
                                          .AddOrbit("E", "J")
                                          .AddOrbit("J", "K")
                                          .AddOrbit("K", "L");

            Assert.Equal(42, sut.Checksum());
        }

        [Fact]
        public void Orbits_ReturnsOrbitedBodies_InOrder()
        {
            var sut = new OrbitMap().AddOrbit("COM", "B")
                                         .AddOrbit("B", "C")
                                         .AddOrbit("C", "D")
                                         .AddOrbit("D", "E")
                                         .AddOrbit("E", "F")
                                         .AddOrbit("B", "G")
                                         .AddOrbit("G", "H")
                                         .AddOrbit("D", "I")
                                         .AddOrbit("E", "J")
                                         .AddOrbit("J", "K")
                                         .AddOrbit("K", "L");

            Assert.Equal(Enumerable.Empty<string>(), sut.Orbits("COM"));
            Assert.Equal("B,COM".AsArray(), sut.Orbits("C"));
            Assert.Equal("K,J,E,D,C,B,COM".AsArray(), sut.Orbits("L"));
        }

        [Theory]
        [InlineData("G", "C", "B")]        
        [InlineData("H", "C", "B")]
        [InlineData("YOU", "SAN", "D")]
        public void NearestCommon_ReturnsClosest_InCommon_Body(string satellite1, string satellite2, string expected)
        {
            var sut = new OrbitMap().AddOrbit("COM", "B")
                                        .AddOrbit("B", "C")
                                        .AddOrbit("C", "D")
                                        .AddOrbit("D", "E")
                                        .AddOrbit("E", "F")
                                        .AddOrbit("B", "G")
                                        .AddOrbit("G", "H")
                                        .AddOrbit("D", "I")
                                        .AddOrbit("E", "J")
                                        .AddOrbit("J", "K")
                                        .AddOrbit("K", "L")
                                        .AddOrbit("K", "YOU")
                                        .AddOrbit("I", "SAN");

            Assert.Equal(expected, sut.NearestCommonBody(satellite1, satellite2));
            Assert.Equal(expected, sut.NearestCommonBody(satellite2, satellite1));
        }

        [Fact]
        public void Transfers_ReturnsTransfersNeededToMoveBetweenOrbits()
        {
            var sut = new OrbitMap().AddOrbit("COM", "B")
                                       .AddOrbit("B", "C")
                                       .AddOrbit("C", "D")
                                       .AddOrbit("D", "E")
                                       .AddOrbit("E", "F")
                                       .AddOrbit("B", "G")
                                       .AddOrbit("G", "H")
                                       .AddOrbit("D", "I")
                                       .AddOrbit("E", "J")
                                       .AddOrbit("J", "K")
                                       .AddOrbit("K", "L")
                                       .AddOrbit("K", "YOU")
                                       .AddOrbit("I", "SAN");

            Assert.Equal(4, sut.Transfers("YOU", "SAN"));
        }
    }


    public static class CollectionExtensions
    {
        public static string[] AsArray(this string str, string delimiter = ",")
        {
            return str.Split(new string[] { delimiter }, StringSplitOptions.RemoveEmptyEntries);
        }
    }

}
