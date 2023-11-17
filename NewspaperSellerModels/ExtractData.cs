using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.IO;

namespace NewspaperSellerModels
{
    public class ExtractData
    {
        public static DataModel dataModel;
        public static string fileName;
        public static void extractFileData(string filePath)
        {
            dataModel = new DataModel();
            string currentKey = "";

            foreach (string line in File.ReadLines(filePath))
            {
                if (string.IsNullOrWhiteSpace(line)) continue;

                // Use a regular expression to match numbers in the line
                MatchCollection matches = Regex.Matches(line, @"\b\d+(\.\d+)?\b");

                if (matches.Count > 0)
                {
                    List<double> numbers = new List<double>();
                    foreach (Match match in matches)
                    {
                        if (double.TryParse(match.Value, out double number))
                        {
                            numbers.Add(number);
                        }
                    }

                    if (currentKey != "")
                    {
                        switch (currentKey)
                        {
                            case "NumOfNewspapers":
                                {
                                    dataModel.numOfNewspapers = (int)numbers[0];
                                }
                                break;
                            case "NumOfRecords":
                                {
                                    dataModel.numOfRecords = (int)numbers[0];
                                }
                                break;
                            case "PurchasePrice":
                                {
                                    dataModel.purchasePrice = (decimal)numbers[0];
                                }
                                break;
                            case "ScrapPrice":
                                {
                                    dataModel.scrapPrice = (decimal)numbers[0];
                                }
                                break;
                            case "SellingPrice":
                                {
                                    dataModel.sellingPrice = (decimal)numbers[0];
                                }
                                break;
                            case "DayTypeDistributions":
                                {
                                    dataModel.typeOfDay.goodProbability = (decimal)numbers[0];
                                    dataModel.typeOfDay.fairProbability = (decimal)numbers[1];
                                    dataModel.typeOfDay.poorProbability = (decimal)numbers[2];
                                }
                                break;
                            default:
                                {
                                    DemandDistributionsData temp = new DemandDistributionsData();
                                    temp.demand = (int)numbers[0];
                                    temp.goodProbability = (decimal)numbers[1];
                                    temp.fairProbability = (decimal)numbers[2];
                                    temp.poorProbability = (decimal)numbers[3];
                                    dataModel.demandDistributionsData.Add(temp);
                                }
                                break;
                        }
                    }
                }
                else
                {
                    currentKey = line.Trim();
                }
            }
        }
    }
}
