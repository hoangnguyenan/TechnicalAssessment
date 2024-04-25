using Customer.WebAPI.Enums;

namespace Customer.WebAPI.Dtos
{
    public class QuerySearchDefault
    {
        private int _size;

        public virtual string SearchKey { get; set; }

        /// <summary>
        /// default = 1
        /// </summary>
        public virtual int Page { get; set; } = 1;

        /// <summary>
        /// default = 10
        /// </summary>
        public virtual int Size
        {
            get
            {
                return (_size == 0) ? 10 : _size;
            }
            set
            {
                _size = (value == 0) ? int.MaxValue : value;
            }
        }

        public virtual string SortField { get; set; }

        public virtual SortTypeEnum SortType { get; set; }

        public int GetSkip()
        {
            return (Page - 1) * Size;
        }

        public int GetTake()
        {
            return Size;
        }
    }
}