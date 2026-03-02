using Game.Camera;
using UnityEngine;

public class Player : MonoBehaviour
{
    [SerializeField] private Rotator _cameraRotator;
    [SerializeField] private Follower _cameraFollower;

    private ICharacter _character;
    private IInputReader _inputReader;
    private IUpdateService _updateService;

    private void OnEnable() =>
        SubscribeToUpdates();

    private void OnDisable() =>
        UnsubscribeFromUpdates();

    public void Initialize(IInputReader inputReader, ICharacter character, IUpdateService updateService)
    {
        _character = character;
        _inputReader = inputReader;
        _updateService = updateService;

        _cameraRotator.SetTarget(transform);
        _cameraFollower.SetTarget(transform);

        if (enabled)
            SubscribeToUpdates();
    }

    private void SubscribeToUpdates()
    {
        if (_updateService != null)
        {
            _updateService.FixedUpdated += OnFixedUpdate;
            _updateService.Updated += OnUpdate;
        }
    }

    private void UnsubscribeFromUpdates()
    {
        if (_updateService != null)
        {
            _updateService.FixedUpdated -= OnFixedUpdate;
            _updateService.Updated -= OnUpdate;
        }
    }

    private void OnFixedUpdate(float deltaTime)
    {
        if (_inputReader != null)
            _character?.Move(_inputReader.Move);
    }

    private void OnUpdate(float deltaTime)
    {
        if (_inputReader == null)
            return;

        _cameraRotator.ApplyLook(_inputReader.Look, deltaTime);
        _character?.Look(_inputReader.Look, _cameraRotator.Sensitivity, deltaTime);

        if (_inputReader.Jump)
            _character?.Jump();
    }
}