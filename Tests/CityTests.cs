using Xunit;
using AirlinePlanner.Objects;
using System;
using System.Data;
using System.Collections.Generic;
using System.Data.SqlClient;

namespace AirlinePlanner
{
  [Collection("AirlinePlanner")]

  public class CityTests : IDisposable
  {
    public CityTests()
    {
    DBConfiguration.ConnectionString = "Data Source=(localdb)\\mssqllocaldb;Initial Catalog=airline_planner_test;Integrated Security=SSPI;";
    }

    [Fact]
    public void City_DatabaseEmptyAtFirst()
    {
      List<City> controlList = new List<City>{};
      List<City> testList = City.GetAll();

      Assert.Equal(controlList, testList);
    }

    [Fact]
    public void City_Equals_TrueForIdenticalObjects()
    {
      City firstCity = new City("Cleveland");
      City secondCity = new City("Cleveland");

      Assert.Equal(firstCity, secondCity);
    }

    [Fact]
    public void City_Save_SavesCityToDatabase()
    {
      City newCity = new City("Chicago");
      newCity.Save();

      City savedCity = City.GetAll()[0];

      Assert.Equal(newCity, savedCity);
    }

    public void Dispose()
    {
      City.DeleteAll();
    }
  }
}
