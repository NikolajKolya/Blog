using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace blogs.Services.Abstract
{
    public interface ICompressionService
    {
        MemoryStream Compress(string source);

        Stream LoadCompressedFile(string path);
    }
}
