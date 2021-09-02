using System.Collections.ObjectModel;
using AvaloniaAlphacodersWallpaperLoader.Models;
using AvaloniaAlphacodersWallpaperLoader.ViewModels.Interfaces;
using ReactiveUI;
using WallsAlphaCodersLib;

namespace AvaloniaAlphacodersWallpaperLoader.ViewModels
{
    public class MainWindowViewModel : ViewModelBase
    {
        private bool _NewPage = true;
        private string _Title;
        private int _SelectedMenuItem = -1;
        private int _oldSelectedMenuItem = -1;
        private int _ImageSelectedIndex = -1;
        private bool _ImageViewIsVisible = false;
        private IView _ActiveView;
        private IView _OldActiveView;
        private CategoryViewModel CategoryViewModel { get; set; }
        private RandomWallpapersViewModel RandomWallpapersViewModel { get; set; }
        private SearchViewModel SearchViewModel { get; set; }
        private WallpaperApi api = new WallpaperApi("dcc164e06ea94f0187af2a6dfdc8bef8");
        private ImageViewViewModel _ImageViewViewModel;
        public ObservableCollection<ImageModel> ImageModels { get; set; } = new ObservableCollection<ImageModel>();

        public IView ActiveView
        {
            get => _ActiveView;
            set
            {
                _OldActiveView = _ActiveView;
                this.RaiseAndSetIfChanged(ref _ActiveView, value);
                _ActiveView.CloseViewEvent += ActiveViewOnCloseViewEvent;
            }
        }

        private void ActiveViewOnCloseViewEvent()
        {
            ActiveView = _OldActiveView;
            _NewPage = false;
            SelectedMenuItem = _oldSelectedMenuItem;
        }

        public ImageViewViewModel ImageViewViewModel
        {
            get => _ImageViewViewModel;
            set => this.RaiseAndSetIfChanged(ref _ImageViewViewModel, value);
        }

        public bool ImageViewIsVisible
        {
            get => _ImageViewIsVisible;
            set => this.RaiseAndSetIfChanged(ref _ImageViewIsVisible, value);
        }


        public int ImageSelectedIndex
        {
            get => _ImageSelectedIndex;
            set
            {
                this.RaiseAndSetIfChanged(ref _ImageSelectedIndex, value);
                if (_ImageSelectedIndex > -1)
                {
                    ImageViewViewModel = new ImageViewViewModel(ImageModels[_ImageSelectedIndex]);
                    ImageViewIsVisible = true;
                    ImageViewViewModel.CloseEvent += delegate
                    {
                        ImageViewIsVisible = false;
                        ImageViewViewModel = null;
                    };
                }
            }
        }

        public int SelectedMenuItem
        {
            get => _SelectedMenuItem;
            set
            {
                if (_NewPage) _oldSelectedMenuItem = _SelectedMenuItem;
                this.RaiseAndSetIfChanged(ref _SelectedMenuItem, value);
                if (_SelectedMenuItem > -1)
                {
                    ActiveView.IsVisible = false;
                    switch (value)
                    {
                        case 0:
                        {
                            ActiveView = RandomWallpapersViewModel;
                            break;
                        }
                        case 1:
                        {
                            ActiveView = SearchViewModel;
                            break;
                        }
                        case 2:
                        {
                            ActiveView = CategoryViewModel;
                            break;
                        }
                    }

                    if (_NewPage) ActiveView.IsVisible = true;
                    else _NewPage = true;
                }
            }
        }

        public MainWindowViewModel()
        {
            CategoryViewModel = new CategoryViewModel(api, ImageModels);
            RandomWallpapersViewModel = new RandomWallpapersViewModel(api, ImageModels);
            SearchViewModel = new SearchViewModel(api, ImageModels);
            ActiveView = RandomWallpapersViewModel;
            SelectedMenuItem = 0;
        }
    }
}