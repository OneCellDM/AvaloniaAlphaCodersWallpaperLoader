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
        private bool _IsLoading;

        public event IView.CloseViewDelegate? CloseViewEvent;
        public WallpaperApi Api { get; set; }

        public IReactiveCommand? CloseCommand { get; set; }

        public string Title { get; set; } = "Случайные";
        public bool IsVisible { get; set; }
        
        public bool IsLoading
        {
            get => _IsLoading;
            set => this.RaiseAndSetIfChanged(ref _IsLoading, value);
        }

        public ObservableCollection<ImageModel> Images { get; set; }

        public RandomWallpapersViewModel(WallpaperApi api, ObservableCollection<ImageModel> images)
        {
            Images = images;
            Api = api;
        }

        public async Task Load()
        {
            
            Images.Clear();
            
            await Task.Run(() =>
            {
                for (int i = 0; i < 30; i++)
                    Images.Add(new());
            });


            var resAwaiter = Api.RandomWallpapers(new RandomWallpaperRequestParams()).GetAwaiter();
            resAwaiter.OnCompleted(() =>
            {
                try
                {
                    if (resAwaiter.GetResult() != null)
                    {
                        var res= resAwaiter.GetResult().wallpapers;
                        
                        for (int i = 0; i <res.Count; i++)
                                Images[i].Load(res[i]);
                        
                        Task.Run(() => Images.ToList().ForEach(x => x.LoadBItmap()));
                    }
                    else Images.Clear();
                }
                catch (ApiException ex)
                {
                    Debug.WriteLine(ex.Message);
                }
                finally
                {
                    IsLoading = false;
                }
            });
        }


        public bool BackPaginationIsVisible { get; set; }
        public bool NextPaginationIsVisible { get; set; }
    }
}