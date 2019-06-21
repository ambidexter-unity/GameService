using UniRx;

namespace Common.Service
{
    public interface IGameService
    {
        void Initialize();
        IReadOnlyReactiveProperty<bool> Ready { get; }
    }
}