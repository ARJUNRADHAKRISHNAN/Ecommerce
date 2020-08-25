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
    public class BrandController: ApiController
    {
          
        [HttpPost]
        [Route("AddBrands")]
        public bool AddBrands(BrandDto dataDto)
        {
            if (dataDto != null)
            {
                using (EcommerceDB context = new EcommerceDB())
                {
                    if (dataDto.Id <= 0)
                    {
                        Brand AddData = new Brand();
                        AddData.Name = dataDto.Name;
                        //AddData.Image = dataDto.Image;
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


                        AddData.IsActive = true;
                        context.Brands.Add(AddData);
                        context.SaveChanges();
                        return true;
                    }
                    else
                    {
                        var olddata = context.Brands.FirstOrDefault(x => x.Id == dataDto.Id);
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
                            olddata.IsActive = true;
                            context.Entry(olddata).Property(x => x.Name).IsModified = true;
                           
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
        [Route("GetAllBrands")]
        public List<BrandDto> GetAllBrands()
        {

            using (EcommerceDB context = new EcommerceDB())
            {

                var data = context.Brands.Where(x => x.IsActive == true)
                     .Select(x => new BrandDto
                     {
                         Id = x.Id,
                         Name = x.Name,
                         Image = x.Image,
                         IsActive = x.IsActive,

                     }).ToList();
                return data;
            }
        }

        [HttpPost]
        [Route("BrandsInView")]
        public DataSourceResult BrandsInView(DataSourceRequest Request)
        {
            using (EcommerceDB context = new EcommerceDB())
            {
                var dataSourceResult = context.Brands.Where(x => x.IsActive == true)
                    .Select(x => new BrandDto
                    {
                        Id = x.Id,
                        Name = x.Name,
                        Image = x.Image,
                        IsActive = x.IsActive,

                    }).OrderByDescending(x => x.Id).ToDataSourceResult(Request);

                DataSourceResult kendoResponseDto = new DataSourceResult();
                kendoResponseDto.Data = dataSourceResult.Data;
                kendoResponseDto.Total = dataSourceResult.Total;
                kendoResponseDto.Aggregates = dataSourceResult.Aggregates;
                return kendoResponseDto;
            }
        }
        

        [HttpGet]
        [Route("DeleteBrand/{id}")]
        public bool DeleteBrand(long id)
        {

            using (EcommerceDB context = new EcommerceDB())
            {

                var data = context.Brands.FirstOrDefault(x => x.Id == id);

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
        [Route("GetAllBrandInHome/{id}")]
        public List<BrandDto> GetAllBrandInHome(int id)
        {
            using (EcommerceDB context = new EcommerceDB())
            {
                var data = context.Brands.Where(x => x.IsActive == true)
             .Select(x => new BrandDto
             {
                 Id = x.Id,
                 Name = x.Name,
                 Image = x.Image,
                 IsActive = x.IsActive,
             }).OrderByDescending(x => x.Id).Skip(id).Take(6).ToList();
                return data;

            }

        }
        [HttpGet]
        [Route("GetBrandById/{id}")]
        public BrandDto GetBrandById(long id)
        {
            using (EcommerceDB context = new EcommerceDB())
            {
                var dataSourceResult = context.Brands.Where(x => x.Id == id)
                    .Select(x => new BrandDto
                    {
                        Id = x.Id,
                        IsActive = x.IsActive,
                        Name = x.Name,
                        Image = x.Image,

                    }).FirstOrDefault();


                return dataSourceResult;
            }
        }
    }
}