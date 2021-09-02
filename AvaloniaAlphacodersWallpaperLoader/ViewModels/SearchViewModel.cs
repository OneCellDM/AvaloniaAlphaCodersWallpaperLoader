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
    public class SearchViewModel : ViewModelBase, IView, IPagination
    {
        private bool _IsVisible;
        private int _CurrentPage;
        private string _Term;
        private string _Title;

        private bool isSearch = false;

        public IReactiveCommand SearchCommand { get; set; }

        public event IView.CloseViewDelegate? CloseViewEvent;
        public WallpaperApi Api { get; set; }
        public IReactiveCommand? CloseCommand { get; set; }

        public string Title
        {
            get => $"Поиск: {_Title}";
            set => this.RaiseAndSetIfChanged(ref _Title, value);
        }

        public string Term
        {
            get => _Term;
            set => this.RaiseAndSetIfChanged(ref _Term, value);
        }

        public bool IsVisible
        {
            get => _IsVisible;
            set
            {
                this.RaiseAndSetIfChanged(ref _IsVisible, value);
                if (_IsVisible)
                    Term = string.Empty;
                if (isSearch)
                {
                    Load();
                }
            }
        }

        public IReactiveCommand NextPageCommand { get; set; }
        public IReactiveCommand PreviousPageCommand { get; set; }
        public int CurrentPage { get; set; } = 1;

        public ObservableCollection<ImageModel> Images { get; set; }

        public SearchViewModel(WallpaperApi api, ObservableCollection<ImageModel> images)
        {
            Images = images;
            Api = api;
            CloseCommand = ReactiveCommand.Create((object obj) =>
            {
                CloseViewEvent?.Invoke();
                IsVisible = false;
            });

            SearchCommand = ReactiveCommand.Create(() =>
            {
                if (!string.IsNullOrEmpty(Term))
                {
                    IsVisible = false;
                    Title = Term;
                    CurrentPage = 1;
                    Load();
                }
            });

            NextPageCommand = ReactiveCommand.Create(() =>
            {
                CurrentPage++;
                Load();
            });
            PreviousPageCommand = ReactiveCommand.Create(() =>
            {
                if (CurrentPage > 0)
                {
                    CurrentPage--;
                    Load();
                }
            });
        }


        public void Load()
        {
            Images.Clear();
            var resAwaiter = Api.SearchWallpapers(new SearchWallpaperRequestParams() {term = Term, page = CurrentPage})
                .GetAwaiter();
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