// StellarCracker
// Copyright (c) 2018 Coding Enthusiast
// Distributed under the MIT software license, see the accompanying
// file LICENCE or http://www.opensource.org/licenses/mit-license.php.

using stellar_dotnet_sdk;
using System;
using System.Diagnostics;
using System.Numerics;
using System.Runtime.CompilerServices;

namespace StellarCracker_NetCore
{
    public static class Worker
    {
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static unsafe bool MoveNextItems(int* items, int len, int max)
        {
            for (int i = len - 1; i >= 0; --i)
            {
                items[i] += 1;

                if (items[i] == max)
                {
                    items[i] = 0;
                }
                else
                {
                    return true;
                }
            }

            return false;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static unsafe bool MoveNextIndex(int* items, int length, int max)
        {
            for (int i = 0; i < length; i++)
            {
                int currentIndex = length - i - 1;
                if (items[currentIndex] < max - i - 1)
                {
                    items[currentIndex]++;

                    for (int j = 1; (currentIndex + j) < length; j++)
                    {
                        items[currentIndex + j] = items[currentIndex] + j;
                    }

                    return true;
                }
            }

            return false;
        }

        public static unsafe void Start(ReadOnlySpan<byte> prv32, ReadOnlySpan<byte> pub256)
        {
            Stopwatch timer = Stopwatch.StartNew();
            int missCount = 1;
            BigInteger a = 1;
            BigInteger b = 1;
            BigInteger c = 1;

            byte[] ba32 = new byte[prv32.Length];
            byte[] ba256 = new byte[32];

            int* mi = stackalloc int[55 + 55];
            int* items = mi + 55;

            while (a > 0)
            {
                Console.WriteLine($"Checking {missCount} char wrong cases:");
                a *= 56 - missCount;
                b *= missCount;
                c *= 32;
                BigInteger total = (a / b) * c;
                Console.WriteLine($"Going to check {total:n0} permutation...");

                fixed (byte* b32 = &ba32[0], b256 = &ba256[0], p32 = &prv32[0])
                {
                    for (int i = 0; i < missCount; i++)
                    {
                        mi[i] = i + 1;
                        items[i] = 0;
                    }

                    do
                    {
                        do
                        {
                            Buffer.MemoryCopy(p32, b32, ba32.Length, prv32.Length);
                            for (int i = 0; i < missCount; i++)
                            {
                                b32[mi[i]] = (byte)items[i];
                            }

                            byte version = (byte)(b32[0] << 3 | b32[1] >> 2);
                            if (version == Base32.PrvByte)
                            {
                                b256[0] = (byte)(b32[1] << 6 | b32[2] << 1 | b32[3] >> 4);
                                b256[1] = (byte)(b32[3] << 4 | b32[4] >> 1);
                                b256[2] = (byte)(b32[4] << 7 | b32[5] << 2 | b32[6] >> 3);
                                b256[3] = (byte)(b32[6] << 5 | b32[7]);
                                b256[4] = (byte)(b32[8] << 3 | b32[9] >> 2);
                                b256[5] = (byte)(b32[9] << 6 | b32[10] << 1 | b32[11] >> 4);
                                b256[6] = (byte)(b32[11] << 4 | b32[12] >> 1);
                                b256[7] = (byte)(b32[12] << 7 | b32[13] << 2 | b32[14] >> 3);
                                b256[8] = (byte)(b32[14] << 5 | b32[15]);
                                b256[9] = (byte)(b32[16] << 3 | b32[17] >> 2);
                                b256[10] = (byte)(b32[17] << 6 | b32[18] << 1 | b32[19] >> 4);
                                b256[11] = (byte)(b32[19] << 4 | b32[20] >> 1);
                                b256[12] = (byte)(b32[20] << 7 | b32[21] << 2 | b32[22] >> 3);
                                b256[13] = (byte)(b32[22] << 5 | b32[23]);
                                b256[14] = (byte)(b32[24] << 3 | b32[25] >> 2);
                                b256[15] = (byte)(b32[25] << 6 | b32[26] << 1 | b32[27] >> 4);
                                b256[16] = (byte)(b32[27] << 4 | b32[28] >> 1);
                                b256[17] = (byte)(b32[28] << 7 | b32[29] << 2 | b32[30] >> 3);
                                b256[18] = (byte)(b32[30] << 5 | b32[31]);
                                b256[19] = (byte)(b32[32] << 3 | b32[33] >> 2);
                                b256[20] = (byte)(b32[33] << 6 | b32[34] << 1 | b32[35] >> 4);
                                b256[21] = (byte)(b32[35] << 4 | b32[36] >> 1);
                                b256[22] = (byte)(b32[36] << 7 | b32[37] << 2 | b32[38] >> 3);
                                b256[23] = (byte)(b32[38] << 5 | b32[39]);
                                b256[24] = (byte)(b32[40] << 3 | b32[41] >> 2);
                                b256[25] = (byte)(b32[41] << 6 | b32[42] << 1 | b32[43] >> 4);
                                b256[26] = (byte)(b32[43] << 4 | b32[44] >> 1);
                                b256[27] = (byte)(b32[44] << 7 | b32[45] << 2 | b32[46] >> 3);
                                b256[28] = (byte)(b32[46] << 5 | b32[47]);
                                b256[29] = (byte)(b32[48] << 3 | b32[49] >> 2);
                                b256[30] = (byte)(b32[49] << 6 | b32[50] << 1 | b32[51] >> 4);
                                b256[31] = (byte)(b32[51] << 4 | b32[52] >> 1);
                            }

                            ushort actualCS = (ushort)((byte)(b32[52] << 7 | b32[53] << 2 | b32[54] >> 3) |
                                                       (b32[54] << 13 | b32[55] << 8));

                            // Compute checksum
                            ushort expectedCS = (ushort)(version << 8);
                            for (int j = 0; j < 8; j++)
                            {
                                expectedCS = (expectedCS & 0x8000) != 0 ?
                                    (ushort)((expectedCS << 1) ^ 0x1021) :
                                    (ushort)(expectedCS << 1);
                            }
                            expectedCS ^= (ushort)(b256[0] << 8);
                            for (int j = 0; j < 8; j++)
                            {
                                expectedCS = (expectedCS & 0x8000) != 0 ?
                                    (ushort)((expectedCS << 1) ^ 0x1021) :
                                    (ushort)(expectedCS << 1);
                            }
                            for (int i = 1; i < ba256.Length; i++)
                            {
                                expectedCS ^= (ushort)(b256[i] << 8);
                                for (int j = 0; j < 8; j++)
                                {
                                    expectedCS = (expectedCS & 0x8000) != 0 ?
                                        (ushort)((expectedCS << 1) ^ 0x1021) :
                                        (ushort)(expectedCS << 1);
                                }
                            }

                            if (actualCS == expectedCS)
                            {
                                KeyPair keypair = KeyPair.FromSecretSeed(ba256);
                                if (pub256.SequenceEqual(keypair.PublicKey))
                                {
                                    Console.WriteLine("Found the correct key:");
                                    Console.WriteLine(Base32.Encode32(ba32));
                                    timer.Stop();
                                    Console.WriteLine($"Elapsed time: {timer.Elapsed}");
                                    return;
                                }
                            }
                        } while (MoveNextItems(items, missCount, 32));
                    } while (MoveNextIndex(mi, missCount, 56));

                    Console.WriteLine($"Elapsed time: {timer.Elapsed}");
                    Console.WriteLine("====================================================");
                    timer.Restart();
                    missCount++;
                }
            }
        }
    }
}
