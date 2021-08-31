using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace WallsAlphaCodersLib.Objects.Interfaces
{
	internal interface ICheckLast
	{
		public bool? check_last { get; set; }
	}
}