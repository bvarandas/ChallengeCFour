using ChallengeCrf.Domain.Interfaces;
using ChallengeCrf.Infra.Data.Context;

namespace ChallengeCrf.Infra.Data.UoW;

public class UnitOfWork : IUnitOfWork
{
    private readonly DbContextClass _dbContext;

    public UnitOfWork(DbContextClass dbContext)
    {
        _dbContext = dbContext;
    }

    public bool Commit()
    {
        bool trySave = false;
        try
        {
            trySave = _dbContext.SaveChanges() > 0;
        }
        catch (Exception ex)
        {

        }
        return trySave;
    }

    public void Dispose()
    {
        _dbContext.Dispose();
    }
}
