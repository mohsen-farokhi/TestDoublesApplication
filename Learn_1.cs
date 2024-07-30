public class WageService
{
    private readonly IWageRepository _wageRepository;
    public WageService(IWageRepository wageRepository)
    {
        _wageRepository = wageRepository;
    }

    public decimal Calculate(decimal amount)
    {
        var wagePercent =  _wageRepository.GetCurrentWagePercent();
        var wageValue = wagePercent * amount / 100;
        return amount - wageValue;
    }
}

public interface IWageRepository
{
    decimal GetCurrentWagePercent();
}

public class StubWageRepository : IWageRepository
{
    public decimal GetCurrentWagePercent()
    {
        return 0.5M;
    }
}

[Fact]
public void wage_is_subtracted_from_amount()
{
     var repository = new StubWageRepository();
     var service = new WageService(repository);
     var calculatedAmount = service.Calculate(1000000);
     calculatedAmount.Should().Be(995000);
}