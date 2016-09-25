namespace TicketingSystem.Common
{
    using System;
    using System.Text;

    public class RandomDataGenerator : IRandomDataGenerator
    {
        private const string Alphabet = "ABCDEFGHIJKLMNOPQRSTUVWXYZabcdefghijklmnopqrstuvwxyz0123456789";

        private static IRandomDataGenerator randomDataGenerator;
        private readonly Random random;

        private RandomDataGenerator()
        {
            this.random = new Random();
        }

        public static IRandomDataGenerator Instance
        {
            get
            {
                if (randomDataGenerator == null)
                {
                    randomDataGenerator = new RandomDataGenerator();
                }

                return randomDataGenerator;
            }
        }

        public string GetRandomString(int min, int max)
        {
            var result = new StringBuilder();
            var length = this.random.Next(min, max + 1);
            for (int i = 0; i < length; i++)
            {
                result.Append(Alphabet[this.random.Next(0, Alphabet.Length)]);
            }

            return result.ToString();
        }

        public int GetRandomNumber(int min, int max)
        {
            return this.random.Next(min, max + 1);
        }
    }
}
