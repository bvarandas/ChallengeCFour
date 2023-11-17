using ChallengeCrf.Domain.Models;
using ChallengeCrf.Domain.Interfaces;
using ChallengeCrf.Infra.Data.Context;
//using Microsoft.EntityFrameworkCore;
using MongoFramework;
using Microsoft.EntityFrameworkCore;
using Amazon.Runtime.Internal.Util;
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
    public void AddCashFlow(CashFlow register)
    {
        _logger.LogInformation("Inserindo no banco de dados");
        try
        {
            _dbContext.CashFlow
                .Add(register);
        }catch (Exception ex) {
            _logger.LogError(ex.Message);
        }
    }
    public async Task DeleteCashFlowAsync(string registerId)
    {
        try
        {
            var filtered = await _dbContext
                .CashFlow
                .ToAsyncEnumerable()
                .SingleOrDefaultAsync(x => x.CashFlowId == registerId);
            _dbContext.CashFlow.Remove(filtered);
        }catch (Exception ex) {
            _logger.LogError(ex.Message);
        }
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