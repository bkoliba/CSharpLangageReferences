using System;
using System.Collections.Generic;
using System.Text;

namespace CollectionUsageTests
{
    public class JaggedArray_Tests
    {
        /// <summary>
        /// Array of arrays, could work with any collection types not just arrays
        /// </summary>
        void ReadingJaggedArrays()
        {
            //arrays must be initialized and can be of different sizes.
            var jaggedArray = new int[][]{
                new int[] { 1,2,3 },
                new int[] {4,5,6 },
                new int[]{1,2},
                new int[]{ 1,2,3,3,4}
            };
        }
    }
}
