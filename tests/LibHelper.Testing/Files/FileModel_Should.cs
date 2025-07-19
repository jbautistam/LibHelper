using Bau.Libraries.LibHelper.Files;

namespace LibHelper.Testing.Files;

/// <summary>
///		Pruebas para <see cref="FileModel"/>
/// </summary>
public class FileModel_Should
{
	/// <summary>
	///		Comprueba que se obtenga correctamente un separador
	/// </summary>
	[Theory]
	[InlineData("C:/A/file.abc", '/')]
	[InlineData("C:\\A\\file.abc", '\\')]
	public void get_separator(string fileName, char separator)
	{
		FileModel file = new(fileName);

			file.Separator.ShouldBe(separator);
	}

	/// <summary>
	///		Comprueba que se obtenga correctamente un nombre de archivo
	/// </summary>
	[Theory]
	[InlineData("C:/A/file.abc")]
	public void create_file(string fileName)
	{
		FileModel file = new(fileName);

			file.FileName.ShouldBe(fileName);
	}

	/// <summary>
	///		Comprueba que se obtenga correctamente la carpeta
	/// </summary>
	[Theory]
	[InlineData("C:/A/file.abc", "C:\\A")]
	public void get_folder(string fileName, string result)
	{
		FileModel file = new(fileName);

			file.GetDirectory().ShouldBe(result);
	}

	/// <summary>
	///		Comprueba que se cree correctamente un nombre archivo combinado
	/// </summary>
	[Theory]
	[InlineData("C:/A", "file.txt", "C:/A/file.txt")]
	[InlineData("C:\\A", "file.txt", "C:\\A\\file.txt")]
	[InlineData("C:\\A", "../file.txt", "C:\\file.txt")]
	[InlineData("C:/A", "../file.txt", "C:/file.txt")]
	[InlineData("C:\\A\\B\\C", "../../file.txt", "C:\\A\\file.txt")]
	public void combine(string folder, string fileName, string result)
	{
		FileModel file = new(folder, fileName);
		FileModel file2 = new(folder);

			// Prueba el resultado del constructor
			file.FileName.ShouldBe(result);
			// Prueba el resultado de la combinación en otro objeto
			// Debería ser lo mismo, por eso se hace en el mismo método de prueba
			file2.Combine(fileName).FileName.ShouldBe(result);
	}

	/// <summary>
	///		Comprueba que se cree correctamente un nombre archivo combinado
	/// </summary>
	[Theory]
	[InlineData("C:/A/file.txt", "pas", "C:/A/file.pas")]
	[InlineData("C:/A/file.txt", ".pas", "C:/A/file.pas")]
	[InlineData("C:\\A\\file.txt", "pas", "C:\\A\\file.pas")]
	[InlineData("C:\\A\\B\\C\\file.txt", ".pas", "C:\\A\\B\\C\\file.pas")]
	public void update_extension(string fileName, string extension, string result)
	{
		FileModel file = new(fileName);

			// Cambia la extensión
			file.UpdateExtension(extension);
			// Comprueba el resultado
			file.FileName.ShouldBe(result);
	}

	/// <summary>
	///		Comprueba que se modifique correctamente el nombre de archiov
	/// </summary>
	[Theory]
	[InlineData("C:/A/file.txt", "pepito", true, "C:/A/pepito.txt")]
	[InlineData("C:/A/file.txt", "pepito.pas", true, "C:/A/pepito.pas.txt")]
	[InlineData("C:/A/file.txt", "pepito.pas", false, "C:/A/pepito.pas")]
	public void update_file_name(string fileName, string newFileName, bool preserveExtension, string result)
	{
		FileModel file = new(fileName);

			// Cambia la extensión
			file.UpdateName(newFileName, preserveExtension);
			// Comprueba el resultado
			file.FileName.ShouldBe(result);
	}
}