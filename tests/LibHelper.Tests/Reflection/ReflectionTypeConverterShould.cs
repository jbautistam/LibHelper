using System;
using System.Collections.Generic;
using Xunit;
using FluentAssertions;

using Bau.Libraries.LibHelper.Services.Reflection;
using LibHelperTests.TestModels;

namespace LibHelperTests.Reflection
{
	/// <summary>
	///		Pruebas de conversión desde cadenas a objetos utilizando Reflection
	/// </summary>
	public class ReflectionTypeConverterShould
	{   
		/// <summary>
		///		Comprueba el servicio de reflection para obtener los valores de un objeto a partir de una colección 
		/// de clave / valor para el objeto de cliente
		/// </summary>
		[Fact]
		public void convert_client_properties()
		{
			ClientTestModel client = new ModelsFactory().CreateClient(5);
			List<KeyValuePair<string, object>> parameters = new List<KeyValuePair<string, object>>();

				// Genera los parámetros a partir de los valores del cliente
				parameters.Add(new KeyValuePair<string, object>("ID", client.ID));
				parameters.Add(new KeyValuePair<string, object>("Name", client.Name));
				parameters.Add(new KeyValuePair<string, object>("Description", client.Description));
				// Genera un objeto de cliente a partir de las propiedades y comprueba que el resultado sea igual que el inicial
				AssertCompareClients(client, GetConverter().Convert<ClientTestModel>(parameters));
		}

		/// <summary>
		///		Comprueba el servicio de reflection para obtener los valores de un objeto a partir de una colección 
		/// de clave / valor para el objeto de tienda
		/// </summary>
		[Fact]
		public void convert_shop_properties()
		{
			ShopTestModel shop = new ModelsFactory().CreateShop(23);
			List<KeyValuePair<string, object>> parameters = new List<KeyValuePair<string, object>>();

				// Genera los parámetros a partir de los valores del cliente
				parameters.Add(new KeyValuePair<string, object>("ID", shop.ID));
				parameters.Add(new KeyValuePair<string, object>("Name", shop.Name));
				parameters.Add(new KeyValuePair<string, object>("Description", shop.Description));
				parameters.Add(new KeyValuePair<string, object>("Budget", shop.Budget));
				parameters.Add(new KeyValuePair<string, object>("Stock", shop.Stock));
				parameters.Add(new KeyValuePair<string, object>("DateNew", shop.DateNew));
				// Genera un objeto de tienda a partir de las propiedades y comprueba que el resultado sea igual que el inicial
				AssertCompareShops(shop, GetConverter().Convert<ShopTestModel>(parameters));
		}

		/// <summary>
		///		Comprueba la conversión de las propiedades de un objeto de tipo cliente a una colección de parámetros
		/// </summary>
		[Fact]
		public void get_client_properties()
		{
			ClientTestModel client = new ModelsFactory().CreateClient(30);
			List<KeyValuePair<string, object>> parameters;

				// Obtiene los parámetros
				parameters = GetConverter().GetParameters(client);
				// Comprueba los valores
				SearchValue(parameters, "ID").Should().Be(client.ID);
				SearchValue(parameters, "Name").Should().Be(client.Name);
				SearchValue(parameters, "Description").Should().Be(client.Description);
		}

		/// <summary>
		///		Comprueba la conversión de las propiedades de un objeto de tipo tienda a una colección de parámetros
		/// </summary>
		[Fact]
		public void get_shop_properties()
		{
			ShopTestModel shop = new ModelsFactory().CreateShop(30);
			List<KeyValuePair<string, object>> parameters;

				// Obtiene los parámetros
				parameters = GetConverter().GetParameters(shop);
				// Comprueba los valores
				SearchValue(parameters, "ID").Should().Be(shop.ID);
				SearchValue(parameters, "Name").Should().Be(shop.Name);
				SearchValue(parameters, "Description").Should().Be(shop.Description);
				SearchValue(parameters, "Budget").Should().Be(shop.Budget);
				SearchValue(parameters, "Stock").Should().Be(shop.Stock);
				SearchValue(parameters, "DateNew").Should().Be(shop.DateNew);
		}

		/// <summary>
		///		Comprueba la conversión de las propiedades de un objeto de tipo tienda a una colección de parámetros
		/// </summary>
		[Fact]
		public void convert_with_not_equal_types()
		{
			List<KeyValuePair<string, object>> parameters = new List<KeyValuePair<string, object>>();

				// Crea una colección de parámetros con valores "incompatibles", por ejemplo un double cuando es entero
				parameters.Add(new KeyValuePair<string, object>("ID", 18.3));
				parameters.Add(new KeyValuePair<string, object>("Name", 20));
				parameters.Add(new KeyValuePair<string, object>("Description", "Description x"));
				parameters.Add(new KeyValuePair<string, object>("Budget", 4));
				parameters.Add(new KeyValuePair<string, object>("Stock", 3.5));
				parameters.Add(new KeyValuePair<string, object>("DateNew", "2016-3-17"));
				// Convierte el objeto
				ShopTestModel shop = GetConverter().Convert<ShopTestModel>(parameters);
				// Comprueba los valores
				shop.ID.Should().Be(18);
				shop.Name.Should().Be("20");
				shop.Description.Should().Be("Description x");
				shop.Budget.Should().Be(4);
				shop.Stock.Should().Be(3);
				shop.DateNew.Date.Should().Be(new DateTime(2016, 3, 17));
		}

		/// <summary>
		///		Compara objetos de tipos equivalentes
		/// </summary>
		[Theory]
		[InlineData(0.0, 0, true)]
		[InlineData(1.0, 1, true)]
		[InlineData(1234, 1234, true)]
		[InlineData(1.9, 1.9, true)]
		[InlineData(0.0, 1, false)]
		[InlineData(1.0, 3, false)]
		[InlineData(12.34, 12, false)]
		[InlineData(2.9, 1.9, false)]
		public void compare_objects_equivalent_types(object a, object b, bool result)
		{
			ReflectionTypeConverter converter = GetConverter();

				converter.CompareObjectValues(a, b).Should().Be(result);
		}

		/// <summary>
		///		Busca un valor en la colección de parámetros por nombre
		/// </summary>
		private object SearchValue(List<KeyValuePair<string, object>> parameters, string key)
		{ 
			// Busca un elemento en la colección
			foreach (KeyValuePair<string, object> parameter in parameters)
				if (parameter.Key == key)
					return parameter.Value;
			// Si ha llegado hasta aquí es porque no ha encontrado nada
			return null;
		}

		/// <summary>
		///		Obtiene un conversor
		/// </summary>
		private ReflectionTypeConverter GetConverter()
		{
			return new ReflectionTypeConverter(new ReflectionService());
		}

		/// <summary>
		///		Compara los datos de dos objetos de cliente
		/// </summary>
		private void AssertCompareClients(ClientTestModel first, ClientTestModel second)
		{
			first.ID.Should().Be(second.ID);
			first.Name.Should().Be(second.Name);
			first.Description.Should().Be(second.Description);
		}

		/// <summary>
		///		Compara los datos de dos objetos de tienda
		/// </summary>
		private void AssertCompareShops(ShopTestModel first, ShopTestModel second)
		{
			first.ID.Should().Be(second.ID);
			first.Name.Should().Be(second.Name);
			first.Description.Should().Be(second.Description);
			first.Budget.Should().BeApproximately(second.Budget, 0.001);
			first.Stock.Should().Be(second.Stock);
			first.DateNew.Date.Should().Be(second.DateNew.Date);
		}
	}
}
