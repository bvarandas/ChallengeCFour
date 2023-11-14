using ChallengeCrf.Domain.Models;
using ChallengeCrf.Domain.Interfaces;
using ChallengeCrf.Infra.Data.Context;
//using Microsoft.EntityFrameworkCore;
using MongoFramework;
using Microsoft.EntityFrameworkCore;

namespace ChallengeCrf.Infra.Data.Repository;
public class CashFlowRepository: ICashFlowRepository
{
    protected readonly CashFlowContext _dbContext;
    public CashFlowRepository(CashFlowContext dbContext)
    {
        _dbContext = dbContext;
    }
    public void AddCashFlow(CashFlow register)
    {
        _dbContext.CashFlow.Add(register);
    }
    public async Task DeleteCashFlowAsync(string registerId)
    {
        var filtered = await _dbContext.CashFlow.FirstOrDefaultAsync(x => x.CashFlowId == registerId);
        _dbContext.CashFlow.Remove(filtered);
    }
    public async Task<IAsyncEnumerable<CashFlow>> GetAllCashFlowAsync()
    {
        var registerList = _dbContext.CashFlow
            .AsNoTracking()
            .ToAsyncEnumerable();

        return registerList;
    }
    public async Task<CashFlow> GetCashFlowByIDAsync(string registerId)
    {
        var registerResult = await _dbContext
            .CashFlow
            .ToAsyncEnumerable()
            .SingleOrDefaultAsync(x => x.CashFlowId == registerId);
            //.SingleOrDefaultAsync(x => x.CashFlowId == registerId);
            
        return registerResult;
    }
    public async Task<CashFlow> UpdateCashFlowAsync(CashFlow register)
    {
        var local = _dbContext.CashFlow.
            AsNoTracking().
            FirstOrDefault(entry => 
                entry
                .CashFlowId
                .Equals(register.CashFlowId));
                        
        _dbContext.CashFlow.Update(register);
        
        return await Task.FromResult(register);
    }
    public void Dispose()
    {
        _dbContext.Dispose();
        GC.SuppressFinalize(this);
    }
}