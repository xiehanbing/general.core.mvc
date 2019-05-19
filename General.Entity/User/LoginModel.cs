using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace General.Entity.User
{
    public class LoginModel
    {
        [Required(ErrorMessage = "账号不能为空")]
        public string  Account { get; set; }
        [Required(ErrorMessage = "密码不能为空")]
        public string  Password { get; set; }
        /// <summary>
        /// 随机数
        /// </summary>
        public string  Range { get; set; }
    }
}
