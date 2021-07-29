using System;
using System.Linq;
using System.Reflection;
using Xunit;
using FluentAssertions;

using Bau.Libraries.LibHelper.Services.Reflection;
using LibHelperTests.TestModels;
using System.Collections.Generic;

namespace LibHelperTests.Reflection
{
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
					ClientTestModel objClient = new ModelsFactory().CreateClient(index);
					ShopTestModel objShop = new ModelsFactory().CreateShop(index);

						// Obtiene los valores
						service.GetValue(objClient, "ID").Should().Be(objClient.ID);
						service.GetValue(objClient, "Name").Should().Be(objClient.Name);
						service.GetValue(objClient, "Description").Should().Be(objClient.Description);
						// Obtiene los valores
						service.GetValue(objShop, "ID").Should().Be(objShop.ID);
						service.GetValue(objShop, "Name").Should().Be(objShop.Name);
						service.GetValue(objShop, "Description").Should().Be(objShop.Description);
						service.GetValue(objShop, "Budget").Should().Be(objShop.Budget);
						service.GetValue(objShop, "Stock").Should().Be(objShop.Stock);
						service.GetValue(objShop, "DateNew").Should().Be(objShop.DateNew);
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
				service.GetProperty(typeof(ClientTestModel), "ID").Should().NotBeNull();
				service.GetProperty(typeof(ClientTestModel), "Name").Should().NotBeNull();
				service.GetProperty(typeof(ClientTestModel), "Description").Should().NotBeNull();
				// Comprueba que no obtenga propiedades que no existen en los clientes
				service.GetProperty(typeof(ClientTestModel), "ID2").Should().BeNull();
				service.GetProperty(typeof(ClientTestModel), "Name2").Should().BeNull();
				service.GetProperty(typeof(ClientTestModel), "Description2").Should().BeNull();
				// Comprueba las propiedades del objeto
				service.GetProperty(typeof(ShopTestModel), "ID").Should().NotBeNull();
				service.GetProperty(typeof(ShopTestModel), "Name").Should().NotBeNull();
				service.GetProperty(typeof(ShopTestModel), "Description").Should().NotBeNull();
				service.GetProperty(typeof(ShopTestModel), "Budget").Should().NotBeNull();
				service.GetProperty(typeof(ShopTestModel), "Stock").Should().NotBeNull();
				service.GetProperty(typeof(ShopTestModel), "DateNew").Should().NotBeNull();
				// Comprueba las propiedades del objeto
				service.GetProperty(typeof(ShopTestModel), "ID2").Should().BeNull();
				service.GetProperty(typeof(ShopTestModel), "Name2").Should().BeNull();
				service.GetProperty(typeof(ShopTestModel), "Description2").Should().BeNull();
				service.GetProperty(typeof(ShopTestModel), "Budget2").Should().BeNull();
				service.GetProperty(typeof(ShopTestModel), "Stock2").Should().BeNull();
				service.GetProperty(typeof(ShopTestModel), "DateNew2").Should().BeNull();
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
				properties.Count.Should().Be(3);
				ExistsProperty(properties, "ID").Should().Be(true);
				ExistsProperty(properties, "Name").Should().Be(true);
				ExistsProperty(properties, "Description").Should().Be(true);
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
				properties.Count.Should().Be(6);
				ExistsProperty(properties, "ID").Should().BeTrue();
				ExistsProperty(properties, "Name").Should().BeTrue();
				ExistsProperty(properties, "Description").Should().BeTrue();
				ExistsProperty(properties, "Budget").Should().BeTrue();
				ExistsProperty(properties, "Stock").Should().BeTrue();
				ExistsProperty(properties, "DateNew").Should().BeTrue();
		}

		/// <summary>
		///		Comprueba si existe una propiedad en la colección
		/// </summary>
		private bool ExistsProperty(List<PropertyInfo> properties, string name)
		{
			return properties.Any<PropertyInfo>(objProperty => objProperty.Name.Equals(name));
		}
	}
}
