// StellarCracker Tests
// Copyright (c) 2018 Coding Enthusiast
// Distributed under the MIT software license, see the accompanying
// file LICENCE or http://www.opensource.org/licenses/mit-license.php.

using stellar_dotnet_sdk;
using StellarCracker_NetCore;
using System;
using System.Collections.Generic;
using Xunit;

namespace Tests
{
    public class Base32Tests
    {
        public static IEnumerable<object[]> GetDecodeCases()
        {
            string prv1 = "SBLVIQ4IQQG47BQVRUXXHJNTAQKRAXOURFZYSIGNJ4XLVFY4MPD552NS";
            string prv2 = "SANNP4MMDM7RWGXDFLASFKEQLQWSEOVXRORMQOUINAJ5C43BA5JIYZBT";
            string prv3 = "SCAKGTFVUL3NZLJ3W6NCQM3ZE7BFNIYJKSLVHXPOQATJHV43H5MRHRU3";
            string pub1 = "GBYBAH2S2JQBDJL47RX6Y5BFQCJ3DMQG2Z6A6IBNGXYWCNK67LIS7L4K";
            string pub2 = "GA3WU6B24EKA7CYLCJIBKUF5USQCMEUK4QBSQMSYKYA5ZD4JTW64ZF7E";
            string pub3 = "GDDHOVH5KWSBRJ3QHM2T3G6T326MXAQ4ASZAEDUOL3RNOWIHTMTLAR2W";

            yield return new object[] { prv1, false };
            yield return new object[] { prv2, false };
            yield return new object[] { prv3, false };
            yield return new object[] { pub1, true };
            yield return new object[] { pub2, true };
            yield return new object[] { pub3, true };
        }
        [Theory]
        [MemberData(nameof(GetDecodeCases))]
        public void TryDecodeTest(string key, bool isPublic)
        {
            byte[] expected = Decode(key, isPublic);
            bool b = Base32.TryDecode(key, isPublic, out _, out byte[] actual, out bool actualCS);
            Assert.True(b);
            Assert.True(actualCS);
            Assert.Equal(expected, actual);
        }

        private static byte[] Decode(string s, bool isPublic)
        {
            byte[] result = Base32Encoding.ToBytes(s);
            if (result.Length != 35)
            {
                throw new Exception();
            }
            if ((isPublic && result[0] != 48) || (!isPublic && result[0] != 144))
            {
                throw new Exception();
            }

            return result.AsSpan().Slice(1, 32).ToArray();
        }
    }
}
