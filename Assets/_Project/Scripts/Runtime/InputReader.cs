using UnityEngine;

public class InputReader : IInputReader
{
    private const string HorizontalAxis = "Horizontal";
    private const string VerticalAxis = "Vertical";
    private const string MouseX = "Mouse X";
    private const string MouseY = "Mouse Y";

    public Vector2 Move => new(Input.GetAxis(HorizontalAxis), Input.GetAxis(VerticalAxis));

    public Vector2 Look => new(Input.GetAxisRaw(MouseX), Input.GetAxisRaw(MouseY));

    public bool Jump => Input.GetKeyDown(KeyCode.Space);
}
