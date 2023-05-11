using blogs.Services.Abstract;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ZstdNet;

namespace blogs.Services.Implementations
{
    public class CompressionService : ICompressionService
    {
        public MemoryStream Compress(string source)
        {
            var inputStream = GenerateStreamFromString(source);
            var outputStream = new MemoryStream();

            var compressionStream = new CompressionStream(outputStream);
            
            inputStream.CopyTo(compressionStream);
            compressionStream.Flush();

            outputStream.Position = 0;
            return outputStream;
        }

        /// <summary>
        /// Получает на вход строку и преобразует её в поток байтов
        /// </summary>
        private Stream GenerateStreamFromString(string s)
        {
            var stream = new MemoryStream();
            var writer = new StreamWriter(stream);
            writer.Write(s);
            writer.Flush();
            stream.Position = 0;
            return stream;
        }
    }
}
