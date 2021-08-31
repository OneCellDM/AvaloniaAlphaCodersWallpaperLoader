using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WallsAlphaCodersLib.Objects.RequestModels.Interfaces;

namespace WallsAlphaCodersLib.Objects.RequestModels
{
	public class CategoryRequest : Iid, IInfoLevel, ISort, IPage, ISize, IOperator, ICheckLast

	{
		public int? Id { get; set; }
		public int? Info_Level { get; set; }
		public int? Page { get; set; }
		public Sort? Sort { get; set; }
		public int? Width { get; set; }
		public int? Height { get; set; }
		public Operator? Operator { get; set; }
		public bool? Check_Last { get; set; }
	}
}