
using ChallengeCrf.Domain.Interfaces;
using ChallengeCrf.Domain.Models;
using ChallengeCrf.Infra.Data.Context;
using Microsoft.Extensions.Logging;

namespace ChallengeCrf.Infra.Data.Repository;

public class DailyConsolidatedRepository : IDailyConsolidatedRepository
{
    private readonly ILogger<DailyConsolidatedRepository> _logger;
    protected readonly CashFlowContext _dbContext;

    public DailyConsolidatedRepository(CashFlowContext dbContext, ILogger<DailyConsolidatedRepository> logger)
    {
        _dbContext = dbContext;
        _logger = logger;
    }

    public void AddCashFlow(DailyConsolidated register)
    {
        _logger.LogInformation("Inserindo no banco de dados");
        try
        {
            _dbContext.DailyConsolidated.Add(register);
        }
        catch (Exception ex)
        {
            _logger.LogError(ex.Message);
        }
    }

    public async Task<IAsyncEnumerable<DailyConsolidated>> GetAllDailyConsolidatedAsync()
    {
        IAsyncEnumerable<DailyConsolidated>? registerList = null;
        try
        {
            _logger.LogInformation("Coletando  GetAllDailyConsolidatedAsync no MongoDB");

            registerList = _dbContext.DailyConsolidated
                .AsNoTracking()
                .ToAsyncEnumerable();

            _logger.LogInformation("Conseguiu Coletar do MongoDB em GetAllDailyConsolidatedAsync ");
        }
        catch (Exception ex)
        {
            _logger.LogError(ex.Message);
        }
        return registerList;

    }

    public async  Task<DailyConsolidated> GetDailyConsolidatedByDateAsync(DateTime date)
    {
        var registerResult = await _dbContext
            .DailyConsolidated
            .ToAsyncEnumerable()
            .SingleOrDefaultAsync(x => x.Date.ToString("yyyyMMdd") == date.ToString("yyyyMMdd"));
        //.SingleOrDefaultAsync(x => x.CashFlowId == registerId);

        return registerResult;
    }

}
