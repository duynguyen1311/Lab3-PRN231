using BusinessObjects;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccess
{
	public class ProductDAO
	{
		public static List<Product>? GetProducts()
		{
			List<Product>? listProducts = null;
			try
			{
				using(var context = new MyDbContext())
				{
					listProducts = context.Products.Include(i => i.Category).AsNoTracking().ToList();
				}
			}
			catch (Exception e)
			{
				throw new Exception(e.Message);
			}
			return listProducts;
		}

        public static List<Product> SearchProducts(string name)
        {
            List<Product>? listProducts = null;
            try
            {
                using (var context = new MyDbContext())
                {
					if(name == null) listProducts = context.Products.AsNoTracking().ToList();
					else listProducts = context.Products.Where(i => !string.IsNullOrEmpty(i.ProductName) && i.ProductName.Contains(name)).AsNoTracking().ToList();
                }
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
            return listProducts;
        }

        public static Product FindProductById(int prodId) 
		{
			Product p = new Product();
			try
			{
				using(var context = new MyDbContext())
				{
					p = context.Products.SingleOrDefault(x => x.ProductId== prodId);
				}
			}
			catch (Exception e)
			{
				throw new Exception(e.Message);
			}
			return p;
		}

		public static void SaveProduct(Product p)
		{
			try
			{
				using(var context = new MyDbContext())
				{
					context.Products.Add(p);
					context.SaveChanges();
				}
			}
			catch (Exception e)
			{
				throw new Exception(e.Message);
			}
		}

		public static void UpdateProduct(Product p)
		{
			try
			{
				using (var context = new MyDbContext())
				{
					context.Entry<Product>(p).State = Microsoft.EntityFrameworkCore.EntityState.Modified;
					context.SaveChanges();
				}
			}
			catch (Exception e)
			{
				throw new Exception(e.Message);
			}
		}

        public static void DeleteProduct(Product p)
        {
            try
            {
                using (var context = new MyDbContext())
                {
                    var p1 = context.Products.SingleOrDefault(
                        c => c.ProductId == p.ProductId);
                    context.Products.Remove(p1);
                    context.SaveChanges();
                }
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }
    }
}
