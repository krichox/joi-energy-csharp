using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace JOIEnergy.Domain
{
    public class MeterReadings
    {
        [Required]
        public string SmartMeterId { get; set; }
        [Required]
        public List<ElectricityReading> ElectricityReadings { get; set; }
    }
}
