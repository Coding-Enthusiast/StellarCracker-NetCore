// StellarCracker
// Copyright (c) 2018 Coding Enthusiast
// Distributed under the MIT software license, see the accompanying
// file LICENCE or http://www.opensource.org/licenses/mit-license.php.

using System;

namespace StellarCracker_NetCore
{
    /// <summary>
    /// From SharpCryptoLibrary by Coding Enthusiast
    /// </summary>
    public static class ByteArrayExtension
    {
        public static char[] ShallowReplace(this char[] sourceArray, int index1, char c1, int index2, char c2)
        {
            char[] result = new char[sourceArray.Length];
            Array.Copy(sourceArray, result, sourceArray.Length);

            result[index1] = c1;
            result[index2] = c2;

            return result;
        }

        public static char[] ShallowReplace(this char[] sourceArray, int index, char c)
        {
            char[] result = new char[sourceArray.Length];
            Array.Copy(sourceArray, result, sourceArray.Length);

            result[index] = c;

            return result;
        }

        /// <summary>
        /// Creates a sub array from the given array starting from a given index and taking the specified number of items.
        /// </summary>
        /// <exception cref="ArgumentNullException"/>
        /// <exception cref="ArgumentOutOfRangeException"/>
        /// <typeparam name="T">Type of the objects in the given array.</typeparam>
        /// <param name="sourceArray">The array containing the data to copy.</param>
        /// <param name="index">Starting index in <paramref name="sourceArray"/>.</param>
        /// <param name="count">Number of elements to copy.</param>
        /// <returns>Sub array result.</returns>
        public static byte[] SubArray(this byte[] sourceArray, int index, int count)
        {
            if (sourceArray == null)
            {
                throw new ArgumentNullException(nameof(sourceArray));
            }
            if (index < 0 || index > sourceArray.Length - 1)
            {
                throw new ArgumentOutOfRangeException(nameof(index), $"Index can not be negative or smaller than {sourceArray.Length}");
            }

            if (count > sourceArray.Length)
            {
                throw new ArgumentOutOfRangeException(nameof(count), "Source array is not long enough.");
            }

            byte[] result = new byte[count];
            for (int i = 0; i < count; i++)
            {
                result[i] = sourceArray[index++];
            }

            return result;
        }


        /// <summary>
        /// Returns an specified number of elements from the end of the array.
        /// </summary>
        /// <exception cref="ArgumentNullException"/>
        /// <exception cref="ArgumentOutOfRangeException"/>
        /// <typeparam name="T">Type of the objects in the given array.</typeparam>
        /// <param name="sourceArray">The array containing the data to copy.</param>
        /// <param name="count">Number of elements to copy.</param>
        /// <returns>Sub array result.</returns>
        public static byte[] SubArrayFromEnd(this byte[] sourceArray, int count)
        {
            if (sourceArray == null)
            {
                throw new ArgumentNullException(nameof(sourceArray));
            }
            if (count > sourceArray.Length)
            {
                throw new ArgumentOutOfRangeException(nameof(count), "Source array is not long enough.");
            }

            return sourceArray.SubArray(sourceArray.Length - count, count);
        }


        /// <summary>
        /// Converts a given 16-bit unsigned integer to an array of bytes.
        /// </summary>
        /// <param name="i">a 16-bit unsigned integer</param>
        /// <param name="bigEndian">Endianness of returned byte array.</param>
        /// <returns>An array of bytes.</returns>
        public static byte[] ToByteArray(this ushort i, bool bigEndian)
        {
            return GetBytes(i, sizeof(ushort), bigEndian);
        }

        private static byte[] GetBytes(ulong val, int size, bool bigEndian)
        {
            byte[] ba = new byte[size];
            if (bigEndian)
            {
                for (int i = 0, j = size - 1; i < size; i++, j--)
                {
                    ba[i] = (byte)(val >> (8 * j));
                }
            }
            else
            {
                for (int i = 0; i < size; i++)
                {
                    ba[i] = (byte)(val >> (8 * i));
                }
            }

            return ba;
        }

    }
}
