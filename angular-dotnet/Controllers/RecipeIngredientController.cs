using angular_dotnet.DbContext;
using angular_dotnet.DbModels;
using angular_dotnet.Dto;
using AutoMapper;
using AutoMapper.QueryableExtensions;
using Microsoft.AspNetCore.Mvc;

namespace angular_dotnet.Controllers;

[ApiController]
[Route("[controller]")]
public class RecipeIngredientController : ControllerBase
{

    private readonly ILogger<RecipeIngredientController> _logger;
    private readonly IMapper _mapper;
    private readonly IUnitOfWork _unitOfWork;

    public RecipeIngredientController(ILogger<RecipeIngredientController> logger, IMapper mapper, IUnitOfWork unitOfWork)
    {
        _logger = logger;
        _mapper = mapper;
        _unitOfWork = unitOfWork;
    }

    [HttpGet]
    [Route("[action]")]
    public RecipeIngredientDto? Get(string id)
    {
        if (Guid.TryParse(id, out Guid guid))
        {
            var recipeIngredient = _unitOfWork.RecipeIngredient.GetById(guid);
            var recipeIngredientDto = _mapper.Map<RecipeIngredientDto>(recipeIngredient);
            return recipeIngredientDto;
        }

        return null;
    }
    
    [HttpGet]
    [Route("")]
    [Route("[action]")]
    public IEnumerable<RecipeIngredientDto> GetAll()
    {
        var recipeIngredientJunctions = _unitOfWork.RecipeIngredient.GetAll().AsQueryable();
        var recipeIngredientDtos = recipeIngredientJunctions.ProjectTo<RecipeIngredientDto>(_mapper.ConfigurationProvider);
        return recipeIngredientDtos;
    }
    
    [HttpPost]
    [Route("[action]")]
    public ActionResult<RecipeIngredientDto> AddOrUpdate(RecipeIngredientDto recipeIngredientDto)
    {
        var recipeIngredientToUpdate = _unitOfWork.RecipeIngredient.GetAll()
            .FirstOrDefault(x => 
                x.RecipeId == recipeIngredientDto.RecipeId && x.IngredientId == recipeIngredientDto.IngredientId);
        RecipeIngredientJunction? responseRecipeIngredient;
        if (recipeIngredientToUpdate == null)
        {
            var recipeIngredient = _mapper.Map<RecipeIngredientJunction>(recipeIngredientDto);
            responseRecipeIngredient = _unitOfWork.RecipeIngredient.Add(recipeIngredient);
        }
        else
        {
            responseRecipeIngredient = _mapper.Map(recipeIngredientDto, recipeIngredientToUpdate);
        }
        _unitOfWork.Save();
        return Ok(_mapper.Map<RecipeIngredientDto>(responseRecipeIngredient));
    }
    [HttpDelete]
    [HttpPost]
    [Route("[action]/{recipeId}/{ingredientId}")]
    public ActionResult Delete(Guid recipeId, Guid ingredientId)
    {
        if (recipeId != Guid.Empty && ingredientId != Guid.Empty)
        {
            var recipeIngredient = _unitOfWork.RecipeIngredient.GetAll()
                .FirstOrDefault(x => x.RecipeId == recipeId && x.IngredientId == ingredientId);
            if (recipeIngredient != null)
            {
                _unitOfWork.RecipeIngredient.Remove(recipeIngredient);
                _unitOfWork.Save();
                // var shouldNotExist = _unitOfWork.RecipeIngredient.GetAll()
                //     .FirstOrDefault(x => x.RecipeId == recipeId && x.IngredientId == ingredientId);
                // if (shouldNotExist == null)
                // {                
                    return Ok();            
                // }
                // return Problem("Item still exists somehow");
            }
            else
            {
                return NotFound();
            }
        }
        else
        {
            return BadRequest();
        }
    }
}
