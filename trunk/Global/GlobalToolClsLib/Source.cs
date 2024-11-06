using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GlobalToolClsLib
{
    public class Source : Source<int, string>
    {
    }
	public class Source<T, K>
	{
		public T Key { get; set; }

		public K Value { get; set; }

		public override string ToString()
		{
			return Value.ToString();
		}
	}
}
