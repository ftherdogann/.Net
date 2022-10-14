using DatebaseFirst.DataAccessLayer;
using Microsoft.EntityFrameworkCore;

DbContextInitializer.Build();

using (var _context = new AppDbContext())
{
    var products = await _context.Products.ToListAsync();

    products.ForEach(x =>
    {
        Console.WriteLine(x.Id+"- "+x.Name + " : " + x.Price+ " - "+x.Stock);
    });
    Console.ReadLine();
}
