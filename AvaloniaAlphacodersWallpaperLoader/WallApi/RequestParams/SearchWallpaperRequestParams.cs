using WallsAlphaCodersLib.Interfaces;

namespace WallsAlphaCodersLib.RequestParams
{
    public class SearchWallpaperRequestParams : IInfoLevel, IPage, ISize, IOperator
    {
        public string term { get; set; }
        public Operator? @operator { get; set; }
        public int? info_level { get; set; }
        public int? page { get; set; }
        public int? width { get; set; }
        public int? height { get; set; }
    }
}