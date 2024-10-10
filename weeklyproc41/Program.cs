using AssetTracking;
using System.Globalization;


class Program
{
    static void Main(string[] args)
    {
        List<Asset> assets = new List<Asset>();
        Dictionary<OfficeLocation, decimal> currencyConversionRates = new Dictionary<OfficeLocation, decimal>();

        // Set the currencies rate depends on location in the Enum list
        Console.WriteLine("Enter the currency conversion rates for different offices(USA:1.0m , SPAIN:0.89m , SWEDEN:10.50m):");
        foreach (OfficeLocation location in Enum.GetValues(typeof(OfficeLocation)))
        {
            Console.Write($"Conversion rate for {location} (USD to local {location}): ");
            decimal rate = decimal.Parse(Console.ReadLine(), CultureInfo.InvariantCulture);
            currencyConversionRates[location] = rate;
        }

        // Input assets from the user
        Console.WriteLine("\n--- Add Assets ---");

        while (true)
        {
            Console.Write("Enter asset type (Laptop/Phone or type 'exit' to finish): ");
            string assetType = Console.ReadLine().ToLower();

            if (assetType == "exit")
                break;

            Asset asset;

            if (assetType == "laptop")
            {
                asset = new Laptop();
            }
            else if (assetType == "phone")
            {
                asset = new Phone();
            }
            else
            {
                Console.WriteLine("Invalid asset type. Try again.");
                continue;
            }

            // Input common asset details
            Console.Write("Enter brand name: ");
            asset.BrandName = Console.ReadLine();

            Console.Write("Enter model name: ");
            asset.ModelName = Console.ReadLine();

            Console.Write("Enter purchase date (yyyy-mm-dd): ");
            asset.PurchaseDate = DateTime.Parse(Console.ReadLine());

            Console.Write("Enter price in USD: ");
            asset.PriceInUSD = decimal.Parse(Console.ReadLine());

            Console.Write("Enter office location (1: USA, 2: SPAIN, 3: SWEDEN): ");
            int officeChoice = int.Parse(Console.ReadLine());
            asset.Office = (OfficeLocation)(officeChoice - 1);

            assets.Add(asset);

            Console.WriteLine("\nAsset added successfully!\n");
        }

        // Sort by Office, then by Purchase Date
        var sortedAssets = assets.OrderBy(a => a.Office)
                                 .ThenBy(a => a.PurchaseDate)
                                 .ThenBy(a => a.AssetType)
                                 .ToList();

        // Output Asset Tracking List
        Console.WriteLine();
        Console.WriteLine("/****************************************************/");
        Console.WriteLine("/*               MOHAMMED ISMAIL                    */");
        Console.WriteLine("/*             --------------------                 */");
        Console.WriteLine("/*                WEEKLY PROJECT                    */");
        Console.WriteLine("/*                   WEEK 41                        */");
        Console.WriteLine("/****************************************************/");
        Console.WriteLine();
        Console.WriteLine("Type".PadRight(12) + 
                          "Brand".PadRight(12) + 
                          "Model".PadRight(12) + 
                          "Office".PadRight(12) + 
                          "Puchase Date".PadRight(18) + 
                          "Price (USD)".PadRight(15) + 
                          "Currency".PadRight(12) + "Local price today");
        foreach (var asset in sortedAssets)
        {
            decimal convertedPrice = asset.ConvertPrice(currencyConversionRates[asset.Office]);
            string currencySymbol = asset.GetCurrencySymbol(asset.Office);

            Console.ForegroundColor = asset.GetColorByEndOfLife();
            Console.WriteLine($"{asset.AssetType.PadRight(12)} " +
                              $"{asset.BrandName.PadRight(12)} " +
                              $"{asset.ModelName.PadRight(12)} " +
                              $"{asset.Office}".PadRight(12) +
                              $"{asset.PurchaseDate.ToString("yyyy-MM-dd")}".PadRight(18) +  
                              $"{asset.PriceInUSD}".PadRight(15) +
                              $"{currencySymbol.PadRight(12)}" +
                              $"{convertedPrice:0.00}");
            Console.ResetColor(); // Reset the color to default
        }

      
    }
}
 
