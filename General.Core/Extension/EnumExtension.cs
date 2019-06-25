using System;
using General.Core.Librs;

namespace General.Core.Extension
{
    /// <summary>
    /// 枚举描述类
    /// </summary>
    public static class EnumExtension
    {
        /// <summary>
        /// 获取枚举项的描述
        /// </summary>
        /// <param name="typeOfEnum">枚举类型</param>
        /// <param name="value">枚举项/枚举值</param>
        /// <returns>描述</returns>
        public static string GetEnumDescription(this Type typeOfEnum, object value)
        {
            if (value is int)
            {
                return EnumHelper.GetEnumDescriptionByValue(typeOfEnum, Convert.ToInt16(value));
            }
            else if (value is string)
            {
                return EnumHelper.GetDescriptionByName(typeOfEnum, value.ToString());
            }
            return "";
        }
        /// <summary>
        /// 根据值获取枚举描述（object 扩展）
        /// </summary>
        /// <param name="value">枚举项/枚举值</param>
        /// <param name="typeOfEnum">枚举类型</param>
        /// <returns></returns>
        public static string GetGetEnumDescriptionByValue(this object value, Type typeOfEnum)
        {
            if (value is int)
            {
                return EnumHelper.GetEnumDescriptionByValue(typeOfEnum, Convert.ToInt16(value));
            }
            else if (value is string)
            {
                return EnumHelper.GetDescriptionByName(typeOfEnum, value.ToString());
            }
            return "";

        }
    }
}