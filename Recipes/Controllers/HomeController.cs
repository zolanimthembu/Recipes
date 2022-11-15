using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Recipes.Data;
using Recipes.Models;
using Recipes.Repo;
using Recipes.ViewModels;
using System.Diagnostics;

namespace Recipes.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private readonly ApplicationDbContext applicationDbContext;

        public HomeController(ILogger<HomeController> logger, ApplicationDbContext applicationDbContext)
        {
            _logger = logger;
            this.applicationDbContext = applicationDbContext;
        }

        public IActionResult Index()
        {
            IEnumerable<Recipe> recipes = null;
            try
            {
                recipes = (from r in applicationDbContext.Recipe
                           join u in applicationDbContext.User
                            on r.User.Id equals u.Id
                           where r.isDeleted == false
                           select new Recipe
                           {
                               RecipeName = r.RecipeName,
                               DateAdded = r.DateAdded,
                               User = r.User,
                               Image = r.Image,
                               RecipeID = r.RecipeID
                           });
            }
            catch(Exception)
            {

            }
            ViewBag.LoadMore = recipes.Count();
            return View(recipes.Take(6));
        }
        public IActionResult All()
        {
            IEnumerable<Recipe> recipes = (from r in applicationDbContext.Recipe
                                           join u in applicationDbContext.User
                                            on r.User.Id equals u.Id
                                           where r.isDeleted == false
                                           select new Recipe
                                           {
                                               RecipeName = r.RecipeName,
                                               DateAdded = r.DateAdded,
                                               User = r.User,
                                               Image = r.Image,
                                               RecipeID = r.RecipeID
                                           });

            return View(recipes);
        }
        public IActionResult Instructions(int recipeID)
        {
            Recipe recipe = null;
            try {
                recipe = applicationDbContext.Recipe.Where(r => r.RecipeID == recipeID).Include(u => u.User).FirstOrDefault();
            }
            catch (Exception) { }
            return View(recipe);
        }
        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}