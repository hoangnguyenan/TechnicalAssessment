
namespace Customer.WebAPI.Dtos
{
    public class PageResultDto<T>
    {
        public long TotalRecord { get; set; }
        
        public int TotalPage { get; set; }

        public object ExtensionValue { get; set; }
        
        public IEnumerable<T> Items { get; set; }

         public PageResultDto(long totalRecord, int take, IEnumerable<T> items)
        {
            TotalRecord = totalRecord;
            TotalPage = (int)Math.Ceiling((totalRecord / (double)take));
            Items = items;
        }
    }
}