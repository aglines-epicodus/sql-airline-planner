using Xunit;
using AirlinePlanner.Objects;
using System;
using System.Data;
using System.Collections.Generic;
using System.Data.SqlClient;

namespace AirlinePlanner
{
  [Collection("AirlinePlanner")]

  public class FlightTests : IDisposable
  {
    public FlightTests()
    {
    DBConfiguration.ConnectionString = "Data Source=(localdb)\\mssqllocaldb;Initial Catalog=airline_planner_test;Integrated Security=SSPI;";
    }

    [Fact]
    public void Flight_DatabaseEmptyAtFirst()
    {
      List<Flight> controlList = new List<Flight>{};
      List<Flight> testList = Flight.GetAll();

      Assert.Equal(controlList, testList);
    }

    [Fact]
    public void Flight_Equals_TrueForIdenticalObjects()
    {
      Flight firstFlight = new Flight(new DateTime(2017, 04, 25), "on-time");
      Flight secondFlight = new Flight(new DateTime(2017, 04, 25), "on-time");


      Assert.Equal(firstFlight, secondFlight);
    }

    [Fact]
    public void Flight_Save_SavesFlightToDatabase()
    {
      Flight newFlight = new Flight(new DateTime(2017, 04, 25), "on-time");
      newFlight.Save();

      Flight savedFlight = Flight.GetAll()[0];

      // Console.WriteLine("Created flight. Id: {0}, status: {1}, time: {2}", newFlight.GetId(), newFlight.GetStatus(), newFlight.GetDepartureTime());
      // Console.WriteLine("Saved flight. Id: {0}, status: {1}, time: {2}", savedFlight.GetId(), savedFlight.GetStatus(), savedFlight.GetDepartureTime());

      Assert.Equal(newFlight, savedFlight);
    }

    [Fact]
    public void Flight_Find_FindsFlightInDatabase()
    {
      Flight newFlight = new Flight(new DateTime(2017, 04, 25), "on-time");
      newFlight.Save();

      Flight foundFlight = Flight.Find(newFlight.GetId());

      Assert.Equal(newFlight, foundFlight);
    }

    [Fact]
    public void Flight_CreateFlightPlan_CreateRelationshipInJoinTable()
    {
      Flight newFlight = new Flight(new DateTime(2017, 04, 25), "on-time");
      newFlight.Save();

      City departureCity = new City("Cleveland");
      departureCity.Save();
      City arrivalCity = new City("Portland");
      arrivalCity.Save();

      newFlight.CreateFlightPlan(departureCity, arrivalCity);

      Dictionary<string, City> testFlightPlans = newFlight.GetAllFlightPlans()[0];
      Dictionary<string, City> controlFlightPlans = new Dictionary<string, City>{{"departure-city", departureCity}, {"arrival-city", arrivalCity}};

      Console.WriteLine("Control. depart: {0}, arrival: {1}. Flight1_id: {2}, flight2_id: {3}", departureCity.GetName(), arrivalCity.GetName(), departureCity.GetId(), arrivalCity.GetId());
      Console.WriteLine("Test. depart: {0}, arrival: {1}. Flight1_id: {2}, flight2_id: {3}", testFlightPlans["departure-city"].GetName(), testFlightPlans["arrival-city"].GetName(), testFlightPlans["departure-city"].GetId(), testFlightPlans["arrival-city"].GetId());

      Assert.Equal(controlFlightPlans, testFlightPlans);
    }


    public void Dispose()
    {
      Flight.DeleteAll();
    }
  }
}
