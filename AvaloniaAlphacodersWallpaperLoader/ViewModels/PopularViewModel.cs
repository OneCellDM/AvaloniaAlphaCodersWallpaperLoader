using System;
using System.Collections.ObjectModel;
using System.Diagnostics;
using System.Drawing;
using System.Threading.Tasks;
using AvaloniaAlphacodersWallpaperLoader.Models;
using AvaloniaAlphacodersWallpaperLoader.ViewModels.Interfaces;
using JetBrains.Annotations;
using ReactiveUI;
using WallsAlphaCodersLib;

namespace AvaloniaAlphacodersWallpaperLoader.ViewModels
{
    public class PopularViewModels:WallpapersPageViewModelBase
    {
        public PopularViewModels( WallpaperApi api,  ObservableCollection<ImageModel> collection) : 
            base(api, collection)
        {
                
        }
        
        public override async Task LoadWallpapers()
        {
            try
            {
                
            }
            catch (Exception ex)
            {
                Debug.WriteLine(ex.Message);
            }

        }
    }
}