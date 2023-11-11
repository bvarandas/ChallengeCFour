using ChallengeCrf.Domain.Models;
using MongoFramework;

namespace ChallengeCrf.Infra.Data.Context;

public class CashFlowContext : MongoDbContext
{
    public MongoDbSet<CashFlow> CashFlow { get; set; } = null!;
    public CashFlowContext(IMongoDbConnection connection) : base(connection)
    {
    }
}
