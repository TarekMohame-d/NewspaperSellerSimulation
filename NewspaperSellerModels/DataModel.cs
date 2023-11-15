using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NewspaperSellerModels
{
    public class DataModel
    {
        public int numOfNewspapers;
        public int numOfRecords; // days of simulation
        public decimal purchasePrice;
        public decimal scrapPrice;
        public decimal sellingPrice;
        public TypeOfDay typeOfDay = new TypeOfDay();
        public List<DemandDistributionsData> demandDistributionsData = new List<DemandDistributionsData>();
    }
    public class TypeOfDay
    {
        public decimal goodProbability;
        public decimal fairProbability;
        public decimal poorProbability;
    }
    public class DemandDistributionsData
    {
        public int demand;
        public decimal goodProbability;
        public decimal fairProbability;
        public decimal poorProbability;
    }
}
