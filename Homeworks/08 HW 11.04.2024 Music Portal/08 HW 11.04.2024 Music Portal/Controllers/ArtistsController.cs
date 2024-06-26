﻿using System;
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
    public class ArtistsController : Controller
    {
        //private readonly MusicPortalContext _context;
        IArtistsRepository _artistsRepository;

        //public ArtistsController(MusicPortalContext context)
        //{
        //    _context = context;
        //}
        public ArtistsController(IArtistsRepository artistsRepository)
        {
            _artistsRepository = artistsRepository;
        }

        // GET: Artists
        public async Task<IActionResult> Index()
        {
            return View(await _artistsRepository.GetArtistsListAsync());
        }

        //// GET: Artists/Details/5
        //public async Task<IActionResult> Details(int? id)
        //{
        //    if (id == null)
        //    {
        //        return NotFound();
        //    }

        //    var artist = await _context.Artists
        //        .FirstOrDefaultAsync(m => m.Id == id);
        //    if (artist == null)
        //    {
        //        return NotFound();
        //    }

        //    return View(artist);
        //}

        // GET: Artists/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: Artists/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,Name")] Artist artist)
        {
            if (ModelState.IsValid)
            {
                //_context.Add(artist);
                //await _context.SaveChangesAsync();
                await _artistsRepository.AddArtistAndSaveAsync(artist);
                return RedirectToAction(nameof(Index));
            }
            return View(artist);
        }

        // GET: Artists/Edit/5
        public async Task<IActionResult> Edit(int id)
        {
            if (id == null)
            {
                return NotFound();
            }

            //var artist = await _context.Artists.FindAsync(id);
            var artist = await _artistsRepository.FindArtistByIdAsync(id);
            if (artist == null)
            {
                return NotFound();
            }
            return View(artist);
        }

        // POST: Artists/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,Name")] Artist artist)
        {
            if (id != artist.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    //_context.Update(artist);
                    //await _context.SaveChangesAsync();
                    await _artistsRepository.UpdateArtistAsync(artist);
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!ArtistExists(artist.Id))
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
            return View(artist);
        }

        // GET: Artists/Delete/5
        public async Task<IActionResult> Delete(int id)
        {
            if (id == null)
            {
                return NotFound();
            }

            //var artist = await _context.Artists
            //    .FirstOrDefaultAsync(m => m.Id == id);
            var artist = await _artistsRepository.GetFisrtOrDefaultArtistByIdAsync(id);
            if (artist == null)
            {
                return NotFound();
            }

            return View(artist);
        }

        // POST: Artists/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            //var artist = await _context.Artists.FindAsync(id);
            var artist = await _artistsRepository.FindArtistByIdAsync(id);
            if (artist != null)
            {
                //_context.Artists.Remove(artist);
                _artistsRepository.RemoveArtistAsync(artist);

            }

            //await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool ArtistExists(int id)
        {
            //return _context.Artists.Any(e => e.Id == id);
            return _artistsRepository.IsArtistExistsById(id);
        }
    }
}
