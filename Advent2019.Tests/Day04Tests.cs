using Advent2019.Day04;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xunit;

namespace Advent2019.Tests
{
    public class Day04Tests
    {
        [Theory]
        [InlineData("111111", true)]
        [InlineData("123456", true)]
        [InlineData("223450", true)]
        [InlineData("a32351", false)]
        [InlineData("22345", false)]
        [InlineData("2234599", false)]
        public void LengthPasswordValidator(string password, bool expected)
        {
            var actual = new LengthPasswordValidator().Validate(password);

            Assert.Equal(expected, actual);
        }

        [Theory]
        [InlineData("111111", true)]
        [InlineData("123456", true)]
        [InlineData("223450", false)]
        [InlineData("a32351", false)]
        [InlineData("22345", true)]
        [InlineData("2234599", true)]
        [InlineData("888887", false)]
        [InlineData("0", true)]
        [InlineData("01", true)]
        [InlineData("89", true)]
        [InlineData("1235367", false)]
        public void IncreasingSequencePasswordValidator(string password, bool expected)
        {
            var actual = new IncreasingSequencePasswordValidator().Validate(password);

            Assert.Equal(expected, actual);
        }

        [Theory]
        [InlineData("111111", true)]
        [InlineData("123456", false)]
        [InlineData("223450", true)]
        [InlineData("a32351", false)]
        [InlineData("22345", true)]
        [InlineData("2234599", true)]
        [InlineData("888887", true)]
        [InlineData("0", false)]
        [InlineData("01", false)]
        [InlineData("89", false)]
        [InlineData("1235367", false)]
        public void RepeatingGroupPasswordValidatorTest(string password, bool expected)
        {
            var actual = new RepeatingGroupPasswordValidator().Validate(password);

            Assert.Equal(expected, actual);
        }

        [Theory]
        [InlineData("112233", true)]
        [InlineData("111122", true)]
        [InlineData("123444", false)]
        public void IsolatedRepeatingGroupPasswordValidatorTest(string password, bool expected)
        {
            var actual = new IsolatedRepeatingGroupPasswordValidator().Validate(password);

            Assert.Equal(expected, actual);
        }

        [Theory]
        [InlineData("111111", true)]
        [InlineData("abcdef", true)]
        public void CompositePasswordValidator_ReturnsTrue_WhenNoInnerValidators(string password, bool expected)
        {
            var actual = new CompositePasswordValidator().Validate(password);

            Assert.Equal(expected, actual);
        }

        [Theory]
        [InlineData("111111", true)]
        [InlineData("111123", true)]
        [InlineData("122345", true)]
        [InlineData("135679", false)]
        [InlineData("123456", false)]
        [InlineData("223450", false)]
        [InlineData("a32351", false)]
        [InlineData("22345", false)]
        [InlineData("2234599", false)]
        [InlineData("888887", false)]
        [InlineData("0", false)]
        [InlineData("01", false)]
        [InlineData("89", false)]
        [InlineData("1235367", false)]
        public void CompositePasswordValidator_CompleteRuleSetTestPartA(string password, bool expected)
        {
            var sut = new CompositePasswordValidator(new LengthPasswordValidator(),
                                                        new IncreasingSequencePasswordValidator(),
                                                        new RepeatingGroupPasswordValidator());
            
            var actual = sut.Validate(password);

            Assert.Equal(expected, actual);
        }

        [Theory]
        [InlineData("111111", false)]
        [InlineData("111123", false)]
        [InlineData("122345", true)]
        [InlineData("135679", false)]
        [InlineData("123456", false)]
        [InlineData("223450", false)]
        [InlineData("a32351", false)]
        [InlineData("22345", false)]
        [InlineData("2234599", false)]
        [InlineData("888887", false)]
        [InlineData("0", false)]
        [InlineData("01", false)]
        [InlineData("89", false)]
        [InlineData("1235367", false)]
        [InlineData("112233", true)]
        [InlineData("111122", true)]
        [InlineData("123444", false)]
        public void CompositePasswordValidator_CompleteRuleSetTestPartB(string password, bool expected)
        {
            var sut = new CompositePasswordValidator(new LengthPasswordValidator(),
                                                        new IncreasingSequencePasswordValidator(),
                                                        new IsolatedRepeatingGroupPasswordValidator());

            var actual = sut.Validate(password);

            Assert.Equal(expected, actual);
        }
    }
}
