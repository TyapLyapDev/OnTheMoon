using GameCharacter;
using UnityEngine;

public class GameBootstrap : MonoBehaviour
{
    [SerializeField] private Player _player;
    [SerializeField] private Character _character;
    [SerializeField] private UpdateService _updateService;

    private void Start()
    {
        CursorLocker cursorLocker = new();
        cursorLocker.Lock();

        IInputReader reader = new InputReader();
        _character.Initialize(_updateService);
        _player.Initialize(reader, _character, _updateService);
    }
}