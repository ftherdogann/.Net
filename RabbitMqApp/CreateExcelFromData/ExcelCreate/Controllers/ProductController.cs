using ExcelCreate.Models;
using ExcelCreate.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace ExcelCreate.Controllers
{
    [Authorize]
    public class ProductController : Controller
    {
        private readonly AppDbContext _context;
        private readonly UserManager<IdentityUser> _userManager;
        private readonly RabbitMqPublisher _rabbitMqPublisher;
        public ProductController(AppDbContext context, UserManager<IdentityUser> userManager,RabbitMqPublisher rabbitMqPublisher)
        {
            _context = context;
            _userManager = userManager;
            _rabbitMqPublisher = rabbitMqPublisher;
        }

        public IActionResult Index()
        {
            return View();
        }
        public async Task<IActionResult> CreateProductExcel()
        {
            var user = await _userManager.FindByNameAsync(User.Identity.Name);

            var fileName = $"product-excel-{Guid.NewGuid().ToString().Substring(1, 10)}";

            UserFile userFile = new UserFile()
            {
                UserId = user.Id,
                FileName = fileName ,
                FileStatus = FileStatus.Creating
            };

            await _context.UserFiles.AddAsync(userFile);

            await _context.SaveChangesAsync();

            _rabbitMqPublisher.Publish(new Shared.CreateExcelMessage()
            {
                FileId = userFile.Id             
            });

            TempData["StartCreatingExcel"] = true;
            return RedirectToAction(nameof(Files));
        }

        public async Task<IActionResult> Files()
        {
            var user = await _userManager.FindByNameAsync(User.Identity.Name);

            var userList =  _context.UserFiles.Where(x => x.UserId == user.Id).OrderByDescending(x=>x.Id).ToList();
            return View(userList);
        }
    }
}
