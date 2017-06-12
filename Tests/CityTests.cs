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

    public void Dispose()
    {

    }
  }
}
