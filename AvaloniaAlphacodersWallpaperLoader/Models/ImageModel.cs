using System;
using System.ComponentModel;
using System.IO;
using System.Net.Http;
using System.Runtime.CompilerServices;
using System.Threading.Tasks;
using Avalonia.Media.Imaging;
using JetBrains.Annotations;
using WallsAlphaCodersLib.ResponseModels;


namespace AvaloniaAlphacodersWallpaperLoader.Models
{
    public class ImageModel : Wallpaper, INotifyPropertyChanged
    {
        private Bitmap _bitmap;
        private bool _Checked;
        public bool Checked
        {
            get => _Checked;
            set
            {
                _Checked = value;
                OnPropertyChanged(); 
            }
        }

        public Bitmap BitmapImage
        {
            get => _bitmap;
            set
            {
                _bitmap = value;
                OnPropertyChanged();
            }
        }

        public async Task<MemoryStream>? GetStream()
        {
            try
            {
                using (HttpClient client = new HttpClient())
                    return new MemoryStream(await client.GetByteArrayAsync(url_thumb));
            }
            catch (Exception ex)
            {
                return null;
            }
        }

        public async void LoadBItmap()
        {
            using (var stream = await GetStream())
                if (stream != null)
                    BitmapImage = await Task.Run(() => Bitmap.DecodeToWidth(stream, 600));
        }

        public event PropertyChangedEventHandler? PropertyChanged;

        [NotifyPropertyChangedInvocator]
        protected virtual void OnPropertyChanged([CallerMemberName] string propertyName = null) =>
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));

        public void Load(Wallpaper wallpaper)
        {
            id = wallpaper.id;
            url_image = wallpaper.url_image;
            url_page = wallpaper.url_page;
            url_thumb = wallpaper.url_thumb;
            width = wallpaper.width;
            height = wallpaper.height;
            file_size = wallpaper.file_size;
            file_type = wallpaper.file_type;
        }
        public ImageModel(Wallpaper wallpaper)
        {
            id = wallpaper.id;
            url_image = wallpaper.url_image;
            url_page = wallpaper.url_page;
            url_thumb = wallpaper.url_thumb;
            width = wallpaper.width;
            height = wallpaper.height;
            file_size = wallpaper.file_size;
            file_type = wallpaper.file_type;
        }

        public ImageModel()
        {
        }
    }
}