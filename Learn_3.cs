public class StubWageRepository : IWageRepository
{
    private decimal _wagePercent;
    public void WithWagePercent(decimal wagePercent)
    {
        _wagePercent = wagePercent;
    }
    public decimal GetCurrentWagePercent()
    {
        return _wagePercent;
    }
}

[InlineData(1000000, 0.5, 995000)]
[InlineData(1000000, 1, 990000)]
[Theory]
public void wage_is_subtracted_from_amount (decimal amount, decimal wagePercent, decimal expected)
{
    var repository = new StubWageRepository();
    repository.WithWagePercent(wagePercent);

    var service = new WageService(repository);
    var actual = service.Calculate(amount);
    actual.Should().Be(expected);
}