using System;

namespace Bau.Libraries.LibHelper.Extensors
{
	/// <summary>
	///		Conversor de objetos a cadenas
	/// </summary>
	public static class ConversorToString
	{
		/// <summary>
		///		Obtiene el valor de una cadena
		/// </summary>
		public static string Convert(object value, string dateMask = "yyyy-MM-dd HH:mm:ss", string trueValue = "1", string falseValue = "0")
		{
			switch (value)
			{
				case null:
					return string.Empty;
				case DateTime dateValue:
					return string.Format(dateMask, dateValue);
				case int intValue:
					return intValue.ToString(System.Globalization.CultureInfo.InvariantCulture);
				case long longValue:
					return longValue.ToString(System.Globalization.CultureInfo.InvariantCulture);
				case short shortValue:
					return shortValue.ToString(System.Globalization.CultureInfo.InvariantCulture);
				case byte byteValue:
					return byteValue.ToString(System.Globalization.CultureInfo.InvariantCulture);
				case float floatValue:
					return floatValue.ToString(System.Globalization.CultureInfo.InvariantCulture);
				case double doubleValue:
					return doubleValue.ToString(System.Globalization.CultureInfo.InvariantCulture);
				case decimal decimalValue:
					return decimalValue.ToString(System.Globalization.CultureInfo.InvariantCulture);
				case bool boolValue:
					if (boolValue)
						return trueValue;
					else
						return falseValue;
				case string stringValue:
					return stringValue;
				default:
					return value?.ToString();
			}
		}
	}
}
