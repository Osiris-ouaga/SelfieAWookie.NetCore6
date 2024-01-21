

using Selfies.AWookies.Core.Framework;

namespace SelfieAWookies.Core.Selfies.Domain
{
    public interface ISelfieRepository : IRepository
    {
        ICollection<Selfie> GetAll(int? wookieId);

        Selfie AddOne(Selfie item);

        Picture AddOnePicture(string url);

        //Picture AddOnePicture(int selfieId, string url);
    }
}
