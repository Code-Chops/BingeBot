using BingeBot.Domain.Shows;

namespace BingeBot.Domain.UnitTests;

public class ShowNameTests
{
	[Fact]
	public void ShowName_ShouldThrowValidationMessage_WhenNull()
	{
		Assert.Throws<ValidationException<NotNullGuard<string>>>(() => new ShowName(value: null!));
	}
}