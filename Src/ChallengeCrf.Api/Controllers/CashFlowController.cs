using ChallengeCrf.Domain.Interfaces;
using ChallengeCrf.Domain.Models;
using Microsoft.AspNetCore.Mvc;

namespace ChallengeCrf.Api.Controllers;

[ApiController]
[Route("api/[controller]")]
public class CashFlowController : ControllerBase
{
	private readonly ILogger<CashFlowController> _logger;
	private readonly IQueueProducer _queueProducer;
    private readonly IQueueConsumer _queueConsumer;

    public CashFlowController(ILogger<CashFlowController> logger, 
        IQueueProducer queueProducer,
        IQueueConsumer queueConsumer        )
	{
		_logger = logger;
		_queueProducer = queueProducer;
        _queueConsumer = queueConsumer;
        //_registerService = registerService;
        _queueConsumer.ExecuteAsync();
    }

    [HttpDelete("{registerId}")]
    public IActionResult DeleteRegister(int cashFlowId)
    {
        try
        {
            var cash = new CashFlow(cashFlowId, string.Empty, 0, string.Empty, DateTime.MinValue, "remove");
            _queueProducer.PublishMessage(cash);

            return Accepted();
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, $"{ex.Message}");
            return BadRequest(ex);
        }
    }

    [HttpPost]
    public IActionResult InsertRegister(CashFlow cash)
    {
		try
		{
            cash.Action = "insert";
			_queueProducer.PublishMessage(cash);

			return Accepted(cash);
		}
		catch (Exception ex)
		{
			_logger.LogError(ex, $"{ex.Message}");
			return BadRequest(ex);
		}
    }


    [HttpPut]
    public IActionResult UpdateRegister(CashFlow cash)
    {
        try
        {
            cash.Action = "update";
            _queueProducer.PublishMessage(cash);

            return Accepted(cash);
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, $"{ex.Message}");
            return BadRequest(ex);
        }
    }

    [HttpGet]
	public IActionResult GetListRegister()
    {
        try
		{
            var cash = new CashFlow() { Action = "getall" };
            _queueProducer.PublishMessage(cash);
            
			return Ok();
		}
		catch (Exception ex)
		{
            _logger.LogError(ex, $"{ex.Message}");
            return BadRequest(ex);
        }
	}


    [HttpGet("{cashFlowId}")]
    public IActionResult GetRegister(int cashFlowId)
    {
        try
        {
            var register = new CashFlow() { Action = "get", CashFlowId=cashFlowId };
            //_queueProducer.PublishMessage(register);
            //_registerService.GetRegisterByIDAsync(registerId);

            return Ok(_queueConsumer.RegisterGetById(cashFlowId));
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, $"{ex.Message}");
            return BadRequest(ex);
        }
    }
}