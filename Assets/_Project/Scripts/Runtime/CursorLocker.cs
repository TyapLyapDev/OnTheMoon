using UnityEngine;

public class CursorLocker
{
    public void Lock()
    {
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
    }

    public void Unlock()
    {
        Cursor.lockState = CursorLockMode.Confined;
        Cursor.visible = true;
    }
}