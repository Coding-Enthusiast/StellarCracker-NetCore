// StellarCracker Tests
// Copyright (c) 2018 Coding Enthusiast
// Distributed under the MIT software license, see the accompanying
// file LICENCE or http://www.opensource.org/licenses/mit-license.php.

using StellarCracker_NetCore;
using System.Collections.Generic;
using Xunit;

namespace Tests
{
    public class WorkerTests
    {
        private const int Max = 7;
        public static IEnumerable<object[]> GetMoveNextIndexCases()
        {
            yield return new object[]
            {
                new int[1] { 1 },
                new int[][]
                {
                    new int[1]{ 02 }, new int[1]{ 03 }, new int[1]{ 04 }, new int[1]{ 05 }, new int[1]{ 06 }
                }
            };
            yield return new object[]
            {
                new int[2] { 1, 2 },
                new int[][]
                {
                    new int[2]{1,3}, new int[2]{1,4}, new int[2]{1,5}, new int[2]{1,6},
                    new int[2]{2,3}, new int[2]{2,4}, new int[2]{2,5}, new int[2]{2,6},
                    new int[2]{3,4}, new int[2]{3,5}, new int[2]{3,6},
                    new int[2]{4,5}, new int[2]{4,6},
                    new int[2]{5,6}
                }
            };
            yield return new object[]
            {
                new int[3] { 1, 2, 3 },
                new int[][]
                {
                    new int[3]{1,2,4}, new int[3]{1,2,5}, new int[3]{1,2,6},
                    new int[3]{1,3,4}, new int[3]{1,3,5}, new int[3]{1,3,6},
                    new int[3]{1,4,5}, new int[3]{1,4,6},
                    new int[3]{1,5,6},
                    new int[3]{2,3,4}, new int[3]{2,3,5}, new int[3]{2,3,6},
                    new int[3]{2,4,5}, new int[3]{2,4,6},
                    new int[3]{2,5,6},
                    new int[3]{3,4,5}, new int[3]{3,4,6},
                    new int[3]{3,5,6},
                    new int[3]{4,5,6},
                }
            };
            yield return new object[]
            {
                new int[4] { 1, 2, 3, 4 },
                new int[][]
                {
                    new int[4]{1,2,3,5}, new int[4]{1,2,3,6},
                    new int[4]{1,2,4,5}, new int[4]{1,2,4,6},
                    new int[4]{1,2,5,6},
                    new int[4]{1,3,4,5}, new int[4]{1,3,4,6},
                    new int[4]{1,3,5,6},
                    new int[4]{1,4,5,6},
                    new int[4]{2,3,4,5}, new int[4]{2,3,4,6},
                    new int[4]{2,3,5,6},
                    new int[4]{2,4,5,6},
                    new int[4]{3,4,5,6},
                }
            };
        }
        [Theory]
        [MemberData(nameof(GetMoveNextIndexCases))]
        public unsafe void MoveNextIndexTest(int[] array, int[][] expected)
        {
            fixed (int* items = &array[0])
            {
                for (int i = 0; i < expected.Length; i++)
                {
                    bool b = Worker.MoveNextIndex(items, array.Length, Max);
                    Assert.True(b);
                    Assert.Equal(expected[i], array);
                }

                bool final = Worker.MoveNextIndex(items, array.Length, Max);
                Assert.False(final);
            }
        }

        public static IEnumerable<object[]> GetMoveNextItemsCases()
        {
            yield return new object[]
            {
                new int[1] { 0 },
                new int[][]
                {
                    new int[1]{ 1 }, new int[1]{ 2 }, new int[1]{ 3 }, new int[1]{ 4 }, new int[1]{ 5 }, new int[1]{ 6 }
                }
            };
            yield return new object[]
            {
                new int[2] { 0, 0 },
                new int[][]
                {
                    new int[2]{0,1}, new int[2]{0,2}, new int[2]{0,3}, new int[2]{0,4}, new int[2]{0,5}, new int[2]{0,6},
                    new int[2]{1,0},
                    new int[2]{1,1}, new int[2]{1,2}, new int[2]{1,3}, new int[2]{1,4}, new int[2]{1,5}, new int[2]{1,6},
                    new int[2]{2,0},
                    new int[2]{2,1}, new int[2]{2,2}, new int[2]{2,3}, new int[2]{2,4}, new int[2]{2,5}, new int[2]{2,6},
                    new int[2]{3,0},
                    new int[2]{3,1}, new int[2]{3,2}, new int[2]{3,3}, new int[2]{3,4}, new int[2]{3,5}, new int[2]{3,6},
                    new int[2]{4,0},
                    new int[2]{4,1}, new int[2]{4,2}, new int[2]{4,3}, new int[2]{4,4}, new int[2]{4,5}, new int[2]{4,6},
                    new int[2]{5,0},
                    new int[2]{5,1}, new int[2]{5,2}, new int[2]{5,3}, new int[2]{5,4}, new int[2]{5,5}, new int[2]{5,6},
                    new int[2]{6,0},
                    new int[2]{6,1}, new int[2]{6,2}, new int[2]{6,3}, new int[2]{6,4}, new int[2]{6,5}, new int[2]{6,6},
                }
            };
        }
        [Theory]
        [MemberData(nameof(GetMoveNextItemsCases))]
        public unsafe void MoveNextItemsTest(int[] array, int[][] expected)
        {
            fixed (int* items = &array[0])
            {
                for (int i = 0; i < expected.Length; i++)
                {
                    Worker.MoveNextItems(items, array.Length, Max);
                    Assert.Equal(expected[i], array);
                }

                bool final = Worker.MoveNextItems(items, array.Length, Max);
                Assert.False(final);
            }
        }
    }
}
