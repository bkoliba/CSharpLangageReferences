using Xunit;

namespace FormattingTypesTests
{
    [Trait("String Formatting", "String Formatting Methods")]
    public class StringFormattingMethodsTests
    {
        //Adds padding to string with char
        [Fact]
        void AddingPaddingToString()
        {
            Assert.Equal("--Test","Test".PadLeft(6,'-'));
            Assert.Equal("Test--", "Test".PadRight(6, '-'));
        }

    }
}
