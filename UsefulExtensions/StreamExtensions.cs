using System;
using System.IO;

namespace UsefulExtensions
{
    public static class StreamExtensions
    {
        public static void CopyTo(this Stream instance, Stream output)
        {
            instance.Position = 0;
            var buffer = new byte[8 * 1024];
            int len;
            while ((len = instance.Read(buffer, 0, buffer.Length)) > 0) output.Write(buffer, 0, len);
        }

        public static byte[] ToByteArray(this Stream stream)
        {
            long originalPosition = 0;

            if (stream.CanSeek)
            {
                originalPosition = stream.Position;
                stream.Position = 0;
            }

            try
            {
                var readBuffer = new byte[4096];
                var totalBytesRead = 0;
                int bytesRead;

                while ((bytesRead = stream.Read(readBuffer, totalBytesRead, readBuffer.Length - totalBytesRead)) > 0)
                {
                    CopyBytes(stream, ref readBuffer, ref totalBytesRead, bytesRead);
                }

                var buffer = readBuffer;
                if (readBuffer.Length == totalBytesRead) return buffer;

                buffer = new byte[totalBytesRead];
                Buffer.BlockCopy(readBuffer, 0, buffer, 0, totalBytesRead);
                return buffer;
            }
            finally
            {
                if (stream.CanSeek)
                {
                    stream.Position = originalPosition;
                }
            }
        }

        private static void CopyBytes(Stream stream, ref byte[] readBuffer, ref int totalBytesRead, int bytesRead)
        {
            totalBytesRead += bytesRead;

            if (totalBytesRead != readBuffer.Length) return;

            var nextByte = stream.ReadByte();

            if (nextByte == -1) return;

            var temp = new byte[readBuffer.Length * 2];
            Buffer.BlockCopy(readBuffer, 0, temp, 0, readBuffer.Length);
            Buffer.SetByte(temp, totalBytesRead, (byte)nextByte);
            readBuffer = temp;
            totalBytesRead++;
        }

    }
}
