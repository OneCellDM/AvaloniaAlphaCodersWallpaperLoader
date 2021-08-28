using System;
using System.ComponentModel;
using System.IO;
using System.Net.Http;
using System.Reactive.Subjects;
using System.Runtime.CompilerServices;
using System.Threading.Tasks;
using Avalonia.Media.Imaging;
using JetBrains.Annotations;

namespace AvaloniaWallboxLoader.Models
{
    public class ImageModel:WallBox.DataModel.ImageModel,INotifyPropertyChanged
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

        public  ImageModel(WallBox.DataModel.ImageModel image)
        {
            Alt = image.Alt;
            Date = image.Date;
            Height = image.Height;
            Width = image.Width;
            LoadPageUrl = image.LoadPageUrl;
            Url = image.Url;
        }

        public async Task<MemoryStream>? GetStream()
        {
            HttpClient client = new HttpClient();
            try
            {
                return  new MemoryStream(await client.GetByteArrayAsync(Url));
            }
            catch (Exception)
            {
                return null;
            }
            finally{client.Dispose();}
            
        }

        public async void LoadBItmap()
        {
            try
            {


                using (var stream = await GetStream())
                {
                    BitmapImage = await Task.Run(() => new Bitmap(stream));
                }
            }
            
            catch (Exception e)
            {
                
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