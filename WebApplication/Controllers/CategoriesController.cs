using Application.Categories.Commands.CreateCategory;
using Application.Categories.Commands.DeleteCategory;
using Application.Categories.Commands.EditCategory;
using Application.Categories.Queries.GetCategories;
using Application.Categories.Queries.GetCategoriesWithPagination;
using Application.Categories.Queries.GetCategoryById;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WebApplication.ViewModels;

namespace WebApplication.Controllers
{
    public class CategoriesController : MvcController
    {
        public async Task<IActionResult> Index([FromQuery] GetCategoriesWithPaginationQuery query)
        {
            if (ModelState.IsValid)
                return View(await Mediator.Send(query));

            return BadRequest();
        }

        public async Task<IActionResult> Create()
        {
            var vm = new CategoryViewModel();

            vm.Categories = await PopulateCategoriesSelect();

            return View(vm);
        }

        [HttpPost]
        public async Task<IActionResult> Create(CreateCategoryCommand command)
        {
            if (ModelState.IsValid)
            {
                await Mediator.Send(command);

                return RedirectToAction(nameof(Index));
            }

            var vm = new CategoryViewModel();

            vm.Categories = await PopulateCategoriesSelect();

            return View(vm);
        }

        public async Task<IActionResult> Update(int id)
        {
            var category = await Mediator.Send(new GetCategoryByIdQuery { Id = id });

            if(category == default)
            {
                return NotFound();
            }

            var vm = new CategoryViewModel
            {
                Name = category.Name,
                ParentId = category.ParentId
            };

            vm.Categories = await PopulateCategoriesSelect();

            return View(vm);
        }

        [HttpPost]
        public async Task<IActionResult> Update(int id, UpdateCategoryCommand command)
        {
            if(id != command.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                await Mediator.Send(command);

                return RedirectToAction(nameof(Index));
            }

            var vm = new CategoryViewModel
            {
                Name = command.Name,
                ParentId = command.ParentId
            };

            vm.Categories = await PopulateCategoriesSelect();

            return View(vm);
        }

        public async Task<IActionResult> Delete(int id)
        {
            var category = await Mediator.Send(new GetCategoryByIdQuery { Id = id });

            if (category == default)
            {
                return NotFound();
            }

            return View(category);
        }

        [HttpPost, ActionName(nameof(Delete))]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            await Mediator.Send(new DeleteCategoryCommand { Id = id });

            return RedirectToAction(nameof(Index));
        }

        private async Task<IEnumerable<SelectListItem>> PopulateCategoriesSelect()
        {
            return (await Mediator.Send(new GetParentCategoriesQuery())).Select(c => new SelectListItem
            {
                Value = c.Id.ToString(),
                Text = c.Name
            });
        }
    }
}
