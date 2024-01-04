using ChallengeCrf.Domain.Models;
using ChallengeCrf.Domain.Interfaces;
using ChallengeCrf.Infra.Data.Context;
using MongoFramework;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;

namespace ChallengeCrf.Infra.Data.Repository;
public class CashFlowRepository: ICashFlowRepository
{
    private readonly ILogger<CashFlowRepository> _logger;
    protected readonly CashFlowContext _dbContext;
    public CashFlowRepository(CashFlowContext dbContext, ILogger<CashFlowRepository> logger)
    {
        _dbContext = dbContext;
        _logger = logger;
    }
    public async Task<bool> AddCashFlow(CashFlow register)
    {
        _logger.LogInformation("Inserindo de CashFlow no banco de dados");
        var ret = false;
        try
        {
             _dbContext.CashFlow.Add(register);
            ret = true;
        }catch (Exception ex) 
        {
            _logger.LogError(ex.Message);
        }

        return await Task.FromResult(ret);
    }

    public async Task<bool> DeleteCashFlowAsync(string registerId)
    {
        var ret = false;
        try
        {
            var filtered = await _dbContext
                .CashFlow
                .ToAsyncEnumerable()
                .SingleOrDefaultAsync(x => x.CashFlowId == registerId);
            _dbContext.CashFlow.Remove(filtered);

            ret = true;

        }catch (Exception ex) {
            _logger.LogError(ex.Message);
        }
        return await Task.FromResult(ret);
    }
    public async Task<IAsyncEnumerable<CashFlow>> GetAllCashFlowAsync()
    {
        IAsyncEnumerable<CashFlow>? registerList = null;
        try
        {
            _logger.LogInformation("Coletando  GetAllCashFlowAsync no MongoDB");
            
            registerList = _dbContext.CashFlow
                .AsNoTracking()
                .ToAsyncEnumerable();

            _logger.LogInformation("Conseguiu Coletar do MongoDB em GetAllCashFlowAsync ");
        }
        catch (Exception ex)
        {
            _logger.LogError(ex.Message);
        }
        return registerList;

    }
    public async Task<CashFlow> GetCashFlowByIDAsync(string cashFlowId)
    {
        var registerResult = await _dbContext
            .CashFlow
            .ToAsyncEnumerable()
            .SingleOrDefaultAsync(x => x.CashFlowId == cashFlowId);
            
        return registerResult;
    }

    public async Task<IAsyncEnumerable<CashFlow>> GetCashFlowByDateAsync(DateTime date)
    {
        var registerResult = _dbContext
            .CashFlow
            .AsNoTracking()
            .ToAsyncEnumerable()
            .Where(x => x.Date.ToString("dd/MM/yyyy") == date.ToString("dd/MM/yyyy"));
        
        return registerResult;
    }

    public async Task<bool> UpdateCashFlowAsync(CashFlow register)
    {
        var ret = false;
        var local = _dbContext.CashFlow.
            AsNoTracking().
            FirstOrDefault(entry => 
                entry
                .CashFlowId
                .Equals(register.CashFlowId));
                        
        _dbContext.CashFlow.Update(register);
        
        return await Task.FromResult(ret);
    }
    public void Dispose()
    {
        _dbContext.Dispose();
        GC.SuppressFinalize(this);
    }
}