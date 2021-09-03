using System.Collections.ObjectModel;
using System.Diagnostics;
using System.Drawing;
using System.Linq;
using System.Runtime.CompilerServices;
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
        private bool _IsLoading;

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
        public bool IsLoading
        {
            get => _IsLoading;
            set => this.RaiseAndSetIfChanged(ref _IsLoading, value);
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


        public async Task Load()
        {
            Images.Clear();
            
            await Task.Run(() =>
            {
                for (int i = 0; i < 30; i++)
                    Images.Add(new());
                
            });
            
            var resAwaiter = Api.SearchWallpapers(new SearchWallpaperRequestParams() {term = Term, page = CurrentPage})
                .GetAwaiter();
            
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
                    Images.Clear();
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