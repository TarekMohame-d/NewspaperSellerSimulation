using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NewspaperSellerModels
{
    public class Calculations
    {
        public static List<DemandDistribution> demandDistributionTable = new List<DemandDistribution>();
        public static List<DayTypeDistribution> newsdayTable = new List<DayTypeDistribution>();
        public static SimulationSystem systemTable = new SimulationSystem();
        public static void fillNewsdayTable()
        {
            DataModel model = ExtractData.dataModel;
            DayTypeDistribution temp = new DayTypeDistribution();
            decimal CummProbability = 0;
            int minRange = 0;
            int maxRange = 0;
            for (int i = 0; i < 3; i++)
            {
                temp = new DayTypeDistribution();
                switch (i)
                {
                    case 0:
                        {
                            temp.DayType = (Enums.DayType)i;
                            temp.Probability = model.typeOfDay.goodProbability;
                            CummProbability += model.typeOfDay.goodProbability;
                            temp.CummProbability = CummProbability;
                            minRange = 1;
                            maxRange = (int)(CummProbability * 100);
                            temp.MinRange = minRange;
                            temp.MaxRange = maxRange;
                            newsdayTable.Add(temp);
                        }
                        break;
                    case 1:
                        {
                            temp.DayType = (Enums.DayType)i;
                            temp.Probability = model.typeOfDay.fairProbability;
                            CummProbability += model.typeOfDay.fairProbability;
                            temp.CummProbability = CummProbability;
                            minRange = maxRange + 1;
                            maxRange = (int)(CummProbability * 100);
                            temp.MinRange = minRange;
                            temp.MaxRange = maxRange;
                            newsdayTable.Add(temp);
                        }
                        break;
                    case 2:
                        {
                            temp.DayType = (Enums.DayType)i;
                            temp.Probability = model.typeOfDay.poorProbability;
                            CummProbability += model.typeOfDay.poorProbability;
                            temp.CummProbability = CummProbability;
                            minRange = maxRange + 1;
                            maxRange = (int)(CummProbability * 100);
                            temp.MinRange = minRange;
                            temp.MaxRange = maxRange;
                            newsdayTable.Add(temp);
                        }
                        break;
                }
            }
        }

        public static void fillDemandTable()
        {
            DemandDistribution temp = new DemandDistribution();
            DataModel model = ExtractData.dataModel;
            DayTypeDistribution dayDistributions = new DayTypeDistribution();
            decimal cummGoodProbability = 0;
            decimal cummFairProbability = 0;
            decimal cummPoorProbability = 0;
            int minRangeGood = 0;
            int maxRangeGood = 0;
            int minRangeFair = 0;
            int maxRangeFair = 0;
            int minRangePoor = 0;
            int maxRangePoor = 0;
            for (int i = 0; i < model.demandDistributionsData.Count; i++)
            {
                temp = new DemandDistribution();
                temp.Demand = model.demandDistributionsData[i].demand;
                for (int j = 0; j < 3; j++)
                {
                    dayDistributions = new DayTypeDistribution();
                    switch (j)
                    {
                        case 0:
                            {
                                cummGoodProbability += model.demandDistributionsData[i].goodProbability;
                                dayDistributions.CummProbability = cummGoodProbability;
                                minRangeGood = maxRangeGood + 1;
                                maxRangeGood = (int)(cummGoodProbability * 100);
                                dayDistributions.MinRange = minRangeGood;
                                dayDistributions.MaxRange = maxRangeGood;
                                dayDistributions.DayType = (Enums.DayType)j;
                            }
                            break;
                        case 1:
                            {
                                cummFairProbability += model.demandDistributionsData[i].fairProbability;
                                dayDistributions.CummProbability = cummFairProbability;
                                minRangeFair = maxRangeFair + 1;
                                maxRangeFair = (int)(cummFairProbability * 100);
                                if (minRangeFair >= 100)
                                {
                                    dayDistributions.MinRange = -1;
                                    dayDistributions.MaxRange = -1;
                                }
                                else
                                {
                                    dayDistributions.MinRange = minRangeFair;
                                    dayDistributions.MaxRange = maxRangeFair;
                                }
                                dayDistributions.DayType = (Enums.DayType)j;
                            }
                            break;
                        case 2:
                            {
                                cummPoorProbability += model.demandDistributionsData[i].poorProbability;
                                dayDistributions.CummProbability = cummPoorProbability;
                                minRangePoor = maxRangePoor + 1;
                                maxRangePoor = (int)(cummPoorProbability * 100);
                                if (minRangePoor >= 100)
                                {
                                    dayDistributions.MinRange = -1;
                                    dayDistributions.MaxRange = -1;
                                }
                                else
                                {
                                    dayDistributions.MinRange = minRangePoor;
                                    dayDistributions.MaxRange = maxRangePoor;
                                }
                                dayDistributions.DayType = (Enums.DayType)j;
                            }
                            break;
                    }
                    temp.DayTypeDistributions.Add(dayDistributions);
                }
                demandDistributionTable.Add(temp);
            }
        }

        public static void fillSystemTable()
        {
            DataModel model = ExtractData.dataModel;
            SimulationCase simulationCase;
            List<int> newsdaysRandomNumbers = RandomNumber.GenerateRandomNumbers(model.numOfRecords);
            List<int> demandRandomNumbers = RandomNumber.GenerateRandomNumbers(model.numOfRecords);
            int daysWithMoreDemand = 0;
            int daysWithUnsoldPapers = 0;
            for (int i = 0; i < model.numOfRecords; i++)
            {
                simulationCase = new SimulationCase();
                int randomNewsDayTypeNumber = newsdaysRandomNumbers[i];
                int randomDemandNumber = demandRandomNumbers[i];
                simulationCase.DayNo = i + 1;
                simulationCase.RandomNewsDayType = randomNewsDayTypeNumber;
                for (int j = 0; j < newsdayTable.Count; j++)
                {
                    if (randomNewsDayTypeNumber >= newsdayTable[j].MinRange &&
                        randomNewsDayTypeNumber <= newsdayTable[j].MaxRange)
                    {
                        simulationCase.NewsDayType = newsdayTable[j].DayType;
                        break;
                    }
                }
                simulationCase.RandomDemand = randomDemandNumber;
                bool found = false;
                for (int j = 0; j < demandDistributionTable.Count; j++)
                {
                    for (int k = 0; k < demandDistributionTable[j].DayTypeDistributions.Count; k++)
                    {
                        if (demandDistributionTable[j].DayTypeDistributions[k].DayType == simulationCase.NewsDayType)
                        {
                            if (randomDemandNumber >= demandDistributionTable[j].DayTypeDistributions[k].MinRange &&
                            randomDemandNumber <= demandDistributionTable[j].DayTypeDistributions[k].MaxRange)
                            {
                                simulationCase.Demand = demandDistributionTable[j].Demand;
                                found = true;
                                break;
                            }
                        }
                    }
                    if (found) break;
                }
                simulationCase.DailyCost = (decimal)(model.numOfNewspapers * 33) / 100;
                if(model.numOfNewspapers >= simulationCase.Demand)
                {
                    simulationCase.SalesProfit = (decimal)(simulationCase.Demand * 50) / 100;
                }
                else
                {
                    simulationCase.SalesProfit = (decimal)(model.numOfNewspapers * 50) / 100;
                }
                decimal lostProfit = (decimal)((simulationCase.Demand - model.numOfNewspapers) * (50 - 33)) / 100;
                if (lostProfit <= 0)
                {
                    simulationCase.LostProfit = 0;
                }
                else
                {
                    daysWithMoreDemand += 1;
                    simulationCase.LostProfit = lostProfit;
                }
                decimal scrapProfit = (decimal)((model.numOfNewspapers - simulationCase.Demand) * 5) / 100;
                if (scrapProfit <= 0)
                {
                    simulationCase.ScrapProfit = 0;
                }
                else
                {
                    daysWithUnsoldPapers += 1;
                    simulationCase.ScrapProfit = scrapProfit;
                }
                simulationCase.DailyNetProfit = (simulationCase.SalesProfit - simulationCase.DailyCost -
                    simulationCase.LostProfit + simulationCase.ScrapProfit);
                systemTable.SimulationTable.Add(simulationCase);
            }
            systemTable.PerformanceMeasures.DaysWithMoreDemand = daysWithMoreDemand;
            systemTable.PerformanceMeasures.DaysWithUnsoldPapers = daysWithUnsoldPapers;
        }
    }
}
