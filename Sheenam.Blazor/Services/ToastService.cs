namespace Sheenam.Blazor.Services
{
    public class ToastService
    {
        public event Action<string, string, ToastType>? OnShow;

        public void ShowSuccess(string message, string title = "Muvaffaqiyatli!")
        {
            OnShow?.Invoke(title, message, ToastType.Success);
        }

        public void ShowError(string message, string title = "Xato!")
        {
            OnShow?.Invoke(title, message, ToastType.Error);
        }

        public void ShowWarning(string message, string title = "Ogohlantirish!")
        {
            OnShow?.Invoke(title, message, ToastType.Warning);
        }

        public void ShowInfo(string message, string title = "Ma'lumot")
        {
            OnShow?.Invoke(title, message, ToastType.Info);
        }
    }

    public enum ToastType
    {
        Success,
        Error,
        Warning,
        Info
    }
}