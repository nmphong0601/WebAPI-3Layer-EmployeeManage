using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;
using System.Web;
using System.Web.Http;
using System.Web.Http.Filters;

namespace Ultilities
{
    public class ExceptionHandlingAttribute: ExceptionFilterAttribute
    {
        public override void OnException(HttpActionExecutedContext context)
        {


            if (context.Exception is BusinessLayerException)
            {


                var businessException = context.Exception as BusinessLayerException;

                if (businessException != null)
                {

                    //Write to database
                    //LogException(businessException);

                    switch (businessException.ErrorCode)
                    {

                        case 204:
                            throw new HttpResponseException(new HttpResponseMessage(HttpStatusCode.NoContent)
                            {

                                Content = new StringContent(businessException.ErrorDescription),
                                ReasonPhrase = businessException.ErrorDescription
                            });

                        default:
                            throw new HttpResponseException(new HttpResponseMessage(HttpStatusCode.InternalServerError)
                            {

                                Content = new StringContent(businessException.ErrorDescription),
                                ReasonPhrase = businessException.ErrorDescription

                            });
                    }

                }
            }
            else
            {


                //LogException(context.Exception);


                throw new HttpResponseException(new HttpResponseMessage(HttpStatusCode.InternalServerError)
                {
                    Content = new StringContent("Không kết nối được máy chủ"),
                    ReasonPhrase = "Không kết nối được máy chủ"

                });
            }
        }


        //static void LogException(Exception ex)
        //{

        //    try
        //    {
        //        string userId = "";
        //        if (!ClaimsPrincipal.Current.Identity.IsAuthenticated)
        //            userId = "Chưa đăng nhập";
        //        else
        //            userId = ClaimsPrincipal.Current.Identity.Name;

        //        var businessException = ex as BusinessLayerException;
        //        var ErrorDescription = string.Empty;
        //        if (businessException != null)
        //            ErrorDescription = businessException.ErrorDescription;


        //        var error = new Error(true)
        //        {
        //            UserId = userId,
        //            Exception = ex.GetType().FullName,
        //            Message = ex.Message + ErrorDescription,
        //            Everything = ex.ToString(),
        //            IpAddress = HttpContext.Current.Request.UserHostAddress,
        //            UserAgent = HttpContext.Current.Request.UserAgent,
        //            PathAndQuery = HttpContext.Current.Request.Url == null ? "" : HttpContext.Current.Request.Url.PathAndQuery,
        //            HttpReferer = HttpContext.Current.Request.UrlReferrer == null ? "" : HttpContext.Current.Request.UrlReferrer.PathAndQuery
        //        };

        //        error.Insert();
        //    }
        //    catch { /* do nothing, or send email to webmaster*/}
        //}
    }
}
