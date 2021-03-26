using JOIEnergy.Domain;
using JOIEnergy.Enums;
using JOIEnergy.Generator;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace JOIEnergy.WebOS
{
    /// <summary>
    ///  初始化数据
    /// </summary>
    public static class InitClassDatas
    {
        public static IServiceCollection InitJOIEnergyDatas(this IServiceCollection services)
        {
            var readings =
                GenerateMeterElectricityReadings();
            List<PricePlan> pricePlans = GeneratePricePlan();
            services.AddSingleton((IServiceProvider arg) => readings);
            services.AddSingleton((IServiceProvider arg) => pricePlans);
            services.AddSingleton((IServiceProvider arg) => SmartMeterToPricePlanAccounts);
            return services;
        }

        private static List<PricePlan> GeneratePricePlan()
        {
            return new List<PricePlan> {
                new PricePlan{
                    EnergySupplier = Enums.Supplier.DrEvilsDarkEnergy,
                    UnitRate = 10m,
                    PeakTimeMultiplier = new List<PeakTimeMultiplier>()
                },
                new PricePlan{
                    EnergySupplier = Enums.Supplier.TheGreenEco,
                    UnitRate = 2m,
                    PeakTimeMultiplier = new List<PeakTimeMultiplier>()
                },
                new PricePlan{
                    EnergySupplier = Enums.Supplier.PowerForEveryone,
                    UnitRate = 1m,
                    PeakTimeMultiplier = new List<PeakTimeMultiplier>()
                }
            };
        }

        private static Dictionary<string, List<ElectricityReading>> GenerateMeterElectricityReadings()
        {
            var readings = new Dictionary<string, List<ElectricityReading>>();
            var generator = new ElectricityReadingGenerator();
            var smartMeterIds = SmartMeterToPricePlanAccounts.Select(mtpp => mtpp.Key);

            foreach (var smartMeterId in smartMeterIds)
            {
                readings.Add(smartMeterId, generator.Generate(20));
            }
            return readings;
        }

        public static Dictionary<String, Supplier> SmartMeterToPricePlanAccounts
        {
            get
            {
                Dictionary<String, Supplier> smartMeterToPricePlanAccounts = new Dictionary<string, Supplier>();
                smartMeterToPricePlanAccounts.Add("smart-meter-0", Supplier.DrEvilsDarkEnergy);
                smartMeterToPricePlanAccounts.Add("smart-meter-1", Supplier.TheGreenEco);
                smartMeterToPricePlanAccounts.Add("smart-meter-2", Supplier.DrEvilsDarkEnergy);
                smartMeterToPricePlanAccounts.Add("smart-meter-3", Supplier.PowerForEveryone);
                smartMeterToPricePlanAccounts.Add("smart-meter-4", Supplier.TheGreenEco);
                return smartMeterToPricePlanAccounts;
            }

            //用户 智能电表ID 电力供应商
            //莎拉 smart-meter-0	邪恶博士的黑暗能量
            //彼德 smart-meter-1	绿色生态
            //查理 smart-meter-2	邪恶博士的黑暗能量
            //安德里亚 smart-meter-3	每个人的力量
            //亚历克斯 smart-meter-4	绿色生态

            //Sarah smart-meter-0	Dr Evil's Dark Energy
            //Peter smart-meter-1	The Green Eco
            //Charlie smart-meter-2	Dr Evil's Dark Energy
            //Andrea smart-meter-3	Power for Everyone
            //Alex smart-meter-4	The Green Eco
        }
    }
}

