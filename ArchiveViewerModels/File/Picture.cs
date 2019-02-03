using System;
using System.Collections.Generic;
using System.IO;
using System.IO.Compression;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ArchiveViewerModels.File
{
    public class Picture
    {
        private ZipArchiveEntry zipArchiveEntry;

        public string Name
        {
            get
            {
                return zipArchiveEntry.Name;
            }
        }

        public MemoryStream MemoryStream
        {
            get
            {
                var memoryStream = new MemoryStream();
                zipArchiveEntry.Open().CopyTo(memoryStream);
                memoryStream.Position = 0;
                return memoryStream;
            }
        }

        public Picture(ZipArchiveEntry entry)
        {
            zipArchiveEntry = entry;
        }

        public static bool Validate(Picture picture)
        {
            return picture != null && !string.IsNullOrEmpty(picture.Name) && (picture.Name.ToLower().EndsWith(".jpg") || picture.Name.ToLower().EndsWith(".png") || picture.Name.ToLower().EndsWith(".gif"));
        }
    }

}
