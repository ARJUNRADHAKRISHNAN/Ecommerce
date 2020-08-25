using Ecommerce.Model;
using Ecommerce.Web.Dto;
using Kendo.DynamicLinq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace Ecommerce.Web.Controllers
{
    public class NewlaunchproductController : ApiController
    {
        [HttpPost]
        [Route("AddNewlaunchproduct")]
        public bool AddNewlaunchproduct(NewlaunchproductDto dataDto)
        {
            if (dataDto != null)
            {
                using (EcommerceDB context = new EcommerceDB())
                {
                    if (dataDto.Id <= 0)
                    {
                        Newlaunchproduct AddData = new Newlaunchproduct();
                        AddData.NewlaunchId = dataDto.NewlaunchId;
                        AddData.ItemId = dataDto.ItemId;
                        AddData.IsActive = true;
                        context.Newlaunchproducts.Add(AddData);
                        context.SaveChanges();
                        return true;
                    }
                    else
                    {
                        var olddata = context.Newlaunchproducts.FirstOrDefault(x => x.Id == dataDto.Id);
                        if (olddata != null)
                        {

                            olddata.IsActive = true;
                            context.Entry(olddata).Property(x => x.IsActive).IsModified = true;
                            olddata.NewlaunchId = dataDto.NewlaunchId;
                            context.Entry(olddata).Property(x => x.NewlaunchId).IsModified = true;
                            olddata.ItemId = dataDto.ItemId;
                            context.Entry(olddata).Property(x => x.ItemId).IsModified = true;
                            olddata.IsActive = true;
                            context.SaveChanges();
                            return true;


                        }


                    }
                }

            }
            return false;
        }
        [HttpGet]
        [Route("GetAllNewlaunchproduct")]
        public List<NewlaunchproductDto> GetAllNewlaunchproduct()
        {

            using (EcommerceDB context = new EcommerceDB())
            {

                var data = context.Newlaunchproducts.Where(x => x.IsActive == true)
                     .Select(x => new NewlaunchproductDto
                     {
                         Id = x.Id,  
                        IsActive = x.IsActive,
                         NewlaunchId = x.NewlaunchId,
                         Newlaunch = new NewlaunchDto
                         {
                             Id = x.Newlaunch.Id,
                             Name = x.Newlaunch.Name,


                         },
                           ItemId = x.ItemId,
                         Item = new ItemDto
                         {
                             Id = x.Item.Id,
                             Name = x.Item.Name,


                         }

                     }).ToList();
                return data;
            }
        }   
        [HttpGet]
        [Route("DeleteNewlaunchproduct/{id}")]
        public bool DeleteNewlaunchproduct(long id)
        {

            using (EcommerceDB context = new EcommerceDB())
            {

                var data = context.Newlaunchproducts.FirstOrDefault(x => x.Id == id);

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
        [Route("NewlaunchproductInView")]
        public DataSourceResult NewlaunchproductInView(DataSourceRequest Request)
        {
            using (EcommerceDB context = new EcommerceDB())
            {
                var dataSourceResult = context.Newlaunchproducts.Where(x => x.IsActive == true)
                    .Select(x => new NewlaunchproductDto
                    {
                        Id = x.Id,
                        IsActive = x.IsActive,
                        NewlaunchId = x.NewlaunchId,
                        Newlaunch = new NewlaunchDto
                        {
                            Id = x.Newlaunch.Id,
                            Name = x.Newlaunch.Name,


                        },
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
        [Route("GetAllNewlaunchproductInHome/{id}")]
        public List<NewlaunchproductDto> GetAllNewlaunchproductInHome(int id)
        {
            using (EcommerceDB context = new EcommerceDB())
            {
                var data = context.Newlaunchproducts.Where(x => x.IsActive == true)
             .Select(x => new NewlaunchproductDto
             {
                 Id = x.Id,
                 IsActive = x.IsActive,
                 NewlaunchId = x.NewlaunchId,
                 Newlaunch = new NewlaunchDto
                 {
                     Id = x.Newlaunch.Id,
                     Name = x.Newlaunch.Name,


                 },
                 ItemId = x.ItemId,
                 Item = new ItemDto
                 {
                     Id = x.Item.Id,
                     Name = x.Item.Name,


                 }
             }).OrderByDescending(x => x.Id).Skip(id).Take(6).ToList();
                return data;

            }

        }
        [HttpGet]
        [Route("GetNewlaunchproductById/{id}")]
        public List<NewlaunchproductDto> GetNewlaunchproductById(long id)
        {
            List<NewlaunchproductDto> listdata = new List<NewlaunchproductDto>();

            using (EcommerceDB context = new EcommerceDB())
            {
                var dataSourceResult = context.Newlaunchproducts.Where(x => x.NewlaunchId == id && x.IsActive==true)
                    .Select(x => new NewlaunchproductDto
                    {
                        Id = x.Id,
                        ItemId = x.ItemId,
                        Item = new ItemDto
                        {
                            Id = x.Item.Id,
                            Name = x.Item.Name,
                            Image = x.Item.Image
                        },
                        NewlaunchId = x.NewlaunchId,
                        Newlaunch = new NewlaunchDto
                        {
                            Id = x.Newlaunch.Id,
                            Name = x.Newlaunch.Name
                        },
                        IsActive = x.IsActive,

                    }).OrderByDescending(x => x.Id).Take(8).ToList();

                listdata = dataSourceResult;
            }
            return listdata;
        }
    }
}
