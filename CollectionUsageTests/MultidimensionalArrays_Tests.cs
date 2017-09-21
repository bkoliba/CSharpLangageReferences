using System;
using System.Collections.Generic;
using System.Text;
using Xunit;
using Xunit.Abstractions;

namespace CollectionUsageTests
{
    [Trait("Collection Usage","Multidemensional Arrays")]
    public class MultidimensionalArrays_Tests
    {
        private ITestOutputHelper _output;

        /// <summary>
        /// MultidimensionalArrays are used when you have data on a grid and need to access the data with multiple indices.
        /// </summary>
        public MultidimensionalArrays_Tests(ITestOutputHelper output)
        {
            _output = output;
        }

        [Fact]
        void ReadingFromMultideminsionalArrays()
        {
            var ary = new int[4, 3] { { 1, 2, 3 }, { 2, 3, 4 }, { 3, 4, 5 }, { 4, 5, 6 } };

            for (int i = 0; i < ary.GetLength(0); i++)
            {
                var sb = new StringBuilder();
                for (int j = 0; j < ary.GetLength(1); j++)
                {
                    sb.Append(ary[i, j]);
                }
                _output.WriteLine(sb.ToString());
            }
            //Output
            //123
            //234
            //345
            //456
        }
    }
}
