using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using StarWars.Data;
using StarWars.Models;
using StarWars.Models.Domain;

namespace StarWars.Controllers
{
    public class CharactersController : Controller
    {
        private readonly MVCDbContext mvcDbContext;

        public CharactersController(MVCDbContext mvcDbContext)
        {
            this.mvcDbContext = mvcDbContext;
        }

        
        public async Task<IActionResult> Index(int page = 1)
        {
            var characters = await mvcDbContext.Characters.ToListAsync();
            int pageSize = 5;   

            IQueryable<Character> source = mvcDbContext.Characters;
            var count = await source.CountAsync();
            var items = await source.Skip((page - 1) * pageSize).Take(pageSize).ToListAsync();

            PageViewModel pageViewModel = new PageViewModel(count, page, pageSize);
            IndexViewModel viewModel = new IndexViewModel
            {
                PageViewModel = pageViewModel,
                Characters = items
            };
            return View(viewModel);
        }

        [HttpGet]
        public IActionResult Add()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Add(AddCharacterViewModel addCharacterRequest)
        {
            var character = new Character()
            {
                Id = Guid.NewGuid(),
                Name = addCharacterRequest.Name,
                OriginalName = addCharacterRequest.OriginalName,
                DateOfBirth = addCharacterRequest.DateOfBirth,
                Planet = addCharacterRequest.Planet,
                Sex = addCharacterRequest.Sex,
                Race = addCharacterRequest.Race,
                Height = addCharacterRequest.Height,
                HairColor = addCharacterRequest.HairColor,
                EyeColor = addCharacterRequest.EyeColor,
                Description = addCharacterRequest.Description,
            };

            await mvcDbContext.Characters.AddAsync(character);
            await mvcDbContext.SaveChangesAsync();
            return RedirectToAction("Index");
        }

        [HttpGet]
        public async Task<IActionResult> View(Guid id)
        {
            var character = await mvcDbContext.Characters.FirstOrDefaultAsync(x => x.Id== id);

            if (character != null)
            {
                var viewModel = new UpdateCharacterViewModel()
                {
                    Id = character.Id,
                    Name = character.Name,
                    OriginalName = character.OriginalName,
                    DateOfBirth = character.DateOfBirth,
                    Planet = character.Planet,
                    Sex = character.Sex,
                    Race = character.Race,
                    Height = character.Height,
                    HairColor = character.HairColor,
                    EyeColor = character.EyeColor,
                    Description = character.Description,
            };
                return await Task.Run(() => View("View", viewModel));
            }
            return RedirectToAction("Index");
        }

        [HttpPost]
        public async Task<IActionResult> Update(UpdateCharacterViewModel model)
        {
            var character = await mvcDbContext.Characters.FindAsync(model.Id);

            if (character != null)
            {
                character.Name = model.Name;
                character.EyeColor = model.EyeColor;
                character.Height = model.Height;
                character.DateOfBirth = model.DateOfBirth;
                character.Planet = model.Planet;
                character.Sex = model.Sex;
                character.Race = model.Race;
                character.OriginalName = model.OriginalName;
                character.HairColor = model.HairColor;
                character.Description = model.Description;
                //character.Films = model.Films;

                await mvcDbContext.SaveChangesAsync();

                return RedirectToAction("Index");
            }

            return RedirectToAction("Index");
        }


        [HttpPost]
        public async Task<IActionResult> Delete(UpdateCharacterViewModel model)
        {
            var character = await mvcDbContext.Characters.FindAsync(model.Id);

            if (character != null)
            {
                mvcDbContext.Characters.Remove(character);
                await mvcDbContext.SaveChangesAsync();

                return RedirectToAction("Index");
            }
            return RedirectToAction("Index");
        }
        [HttpGet]
        public async Task<IActionResult> CheckInDetail(Guid id)
        {
            var character = await mvcDbContext.Characters.FirstOrDefaultAsync(x => x.Id == id);

            if (character != null)
            {
                var viewModel = new UpdateCharacterViewModel()
                {
                    Id = character.Id,
                    Name = character.Name,
                    OriginalName = character.OriginalName,
                    DateOfBirth = character.DateOfBirth,
                    Planet = character.Planet,
                    Sex = character.Sex,
                    Race = character.Race,
                    Height = character.Height,
                    HairColor = character.HairColor,
                    EyeColor = character.EyeColor,
                    Description = character.Description,
                };
                return await Task.Run(() => View("InDetail", viewModel));
            }
            return RedirectToAction("Index");

        }
        [HttpPost]
        public async Task<IActionResult> Back(UpdateCharacterViewModel model)
        {
            return RedirectToAction("Index");
        }
    }
}
