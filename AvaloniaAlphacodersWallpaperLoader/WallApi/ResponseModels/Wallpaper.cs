using WallsAlphaCodersLib.Interfaces;

namespace WallsAlphaCodersLib.ResponseModels
{
    public class Wallpaper : Iid, ISize
    {
        public int? id { get; set; }
        public int? width { get; set; }
        public int? height { get; set; }
        public string? file_type { get; set; }
        public int? file_size { get; set; }
        public string? url_image { get; set; }
        public string? url_thumb { get; set; }
        public string? url_page { get; set; }
    }
}