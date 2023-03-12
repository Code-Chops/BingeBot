namespace BingeBot.Infrastructure.Api.TVmaze.Configuration;

public static class Paging
{
	/// <summary>
	/// This TVmaze endpoint is paginated, with a maximum of <see cref="MaximumPageSize"/> results per page.
	/// The pagination is based on show ID, e.g. page 0 will contain shows with IDs between 0 and 250.
	/// This means a single page might contain less than 250 results (in case of deletions).
	/// But it also guarantees that deletions won't cause shuffling in the page numbering for other shows.
	/// See: https://www.tvmaze.com/api#show-index.
	/// </summary>
	public static int MaximumPageSize => 250;
}