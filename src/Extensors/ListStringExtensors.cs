namespace Bau.Libraries.LibHelper.Extensors;

/// <summary>
///		Extensores para <see cref="List{string}"/>
/// </summary>
public static class ListStringExtensors
{
	/// <summary>
	///		Concatena una lista
	/// </summary>
	public static string Concatenate(this List<string> values, string? separator = null)
	{
		string result = string.Empty;

			// Asiga el separador
			if (string.IsNullOrWhiteSpace(separator))
				separator = Environment.NewLine;
			// Añade los errores
			foreach (string error in values)
				result += error + separator;
			// Devuelve la cadena de error
			return result;
	}

	/// <summary>
	///		Comprueba si existe una cadena en la lista
	/// </summary>
	public static bool ExistsIgnoreCase(this List<string> list, string text)
	{
		// Recorre la lista
		foreach (string item in list)
			if ((string.IsNullOrWhiteSpace(item) && string.IsNullOrWhiteSpace(text)) ||
					(!string.IsNullOrWhiteSpace(item) && item.Equals(text, StringComparison.CurrentCultureIgnoreCase)))
				return true;
		// Si ha llegado hasta aquí es porque no ha encontrado el valor
		return false;
	}
}
