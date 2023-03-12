namespace BingeBot.Infrastructure.Api.TVmaze.Http;

public interface ITVmazeHttpClient
{
	Task<IEnumerable<TElement>> GetListAsync<TElement>(string url);
	Task<T> GetSingleAsync<T>(string url);
}