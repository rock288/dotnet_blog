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
            return Ok(mapper.Map<CategoryDto>(categoryDomain));
        }

        [HttpPost]
        public async Task<IActionResult> Create([FromBody] AddCategoryDto addCategoryDto)
        {
            var categoriesDomainModel = new Category
            {
                Name = addCategoryDto.Name,
            };
            categoriesDomainModel = await categoryRepository.CreateAsync(categoriesDomainModel);

            return Ok(mapper.Map<CategoryDto>(categoriesDomainModel));
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

            return Ok(mapper.Map<CategoryDto>(categoryDomainModel));
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
