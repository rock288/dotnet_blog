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
    public class ArticleController : ControllerBase
    {
        private readonly AppDbContext dbContext;
        private readonly IArticleRepository ArticleRepository;
        private readonly IMapper mapper;


        public ArticleController(AppDbContext dbContext, IArticleRepository ArticleRepository, IMapper mapper)
        {
            this.dbContext = dbContext;
            this.ArticleRepository = ArticleRepository;
            this.mapper = mapper;
        }

        [HttpGet]
        [Route("category")]
        public async Task<IActionResult> GetArticleByCategoryAsync([FromQuery] int CategoryId, int pageSize, int pageNumber)
        {
            var categoriesDomain = await ArticleRepository.GetArticleByCategoryAsync(CategoryId, pageSize, pageNumber);
            var categoriesDto = mapper.Map<List<Article>, List<ArticleListDto>>(categoriesDomain);
            return Ok(categoriesDto);
        }

        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            var categoriesDomain = await ArticleRepository.GetAllAsync();
            var categoriesDto = mapper.Map<List<Article>, List<ArticleDto>>(categoriesDomain);
            return Ok(categoriesDto);
        }

        [HttpGet]
        [Route("{ArticleId}")]
        public async Task<IActionResult> GetById([FromRoute] int ArticleId)
        {
            var ArticleDomain = await ArticleRepository.GetByIdAsync(ArticleId);
            if (ArticleDomain == null)
            {
                return NotFound();
            }
            return Ok(mapper.Map<ArticleDto>(ArticleDomain));
        }

        [HttpPost]
        public async Task<IActionResult> Create([FromBody] AddArticleDto addArticleDto)
        {
            var categoriesDomainModel = new Article
            {
                Title = addArticleDto.Title,
                Content = addArticleDto.Content,
                CategoryId = addArticleDto.CategoryId,
            };
            categoriesDomainModel = await ArticleRepository.CreateAsync(categoriesDomainModel);

            return Ok(mapper.Map<ArticleDto>(categoriesDomainModel));
        }

        [HttpPut]
        [Route("{ArticleId}")]
        public async Task<IActionResult> Update([FromRoute] int ArticleId, [FromBody] UpdateArticleDto updateArticleDto)
        {
            var ArticleDomainModel = new Article
            {
                Content = updateArticleDto.Content,
                Title = updateArticleDto.Title,
                CategoryId = updateArticleDto.CategoryId
            };
            ArticleDomainModel = await ArticleRepository.UpdateAsync(ArticleId,  ArticleDomainModel);

            if (ArticleDomainModel == null)
            {
                return NotFound();
            }

            return Ok(mapper.Map<ArticleDto>(ArticleDomainModel));
        }

        [HttpDelete]
        [Route("{ArticleId}")]
        public async Task<IActionResult> Delete([FromRoute] int ArticleId)
        {
            var ArticleDomainModel = await ArticleRepository.DeleteAsync(ArticleId);
            if (ArticleDomainModel == null)
            {
                return NotFound();
            }
            return Ok();
        }
    }
}
