using System;
using Xunit;
using Xunit.Abstractions;

namespace EqualityAndComparisonTests
{
    [Trait("Comparison", "String Comparison")]
    public class ComparisonStringTests
    {
        [Fact]
        void WhenComparingStrings()
        {
            var apple = "apple";//97,112,112,108,101
            var appleUpper = "APPLE"; //65,80,80,76,69

            //Ordinal comparison looks at each char numerical values an compares them 
            //'apple' is greater than 'APPLE' because the lowercase 'a' value is 97 and 'A' value is 65
            //Compare method will return 32 = 97 - 65
            //If first characters are equal than it compares the next value (High preformance)
            Assert.True(String.Compare(apple, appleUpper, StringComparison.Ordinal) == 32); // '==' 
            Assert.True(String.Compare(apple, appleUpper, StringComparison.OrdinalIgnoreCase) == 0);

            //When displaying to users use CurrentCulture, when doing so it applys rules from current language, such as
            //lowercase character comes first compared to its same letter uppercase.
            //Also take in account for unicode values and their equivalents
            Assert.True(String.Compare(apple, appleUpper, StringComparison.CurrentCulture) < 0);//-1 IComparable<T>.CompareTo()
            Assert.True(String.Compare(apple, appleUpper, StringComparison.CurrentCultureIgnoreCase) == 0);

            //Not recommended unless you are persisting linguistically meaningful but culturally agnostic data
            Assert.True(String.Compare(apple, appleUpper, StringComparison.InvariantCulture) < 0);//-1
            Assert.True(String.Compare(apple, appleUpper, StringComparison.InvariantCultureIgnoreCase) == 0);
        }
    }
}
