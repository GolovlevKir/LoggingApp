using LogApp2.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using System;
using System.Linq;
using System.Threading.Tasks;

namespace LogApp2.Controllers
{
    public class UsersController : Controller
    {
        private readonly Context _context;
        private readonly ILogger<UsersController> _logger;

        public UsersController(Context context, ILogger<UsersController> logger)
        {
            _context = context;
            _logger = logger;
            _logger.LogDebug(1, "Страница контроллера UsersController");
        }
        

        public async Task<IActionResult> Index()
        {
            try
            {
                _logger.LogInformation("Открытие страницы Данные пользователей");
                return View(await _context.Users.ToListAsync());
            }
            catch
            {
                _logger.LogError("Ошибка получения списка пользователей");
                return View(await _context.Users.ToListAsync());
            }

        }

        public IActionResult Create()
        {
            _logger.LogInformation("Открытие страницы Добавление данных пользователей");
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,Name,Age")] Users users)
        {
            if (ModelState.IsValid)
            {
                _context.Add(users);
                _logger.LogDebug("Новый пользователь добавлен");
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            else
                _logger.LogError("Пользователь заполнил не все поля для добавления пользователя");
            return View(users);
        }

        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                _logger.LogWarning("ID ссылки равно null");
                return NotFound();
            }

            var users = await _context.Users.FindAsync(id);
            if (users == null)
            {
                _logger.LogWarning("Ошибка получения данных пользователя по ID");
                return NotFound();
            }
            _logger.LogInformation("Открытие страницы Добавление данных пользователей");
            return View(users);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,Name,Age")] Users users)
        {
            if (id != users.Id)
            {
                _logger.LogWarning("ID не сопоставим с ID пользователя");
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(users);
                    _logger.LogDebug("Данные пользователя изменены");
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException ex)
                {
                    _logger.LogError("Ошибка изменения данных пользователя (" + ex.Message + ")");
                    if (!UsersExists(users.Id))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
                return RedirectToAction(nameof(Index));
            }
            else
                _logger.LogError("Пользователь заполнил не все поля для изменения данных пользователя");
            return View(users);
        }

        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                _logger.LogWarning("ID ссылки равно null");
                return NotFound();
            }

            var users = await _context.Users
                .FirstOrDefaultAsync(m => m.Id == id);
            if (users == null)
            {
                _logger.LogWarning("Ошибка получения данных пользователя по ID");
                return NotFound();
            }
            _logger.LogInformation("Открытие страницы Удаления данных пользователей");
            return View(users);
        }

        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var users = await _context.Users.FindAsync(id);
            try
            {
                _context.Users.Remove(users);
                _logger.LogDebug("Данные пользователя Удалены");
                await _context.SaveChangesAsync();
            }
            catch (Exception ex)
            {
                _logger.LogWarning("Ошибка при удалении (" + ex.Message + ")");
            }

            return RedirectToAction(nameof(Index));
        }

        private bool UsersExists(int id)
        {
            return _context.Users.Any(e => e.Id == id);
        }
    }
}
