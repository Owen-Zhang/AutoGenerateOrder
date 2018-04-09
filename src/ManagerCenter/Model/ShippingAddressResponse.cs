using System.Collections.Generic;

namespace ManagerCenter.Model
{
    public class ShippingAddressResponse
    {
        public bool Success { get; set; }

        public string Message { get; set; }

        public List<Address> Data { get; set; }
    }

    public class Address
    {
        public int SysNo { get; set; }

        public bool IsDefault { get; set; }
    }
}
