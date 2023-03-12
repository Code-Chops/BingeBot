using BingeBot.Application;
using BingeBot.Contracts.BingeBot.V1.Persons;

namespace BingeBot.Api.Controllers.V1;

// If another version will be released this version can be placed in the Api\Controllers\V1 folder and the new one in Api\V2, etc.
[ApiController]
[Route("person")]
public sealed class PersonController : ControllerBase
{
	private IPersonApplicationService PersonApplicationService { get; }
	private DefaultValidator Validator { get; }

	public const string ErrorCode_PersonController_CreatePersonRequest_Null = nameof(ErrorCode_PersonController_CreatePersonRequest_Null);
	public const string ErrorCode_PersonController_UpdatePersonRequest_Null = nameof(ErrorCode_PersonController_UpdatePersonRequest_Null);
	
	public PersonController(IPersonApplicationService personApplicationService)
	{
		this.PersonApplicationService = personApplicationService ?? throw new ArgumentNullException(nameof(personApplicationService));
		this.Validator = CodeChops.DomainModeling.Validation.Validator.Get<PersonController>.Default;
	}
	
	/// <summary>
	/// Creates a new person.
 	/// </summary>
	[HttpPost]
	public async Task<IActionResult> CreatePerson([FromBody] CreatePersonRequest request, CancellationToken cancellationToken)
	{
		this.Validator.GuardNotNull(request, ErrorCode_PersonController_CreatePersonRequest_Null);
		
		await this.PersonApplicationService.CreatePersonAsync(request, cancellationToken);
		
		return this.Ok();
	}

	/// <summary>
	/// Gets a person by ID.
	/// </summary>
	[HttpGet("{id}")]
	public async Task<IActionResult> GetPerson(int id, CancellationToken cancellationToken)
	{
		var person = (await this.PersonApplicationService.GetFilteredPersonsAsync(new GetPersonsRequest() { Id = id }, cancellationToken)).SingleOrDefault();
		
		return person is null 
			? this.NotFound()
			: this.Ok(person);
	}

	/// <summary>
	/// Lists all persons based on the (paged) filter.
	/// </summary>
	[HttpGet]
	public async Task<GetPersonsResponse> GetPersons([FromQuery] GetPersonsRequest? request, int? page, int? size, CancellationToken cancellationToken)
	{
		var personRequest = request ?? new GetPersonsRequest() { Page = page ?? 0, Size = size ?? 0 };
		var persons = await this.PersonApplicationService.GetFilteredPersonsAsync(personRequest, cancellationToken);
		
		return new() { Values = persons.ToList() };

	}
	
	/// <summary>
	/// Creates or updates a person based on the ID (idempotent).
	/// </summary>
	// According to REST 'put' has to be idempotent.
	[HttpPut("{id}")]
	public async Task<IActionResult> CreateOrUpdatePerson(int? id, [FromBody] UpdatePersonRequest request, CancellationToken cancellationToken)
	{
		this.Validator.GuardNotNull(request, ErrorCode_PersonController_UpdatePersonRequest_Null);
		
		await this.PersonApplicationService.CreateOrUpdatePersonAsync(id, request, cancellationToken);
		
		return this.Ok();
	}
	
	/// <summary>
	/// Deletes a person by ID. Always returns success.
	/// </summary>
	[HttpDelete]
	public async Task<IActionResult> DeletePerson(int id, CancellationToken cancellationToken)
	{
		await this.PersonApplicationService.DeletePersonAsync(id, cancellationToken);
		
		return this.Ok();
	}
}