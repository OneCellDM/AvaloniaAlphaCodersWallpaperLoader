using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WallsAlphaCodersLib.Objects.RequestModels.Interfaces
{
	public enum Operator
	{
		max,
		equal,
		min,
	}

	public interface IOperator
	{
		public Operator? Operator { get; set; }
	}
}