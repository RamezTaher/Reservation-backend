namespace Zamazimah.Generic.Models
{
    public class ResultApiModel<T>
    {
        public T Data { get; set; }
        public int TotalCount { get; set; }
        public int PageCount { get; set; }
        public int CurrentPage { get; set; }
        public bool Success { get; set; }
        public int OtherTotal { get; set; }
        public int NumberOfDistributedBottles { get; set; }
        public int NumberOfTripsDone { get; set; }

    }
}