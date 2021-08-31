using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using AvaloniaAlphacodersWallpaperLoader.Models;
using ReactiveUI;
using WallsAlphaCodersLib;
using WallsAlphaCodersLib.Objects.RequestModels;

namespace AvaloniaAlphacodersWallpaperLoader.ViewModels
{
    public class MainWindowViewModel : ViewModelBase
    {
        private int _CurrentPage = 1;
        private int _PageCount = 0;
        private string _Title;

        private int _SelectedMenuItem = -1;
        private bool _CategoriesIsVisible = false;

        private bool _ControlIsVisible = true;
        private bool _SearchIsVisible = false;


        private List<int> Pages;

        public bool CategoriesIsVisible
        {
            get => _CategoriesIsVisible;
            set => this.RaiseAndSetIfChanged(ref _CategoriesIsVisible, value);
        }

        public bool SearchIsVisible
        {
            get => _SearchIsVisible;
            set => this.RaiseAndSetIfChanged(ref _SearchIsVisible, value);
        }

        public bool ControlIsVisible
        {
            get => _ControlIsVisible;
            set => this.RaiseAndSetIfChanged(ref _ControlIsVisible, value);
        }


        public int CurrentPage
        {
            get => _CurrentPage;
            set => this.RaiseAndSetIfChanged(ref _CurrentPage, value);
        }

        public int PageCount
        {
            get => _PageCount;
            set => this.RaiseAndSetIfChanged(ref _PageCount, value);
        }

        public string Title
        {
            get => _Title;
            set => this.RaiseAndSetIfChanged(ref _Title, value);
        }


        public ObservableCollection<ImageModel> ImageModels { get; set; } = new ObservableCollection<ImageModel>();

        public IReactiveCommand CloseCategoriesCommand { get; set; }

        public IReactiveCommand CloseSearchCommand { get; set; }
        
      

        public int SelectedMenuItem
        {
            get => _SelectedMenuItem;
            set
            {
                this.RaiseAndSetIfChanged(ref _SelectedMenuItem, value);
                switch (value)
                {
                    case 0:
                    {
                        ControlIsVisible = true;
                        Title = "Все";
                       
                        break;
                    }
                    case 1:
                    {
                        ControlIsVisible = false;
                        SearchIsVisible = true;
                        SelectedMenuItem = -1;
                        break;
                    }
                    case 2:
                    {
                        ControlIsVisible = false;
                        CategoriesIsVisible = true;
                        SelectedMenuItem = -1;
                        break;
                    }
                }
            }
        }

        public MainWindowViewModel()
        {
         
            LoadData();
            CloseCategoriesCommand = ReactiveCommand.Create((object ob) =>
            {
                CategoriesIsVisible = false;
                ControlIsVisible = true;
                SelectedMenuItem = -1;
            });
            CloseSearchCommand = ReactiveCommand.Create((object ob) =>
            {
                SearchIsVisible = false;
                ControlIsVisible = true;
                SelectedMenuItem = -1;
            });
        }

        private void GetCategories()
        {
            
        }

        private void LoadData()
        {
            WallpaperApi api = new WallpaperApi("dcc164e06ea94f0187af2a6dfdc8bef8");


            var resAwaiter = api.RandomWallpapers(new GetRandomWallpaperRequest()).GetAwaiter();
                resAwaiter.OnCompleted(() =>
                {
                    if (resAwaiter.GetResult() != null)
                    {
                        resAwaiter.GetResult().wallpapers?.ForEach(x => ImageModels.Add(new() {url_thumb = x.url_image}));

                        Task.Run(() => ImageModels.ToList().ForEach(x => x.LoadBItmap()));
                    }
                });
            
        }
    }
}