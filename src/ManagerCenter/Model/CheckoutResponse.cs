namespace ManagerCenter.Model
{
    public class CheckoutResponse
    {
        public int Code { get; set; }

        public string Message { get; set; }

        public bool Success { get; set; }

        public CheckOutDetail Data { get; set; }
    }

    public class CheckOutDetail
    {
        public bool HasSucceed { get; set; }

        public string ErrorMessages { get; set; }
    }
}
