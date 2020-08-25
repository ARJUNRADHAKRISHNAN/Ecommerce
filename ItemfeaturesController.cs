using Ecommerce.Model;
using Ecommerce.Web.Dto;
using Kendo.DynamicLinq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Http;

namespace Ecommerce.Web.Controllers
{
    public class ItemfeaturesController: ApiController
    {
        [HttpPost]
        [Route("AddItemfeature")]
        public bool AddItemfeature(ItemfeaturesDto dataDto)
        {
            if (dataDto != null)
            {
                using (EcommerceDB context = new EcommerceDB())
                {
                    if (dataDto.Id <= 0)
                    {
                        Itemfeatures AddData = new Itemfeatures();
                        AddData.Description = dataDto.Description;
                        AddData.ItemId = dataDto.ItemId;
                        AddData.IsActive = true;
                        context.Itemfeaturess.Add(AddData);
                        context.SaveChanges();
                        return true;
                    }
                    else
                    {
                        var olddata = context.Itemfeaturess.FirstOrDefault(x => x.Id == dataDto.Id);
                        if (olddata != null)
                        {
                            olddata.Description = dataDto.Description;
                            olddata.IsActive = true;
                            context.Entry(olddata).Property(x => x.Description).IsModified = true;
                            olddata.ItemId = dataDto.ItemId;
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

        [HttpGet]
        [Route("GetAllItemfeature")]
        public List<ItemfeaturesDto> GetAllItemfeature()
        {

            using (EcommerceDB context = new EcommerceDB())
            {

                var data = context.Itemfeaturess.Where(x => x.IsActive == true)
                     .Select(x => new ItemfeaturesDto
                     {
                         Id = x.Id,
                         Description = x.Description,
                         IsActive = x.IsActive,
                         ItemId = x.ItemId,
                         Item = new ItemDto
                         {
                             Id = x.Item.Id,
                             Name = x.Item.Name,
                             Price =x.Item.Price,


                         }

                     }).ToList();
                return data;
            }
        }

        [HttpPost]
        [Route("ItemfeatureInView")]
        public DataSourceResult ItemfeatureInView(DataSourceRequest Request)
        {
            using (EcommerceDB context = new EcommerceDB())
            {
                var dataSourceResult = context.Itemfeaturess.Where(x => x.IsActive == true)
                    .Select(x => new ItemfeaturesDto
                    {
                        Id = x.Id,
                        Description = x.Description,
                        IsActive = x.IsActive,
                        ItemId = x.ItemId,
                        Item = new ItemDto
                        {
                            Id = x.Item.Id,
                            Name = x.Item.Name,
                            Price = x.Item.Price
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
        [Route("GetItemFeaturesByItemId/{id}")]
        public List<ItemfeaturesDto> GetItemFeaturesByItemId(long id)
        {
            using (EcommerceDB context = new EcommerceDB())
            {
                var dataSourceResult = context.Itemfeaturess.Where(x => x.IsActive == true && x.ItemId == id)
                    .Select(x => new ItemfeaturesDto
                    {
                        Id = x.Id,
                        Description = x.Description,
                        IsActive = x.IsActive,
                        ItemId = x.ItemId,
                        Item = new ItemDto
                        {
                            Id = x.Item.Id,
                            Name = x.Item.Name,
                            Price = x.Item.Price,


                        }
                    }).OrderByDescending(x => x.Id).ToList();


                return dataSourceResult;
            }
        }
        [HttpGet]
        [Route("DeleteItemfeature/{id}")]
        public bool DeleteItemfeature(long id)
        {

            using (EcommerceDB context = new EcommerceDB())
            {

                var data = context.Itemfeaturess.FirstOrDefault(x => x.Id == id);

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
        [HttpGet]
        [Route("GetAllItemfeatureInHome/{id}")]
        public List<ItemfeaturesDto> GetAllItemfeatureInHome(int id)
        {
            using (EcommerceDB context = new EcommerceDB())
            {
                var data = context.Itemfeaturess.Where(x => x.IsActive == true)
             .Select(x => new ItemfeaturesDto
             {


                 Id = x.Id,
                 Description = x.Description,
                 IsActive = x.IsActive,
                 ItemId = x.ItemId,
                 Item = new ItemDto
                 {
                     Id = x.Item.Id,
                     Name = x.Item.Name,
                     Price = x.Item.Price,


                 }
             }).OrderByDescending(x => x.Id).Skip(id).Take(4).ToList();
                return data;

            }

        }
    }
}