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
}
