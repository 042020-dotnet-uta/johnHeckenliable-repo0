using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Xml.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.Entity;
using System.Linq.Expressions;

namespace Cars
{
    class Program
    {
        static void Main(string[] args)
        {
            //QueryCsv();

            //CreateXml();
            //QueryXml("Fuel.xml");

            #region Looking at Func vs Expression
            /*
            Func<int, int> square = x => x * x;
            Expression<Func<int, int, int>> add = (x, y) => x + y;
            Func<int, int, int> IAdd = add.Compile();
            var result = IAdd(3, 5);
            Console.WriteLine(result);
            Console.WriteLine(add);
            */
            #endregion

            //Not to be used in an actual project (just here to make things simpler as changes etc...happen)
            Database.SetInitializer(new DropCreateDatabaseIfModelChanges<CarDb>());
            InsertData();
            QueryData();
        }

        private static void QueryData()
        {
            var db = new CarDb();

            //Setting he databases log function to use Console.WriteLine (i.e. the database will log to the console)
            db.Database.Log = Console.WriteLine;

            var query =
                from car in db.Cars
                group car by car.Manufacturer into manufacturer
                select new
                {
                    Name = manufacturer.Key,
                    Cars = (from car in manufacturer
                           orderby car.Combined descending
                           select car).Take(2)
                };

            var query2 =
                db.Cars.GroupBy(c => c.Manufacturer)
                        .Select(g => new
                        {
                            Name = g.Key,
                            Cars = g.OrderByDescending(c => c.Combined).Take(2)
                        });

            foreach (var group in query)
            {
                Console.WriteLine(group.Name);
                foreach (var car in group.Cars)
                {
                    Console.WriteLine($"\t{car.Name}: {car.Combined}");
                }
            }
        }

        private static void InsertData()
        {
            var cars = ProcessCarsFile("Fuel.csv");
            var db = new CarDb();

            if (!db.Cars.Any())
            {
                foreach (var car in cars)
                {
                    db.Cars.Add(car);
                }
                db.SaveChanges();
            }
        }

        private static void QueryCsv()
        {
            var carRecords = ProcessCarsFile("Fuel.csv");
            var manufacturers = ProcessManufacturersFile("manufacturers.csv");

            #region Test Queries
            //Testing All, Any, Contains
            var allResult = carRecords.All(c => c.Manufacturer == "Ford");
            //Console.WriteLine(allResult);

            //Gets only the first result that matches
            //Probably more efficient  to use a Where clause to filter first
            var top = carRecords.OrderByDescending(c => c.Combined)
                          .ThenBy(c => c.Name)
                          .FirstOrDefault(c => c.Manufacturer == "BMW" && c.Year == 2016);
            //Console.WriteLine(top.Name);

            var manyResult = carRecords.SelectMany(c => c.Name)
                                 .OrderBy(c => c);
            /*
            foreach (var character in manyResult)
            {
                Console.WriteLine(character);
            }
            */
            #endregion

            #region Joins
            var joinQuery =
                 from car in carRecords
                 join manufacturer in manufacturers
                 on new { car.Manufacturer, car.Year }
                    equals
                    new { Manufacturer = manufacturer.Name, manufacturer.Year }
                 orderby car.Combined descending, car.Name
                 select new
                 {
                     manufacturer.Headquarters,
                     car.Name,
                     car.Combined
                 };
            var joinQuery2 =
                carRecords.Join(manufacturers,
                    c => new { c.Manufacturer, c.Year },
                    m => new { Manufacturer = m.Name, m.Year },
                    (c, m) => new
                    {
                        m.Headquarters,
                        c.Name,
                        c.Combined
                    })
                .OrderByDescending(c => c.Combined)
                .ThenBy(c => c.Name);
            /*
            foreach (var car in joinQuery2.Take(10))
            {
                Console.WriteLine($"{car.Headquarters} {car.Name} : {car.Combined}");
            }
            */
            #endregion

            #region GroupBy
            var groupQuery =
                from car in carRecords
                group car by car.Manufacturer.ToUpper() into manufacturer
                orderby manufacturer.Key
                select manufacturer;
            var groupQuery2 =
                carRecords.GroupBy(c => c.Manufacturer.ToUpper())
                    .OrderBy(g => g.Key);

            /*
            foreach (var group in groupQuery2)
            {
                Console.WriteLine(group.Key);
                foreach (var car in group.OrderByDescending(c=>c.Combined).Take(2))
                {
                    Console.WriteLine($"\t{car.Name} : {car.Combined}");
                }
            }
            */
            #endregion

            #region GroupJoin
            var groupJoinQuery =
                from manufacturer in manufacturers
                join car in carRecords on manufacturer.Name equals car.Manufacturer
                    into carGroup
                orderby manufacturer.Name
                select new
                {
                    Manufacturer = manufacturer,
                    Cars = carGroup
                };
            var groupJoinQuery2 =
                manufacturers.GroupJoin(carRecords, m => m.Name, c => c.Manufacturer, (m, g) =>
                  new
                  {
                      Manufacturer = m,
                      Cars = g
                  }).OrderBy(m => m.Manufacturer.Name);

            /*
            foreach (var group in groupJoinQuery2)
            {
                Console.WriteLine($"{group.Manufacturer.Name} : {group.Manufacturer.Headquarters}");
                foreach (var car in group.Cars.OrderByDescending(c => c.Combined).Take(2))
                {
                    Console.WriteLine($"\t{car.Name} : {car.Combined}");
                }
            }
            */
            #endregion

            #region Test Group/joins
            var testQuery =
                 from manufacturer in manufacturers
                 join car in carRecords on manufacturer.Name equals car.Manufacturer
                 orderby manufacturer.Headquarters
                 select new
                 {
                     manufacturer.Headquarters,
                     car.Name,
                     car.Combined
                 } into result
                 group result by result.Headquarters;

            /*
            Console.WriteLine("**********Query Syntax**********");
            Console.WriteLine();
            foreach (var group in testQuery)
            {
                Console.WriteLine($"{group.Key}");
                foreach (var car in group.OrderByDescending(c => c.Combined).Take(3))
                {
                    Console.WriteLine($"\t{car.Name} : {car.Combined}");
                }
            }
            */
            var testQuery2 =
                manufacturers.GroupJoin(carRecords, m => m.Name, c => c.Manufacturer, (m, g) =>
                  new
                  {
                      Manufacturer = m,
                      Cars = g
                  })
                .OrderBy(m => m.Manufacturer.Headquarters)
                .GroupBy(m => m.Manufacturer.Headquarters);
            /*
            Console.WriteLine();
            Console.WriteLine("**********Method Syntax**********");
            Console.WriteLine();
            foreach (var group in testQuery2)
            {
                Console.WriteLine($"{group.Key}");
                foreach (var car in group.SelectMany(c=>c.Cars).OrderByDescending(c => c.Combined).Take(3))
                {
                    Console.WriteLine($"\t{car.Name} : {car.Combined}");
                }
            }
            */
            #endregion

            #region Aggregates
            var aggregateQuery =
                from car in carRecords
                group car by car.Manufacturer into carGroup
                select new
                {
                    Name = carGroup.Key,
                    //Must run through each group for every aggregate (i.e. three times)
                    Max = carGroup.Max(c => c.Combined),
                    Min = carGroup.Min(c => c.Combined),
                    Avg = carGroup.Average(c => c.Combined)
                } into result
                orderby result.Max descending
                select result;

            var aggregateQuery2 =
                carRecords.GroupBy(c => c.Manufacturer)
                .Select(g =>
                {
                    //Method to get all aggregate data at once (i.e. only loop through each group once)
                    var results = g.Aggregate(new CarStatistics(),
                                    (acc, c) => acc.Accumulate(c),
                                    acc => acc.Compute());
                    return new
                    {
                        Name = g.Key,
                        Max = results.Max,
                        Min = results.Min,
                        Avg = results.Avg
                    };
                })
                .OrderByDescending(r => r.Max);
            /*
            foreach (var result in aggregateQuery2)
            {
                Console.WriteLine($"{result.Name}");
                Console.WriteLine($"\tMax: {result.Max}");
                Console.WriteLine($"\tMix: {result.Min}");
                Console.WriteLine($"\tAvg: {result.Avg.ToString("F1")}");
            }
            */
            #endregion

        }

