using AutoMapper;
using Rock288.API.Models.Domain;
using Rock288.API.Models.DTO;

namespace Rock288.API.Mappings
{
    public class AutoMapperCategories: Category
    {
        public static IMapper Initialize()
        {
            var mapperConfig = new MapperConfiguration(cfg =>
            {
                cfg.CreateMap<Category, CategoryDto>();
                cfg.CreateMap<Article, ArticleDto>();
                cfg.CreateMap<Article, ArticleListDto>();
            });

            return mapperConfig.CreateMapper();
        }
    }
}