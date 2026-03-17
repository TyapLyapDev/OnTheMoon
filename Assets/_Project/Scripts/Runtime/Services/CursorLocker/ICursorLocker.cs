namespace OnTheMoon.Runtime.Services
{
    public interface ICursorLocker
    {
        void Lock();

        void Unlock();
    }
}