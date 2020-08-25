using Ecommerce.Model;
using Ecommerce.Web.Dto;
using Kendo.DynamicLinq;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace Ecommerce.Web.Controllers
{
    public class NewlaunchController : ApiController
    {

        [HttpPost]
        [Route("AddNewlaunch")]
        public bool AddNewlaunch(NewlaunchDto dataDto)
        {
            if (dataDto != null)
            {
                using (EcommerceDB context = new EcommerceDB())
                {
                    if (dataDto.Id <= 0)
                    {
                        Newlaunch AddData = new Newlaunch();
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
                        context.Newlaunchs.Add(AddData);
                        context.SaveChanges();
                        return true;
                    }
                    else
                    {
                        var olddata = context.Newlaunchs.FirstOrDefault(x => x.Id == dataDto.Id);
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
        [Route("GetAllNewlaunch")]
        public List<NewlaunchDto> GetAllNewlaunch()
        {

            using (EcommerceDB context = new EcommerceDB())
            {

                var data = context.Newlaunchs.Where(x => x.IsActive == true)
                     .Select(x => new NewlaunchDto
                     {
                         Id = x.Id,
                         Name = x.Name,
                         Image = x.Image,
                         IsActive = x.IsActive,

                     }).ToList();
                return data;
            }
        }
        [HttpGet]
        [Route("DeleteNewlaunch/{id}")]
        public bool DeleteNewlaunch(long id)
        {

            using (EcommerceDB context = new EcommerceDB())
            {

                var data = context.Newlaunchs.FirstOrDefault(x => x.Id == id);

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
        [Route("NewlaunchInView")]
        public DataSourceResult NewlaunchInView(DataSourceRequest Request)
        {
            using (EcommerceDB context = new EcommerceDB())
            {
                var dataSourceResult = context.Newlaunchs.Where(x => x.IsActive == true)
                    .Select(x => new NewlaunchDto
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
        [Route("GetAllNewlaunchInHome/{id}")]
        public List<NewlaunchDto> GetAllNewlaunchInHome(int id)
        {
            using (EcommerceDB context = new EcommerceDB())
            {
                var data = context.Newlaunchs.Where(x => x.IsActive == true)
             .Select(x => new NewlaunchDto
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
        [Route("GetNewlaunchById/{id}")]
        public NewlaunchDto GetNewlaunchById(long id)
        {
            using (EcommerceDB context = new EcommerceDB())
            {
                var dataSourceResult = context.Newlaunchs.Where(x => x.Id == id)
                    .Select(x => new NewlaunchDto
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

