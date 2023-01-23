namespace Omoqo.Challenge.Api.Controllers;

[Route("[controller]")]
[ApiController]
[Authorize]
public class ShipController : ControllerBase
{
    private readonly IMapper _mapper;
    private readonly IShipRepository _shipRepository;
    private readonly IShipService _shipService;

    public ShipController(IMapper mapper,
        IShipRepository shipRepository,
        IShipService shipService)
    {
        _mapper = mapper;
        _shipRepository = shipRepository;
        _shipService = shipService;
    }

    /// <summary>
    /// Returns a Ship based on its identification
    /// </summary>
    /// <param name="id"></param>
    /// <returns>Ship info</returns>
    /// <response code="200">Return object **ShipGetResponse**.</response>
	/// <response code="400">Return object **ErrorResponse**.</response>
    [HttpGet("{id:int}")]
    [ProducesResponseType(typeof(ShipGetResponse), StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(ErrorResponse), StatusCodes.Status400BadRequest)]
    public async Task<IActionResult> GetAsync(int id)
    {
        try
        {
            Result<Ship?> result = await _shipRepository.SingleOrDefaultAsync(id);

            if (!result.Success)
                return BadRequest(new ErrorResponse(result.Errors));

            return Ok(_mapper.Map<Ship, ShipGetResponse>(result.Value!));
        }
        catch (Exception ex)
        {
            return BadRequest(new ErrorResponse(ex));
        }
    }

    /// <summary>
    /// Returns a list of Ships based on filters
    /// </summary>
    /// <param name="request"></param>
    /// <returns>Ship list</returns>
    /// <response code="200">Return object **ShipListResponse**.</response>
	/// <response code="400">Return object **ErrorResponse**.</response>
    [HttpGet("list")]
    [ProducesResponseType(typeof(ShipListResponse), StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(ErrorResponse), StatusCodes.Status400BadRequest)]
    public async Task<IActionResult> ListAsync([FromQuery] ShipListRequest request)
    {
        try
        {
            Expression<Func<Ship, bool>> query =
                q => (string.IsNullOrEmpty(request.Name) || q.Name.ToLower().Contains(request.Name.ToLower())) &&
                     (string.IsNullOrEmpty(request.Code) || q.Code.Equals(request.Code));

            Result<int> countResult = await _shipRepository.CountAsync(query);

            if (!countResult.Success)
                return BadRequest(new ErrorResponse(countResult.Errors));

            Result<IEnumerable<Ship>> listResult = await _shipRepository.ListPartialAsync(query, request.Skip, request.Limit);

            if (!listResult.Success)
                return BadRequest(new ErrorResponse(listResult.Errors));

            return Ok(new ShipListResponse
            {
                Data = _mapper.Map<IEnumerable<Ship>, List<ShipListItemResponse>>(listResult.Value!),
                Total = countResult.Value
            });
        }
        catch (Exception ex)
        {
            return BadRequest(new ErrorResponse(ex));
        }
    }

    /// <summary>
    /// Add a new Ship
    /// </summary>
    /// <param name="request"></param>
    /// <returns>Execution status</returns>
	/// <response code="200">Return Status 200</response>
	/// <response code="400">Return object **Result**</response>
    [HttpPost]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(ErrorResponse), StatusCodes.Status400BadRequest)]
    public IActionResult Add([FromBody] ShipAddRequest request)
    {
        try
        {
            Result result = _shipService.Add(_mapper.Map<ShipAddRequest, ShipAddModel>(request));

            return result.Success
                ? Ok()
                : BadRequest(new ErrorResponse(result.Errors));
        }
        catch (Exception ex)
        {
            return BadRequest(new ErrorResponse(ex));
        }
    }

    /// <summary>
    /// Update a Ship
    /// </summary>
    /// <param name="request"></param>
    /// <returns>Execution status</returns>
	/// <response code="200">Return Status 200</response>
	/// <response code="400">Return object **Result**</response>
    [HttpPut]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(ErrorResponse), StatusCodes.Status400BadRequest)]
    public async Task<IActionResult> UpdateAsync([FromBody] ShipUpdateRequest request)
    {
        try
        {
            Result result = await _shipService.UpdateAsync(_mapper.Map<ShipUpdateRequest, ShipUpdateModel>(request));

            return result.Success
                ? Ok()
                : BadRequest(new ErrorResponse(result.Errors));
        }
        catch (Exception ex)
        {
            return BadRequest(new ErrorResponse(ex));
        }
    }

    /// <summary>
    /// Remove a Ship
    /// </summary>
    /// <param name="id"></param>
    /// <returns>Execution status</returns>
	/// <response code="200">Return Status 200</response>
	/// <response code="400">Return object **Result**</response>
    [HttpDelete("{id:int}")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(ErrorResponse), StatusCodes.Status400BadRequest)]
    public async Task<IActionResult> DeleteAsync(int id)
    {
        try
        {
            Result result = await _shipService.RemoveAsync(id);

            return result.Success
                ? Ok()
                : BadRequest(new ErrorResponse(result.Errors));
        }
        catch (Exception ex)
        {
            return BadRequest(new ErrorResponse(ex));
        }
    }
}
