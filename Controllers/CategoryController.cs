using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Rock288.API.Data;
using Rock288.API.Models.Domain;
using Rock288.API.Models.DTO;
using Rock288.API.Repositories;

namespace MyApp.Namespace
{
    [Route("api/[controller]")]
    [ApiController]
    public class CategoryController : ControllerBase
    {
        private readonly AppDbContext dbContext;
        private readonly ICategoryRepository categoryRepository;
        private readonly IMapper mapper;


        public CategoryController(AppDbContext dbContext, ICategoryRepository categoryRepository, IMapper mapper)
        {
            this.dbContext = dbContext;
            this.categoryRepository = categoryRepository;
            this.mapper = mapper;
        }

        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            var categoriesDomain = await categoryRepository.GetAllAsync();
            // var categoriesDto = new List<CategoryDto>();
            // foreach (var categoryDomain in categoriesDomain)
            // {
            //     categoriesDto.Add(new CategoryDto()
            //     {
            //         CategoryId = categoryDomain.CategoryId,
            //         Name = categoryDomain.Name,
            //     });
            // }
            var categoriesDto = mapper.Map<List<Category>, List<CategoryDto>>(categoriesDomain);
            return Ok(categoriesDto);
        }

        [HttpGet]
        [Route("{categoryId}")]
        public async Task<IActionResult> GetById([FromRoute] int categoryId)
        {
            var categoryDomain = await categoryRepository.GetByIdAsync(categoryId);
            if (categoryDomain == null)
            {
                return NotFound();
            }
            var categoryDto = new CategoryDto()
            {
                CategoryId = categoryDomain.CategoryId,
                Name = categoryDomain.Name,
            };
            return Ok(categoryDto);
        }

        [HttpPost]
        public async Task<IActionResult> Create([FromBody] AddCategoryDto addCategoryDto)
        {
            var categoriesDomainModel = new Category
            {
                Name = addCategoryDto.Name,
            };
            categoriesDomainModel = await categoryRepository.CreateAsync(categoriesDomainModel);

            var categoriesDto = new CategoryDto
            {
                CategoryId = categoriesDomainModel.CategoryId,
                Name = categoriesDomainModel.Name,
            };
            
            return Ok(categoriesDto);
        }

        [HttpPut]
        [Route("{categoryId}")]
        public async Task<IActionResult> Update([FromRoute] int categoryId, [FromBody] UpdateCategoryDto updateCategoryDto)
        {
            var categoryDomainModel = new Category
            {
                Name = updateCategoryDto.Name
            };
            categoryDomainModel = await categoryRepository.UpdateAsync(categoryId,  categoryDomainModel);

            if (categoryDomainModel == null)
            {
                return NotFound();
            }

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
            var categoryDomainModel = await categoryRepository.DeleteAsync(categoryId);
            if (categoryDomainModel == null)
            {
                return NotFound();
            }
            return Ok();
        }
    }
}
