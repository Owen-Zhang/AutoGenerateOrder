namespace ManagerCenter.Model
{
    public class AddCartRequest
    {
        public int SysNo { get; set; }

        public int Qty { get; set; }

        public bool IsPackage { get; set; }
    }
}