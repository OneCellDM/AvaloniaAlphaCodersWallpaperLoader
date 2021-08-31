using System.Collections.Generic;
using WallsAlphaCodersLib.Objects.RequestModels;

namespace WallsAlphaCodersLib.Objects.ResponseModels
{
    public class CategoryListResponse
    {
        public bool? success { get; set; }

        public List<CategoryListResponse> Categories { get; set; }
    }
}