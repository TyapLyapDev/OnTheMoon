using UnityEngine;

namespace OnTheMoon.Runtime.Services
{
    public class CursorLocker : ICursorLocker
    {
        public void Lock()
        {
            Cursor.lockState = CursorLockMode.Locked;
            Cursor.visible = false;
        }

        public void Unlock()
        {
            Cursor.lockState = CursorLockMode.None;
            Cursor.visible = true;
        }
    }
}