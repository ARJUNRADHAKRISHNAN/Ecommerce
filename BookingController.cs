using Ecommerce.Model;
using Ecommerce.Web.Dto;
using Kendo.DynamicLinq;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace Ecommerce.Web.Controllers
{
    public class BookingController : ApiController
    {
        [HttpPost]
        [Route("AddBooking")]
        public bool AddBooking(BookingDto dataDto)
        {
            if (dataDto != null)
            {
                using (EcommerceDB context = new EcommerceDB())
                {
                    if (dataDto.Id <= 0)
                    {
                        Booking AddData = new Booking();
                        AddData.Date = dataDto.Date;
                        AddData.Name = dataDto.Name;
                        AddData.MobileNo = dataDto.MobileNo;
                        AddData.EmailId = dataDto.EmailId;
                        AddData.Address = dataDto.Address;
                        AddData.Remarks = dataDto.Remarks;
                        AddData.ItemId = dataDto.ItemId;
                        AddData.IsActive = true;
                        context.Bookings.Add(AddData);
                        context.SaveChanges();
                        return true;
                    }
                    else
                    {
                        var olddata = context.Bookings.FirstOrDefault(x => x.Id == dataDto.Id);
                        if (olddata != null)
                        {
                            olddata.Date = dataDto.Date;
                            olddata.Name = dataDto.Name;
                            olddata.MobileNo = dataDto.MobileNo;
                            olddata.EmailId = dataDto.EmailId;
                            olddata.Address = dataDto.Address;
                            olddata.Remarks = dataDto.Remarks;
                            olddata.ItemId = dataDto.ItemId;
                            olddata.IsActive = true;
                            context.Entry(olddata).Property(x => x.Date).IsModified = true;
                            context.Entry(olddata).Property(x => x.Name).IsModified = true;
                            context.Entry(olddata).Property(x => x.MobileNo).IsModified = true;
                            context.Entry(olddata).Property(x => x.EmailId).IsModified = true;
                            context.Entry(olddata).Property(x => x.Address).IsModified = true;
                            context.Entry(olddata).Property(x => x.Remarks).IsModified = true;
                            context.Entry(olddata).Property(x => x.ItemId).IsModified = true;
                            context.Entry(olddata).Property(x => x.IsActive).IsModified = true;
                            olddata.IsActive = true;
                            context.SaveChanges();
                            return true;


                        }


                    }
                }

            }
            return false;
        }
        [HttpPost]
        [Route("BookingInView")]
        public DataSourceResult BookingInView(DataSourceRequest Request)
        {
            using (EcommerceDB context = new EcommerceDB())
            {
                var dataSourceResult = context.Bookings.Where(x => x.IsActive == true)
                    .Select(x => new BookingDto
                    {
                        Id = x.Id,
                        Date = x.Date,
                        Name = x.Name,
                        MobileNo = x.MobileNo,
                        EmailId = x.EmailId,
                        Address = x.Address,
                        Remarks = x.Remarks,
                        IsActive = x.IsActive,
                        ItemId = x.ItemId,
                        Item = new ItemDto
                        {
                            Id = x.Item.Id,
                            Name = x.Item.Name,
                        }

                    }).OrderByDescending(x => x.Id).ToDataSourceResult(Request);

                DataSourceResult kendoResponseDto = new DataSourceResult();
                kendoResponseDto.Data = dataSourceResult.Data;
                kendoResponseDto.Total = dataSourceResult.Total;
                kendoResponseDto.Aggregates = dataSourceResult.Aggregates;
                return kendoResponseDto;
            }
        }
        [HttpGet]
        [Route("GetAllBookingInDashboardHome")]
        public List<BookingDto> GetAllBookingInDashboardHome()
        {
            using (EcommerceDB context = new EcommerceDB())
            {
                var data = context.Bookings.Where(x => x.IsActive == true)
             .Select(x => new BookingDto
             {
                 Id = x.Id,
                 Date = x.Date,
                 Name = x.Name,
                 MobileNo = x.MobileNo,
                 EmailId = x.EmailId,
                 Address = x.Address,
                 Remarks = x.Remarks,
                 IsActive = x.IsActive,
             }).OrderByDescending(x => x.Id).Take(3).ToList();
                return data;

            }
        }

        [HttpGet]
        [Route("DeleteBooking/{id}")]
        public bool DeleteBooking(long id)
        {

            using (EcommerceDB context = new EcommerceDB())
            {

                var data = context.Bookings.FirstOrDefault(x => x.Id == id);

                if (data != null)
                {

                    data.IsActive = false;
                    context.Entry(data).Property(x => x.IsActive).IsModified = true;
                    context.SaveChanges();

                    return true;

                }
                return false;
            }
        }
        [HttpPost]
        [Route("FilterBookingreportInView")]
        public DataSourceResult FilterBookingreportInView(FilterDto Request)
        {
            using (EcommerceDB context = new EcommerceDB())
            {
                var data = context.Bookings.Where(x => x.IsActive == true);
                if (Request.CategoryIds.Count() > 0)
                {
                    data = data.Where(x => Request.CategoryIds.Contains(x.Item.CategoryId));
                }
                if (Request.BrandIds.Count() > 0)
                {
                    data = data.Where(x => Request.BrandIds.Contains(x.Item.BrandId));
                }
                var dataSourceResult = data.Where(x => (DbFunctions.TruncateTime(x.Date)) >= (DbFunctions.TruncateTime(Request.Fromdate)) && (DbFunctions.TruncateTime(x.Date)) <= (DbFunctions.TruncateTime(Request.Todate)))
                   .Select(x => new BookingDto
                   {
                       Id = x.Id,
                       Name = x.Name,
                       Date = x.Date,
                       Address = x.Address,
                       Remarks = x.Remarks,
                       MobileNo = x.MobileNo,
                       EmailId = x.EmailId,
                       IsActive = x.IsActive,

                       ItemId = x.ItemId,
                       Item = new ItemDto
                       {
                           Id = x.Item.Id,
                           Name = x.Item.Name
                       },


                   }).OrderByDescending(x => x.Id).ToDataSourceResult(Request);

                DataSourceResult kendoResponseDto = new DataSourceResult();
                kendoResponseDto.Data = dataSourceResult.Data;
                kendoResponseDto.Total = dataSourceResult.Total;
                return kendoResponseDto;
            }
        }
    }
}
