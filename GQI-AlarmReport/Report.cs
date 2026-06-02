using Skyline.DataMiner.Analytics.GenericInterface;
using System;

internal sealed class Report
{
    private const int DEFAULT_VIEW_FILTER = -1;
    private const int DEFAULT_MAX_AMOUNT = 5;

    private static readonly Lazy<Report> _lazyInstance = new Lazy<Report>(() => new Report());

    private Report()
    {
        ViewFilterArgument = new GQIIntArgument("View filter")
        {
            IsRequired = false,
            DefaultValue = DEFAULT_VIEW_FILTER,
        };

        var timeSpanOptions = new[]
        {
            TimeSpans.DAY,
            TimeSpans.WEEK,
            TimeSpans.MONTH,
        };
        TimeSpanArgument = new GQIStringDropdownArgument("Time span", timeSpanOptions)
        {
            IsRequired = true,
            DefaultValue = TimeSpans.DAY,
        };

        MaxAmountArgument = new GQIIntArgument("Max amount")
        {
            IsRequired = false,
            DefaultValue = DEFAULT_MAX_AMOUNT,
        };
    }

    public static Report Instance => _lazyInstance.Value;

    public GQIArgument<int> ViewFilterArgument { get; }

    public GQIArgument<string> TimeSpanArgument { get; }

    public GQIArgument<int> MaxAmountArgument { get; }

    public int GetViewFilter(OnArgumentsProcessedInputArgs argumentValues)
    {
        if (argumentValues.TryGetArgumentValue(ViewFilterArgument, out int viewFilter))
            return viewFilter;
        return DEFAULT_VIEW_FILTER;
    }

    public string GetTimeSpan(OnArgumentsProcessedInputArgs argumentValues)
    {
        return argumentValues.GetArgumentValue(TimeSpanArgument);
    }

    public int GetMaxAmount(OnArgumentsProcessedInputArgs argumentValues)
    {
        if (argumentValues.TryGetArgumentValue(MaxAmountArgument, out int maxAmount))
            return maxAmount;
        return DEFAULT_MAX_AMOUNT;
    }
}
