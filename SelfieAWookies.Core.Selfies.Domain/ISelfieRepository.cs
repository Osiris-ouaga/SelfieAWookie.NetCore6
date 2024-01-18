

using Selfies.AWookies.Core.Framework;

namespace SelfieAWookies.Core.Selfies.Domain
{
    public interface ISelfieRepository : IRepository
    {
        ICollection<Selfie> GetAll();
    }
}
