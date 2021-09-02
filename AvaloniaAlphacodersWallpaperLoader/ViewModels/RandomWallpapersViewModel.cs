using System.Collections.ObjectModel;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using AvaloniaAlphacodersWallpaperLoader.Models;
using AvaloniaAlphacodersWallpaperLoader.ViewModels.Interfaces;
using ReactiveUI;
using WallsAlphaCodersLib;
using WallsAlphaCodersLib.RequestParams;

namespace AvaloniaAlphacodersWallpaperLoader.ViewModels
{
    public class RandomWallpapersViewModel : ViewModelBase, IView, IpaginationVisible
    {
        private bool _IsVisible;
        private int _CurrentPage;

        public event IView.CloseViewDelegate? CloseViewEvent;
        public WallpaperApi Api { get; set; }

        public IReactiveCommand? CloseCommand { get; set; }

        public string Title { get; set; } = "Случайные";
        public bool IsVisible { get; set; }

        public ObservableCollection<ImageModel> Images { get; set; }

        public RandomWallpapersViewModel(WallpaperApi api, ObservableCollection<ImageModel> images)
        {
            Images = images;
            Api = api;
        }

        public void Load()
        {
            Images.Clear();
            var resAwaiter = Api.RandomWallpapers(new RandomWallpaperRequestParams()).GetAwaiter();
            resAwaiter.OnCompleted(() =>
            {
                try
                {
                    if (resAwaiter.GetResult() != null)
                    {
                        resAwaiter.GetResult().wallpapers.ForEach(x => Images.Add(new(x)));
                        Task.Run(() => Images.ToList().ForEach(x => x.LoadBItmap()));
                    }
                }
                catch (ApiException ex)
                {
                    Debug.WriteLine(ex.Message);
                }
            });
        }


        public bool BackPaginationIsVisible { get; set; }
        public bool NextPaginationIsVisible { get; set; }
    }
}