namespace LibHelperTests.TestModels;

/// <summary>
///		Clase base
/// </summary>
public class BaseTestModel
{
	public BaseTestModel(int? id = null)
	{
		ID = id ?? 0;
		Name = $"Name {ID}";
		Description = $"Description {ID}";
	}

	/// <summary>
	///		Código
	/// </summary>
	public int ID { get; set; }

	/// <summary>
	///		Nombre
	/// </summary>
	public string Name { get; set; }

	/// <summary>
	///		Descripción
	/// </summary>
	public string Description { get; set; }
}
