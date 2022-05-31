using ClosedXML.Excel;
using DAL.Context;
using DAL.Model;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.FileProviders;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net.Mime;
using System.Threading.Tasks;
using System.Web;

namespace ProjectReceivingDataLink
{
    public class Parameters : ControllerBase
    {
        RequestDelegate next;

        public Parameters(RequestDelegate next)
        {
            this.next = next;
        }

        public async Task InvokeAsync(HttpContext httpContext)
        {
            var customerId = httpContext.Request.Query["customer"].ToString();

            var take = httpContext.Request.Query["take"];

            var skip = httpContext.Request.Query["skip"];

            var dateFrom = httpContext.Request.Query["datefrom"];

            Data data = new Data();

            if(httpContext.Request.Query.Count() != 0)
            {
                using (var workbook = new XLWorkbook())
                {
                    var worksheet = workbook.Worksheets.Add("Sample Sheet");
                    worksheet.Cell("A1").Value = "OrderId";
                    worksheet.Cell("B1").Value = "CustomerId";
                    worksheet.Cell("C1").Value = "OrderDate";

                    int i = 2;

                    foreach (var item in data.GetData(Convert.ToInt32(skip), Convert.ToInt32(take), customerId, Convert.ToDateTime(dateFrom)))
                    {
                        worksheet.Cell("A" + i).Value = item.OrderId;
                        worksheet.Cell("B" + i).Value = item.CustomerId;
                        worksheet.Cell("C" + i).Value = item.OrderDate;
                        
                        i++;
                    }

                    workbook.SaveAs("wwwroot\\workbook.xlsx");

                    httpContext.Response.ContentType = "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet";

                    ContentDisposition cd = new ContentDisposition() { FileName = "Orders.xlsx" };

                    httpContext.Response.Headers.Add("Content-Disposition", cd.ToString());

                    await httpContext.Response.SendFileAsync("wwwroot\\workbook.xlsx");
                }
            }
            await next(httpContext); 
        }
    }
}
