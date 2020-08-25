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
    public class SubcategoryController : ApiController
    {
        [HttpPost]
        [Route("AddSubcategories")]
        public bool AddSubcategories(SubcategoryDto dataDto)
        {
            if (dataDto != null)
            {
                using (EcommerceDB context = new EcommerceDB())
                {
                    if (dataDto.Id <= 0)
                    {
                        Subcategory AddData = new Subcategory();
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
                        AddData.CategoryId = dataDto.CategoryId;
                        AddData.IsActive = true;
                        context.Subcategories.Add(AddData);
                        context.SaveChanges();
                        return true;
                    }
                    else
                    {
                        var olddata = context.Subcategories.FirstOrDefault(x => x.Id == dataDto.Id);
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
                            olddata.CategoryId = dataDto.CategoryId;
                            context.Entry(olddata).Property(x => x.CategoryId).IsModified = true;
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
        [Route("GetAllSubcategories")]
        public List<SubcategoryDto> GetAllSubcategories()
        {

            using (EcommerceDB context = new EcommerceDB())
            {

                var data = context.Subcategories.Where(x => x.IsActive == true)
                     .Select(x => new SubcategoryDto
                     {
                         Id = x.Id,
                         Name = x.Name,
                         Image = x.Image,
                         IsActive = x.IsActive,
                         CategoryId = x.CategoryId,
                         Category = new CategoryDto
                         {
                             Id = x.Category.Id,
                             Name = x.Category.Name,


                         }

                     }).ToList();
                return data;
            }
        }
        [HttpPost]
        [Route("SubcategoryInView")]
        public DataSourceResult SubcategoryInView(DataSourceRequest Request)
        {
            using (EcommerceDB context = new EcommerceDB())
            {
                var dataSourceResult = context.Subcategories.Where(x => x.IsActive == true)
                    .Select(x => new SubcategoryDto
                    {
                        Id = x.Id,
                        Name = x.Name,
                        Image = x.Image,
                        IsActive = x.IsActive,
                        CategoryId = x.CategoryId,
                        Category = new CategoryDto
                        {
                            Id = x.Category.Id,
                            Name = x.Category.Name
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
        [Route("DeleteSubcategories/{id}")]
        public bool DeleteSubcategory(long id)
        {

            using (EcommerceDB context = new EcommerceDB())
            {

                var data = context.Subcategories.FirstOrDefault(x => x.Id == id);

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
        [Route("GetAllSubcategoriesInHome/{id}")]
        public List<SubcategoryDto> GetAllSubcategoriesInHome(int id)
        {
            using (EcommerceDB context = new EcommerceDB())
            {
                var data = context.Subcategories.Where(x => x.IsActive == true)
             .Select(x => new SubcategoryDto
             {

                 Id = x.Id,
                 Name = x.Name,
                 Image = x.Image,
                 IsActive = x.IsActive,
                 CategoryId = x.CategoryId,
                 Category = new CategoryDto
                 {
                     Id = x.Category.Id,
                     Name = x.Category.Name,


                 }
             }).OrderByDescending(x => x.Id).Skip(id).Take(4).ToList();
                return data;

            }

        }
        [HttpGet]
        [Route("GetSubcategoryById/{id}")]
        public SubcategoryDto GetSubcategoryById(long id)
        {
            using (EcommerceDB context = new EcommerceDB())
            {
                var dataSourceResult = context.Subcategories.Where(x => x.Id == id)
                    .Select(x => new SubcategoryDto
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