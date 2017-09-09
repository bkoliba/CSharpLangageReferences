using System;
using System.Collections.Generic;
using System.Text;
using Xunit;

namespace FormattingTypesTests
{
    [Trait("String Formatting Methods","")]
    public class StringFormattingMethodsTests
    {
        [Fact]
        void AddingPaddingToString()
        {
            Assert.Equal("--Test","Test".PadLeft(6,'-'));
            Assert.Equal("Test--", "Test".PadRight(6, '-'));
        }

    }
}
