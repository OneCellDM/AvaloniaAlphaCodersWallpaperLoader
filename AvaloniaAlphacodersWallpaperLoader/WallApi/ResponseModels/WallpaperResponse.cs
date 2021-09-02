using System.Collections.Generic;
using WallsAlphaCodersLib.Interfaces;

namespace WallsAlphaCodersLib.ResponseModels
{
    public class WallpaperResponse : ICheckLast
    {
        public List<Wallpaper>? wallpapers { get; set; }

        public string? total_match { get; set; }

        public bool? check_last { get; set; }
    }
}