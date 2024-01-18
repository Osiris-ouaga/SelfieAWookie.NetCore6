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

        public SelfieController(ISelfieRepository repository) 
        { 
            this._repository = repository;
        }

        //[HttpGet]   
        //public IEnumerable<Selfie> TestAMoi()
        //{
        //    return Enumerable.Range(1, 10).Select(item => new Selfie() { id = item });
        //}

        [HttpGet]
        public IActionResult TestAMoi()
        {
            //var model = Enumerable.Range(1, 10).Select(item => new Selfie() { Id = item });

            var selfiesList = this._repository.GetAll();
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

        public IActionResult AddOne(Selfie selfie)
        {
            return this.Ok(selfie);
        }
    }
}
