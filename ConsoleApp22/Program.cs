using ConsoleApp22.Context;
using ConsoleApp22.Entities;
using Microsoft.EntityFrameworkCore;

class Program
{
    static void Main(string[] args)
    {
        using var context = new DbContext();

        Console.WriteLine("Products:");
        GetAllProducts(context);

        Console.WriteLine("Products for Id");
        GetProductById(1, context);

        Console.WriteLine("Add Product");
        InsertProduct(new Product { Name = "New Product", Price = 100, CategoryId = 1 }, context);
        GetAllProducts(context);

        Console.WriteLine("Delete Product");
        DeleteProduct(1, context);
        GetAllProducts(context);

        Console.WriteLine("Update Product");
        UpdateProduct(2, new Product { Name = "New Product", Price = 50, CategoryId = 1 }, context);
        GetAllProducts(context);
    }

    static void GetAllProducts(AppDbContext context)
    {
        var products = context.Products.Include(p => p.Category).ToList();
        foreach (var product in products)
        {
            Console.WriteLine($"Product: {product.Name}, Price: {product.Price}, Category: {product.Category.CategoryName}");
        }
    }

    static void GetProductById(int id, AppDbContext context)
    {
        var product = context.Products.Include(p => p.Category).FirstOrDefault(p => p.Id == id);
        if (product != null)
        {
            Console.WriteLine($"Product: {product.Name}, Price: {product.Price}, Category: {product.Category.CategoryName}");
        }
        else
        {
            Console.WriteLine("dont found");
        }
    }

    static void InsertProduct(Product product, AppDbContext context)
    {
        context.Products.Add(product);
        context.SaveChanges();
        Console.WriteLine("New Product added");
    }

    static void DeleteProduct(int id, AppDbContext context)
    {
        var product = context.Products.FirstOrDefault(p => p.Id == id);
        if (product != null)
        {
            context.Products.Remove(product);
            context.SaveChanges();
            Console.WriteLine("Product deleted");
        }
        else
        {
            Console.WriteLine("dont found Product");
        }
    }

    static void UpdateProduct(int id, Product updatedProduct, AppDbContext context)
    {
        var product = context.Products.FirstOrDefault(p => p.Id == id);
        if (product != null)
        {
            product.Name = updatedProduct.Name;
            product.Price = updatedProduct.Price;
            product.CategoryId = updatedProduct.CategoryId;
            context.SaveChanges();
            Console.WriteLine("Product Update");
        }
        else
        {
            Console.WriteLine("Product dont found");
        }
    }
}