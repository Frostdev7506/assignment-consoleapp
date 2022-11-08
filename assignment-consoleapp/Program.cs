using System;
using CsvHelper;
using System.IO;
using System.Globalization;
using System.Linq;
using CsvHelper.Configuration;
using CsvHelper.Configuration.Attributes;





class csvedit
{
    class program
    {
        public double yearMean { get; set; }

        public string? date { get; set; }

        public double yearMedian { get; set; }

        public string? isBeerProductionGreaterThanYearMean { get; set; }

        
        static void Main(string[] args)
        {
            using (var streamReader = new StreamReader(@"C:\Users\Frostdev\Desktop\Input_v1.0.csv"))
            {

                using (var csvReader = new CsvReader(streamReader, CultureInfo.InvariantCulture))
                {
                    //var records = csvReader.GetRecords<TableData>().ToList();

                    csvReader.Context.RegisterClassMap<TableDataClassMap>();

                    var record = new TableData();
                    var records = csvReader.EnumerateRecords(record);


                    var data = csvReader.GetRecords<TableData>().ToList();




                    int date = 1992;
                    double beerValue = 0;
                    Dictionary<int,double> beerValues = new Dictionary<int,double>();
                    int count = 0;
                    int iterator=0;
                    int jterator = 0;
                    int kterator = 0;

                    int[] math = new int[100];
                    double[] mean = new double[300];
                    int[] imean = new int[300];

                    string[] str = new string[300];
                    double[] fullMean = new double[300];
                    string[] isbeer = new string[300];
                    double[] beerProduction = new double[300];




                    //finding counts + Total beer production in a year
                    foreach (var row in data)
                    {
                        //Console.WriteLine(row.date.Year + " " +row.grain+ " " + row.beerProduction+ " " + row.factoryManagerName);
                        //Console.WriteLine(row.date.Year);

                        beerProduction[jterator] = (double)row.beerProduction;
                        jterator++;

                        int bvc = 0;
                        if (date == row.date.Year)
                        {
                            beerValue = beerValue + row.beerProduction;
                            bvc++;
                            if (bvc > 11 || bvc == 11)
                            {
                                beerValue = 0;
                            }

                        }
                        else
                        {
                            beerValues.Add((row.date.Year) - 1, beerValue);
                            math[iterator] = count;
                            count = 0;

                            date++;
                            iterator++;
                        }

                        if (bvc > 11 || bvc == 11)
                        {
                            beerValue = 0;
                        }
                        count++;
                    }























                    iterator = 0;
                    //finding Mean
                    foreach (var beer in beerValues)
                    {

                        double value = Math.Round(beer.Value, 2, MidpointRounding.AwayFromZero);
                        mean[iterator] = (int) value  / math[iterator] ;
                        //Console.WriteLine(beer.Value);

                        Console.WriteLine(beer.Key.ToString() + " " + beer.Value.ToString() + " " + math[iterator] + " " + mean[iterator] );
                        iterator++;
                    }
                    //finding if beer production is > mean

                    iterator = 0;
                    jterator = 0;
                    kterator = 0;
                    count = 0;

                    foreach (var prod in beerProduction)
                    {
                        fullMean[kterator] = mean[iterator];
                        if (prod > mean[iterator])
                        {
                            isbeer[jterator] = "yes";
                        }
                        else
                        {
                            isbeer[jterator] = "No";
                        }
                        iterator= count>11?iterator++ :count++;
                        if (count>11)
                        {
                            count=0;
                        }
                       

                        kterator = kterator+1;
                        jterator++;
                    }

               







                    int j = 0;
                    int g = 0;
                    count=0;

                    kterator=0;
                    foreach (var m in fullMean)
                    {
                       
                        string  specifier = "G";
                       CultureInfo culture = CultureInfo.CreateSpecificCulture("eu-ES");
                        str[j] = m.ToString(specifier);
                        
                        j++;
                       
                    }





                  
                    for (int i=0; i<data.Count; i++)
                    {
                       
                        data[i].isBeerProductionGreaterThanYearMean = (string) isbeer[i];

                        data[i].yearMean = str[i];
                        

                        Console.WriteLine(data[i].isBeerProductionGreaterThanYearMean + data[i].yearMean);
                      
                    }


                    //output CSV 
                    using (var writer = new StreamWriter(@"C:\Users\Frostdev\Desktop\output_v1.0.csv"))
                    using (var csv = new CsvWriter(writer, CultureInfo.InvariantCulture))
                    {
                        csv.WriteRecords(data);
                    }







                }
            }






        }
    }


    //class map for maping headers from CSV 
    public class TableDataClassMap : ClassMap<TableData>
    {

        public TableDataClassMap()
        {
            Map(m => m.date).Name("DATE");
            Map(m => m.grain).Name("grain");
            Map(m => m.beerProduction).Name("BeerProduction");
            Map(m => m.factoryManagerName).Name("FactoryManagerName");
        }





    }




    public class TableData
    {
        //[Name("DATE")]
        public DateOnly date { get; set; }

        //[Name("grain")]
        public string? grain { get; set; }

        //[Name("BeerProduction")]
        public double beerProduction { get; set; }

        //[Name("FactoryManagerName")]
        public string? factoryManagerName { get; set; }

        public string yearMean { get; set; }

        public string yearMedian { get; set; }

        public string? isBeerProductionGreaterThanYearMean { get; set; }





    }


}



