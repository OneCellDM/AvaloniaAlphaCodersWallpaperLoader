using System;
using System.Net.NetworkInformation;
using WallsAlphaCodersLib.Objects.RequestModels.Interfaces;

namespace WallsAlphaCodersLib.Objects.RequestModels
{
	public class SearchWallpaperRequest : IInfoLevel, IPage, ISize, IOperator
	{
		public string Term { get; set; }
		public Operator? Operator { get; set; }
		public int? Info_Level { get; set; }
		public int? Page { get; set; }
		public int? Width { get; set; }
		public int? Height { get; set; }
	}
}