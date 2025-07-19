using Bau.Libraries.LibHelper.Files;

namespace LibHelper.Testing.Files;

/// <summary>
///		Pruebas para <see cref="HelperFiles"/>
/// </summary>
public class FilesHelper_Should
{
	/// <summary>
	///		Comprueba que se obtenga correctamente una extensión completa
	/// </summary>
	[Theory]
	[InlineData("C:/A/file.abc", "abc")]
	[InlineData("C:/A/file.abc.def", "abc.def")]
	[InlineData("C:/A/file", "")]
	[InlineData("C:/A.b.c/file.abc", "abc")]
	[InlineData("C:/A.b.c/file.abc.def", "abc.def")]
	[InlineData("C:/A.b.c/file", "")]
	[InlineData("file.abc", "abc")]
	[InlineData("file.abc.def", "abc.def")]
	[InlineData("file", "")]
	[InlineData(".file", "file")]
	public void get_total_extension(string fileName, string result)
	{
		HelperFiles.GetTotalExtension(fileName).ShouldBe(result);
	}

	/// <summary>
	///		Comprueba que se obtenga correctamente un nombre de archivo completo
	/// </summary>
	[Theory]
	[InlineData("C:/A/file.abc", "file")]
	[InlineData("C:/A/file.abc.def", "file")]
	[InlineData("C:/A/file", "file")]
	[InlineData("C:/A.b.c/file.abc", "file")]
	[InlineData("C:/A.b.c/file.abc.def", "file")]
	[InlineData("C:/A.b.c/file", "file")]
	[InlineData("file.abc", "file")]
	[InlineData("file.abc.def", "file")]
	[InlineData("file", "file")]
	[InlineData(".file", "")]
	public void get_total_file(string fileName, string result)
	{
		HelperFiles.GetTotalFileName(fileName).ShouldBe(result);
	}
}
