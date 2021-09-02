using Avalonia.Controls;
using Avalonia.Markup.Xaml;

namespace AvaloniaAlphacodersWallpaperLoader.Views
{
    public partial class LoadingBlankPage : UserControl
    {
        public LoadingBlankPage()
        {
            InitializeComponent();
        }

        private void InitializeComponent()
        {
            AvaloniaXamlLoader.Load(this);
        }
    }
}