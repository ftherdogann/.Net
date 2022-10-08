using DbContextProperties;
using Microsoft.EntityFrameworkCore;

Initializer.Build();

using (var _context = new AppDbContext())
{

    #region Default data çekme --track etmeme özelliği
    // AsNoTracking ef core un dataları track etmemesini sağlıyor.Memory yönetimi için
    var products = await _context.ProductsItem.AsNoTracking().ToListAsync();

    products.ForEach(x =>
    {

        var state = _context.Entry(x).State;
        Console.WriteLine(x.Id + "- " + x.Name + " : " + x.Price + " - " + x.Stock + "--> State: " + state);
    });
    #endregion

    #region CreatedDate SaveChanges metodu override edilerek tüm kayıtlara verildi.Merkezi yapı kuruldu
    _context.ProductsItem.Add(new Product { Name = "Masa", Price = 4555, Stock = 111, Barcode = "msmsms" });
    _context.ProductsItem.Add(new Product { Name = "Sandalye", Price = 111, Stock = 222, Barcode = "ssdsdssd" });
    _context.ProductsItem.Add(new Product { Name = "Yastık", Price = 32, Stock = 655, Barcode = "ysysysy" });
    _context.SaveChanges();
    #endregion

    #region ContextId property
    Console.WriteLine($"Context Id: {_context.ContextId}");
    #endregion

    #region Database property --veritabanı işlemlerini yapmak için kullanılır.
    var timeout =_context.Database.GetCommandTimeout();
    Console.WriteLine($"Command Timeout: {timeout}");
    #endregion



    #region DbSet properties
    //ilk kaydı getirir.kayıt yoksa hata alır.
    var firstItem =_context.ProductsItem.First(x=>x.Id>100);

    //ilk kaydı getirir.kayıt yoksa default değer döner. Default değer değiştirilebilir.
    var firstDefaultItem = _context.ProductsItem.FirstOrDefault(x => x.Id == 100,new Product { Name="Boş Ürün"});

    //şayet eşleşen tek kayıt varsa çalışır.Birden fazla kayıtta yada kayıt yoksa hata fırlatır.
    var singleItem = _context.ProductsItem.Single(x=>x.Stock>1);

    //birden fazla kayıt varsa hata alır.Kayıt yoksa default değeri döner.
    var singleDefaultItem = _context.ProductsItem.SingleOrDefault(x => x.Stock > 1,new Product { Name="Boş Ürün"});

    //koşulu sağlayan dataları döner.
    var whereItem = _context.ProductsItem.Where(x=>x.Price>100 && x.CreatedDate==null).ToList();

    // find primary keye göre ilgili datayı getirir. Alternatif olarak single ve find kullanılabilir.Bulamazsa null döner.
    var findItem = _context.ProductsItem.Find(10);
    var findfirstItem = _context.ProductsItem.First(x => x.Id ==10);
    var singlefirstItem = _context.ProductsItem.Single(x => x.Id == 10);
    #endregion

    Console.ReadLine();
}