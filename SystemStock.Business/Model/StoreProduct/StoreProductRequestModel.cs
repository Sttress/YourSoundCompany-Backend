namespace SystemStock.Business.Model.StoreProduct
{
    public class StoreProductRequestModel
    {
        public long StoreId { get; set; }
        public List<StoreProductAmountRequest>? Products { get; set; }
    }

    public class StoreProductAmountRequest
    {
        public long ProductId { get; set; }
        public long Amount { get; set; }
    }
}
