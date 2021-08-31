using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WallsAlphaCodersLib.Objects.Interfaces;

namespace WallsAlphaCodersLib.Objects.RequestModels
{
	public class CategoryRequest : Iid, IInfoLevel, ISort, IPage, ISize, IOperator, ICheckLast

	{
		public int? id { get; set; }
		public int? info_level { get; set; }
		public int? page { get; set; }
		public Sort? sort { get; set; }
		public int? width { get; set; }
		public int? height { get; set; }
		public Operator? @operator { get; set; }
		public bool? check_last { get; set; }
	}
}