namespace ManagerCenter.Model
{
    public class GenerateRequest
    {
        public int IsUsedPrePay { get; set; }

        public int PayTypeID { get; set; }

        public int PointPay { get; set; }

        public int ShippingAddressID { get; set; }
    }
}
