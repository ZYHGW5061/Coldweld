using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Drawing;
using System.Linq;
using System.Reflection;
using System.Threading;
using System.Windows.Forms;

namespace GlobalToolClsLib
{


	public static class GlobalCommFunc
	{
		public static class EnumHelper
		{
			public static List<Source> GetEnumResource<T>() where T : struct
			{
				List<Source> rslt = new List<Source>();
				foreach (T item in Enum.GetValues(typeof(T)))
				{
					string displayText = item.GetEnumDescription();
					if (!string.IsNullOrEmpty(displayText))
					{
						rslt.Add(new Source
						{
							Key = Convert.ToInt32(item),
							Value = displayText
						});
					}
				}
				return rslt;
			}

			public static T GetEnumValue<T>(string enumString) where T : struct
			{
				if (Enum.TryParse<T>(enumString, ignoreCase: true, out var rslt))
				{
					return rslt;
				}
				throw new ArgumentException("未找到对应的枚举", enumString);
			}

			public static string GetDisplayName(Enum value)
			{
				FieldInfo field = value.GetType().GetField(value.ToString());

				DisplayAttribute attribute = field.GetCustomAttribute<DisplayAttribute>();

				return attribute == null ? value.ToString() : attribute.Name;
			}

			public static void FillComboBoxWithEnumDisplay(ComboBox comboBox, Type enumType)
			{
				foreach (var name in Enum.GetNames(enumType))
				{
					var field = enumType.GetField(name);
					var attribute = field.GetCustomAttribute<DisplayAttribute>();
					var displayName = attribute == null ? name : attribute.Name;
					comboBox.Items.Add(displayName);
				}
			}
			public static void FillComboBoxWithEnumDesc(ComboBox comboBox, Type enumType)
			{
				comboBox.DataSource = Enum.GetValues(enumType).Cast<Enum>().Select(value => new
				{
					(Attribute.GetCustomAttribute(value.GetType().GetField(value.ToString()), typeof(DescriptionAttribute)) as DescriptionAttribute).Description,
					Value = value
				}).ToList().OrderBy(x=>x.Value).ToList();

				comboBox.DisplayMember = "Description";
				comboBox.ValueMember = "Value";
			}

			public static string GetEnumDescription(Enum value)
			{
				FieldInfo field = value.GetType().GetField(value.ToString());

				DescriptionAttribute attribute = field.GetCustomAttribute<DescriptionAttribute>();

				return attribute == null ? value.ToString() : attribute.Description;
			}
		}

		public static class GdiFunc
		{
			public static PointF GetPolygonCenter(PointF[] points)
			{
				float sumX = points.Sum((PointF s) => s.X);
				float sumY = points.Sum((PointF s) => s.Y);
				float centerX = sumX / (float)points.Length;
				float centerY = sumY / (float)points.Length;
				return new PointF(centerX, centerY);
			}

			public static PointF[] PointOffset(PointF[] points, float dx, float dy)
			{
				PointF[] rslt = new PointF[points.Length];
				for (int i = 0; i < rslt.Length; i++)
				{
					rslt[i].X = points[i].X + dx;
					rslt[i].Y = points[i].Y + dy;
				}
				return rslt;
			}

			public static PointF PointOffset(PointF point, float dx, float dy)
			{
				PointF rslt = default(PointF);
				rslt.X = point.X + dx;
				rslt.Y = point.Y + dy;
				return rslt;
			}
		}

		public static class DebugHelper
		{
			public static void ConsoleWrite(string message)
			{
				Console.WriteLine(message);
			}
		}

		public static Form MainForm { get; set; }

		public static void Disponse(CancellationTokenSource cts)
		{
			if (cts != null)
			{
				if (!cts.IsCancellationRequested)
				{
					cts.Cancel();
				}
				cts.Dispose();
				cts = null;
			}
		}
		
	}
}
