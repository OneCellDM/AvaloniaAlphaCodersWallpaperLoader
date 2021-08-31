using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace WallsAlphaCodersLib.Objects.Interfaces
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
		public Sort? sort { get; set; }
	}
}