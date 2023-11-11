using ProtoBuf;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ChallengeCrf.Domain.Models;

[Table("tb_DailyConsolidated")]
[ProtoContract]
public sealed class DailyConsolidated
{
    [Key]
    [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
    [ProtoMember(1)]
    public int DailyConsolidatedId { get; set; }

    [ProtoMember(2)]
    public double AmountCredit { get; set; }

    [ProtoMember(3)]
    public double AmountDebit { get; set; }

    [ProtoMember(4)]
    public DateTime Date { get; set; }

    public DailyConsolidated()
    {
    }

    public DailyConsolidated(int dailyConsolidatedId, double amountCredit, double amountDebit, DateTime date)
    {
        DailyConsolidatedId = dailyConsolidatedId;
        AmountCredit = amountCredit;
        AmountDebit = amountDebit;
        Date = date;
    }
}
