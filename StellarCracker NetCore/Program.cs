// StellarCracker
// Copyright (c) 2018 Coding Enthusiast
// Distributed under the MIT software license, see the accompanying
// file LICENCE or http://www.opensource.org/licenses/mit-license.php.

#if !DEBUG
using stellar_dotnet_sdk;
#endif
using System;
#if DEBUG
using System.Diagnostics;
#endif

namespace StellarCracker_NetCore
{
    class Program
    {
        static void Main(string[] args)
        {
#pragma warning disable IDE0018 // Inline variable declaration
            byte[] prv, pub;
#pragma warning restore IDE0018 // Inline variable declaration
#if DEBUG
            Console.ForegroundColor = ConsoleColor.Red;
            Console.WriteLine("You are running in debug mode, only a hard coded test key will run.");
            Console.WriteLine("Compile in release mode for optimization and ability to enter your key.");
            // correct key is     SBLVIQ4IQQG47BQVRUXXHJNTAQKRAXOURFZYSIGNJ4XLVFY4MPD552NS
            string testPrivate = "SBLVIQ4IQQG4HBQVRUXXHJNTAKKXAXOURFZYSIGNJ4XLVFY4MPD552NS";
            string testPublic = "GBYBAH2S2JQBDJL47RX6Y5BFQCJ3DMQG2Z6A6IBNGXYWCNK67LIS7L4K";

            bool b = Base32.TryDecode(testPrivate, false, out prv, out _, out bool cs);
            Debug.Assert(b);
            Debug.Assert(!cs);
            b = Base32.TryDecode(testPublic, true, out _, out pub, out cs);
            Debug.Assert(b);
            Debug.Assert(cs);
#else
            string input;
            bool isPrvCSValid = false;
            byte[] prv256;
            do
            {
                Console.WriteLine("Enter Stellar private key (starts with S and is 56 characters long)");
                input = Console.ReadLine();
            } while (!Base32.TryDecode(input, false, out prv, out prv256, out isPrvCSValid));


            do
            {
                Console.WriteLine("Enter Stellar public key (starts with G and is 56 characters long)");
                input = Console.ReadLine();
            } while (!Base32.TryDecode(input, true, out _, out pub, out bool cs) || !cs);

            if (isPrvCSValid)
            {
                Console.WriteLine("The given private key string has a valid checksum. Checking against the given public key.");
                KeyPair keypair = KeyPair.FromSecretSeed(prv256);
                if (((ReadOnlySpan<byte>)pub).SequenceEqual(keypair.PublicKey))
                {
                    Console.WriteLine("The given private key is without mistakes!");
                    Console.ReadLine();
                    return;
                }
                else
                {
                    Console.WriteLine("The given private key doesn't correspond to the given public key. Starting brute force.");
                }
            }
#endif
            Worker.Start(prv, pub);

            Console.WriteLine("Press enter to exit!");
            Console.ReadLine();
        }
    }
}
