using DemoWeb.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace DemoWeb.Ultilities
{
    public static class AddHelpers
    {
        public static MvcHtmlString LessString(this HtmlHelper html,string str, int maxLength){
            if (str.Length < maxLength)
            {
                return new MvcHtmlString(str);
            }
            return new MvcHtmlString(
                string.Format("{0}...",str.Substring(0,maxLength-3))
                );

        }
        public static string Price2String(this HtmlHelper html, decimal price)
        {
            return string.Format("{0:N0} đ",price);
        }
        //kiểm tra xem đã đăng nhập chưa và lấy cookie cho người dùng
        //public static bool IsLogged( this HtmlHelper html)
        //{
        //    if( HttpContext.Current.Session["Logged"]==null){
        //        if (HttpContext.Current.Request.Cookies["UserId"]!=null)
        //        {
        //            int id = int.Parse(HttpContext.Current.Request.Cookies["UserId"].Value);
        //            using (var dc=new QLBH_WebEntities())
        //            {
        //                var user = dc.Users.Where(u => u.f_ID == id).FirstOrDefault();
        //                if (user != null)
        //                {
        //                    HttpContext.Current.Session["Logged"] = new UserInfo { Username = user.f_Username,Permission=user.f_Permission };
        //                    return true;
        //                }
        //            }
        //        }
        //        return false;
        //    }
        //    return true;

        //}
        
    }
}