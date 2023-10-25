using AutoMapper;
using MediatR;
using NewsReader.Domain.Data;
using NewsReader.Domain.DTO.NewsCategoryDTO;
using NewsReader.Domain.Helpers;
using NewsReader.Domain.Models;
using NewsReader.Domain.Repository.IRepository;

namespace NewsReader.Features.NewsCategories.Commands
{
    public class NewsCategoryCreateCommand : IRequest<ApiResponse<NewsCategoryDTO>>
    {
        public string Category { get; set; }

        public class NewsCategoryCreateCommandHandler : IRequestHandler<NewsCategoryCreateCommand, ApiResponse<NewsCategoryDTO>>
        {
            private readonly IMapper _mapper;
            private readonly ILogger _logger;
            private readonly NewsReaderContext _context;
            private readonly INewsCategoryRepository _newsCategoryRepository;

            public NewsCategoryCreateCommandHandler(IMapper mapper, ILogger logger, NewsReaderContext context, INewsCategoryRepository newsCategoryRepository)
            {
                _mapper = mapper;
                _context = context;
                _logger = logger;
                _newsCategoryRepository = newsCategoryRepository;
            }

            public async Task<ApiResponse<NewsCategoryDTO>> Handle(NewsCategoryCreateCommand request, CancellationToken cancellationToken)
            {
                try
                {
                    var oldCategory = await _newsCategoryRepository.GetAsync(request.Category);
                    if (oldCategory is not null)
                        return new ApiResponse<NewsCategoryDTO>(Domain.Enums.HttpStatusCodeEnum.BadRequest, null, ApiMessage.NewsCategoryDuplicateError);

                    var createdCategory = await _newsCategoryRepository.CreateAsynce(new NewsCategory
                    {
                        Category = request.Category
                    });

                    if (createdCategory is null)
                        return new ApiResponse<NewsCategoryDTO>(Domain.Enums.HttpStatusCodeEnum.BadRequest, null, ApiMessage.Error);

                    await _context.SaveChangesAsync();
                    var result = _mapper.Map<NewsCategoryDTO>(createdCategory);
                    return new ApiResponse<NewsCategoryDTO>(Domain.Enums.HttpStatusCodeEnum.Success, result, ApiMessage.NewsCategoryCreateSuccessful);
                }
                catch (Exception ex)
                {
                    _logger.LogError(ex.Message);
                    return new ApiResponse<NewsCategoryDTO>(Domain.Enums.HttpStatusCodeEnum.BadRequest, null, ApiMessage.Error);
                }
            }
        }
    }
}
