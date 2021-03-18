using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TeledocTest.Models;

namespace TeledocTest
{
    public static class Seeds
    {
        public static void Initialize(TeledocContext context)
        {
            Founder f1 = new Founder
            {
                Inn = "111111111111",
                FirstName = "Геннадий",
                LastName = "Петрулькин",
                SecondName = "Аристархович",
                FullName = "Петрулькин Геннадий Аристархович",
                CreateDate = DateTime.Now,
                UpdateDate = DateTime.Now
            };
            Founder f2 = new Founder
            {
                Inn = "222222222222",
                FirstName = "Аркадий",
                LastName = "Кармашкин",
                SecondName = "Евстигнеевич",
                FullName = "Кармашкин Аркадий Евстигнеевич",
                CreateDate = DateTime.Now,
                UpdateDate = DateTime.Now
            };
            Founder f3 = new Founder
            {
                Inn = "333333333333",
                FirstName = "Акакий",
                LastName = "Башмаков",
                SecondName = "",
                FullName = "Башмаков Акакий",
                CreateDate = DateTime.Now,
                UpdateDate = DateTime.Now
            };
            if (!context.Founders.Any())
            {
                context.Founders.Add(f1);
                context.Founders.Add(f2);
                context.Founders.Add(f3);
            }
            Client c1 = new Client
            {
                Inn = "123123123123",
                Name = "ООО Веселая скамейка",
                Type = "Юридическое лицо",
                CreateDate = DateTime.Now,
                UpdateDate = DateTime.Now,
                Founders = new List<Founder>() { f1, f2 }
            };
            Client c2 = new Client
            {
                Inn = "222222222222",
                Name = "ИП Кармашкин Аркадий Евстигнеевич",
                Type = "Индивидуальный предприниматель",
                CreateDate = DateTime.Now,
                UpdateDate = DateTime.Now,
                Founders = new List<Founder>() { f2 }
            };
            if (!context.Clients.Any())
            {
                context.Clients.Add(c1);
                context.Clients.Add(c2);
            }
            context.SaveChanges();
        }
    }
}
