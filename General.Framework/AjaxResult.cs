namespace General.Framework
{
    public class AjaxResult
    {
        public bool Status { get; set; }
        public string  Message { get; set; }
        public int  ErorCode { get; set; }
        public object Data { get; set; }
    }
}