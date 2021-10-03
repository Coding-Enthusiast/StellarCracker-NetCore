// StellarCracker
// Copyright (c) 2018 Coding Enthusiast
// Distributed under the MIT software license, see the accompanying
// file LICENCE or http://www.opensource.org/licenses/mit-license.php.

using System;
using System.Linq;

namespace StellarCracker_NetCore
{
    public static class Base32
    {
        public const string Digits = "ABCDEFGHIJKLMNOPQRSTUVWXYZ234567";
        public const char PrvChar = 'S';
        public const char PubChar = 'G';
        public const byte PrvByte = 144;
        public const byte PubByte = 48;

        
        public static string Encode32(ReadOnlySpan<byte> prv32)
        {
            Span<char> ca = new char[prv32.Length];
            for (int i = 0; i < prv32.Length; i++)
            {
                ca[i] = Digits[prv32[i]];
            }
            return new string(ca);
        }

        public static bool TryDecode(string key, bool isPublic, out byte[] result32, out byte[] result256, out bool cs)
        {
            key = key.ToUpper();
            if (key.Length != 56 ||
                key.Any(c => !Digits.Contains(c)) ||
                (isPublic && !key.StartsWith(PubChar)) ||
                (!isPublic && !key.StartsWith(PrvChar)))
            {
                if (key.Any(c => !Digits.Contains(c)))
                {
                    for (int i = 0; i < key.Length; i++)
                    {
                        if (!Digits.Contains(key[i]))
                        {
                            Console.WriteLine($"Invalid character at index {i} ({key[i]})");
                        }
                    }
                }
                result32 = null;
                result256 = null;
                cs = false;
                return false;
            }

            Span<byte> base32 = new byte[key.Length];
            Span<byte> base256 = new byte[key.Length];
            for (int i = 0; i < key.Length; i++)
            {
                base32[i] = (byte)Digits.IndexOf(key[i]);
            }

            base256[0] = (byte)(base32[0] << 3 | base32[1] >> 2);
            base256[1] = (byte)(base32[1] << 6 | base32[2] << 1 | base32[3] >> 4);
            base256[2] = (byte)(base32[3] << 4 | base32[4] >> 1);
            base256[3] = (byte)(base32[4] << 7 | base32[5] << 2 | base32[6] >> 3);
            base256[4] = (byte)(base32[6] << 5 | base32[7]);
            base256[5] = (byte)(base32[8] << 3 | base32[9] >> 2);
            base256[6] = (byte)(base32[9] << 6 | base32[10] << 1 | base32[11] >> 4);
            base256[7] = (byte)(base32[11] << 4 | base32[12] >> 1);
            base256[8] = (byte)(base32[12] << 7 | base32[13] << 2 | base32[14] >> 3);
            base256[9] = (byte)(base32[14] << 5 | base32[15]);
            base256[10] = (byte)(base32[16] << 3 | base32[17] >> 2);
            base256[11] = (byte)(base32[17] << 6 | base32[18] << 1 | base32[19] >> 4);
            base256[12] = (byte)(base32[19] << 4 | base32[20] >> 1);
            base256[13] = (byte)(base32[20] << 7 | base32[21] << 2 | base32[22] >> 3);
            base256[14] = (byte)(base32[22] << 5 | base32[23]);
            base256[15] = (byte)(base32[24] << 3 | base32[25] >> 2);
            base256[16] = (byte)(base32[25] << 6 | base32[26] << 1 | base32[27] >> 4);
            base256[17] = (byte)(base32[27] << 4 | base32[28] >> 1);
            base256[18] = (byte)(base32[28] << 7 | base32[29] << 2 | base32[30] >> 3);
            base256[19] = (byte)(base32[30] << 5 | base32[31]);
            base256[20] = (byte)(base32[32] << 3 | base32[33] >> 2);
            base256[21] = (byte)(base32[33] << 6 | base32[34] << 1 | base32[35] >> 4);
            base256[22] = (byte)(base32[35] << 4 | base32[36] >> 1);
            base256[23] = (byte)(base32[36] << 7 | base32[37] << 2 | base32[38] >> 3);
            base256[24] = (byte)(base32[38] << 5 | base32[39]);
            base256[25] = (byte)(base32[40] << 3 | base32[41] >> 2);
            base256[26] = (byte)(base32[41] << 6 | base32[42] << 1 | base32[43] >> 4);
            base256[27] = (byte)(base32[43] << 4 | base32[44] >> 1);
            base256[28] = (byte)(base32[44] << 7 | base32[45] << 2 | base32[46] >> 3);
            base256[29] = (byte)(base32[46] << 5 | base32[47]);
            base256[30] = (byte)(base32[48] << 3 | base32[49] >> 2);
            base256[31] = (byte)(base32[49] << 6 | base32[50] << 1 | base32[51] >> 4);
            base256[32] = (byte)(base32[51] << 4 | base32[52] >> 1);
            base256[33] = (byte)(base32[52] << 7 | base32[53] << 2 | base32[54] >> 3);
            base256[34] = (byte)(base32[54] << 5 | base32[55]);

            if ((isPublic && base256[0] != PubByte) || (!isPublic && base256[0] != PrvByte))
            {
                result32 = null;
                result256 = null;
                cs = false;
                return false;
            }

            cs = base256.Slice(33, 2).SequenceEqual(CalculateChecksum(base256.Slice(0, 33)).ToByteArray(false));
            result32 = base32.ToArray();
            result256 = base256.Slice(1, 32).ToArray();
            return true;
        }

        

        /// <summary>
        /// Calculates checksum of a given byte array based on CRC16-XModem specifications.
        /// </summary>
        /// <param name="data">An array of bytes to calculate checksum of.</param>
        /// <returns>a 16-bit unsigned integer.</returns>
        public static ushort CalculateChecksum(Span<byte> data)
        {
            unchecked
            {
                ushort crc = 0;

                for (int i = 0; i < data.Length; i++)
                {
                    crc ^= (ushort)(data[i] << 8);
                    for (int j = 0; j < 8; j++)
                    {
                        crc = (crc & 0x8000) != 0 ? (ushort)((crc << 1) ^ 0x1021) : (ushort)(crc << 1);
                    }
                }

                return crc;
            }
        }
    }
}
