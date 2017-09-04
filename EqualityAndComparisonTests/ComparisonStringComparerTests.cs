using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Xunit;
using Xunit.Abstractions;

namespace EqualityAndComparisonTests
{
    [Trait("String Comparers","")]
    public class ComparisonStringComparerTests
    {
        private ITestOutputHelper _outputLogger;

        public ComparisonStringComparerTests(ITestOutputHelper outputLogger)
        {
            _outputLogger = outputLogger;
        }

        [Fact]
        void WhenUsingStringComparer_InvariantCultureIgnoreCase_ShouldNotAddSameItemOfDifferentCase()
        {
            var foods = new HashSet<string>(StringComparer.InvariantCultureIgnoreCase)
            {
                "Apple","apple","pizza","cheese","pizza","Grapes"
            };

            Assert.Equal(4, foods.Count());

            //Output: Apple, pizza, cheese, Grapes
            _outputLogger.WriteLine(string.Join(", ", foods));
        }
    }
}
