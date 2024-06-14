namespace Bookcase.Models
{
    public class islemSonuc
    {
        public int id { get; set; }
        public short sonuc_id { get; set; }
        public string message { get; set; }
    }

    public class DataGridReturnModel
    {
        public int recordsTotal { get; set; }
        public int recordsFiltered { get; set; }
        public object data { get; set; }
    }
}
