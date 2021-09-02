using WallsAlphaCodersLib.Interfaces;

namespace WallsAlphaCodersLib.RequestParams
{
    public class RandomWallpaperRequestParams : ICount, IInfoLevel
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