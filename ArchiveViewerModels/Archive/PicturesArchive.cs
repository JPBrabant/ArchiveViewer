using ArchiveViewerModels.File;
using System;
using System.Collections.Generic;
using System.IO;
using System.IO.Compression;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Windows.Storage;

namespace ArchiveViewerModels.Archive
{
    public class PicturesArchive
    {
        private ZipArchive archive;

        private List<Picture> pictures;

        private int index;
        private int count;

        public PicturesArchive(StorageFile file)
        {
            // TODO validate archive.
            // TODO Ignore non-pictures ?

            var task = file.OpenStreamForReadAsync();
            task.Wait();

            archive = new ZipArchive(task.Result, ZipArchiveMode.Read);

            pictures = new List<Picture>();

            foreach (var entry in archive.Entries)
            {
                var picture = new Picture(entry);
                if (Picture.Validate(picture))
                {
                    pictures.Add(picture);
                }
            }

            count = pictures.Count;
            index = 0;
        }

        public Picture GetCurrentPicture()
        {
            return pictures[index];
        }

        public Picture GetNextPicture()
        {
            if (index < (count-1))
            {
                index++;
            }
            return GetCurrentPicture();
        }

        public Picture GetPreviousPicture()
        {
            if (index > 0)
            {
                index--;
            }
            return GetCurrentPicture();
        }
    }
}
