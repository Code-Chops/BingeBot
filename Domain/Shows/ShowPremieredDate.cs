using System.Globalization;

namespace BingeBot.Domain.Shows;

/// <summary>
/// The date of the premiere of a show.
/// </summary>
[GenerateValueObject<DateOnly>(generateToString: false, forbidParameterlessConstruction: false, propertyIsPublic: true)]
public readonly partial record struct ShowPremieredDate
{
	public override partial string ToString() => this.Value.ToShortDateString();

	public readonly string ErrorCode_BingeBot_ShowPremieredDate_Invalid = nameof(ErrorCode_BingeBot_ShowPremieredDate_Invalid);
	
	public ShowPremieredDate(string dateOnly, Validator? validator = null)
		: this(Convert(dateOnly), validator)
	{
	}

	private static DateOnly Convert(string dateOnly)
	{
		if (!DateOnly.TryParse(dateOnly, CultureInfo.InvariantCulture, out var value))
			throw new ValidationException(nameof(ErrorCode_BingeBot_ShowPremieredDate_Invalid), new ValidationExceptionMessage(nameof(ShowPremieredDate), $"{0} incorrect (value: {dateOnly})."));

		return value;
	}
}