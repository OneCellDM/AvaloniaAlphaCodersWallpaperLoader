using System.Collections.Generic;
using WallsAlphaCodersLib.Interfaces;

namespace WallsAlphaCodersLib.ResponseModels
{
    public class Category : Iid, ICount
    {
        public int? id { get; set; }
        public string? name { get; set; }
        public int? count { get; set; }
        public string? url { get; set; }
    }

    public class CategoryListResponse
    {
        public List<Category> categories { get; set; }
    }
}