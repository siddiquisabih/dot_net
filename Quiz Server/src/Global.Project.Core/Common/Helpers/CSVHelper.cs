using System;
using System.Linq;
using System.Configuration;
using Microsoft.AspNetCore.Http;
using System.Reflection;
using System.Collections.Generic;
using System.ComponentModel;
using System.Text;

namespace Global.Project.Common.Helpers
{
	public static class CSVHelper
	{
		public static string ToCSV<T>(this IEnumerable<T> objects, Type type = null, string CsvSeparator = ",")
		{
			StringBuilder output = new StringBuilder();
			var fields =
				from mi in typeof(T).GetMembers(BindingFlags.Public | BindingFlags.Instance | BindingFlags.Static)
				where new[] { MemberTypes.Field, MemberTypes.Property }.Contains(mi.MemberType)

				select mi;

			if (type != null)
			{
				var props = type.GetProperties();

				List<string> record = new List<string>();

				foreach (var prop in props)
				{
					object[] attrs = prop.GetCustomAttributes(true);
					foreach (object attr in attrs)
					{
						DisplayNameAttribute authAttr = attr as DisplayNameAttribute;
						if (authAttr != null)
						{
							string fieldName = authAttr.DisplayName;
							record.Add(fieldName);
						}
					}
				}

				output.AppendLine(QuoteRecord(record, CsvSeparator));
			}
			else
			{
				output.AppendLine(QuoteRecord(fields.Select(f => f.Name), CsvSeparator));
			}

			foreach (var record in objects)
			{
				output.AppendLine(QuoteRecord(FormatObject(fields, record), CsvSeparator));
			}
			return output.ToString();
		}

		static IEnumerable<string> FormatObject<T>(IEnumerable<MemberInfo> fields, T record)
		{
			foreach (var field in fields)
			{
				if (field is FieldInfo)
				{
					var fi = (FieldInfo)field;
					yield return Convert.ToString(fi.GetValue(record));
				}
				else if (field is PropertyInfo)
				{
					var pi = (PropertyInfo)field;
					yield return Convert.ToString(pi.GetValue(record, null));
				}
				else
				{
					throw new Exception("Unhandled case.");
				}
			}
		}

		static string QuoteRecord(IEnumerable<string> record, string csvSeparator)
		{
			return string.Join(csvSeparator, record.Select(field => QuoteField(field, csvSeparator)).ToArray());
		}

		static string QuoteField(string field, string csvSeparator)
		{
			if (string.IsNullOrEmpty(field))
			{
				return "\"\"";
			}
			else if (field.Contains(csvSeparator) || field.Contains("\"") || field.Contains("\r") || field.Contains("\n"))
			{
				return string.Format("\"{0}\"", field.Replace("\"", "\"\""));
			}
			else
			{
				return field;
			}
		}
	}
}
