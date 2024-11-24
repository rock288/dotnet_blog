using Microsoft.AspNetCore.Mvc;
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
        public IActionResult GetAll()
        {
            var categoriesDomain = dbContext.Categories.ToList();
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
        public IActionResult GetById([FromRoute] int categoryId)
        {
            var categoryDomain = dbContext.Categories.FirstOrDefault(o => o.CategoryId == categoryId);
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
        public IActionResult Create([FromBody] AddCategoryDto addCategoryDto)
        {
            var categoriesDomainModel = new Category
            {
                Name = addCategoryDto.Name,
            };
            var categoryDomain = dbContext.Categories.Add(categoriesDomainModel);
            dbContext.SaveChanges();

            var categoriesDto = new CategoryDto
            {
                Name = categoriesDomainModel.Name,
            };
            
            return CreatedAtAction(nameof(GetById), new { CategoryId = categoriesDto.CategoryId }, categoriesDto);
        }

        [HttpPut]
        [Route("{categoryId}")]
        public IActionResult Update([FromRoute] int categoryId, [FromBody] UpdateCategoryDto updateCategoryDto)
        {
            var categoryDomainModel = dbContext.Categories.FirstOrDefault(o => o.CategoryId == categoryId);
            if (categoryDomainModel == null)
            {
                return NotFound();
            }
            categoryDomainModel.Name = updateCategoryDto.Name;
            dbContext.SaveChanges();

            var categoryDto = new CategoryDto
            {
                CategoryId = categoryDomainModel.CategoryId,
                Name = categoryDomainModel.Name,
            };

            return Ok(categoryDto);
        }

        [HttpDelete]
        [Route("{categoryId}")]
        public IActionResult Delete([FromRoute] int categoryId)
        {
            var categoryDomainModel = dbContext.Categories.FirstOrDefault(o => o.CategoryId == categoryId);
            if (categoryDomainModel == null)
            {
                return NotFound();
            }
            // Delete
            dbContext.Categories.Remove(categoryDomainModel);
            dbContext.SaveChanges();

            return Ok();
        }
    }
}
