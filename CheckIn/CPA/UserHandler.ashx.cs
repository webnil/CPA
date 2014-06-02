using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace CheckIn.CPA
{
    /// <summary>
    /// Summary description for UserHandler
    /// </summary>
    public class UserHandler : IHttpHandler
    {


        public void ProcessRequest(HttpContext context)
        {
            if (context.Request.QueryString["UserID"] != null)
            {

                Byte[] bytes = BusinessLogic.GetUserImage(int.Parse(context.Request.QueryString["UserID"]));

                if (bytes == null)
                    return;

                context.Response.Buffer = true;
                context.Response.Charset = "";
                context.Response.Cache.SetCacheability(HttpCacheability.NoCache);
                context.Response.ContentType = "image/jpg";
                context.Response.AddHeader("content-disposition", "attachment;filename=" + context.Request.QueryString["UserID"] + ".jpg");
                context.Response.BinaryWrite(bytes);
                context.Response.Flush();
                context.Response.End();
            }
        }



        public bool IsReusable
        {
            get
            {
                return false;
            }
        }
    }
}