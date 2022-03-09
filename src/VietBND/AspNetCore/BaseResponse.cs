namespace VietBND.AspNetCore
{
    public class BaseResponse
    {
        public bool IsSuccess { get; set; }
        public object Data { get; set; }
        public string[] ErrorMessages { get; set; }
    }
}
