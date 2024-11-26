using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using MVCFilmes.Data;
using MVCFilmes.Models;


//  Aula 66 - Pesquisando na base de dados 04/05/24
//  Aula 67 - View Model 04/05/24
//  Aula 68 Alterações de Campo e Base de Dados 05/05/24
namespace MVCFilmes.Controllers
{
    public class FilmesAController : Controller
    {
        private readonly MVCFilmesContext _context;

        public FilmesAController(MVCFilmesContext context)
        {
            _context = context;
        }



        // GET: FilmesA
       
        // (2-67) 
        public async Task<IActionResult> Index(string Texto, string Genero)
        {

            //   // QUERY GENEROS
            //   (4-67) Vou criar uma Query especifica para uma lista de generos
            IQueryable<string> generos = from m in _context.Filmes
                                         orderby m.Genero
                                         select m.Genero;

            //  // QUERY FILME
            //  (1-66) Fazendo uma verificação se o usuário digitou algo especifico, montando uma query especifica para achar o filme
            var filmes = from m in _context.Filmes select m;

            // FILTRO GENERO
            // (5-67) 
            if (!string.IsNullOrWhiteSpace(Genero))
            {
                filmes = filmes.Where(s => s.Genero == Genero);
            }


            // FILTRO O FILME
            if (!String.IsNullOrWhiteSpace(Texto))
            {
                filmes = filmes.Where(s => s.Titulo!.Contains(Texto));
            }


            // (3-67) ViewModel = Query especifica para achar o filme
            var filmeViewModel = new Models.FilmesViewModel
            {
                // Tenhos as informações necessárias, preciso criar o objeto e mandar para o C#
                Filmes = await filmes.ToListAsync(),  
                /*Na pasta (Models) Arq (FilmesViewModel.cs) eu criei um selectList então preciso retornar
                um novo SelectList (Como eu criei várias comédias não quero que duplique então vou usar um Distinct*/
                Generos = new SelectList(await generos.Distinct().ToListAsync())


            };

            return View(filmeViewModel);

        }



        // GET: FilmesA/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var filmes = await _context.Filmes
                .FirstOrDefaultAsync(m => m.ID == id);
            if (filmes == null)
            {
                return NotFound();
            }

            return View(filmes);
        }

        // GET: FilmesA/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: FilmesA/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]

        //  (6-68) 
        public async Task<IActionResult> Create([Bind("ID,Titulo,DataLancamento,Genero,Preco, Pontos")] Filmes filmes)
        {
            if (ModelState.IsValid)
            {
                _context.Add(filmes);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(filmes);
        }

        // GET: FilmesA/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var filmes = await _context.Filmes.FindAsync(id);
            if (filmes == null)
            {
                return NotFound();
            }
            return View(filmes);
        }

        // POST: FilmesA/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]


        //  (7-68) Adicionar Pontos no final do campo para realizar um Bind para o objeto Filmes    
        public async Task<IActionResult> Edit(int id, [Bind("ID,Titulo,DataLancamento,Genero,Preco,Pontos")] Filmes filmes)
        {
            if (id != filmes.ID)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(filmes);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!FilmesExists(filmes.ID))
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
            return View(filmes);
        }

        // GET: FilmesA/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var filmes = await _context.Filmes
                .FirstOrDefaultAsync(m => m.ID == id);
            if (filmes == null)
            {
                return NotFound();
            }

            return View(filmes);
        }

        // POST: FilmesA/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var filmes = await _context.Filmes.FindAsync(id);
            if (filmes != null)
            {
                _context.Filmes.Remove(filmes);
            }

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool FilmesExists(int id)
        {
            return _context.Filmes.Any(e => e.ID == id);
        }
    }
}
