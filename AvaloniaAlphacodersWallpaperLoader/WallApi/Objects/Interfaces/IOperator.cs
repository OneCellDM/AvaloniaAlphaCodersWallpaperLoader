using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace WallsAlphaCodersLib.Objects.Interfaces
{
	public enum Operator
	{
		max,
		equal,
		min,
	}

	public interface IOperator
	{
		public Operator? @operator { get; set; }
	}
}