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

        public ICollection<Selfie> GetAll()
        {
            return this._context.Selfies.Include(item =>item.Wookie).ToList();
        }

        public IUnitOfWork unitOfWork => this._context;

    }
}
