using WeSplit.Common.Entities.Consumable;

namespace WeSplit.Common.Helpers
{
    public class CurrenyConverter
    {
        public double Convert<TFrom, TTo>(TFrom FromCurrency, TTo ToCurrency, double amount)
            where TFrom : Currency
            where TTo : Currency
        {
            throw new NotImplementedException();
        }
    }
}
