using System;
using Xunit;

//References:https://docs.microsoft.com/en-us/dotnet/standard/base-types/standard-numeric-format-strings
namespace FormattingTypesTests
{
    [Trait("Numeric String Formatting","")]
    public class NumericFormatStringTests
    {
        [Fact]
        public void ConvertingIntToHex()
        {
            Assert.Equal("FF", 255.ToString("X")); //1111 1111
            Assert.Equal("ff", 255.ToString("x")); //1111 1111

            Assert.Equal("12c", 300.ToString("x")); // 1 0010 1100
        }

    }
}
