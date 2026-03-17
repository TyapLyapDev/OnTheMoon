using OnTheMoon.Runtime.Game;
using OnTheMoon.Runtime.Services;
using OnTheMoon.Runtime.Services.PrefabComponentProvider;
using OnTheMoon.Runtime.Services.PrefabComponentProvider.Config;
using UnityEngine;
using VContainer;
using VContainer.Unity;

namespace OnTheMoon.Runtime.DI
{
    public class GameScope : LifetimeScope
    {
        [SerializeField] private PlayerStartPosition _playerStartPosition;
        [SerializeField] private CameraRoot _cameraRoot;
        [SerializeField] private GravitySource _gravitySource;
        [SerializeField] private UpdateService _updateService;
        [SerializeField] private PrefabComponentProviderConfig _prefabComponentProviderConfig;
        [SerializeField] private CharacterConfig _characterConfig;
        [SerializeField] private ViewpointPresenceConfig _viewpointPresenceConfig;

        protected override void Configure(IContainerBuilder builder)
        {
            builder.RegisterEntryPoint<GameBootstrap>(Lifetime.Singleton);

            builder.RegisterComponent(_playerStartPosition);
            builder.RegisterComponent<ICameraRoot>(_cameraRoot);
            builder.RegisterComponent<IGravitySource>(_gravitySource);
            builder.RegisterComponent<IPrefabComponentProviderConfig>(_prefabComponentProviderConfig);
            builder.RegisterComponent<ICharacterConfig>(_characterConfig);
            builder.RegisterComponent<IViewpointPresenceConfig>(_viewpointPresenceConfig);
            builder.RegisterComponent<IUpdateService>(_updateService);

            builder.Register<IViewpointPresenceStrategyFactory, ViewpointPresenceStrategyFactory>(Lifetime.Singleton);
            builder.Register<IPrefabComponentProvider, PrefabComponentProvider>(Lifetime.Singleton);
            builder.Register<ICursorLocker, CursorLocker>(Lifetime.Singleton);
            builder.Register<IInputReader, InputReader>(Lifetime.Singleton);
            builder.Register<ICharacterBuilder, CharacterBuilder>(Lifetime.Transient);
            builder.Register<IViewpointPresenceSwitcher, ViewpointPresenceSwitcher>(Lifetime.Singleton);
        }
    }
}