using System;
using Common.GameTask;
using UniRx;
using UnityEngine.Assertions;

namespace Common.Service
{
    /// <inheritdoc />
    /// <summary>
    /// Вспомогательная задача на инициализацию сервиса.
    /// </summary>
    public class GameTaskInitService : IGameTask
    {
        private readonly IGameService _gameService;
        
        public GameTaskInitService(IGameService gameService)
        {
            _gameService = gameService;
        }
        
        // ITask
        
        public void Start()
        {
            Assert.IsFalse(_gameService.Ready.Value);
            
            IDisposable d = null;
            d = _gameService.Ready.Subscribe(value =>
            {
                if (!value) return;
                // ReSharper disable once AccessToModifiedClosure
                d?.Dispose();
                Complete.SetValueAndForceNotify(true);
            });
            _gameService.Initialize();
        }

        public void Reset(bool dispatchComplete = false)
        {
            throw new NotImplementedException();
        }

        IReadOnlyReactiveProperty<bool> IGameTask.Complete => Complete;
        
        // \ITask
        
        public BoolReactiveProperty Complete { get; } = new BoolReactiveProperty(false);
    }
}