using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace WallsAlphaCodersLib.Objects.Interfaces
{
	public interface ISize
	{
		public int? width { get; set; }

		public int? height { get; set; }
	}
}