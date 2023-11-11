﻿namespace ChallengeCrf.Domain.Commands;

public abstract class CashFlowCommand : Command
{
    public string RegisterId { get; protected set; } = string.Empty;
    public string Description { get; protected set; } = string.Empty;
    public double Amount { get; protected set; }
    public string Entry { get; protected set; } = string.Empty;
    public DateTime Date { get; protected set; }
    public string Action { get;protected set; } = string.Empty;
}
