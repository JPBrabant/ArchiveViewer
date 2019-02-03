using System;
using System.Collections.Generic;
using System.IO;
using System.IO.Compression;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ArchiveViewerModels.Other;
using Windows.Storage;
using Windows.UI.Xaml.Media.Imaging;

namespace ArchiveViewerModels.Archive
{
    public class CurrentArchive : IArchive, ICurrentArchive
    {
        private int i = 0;
        private List<BitmapImage> images = null;

        private StorageFile currentArchive = null;

        private static CurrentArchive instance = new CurrentArchive();

        private CurrentArchive() { }

        public static CurrentArchive GetInstance()
        {
            return instance;
        }

        private ICustomObserver observer = null;

        public void Register(ICustomObserver observer)
        {
            this.observer = observer;
        }

        public async Task UpdateArchiveAsync(StorageFile file)
        {
            images = new List<BitmapImage>();

            var stream = await file.OpenStreamForReadAsync();

            ZipArchive archive = new ZipArchive(stream, ZipArchiveMode.Read);

            foreach (ZipArchiveEntry entry in archive.Entries)
            {
                if (entry.Length != 0 && entry.Name.EndsWith(".jpg"))
                {
                    var memoryStream = new MemoryStream();
                    await entry.Open().CopyToAsync(memoryStream);
                    memoryStream.Position = 0;

                    BitmapImage bitmapImage = new BitmapImage();
                    bitmapImage.DecodePixelHeight = 200;
                    await bitmapImage.SetSourceAsync(memoryStream.AsRandomAccessStream());

                    images.Add(bitmapImage);
                }

            }
            

            this.observer.Update();
        }

        public BitmapImage GetCurrentImage()
        {
            return this.images[this.i];
        }

        public BitmapImage GetNextImage()
        {
            if (this.i < this.images.Count)
            {
                this.i++;
            }
            return this.GetCurrentImage();
        }

        public BitmapImage GetPreviousImage()
        {
            if (this.i > 0)
            {
                this.i--;
            }
            return this.GetCurrentImage();
        }
    }
}
