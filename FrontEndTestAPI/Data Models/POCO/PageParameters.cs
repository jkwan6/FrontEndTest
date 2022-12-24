namespace FrontEndTestAPI.Data_Models.POCO
{
    public class PageParameters
    {
        public int pageIndex { get; set; } = 0;
        public int pageSize { get; set; } = 10;
        public string? sortColumn { get; set; }
        public string? sortOrder { get; set; }
        public string? filterColumn { get; set; }
        public string? filterQuery { get; set; }
    }
}
