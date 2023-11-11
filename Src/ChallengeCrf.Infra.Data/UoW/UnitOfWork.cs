using ChallengeCrf.Domain.Interfaces;
using ChallengeCrf.Infra.Data.Context;

namespace ChallengeCrf.Infra.Data.UoW;

public class UnitOfWork : IUnitOfWork
{
    private readonly CashFlowContext _dbContext;

    public UnitOfWork(CashFlowContext dbContext)
    {
        _dbContext = dbContext;
    }

    public async Task<bool> Commit()
    {
        bool trySave = false;
        try
        {
            trySave = true;
            
            await _dbContext.SaveChangesAsync();
        }
        catch (Exception ex)
        {
            trySave = false;
        }
        return trySave;
    }

    public void Dispose()
    {
        _dbContext.Dispose();
    }
}
