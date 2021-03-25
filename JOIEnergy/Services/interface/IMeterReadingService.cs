using System.Collections.Generic;
using JOIEnergy.Domain;
using WebOS.JOIEnergy.Dependency;

namespace JOIEnergy.Services
{
    [Dependency(DependencyEnum.Trainsient)]
    public interface IMeterReadingService
    {
        List<ElectricityReading> GetReadings(string smartMeterId);
        void StoreReadings(string smartMeterId, List<ElectricityReading> electricityReadings);
    }
}