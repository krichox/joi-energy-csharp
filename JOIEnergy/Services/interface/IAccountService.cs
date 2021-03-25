using JOIEnergy.Enums;
using WebOS.JOIEnergy.Dependency;

namespace JOIEnergy.Services
{
    [Dependency(DependencyEnum.Trainsient)]
    public interface IAccountService
    {
        Supplier GetPricePlanIdForSmartMeterId(string smartMeterId);
    }
}