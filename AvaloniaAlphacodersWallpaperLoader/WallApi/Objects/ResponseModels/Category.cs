using WallsAlphaCodersLib.Objects.Interfaces;

namespace WallsAlphaCodersLib.Objects.ResponseModels
{
	public class Category : Iid, ICount
	{
		public int? id { get; set; }
		public string? name { get; set; }
		public int? count { get; set; }
		public string? url { get; set; }
	}
}