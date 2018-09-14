using System;
using System.Collections.Generic;

namespace StellarCracker_NetCore
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("How many mistakes are in this key?");
            int mode = 0;
            do
            {
                Console.WriteLine("Enter 1 or 2");
                string s = Console.ReadLine();
                if (s == "1" || s == "2")
                {
                    mode = int.Parse(s);
                }
            } while (mode == 0);

            string key = string.Empty;
            do
            {
                Console.WriteLine("Enter your key (it should be 56 chars long and start with S):");
                key = Console.ReadLine();
            } while (key.Length != 56 || !key.StartsWith("S"));

            Console.WriteLine("Please wait...");

            char[] keyArr = key.ToCharArray();

            List<string> validKeys = new List<string>();

            DateTime t = DateTime.Now;
            int count = 0;
            if (mode == 1)
            {
                for (int i = 1; i < 56; i++)
                {
                    for (int j = 0; j < 32; j++)
                    {
                        char[] temp = keyArr.ShallowReplace(i, CharSet[j]);
                        string keyToCheck = new string(temp);
                        if (Base32.IsValid(keyToCheck))
                        {
                            if (!validKeys.Contains(keyToCheck))
                            {
                                validKeys.Add(keyToCheck);
                            }
                        }
                        count++;
                    }
                }
            }
            else if (mode == 2)
            {
                for (int i1 = 1; i1 < 56; i1++)
                {
                    for (int i2 = 0; i2 < 56; i2++)
                    {
                        for (int j1 = 0; j1 < 32; j1++)
                        {
                            for (int j2 = 0; j2 < 32; j2++)
                            {
                                char[] temp = keyArr.ShallowReplace(i1, CharSet[j1], i2, CharSet[j2]);
                                string keyToCheck = new string(temp);
                                if (Base32.IsValid(keyToCheck))
                                {
                                    if (!validKeys.Contains(keyToCheck))
                                    {
                                        validKeys.Add(keyToCheck);
                                    }
                                }
                                count++;
                            }
                        }
                    }
                }
            }

            Console.WriteLine("====================================================");
            Console.WriteLine($"Checked {count} keys and it took {DateTime.Now.Subtract(t).TotalSeconds} seconds.");
            Console.WriteLine("Here is a list of valid keys I found:");
            validKeys.ForEach(x => Console.WriteLine(x));

            Console.WriteLine("Press enter to exit!");
            Console.ReadLine();
        }

        private const string CharSet = "ABCDEFGHIJKLMNOPQRSTUVWXYZ234567";
    }
}
