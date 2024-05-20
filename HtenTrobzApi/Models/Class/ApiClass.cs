namespace HtenTrobzApi.Models
{
    public class LoginResponse
    {
        public int Code { get; set; }
        public string Status
        {
            get
            {
                if (Code == 0) return "Success";
                else return "Failed";
            }
        }
        public DateTimeOffset Expires { get; set; }
    }

    public class ApiInput
    {
        public string endpoint { get; set; } = string.Empty;
        public string code { get; set; } = string.Empty;
        public string type { get; set; } = string.Empty;
    }

    public class ApiReponse<T> where T : class
    {
        public List<T> data { get; set; } = new List<T>();
        public string? error { get; set; }
    }

    public class CustomerInput
    {
        public string CusCode { get; set; } = string.Empty;
        public string CusName { get; set; } = string.Empty;
        public string Address { get; set; } = string.Empty;
        public string Phone { get; set; } = string.Empty;
        public string Note { get; set; } = string.Empty;
    }

    public class UomInput
    {
        public string Name { get; set; } = string.Empty;
        public double Ratio { get; set; }
    }

    public class ItemInput
    {
        public string ItemCode { get; set; } = string.Empty;
        public string ItemName { get; set; } = string.Empty;
        public UomInput? Uom { get; set; }
        public string Type { get; set; } = string.Empty;
    }

    public class SiteInput
    {
        public string JobCode { get; set; } = string.Empty;
        public string JobName { get; set; } = string.Empty;
        public string JobAddress { get; set; } = string.Empty;
    }

    public class TruckInput
    {
        public string CarCode { get; set; } = string.Empty;
        public string CarNo { get; set; } = string.Empty;
    }

    public class DriverInput
    {
        public string DriverCode { get; set; } = string.Empty;
        public string DriverName { get; set; } = string.Empty;
    }

    public class SOInput
    {
        public DateTime VcDate { get; set; }
        public string VcNo { get; set;} = string.Empty;
        public string CusCode { get; set;} = string.Empty;
        public string ContractNo { get; set;} = string.Empty;
        public Factory? Factory { get; set; }
        public MixingStation? MixingStation { get; set; }
        public List<SODetailInput> SoDetails { get; set; } = new List<SODetailInput>();
    }

    public class SODetailInput
    {
        public string ItemCode {  get; set; } = string.Empty;
        public string ItemName {  get; set; } = string.Empty;
        public double Quantity { get; set; }
    }

    public class Factory
    {
        public string Code {  get; set; } = string.Empty;
        public string Name { get; set; } = string.Empty;
    }

    public class MixingStation
    {
        public int Code { get; set; }
        public string Name { get; set; } = string.Empty;
    }
}
