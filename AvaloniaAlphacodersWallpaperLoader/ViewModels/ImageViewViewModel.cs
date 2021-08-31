using Avalonia.Media.Imaging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ReactiveUI;
using System.Net.Http;
using AvaloniaAlphacodersWallpaperLoader.Models;
using System.IO;

namespace AvaloniaAlphacodersWallpaperLoader.ViewModels
{
	public class ImageViewViewModel : ViewModelBase
	{
		private Bitmap _Bitmap;
		public Bitmap Image { get => _Bitmap; set => this.RaiseAndSetIfChanged(ref _Bitmap, value); }

		public ImageModel ImageModel { get; private set; }

		public ImageViewViewModel(ImageModel imageModel)
		{
			ImageModel = imageModel;

			Task.Run(() => loadFullImage());
		}

		public async void loadFullImage()
		{
			HttpClient httpClient = new HttpClient();
			try
			{
				Image = new Bitmap(new MemoryStream(await httpClient.GetByteArrayAsync(ImageModel.url_image)));
			}
			finally { httpClient.Dispose(); }
		}
	}
}