using ReactiveUI;
using System.Collections.ObjectModel;
using WallsAlphaCodersLib;

namespace AvaloniaAlphacodersWallpaperLoader.ViewModels.Interfaces
{
    public interface IView
    {
        public delegate void CloseViewDelegate();

        public event CloseViewDelegate CloseViewEvent;
        WallpaperApi Api { get; set; }
        IReactiveCommand? CloseCommand { get; set; }
        public string Title { get; set; }
        public bool IsVisible { get; set; }
        public ObservableCollection<Models.ImageModel> Images { get; set; }
        public void Load();
    }
}