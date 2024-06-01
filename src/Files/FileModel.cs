namespace Bau.Libraries.LibHelper.Files;

/// <summary>
///		Clase de tratamiento de un archivo
/// </summary>
public class FileModel
{
	public FileModel(string fileName)
	{
		FileName = fileName;
	}

	/// <summary>
	///		Normaliza la salida de una función
	/// </summary>
	private string? NormalizeOutput(Func<string, string?> function)
	{
		if (!IsEmpty())
			return function(FileName);
		else
			return string.Empty;
	}

	/// <summary>
	///		Comprueba si está vacío
	/// </summary>
	public bool IsEmpty() => string.IsNullOrWhiteSpace(FileName);

	/// <summary>
	///		Nombre de archivo
	/// </summary>
	public string? GetFileName() => NormalizeOutput(Path.GetFileName);

	/// <summary>
	///		Obtiene el directorio
	/// </summary>
	public string? GetDirectory() => NormalizeOutput(Path.GetDirectoryName);

	/// <summary>
	///		Obtiene el nombre de archivo sin extensión
	/// </summary>
	public string? GetFileNameWithoutExtension() => NormalizeOutput(Path.GetFileNameWithoutExtension);

	/// <summary>
	///		Obtiene la extensión
	/// </summary>
	public string? GetExtension() => NormalizeOutput(Path.GetExtension);

	/// <summary>
	///		Combina con un nombre de archivo
	/// </summary>
	public FileModel Combine(string path)
	{
		if (IsEmpty())
			return new FileModel(path);
		else if (string.IsNullOrEmpty(path))
			return new FileModel(FileName);
		else
			return new FileModel(Path.Combine(FileName, path));
	}

	/// <summary>
	///		Modifica el separador
	/// </summary>
	public void UpdateSeparator(char oldSeparator, char newSeparator)
	{
		if (!IsEmpty())
			FileName.Replace(oldSeparator, newSeparator);
	}

	/// <summary>
	///		Modifica el separador para Windows
	/// </summary>
	public void UpdateSeparatorWindows()
	{
		UpdateSeparator('/', '\\');
	}

	/// <summary>
	///		Modifica el separador para Unix
	/// </summary>
	public void UpdateSeparatorUnix()
	{
		UpdateSeparator('\\', '/');
	}

	/// <summary>
	///		Modifica la extensión
	/// </summary>
	public void UpdateExtension(string extension)
	{
		if (!IsEmpty())
		{
			// Normaliza la extensión
			if (!string.IsNullOrWhiteSpace(extension))
			{
				// Quita los espacios
				extension = extension.Trim();
				// Añade el punto inicial
				if (!extension.StartsWith('.'))
					extension = '.' + extension;
				// Cambia la extensión
				FileName = GetFileNameWithoutExtension() + extension;
			}
		}
	}

	/// <summary>
	///		Modifica el nombre conservando la extensión o no
	/// </summary>
	public void UpdateName(string newName, bool preserveExtension)
	{
		if (!IsEmpty())
		{
			if (preserveExtension)
				FileName = GetFileNameWithoutExtension() + '\\' + newName + GetExtension();
			else
				FileName = GetFileNameWithoutExtension() + '\\' + newName;
		}
	}
	
	/// <summary>
	///		Normaliza un nombre de archivo
	/// </summary>
	public void Normalize()
	{ 
		const string ValidChars = " .-,#@";
		string target = string.Empty;

			// Normaliza el nombre
			if (!IsEmpty())
			{
				// Normaliza el nombre de archivo
				foreach (char chr in FileName)
					if (chr == '\\' || chr == '/' || char.IsDigit(chr) || char.IsLetter(chr))
						target += chr;
					else if (ValidChars.Contains(chr))
						target += chr;
				// Quita los puntos iniciales
				while (target.Length > 0 && target.StartsWith('.'))
					target = target[1..];
				// Asigna el nombre de archivo
				if (!string.IsNullOrWhiteSpace(target))
					FileName = target.Trim();
			}
	}

	/// <summary>
	///		Comprueba si es un directorio
	/// </summary>
	public bool IsFolder()
	{
		if (IsEmpty())
			return false;
		else
			return Directory.Exists(FileName);
	}

	/// <summary>
	///		Comprueba si es un archivo
	/// </summary>
	public bool IsFile()
	{
		if (IsEmpty())
			return false;
		else
			return File.Exists(FileName);
	}

	/// <summary>
	///		Comprueba si existe (como archivo o como directorio)
	/// </summary>
	public bool Exists => IsFolder() || IsFile();

	/// <summary>
	///		Nombre de archivo
	/// </summary>
	public string FileName { get; private set; }
}
