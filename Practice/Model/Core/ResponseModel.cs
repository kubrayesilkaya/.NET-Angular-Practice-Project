namespace Practice.Model.Core
{
    public class ResponseModel<T>
    {
        public bool Result { get; set; }
        public string Message { get; set; }
        public int? HttpStatusCode { get; set; } = 200;

        private T _data;
        public T Data
        {
            get { return _data; }
            set { _data = value; }
        }
    }
}
