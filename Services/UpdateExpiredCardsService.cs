using API.Interfaces;

namespace API.Services;

public class UpdateExpiredCardsService(IScratchCardRepository scratchCardRepository) : IHostedService, IDisposable
{
    private Timer? _timer;
    private readonly IScratchCardRepository _scratchCardRepository = scratchCardRepository;

    public Task StartAsync(CancellationToken cancellationToken)
    {
        #nullable disable
        _timer = new Timer(UpdateExpiredCards, null, TimeSpan.Zero, TimeSpan.FromDays(1));
        #nullable restore
        return Task.CompletedTask;
    }

    private void UpdateExpiredCards(object state)
    {
        _scratchCardRepository.UpdateExpiredCards().Wait();
    }

    public Task StopAsync(CancellationToken cancellationToken)
    {
        _timer?.Change(Timeout.Infinite, 0);
        return Task.CompletedTask;
    }

    public void Dispose()
    {
        _timer?.Dispose();
    }
}