        private static void QueryXml(string path)
        {
            var ns = (XNamespace)"http://pluralsight.com/cars/2016";
            var ex = (XNamespace)"http://pluralsight.com/cars/2016/ex";
            var document = XDocument.Load(path);

            var query =
                from element in document.Element(ns + "Cars")?.Elements(ex + "Car") 
                                                            ?? Enumerable.Empty<XElement>()//if the from returns null this will change it to an empty Enumerable
                where element.Attribute("Manufacturer")?.Value == "BMW"
                select element.Attribute("Name").Value;

            foreach (var name in query)
            {
                Console.WriteLine(name);
            }
        }

        private static void CreateXml()
        {
            var carRecords = ProcessCarsFile("Fuel.csv");

            var ns = (XNamespace)"http://pluralsight.com/cars/2016";
            var ex = (XNamespace)"http://pluralsight.com/cars/2016/ex";

            var document = new XDocument();
            var cars = new XElement(ns + "Cars",
                from record in carRecords
                select new XElement(ex + "Car",
                        new XAttribute("Name", record.Name),
                        new XAttribute("Manufacturer", record.Manufacturer),
                        new XAttribute("Displacement", record.Displacement),
                        new XAttribute("Cylinders", record.Cylinders),
                        new XAttribute("City", record.City),
                        new XAttribute("Highway", record.Highway),
                        new XAttribute("Combined", record.Combined),
                        new XAttribute("Year", record.Year))
                );
            //Creates a prefix for the namespace
            cars.Add(new XAttribute(XNamespace.Xmlns + "ex", ex));

            document.Add(cars);
            document.Save("Fuel.xml");
        }

        private static List<Car> ProcessCarsFile(string path)
        {
            var query =
                File.ReadAllLines(path)
                    .Skip(1)
                    .Where(line => line.Length > 1)
                    .ToCar();

            return query.ToList();
        }
        private static List<Manufacturer> ProcessManufacturersFile(string path)
        {
            var query =
                File.ReadAllLines(path)
                    .Where(line => line.Length > 1)
                    .ToManufacturer();

            return query.ToList();
        }
    }
}
