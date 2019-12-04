using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Advent2019.Day04
{
    public interface IPasswordValidator
    {
        bool Validate(string password);
    }

    public class LengthPasswordValidator : NumericSequencePasswordValidator
    {
        protected override bool ValidateSequence(int[] digits)
        {
            return digits.Length == 6;
        }
    }

    public abstract class NumericSequencePasswordValidator : IPasswordValidator
    {
        private const int ascii0 = 48;
        private const int ascii9 = 57;
        private readonly System.Text.RegularExpressions.Regex pattern = new System.Text.RegularExpressions.Regex("^[0-9]+$");

        public bool Validate(string password)
        {
            if (!AllNumbers(password))
            {
                return false;
            }

            var digits = AsDigits(password);

            return ValidateSequence(digits);
        }

        private bool AllNumbers(string password)
        {
            return this.pattern.IsMatch(password);
        }

        private int[] AsDigits(string password)
        {
            return password.Select(a => a - '0').ToArray();
        }

        protected abstract bool ValidateSequence(int[] digits);
    }

    public class IncreasingSequencePasswordValidator : NumericSequencePasswordValidator
    {
        protected override bool ValidateSequence(int[] digits)
        {
            return Enumerable.Range(0, digits.Length - 1)
                             .Select(index => digits[index] - digits[index + 1])
                             .All(difference => difference <= 0);
        }
    }

    public class RepeatingGroupPasswordValidator : NumericSequencePasswordValidator
    {
        protected override bool ValidateSequence(int[] digits)
        {
            return Enumerable.Range(0, digits.Length - 1)
                             .Any(index => digits[index] == digits[index + 1]);
        }
    }

    public class IsolatedRepeatingGroupPasswordValidator : NumericSequencePasswordValidator
    {
        protected override bool ValidateSequence(int[] digits)
        {
            var groups = Group.Create(digits);

            return groups.Any(g => g.Length == 2);
        }

      
        public class Group
        {
            public static IEnumerable<Group> Create(int[] digits)
            {
                var groups = new List<Group>();
                var index = 0;

                while (index < digits.Length)
                {
                    var group = Group.Create(digits, index);

                    index = group.End + 1;

                    groups.Add(group);
                }

                return groups;
            }

            public static Group Create(int[] digits, int startIndex)
            {
                var index = startIndex;

                while (index < digits.Length && digits[index] == digits[startIndex])
                {
                    index++;
                }

                return (index < digits.Length) ? new Group(startIndex, index - 1) : new Group(startIndex, index - 1);
            }

            public Group(int start, int end)
            {
                this.Start = start;
                this.End = end;
            }

            public int Start { get; set; }

            public int End { get; set; }

            public int Length { get => this.End - this.Start + 1; }
        }
    }

    public class CompositePasswordValidator : IPasswordValidator
    {
        private IPasswordValidator[] validators;

        public CompositePasswordValidator(params IPasswordValidator[] validators)
        {
            this.validators = validators ?? new IPasswordValidator[0];
        }

        public bool Validate(string password)
        {
            return this.validators.All(v => v.Validate(password));
        }
    }
}
