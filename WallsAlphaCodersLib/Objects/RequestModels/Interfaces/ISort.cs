using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WallsAlphaCodersLib.Objects.RequestModels.Interfaces
{
	public enum Sort
	{
		newest,
		rating,
		views,
		favorites,
	}

	public interface ISort
	{
		public Sort? Sort { get; set; }
	}
}