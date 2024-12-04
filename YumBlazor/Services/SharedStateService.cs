namespace YumBlazor.Services
{
    public class SharedStateService
    {
        public event Action OnChange;
        private int _cartCount = 0;

        public int CartCount
        {
            get => _cartCount; 
            set
            {
                _cartCount = value;
                NotifyStateChanged();
            } 
        }

        private void NotifyStateChanged() => OnChange?.Invoke();

    }
}
