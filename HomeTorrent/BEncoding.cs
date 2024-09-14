using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HomeTorrent
{
    public static class BEncoding
    {
        // We are storing the byte values of each of these characters for comparison later.
        // GetBytes() converts any char/string given to it into a byte array.
        // Here, since we're only dealing with single letter strings, there is only one value in byte array.
        // If there were more, we'd have more byte numbers.
        private static byte DictionaryStart = System.Text.Encoding.UTF8.GetBytes("d")[0];
        private static byte DictionaryEnd = System.Text.Encoding.UTF8.GetBytes("e")[0];
        private static byte ListStart = System.Text.Encoding.UTF8.GetBytes("l")[0];
        private static byte ListEnd = System.Text.Encoding.UTF8.GetBytes("e")[0];
        private static byte NumberStart = System.Text.Encoding.UTF8.GetBytes("i")[0];
        private static byte NumberEnd = System.Text.Encoding.UTF8.GetBytes("e")[0];
        private static byte ByteArrayDivider = System.Text.Encoding.UTF8.GetBytes(":")[0];

        public static object Decode(byte[] bytes)
        {
            IEnumerator<byte> enumerator = ((IEnumerable<byte>)bytes).GetEnumerator();
            enumerator.MoveNext(); // enumerator starts before the first element

            return DecodeNextObject(enumerator);
        }

        private static object DecodeNextObject(IEnumerator<byte> enumerator)
        {
            if (enumerator.Current == DictionaryStart)
            {
                return DecodeDictionary(enumerator);
            }

            if (enumerator.Current == ListStart)
            {
                return DecodeList(enumerator);
            }

            if (enumerator.Current == NumberStart)
            {
                return DecodeNumber(enumerator);
            }

            return DecodeByteArray(enumerator);
        }
    }
}
