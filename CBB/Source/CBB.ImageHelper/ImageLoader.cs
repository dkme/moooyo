using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;

namespace CBB.ImageHelper
{
    public  class ImageLoader
    {
        public byte[] loadimage(string filename)
        {
            return new CBB.MongoDB.GridFSHelper().GetFile(filename);
        }
    }
}
