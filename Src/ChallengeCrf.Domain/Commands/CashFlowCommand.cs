using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ChallengeCrf.Domain.Commands;

public abstract class CashFlowCommand : Command
{
    public int RegisterId { get; protected set; }
    public string Description { get; protected set; } = string.Empty;
    public double Amount { get; protected set; }
    public string Entry { get; protected set; }
    public DateTime Date { get; protected set; }
    public string Action { get;protected set; } = string.Empty;
}
