using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Web;

namespace DemoWeb.Ultilities
{
    public class Ulti
    {
        public static string Md5Hash(string str)
        {
            using (var md5 = MD5.Create())
            {
                byte[] arr = Encoding.ASCII.GetBytes(str);
                byte[] arrMd5=md5.ComputeHash(arr);
                StringBuilder sb = new StringBuilder();
                foreach (var b in arrMd5)
                {
                    sb.AppendFormat("{0:X}", b);
                }
                return sb.ToString();
            }
        }
        static string imgMainName = "main.jpg";
        static string imgThumbsName = "mini.jpg";
        //xử lí lấy ảnh ở file thêm sản phẩn của trang admin
        public static void SaveProductImgs(int pId,string pathServer, HttpPostedFile imgLg, HttpPostedFile imgSm)
        {
            string pathProductImgs = Path.Combine(pathServer,"imgs\\imgs\\sp\\",pId.ToString());
            Directory.CreateDirectory(pathProductImgs);
            imgLg.SaveAs(Path.Combine(pathProductImgs, imgMainName));
            imgSm.SaveAs(Path.Combine(pathProductImgs, imgThumbsName));
        }

        public static void DeleteProductImgs(int pId, string pathServer)
        {
            string pathProductImgs = Path.Combine(pathServer, "imgs\\imgs\\sp", pId.ToString());
            Directory.Delete(pathProductImgs,true);
           
        }
    }
}