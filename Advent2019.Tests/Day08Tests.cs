using Advent2019.Day08;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xunit;

namespace Advent2019.Tests
{
    public class Day08Tests
    {
        [Fact]
        public void Image_Created_Correctly()
        {
            var sut = new Image("123456789012", 3, 2);

            Assert.Equal(2, sut.Layers.Length);
            Assert.Equal(new int[] { 1, 2, 3 }, sut.Layers[0].Rows[0]);
            Assert.Equal(new int[] { 4, 5, 6 }, sut.Layers[0].Rows[1]);
            Assert.Equal(new int[] { 7, 8, 9 }, sut.Layers[1].Rows[0]);
            Assert.Equal(new int[] { 0, 1, 2 }, sut.Layers[1].Rows[1]);
        }

        [Fact]
        public void Image_ComputesChecksum_ForCorrectLayer()
        {
            var sut = new Image("121252789012", 3, 2);

            Assert.Equal(6, sut.Checksum());
        }

        [Fact]
        public void Image_Decode_ReturnsVisiblePixel_PerPosition()
        {
            var expected = "0110";

            var sut = new Image("0222112222120000", 2, 2);

            Assert.Equal(expected, sut.Decode());
        }

        [Fact]
        public void Image_RawDecode_ReturnsNewVisibleLayer()
        {
            var expected = new Layer(new int[] { 0, 1, 1, 0 }, 2, 2);

            var sut = new Image("0222112222120000", 2, 2).RawDecode();

            Assert.Equal(expected.Rows[0], sut.Rows[0]);
        }

        [Fact]
        public void Layer_Created_Correctly()
        {
            var expected = new[] { new int[] { 1, 2, 3 }, new int[] { 4, 5, 6 } };           

            var sut = new Layer(new int[] { 1, 2, 3, 4, 5, 6 }, 3, 2);
        
            Assert.Equal(2, sut.Rows.Length);
            Assert.Equal(expected[0], sut.Rows[0]);
            Assert.Equal(expected[1], sut.Rows[1]);
        }

        [Fact]
        public void Layer_Equals_IsCorrect()
        {

            Assert.Equal(new Layer(new[] { 1, 2, 3, 4 }, 2, 2), new Layer(new[] { 1, 2, 3, 4 }, 2, 2));
            Assert.NotEqual(new Layer(new[] { 1, 2, 3, 2 }, 2, 2), new Layer(new[] { 1, 2, 3, 4 }, 2, 2));
            Assert.NotEqual(new Layer(new[] { 1, 2, 3, 3, 3, 4 }, 3, 2), new Layer(new[] { 1, 2, 3, 4 }, 2, 2));
        }

    
        [Fact]
        public void Layer_Computes_PixelCount_Correctly()
        {
            var sut = new Image("123456789012", 3, 2);

            Assert.Equal(0, sut.Layers[0].PixelCount(0));
            Assert.Equal(1, sut.Layers[1].PixelCount(0));
        }

        [Fact]
        public void Layer_Computes_Checksum_Correctly()
        {
            var sut = new Layer(new int[] { 2, 2, 2, 2, 1, 1, 1, 1 }, 4, 2);

            Assert.Equal(16, sut.Checksum());
        }
    }
}

