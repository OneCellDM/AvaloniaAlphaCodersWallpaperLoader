using System;
using System.ComponentModel;
using System.Diagnostics;
using System.IO;
using System.Net.Http;
using System.Reactive.Subjects;
using System.Runtime.CompilerServices;
using System.Threading.Tasks;
using Avalonia.Media.Imaging;
using JetBrains.Annotations;
using WallsAlphaCodersLib.Objects.ResponseModels;

namespace AvaloniaAlphacodersWallpaperLoader.Models
{
    public class ImageModel:Wallpaper,INotifyPropertyChanged
    {
        
        private Bitmap _bitmap;

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
            HttpClient client = new HttpClient();
           
            Debug.WriteLine(url_thumb);
                return  new MemoryStream(await client.GetByteArrayAsync(url_thumb));
            
            
            
        }

        public async void LoadBItmap()
        {
           


                using (var stream = await GetStream())
                {
                    BitmapImage = await Task.Run(() => Bitmap.DecodeToWidth( stream,300));
                }
          
        }

        public event PropertyChangedEventHandler? PropertyChanged;

        [NotifyPropertyChangedInvocator]
        protected virtual void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
} 