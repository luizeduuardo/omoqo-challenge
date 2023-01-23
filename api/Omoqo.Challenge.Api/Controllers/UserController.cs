namespace Omoqo.Challenge.Api.Controllers;

[Route("[controller]")]
[ApiController]
public class UserController : ControllerBase
{
    private readonly IMapper _mapper;
    private readonly IUserService _userService;
    private readonly IJwtHelper _jwtHelper;

    public UserController(IMapper mapper,
        IUserService userService,
        IJwtHelper jwtHelper)
    {
        _mapper = mapper;
        _userService = userService;
        _jwtHelper = jwtHelper;
    }

    /// <summary>
    /// Authenticate an user
    /// </summary>
    /// <param name="request"></param>
    /// <returns>Authentication info</returns>
    /// <response code="200">Return object **UserAuthenticateResponse**.</response>
	/// <response code="400">Return object **ErrorResponse**.</response>
    [HttpPost("authenticate")]
    [ProducesResponseType(typeof(UserAuthenticateResponse), StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(ErrorResponse), StatusCodes.Status400BadRequest)]
    public async Task<IActionResult> AuthenticateAsync([FromBody] UserAuthenticateRequest request)
    {
        try
        {
            Result<User> authenticateResult =
                await _userService.AuthenticateAsync(_mapper.Map<UserAuthenticateRequest, UserAuthenticateModel>(request)).ConfigureAwait(false);

            if (!authenticateResult.Success)
                return BadRequest(new ErrorResponse(authenticateResult.Errors));

            string token = _jwtHelper.GenerateAccessToken(authenticateResult.Value!);

            UserAuthenticateResponse response = new()
            {
                Token = token
            };

            return Ok(response);
        }
        catch (Exception ex)
        {
            return BadRequest(new ErrorResponse(ex));
        }
    }
}
