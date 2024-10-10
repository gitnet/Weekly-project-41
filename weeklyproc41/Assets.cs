using System;
using System.Collections.Generic;
using System.Linq;

namespace AssetTracking
{
    // Enum to represent different types of offices
    enum OfficeLocation
    {
        USA,
        SPAIN,
        SWEDEN
    }

    // Base class for all assets
    abstract class Asset
    {
        public string BrandName { get; set; }
        public string ModelName { get; set; }
        public DateTime PurchaseDate { get; set; }
        public decimal PriceInUSD { get; set; }
        public OfficeLocation Office { get; set; }

        public abstract string AssetType { get; }

        public decimal ConvertPrice(decimal conversionRate)
        {
            return PriceInUSD * conversionRate;
        }

        public string GetCurrencySymbol(OfficeLocation office)
        {
            switch (office)
            {
                case OfficeLocation.USA:
                    return "USD";
                case OfficeLocation.SPAIN:
                    return "EUR";
                case OfficeLocation.SWEDEN:
                    return "SEK";
                default:
                    return "USD";
            }
        }

        // Check if the asset is near end of life (3 years)
        public ConsoleColor GetColorByEndOfLife()
        {
            var endOfLifeDate = PurchaseDate.AddYears(3);
            var timeLeft = endOfLifeDate - DateTime.Now;
        
            if (Math.Abs(timeLeft.TotalDays) <= 90) // Less than 3 months
                return ConsoleColor.Red;
            else if (Math.Abs(timeLeft.TotalDays) <= 180) // Less than 6 months
                return ConsoleColor.Yellow;
            else
                return ConsoleColor.Green;
        }
    }

    // Laptop class derived from Asset
    class Laptop : Asset
    {
        public override string AssetType => "Laptop";
    }

    // Phone class derived from Asset
    class Phone : Asset
    {
        public override string AssetType => "Phone";
    }

}