using System.Collections.Generic;
using System.Text.Json.Serialization;
using WallsAlphaCodersLib.Objects.Interfaces;

namespace WallsAlphaCodersLib.Objects.ResponseModels
{
	public class WallpaperResponse : ICheckLast
	{
		public List<Wallpaper>? wallpapers { get; set; }

		public string? total_match { get; set; }

		public bool? check_last { get; set; }
	}
}