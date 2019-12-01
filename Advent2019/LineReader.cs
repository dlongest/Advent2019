using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Advent2019
{
    public static class LineReader
    {
        public static string BaseDirectory = @"..\..\..\Data\";

        public static IEnumerable<string> Read(string filename)
        {
            return Read(filename, line => line);
        }

        public static IEnumerable<T> Read<T>(string filename, Func<string, T> lineTransform)
        {
            var absoluteFilePath = GetAbsoluteFilePath(filename);

            using (var reader = new StreamReader(absoluteFilePath))
            {
                while (!reader.EndOfStream)
                {
                    var line = lineTransform(reader.ReadLine());

                    yield return line;
                }
            }
        }

        private static string GetAbsoluteFilePath(string filename)
        {
            return Path.Combine(BaseDirectory, filename);
        }

    }
}
