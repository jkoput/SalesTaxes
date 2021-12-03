namespace SalesTaxes
{
    public struct InventoryItem
    {
        public bool IsImported { get; set; }
        public bool IsTaxable { get; set; }
        public string ItemName { get; set; }

        public InventoryItem(bool isImported, bool isTaxable, string itemName)
        {
            this.IsImported = isImported;
            this.IsTaxable = isTaxable;
            this.ItemName = itemName;
        }
        
    }
}
