using BlazorFullStackCrud.Server.Data;
using BlazorFullStackCrud.Shared;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace BlazorFullStackCrud.Server.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class Student : ControllerBase
    {
        private readonly DataContext _context;

        public Student(DataContext context)
        {
            _context = context;
        }

        [HttpGet]
        public async Task<ActionResult<List<Shared.Student>>> GetSuperHeroes()
        {
            var heroes = await _context.Student.Include(sh => sh.Department).ToListAsync();
            return Ok(heroes);
        }

        [HttpGet("departemnt")]
        public async Task<ActionResult<List<Department>>> GetComics()
        {
            var comics = await _context.Department.ToListAsync();
            return Ok(comics);
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<Shared.Student>> GetSingleHero(int id)
        {
            var hero = await _context.Student
                .Include(h => h.Department)
                .FirstOrDefaultAsync(h => h.Id == id);
            if (hero == null)
            {
                return NotFound("Sorry, no student here. :/");
            }
            return Ok(hero);
        }

        [HttpPost]
        public async Task<ActionResult<List<Shared.Student>>> CreateSuperHero(Shared.Student hero)
        {
            hero.Department = null;
            _context.Student.Add(hero);
            await _context.SaveChangesAsync();

            return Ok(await GetDbHeroes());
        }

        [HttpPut("{id}")]
        public async Task<ActionResult<List<Shared.Student>>> UpdateSuperHero(Shared.Student hero, int id)
        {
            var dbHero = await _context.Student
                .Include(sh => sh.Department)
                .FirstOrDefaultAsync(sh => sh.Id == id);
            if (dbHero == null)
                return NotFound("Sorry, but no student for you. :/");

            dbHero.FirstName = hero.FirstName;
            dbHero.LastName = hero.LastName;
            dbHero.Course = hero.Course;
            dbHero.DepartmentId = hero.DepartmentId;

            await _context.SaveChangesAsync();

            return Ok(await GetDbHeroes());
        }

        [HttpDelete("{id}")]
        public async Task<ActionResult<List<Shared.Student>>> DeleteSuperHero(int id)
        {
            var dbHero = await _context.Student
                .Include(sh => sh.Department)
                .FirstOrDefaultAsync(sh => sh.Id == id);
            if (dbHero == null)
                return NotFound("Sorry, but no student for you. :/");

            _context.Student.Remove(dbHero);
            await _context.SaveChangesAsync();

            return Ok(await GetDbHeroes());
        }

        private async Task<List<Shared.Student>> GetDbHeroes()
        {
            return await _context.Student.Include(sh => sh.Department).ToListAsync();
        }
    }
}
