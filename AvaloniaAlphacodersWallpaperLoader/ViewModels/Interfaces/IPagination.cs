using ReactiveUI;

namespace AvaloniaAlphacodersWallpaperLoader.ViewModels.Interfaces
{
    public interface IPagination : IpaginationVisible
    {
        public IReactiveCommand NextPageCommand { get; set; }
        public IReactiveCommand PreviousPageCommand { get; set; }
        public int CurrentPage { get; set; }
    }
}