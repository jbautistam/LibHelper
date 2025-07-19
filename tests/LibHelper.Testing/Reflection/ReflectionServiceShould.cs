using System.Reflection;

using Bau.Libraries.LibHelper.Services.Reflection;
using LibHelperTests.TestModels;

namespace LibHelperTests.Reflection;

/// <summary>
///		Clase de pruebas para <see cref="ReflectionService"/>
/// </summary>
public class ReflectionServiceShould
{
	/// <summary>
	///		Comprueba el servicio de reflection para obtener los valores de un objeto
	/// </summary>
	[Fact]
	public void get_correct_values_from_objects_properties()
	{
		ReflectionService service = new ReflectionService();

			// Comprueba clientes y tiendas
			for (int index = 0; index < 10; index++)
			{
				ClientTestModel client = new ModelsFactory().CreateClient(index);
				ShopTestModel shop = new ModelsFactory().CreateShop(index);

					// Obtiene los valores
					service.GetValue(client, "ID").ShouldBe(client.ID);
					service.GetValue(client, "Name").ShouldBe(client.Name);
					service.GetValue(client, "Description").ShouldBe(client.Description);
					// Obtiene los valores
					service.GetValue(shop, "ID").ShouldBe(shop.ID);
					service.GetValue(shop, "Name").ShouldBe(shop.Name);
					service.GetValue(shop, "Description").ShouldBe(shop.Description);
					service.GetValue(shop, "Budget").ShouldBe(shop.Budget);
					service.GetValue(shop, "Stock").ShouldBe(shop.Stock);
					service.GetValue(shop, "DateNew").ShouldBe(shop.DateNew);
			}
	}

	/// <summary>
	///		Comprueba el servicio de reflection para obtener una propiedad de un objeto
	/// </summary>
	[Fact]
	public void get_property_from_object()
	{
		ReflectionService service = new ReflectionService();

			// Comprueba las propiedades de los clientes
			service.GetProperty(typeof(ClientTestModel), "ID").ShouldNotBeNull();
			service.GetProperty(typeof(ClientTestModel), "Name").ShouldNotBeNull();
			service.GetProperty(typeof(ClientTestModel), "Description").ShouldNotBeNull();
			// Comprueba que no obtenga propiedades que no existen en los clientes
			service.GetProperty(typeof(ClientTestModel), "ID2").ShouldBeNull();
			service.GetProperty(typeof(ClientTestModel), "Name2").ShouldBeNull();
			service.GetProperty(typeof(ClientTestModel), "Description2").ShouldBeNull();
			// Comprueba las propiedades del objeto
			service.GetProperty(typeof(ShopTestModel), "ID").ShouldNotBeNull();
			service.GetProperty(typeof(ShopTestModel), "Name").ShouldNotBeNull();
			service.GetProperty(typeof(ShopTestModel), "Description").ShouldNotBeNull();
			service.GetProperty(typeof(ShopTestModel), "Budget").ShouldNotBeNull();
			service.GetProperty(typeof(ShopTestModel), "Stock").ShouldNotBeNull();
			service.GetProperty(typeof(ShopTestModel), "DateNew").ShouldNotBeNull();
			// Comprueba las propiedades del objeto
			service.GetProperty(typeof(ShopTestModel), "ID2").ShouldBeNull();
			service.GetProperty(typeof(ShopTestModel), "Name2").ShouldBeNull();
			service.GetProperty(typeof(ShopTestModel), "Description2").ShouldBeNull();
			service.GetProperty(typeof(ShopTestModel), "Budget2").ShouldBeNull();
			service.GetProperty(typeof(ShopTestModel), "Stock2").ShouldBeNull();
			service.GetProperty(typeof(ShopTestModel), "DateNew2").ShouldBeNull();
	}

	/// <summary>
	///		Comprueba el servicio de reflection para obtener una colección de propiedades de un objeto
	/// </summary>
	[Fact]
	public void get_collection_properties_from_client_test_object()
	{
		ReflectionService service = new ReflectionService();
		List<PropertyInfo> properties = service.GetProperties(typeof(ClientTestModel));

			// Comprueba los clientes
			properties.Count.ShouldBe(3);
			ExistsProperty(properties, "ID").ShouldBe(true);
			ExistsProperty(properties, "Name").ShouldBe(true);
			ExistsProperty(properties, "Description").ShouldBe(true);
	}

	/// <summary>
	///		Comprueba el servicio de reflection para obtener una colección de propiedades de un objeto
	/// </summary>
	[Fact]
	public void get_collection_properties_from_shop_test_object()
	{
		ReflectionService service = new ReflectionService();
		List<PropertyInfo> properties = service.GetProperties(typeof(ShopTestModel));

			// Comprueba las propiedades del objeto
			properties.Count.ShouldBe(6);
			ExistsProperty(properties, "ID").ShouldBeTrue();
			ExistsProperty(properties, "Name").ShouldBeTrue();
			ExistsProperty(properties, "Description").ShouldBeTrue();
			ExistsProperty(properties, "Budget").ShouldBeTrue();
			ExistsProperty(properties, "Stock").ShouldBeTrue();
			ExistsProperty(properties, "DateNew").ShouldBeTrue();
	}

	/// <summary>
	///		Comprueba si existe una propiedad en la colección
	/// </summary>
	private bool ExistsProperty(List<PropertyInfo> properties, string name) => properties.Any(property => property.Name.Equals(name));
}
