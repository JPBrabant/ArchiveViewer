using System;
using System.Collections.Generic;
using System.IO;
using System.IO.Compression;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using Windows.Foundation;
using Windows.Foundation.Collections;
using Windows.Graphics.Imaging;
using Windows.Storage;
using Windows.Storage.Pickers;
using Windows.Storage.Streams;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Controls.Primitives;
using Windows.UI.Xaml.Data;
using Windows.UI.Xaml.Input;
using Windows.UI.Xaml.Media;
using Windows.UI.Xaml.Media.Imaging;
using Windows.UI.Xaml.Navigation;
using ArchiveViewerModels.Archive;
using ArchiveViewerModels.Other;
using ArchiveViewerUI.Components;
using Windows.UI.Core;
using ArchiveViewerModels.File;
using System.Threading.Tasks;

// The Blank Page item template is documented at https://go.microsoft.com/fwlink/?LinkId=402352&clcid=0x409

namespace ArchiveViewerUI
{
    /// <summary>
    /// An empty page that can be used on its own or navigated to within a Frame.
    /// </summary>
    public sealed partial class MainWindow : Page
    {
        private PicturesArchive currentPicturesArchive;

        public MainWindow()
        {
            this.InitializeComponent();
            Window.Current.CoreWindow.PointerWheelChanged += ScrollWheelEvent;
        }

        private async void ScrollWheelEvent(CoreWindow sender, PointerEventArgs args)
        {
            if (currentPicturesArchive != null)
            {
                var scrollUp = args.CurrentPoint.Properties.MouseWheelDelta >= 0;

                if (scrollUp)
                {
                    await updatePicture(currentPicturesArchive.GetPreviousPicture());
                }
                else
                {
                    await updatePicture(currentPicturesArchive.GetNextPicture());
                }
            }
        }

        private async void OpenFilePickerEvent(object sender, RoutedEventArgs e)
        {
            var file = await ArchivePicker.GetArchiveAsync();
            currentPicturesArchive = new PicturesArchive(file);

            await updatePicture(currentPicturesArchive.GetCurrentPicture());
        }

        private async Task updatePicture(Picture picture)
        {
            BitmapImage bitmap = new BitmapImage();
            //bitmap.DecodePixelHeight = 200;
            await bitmap.SetSourceAsync(picture.MemoryStream.AsRandomAccessStream());

            ImageViewer.Source = bitmap;
        }
    }
}
