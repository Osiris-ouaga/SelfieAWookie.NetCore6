using Microsoft.EntityFrameworkCore;
using SelfieAWookies.Core.Selfies.Domain;
using SelfieAWookies.Core.Selfies.Infrastructures.Data;
using Selfies.AWookies.Core.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SelfieAWookies.Core.Selfies.Infrastructures.Repositories
{
    public class DefaultSelfieRepository : ISelfieRepository
    {

        private readonly SelfiesContext _context;
        public DefaultSelfieRepository(SelfiesContext context) 
        {
            _context = context;
        }

        public ICollection<Selfie> GetAll(int? wookieId)
        {
            var query = this._context.Selfies.Include(item =>item.Wookie).AsQueryable();
            if (wookieId > 0)
            {
                query = query.Where(item => item.WookieId == wookieId);
            }

            return query.ToList();
        }

        public Selfie AddOne(Selfie item)
        {
            return this._context.Selfies.Add(item).Entity;
        }

        public IUnitOfWork UnitOfWork => this._context;

    }
}
