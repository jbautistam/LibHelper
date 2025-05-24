using System.Runtime.InteropServices;

/// <summary>
///		Operaciones de archivos utilizando la API de Windows
/// </summary>
public static class FileOperationAPIWrapper
{
	/// <summary>
	///		Flags del método SHFileOperation
	/// </summary>
	[Flags]
	public enum FileOperationFlags : ushort
	{
		/// <summary>No mostrar un cuadro de diálogo durante el proceso</summary>
		FOF_SILENT = 0x0004,
		/// <summary>No preguntar al usuario que confirme la selección</summary>
		FOF_NOCONFIRMATION = 0x0010,
		/// <summary>Borra el archivo enviándolo a la papelera de reciclaje</summary>
		FOF_ALLOWUNDO = 0x0040,
		/// <summary>No muestra los nombres de los archivos o carpetas que se están eliminando</summary>
		FOF_SIMPLEPROGRESS = 0x0100,
		/// <summary>Suprime los errores si hay alguno</summary>
		FOF_NOERRORUI = 0x0400,
		/// <summary>Avisa si los archivos son demasiado grandes para la papelera de reciclaje</summary>
		FOF_WANTNUKEWARNING = 0x4000,
	}

	/// <summary>
	///		Tipo de operación de SHFileOperation
	/// </summary>
	public enum FileOperationType : uint
	{
		/// <summary>Mueve los archivos</summary>
		FO_MOVE = 0x0001,
		/// <summary>Copia los archivos</summary>
		FO_COPY = 0x0002,
		/// <summary>Borra o recicla los archivos (depende de <see cref="FileOperationFlags"/></summary>
		FO_DELETE = 0x0003,
		/// <summary>Renombra los archivos</summary>
		FO_RENAME = 0x0004,
	}

	/// <summary>
	/// SHFILEOPSTRUCT: estructura del método SHFileOperation de la api
	/// </summary>
	[StructLayout(LayoutKind.Sequential, CharSet = CharSet.Auto)]
	private struct SHFILEOPSTRUCT
	{
		public IntPtr hwnd;
		[MarshalAs(UnmanagedType.U4)]
		public FileOperationType wFunc;
		public string pFrom;
		public string pTo;
		public FileOperationFlags fFlags;
		[MarshalAs(UnmanagedType.Bool)]
		public bool fAnyOperationsAborted;
		public IntPtr hNameMappings;
		public string lpszProgressTitle;
	}

	/// <summary>
	///		Método de la api para tratamiento de archivos
	/// </summary>
	[DllImport("shell32.dll", CharSet = CharSet.Auto)]
	private static extern int SHFileOperation(ref SHFILEOPSTRUCT FileOp);

	/// <summary>
	///		Envía un archivo a la papelera de reciclaje
	/// </summary>
	public static bool MoveToRecycleBin(string path, FileOperationFlags flags)
	{
		try
		{
			SHFILEOPSTRUCT fs = new()
									{
										wFunc = FileOperationType.FO_DELETE,
										pFrom = path + '\0' + '\0',
										fFlags = FileOperationFlags.FOF_ALLOWUNDO | flags
									};

				// Llama a la API
				SHFileOperation(ref fs);
				// Indica que se ha borrado correctamente
				return true;
		}
		catch
		{
			return false;
		}
	}

	/// <summary>
	///		Envía un archivo a la papelera de reciclaje. Si se indica que muestre las advertencias (warnings = true), mostrará una
	///		avertencia si los archivos son demasiado grandes para la papelera de reciclaje
	/// </summary>
	public static bool MoveToRecycleBin(string path, bool warnings = false)
	{
		if (warnings)
			return MoveToRecycleBin(path, FileOperationFlags.FOF_NOCONFIRMATION | FileOperationFlags.FOF_WANTNUKEWARNING);
		else
			return MoveToRecycleBin(path, FileOperationFlags.FOF_NOCONFIRMATION | FileOperationFlags.FOF_NOERRORUI | FileOperationFlags.FOF_SILENT);
	}
}