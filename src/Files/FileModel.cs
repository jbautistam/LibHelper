namespace Bau.Libraries.LibHelper.Files;

/// <summary>
///		Clase de tratamiento de un archivo
/// </summary>
public class FileModel
{
	// Variables privadas
	private string _fileName = default!;
	private char _separator = '\\';

	public FileModel(string folder, string? file = null)
	{
		if (!string.IsNullOrWhiteSpace(folder))
		{
			if (folder.Contains('/'))
				Separator = '/';
		}
		FileName = Combine(folder, file);
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
	public FileModel Combine(string file) => new(FileName, file);

	/// <summary>
	///		Combina con un nombre de archivo
	/// </summary>
	private string Combine(string folder, string? file)
	{
		if (string.IsNullOrWhiteSpace(file))
			return folder;
		else if (string.IsNullOrWhiteSpace(folder))
			return file;
		else
		{
			string? actualPath = folder;

				// Normaliza el archivo
				file = file.Replace('/', '\\');
				// Quita los directorios ..\
				while (!string.IsNullOrWhiteSpace(file) && file.StartsWith("..\\"))
				{
					// Quita el directorio final
					if (!string.IsNullOrWhiteSpace(actualPath))
						actualPath = Path.GetDirectoryName(actualPath);
					// Quita el comienzo de la cadena
					file = file[3..];
				}
				// Devuelve la combinación de archivos
				if (string.IsNullOrWhiteSpace(actualPath))
					return file;
				else
					return Path.Combine(actualPath, file);
		}
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
				FileName = Path.Combine(Path.GetDirectoryName(FileName)!, GetFileNameWithoutExtension() + extension);
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
				FileName = Combine(GetDirectory()!, newName + GetExtension());
			else
				FileName = Combine(GetDirectory()!, newName);
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
	public string FileName 
	{ 
		get
		{
			if (string.IsNullOrWhiteSpace(_fileName))
				return string.Empty;
			else if (Separator == '/')
				return _fileName.Replace('\\', '/');
			else
				return _fileName.Replace('/', '\\');
		}
		private set { _fileName = value; }
	}

	/// <summary>
	///		Separador
	/// </summary>
	public char Separator 
	{ 
		get { return _separator; }
		set { _separator = value; }
	}
}
