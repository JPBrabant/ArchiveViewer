using System;
using System.Collections.Generic;
using System.IO;
using System.IO.Compression;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Windows.Storage;
using Windows.Storage.Pickers;
using Windows.UI.Xaml.Media.Imaging;
using ArchiveViewerModels.Archive;

namespace ArchiveViewerUI.Components
{
    public class ArchivePicker
    {
        public static async Task<StorageFile> GetArchiveAsync()
        {
            var picker = new FileOpenPicker();
            picker.ViewMode = PickerViewMode.List;
            picker.SuggestedStartLocation = PickerLocationId.Desktop;
            picker.FileTypeFilter.Add(".zip");
            return await picker.PickSingleFileAsync();
        }
    }
}
