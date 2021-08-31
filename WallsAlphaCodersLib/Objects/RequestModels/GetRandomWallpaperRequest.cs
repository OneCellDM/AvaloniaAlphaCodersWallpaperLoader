namespace WallsAlphaCodersLib.Objects.RequestModels
{
    public class GetRandomWallpaperRequest
    {
        /// <summary>
        /// Max 3
        /// </summary>
        public int? InfoLevel { get; set; }

        /// <summary>
        /// value: 1-30
        /// </summary>
        public int? Count { get; set; }
    }
}