using System.Collections.Generic;

namespace ManagerCenter.Model
{
    public class GetCartResponse
    {
        public bool Success { get; set; }

        public string Message { get; set; }

        public CartResponseDetail Data { get; set; }
    }

    public class CartResponseDetail
    {
        public bool HasSucceed { get; set; }

        public ReturnData ReturnData { get; set; }
    }

    public class ReturnData
    {
        public List<OrderItemGroup> OrderItemGroupList { get; set; }
    }

    public class OrderItemGroup
    {
        public bool IsSelected { get; set; }

        public int PackageType { get; set; }

        public List<ProductItem> ProductItemList { get; set; }
    }

    public class ProductItem
    {
        public int ProductSysNo { get; set; }

        public string ProductName { get; set; }
    }
}
