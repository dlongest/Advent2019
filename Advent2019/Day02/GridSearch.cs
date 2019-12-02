using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Advent2019.Day02
{
    public class GridSearch
    {

        public SearchResult Search(IntCode[] input, SearchRange noun, SearchRange verb, int targetValue)
        {          
            foreach (var nounValue in noun.Range())
            {
                foreach (var verbValue in verb.Range())
                {
                    var searchInput = Copy(input);

                    searchInput[noun.Position] = new IntCode(nounValue);
                    searchInput[verb.Position] = new IntCode(verbValue);

                    var result = new IntCodeProgram().Process(Copy(searchInput));

                    if (result[0].Value == targetValue)
                    {
                        return new SearchResult
                        {
                            Noun = nounValue,
                            Verb = verbValue
                        };
                    }
                }
            }

            throw new ArgumentException("Couldn't find values within the search ranges to hit the target");
        }

        private static IntCode[] Copy(IntCode[] source)
        {
            return source.Select(s => new IntCode(s.Value)).ToArray();
        }
    }

    public class SearchResult
    {
        public int Noun { get; set; }

        public int Verb { get; set; }

    }

    public class SearchRange
    {
        public static SearchRange From0To99(int position) => new SearchRange(position, 0, 99);

        public SearchRange(int position, int start, int end)
        {
            this.Position = position;
            this.Start = start;
            this.End = end;
        }

        public int Position;

        public int Start { get; set; }

        public int End { get; set; }

        public IEnumerable<int> Range()
        {
            return Enumerable.Range(Start, End - Start + 1);
        }
    }
}
