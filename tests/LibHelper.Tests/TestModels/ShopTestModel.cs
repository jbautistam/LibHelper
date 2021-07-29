using System;

namespace LibHelperTests.TestModels
{
	/// <summary>
	///		Modelo para pruebas de una tienda
	/// </summary>
	public class ShopTestModel : BaseTestModel
	{
		public ShopTestModel() : this(null) { }

		public ShopTestModel(int? id = null) : base(id)
		{
			Budget = (ID + 1) * 0.05;
			Stock = (ID + 1) * 157;
			DateNew = DateTime.Now.AddDays(ID + 1);
		}

		/// <summary>
		///		Presupuesto
		/// </summary>
		public double Budget { get; set; }

		/// <summary>
		///		Stock
		/// </summary>
		public int Stock { get; set; }

		/// <summary>
		///		Fecha de alta
		/// </summary>
		public DateTime DateNew { get; set; }
	}
}
