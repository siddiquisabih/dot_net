using System;
using System.Linq;
using System.Configuration;
using Microsoft.AspNetCore.Http;
using System.Reflection;
using System.Collections.Generic;
using System.ComponentModel;
using System.Text;

namespace Tatweer.SafeCity.Common.Helpers
{
	public static class CSVHelper
	{


		/// <summary>
		/// Serialize objects to Comma Separated Value (CSV) format [1].
		/// 
		/// Rather than try to serialize arbitrarily complex types with this
		/// function, it is better, given type A, to specify a new type, A'.
		/// Have the constructor of A' accept an object of type A, then assign
		/// the relevant values to appropriately named fields or properties on
		/// the A' object.
		/// </summary>
		public static string ToCSV<T>(this IEnumerable<T> objects, Type type = null, string CsvSeparator = ",")
		{
			StringBuilder output = new StringBuilder();
			var fields =
				from mi in typeof(T).GetMembers(BindingFlags.Public | BindingFlags.Instance | BindingFlags.Static)
				where new[] { MemberTypes.Field, MemberTypes.Property }.Contains(mi.MemberType)

				select mi;

			if (type != null)
			{
				PropertyInfo[] props = type.GetProperties();

				List<string> record = new List<string>();

				foreach (PropertyInfo prop in props)
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
			return String.Join(csvSeparator, record.Select(field => QuoteField(field, csvSeparator)).ToArray());
		}

		static string QuoteField(string field, string csvSeparator)
		{
			if (String.IsNullOrEmpty(field))
			{
				return "\"\"";
			}
			else if (field.Contains(csvSeparator) || field.Contains("\"") || field.Contains("\r") || field.Contains("\n"))
			{
				return String.Format("\"{0}\"", field.Replace("\"", "\"\""));
			}
			else
			{
				return field;
			}
		}
	}
}
