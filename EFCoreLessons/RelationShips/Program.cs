
using Microsoft.EntityFrameworkCore;
using RelationShips;
using RelationShips.DataAccessLayer;

Initializer.Build();

using (var _context = new AppDbContext())
{

    #region One-Many veri ekleme
    var category = new Category () { Name = "Kitaplar" };
    category.Products.Add(new Product { Name = "Matematik2", Price = 100, Stock = 22, Barcode = "ddddvdrfdf", Category = category });

    _context.Categories.Add(category);
    #endregion

    #region One-One veri ekleme
    var productFeature = new ProductFeature() { Color = "Siyah", Width = 500, Height = 100 };
    var productOneToOne = new Product() { Name = "Fizik Kitabı", Price = 100, Stock = 22, Barcode = "fsdfsd", ProductFeature = productFeature, Category=category };
    _context.Products.Add(productOneToOne);

    #endregion

    #region Many To Many
    var student = new Student() { Name = "Fatih" };
    student.Teachers.Add(new Teacher { Name = "Sinem" });
    student.Teachers.Add(new Teacher { Name = "Şükran" });
    _context.Students.Add(student);
    #endregion


    #region Master table veri silme

    #region Restrict: bu blok çalışmaz çünkü delete modu restrict olarak ayarlandı. Önce category e ait product ları silmelisin.
    //var deletedCategory = _context.Categories.First();
    //_context.Categories.Remove(deletedCategory);
    #endregion

    #region SetNull: productfeature tablosundaki productId alanını null set eder.
    //var deleted = _context.Products.First();
    //_context.Products.Remove(deleted);
    #endregion

    #endregion

    _context.SaveChanges();
    Console.WriteLine("Kaydedildi.");
    Console.ReadLine();
}