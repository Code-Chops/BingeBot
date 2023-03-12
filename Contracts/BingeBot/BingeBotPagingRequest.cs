namespace BingeBot.Contracts.BingeBot;

public record BingeBotPagingRequest : PagingContract
{
	protected override int MaximumSize => 250;
	protected override int DefaultSize => 50;
}