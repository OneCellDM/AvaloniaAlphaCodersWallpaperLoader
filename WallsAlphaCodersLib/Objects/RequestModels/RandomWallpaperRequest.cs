using WallsAlphaCodersLib.Objects.RequestModels.Interfaces;

namespace WallsAlphaCodersLib.Objects.RequestModels
{
	public class RandomWallpaperRequest : ICount, IInfoLevel
	{
		/// <summary>
		/// Max 3
		/// </summary>

		public int? Info_Level { get; set; }

		/// <summary>
		/// value: 1-30
		/// </summary>
		public int? Count { get; set; }
	}
}