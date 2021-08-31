using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Text.Json;
using System.Text.Json.Serialization;

namespace WallsAlphaCodersLib.Objects.ResponseModels
{
	public class ApiResponse<TResponse>
	{
		public bool Success { get; set; }

		public TResponse Response { get; set; }

		public string Error { get; set; }

		private TResponse Wallpapers { set { Response = value; } }
		private TResponse Count { set { Response = value; } }
		private TResponse Categories { set { Response = value; } }
	}
}