using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Advent2019.Day08
{
    public class Image
    {
        private readonly int layerWidth;
        private readonly int layerHeight;

        public Image(string digits, int width, int height)
        {
            this.layerWidth = width;
            this.layerHeight = height;

            var layers = digits.Length / (width * height);

            var individualDigits = digits.ToCharArray().Select(c => c - '0');

            this.Layers = Enumerable.Range(0, layers)
                                    .Select(i => new Layer(individualDigits.Skip(i * width * height).Take(width * height), width, height))
                                    .ToArray();

        }

        public Layer[] Layers { get; private set; }

        public int Checksum()
        {
            var lowest0Layer = Enumerable.Range(0, this.Layers.Length)
                                          .Select(i => new { Index = i, ZeroCount = this.Layers[i].PixelCount(0) })
                                          .OrderBy(i => i.ZeroCount)
                                          .First().Index;

            return this.Layers[lowest0Layer].Checksum();
        }

        public string Decode()
        {
            var stacked = new List<int[]>();


            foreach (var row in Enumerable.Range(0, this.layerHeight))
            {
                foreach (var column in Enumerable.Range(0, this.layerWidth))
                {
                    stacked.Add(this.Layers.Select(y => y.Rows[row][column]).ToArray());
                }
            }

            var selectedPixels = string.Join(string.Empty, stacked.Select(s => VisiblePixel(s)));

            return selectedPixels;
        }

        public Layer RawDecode()
        {
            var stacked = new List<int[]>();

            foreach (var row in Enumerable.Range(0, this.layerHeight))
            {
                foreach (var column in Enumerable.Range(0, this.layerWidth))
                {
                    stacked.Add(this.Layers.Select(y => y.Rows[row][column]).ToArray());
                }
            }

            return new Layer(stacked.Select(s => VisiblePixel(s)), this.layerWidth, this.layerHeight);
        }

        private int VisiblePixel(int[] pixels)
        {
            return pixels.First(p => p == 0 || p == 1);
        }
    }


    public class Layer
    {
        public Layer(IEnumerable<int> digits, int width, int height)
        {
            if (digits.Count() != width * height)
            {
                throw new ArgumentException("Digits, width, and height don't agree on the Layer dimensions");
            }

            this.Rows = Enumerable.Range(0, height)
                                  .Select(i => digits.Skip(i * width).Take(width).ToArray())
                                  .ToArray();
        }

        public int[][] Rows { get; private set; }

        public int PixelCount(int value)
        {
            return this.Rows.Select(r => r.Count(v => v == value)).Sum();
        }
       
        public int Checksum()
        {
            return this.PixelCount(1) * this.PixelCount(2);
        }

        public int Width { get => this.Rows[0].Length; } 

        public int Height { get => this.Rows.Length; }

        public override bool Equals(object obj)
        {
            var other = obj as Layer;
            
            if (other == null)
            {
                return false;
            }

            if (other.Height != this.Height || other.Width != this.Width)
            {
                return false;
            }

            foreach (var row in Enumerable.Range(0, this.Height))
            {
                foreach (var col in Enumerable.Range(0, this.Width))
                {
                    if (this.Rows[row][col] != other.Rows[row][col])
                    {
                        return false;
                    }
                }
            }

            return true;
        }
    }
}
