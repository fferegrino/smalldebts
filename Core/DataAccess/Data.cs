using System;
using System.Collections.Generic;
using Smalldebts.ItermediateObjects;
using System.Globalization;
using System.Threading.Tasks;

namespace Smalldebts.Core.DataAccess
{
    public class Data
    {
        static CultureInfo ci = new CultureInfo("en");
        public static Debt Detail = new Debt
        {
            Id = "9ec96add-f03f-4a2f-aafb-7d942aa3cdaf",
            Name = "Aaron",
            //Movements = new List<Movement>
            //{
            //    new Movement { Amount = -7480.61m, Date = DateTimeOffset.ParseExact("12/22/2016","MM/dd/yyyy", ci) },
            //    new Movement { Amount = 491.29m, Date = DateTimeOffset.ParseExact("08/26/2016"  ,"MM/dd/yyyy", ci) },
            //    new Movement { Amount = 3034.67m, Date = DateTimeOffset.ParseExact("10/22/2016" ,"MM/dd/yyyy", ci) },
            //    new Movement { Amount = 8673.48m, Date = DateTimeOffset.ParseExact("02/10/2017" ,"MM/dd/yyyy", ci) },
            //    new Movement { Amount = -1650.55m, Date = DateTimeOffset.ParseExact("03/30/2016","MM/dd/yyyy", ci) },
            //    new Movement { Amount = -5664.99m, Date = DateTimeOffset.ParseExact("04/28/2016","MM/dd/yyyy", ci) },
            //    new Movement { Amount = 9029.06m, Date = DateTimeOffset.ParseExact("03/30/2016" ,"MM/dd/yyyy", ci) },
            //    new Movement { Amount = 1370.26m, Date = DateTimeOffset.ParseExact("06/04/2016" ,"MM/dd/yyyy", ci) },
            //    new Movement { Amount = -8707.55m, Date = DateTimeOffset.ParseExact("03/13/2016","MM/dd/yyyy", ci) },
            //    new Movement { Amount = -1849.52m, Date = DateTimeOffset.ParseExact("05/15/2016","MM/dd/yyyy", ci) },
            //    new Movement { Amount = 9144.73m, Date = DateTimeOffset.ParseExact("07/19/2016" ,"MM/dd/yyyy", ci) },
            //    new Movement { Amount = -8450.14m, Date = DateTimeOffset.ParseExact("05/30/2016","MM/dd/yyyy", ci) },
            //    new Movement { Amount = -2651.97m, Date = DateTimeOffset.ParseExact("09/06/2016","MM/dd/yyyy", ci) },
            //    new Movement { Amount = -5496.81m, Date = DateTimeOffset.ParseExact("02/04/2017","MM/dd/yyyy", ci) },
            //    new Movement { Amount = -3254.46m, Date = DateTimeOffset.ParseExact("02/27/2016","MM/dd/yyyy", ci) },
            //}
        };

