using Binit.Framework.Constants.Authentication;
using Domain.Entities.Model;
using Domain.Entities.Model.SPs;
using Domain.Logic.Interfaces;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WebAPI.Attributes;
using WebAPI.DTOs.CategoryDTOs;

namespace WebAPI.Controllers
{
    [AuthorizeAnyRoles(Roles.BackofficeSuperAdministrator, Roles.BackofficeCategoryAdministrator, Roles.BackofficeCategoryUser)]
    [Route("api/[controller]")]
    [ApiController]
    public class CategoryController : ControllerBase
    {

        #region Properties

        private readonly ICategoryService categoryService;

        #endregion

        #region Constructor

        public CategoryController(ICategoryService categoryService)
        {
            this.categoryService = categoryService;
        }

        #endregion

        #region Endpoints

        /// <summary>
        /// Get a list with all the categorys
        /// </summary>
        [HttpGet]
        public async Task<ActionResult<IEnumerable<CategoryDTO>>> Get()
        {
            var categorys = await this.categoryService.GetAll().ToListAsync();

            return categorys.Select(p => new CategoryDTO(p)).ToList();
        }

        /// <summary>
        /// Get a list with all the categorys and a feature count
        /// </summary>
        [HttpGet("CreateBySP")]
        public List<SPReturnCategory> CreateBySP()
        {
            return this.categoryService.CreateCategoryBySP();
        }

        /// <summary>
        /// Get a specific category by id
        /// </summary>
        /// <param name="id">Id of the category you want to get</param>        
        [HttpGet("{id}")]
        public async Task<ActionResult<CategoryDTO>> Get(string id)
        {
            var category = await categoryService.GetAsync(new Guid(id));
            return Ok(new CategoryDTO(category));
        }

        /// <summary>
        /// Creates a new category.
        /// Only available for Backoffice Administrators
        /// </summary>
        [AuthorizeAnyRoles(Roles.BackofficeSuperAdministrator, Roles.BackofficeCategoryAdministrator)]
        [HttpPost]
        public async Task<IActionResult> Post([FromBody] CategoryDTO categoryDTO)
        {
            Category category = categoryDTO.ToEntity();
            await this.categoryService.CreateAsync(category);

            return Ok();
        }

        /// <summary>
        /// Updates an existing category.
        /// Only available for Backoffice Administrators
        /// </summary>
        /// <param name="id">Id of the category you want to update</param>
        /// <param name="categoryDTO">category you want to update</param>
        [AuthorizeAnyRoles(Roles.BackofficeSuperAdministrator, Roles.BackofficeCategoryAdministrator)]
        [HttpPut("{id}")]
        public async Task<IActionResult> Put(string id, [FromBody] CategoryDTO categoryDTO)
        {
            categoryDTO.Id = id;

            await categoryService.UpdateAsync(categoryDTO.ToEntity());

            return Ok();
        }

        /// <summary>
        /// Deletes an existing category.
        /// Only available for Backoffice Administrators
        /// </summary>
        /// <param name="id">Id of the category you want to delete</param>
        [AuthorizeAnyRoles(Roles.BackofficeSuperAdministrator, Roles.BackofficeCategoryAdministrator)]
        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(string id)
        {
            await categoryService.DeleteAsync(new Guid(id));

            return Ok();
        }

        #endregion
    }
}
