using BingeBot.Application;
using BingeBot.Contracts.BingeBot.V1.Shows;

namespace BingeBot.Api.Controllers.V1;

// If another version will be released this version can be placed in the Api\Controllers\V1 folder and the new one in Api\V2, etc.
[ApiController]
[Route("shows")]
public sealed class ShowController : ControllerBase
{
	private IShowApplicationService ShowApplicationService { get; }
	private DefaultValidator Validator { get; }
	
	public const string ErrorCode_ShowController_CreateShowRequest_Null = nameof(ErrorCode_ShowController_CreateShowRequest_Null);
	public const string ErrorCode_ShowController_UpdateShowRequest_Null = nameof(ErrorCode_ShowController_UpdateShowRequest_Null);

	public ShowController(IShowApplicationService showApplicationService)
	{
		this.ShowApplicationService = showApplicationService ?? throw new ArgumentNullException(nameof(showApplicationService));
		this.Validator = CodeChops.DomainModeling.Validation.Validator.Get<PersonController>.Default;
	}
	
	/// <summary>
	/// Creates a new show.
	/// </summary>
	[HttpPost]
	public async Task<IActionResult> CreateShow([FromBody] CreateShowRequest request, CancellationToken cancellationToken)
	{
		this.Validator.GuardNotNull(request, ErrorCode_ShowController_CreateShowRequest_Null);
		
		await this.ShowApplicationService.CreateShowAsync(request, cancellationToken);
		
		return this.Ok();
	}
	
	/// <summary>
	/// Gets a show by ID.
	/// </summary>
	[HttpGet("{id}")]
	public async Task<IActionResult> GetShow(int id, CancellationToken cancellationToken)
	{
		var show = (await this.ShowApplicationService.GetFilteredShowsAsync(new GetShowsRequest() { Id = id }, cancellationToken)).SingleOrDefault();
		
		return show is null 
			? this.NotFound()
			: this.Ok(show);
	}
	
	/// <summary>
	/// Lists all shows based on the (paged) filter.
	/// </summary>
	[HttpGet]
	public async Task<GetShowsResponse> GetShows([FromQuery] GetShowsRequest? request, int? page, int? size, CancellationToken cancellationToken)
	{
		var showRequest = request ?? new GetShowsRequest() { Page = page ?? 0, Size = size ?? 0 };
		var shows = await this.ShowApplicationService.GetFilteredShowsAsync(showRequest, cancellationToken);

		return new() { Values = shows.ToList() };
	}

	/// <summary>
	/// Creates or updates a show based on the ID (idempotent).
	/// </summary>
	// According to REST 'put' has to be idempotent.
	[HttpPut("{id}")]
	public async Task<IActionResult> CreateOrUpdateShow(int? id, [FromBody] UpdateShowRequest request, CancellationToken cancellationToken)
	{
		this.Validator.GuardNotNull(request, ErrorCode_ShowController_UpdateShowRequest_Null);
		
		await this.ShowApplicationService.CreateOrUpdateShowAsync(id, request, cancellationToken);
		
		return this.Ok();
	}
	
	/// <summary>
	/// Deletes a show based by ID. Always returns success.
	/// </summary>
	[HttpDelete]
	public async Task<IActionResult> DeleteShow(int id, CancellationToken cancellationToken)
	{
		await this.ShowApplicationService.DeleteShowAsync(id, cancellationToken);
		
		return this.Ok();
	}
}