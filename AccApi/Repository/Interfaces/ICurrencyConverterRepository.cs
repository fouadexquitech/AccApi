namespace AccApi.Repository.Interfaces
{
    public interface ICurrencyConverterRepository
    {
        public decimal GetCurrencyExchange(string localCurrency, string foreignCurrency);

    }
}
