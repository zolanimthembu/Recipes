using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Recipes.Models;
using Recipes.ViewModels;

namespace Recipes.Controllers
{
    [Authorize]
    public class AdminstratorController : Controller
    {
        private readonly RoleManager<IdentityRole> roleManager;
        private readonly UserManager<User> userManager;
        public AdminstratorController(RoleManager<IdentityRole> roleManager, UserManager<User> userManager)
        {
            this.roleManager = roleManager;
            this.userManager = userManager;
        }
        [HttpGet]
        public IActionResult CreateRole()
        {
            return View();
        }
        [HttpPost]
        public async Task<IActionResult> CreateRole(AdminCreateRoleViewModel model)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    IdentityRole identityRole = new IdentityRole { Name = model.RoleName };
                    IdentityResult identityResult = await roleManager.CreateAsync(identityRole);
                    if (identityResult.Succeeded)
                    {
                        return RedirectToAction("RolesList", "Adminstrator");
                    }
                    foreach (IdentityError error in identityResult.Errors)
                    {
                        ModelState.AddModelError(string.Empty, error.Description);
                    }
                }
            }
            catch (Exception)
            {

            }

            return View(model);
        }
        [HttpGet]
        public IActionResult RolesList()
        {           
            var roles = roleManager.Roles;
            return View(roles);
        }
        [HttpGet]
        public async Task<IActionResult> AssignRoles(string roleID)
        {
            ViewBag.RoleID = roleID;
            var role = await roleManager.FindByIdAsync(roleID);

            if (role == null)
            {
                ViewBag.ErrorMessage = $"Role with {roleID} not found";
                return View("NotFound");
            }
            var model = new List<AdminAssigRolesViewModel>();
            try
            {
                foreach (var user in userManager.Users)
                {
                    var assignRoleViewModel = new AdminAssigRolesViewModel
                    {
                        UserID = user.Id,
                        UserName = user.UserName
                    };
                    assignRoleViewModel.isSelected = await userManager.IsInRoleAsync(user, role.Name) ? true : false;
                    model.Add(assignRoleViewModel);
                }
            }
            catch(Exception)
            {

            }

            return View(model);
        }
        [HttpPost]
        public async Task<IActionResult> AssignRoles(List<AdminAssigRolesViewModel> model, string roleID)
        {
            try
            {
                var role = await roleManager.FindByIdAsync(roleID);
                if (role == null)
                {
                    ViewBag.ErrorMessage = $"Role with {roleID} not found";
                    return View("NotFound");
                }
                foreach (AdminAssigRolesViewModel selectedRoles in model)
                {
                    var user = await userManager.FindByIdAsync(selectedRoles.UserID);
                    IdentityResult result = null;
                    if (selectedRoles.isSelected && !(await userManager.IsInRoleAsync(user, role.Name)))
                    {
                        result = await userManager.AddToRoleAsync(user, role.Name);
                    }
                    else if (!selectedRoles.isSelected && await userManager.IsInRoleAsync(user, role.Name))
                    {
                        result = await userManager.RemoveFromRoleAsync(user, role.Name);
                    }
                    else
                    {
                        continue;
                    }
                }
            }
            catch (Exception)
            {

            }            
            return View(model);
        }
    }
}
