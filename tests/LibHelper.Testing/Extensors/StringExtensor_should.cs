using Bau.Libraries.LibHelper.Extensors;

namespace LibHelperTests.Extensors;

/// <summary>
///		Comprueba los métodos de <see cref="StringExtensor"/>
/// </summary>
public class StringExtensor_should
{
	/// <summary>
	///		Borra una parte de una cadena
	/// </summary>
	[Theory]
	[InlineData("Hola mundo", 3, 5, "Hondo")]
	[InlineData("Hola mundo", 0, 5, "mundo")]
	[InlineData("Hola mundo", 0, 20, "")]
	[InlineData("Hola", 1, 3, "H")]
	public void delete_part(string source, int start, int length, string result)
	{
		string value = source.Delete(start, length);

			// Comprueba el resultado
			value.ShouldBe(result);
	}
}
