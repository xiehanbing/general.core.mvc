using System;

namespace General.Framework
{
    public class MyException:Exception
    {
        public int  Code
        { get; set; }
        public MyException(string message):base(message)
        { }

        public MyException(int code, string message) : base(message)
        {
            Code = code;
        }
    }
}