using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WallsAlphaCodersLib.Objects.RequestModels.Interfaces
{
	public interface ISize
	{
		public int? Width { get; set; }
		public int? Height { get; set; }
	}
}