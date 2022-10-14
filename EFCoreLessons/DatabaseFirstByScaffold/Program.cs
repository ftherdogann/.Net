using DatabaseFirstByScaffold.Models;
using Microsoft.EntityFrameworkCore;

using (var context = new EFCoreDatebaseFirstDbContext())
{
    var products = await context.Products.ToListAsync();

    products.ForEach(x =>
    {
        Console.WriteLine(x.Id + "- " + x.Name + " : " + x.Price + " - " + x.Stock);
    });
    Console.ReadLine();
}
