using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Recipes.Models;
using Recipes.Repo;
using Recipes.ViewModels;
using System.Security.Claims;

namespace Recipes.Controllers
{
    [Authorize(Roles = "Admin")]
    public class RecipesController : Controller
    {
        private readonly IRecipe<Recipe> recipeRepo;
        private readonly SignInManager<User> signInManager;
        private readonly UserManager<User> userManager;
        private readonly IWebHostEnvironment hostEnvironment;
        public RecipesController(IRecipe<Recipe> recipeRepo, SignInManager<User> signInManager
            , UserManager<User> userManager, IWebHostEnvironment hostEnvironment)
        {
            this.recipeRepo = recipeRepo;
            this.signInManager = signInManager;
            this.userManager = userManager;
            this.hostEnvironment = hostEnvironment;
        }
        [HttpGet]
        public IActionResult AddNewRecipe()
        {
            return View();
        }
        
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> AddNewRecipe(AddNewRecipeViewModel model)
        {
            try
            {
                string uniqueFileName = UploadedFile(model);
                User user = null;
                if (signInManager.IsSignedIn(User))
                {
                    user = (User?)await userManager.FindByEmailAsync(User.Identity.Name);

                }
                else
                {
                    return RedirectToAction("Login", "Account");
                }
                if (ModelState.IsValid)
                {
                    Recipe recipe = new Recipe
                    {
                        RecipeName = model.RecipeName,
                        DateAdded = DateTime.UtcNow,
                        Description = model.Description,
                        Image = uniqueFileName,
                        isDeleted = false,
                        User = user
                    };

                    bool isTrue = await recipeRepo.AddRecipe(recipe);
                    return RedirectToAction("GetAllRecipe", "Recipes");
                }
            }
            catch(Exception)
            {

            }
            return View();
        }
        private string UploadedFile(AddNewRecipeViewModel model)
        {
            string uniqueFileName = null;

            if (model.Image != null)
            {
                string uploadsFolder = Path.Combine(hostEnvironment.WebRootPath, "images");
                uniqueFileName = Guid.NewGuid().ToString() + "_" + model.Image.FileName;
                string filePath = Path.Combine(uploadsFolder, uniqueFileName);
                using (var fileStream = new FileStream(filePath, FileMode.Create))
                {
                    model.Image.CopyTo(fileStream);
                }
            }
            return uniqueFileName;
        }
        [HttpGet]
        public async Task<IActionResult> GetAllRecipe()
        {
            try
            {
                var recipes = await recipeRepo.GetAllRecipes();
                if (recipes != null)
                {
                    return View(recipes);
                }
            }
            catch (Exception)
            {

            }
            return View();
        }
        [HttpGet]
        public async Task<IActionResult> EditRecipeAsync(int recipeID)
        {
            AddNewRecipeViewModel recipeView = null;
            try
            {
                Recipe recipe = await recipeRepo.GetRecipeById(recipeID);

                recipeView = new AddNewRecipeViewModel
                {
                    RecipeName = recipe.RecipeName,
                    Description = recipe.Description,
                    Image = null
                };
            }
            catch (Exception)
            {

            }
            return View(recipeView);
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> EditRecipeAsync(AddNewRecipeViewModel model, int recipeID)
        {
            bool isUpdated = false;
            try
            {
               
                string uniqueFileName = UploadedFile(model);
                Recipe user = await recipeRepo.GetRecipeById(recipeID);
                if (ModelState.IsValid)
                {
                    Recipe recipe = new Recipe
                    {
                        RecipeName = model.RecipeName,
                        RecipeID = recipeID,
                        isDeleted = false,
                        User = user.User,
                        DateAdded = user.DateAdded,
                        Image = uniqueFileName,
                        Description = model.Description
                    };
                    isUpdated = await recipeRepo.UpdateRecipe(recipe);
                    if (isUpdated)
                    {
                        return RedirectToAction("GetAllRecipe", "Recipes");
                    }
                }
            }
            catch(Exception)
            {

            }
            return View();
        }
        [HttpGet]
        public async Task<IActionResult> RemoveRecipeAsync(int recipeID)
        {
            DeleteRecipeViewModel model = null;
            try {
                Recipe recipe = await recipeRepo.GetRecipeById(recipeID);
                model = new DeleteRecipeViewModel
                {
                    DateAdded = recipe.DateAdded.ToShortDateString(),
                    RecipeName = recipe.RecipeName,
                    UserName = recipe.User.UserName,
                };
            } catch(Exception) { }

            return View(model);
        }
        [HttpPost]
        public async Task<IActionResult> RemoveRecipeAsync(int recipeID, DeleteRecipeViewModel model)
        {
            try {
                Recipe recipe = await recipeRepo.GetRecipeById(recipeID);
                bool isUpdated = false;
                if (ModelState.IsValid)
                {
                    recipe.isDeleted = true;
                    isUpdated = await recipeRepo.DeleteRecipe(recipe);
                    if (isUpdated)
                    {
                        return RedirectToAction("GetAllRecipe", "Recipes");
                    }
                }
            }
            catch(Exception) { }
            return View();
        }
    }
}
