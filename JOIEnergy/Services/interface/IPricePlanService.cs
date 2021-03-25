using System.Collections.Generic;
using WebOS.JOIEnergy.Dependency;

namespace JOIEnergy.Services
{
    [Dependency(DependencyEnum.Trainsient)]
    public interface IPricePlanService
    {
        Dictionary<string, decimal> GetConsumptionCostOfElectricityReadingsForEachPricePlan(string smartMeterId);
    }
}