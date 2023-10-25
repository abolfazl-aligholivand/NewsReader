using AutoMapper;
using MediatR;
using NewsReader.Domain.DTO.NewsDTO;
using NewsReader.Domain.Helpers;
using NewsReader.Domain.Mappings;
using NewsReader.Domain.Repository.IRepository;

namespace NewsReader.Features.Newses.Queries
{
    public class GetNewsQuery : IRequest<ApiResponse<NewsDTO>>
    {
        public Guid newsId { get; set; }
        public class GetNewsQueryHandler : IRequestHandler<GetNewsQuery, ApiResponse<NewsDTO>>
        {
            private readonly IMapper _mapper;
            private readonly ILogger _logger;
            private readonly INewsRepository  _newsRepository;
            public GetNewsQueryHandler(IMapper mapper, ILogger logger, INewsRepository newsRepository)
            {
                _mapper = mapper;
                _logger = logger;
                _newsRepository = newsRepository;
            }

            public async Task<ApiResponse<NewsDTO>> Handle(GetNewsQuery request, CancellationToken cancellationToken)
            {
                try
                {
                    var news = await _newsRepository.GetAsync(request.newsId);
                    if (news is null)
                        return new ApiResponse<NewsDTO>(Domain.Enums.HttpStatusCodeEnum.NotFound, null, ApiMessage.NotFound);

                    var result = _mapper.Map<NewsDTO>(news);
                    result.Category = news.FKCategory.Category;
                    result.Website = news.FKWebsite.Title;
                    //result.Keywords = _newsKeywordRepository.GetAllByNewsIdAsync(request.newsId)
                    //    .Result.Select(s => s.FKKeyword.Title).ToList();

                    return new ApiResponse<NewsDTO>(Domain.Enums.HttpStatusCodeEnum.Success, result, ApiMessage.Successful);
                }
                catch (Exception ex)
                {
                    _logger.LogError(ex.Message);
                    return new ApiResponse<NewsDTO>(Domain.Enums.HttpStatusCodeEnum.BadRequest, null, ApiMessage.Error);
                }
            }
        }
    }
}
