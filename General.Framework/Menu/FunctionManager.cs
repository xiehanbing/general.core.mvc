using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Runtime.Loader;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore.Internal;

namespace General.Framework.Menu
{
    public class FunctionManager
    {
        /// <summary>
        /// 获取菜单列表
        /// </summary>
        /// <returns></returns>
        public static List<FunctionAttribute> GetFunctionLists()
        {
            var assembly = AssemblyLoadContext.Default.LoadFromAssemblyName(new AssemblyName("General.Mvc"));
            List<FunctionAttribute> list = new List<FunctionAttribute>();
            var types = assembly.GetTypes();
            foreach (var type in types)
            {
                string typeName = type.FullName.ToLower();
                if (typeName.EndsWith("controller"))
                {

                    var funAttlist = type.GetCustomAttributes<FunctionAttribute>(false);
                    FunctionAttribute father = null;
                    if (funAttlist != null && EnumerableExtensions.Any(funAttlist))
                    {
                        foreach (var fun in funAttlist)
                        {
                            if (string.IsNullOrEmpty(fun.SysResource))
                            {
                                fun.SysResource = type.FullName;
                            }

                            father = fun;
                            list.Add(fun);
                            break;
                        }
                    }
                    //获取action 
                    var members = type.FindMembers(MemberTypes.Method, BindingFlags.Public, Type.FilterName, "*");
                    if (members != null && EnumerableExtensions.Any(members))
                    {
                        foreach (var m in members)
                        {
                            var funs = m.GetCustomAttributes<FunctionAttribute>(false);
                            foreach (var fun in funs)
                            {
                                if (string.IsNullOrEmpty(fun.SysResource))
                                {
                                    fun.SysResource = type.FullName + "." + m.Name;
                                }

                                fun.Controller = type.Name.Replace("Controller", "");
                                fun.Action = m.Name;

                                if (string.IsNullOrEmpty(fun.FatherResource))
                                {
                                    if (father != null)
                                    {
                                        fun.FatherResource = father.SysResource;
                                    }
                                }
                                object[] routes = m.GetCustomAttributes(typeof(FunctionAttribute), false);
                                if (routes != null && EnumerableExtensions.Any(routes))
                                {
                                    var route = Enumerable.First(routes) as RouteAttribute;
                                    fun.RouteName = route.Name;
                                }

                                list.Add(fun);
                                break;
                            }
                        }
                    }



                }
            }

            return list;
        }
    }
}