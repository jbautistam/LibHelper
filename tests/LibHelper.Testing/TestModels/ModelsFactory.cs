namespace LibHelperTests.TestModels;

/// <summary>
///		Clase de generación de modelos de prueba
/// </summary>
public class ModelsFactory
{
	/// <summary>
	///		Crea una colección de <see cref="ClientTestModel"/>
	/// </summary>
	public List<ClientTestModel> CreateClients(int number)
	{
		List<ClientTestModel> clients = [];

			// Crea la colección
			for (int index = 0; index < number; index++)
				clients.Add(CreateClient(index));
			// Devuelve la colección
			return clients;
	}

	/// <summary>
	///		Crea un <see cref="ClientTestModel"/>
	/// </summary>
	public ClientTestModel CreateClient(int id) => new ClientTestModel(id);

	/// <summary>
	///		Crea una colección de <see cref="ShopTestModel"/>
	/// </summary>
	public List<ShopTestModel> CreateShops(int number)
	{
		List<ShopTestModel> clients = [];

			// Crea la colección
			for (int index = 0; index < number; index++)
				clients.Add(new ShopTestModel(index));
			// Devuelve la colección
			return clients;
	}

	/// <summary>
	///		Crea un <see cref="ShopTestModel"/>
	/// </summary>
	public ShopTestModel CreateShop(int id) => new ShopTestModel(id);
}
