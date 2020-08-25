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
    public class GalleryController: ApiController
    {
        [HttpPost]
        [Route("AddGallery")]
        public bool AddGallery(GalleryDto dataDto)
        {
            if (dataDto != null)
            {
                using (EcommerceDB context = new EcommerceDB())
                {
                    if (dataDto.Id <= 0)
                    {
                        Gallery AddData = new Gallery();
                        AddData.Name = dataDto.Name;
                        if (dataDto.Img1 != null && dataDto.Img1 != "" && AddData.Img1 != dataDto.Img1 && !dataDto.Img1.Contains("http"))
                        {
                            Guid id = Guid.NewGuid();
                            var imgData = dataDto.Img1.Substring(dataDto.Img1.IndexOf(",") + 1);
                            byte[] bytes = Convert.FromBase64String(imgData);
                            Image image;
                            using (MemoryStream ms = new MemoryStream(bytes))
                            {
                                image = Image.FromStream(ms);
                            }
                            Bitmap b = new Bitmap(image);
                            string filePath = System.Web.HttpContext.Current.Server.MapPath("~") + "UploadedFiles\\" + id + ".jpg";
                            b.Save(filePath, System.Drawing.Imaging.ImageFormat.Jpeg);
                            AddData.Img1 = string.Concat("UploadedFiles\\" + id + ".jpg");
                        }
                        if (dataDto.Img2 != null && dataDto.Img2 != "" && AddData.Img2 != dataDto.Img2 && !dataDto.Img2.Contains("http"))
                        {
                            Guid id = Guid.NewGuid();
                            var imgData = dataDto.Img1.Substring(dataDto.Img2.IndexOf(",") + 1);
                            byte[] bytes = Convert.FromBase64String(imgData);
                            Image image;
                            using (MemoryStream ms = new MemoryStream(bytes))
                            {
                                image = Image.FromStream(ms);
                            }
                            Bitmap b = new Bitmap(image);
                            string filePath = System.Web.HttpContext.Current.Server.MapPath("~") + "UploadedFiles\\" + id + ".jpg";
                            b.Save(filePath, System.Drawing.Imaging.ImageFormat.Jpeg);
                            AddData.Img2 = string.Concat("UploadedFiles\\" + id + ".jpg");
                        }

                        AddData.IsActive = true;
                        context.Gallerys.Add(AddData);
                        context.SaveChanges();
                        return true;
                    }
                    else
                    {
                        var olddata = context.Gallerys.FirstOrDefault(x => x.Id == dataDto.Id);
                        if (olddata != null)
                        {
                            olddata.Name = dataDto.Name;
                            if (dataDto.Img1 != null && dataDto.Img1 != "" && olddata.Img1 != dataDto.Img1 && !dataDto.Img1.Contains("http"))
                            {
                                Guid id = Guid.NewGuid();
                                var imgData = dataDto.Img1.Substring(dataDto.Img1.IndexOf(",") + 1);
                                byte[] bytes = Convert.FromBase64String(imgData);
                                Image image;
                                using (MemoryStream ms = new MemoryStream(bytes))
                                {
                                    image = Image.FromStream(ms);
                                }
                                Bitmap b = new Bitmap(image);
                                string filePath = System.Web.HttpContext.Current.Server.MapPath("~") + "UploadedFiles\\" + id + ".jpg";
                                b.Save(filePath, System.Drawing.Imaging.ImageFormat.Jpeg);
                                olddata.Img1 = string.Concat("UploadedFiles\\" + id + ".jpg");
                                context.Entry(olddata).Property(x => x.Img1).IsModified = true;
                            }
                        }
                            if (dataDto.Img2 != null && dataDto.Img2 != "" && olddata.Img2 != dataDto.Img2 && !dataDto.Img2.Contains("http"))
                            {
                                Guid id = Guid.NewGuid();
                                var imgData = dataDto.Img2.Substring(dataDto.Img2.IndexOf(",") + 1);
                                byte[] bytes = Convert.FromBase64String(imgData);
                                Image image;
                                using (MemoryStream ms = new MemoryStream(bytes))
                                {
                                    image = Image.FromStream(ms);
                                }
                                Bitmap b = new Bitmap(image);
                                string filePath = System.Web.HttpContext.Current.Server.MapPath("~") + "UploadedFiles\\" + id + ".jpg";
                                b.Save(filePath, System.Drawing.Imaging.ImageFormat.Jpeg);
                                olddata.Img2 = string.Concat("UploadedFiles\\" + id + ".jpg");
                                context.Entry(olddata).Property(x => x.Img2).IsModified = true;
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
                return false;
            }
        

        [HttpGet]
        [Route("GetAllGallery")]
        public List<GalleryDto> GetAllGallery()
        {

            using (EcommerceDB context = new EcommerceDB())
            {

                var data = context.Gallerys.Where(x => x.IsActive == true)
                     .Select(x => new GalleryDto
                     {
                         Id = x.Id,
                         Name = x.Name,
                         Img1 = x.Img1,
                         Img2 = x.Img2,
                         IsActive = x.IsActive,

                     }).ToList();
                return data;
            }
        }

        [HttpPost]
        [Route("GalleryInView")]
        public DataSourceResult GalleryInView(DataSourceRequest Request)
        {
            using (EcommerceDB context = new EcommerceDB())
            {
                var dataSourceResult = context.Gallerys.Where(x => x.IsActive == true)
                    .Select(x => new GalleryDto
                    {
                        Id = x.Id,
                        Name = x.Name,
                        Img1 = x.Img1,
                        Img2 = x.Img2,
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
        [Route("DeleteGallery/{id}")]
        public bool DeleteGallery(long id)
        {

            using (EcommerceDB context = new EcommerceDB())
            {

                var data = context.Gallerys.FirstOrDefault(x => x.Id == id);

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
        [Route("GetAllGalleryInHome/{id}")]
        public List<GalleryDto> GetAllGalleryInHome(int id)
        {
            using (EcommerceDB context = new EcommerceDB())
            {
                var data = context.Gallerys.Where(x => x.IsActive == true)
             .Select(x => new GalleryDto
             {

                 Id = x.Id,
                 Name = x.Name,
                 Img1 = x.Img1,
                 Img2 = x.Img2,
                 IsActive = x.IsActive,
             }).OrderByDescending(x => x.Id).Skip(id).Take(4).ToList();
                return data;

            }

        }
    }
}