using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NewspaperSellerModels
{
    public class RandomNumber
    {
        private static Random random = new Random();
        public static List<int> GenerateRandomNumbers(int count)
        {
            List<int> uniqueNumbers = new List<int>();

            while (uniqueNumbers.Count < count)
            {
                int randomNumber = random.Next(1, 101);
                if (!uniqueNumbers.Contains(randomNumber))
                {
                    uniqueNumbers.Add(random.Next(1, 101));
                }
            }
            return uniqueNumbers;
        }
    }
}