        public static List<Debt> Debts = new List<Debt>()
        {
            new Debt { Id = "551ef9ab-0714-4b70-b887-f1290d2053c3", Name = "Ruby", Balance = 0m },
            new Debt { Id = "2a5f867b-6bf6-4bbe-9e1f-74f63e8b4dd2", Name = "David", Balance = -2878.34m },
            new Debt { Id = "09c07cfd-bc19-4240-86bf-008adbcbc064", Name = "Benjamin", Balance = 4911.41m },
            new Debt { Id = "9ae84c63-bfc4-453b-995c-81787fc2d6d1", Name = "Janet", Balance = -9043.6m },
            new Debt { Id = "102c5c81-9ef9-4228-bb76-c68ac66734ca", Name = "Louise", Balance = 7922.95m },
            new Debt { Id = "c2465f12-32a6-4bbd-8f0a-f5973ac8dc88", Name = "Michael", Balance = -9865.14m },
            new Debt { Id = "975367d6-7770-40b5-b9dd-66cf7bd373b2", Name = "Anna", Balance = 8334.43m },
            new Debt { Id = "fb85522e-9870-46f6-8f33-fdc327d46998", Name = "Lillian", Balance = 1066.01m },
            new Debt { Id = "ebbb87b7-07c9-4ac5-a2fd-14e860146fe5", Name = "Brenda", Balance = 0m },
            new Debt { Id = "d3053457-5ff7-4a16-b561-08d14a9850cb", Name = "Brian", Balance = 4969.52m },
            new Debt { Id = "7e85de8c-572a-471a-885f-7a27fd4eaa3a", Name = "Jimmy", Balance = 4874.23m },
            new Debt { Id = "c4c4a83b-fb3b-4339-950e-a2f7decad4aa", Name = "William", Balance = 3107.42m },
            new Debt { Id = "bc3e6c0c-4d20-448d-a2c3-676a3728f271", Name = "Dorothy", Balance = 9514.66m },
            new Debt { Id = "b71022e5-0f28-4cd2-8276-abf5763fa97f", Name = "Juan", Balance = 3016.39m },
            new Debt { Id = "2408a365-5573-4b58-ba88-7d388621dc6f", Name = "Rachel", Balance = 2201.01m },
            new Debt { Id = "0c1e2bf2-2d24-4afd-b161-163e8014b5f8", Name = "Tammy", Balance = 9040.57m },
            new Debt { Id = "fc356acf-66ff-48f2-8362-fa5e8af89751", Name = "Maria", Balance = -5635.18m },
            new Debt { Id = "841eedfb-3e8b-4248-8ffe-5c86d0e4931c", Name = "Kathleen", Balance = 9178.82m },
            new Debt { Id = "455ebac2-9fdd-4606-9b86-4bf39e599d37", Name = "Jessica", Balance = 7139.22m },
            new Debt { Id = "ff014a5c-eecd-4273-acb8-c92ca06e968a", Name = "Nicole", Balance = 5101.73m },
            new Debt { Id = "99ea9bf2-aaab-4810-95df-4efcbbd4bf86", Name = "Adam", Balance = -6697.8m },
            new Debt { Id = "d658cf15-4b53-4f23-8871-c509533b20f0", Name = "Sarah", Balance = 5228.05m },
            new Debt { Id = "da6e89e4-8329-4307-904c-c3fa3752ed62", Name = "Jeffrey", Balance = -348.04m },
            new Debt { Id = "98e54e60-ccbd-4cb2-851c-0e645ff86528", Name = "Cynthia", Balance = 3038.21m },
            new Debt { Id = "9bdc9491-ba56-42ff-a4ca-5bc31e751856", Name = "Diane", Balance = -7801.67m },
            new Debt { Id = "f73cd5ef-ee7f-459b-8a8d-3713bf6cad6c", Name = "David", Balance = 1654.91m },
            new Debt { Id = "16364eb4-6836-4f30-bb34-e496248bae65", Name = "Johnny", Balance = 5617m },
            new Debt { Id = "a16f3623-e799-419f-b3d1-3e843d50542f", Name = "Nancy", Balance = 3348.18m },
            new Debt { Id = "f7373cc4-5fb7-4e0f-99e4-67455a36483e", Name = "Timothy", Balance = -7741.36m },
            new Debt { Id = "8f6bfca6-3c84-4e93-a368-54240fb7d3bc", Name = "Bonnie", Balance = -3795.95m },
            new Debt { Id = "50bd014c-d39c-441d-84c2-f5474b300563", Name = "Louis", Balance = 5661.59m },
            new Debt { Id = "0c6e79de-09ca-4529-87fb-265cce883a6f", Name = "Nicole", Balance = 7326.57m },
            new Debt { Id = "a716d4c4-0a3a-4bc2-9d17-35f238c82d74", Name = "Catherine", Balance = 3766.36m },
            new Debt { Id = "d7cfd35f-4733-4466-9e2c-cb5f8f57de30", Name = "Lawrence", Balance = 4639.7m },
            new Debt { Id = "71ebbc0c-2c02-4817-bc01-7f7f040b8f12", Name = "Sean", Balance = -8413.6m },
            new Debt { Id = "8caa29c2-1da5-4454-b67a-d6e2f412a19a", Name = "Nicholas", Balance = 6981.6m },
            new Debt { Id = "27944d1c-7e13-464b-83b1-0a58c5fa38f2", Name = "Janice", Balance = 4588.56m },
            new Debt { Id = "def5d0b5-8d61-4e80-8aa8-d36d09f6ffed", Name = "Gloria", Balance = -4443.07m },
            new Debt { Id = "3ecd464f-6abb-45e5-b0d7-54e4004bad6e", Name = "Kathryn", Balance = 9788.64m },
            new Debt { Id = "2181840c-9dfa-4162-85a4-4a649b5284c4", Name = "Theresa", Balance = -5887.22m },
            new Debt { Id = "30461440-e820-4131-8000-11e5dab8fa5c", Name = "Ernest", Balance = -8042.81m },
            new Debt { Id = "500d3bfa-0269-48ef-818d-f63d5626e479", Name = "Marilyn", Balance = 5500.13m },
            new Debt { Id = "cdf11a42-e783-4b87-8689-7cdabf119749", Name = "Billy", Balance = -1782.5m },
            new Debt { Id = "7d2cd041-6ce7-4910-945d-0bbf1c18f64d", Name = "Adam", Balance = 8480.99m },
            new Debt { Id = "61d0a31c-3a00-48ec-991f-7bf814511231", Name = "Wanda", Balance = -8865.38m },
            new Debt { Id = "6cff6ab3-283e-48bc-9820-b7e9e58abc3b", Name = "Joshua", Balance = -2702.3m },
            new Debt { Id = "8551ecd5-7ef0-4f8e-9b86-d9f34618b37d", Name = "Carolyn", Balance = 4415.36m },
            new Debt { Id = "353fbc6a-90b7-4664-b4ad-fcbe19dd94d0", Name = "Ernest", Balance = -5327.52m },
            new Debt { Id = "111025cb-55fe-4368-8070-da153104cee3", Name = "Beverly", Balance = 107.32m },
            new Debt { Id = "49e4f60b-7863-4ec0-8035-5103839bcbdf", Name = "Jose", Balance = -7420.37m },
            new Debt { Id = "d3025545-e271-4e33-99e9-6cd6d4cbe1c9", Name = "Amanda", Balance = -4935.07m },
            new Debt { Id = "43f37c21-9363-432d-ac1e-1026c3f8c724", Name = "Jacqueline", Balance = -3537.22m },
            new Debt { Id = "e0f9284f-b875-45ab-8dd4-bd10b54fb666", Name = "Virginia", Balance = 310.91m },
            new Debt { Id = "71b6d068-8319-49b3-8302-ae6dd34f62c3", Name = "Christopher", Balance = -8111.83m },
            new Debt { Id = "0fbe0a68-f6a0-45c9-86fa-85013ee07ef0", Name = "Kevin", Balance = -5328.57m },
            new Debt { Id = "43c95628-e073-4b29-a07e-6c2739c1d480", Name = "Lisa", Balance = -4987.37m },
            new Debt { Id = "ad484eb6-f8a2-4cd5-916b-aad4d6edc5fa", Name = "Jane", Balance = -6829.33m },
            new Debt { Id = "df5dda9f-39f3-4e23-909f-06c8415e9487", Name = "Carolyn", Balance = -3132.19m },
            new Debt { Id = "0fb110a7-2929-4578-97b4-c61b70b83328", Name = "Diana", Balance = 940.44m },
            new Debt { Id = "4228e1e4-19d5-4785-90c2-a92f2ed5849f", Name = "Stephen", Balance = 2926.59m }
        };


		public static async Task<Debt> CreateNewDebt(Debt d)
		{
			await Task.Delay(2000);
			return new Debt { Id = Guid.NewGuid().ToString(), Balance = d.Balance, Name = d.Name };
		}

		public static async Task<Debt> ModifyDebt(Debt d)
		{
			await Task.Delay(2000);
			return new Debt { Id = d.Id, Balance = d.Balance, Name = d.Name };
		}
    }
}
