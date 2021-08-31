using System;
using System.Net.NetworkInformation;
using WallsAlphaCodersLib.Objects.Interfaces;

namespace WallsAlphaCodersLib.Objects.RequestModels
{
	public class SearchWallpaperRequest : IInfoLevel, IPage, ISize, IOperator
	{
		public string term { get; set; }
		public Operator? @operator { get; set; }
		public int? info_level { get; set; }
		public int? page { get; set; }
		public int? width { get; set; }
		public int? height { get; set; }
	}
}