using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.IO;

namespace CheckIn.CPA
{
    /// <summary>
    /// Summary description for Handler
    /// </summary>
    public class Handler : IHttpHandler
    {

        public void ProcessRequest(HttpContext context)
        {
            if (context.Request.QueryString["QueryCPAID"] != null)
            {

                Byte[] bytes = BusinessLogic.GetCPAImage(int.Parse(context.Request.QueryString["QueryCPAID"]));

                if (bytes == null)
                    return;

                context.Response.Buffer = true;
                context.Response.Charset = "";
                context.Response.Cache.SetCacheability(HttpCacheability.NoCache);
                context.Response.ContentType = "image/jpg";
                context.Response.AddHeader("content-disposition", "attachment;filename=" + context.Request.QueryString["QueryCPAID"] + ".jpg");
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