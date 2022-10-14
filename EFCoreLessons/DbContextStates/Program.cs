using DbContextStates;
using Microsoft.EntityFrameworkCore;

Initializer.Build();

using (var _context = new AppDbContext())
{
    #region added state
    var newProduct = new Product { Name = "Klavye2", Price = 45, Stock = 0, Barcode = "kvkvkvkv" };

    Console.WriteLine($"yeni kayıt ilk state--> {_context.Entry(newProduct).State}");

    await _context.AddAsync(newProduct);
    // _context.Entry(newProduct).State = EntityState.Added;// yukarıdaki satır ile aynı görevdedir.

    Console.WriteLine($"yeni kayıt son state--> {_context.Entry(newProduct).State}");
    #endregion

    #region Modified

    var product = await _context.ProductsForState.FirstAsync();
    Console.WriteLine($"güncelleme ilk state--> {_context.Entry(product).State}");

    product.Stock = 111;

    Console.WriteLine($"güncelleme son state--> {_context.Entry(product).State}");
    #endregion


    #region Deleted
    var deleteProduct = await _context.ProductsForState.FirstAsync();
    Console.WriteLine($"silme ilk state--> {_context.Entry(deleteProduct).State}");

     //_context.Remove(deleteProduct);
     _context.Entry(deleteProduct).State = EntityState.Deleted;// yukarıdaki satır ile aynı görevdedir.

    Console.WriteLine($"silme son state--> {_context.Entry(deleteProduct).State}");
    #endregion

    #region Ef core tarafından track edilmeyen datayı güncelleme
    _context.Update(new Product { Id = 2, Name = "KalemManuel", Price = 100, Stock = 53, Barcode = "kmkmkmk" });
    #endregion

    await _context.SaveChangesAsync();
    Console.WriteLine($"silme tamalanınca state--> {_context.Entry(deleteProduct).State}");
    #region Unchanged
    var products = await _context.ProductsForState.ToListAsync();

    products.ForEach(x =>
    {
        var state = _context.Entry(x).State;
        Console.WriteLine(x.Id + "- " + x.Name + " : " + x.Price + " - " + x.Stock + "--> State: " + state);
    });
    #endregion


    Console.ReadLine();
}