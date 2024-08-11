using System.Security.Cryptography;
using System.Text;

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
        public static void SaveProductImgs(int pId,string pathServer, IFormFile imgLg, IFormFile imgSm)
        {
            string pathProductImgs = Path.Combine(pathServer,"imgs\\imgs\\sp\\",pId.ToString());
            Directory.CreateDirectory(pathProductImgs);

            string imgLgPath = Path.Combine(pathProductImgs, imgMainName);
            using (Stream fileStream = new FileStream(imgLgPath, FileMode.Create))
            {
                imgLg.CopyToAsync(fileStream);
            }

            string imgSmPath = Path.Combine(pathProductImgs, imgThumbsName);
            using (Stream fileStream = new FileStream(imgSmPath, FileMode.Create))
            {
                imgSm.CopyToAsync(fileStream);
            }
        }

        public static void DeleteProductImgs(int pId, string pathServer)
        {
            string pathProductImgs = Path.Combine(pathServer, "imgs\\imgs\\sp", pId.ToString());
            Directory.Delete(pathProductImgs,true);
           
        }
    }
}