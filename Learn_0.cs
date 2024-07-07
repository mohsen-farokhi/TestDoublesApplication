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

public class WageRepository : IWageRepository
{
    public decimal GetCurrentWagePercent()
    {
        // read from db
        throw new System.NotImplementedException();
    }
}

public class DamageServiceTest
{
    [Fact]
    public void wage_is_subtracted_from_amount()
    {
        var repository = new WageRepository();
        var service = new Wage.WageService(repository);

        var actual = service.Calculate(amount: 1000000);
        actual.Should().Be(expected: 995000);
    }
}
