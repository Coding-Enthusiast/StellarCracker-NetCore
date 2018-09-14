using System.Linq;

namespace StellarCracker_NetCore
{
    public class Base32
    {
        /// <summary>
        /// Checks to see if a given string is a valid base32-encoded key based on stellar specifications.
        /// </summary>
        /// <param name="key">Key to check</param>
        /// <returns>True if input is valid, false if otherwise.</returns>
        public static bool IsValid(string key)
        {
            key = key.ToUpper();
            if (key.Any(c => c > 127))
            {
                return false;
            }

            byte[] decoded = new byte[key.Length * 5 / 8]; // log(32) / log(256)

            byte curByte = 0, bitsRemaining = 8;
            int mask = 0, arrayIndex = 0;

            foreach (var c in key)
            {
                //int cValue = CharToValue(c);

                int i = c;

                if (i < 91 && i > 64)
                {
                    i -= 65;
                }
                else if (i < 56 && i > 49)
                {
                    i -= 24;
                }

                if (bitsRemaining > 5)
                {
                    mask = i << (bitsRemaining - 5);
                    curByte = (byte)(curByte | mask);
                    bitsRemaining -= 5;
                }
                else
                {
                    mask = i >> (5 - bitsRemaining);
                    curByte = (byte)(curByte | mask);
                    decoded[arrayIndex++] = curByte;
                    curByte = (byte)(i << (3 + bitsRemaining));
                    bitsRemaining += 3;
                }
            }

            //if we didn't end with a full byte
            if (arrayIndex != decoded.Length)
            {
                decoded[arrayIndex] = curByte;
            }

            if (decoded[0] != 144)
            {
                return false;
            }

            byte[] calculatedCheckSum = CalculateChecksum(decoded.SubArray(0, decoded.Length - 2)).ToByteArray(false);
            byte[] decodedChecksum = decoded.SubArrayFromEnd(2);

            return decodedChecksum[0] == calculatedCheckSum[0]
                && decodedChecksum[1] == calculatedCheckSum[1];
        }


        /// <summary>
        /// Calculates checksum of a given byte array based on CRC16-XModem specifications.
        /// </summary>
        /// <param name="data">An array of bytes to calculate checksum of.</param>
        /// <returns>a 16-bit unsigned integer.</returns>
        public static ushort CalculateChecksum(byte[] data)
        {
            unchecked
            {
                ushort crc = 0;

                for (int a = 0; a < data.Length; a++)
                {
                    crc ^= (ushort)(data[a] << 8);
                    for (int i = 0; i < 8; i++)
                    {
                        crc = (crc & 0x8000) != 0 ? (ushort)((crc << 1) ^ 0x1021) : (ushort)(crc << 1);
                    }
                }

                return crc;
            }
        }
    }
}
