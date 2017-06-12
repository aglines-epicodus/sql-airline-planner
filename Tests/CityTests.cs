using Xunit;
using AirlinePlanner.Objects;
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

    public void Dispose()
    {

    }
  }
}
