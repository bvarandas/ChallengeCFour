using ChallengeCrf.Domain.Interfaces;
using ChallengeCrf.Domain.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Win32;
using System;

namespace ChallengeCrf.Api.Controllers;

[ApiController]
[Route("api/[controller]")]
public class RegisterController : ControllerBase
{
	private readonly ILogger<RegisterController> _logger;
	private readonly IQueueProducer _queueProducer;
    private readonly IQueueConsumer _queueConsumer;

    public RegisterController(ILogger<RegisterController> logger, 
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
    public IActionResult DeleteRegister(int registerId)
    {
        try
        {
            var register = new CashFlow(registerId,string.Empty, 0, string.Empty, DateTime.MinValue, "remove");
            _queueProducer.PublishMessage(register);

            return Accepted();
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, $"{ex.Message}");
            return BadRequest(ex);
        }
    }

    [HttpPost]
    public IActionResult InsertRegister(CashFlow register)
    {
		try
		{
            register.Action = "insert";
			_queueProducer.PublishMessage(register);

			return Accepted(register);
		}
		catch (Exception ex)
		{
			_logger.LogError(ex, $"{ex.Message}");
			return BadRequest(ex);
		}
    }


    [HttpPut]
    public IActionResult UpdateRegister(CashFlow register)
    {
        try
        {
            register.Action = "update";
            _queueProducer.PublishMessage(register);

            return Accepted(register);
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
            var register = new CashFlow() { Action = "getall" };
            _queueProducer.PublishMessage(register);
            
			return Ok();
		}
		catch (Exception ex)
		{
            _logger.LogError(ex, $"{ex.Message}");
            return BadRequest(ex);
        }
	}


    [HttpGet("{registerId}")]
    public IActionResult GetRegister(int registerId)
    {
        try
        {
            var register = new CashFlow() { Action = "get", CashFlowId=registerId };
            //_queueProducer.PublishMessage(register);
            //_registerService.GetRegisterByIDAsync(registerId);

            return Ok(_queueConsumer.RegisterGetById(registerId));
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, $"{ex.Message}");
            return BadRequest(ex);
        }
    }
}