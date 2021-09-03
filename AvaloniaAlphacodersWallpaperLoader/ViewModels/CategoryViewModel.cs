using System.Collections.ObjectModel;
using System.Linq;
using System.Threading.Tasks;
using AvaloniaAlphacodersWallpaperLoader.Models;
using System.Diagnostics;
using AvaloniaAlphacodersWallpaperLoader.ViewModels.Interfaces;
using ReactiveUI;
using WallsAlphaCodersLib;
using WallsAlphaCodersLib.ResponseModels;
using WallsAlphaCodersLib.Interfaces;
using WallsAlphaCodersLib.RequestParams;

namespace AvaloniaAlphacodersWallpaperLoader.ViewModels
{
    public class CategoryViewModel : ViewModelBase, IView, IPagination
    {
        private int _CategorySelectedIndex;
        private bool _IsVisible;
        private string _title;

        private bool _BackPginationIsVisible;
        private bool _NextPaginationIsVisible;
        private bool _IsLoading;


        public bool BackPaginationIsVisible
        {
            get => _BackPginationIsVisible;
            set => this.RaiseAndSetIfChanged(ref _BackPginationIsVisible, value);
        }

        public bool NextPaginationIsVisible
        {
            get => _BackPginationIsVisible;
            set => this.RaiseAndSetIfChanged(ref _NextPaginationIsVisible, value);
        }

        public event IView.CloseViewDelegate? CloseViewEvent;
        public WallpaperApi Api { get; set; }

        public IReactiveCommand? CloseCommand { get; set; }

        public string Title
        {
            get => $"Категория: {_title}";
            set => this.RaiseAndSetIfChanged(ref _title, value);
        }

        public bool IsVisible
        {
            get => _IsVisible;
            set => this.RaiseAndSetIfChanged(ref _IsVisible, value);
        }

        public bool IsLoading
        {
            get => _IsLoading;
            set => this.RaiseAndSetIfChanged(ref _IsLoading, value);
        }

        public ObservableCollection<Category> Categories { get; set; } = new();

        public ObservableCollection<ImageModel> Images { get; set; }

        public IReactiveCommand NextPageCommand { get; set; }
        public IReactiveCommand PreviousPageCommand { get; set; }
        public int CurrentPage { get; set; } = 1;

        public int CategorySelectedIndex
        {
            get => _CategorySelectedIndex;
            set
            {
                this.RaiseAndSetIfChanged(ref _CategorySelectedIndex, value);
                if (_CategorySelectedIndex > -1)
                {
                    CurrentPage = 1;

                    IsVisible = false;
                    Title = Categories[_CategorySelectedIndex].name;
                    Load();
                }
            }
        }

        public CategoryViewModel()
        {
        }

        public CategoryViewModel(WallpaperApi api, ObservableCollection<Models.ImageModel> imageModels)
        {
            Images = imageModels;
            Api = api;

            CloseCommand = ReactiveCommand.Create((object obj) =>
            {
                CloseViewEvent?.Invoke();
                IsVisible = false;
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

            var resAwaiter = Api.CategoryList().GetAwaiter();
            resAwaiter.OnCompleted(() => { resAwaiter.GetResult().categories.ForEach(x => Categories.Add(x)); });
        }

        public async Task Load()
        {
           
            Images.Clear();
            await Task.Run(() =>
            {
                for (int i = 0; i < 30; i++)
                    Images.Add(new());
            });
            var resAwaiter = Api.Category(new CategoryRequestParams()
            {
                id = Categories[CategorySelectedIndex].id,
                page = CurrentPage,
                check_last = true,
                sort = Sort.newest,
            }).GetAwaiter();
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
    }
}