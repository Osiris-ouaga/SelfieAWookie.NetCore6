using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using SelfieAWookie.NetCore6.Application.DTOs;
using SelfieAWookies.Core.Selfies.Domain;
using SelfieAWookies.Core.Selfies.Infrastructures.Data;

namespace SelfieAWookie.NetCore6.Controllers
{
    [Route("api/V1/[controller]")]
    [ApiController]
    public class SelfieController : ControllerBase
    {
        private readonly ISelfieRepository _repository = null;
        private readonly IWebHostEnvironment _webHostEnvironment = null;

        public SelfieController(ISelfieRepository repository, IWebHostEnvironment webHostEnvironment) 
        { 
            this._repository = repository;
            this._webHostEnvironment = webHostEnvironment;
        }

        //[HttpGet]   
        //public IEnumerable<Selfie> TestAMoi()
        //{
        //    return Enumerable.Range(1, 10).Select(item => new Selfie() { id = item });
        //}

        [HttpGet]
        public IActionResult GetAll([FromQuery] int? wookieId)
        {
            //var model = Enumerable.Range(1, 10).Select(item => new Selfie() { Id = item });

            var param = this.Request.Query["wookieId"];

            var selfiesList = this._repository.GetAll(wookieId);
            var model = selfiesList.Select(item => new SelfieResumeDto()
            {
                //  Id = item.Id, 
                Title = item.Title!,
                WookieId = item.Wookie!.Id,
                NbreSelfiesFromWookie = (item.Wookie?.Selfies?.Count)
                .GetValueOrDefault(0)
            })
                .ToList();

            return this.Ok(model);
        }

        //[Route("photos")]
        //[HttpPost]
        //public async Task<IActionResult> AddPicture()
        //{
        //   using var stream = new StreamReader(this.Request.Body);
        //   var content = await stream.ReadToEndAsync();
        //    return this.Ok();
        //}

        [Route("photos")]
        [HttpPost]
        public async Task<IActionResult> AddPicture(IFormFile picture)
        {
            string filePath = Path.Combine(this._webHostEnvironment.ContentRootPath, @"images\selfies");

            if(! Directory.Exists(filePath))
            {
                Directory.CreateDirectory(filePath);
            }
            filePath = Path.Combine(filePath, picture.FileName);

            using var stream = new FileStream(filePath, FileMode.OpenOrCreate);
            await picture.CopyToAsync(stream);

            var itemFile = this._repository.AddOnePicture(filePath);
            this._repository.UnitOfWork.SaveChanges();

            return this.Ok(itemFile);
        }


        [HttpPost]
        public IActionResult AddOne(SelfieDto dto)
        {
            IActionResult result = this.BadRequest();

            Selfie addSelfie = this._repository.AddOne(new Selfie()
            {
                Title = dto.Title,
                ImagePath = dto.ImagePath,
                
            });

            this._repository.UnitOfWork.SaveChanges();

           if(addSelfie != null)
            {
                dto.Id = addSelfie.Id;
                result= this.Ok(dto);
            }

            return result;
        }
    }
}
