using System;
using System.Collections.Generic;

namespace SalesTaxes
{
    class Program
    {
        private const decimal SALES_TAX = 0.10m;
        private const decimal IMPORT_CHARGE = 0.05m;
        private const decimal ROUNDING_VALUE = 0.05m;

        readonly static List<InventoryItem> inventory = new List<InventoryItem>()
        {
            new InventoryItem(false, false, "Book"),
            new InventoryItem(false, true, "Music CD"),
            new InventoryItem(false, false, "Chocolate Bar"),
            new InventoryItem(true, false, "Imported box of chocolates"),
            new InventoryItem(true, true, "Imported bottle of perfume"),
            new InventoryItem(false, true, "Bottle of Perfume"),
            new InventoryItem(false, false, "Packet of headache pills")
        };
        static List<ShoppingBasketItem> shoppingBasket = new List<ShoppingBasketItem>();

        private static void CalculateTotals()
        {
            decimal salesTax = 0.0m;
            decimal itemTotal = 0.0m;
            decimal basketTotal = 0.0m;

            shoppingBasket.ForEach(sbItem =>
            {
                bool isImported = inventory.Find(x => x.ItemName.ToUpper() == sbItem.Item.ToUpper()).IsImported;
                bool isTaxable =  inventory.Find(x => x.ItemName.ToUpper() == sbItem.Item.ToUpper()).IsTaxable;
                decimal itemTax = 0.0m;
                decimal itemImport = 0.0m;
                itemTotal = sbItem.Cost * sbItem.Quantity;
                
                if (isTaxable)
                {
                    itemTax = Math.Ceiling( (itemTotal*SALES_TAX)  /ROUNDING_VALUE) * ROUNDING_VALUE;
                    salesTax += itemTax;
                }
                if (isImported)
                {
                    itemImport = Math.Ceiling ( ( sbItem.Cost * IMPORT_CHARGE) / ROUNDING_VALUE) * ROUNDING_VALUE;
                    sbItem.Cost = sbItem.Cost + itemImport ;
                    salesTax += (itemImport * sbItem.Quantity)
                    ;
                }
                itemTotal = itemTotal + itemTax + (itemImport * sbItem.Quantity);
                sbItem.Total = itemTotal;
                basketTotal += Math.Round(itemTotal, 2);
            });
            PrintOutReceipt(salesTax, basketTotal);
        }

        private static void AddtoShoppingBasket(ShoppingBasketItem sbItem)
        {
            if (shoppingBasket.Exists(item => item.Item.ToUpper() == sbItem.Item.ToUpper()))
            {
                int index = shoppingBasket.FindIndex(s => s.Item.ToUpper() == sbItem.Item.ToUpper());
                int qty = shoppingBasket[index].Quantity + 1;
                shoppingBasket[index].Quantity = qty;
            }
            else
            {
                shoppingBasket.Add(sbItem);
            } 
        }

        private static void PrintOutReceipt(decimal salesTax, decimal total)
        {
            shoppingBasket.ForEach(s =>
            {
                Console.Write($"{s.Item}: {s.Total}");
                if (s.Quantity > 1)
                {
                    Console.Write($" ({s.Quantity} @ {s.Cost})");
                }
                Console.WriteLine();
            });
            Console.WriteLine($"Sales Taxes: {salesTax}");
            Console.WriteLine($"Total: {total}");
            
        }

        internal static void Main(string[] args)
        {
            string entry = "exit";
            int qtyLocation = 0;
            int qty = 0;
            int itemLocation = 0;
            string item = "";
            int costLocation = 0;
            decimal cost = 0.0m;

            do
            {
                Console.WriteLine("Enter next item, type 'exit' when finished:");
                Console.Write("Item:  ");
                entry = Console.ReadLine();
                if (entry.ToUpper() != "EXIT" && entry.Length != 0)
                {
                    try
                    {
                        qtyLocation = entry.IndexOf(' ');
                        qty = Int32.Parse(entry.Substring(0, qtyLocation));
                        itemLocation = entry.LastIndexOf(" at ");
                        item = entry.Substring(qtyLocation, itemLocation).Trim();
                        costLocation = entry.LastIndexOf(' ');
                        cost = Convert.ToDecimal(entry.Substring(costLocation));
                        ShoppingBasketItem shoppingBasketItem = new ShoppingBasketItem(qty, item, cost);
                        AddtoShoppingBasket(shoppingBasketItem);
                    }
                    catch (Exception)
                    {
                        Console.WriteLine("An error has occurred.  Please check your entry and try again.");
                    }
                }
            } while (entry.ToUpper() != "EXIT");

            CalculateTotals();
            Console.Read();
        }
    }
}
