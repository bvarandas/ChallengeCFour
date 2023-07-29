using ProtoBuf;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ChallengeCrf.Domain.Models;

[Table("tb_CashFlow")]
[ProtoContract]
public sealed class CashFlow 
{
    [Key]
    [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
    [ProtoMember(1)]
    public int CashFlowId { get; set; }
    [ProtoMember(2)]
    public string Description { get; set; } = string.Empty;
    
    [ProtoMember(3)]
    public double Amount { get; set; }

    [ProtoMember(4)]
    public string Entry { get; set; }

    [ProtoMember(5)]
    public DateTime Date { get; set; }

    [NotMapped]
    [ProtoMember(6)]
    public string Action { get; set; } =string.Empty;

    public CashFlow(int registerId, string description, double cashValue, string entry, DateTime date, string action)
    {
        CashFlowId = registerId;
        Description = description;
        Amount = cashValue;
        Entry = entry;
        Date = date;
        Action = action;
    }

    public CashFlow(string description, double cashValue, string entry, DateTime date, string action)
    {
        Description = description;
        Amount = cashValue;
        Entry = entry;
        Date = date;
        Action = action;
    }

    public CashFlow() { }
}