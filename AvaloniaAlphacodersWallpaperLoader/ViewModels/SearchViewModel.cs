using System;
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


namespace AvaloniaAlphacodersWallpaperLoader.ViewModels
{
    public class SearchViewModel : WallpapersPageViewModelBase
    {
        private bool isSearch = false;
        private string _Term;

        public IReactiveCommand SearchCommand { get; set; }


        public string Term
        {
            get => _Term;
            set => this.RaiseAndSetIfChanged(ref _Term, value);
        }


        public SearchViewModel(WallpaperApi api, ObservableCollection<ImageModel> images) : base(api, images)
        {
            SearchCommand = ReactiveCommand.Create(() =>
            {
                if (!string.IsNullOrEmpty(Term))
                {
                    IsVisible = false;
                    Title = $"Поиск:{Term}";
                    CurrentPage = 1;
                    LoadWallpapers();
                }
            });
        }


        public override async Task LoadWallpapers()
        {
            try
            {
                var param = new SearchRequestParams(){Term = Term, Page = CurrentPage};

                await base.LoadWallpapers(Api.SearchWallpapers, param);
            }
            catch (Exception ex)
            {
                Debug.WriteLine(ex.Message);
            }
        }
    }
}