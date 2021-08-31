using WallsAlphaCodersLib.Objects.Interfaces;

namespace WallsAlphaCodersLib.Objects.RequestModels
{
	public class RandomWallpaperRequest : ICount, IInfoLevel
	{
		/// <summary>
		/// Max 3
		/// </summary>

		public int? info_level { get; set; }

		/// <summary>
		/// value: 1-30
		/// </summary>
		public int? count { get; set; }
	}
}