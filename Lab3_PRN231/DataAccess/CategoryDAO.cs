using BusinessObjects;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccess
{
	public class CategoryDAO
	{
		public static List<Category> GetCategories()
		{
			var ListCategories = new List<Category>();
			try
			{
				using (var context = new MyDbContext())
				{
					ListCategories = context.Categories.ToList();
				}
			}
			catch (Exception e)
			{
				throw new Exception(e.Message);
			}
			return ListCategories;
		}
	}
}
