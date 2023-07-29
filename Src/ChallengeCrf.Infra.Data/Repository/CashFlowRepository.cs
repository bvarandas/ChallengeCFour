using ChallengeCrf.Domain.Models;
using ChallengeCrf.Domain.Interfaces;
using ChallengeCrf.Infra.Data.Context;
using Microsoft.EntityFrameworkCore;

namespace ChallengeCrf.Infra.Data.Repository;

public class CashFlowRepository: ICashFlowRepository
{
    protected readonly DbContextClass _dbContext;
    //protected readonly DbSet<Register> DbSet;

    public CashFlowRepository(DbContextClass dbContext)
    {
        _dbContext = dbContext;
        //DbSet = _dbContext.Set<Register>();
        _dbContext.CashFlows = _dbContext.Set<CashFlow>();
    }

    public async Task AddCashFlowAsync(CashFlow register)
    {
        try
        {
            _dbContext.CashFlows.Add(register);
        }catch (Exception ex)
        {

        }
    }

    public void DeleteCashFlowAsync(int registerId)
    {
        try
        {
            var filtered = _dbContext.CashFlows.Single(x => x.CashFlowId == registerId);
            _dbContext.CashFlows.Remove(filtered);
        }
        catch (Exception ex)
        {

        }
        //_dbContext.SaveChanges();

        //return await Task<bool>.FromResult( result is not null ? true : false);
    }

    public async Task<IEnumerable<CashFlow>> GetAllCashFlowAsync()
    {
        var  registerList = new List<CashFlow>();
        try
        {
            registerList =  await _dbContext.CashFlows.ToListAsync();

        }catch (Exception ex)
        {

        }

        return registerList;
    }

    public async Task<CashFlow> GetCashFlowByIDAsync(int registerId)
    {
        CashFlow? registerResult = new CashFlow();
        try
        {
            registerResult =  await _dbContext.CashFlows.Where(x => x.CashFlowId == registerId).FirstAsync();
        }catch(Exception ex)
        {

        }
        return registerResult;
    }

    public async Task UpdateCashFlowAsync(CashFlow register)
    {
        try
        {
            var local = _dbContext.CashFlows.
                FirstOrDefault(entry => entry.CashFlowId.Equals(register.CashFlowId));

            if (local is not null)
            {
                _dbContext.Entry(local).State = EntityState.Detached;
            }
            
            var result = _dbContext.CashFlows.Update(register);

            _dbContext.Entry(register).State = EntityState.Modified;
        }
        catch (Exception ex)
        {

        }
        //bContext.SaveChanges();
        
        //return await Task.FromResult(result.Entity);
    }

    public void Dispose()
    {
        _dbContext.Dispose();
        GC.SuppressFinalize(this);
    }
}
