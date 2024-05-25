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

    public class VendorInput
    {
        public string VendorCode { get; set; } = string.Empty;
        public string VendorName { get; set; } = string.Empty;
        public string Address { get; set; } = string.Empty;
        public string Phone { get; set; } = string.Empty;
        public string Note { get; set; } = string.Empty;
    }

    public class UomInput
    {
        public string Name { get; set; } = string.Empty;
        public double Ratio { get; set; }
    }

    public class CategoryInput
    {
        public string Name { get; set; } = string.Empty;
        public string TypeMat { get; set; } = string.Empty;
    }

    public class ItemInput
    {
        public string ItemCode { get; set; } = string.Empty;
        public string ItemName { get; set; } = string.Empty;
        public string Settlement { get; set; } = string.Empty;
        public string SaleDescription { get; set; } = string.Empty;
        public string Note { get; set; } = string.Empty;
        public UomInput? Uom { get; set; }
        public CategoryInput? Category {  get; set; }
        public int IsConcrete { get; set; }
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
        public double? Capacity {  get; set; }
        public CodeName? Driver { get; set; }
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
        public CodeName? Customer { get; set; }
        public List<SODetailInput> SoDetails { get; set; } = new List<SODetailInput>();
    }

    public class SODetailInput
    {
        public string ItemCode {  get; set; } = string.Empty;
        public string ItemName {  get; set; } = string.Empty;
        public double Quantity { get; set; }
    }

    public class CodeName
    {
        public string Code { get; set; } = string.Empty;
        public string Name { get; set; } = string.Empty;
    }

    public class TicketDto
    {
        public string YourID { get; set; } = string.Empty;
        public string CusID { get; set; } = string.Empty;
        public string JobID { get; set; } = string.Empty;
        public string VcNo { get; set; } = string.Empty;
        public string VehicleID { get; set; } = string.Empty;
        public string DriverID { get; set; } = string.Empty;
        public string ContractID { get; set; } = string.Empty;
        public string Note { get; set; } = string.Empty;
        public DateTime VcDate { get; set; }
        public string Status { get; set; } = string.Empty;
        public List<MaterialArchDto> Items { get; set; } = new List<MaterialArchDto>();
    }

    public class MaterialArchDto
    {
        public string Code { get; set; } = string.Empty;
        public double Sl_Dat { get; set; }
        public double Quantity { get; set; }
        public double AccumulatedQTY { get; set; }
        public string Uom { get; set; } = string.Empty;
    }

    public class TicketIssue
    {
        public string YourID { get; set; } = string.Empty;
        public string CusID { get; set; } = string.Empty;
        public string JobID { get; set; } = string.Empty;
        public string VcNo { get; set; } = string.Empty;
        public string VehicleID { get; set; } = string.Empty;
        public string DriverID { get; set; } = string.Empty;
        public string Note { get; set; } = string.Empty;
        public string Status { get; set; } = string.Empty;
        public string ProductID { get; set; } = string.Empty;
        public List<MaterialArchIssue> Items { get; set; } = new List<MaterialArchIssue>();
    }

    public class MaterialArchIssue
    {
        public string Code { get; set; } = string.Empty;
        public string Name { get; set; } = string.Empty;
        public double Quantity { get; set; }
        public string Uom { get; set; } = string.Empty;
    }

}
