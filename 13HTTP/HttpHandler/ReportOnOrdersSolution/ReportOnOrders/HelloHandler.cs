using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using DALayer;

namespace ReportOnOrders
{
    public class HelloHandler : IHttpHandler
    {
        public bool IsReusable => true;

        public void ProcessRequest(HttpContext context)
        {
            var request = context.Request;
            var response = context.Response;

            if (request.Url.PathAndQuery == "/~close")
                return;

            var name = request.QueryString["name"];

            string answerString;


            if (name != null)
            {
                answerString = $"Hello, {name}";
                

            }
            else
            {
                answerString = $"Hello, друг";

                NorthwindContext db = new NorthwindContext();

                List<int> vs = new List<int>();

                foreach (var item in db.Orders)
                {
                    vs.Add(item.OrderId);
                }
            }

            response.Output.WriteLine(answerString);
        }
    }
}