﻿using ChallengeCrf.Domain.Events;
using FluentValidation.Results;

namespace ChallengeCrf.Domain.Commands;

public abstract  class Command : Message
{
    public DateTime Timestamp { get; private set; }
    public ValidationResult ValidationResult { get; set; } = null!;

    protected Command() 
    { 
        Timestamp = DateTime.Now;
    }

    public abstract bool IsValid();
}
