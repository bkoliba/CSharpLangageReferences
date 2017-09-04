using System;
using Xunit;

namespace EqualityAndComparisonTests
{
    //Int, Floats,... Implements both IComparable<T> and the operators
    //String only Implements IComparable<T> not the operators (alphbetical ordering)
    [Trait(".NET Comparison Defaults","")]
    public class ComparisonDotNetTests
    {
        //Int32 Implements IComparable<T> and returns
        // 0 - when values are equal
        // 1 - when value is greater than
        // -1 - when value is less than
        [Fact]
        void WhenComparingAgainstGreaterValue_ShouldBeNegativeOne()
        {
            int value1 = 1;
            int value2 = 2;

            Assert.True(-1 == value1.CompareTo(value2));
            Assert.True(value1 < value2);
        }

        [Fact]
        void WhenComparingAgainstLesserValue_ShouldBeOne()
        {
            int value1 = 1;
            int value2 = 2;

            Assert.True(1 == value2.CompareTo(value1));
            Assert.True(value2 > value1);
        }

        [Fact]
        void WhenComparingAgainstSameValue_ShouldBeZero()
        {
            int value1 = 2;
            int value2 = 2;

            Assert.True(0 == value1.CompareTo(value2));
            Assert.True(value1 == value2);
        }

        //String implements IComparable<T> and Returns
        // 0 - when strings are the same
        // 1 - when string comes after compared string in alphabetical order (grape.CompareTo(apple))
        // -1 - when string comes before compared string in alphabetical order (apple.CompareTo(grape))
        [Fact]
        void WhenComparingAgainstLowerAlphaString_ShouldBeNegativeOne()
        {
            string apple = "Apple";
            string grape = "Grape";

            Assert.True(-1 == apple.CompareTo(grape));
        }

        [Fact]
        void WhenComparingAgainstHigherAlphaString_ShouldBeOne()
        {
            string apple = "Apple";
            string grape = "Grape";

            Assert.True(1 == grape.CompareTo(apple));
        }

        [Fact]
        void WhenComparingAgainstSameString_ShouldBeZero()
        {
            string apple = "Apple";

            Assert.True(0 == apple.CompareTo(apple));
        }

        //Generally you would avoid implementing IComparable<T> unless there is really only one way to sort the Type.
        //Note: for full expected support you would need to also implement IEquatable<T>, override object.Equals, overide GetHashCode(), == operator, and IComparable
        private struct CurrentAge : IComparable<CurrentAge>
        {
            public int Age { get; }
            public CurrentAge(int age)
            {
                Age = age;
            }
            public int CompareTo(CurrentAge other)
            {
                return Age.CompareTo(other.Age);
            }
            public static bool operator <(CurrentAge firstAge, CurrentAge secondAge)
            {
                return firstAge.Age < secondAge.Age;
            }
            public static bool operator >(CurrentAge firstAge, CurrentAge secondAge)
            {
                return firstAge.Age > secondAge.Age;
            }
            public static bool operator <=(CurrentAge firstAge, CurrentAge secondAge)
            {
                return firstAge.Age <= secondAge.Age;
            }
            public static bool operator >=(CurrentAge firstAge, CurrentAge secondAge)
            {
                return firstAge.Age >= secondAge.Age;
            }

            public override string ToString()
            {
                return $"{Age} years old";
            }
        }

        [Fact]
        void WhenComparingCurrentAgeWithGreaterAge_ShouldBeNegativeOne()
        {
            var value1 = new CurrentAge(1);
            var value2 = new CurrentAge(2);

            Assert.True(-1 == value1.CompareTo(value2));
            Assert.True(value1 < value2);
        }

        [Fact]
        void WhenComparingCurrentAgeWithLesserAge_ShouldBeOne()
        {
            var value1 = new CurrentAge(1);
            var value2 = new CurrentAge(2);

            Assert.True(1 == value2.CompareTo(value1));
            Assert.True(value2 > value1);
        }

        [Fact]
        void WhenComparingCurrentAgeWithSameAge_ShouldBeZero()
        {
            var value1 = new CurrentAge(1);
            var value2 = value1;

            Assert.True(0 == value1.CompareTo(value2));
        }
    }
}
