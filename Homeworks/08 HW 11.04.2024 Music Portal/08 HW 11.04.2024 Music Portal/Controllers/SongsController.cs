using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using _08_HW_11._04._2024_Music_Portal.Models;
using _08_HW_11._04._2024_Music_Portal.Repositories;

namespace _08_HW_11._04._2024_Music_Portal.Controllers
{
    public class SongsController : Controller
    {
        //private readonly MusicPortalContext _context;
        ISongsRepository _songsRepository;
        IUsersRepository _usersRepository; 
        IArtistsRepository _artistsRepository;

        // IWebHostEnvironment предоставляет информацию об окружении, в котором запущено приложение
        IWebHostEnvironment _appEnvironment;

        //public SongsController(MusicPortalContext context, IWebHostEnvironment appEnvironment)
        //{
        //    _context = context;
        //    _appEnvironment = appEnvironment;
        //}

        public SongsController(ISongsRepository songsRepository, IUsersRepository usersRepository,  IWebHostEnvironment appEnvironment, IArtistsRepository artistsRepository)
        {
            _songsRepository = songsRepository;
            _usersRepository = usersRepository;
            _appEnvironment = appEnvironment;
            _artistsRepository = artistsRepository;
        }

        // GET: Songs
        public async Task<IActionResult> Index()
        {
            //var songs = await _context.Songs
            //                        .Include(s => s.Artist)
            //                        .Include(s => s.Genre)
            //                        .ToListAsync();
            var songs = await _songsRepository.GetSongsListAsync();

            var userId = HttpContext.Session.GetInt32("UserId");

            //var user = _context.Users.FirstOrDefault(u => u.Id == userId);
            var user = await _usersRepository.GetFirstOrDefaultUserAsync((int)userId);

            // Передаем пользователя в представление
            ViewData["CurrentUser"] = user;

            return View(songs);
        }

        // GET: Songs/Details/5
        public async Task<IActionResult> Details(int id)
        {
            if (id == null)
            {
                return NotFound();
            }

            //var song = await _context.Songs
            //    .FirstOrDefaultAsync(m => m.Id == id);
            var song = await _songsRepository.GetFisrtOrDefaultSongById(id);
            if (song == null)
            {
                return NotFound();
            }

            return View(song);
        }

        // GET: Songs/Create
        public IActionResult Create()
        {
            //ViewData["GenreId"] = new SelectList(_context.Genres, "Id", "Name");
            ViewData["GenreId"] = new SelectList(_songsRepository.Genres(), "Id", "Name");
            ViewData["ArtistId"] = new SelectList(_songsRepository.Artists(), "Id", "Name");
            return View();
        }

        // POST: Songs/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        [RequestSizeLimit(1000000000)]
        public async Task<IActionResult> Create([Bind("Id,Title,Year,GenreId,ArtistId")] Song song, IFormFile songFile)
        {
            try
            {
                var userId = HttpContext.Session.GetInt32("UserId");
                if (userId.HasValue)
                {
                    song.UserId = userId.Value;
                }
                else
                {
                    throw new Exception("userId не найден в сессии");
                }

                // Если загружен файл песни
                if (songFile != null && songFile.Length > 0)
                {
                    // Генерация уникального имени файла
                    //var fileName = $"{_context.Artists.FirstOrDefault(a => a.Id == song.ArtistId)?.Name} - {song.Title}" + Path.GetExtension(songFile.FileName);

                    var fileName = $"{_artistsRepository.GetFirstOrDefaultArtistById(song.ArtistId)?.Name} - {song.Title}" + Path.GetExtension(songFile.FileName);

                    // Путь к папке Files
                    var filePath = "/Files/" + fileName;

                    // Сохранение файла песни в папку Files в каталоге wwwroot
                    using (var fileStream = new FileStream(_appEnvironment.WebRootPath + filePath, FileMode.Create))
                    {
                        await songFile.CopyToAsync(fileStream); // копирование файла в поток
                    }

                    // Сохранение имени файла в модели песни
                    song.Path = fileName;
                }

                // Добавление песни в контекст данных и сохранение изменений
                //_context.Add(song);
                //await _context.SaveChangesAsync();
                await _songsRepository.AddSongAndSaveAsync(song);
                return RedirectToAction(nameof(Index));
            }
            catch (Exception ex)
            {
                //ViewData["GenreId"] = new SelectList(_context.Genres, "Id", "Name");
                //ViewData["ArtistId"] = new SelectList(_context.Artists, "Id", "Name");
                ViewData["GenreId"] = new SelectList(_songsRepository.Genres(), "Id", "Name");
                ViewData["ArtistId"] = new SelectList(_songsRepository.Artists(), "Id", "Name");
                return View(song);
            }
        }



        // GET: Songs/Edit/5
        public async Task<IActionResult> Edit(int id)
        {
            if (id == null)
            {
                return NotFound();
            }

            //var song = await _context.Songs.FindAsync(id);
            var song = await _songsRepository.FindSongByIdAsync(id);
            if (song == null)
            {
                return NotFound();
            }

            //ViewData["GenreId"] = new SelectList(_context.Genres, "Id", "Name", song.GenreId);
            //ViewData["ArtistId"] = new SelectList(_context.Artists, "Id", "Name", song.ArtistId);
            ViewData["GenreId"] = new SelectList(_songsRepository.Genres(), "Id", "Name", song.GenreId);
            ViewData["ArtistId"] = new SelectList(_songsRepository.Artists(), "Id", "Name", song.ArtistId);

            return View(song);
        }

        // POST: Songs/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,Title,Year,GenreId,ArtistId")] Song song)
        {
            if (id != song.Id)
            {
                return NotFound();
            }

            try
            {
                var userId = HttpContext.Session.GetInt32("UserId");
                if (userId.HasValue)
                {
                    song.UserId = userId.Value;
                }
                else
                {
                    throw new Exception("userId не найден в сессии");
                }

                //_context.Update(song);
                //await _context.SaveChangesAsync();
                await _songsRepository.UpdateSongAsync(song); 
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!SongExists(song.Id))
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


        // GET: Songs/Delete/5
        public async Task<IActionResult> Delete(int id)
        {
            if (id == null)
            {
                return NotFound();
            }

            //var song = await _context.Songs
            //    .FirstOrDefaultAsync(m => m.Id == id);
            var song = await _songsRepository.GetFisrtOrDefaultSongById(id);
            if (song == null)
            {
                return NotFound();
            }

            return View(song);
        }

        // POST: Songs/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            //var song = await _context.Songs.FindAsync(id);
            var song = await _songsRepository.FindSongByIdAsync(id);
            if (song != null)
            {
                //_context.Songs.Remove(song);
                _songsRepository.RemoveSongAsync(song);
            }

            //await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool SongExists(int id)
        {
            //return _context.Songs.Any(e => e.Id == id);
            return _songsRepository.IsSongExistsById(id);
        }
    }
}
