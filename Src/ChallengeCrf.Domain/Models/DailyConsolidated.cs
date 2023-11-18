using MongoDB.Bson.Serialization.Attributes;
using MongoDB.Bson;
using ProtoBuf;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ChallengeCrf.Domain.Models;

[Table("tb_DailyConsolidated")]
[ProtoContract]
public sealed class DailyConsolidated
{
    [BsonRepresentation(BsonType.ObjectId)]
    public ObjectId Id { get; set; }

    public int DailyConsolidatedId { get; set; }

    [ProtoMember(2)]
    public double AmountCredit { get; set; }

    [ProtoMember(3)]
    public double AmountDebit { get; set; }

    [ProtoMember(4)]
    public DateTime Date { get; set; }

    [ProtoMember(4)]
    public double AmoutTotal { get; set; }

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
