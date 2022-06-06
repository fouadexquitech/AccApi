namespace AccApi.Repository.Interfaces
{
    public interface ICurrencyConverterRepository
    {
        public double GetCurrencyExchange(string localCurrency, string foreignCurrency);
    }
}
