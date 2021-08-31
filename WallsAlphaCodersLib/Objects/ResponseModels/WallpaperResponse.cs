using System.Collections.Generic;

namespace WallsAlphaCodersLib.Objects.ResponseModels
{
    public class WallpaperResponse
    {
        public bool? success { get; set; }
        public List<Wallpaper>? wallpapers { get; set; }
        public string? total_match { get; set; }
    }
}