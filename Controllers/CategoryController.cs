using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Rock288.API.Data;
using Rock288.API.Models.Domain;
using Rock288.API.Models.DTO;

namespace MyApp.Namespace
{
    [Route("api/[controller]")]
    [ApiController]
    public class CategoryController : ControllerBase
    {
        private readonly AppDbContext dbContext;

        public CategoryController(AppDbContext dbContext)
        {
            this.dbContext = dbContext;
        }

        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            var categoriesDomain = await dbContext.Categories.ToListAsync();
            var categoriesDto = new List<CategoryDto>();
            foreach (var categoryDomain in categoriesDomain)
            {
                categoriesDto.Add(new CategoryDto()
                {
                    CategoryId = categoryDomain.CategoryId,
                    Name = categoryDomain.Name,
                    Articles = categoryDomain.Articles,
                });
            }
            return Ok(categoriesDto);
        }

        [HttpGet]
        [Route("{categoryId}")]
        public async Task<IActionResult> GetById([FromRoute] int categoryId)
        {
            var categoryDomain = await dbContext.Categories.FirstOrDefaultAsync(o => o.CategoryId == categoryId);
            if (categoryDomain == null)
            {
                return NotFound();
            }
            var categoryDto = new CategoryDto()
            {
                CategoryId = categoryDomain.CategoryId,
                Name = categoryDomain.Name,
                Articles = categoryDomain.Articles,
                
            };
            return Ok(categoryDomain);
        }

        [HttpPost]
        public async Task<IActionResult> Create([FromBody] AddCategoryDto addCategoryDto)
        {
            var categoriesDomainModel = new Category
            {
                Name = addCategoryDto.Name,
            };
            await dbContext.Categories.AddAsync(categoriesDomainModel);
            await dbContext.SaveChangesAsync();

            var categoriesDto = new CategoryDto
            {
                Name = categoriesDomainModel.Name,
            };
            
            return CreatedAtAction(nameof(GetById), new { CategoryId = categoriesDto.CategoryId }, categoriesDto);
        }

        [HttpPut]
        [Route("{categoryId}")]
        public async Task<IActionResult> Update([FromRoute] int categoryId, [FromBody] UpdateCategoryDto updateCategoryDto)
        {
            var categoryDomainModel = await dbContext.Categories.FirstOrDefaultAsync(o => o.CategoryId == categoryId);
            if (categoryDomainModel == null)
            {
                return NotFound();
            }
            categoryDomainModel.Name = updateCategoryDto.Name;
            await dbContext.SaveChangesAsync();

            var categoryDto = new CategoryDto
            {
                CategoryId = categoryDomainModel.CategoryId,
                Name = categoryDomainModel.Name,
            };

            return Ok(categoryDto);
        }

        [HttpDelete]
        [Route("{categoryId}")]
        public async Task<IActionResult> Delete([FromRoute] int categoryId)
        {
            var categoryDomainModel = await dbContext.Categories.FirstOrDefaultAsync(o => o.CategoryId == categoryId);
            if (categoryDomainModel == null)
            {
                return NotFound();
            }
            // Delete
            dbContext.Categories.Remove(categoryDomainModel);
            await dbContext.SaveChangesAsync();

            return Ok();
        }
    }
}
