namespace MasterDegree.Dto
{
    public class PagingContent<T>
    {
        public List<T> Content { get; set; } = [];
        public int PageIndex { get; set; }
        public int Count { get; set; }
    }
}
