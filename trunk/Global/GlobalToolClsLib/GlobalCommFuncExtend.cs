using System;
using System.ComponentModel;
using System.Linq;
using System.Reflection;
using System.Windows.Forms;

namespace GlobalToolClsLib
{


	public static class GlobalCommFuncExtend
	{
		public static string GetEnumDescription<T>(this T value) where T : struct
		{
			string result = value.ToString();
			Type type = typeof(T);
			FieldInfo info = type.GetField(value.ToString());
			object[] attributes = info.GetCustomAttributes(typeof(DescriptionAttribute), inherit: true);
			if (attributes != null && attributes.FirstOrDefault() != null)
			{
				result = (attributes.First() as DescriptionAttribute).Description;
			}
			return result;
		}

		public static object GetEnumDescription<T>(this T value, Type attributeType, int argumentIndex = 0) where T : struct
		{
			Type type = typeof(T);
			FieldInfo info = type.GetField(value.ToString());
			CustomAttributeData attribute = info.CustomAttributes.FirstOrDefault((CustomAttributeData x) => x.AttributeType == attributeType);
			if (attribute != null)
			{
				return attribute.ConstructorArguments[argumentIndex].Value;
			}
			return default(T);
		}

		public static void OnUIThread2(this Control control, Action code)
		{
			if (control.InvokeRequired)
			{
				control.BeginInvoke(code);
			}
			else
			{
				code();
			}
		}
	}
}
