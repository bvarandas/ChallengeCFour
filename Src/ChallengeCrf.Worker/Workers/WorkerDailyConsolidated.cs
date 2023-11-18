]using ChallengeCrf.Domain.Interfaces;
using ChallengeCrf.Domain.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ChallengeCrf.Queue.Worker.Workers;

public class WorkerDailyConsolidated : BackgroundService
{
    private readonly IDailyConsolidatedService _dcService;
    private readonly ILogger<WorkerDailyConsolidated> _logger;

    public WorkerDailyConsolidated(ILogger<WorkerDailyConsolidated> logger, IDailyConsolidatedService dcService)
    {
        _dcService = dcService;
        _logger = logger;
    }

    protected override async Task ExecuteAsync(CancellationToken stoppingToken)
    {
        while (!stoppingToken.IsCancellationRequested)
        {
            await Task.Delay(5000, stoppingToken);
        }
    }
}
