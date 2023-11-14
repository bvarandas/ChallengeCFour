using System;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace ChallengeCrf.Application.ViewModel;

public class CashFlowViewModel
{
    [Key]
    public string CashFlowId { get; set; }=string.Empty;

    [Required(ErrorMessage = "A Descrição é necessária")]
    [MinLength(2)]
    [MaxLength(100)]
    [DisplayName("Description")]
    public string Description { get; set; } = string.Empty;

    [Required(ErrorMessage = "O valor é necessário")]
    [MinLength(1)]
    [MaxLength(1)]
    [DisplayName("Amount")]
    public double Amount { get; set; }


    [Required(ErrorMessage = "O Lançamento (Debito ou credito) é necessário")]
    [MinLength(1)]
    [MaxLength(100)]
    [DisplayName("Entry")]
    public string Entry { get; set; } = string.Empty;

    [Required(ErrorMessage = "A Data é necessária")]
    [MinLength(1)]
    [MaxLength(100)]
    [DisplayName("Date")]
    public DateTime Date { get; set; }
}
