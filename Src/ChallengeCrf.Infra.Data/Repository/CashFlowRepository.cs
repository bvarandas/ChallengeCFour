using ChallengeCrf.Domain.Models;
using ChallengeCrf.Domain.Interfaces;
using ChallengeCrf.Infra.Data.Context;
using Microsoft.EntityFrameworkCore;

namespace ChallengeCrf.Infra.Data.Repository;

public class CashFlowRepository: ICashFlowRepository
{
    protected readonly CashFlowContext _dbContext;
    public CashFlowRepository(CashFlowContext dbContext)
    {
        _dbContext = dbContext;
    }

    public async Task AddCashFlowAsync(CashFlow register)
    {
        try
        {
            _dbContext.CashFlow.Add(register);
        }
        catch (Exception ex)
        {

        }
    }

    public void DeleteCashFlowAsync(string registerId)
    {
        try
        {
            var filtered = _dbContext.CashFlow.Single(x => x.CashFlowId == registerId);
            _dbContext.CashFlow.Remove(filtered);
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
            registerList =  await _dbContext.CashFlow.ToListAsync();

        }catch (Exception ex)
        {

        }

        return registerList;
    }

    public async Task<CashFlow> GetCashFlowByIDAsync(string registerId)
    {
        CashFlow? registerResult = new CashFlow();
        try
        {
            registerResult =  await _dbContext
                .CashFlow
                .Where(x => x.CashFlowId == registerId)
                .FirstAsync();

        }catch(Exception ex)
        {

        }
        return registerResult;
    }

    public async Task UpdateCashFlowAsync(CashFlow register)
    {
        try
        {
            var local = _dbContext.CashFlow.
                AsNoTracking().
                FirstOrDefault(entry => 
                    entry
                    .CashFlowId
                    .Equals(register.CashFlowId));

            //if (local is not null)
            //{
            //    _dbContext.Entry(local).State = EntityState.Detached;
            //}
            
            _dbContext.CashFlow.Update(register);

            //_dbContext..Entry(register).State = EntityState.Modified;
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
