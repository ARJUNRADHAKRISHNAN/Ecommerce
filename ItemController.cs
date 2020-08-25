using Ecommerce.Model;
using Ecommerce.Web.Dto;
using Kendo.DynamicLinq;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Http;

namespace Ecommerce.Web.Controllers
{
    public class ItemController: ApiController
    {
        [HttpPost]
        [Route("AddItem")]
        public bool AddItem(ItemDto dataDto)
        {
            if (dataDto != null)
            {
                using (EcommerceDB context = new EcommerceDB())
                {
                    if (dataDto.Id <= 0)
                    {
                        Item AddData = new Item();
                        AddData.Name = dataDto.Name;
                        if (dataDto.Image != null && dataDto.Image != "" && AddData.Image != dataDto.Image && !dataDto.Image.Contains("http"))
                        {
                            Guid id = Guid.NewGuid();
                            var imgData = dataDto.Image.Substring(dataDto.Image.IndexOf(",") + 1);
                            byte[] bytes = Convert.FromBase64String(imgData);
                            Image image;
                            using (MemoryStream ms = new MemoryStream(bytes))
                            {
                                image = Image.FromStream(ms);
                            }
                            Bitmap b = new Bitmap(image);
                            string filePath = System.Web.HttpContext.Current.Server.MapPath("~") + "UploadedFiles\\" + id + ".jpg";
                            b.Save(filePath, System.Drawing.Imaging.ImageFormat.Jpeg);
                            AddData.Image = string.Concat("UploadedFiles\\" + id + ".jpg");
                        }

                        AddData.Price = dataDto.Price;
                        AddData.BrandId = dataDto.BrandId;
                        AddData.CategoryId = dataDto.CategoryId;
                        AddData.SubcategoryId = dataDto.SubcategoryId;
                        AddData.IsActive = true;
                        context.Items.Add(AddData);
                        context.SaveChanges();
                        return true;
                    }
                    else
                    {
                        var olddata = context.Items.FirstOrDefault(x => x.Id == dataDto.Id);
                        if (olddata != null)
                        {
                                olddata.Name = dataDto.Name;
                                if (dataDto.Image != null && dataDto.Image != "" && olddata.Image != dataDto.Image && !dataDto.Image.Contains("http"))
                                {
                                    Guid id = Guid.NewGuid();
                                    var imgData = dataDto.Image.Substring(dataDto.Image.IndexOf(",") + 1);
                                    byte[] bytes = Convert.FromBase64String(imgData);
                                    Image image;
                                    using (MemoryStream ms = new MemoryStream(bytes))
                                    {
                                        image = Image.FromStream(ms);
                                    }
                                    Bitmap b = new Bitmap(image);
                                    string filePath = System.Web.HttpContext.Current.Server.MapPath("~") + "UploadedFiles\\" + id + ".jpg";
                                    b.Save(filePath, System.Drawing.Imaging.ImageFormat.Jpeg);
                                    olddata.Image = string.Concat("UploadedFiles\\" + id + ".jpg");
                                    context.Entry(olddata).Property(x => x.Image).IsModified = true;
                                }
                                olddata.Price = dataDto.Price;
                            olddata.IsActive = true;
                            olddata.BrandId = dataDto.BrandId;
                            olddata.CategoryId = dataDto.CategoryId;
                            olddata.SubcategoryId = dataDto.SubcategoryId;
                            context.Entry(olddata).Property(x => x.Name).IsModified = true;
                            context.Entry(olddata).Property(x => x.Price).IsModified = true;
                            context.Entry(olddata).Property(x => x.BrandId).IsModified = true;
                            context.Entry(olddata).Property(x => x.CategoryId).IsModified = true;
                            context.Entry(olddata).Property(x => x.SubcategoryId).IsModified = true;
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
        [Route("GetAllItems")]
        public List<ItemDto> GetAllItems()
        {

            using (EcommerceDB context = new EcommerceDB())
            {

                var data = context.Items.Where(x => x.IsActive == true)
                     .Select(x => new ItemDto
                     {
                         Id = x.Id,
                         Name = x.Name,
                         Image = x.Image,
                         Price = x.Price,
                         IsActive = x.IsActive,
                         BrandId = x.BrandId,
                         Brand = new BrandDto
                         {
                             Id = x.Brand.Id,
                             Name = x.Brand.Name,
                             Image = x.Brand.Image,


                         },
                          CategoryId = x.CategoryId,
                         Category = new CategoryDto
                         {
                             Id = x.Category.Id,
                             Name = x.Category.Name,


                         },
                          SubcategoryId = x.SubcategoryId,
                         Subcategory = new SubcategoryDto
                         {
                             Id = x.Subcategory.Id,
                             Name = x.Subcategory.Name,


                         }

                     }).ToList();
                return data;
            }
        }

        [HttpPost]
        [Route("ItemsInView")]
        public DataSourceResult ItemsInView(DataSourceRequest Request)
        {
            using (EcommerceDB context = new EcommerceDB())
            {
                var dataSourceResult = context.Items.Where(x => x.IsActive == true)
                    .Select(x => new ItemDto
                    {
                        Id = x.Id,
                        Name = x.Name,
                        Image = x.Image,
                        Price = x.Price,
                        IsActive = x.IsActive,
                        BrandId = x.BrandId,
                        CategoryId = x.CategoryId,
                        SubcategoryId = x.SubcategoryId,
                        Brand = new BrandDto
                        {
                            Id = x.Brand.Id,
                            Name = x.Brand.Name,
                            Image = x.Brand.Image
                        },
                        Category = new CategoryDto
                        {
                            Id = x.Category.Id,
                            Name = x.Category.Name
                        },
                        Subcategory = new SubcategoryDto
                        {
                            Id = x.Subcategory.Id,
                            Name = x.Subcategory.Name
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
        [Route("DeleteItem/{id}")]
        public bool DeleteItem(long id)
        {

            using (EcommerceDB context = new EcommerceDB())
            {

                var data = context.Items.FirstOrDefault(x => x.Id == id);

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
        [Route("GetAllItemsInHome/{id}")]
        public List<ItemDto> GetAllItemsInHome(int id)
        {
            using (EcommerceDB context = new EcommerceDB())
            {
                var data = context.Items.Where(x => x.IsActive == true)
             .Select(x => new ItemDto
             {
                 Id = x.Id,
                 Name = x.Name,
                 Image = x.Image,
                 Price = x.Price,
                 IsActive = x.IsActive,
                 BrandId = x.BrandId,
                 CategoryId = x.CategoryId,
                 SubcategoryId = x.SubcategoryId,
                 Brand = new BrandDto
                 {
                     Id = x.Brand.Id,
                     Name = x.Brand.Name,
                     Image = x.Brand.Image
                 },
                 Category = new CategoryDto
                 {
                     Id = x.Category.Id,
                     Name = x.Category.Name
                 },
                 Subcategory = new SubcategoryDto
                 {
                     Id = x.Subcategory.Id,
                     Name = x.Subcategory.Name
                 }
             }).OrderByDescending(x => x.Id).Skip(id).Take(6).ToList();
                return data;

            }

        }

        [HttpGet]
        [Route("GetItemById/{id}")]
        public ItemDto GetItemById(long id)
        {
            ItemDto item = new ItemDto();
            using (EcommerceDB context = new EcommerceDB())
            {
                var data = context.Items.FirstOrDefault(x => x.IsActive == true && x.Id == id);
                item.Id = data.Id;
                item.Name = data.Name;
                item.Price = data.Price;
                item.Image = data.Image;
                item.IsActive = data.IsActive;
                item.CategoryId = data.CategoryId;
                item.SubcategoryId = data.SubcategoryId;
                item.BrandId = data.BrandId;
                item.Brand = new BrandDto
                {
                    Id = data.Brand.Id,
                    Name = data.Brand.Name,
                };
                item.Category = new CategoryDto
                {
                    Id = data.Category.Id,
                    Name = data.Category.Name,
                };
                item.Subcategory = new SubcategoryDto

                {
                    Id = data.Subcategory.Id,
                    Name = data.Subcategory.Name,
                };
                item.Itemfeatures = context.Itemfeaturess.Where(x => x.IsActive == true && x.ItemId == item.Id)
              .Select(x => new ItemfeaturesDto
              {

                  Id = x.Id,
                  Description = x.Description,
              }).OrderByDescending(x => x.Id).ToList();
            }
            return item;
        }

        [HttpGet]
        [Route("GetItemByCategoryId/{id}")]
        public List<ItemDto> GetItemByCategoryId(long id)
        {
            using (EcommerceDB context = new EcommerceDB())
            {
                var dataSourceResult = context.Items.Where(x =>x.IsActive==true && x.CategoryId == id)
                    .Select(x => new ItemDto
                    {
                        Id = x.Id,
                        IsActive = x.IsActive,
                        Name = x.Name,
                        Image = x.Image,
                        Price = x.Price,
                        BrandId = x.BrandId,
                        Brand = new BrandDto
                        {
                            Id = x.Brand.Id,
                            Name = x.Brand.Name,
                            Image = x.Brand.Image
                        },
                        CategoryId = x.CategoryId,
                        Category = new CategoryDto
                        {
                            Id = x.Category.Id,
                            Name = x.Category.Name
                        },
                        SubcategoryId = x.SubcategoryId,
                        Subcategory = new SubcategoryDto
                        {
                            Id = x.Subcategory.Id,
                            Name = x.Subcategory.Name
                        }
                    }).OrderByDescending(x => x.Id).ToList();


                return dataSourceResult;
            }
        }
        [HttpGet]
        [Route("GetItemByBrandId/{id}")]
        public List<ItemDto> GetItemByBrandId(long id)
        {
            using (EcommerceDB context = new EcommerceDB())
            {
                var dataSourceResult = context.Items.Where(x => x.IsActive == true && x.BrandId == id)
                    .Select(x => new ItemDto
                    {
                        Id = x.Id,
                        IsActive = x.IsActive,
                        Name = x.Name,
                        Image = x.Image,
                        Price = x.Price,
                        BrandId = x.BrandId,
                        Brand = new BrandDto
                        {
                            Id = x.Brand.Id,
                            Name = x.Brand.Name,
                            Image = x.Brand.Image
                        },
                        CategoryId = x.CategoryId,
                        Category = new CategoryDto
                        {
                            Id = x.Category.Id,
                            Name = x.Category.Name
                        },
                        SubcategoryId = x.SubcategoryId,
                        Subcategory = new SubcategoryDto
                        {
                            Id = x.Subcategory.Id,
                            Name = x.Subcategory.Name
                        }
                    }).OrderByDescending(x => x.Id).ToList();


                return dataSourceResult;
            }
        }
        [HttpGet]
        [Route("GetItemBySubcategoryId/{id}")]
        public List<ItemDto> GetItemBySubcategoryId(long id)
        {
            using (EcommerceDB context = new EcommerceDB())
            {
                var dataSourceResult = context.Items.Where(x => x.IsActive == true && x.SubcategoryId == id)
                    .Select(x => new ItemDto
                    {
                        Id = x.Id,
                        IsActive = x.IsActive,
                        Name = x.Name,
                        Image = x.Image,
                        Price = x.Price,
                        BrandId = x.BrandId,
                        Brand = new BrandDto
                        {
                            Id = x.Brand.Id,
                            Name = x.Brand.Name,
                            Image = x.Brand.Image
                        },
                        CategoryId = x.CategoryId,
                        Category = new CategoryDto
                        {
                            Id = x.Category.Id,
                            Name = x.Category.Name
                        },
                        SubcategoryId = x.SubcategoryId,
                        Subcategory = new SubcategoryDto
                        {
                            Id = x.Subcategory.Id,
                            Name = x.Subcategory.Name
                        }
                    }).OrderByDescending(x => x.Id).ToList();


                return dataSourceResult;
            }
        }
        [HttpPost]
        [Route("getRelatedItems/{id}")]
        public List<ItemDto> getRelatedItems(long id)
        {
            List<ItemDto> listdata = new List<ItemDto>();

            using (EcommerceDB context = new EcommerceDB())
            {
                var displayedItem = context.Items.FirstOrDefault(x => x.Id == id);

                var data = context.Items.Where(x => x.IsActive && x.Id != id);
                data = data.Where(x => x.CategoryId == displayedItem.CategoryId || x.SubcategoryId == displayedItem.SubcategoryId || x.BrandId == displayedItem.BrandId);

                var dataList = data
                  .Select(x => new ItemDto
                  {
                      Id = x.Id,
                      Name = x.Name,
                      Price = x.Price,
                      Image = x.Image,
                      IsActive = x.IsActive,

                      CategoryId = x.CategoryId,
                      Category = new CategoryDto
                      {
                          Id = x.Category.Id,
                          Name = x.Category.Name
                      },

                      SubcategoryId = x.SubcategoryId,
                      Subcategory = new SubcategoryDto
                      {
                          Id = x.Subcategory.Id,
                          Name = x.Subcategory.Name
                      },

                      BrandId = x.BrandId,
                      Brand = new BrandDto
                      {
                          Id = x.Brand.Id,
                          Name = x.Brand.Name
                      }
                  }).OrderByDescending(x => x.Id).Take(8).ToList();

                listdata = dataList;
            }
            return listdata;
        }
        [HttpPost]
        [Route("SearchItemByKeyword/{Keyword}")]
        public List<ItemDto> SearchItemByKeyword(string Keyword)
        {
            using (EcommerceDB context = new EcommerceDB())
            {
                var data = context.Items.Where(x => x.IsActive == true &&
                                (x.Name.Contains(Keyword) || Keyword.Contains(x.Name) || x.Category.Name.Contains(Keyword) || x.Subcategory.Name.Contains(Keyword) || x.Brand.Name.Contains(Keyword)))
                .Select(x => new ItemDto
                {
                    Id = x.Id,
                    Name = x.Name,
                    Price = x.Price,
                    Image = x.Image,
                    IsActive = x.IsActive,

                    CategoryId = x.CategoryId,
                    Category = new CategoryDto
                    {
                        Id = x.Category.Id,
                        Name = x.Category.Name
                    },

                    SubcategoryId = x.SubcategoryId,
                    Subcategory = new SubcategoryDto
                    {
                        Id = x.Subcategory.Id,
                        Name = x.Subcategory.Name
                    },

                    BrandId = x.BrandId,
                    Brand = new BrandDto
                    {
                        Id = x.Brand.Id,
                        Name = x.Brand.Name
                    }
                }).OrderByDescending(x => x.Id).ToList();

                return data;
            }
        }
        [HttpPost]
        [Route("SearchItemByFilter")] 
        public List<ItemDto> SearchItemByFilter(FilterDto Request)
        {
            using (EcommerceDB context = new EcommerceDB())
            {
                var data = context.Items.Where(x => x.IsActive == true);

                if (Request.Maxval > 0)
                {
                    data = data.Where(x => ((x.Price >= Request.Minval) && (x.Price <= Request.Maxval)) );
                }

                if (Request.CategoryIds.Count() > 0)
                {
                    data = data.Where(x => Request.CategoryIds.Contains(x.CategoryId));
                }
                if (Request.BrandIds.Count() > 0)
                {
                    data = data.Where(x => Request.BrandIds.Contains(x.BrandId));
                }


                var datalist = data
                               .Select(x => new ItemDto
                {
                    Id = x.Id,
                    Name = x.Name,
                    Price = x.Price,
                    Image = x.Image,
                    IsActive = x.IsActive,

                    CategoryId = x.CategoryId,
                    Category = new CategoryDto
                    {
                        Id = x.Category.Id,
                        Name = x.Category.Name
                    },

                    SubcategoryId = x.SubcategoryId,
                    Subcategory = new SubcategoryDto
                    {
                        Id = x.Subcategory.Id,
                        Name = x.Subcategory.Name
                    },

                    BrandId = x.BrandId,
                    Brand = new BrandDto
                    {
                        Id = x.Brand.Id,
                        Name = x.Brand.Name
                    }
                }).OrderByDescending(x => x.Id).ToList();

                return datalist; 
            }
        }
    }
}