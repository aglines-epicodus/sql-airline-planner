using System.Collections.Generic;
using System.Data.SqlClient;
using System;

namespace AirlinePlanner.Objects
{
  public class Flight
  {
    private int _id;
    private DateTime _departureTime;
    private string _status;

    public Flight(DateTime departure, string status, int id = 0)
    {
      _departureTime = departure;
      _status = status;
      _id = id;
    }

    public int GetId()
    {
      return _id;
    }
    public void SetId(int newId)
    {
      _id = newId;
    }
    public string GetStatus()
    {
      return _status;
    }
    public void SetStatus(string newStatus)
    {
      _status = newStatus;
    }
    public DateTime GetDepartureTime()
    {
      return _departureTime;
    }
    public void SetDepartureTime(DateTime newDepartureTime)
    {
      _departureTime = newDepartureTime;
    }

    public override bool Equals(System.Object otherFlight)
    {
      if (!(otherFlight is Flight))
      {
        return false;
      }
      else
      {
        Flight newFlight = (Flight) otherFlight;
        bool statusEquality = this.GetStatus() == newFlight.GetStatus();
        bool idEquality = this.GetId() == newFlight.GetId();
        bool timeEquality = this.GetDepartureTime() == newFlight.GetDepartureTime();
        return (statusEquality && idEquality && timeEquality);
      }
    }

    public static List<Flight> GetAll()
    {
      SqlConnection conn = DB.Connection();
      conn.Open();

      SqlCommand cmd = new SqlCommand("SELECT * FROM flights", conn);

      SqlDataReader rdr = cmd.ExecuteReader();

      List<Flight> flights = new List<Flight>{};

      while(rdr.Read())
      {
        int id = rdr.GetInt32(0);
        DateTime departureTime = rdr.GetDateTime(1);
        string status = rdr.GetString(2);
        Flight newFlight = new Flight(departureTime, status, id);
        flights.Add(newFlight);
      }

      if(rdr != null)
      {
        rdr.Close();
      }
      if(conn != null)
      {
        conn.Close();
      }

      return flights;
    }

    public void Save()
    {
      SqlConnection conn = DB.Connection();
      conn.Open();

      SqlCommand cmd = new SqlCommand("INSERT INTO flights (departure_time, status) OUTPUT INSERTED.id VALUES (@DepartureTime, @FlightStatus);", conn);

      SqlParameter timeParam = new SqlParameter("@DepartureTime", this.GetDepartureTime());
      cmd.Parameters.Add(timeParam);
      SqlParameter statusParam = new SqlParameter("@FlightStatus", this.GetStatus());
      cmd.Parameters.Add(statusParam);

      SqlDataReader rdr = cmd.ExecuteReader();

      while(rdr.Read())
      {
        this._id = rdr.GetInt32(0);
      }

      if(rdr != null)
      {
        rdr.Close();
      }
      if(conn != null)
      {
        conn.Close();
      }
    }

    public static void DeleteAll()
    {
      SqlConnection conn = DB.Connection();
      conn.Open();

      SqlCommand cmd = new SqlCommand("DELETE FROM flights", conn);
      cmd.ExecuteNonQuery();

      if(conn != null)
      {
        conn.Close();
      }
    }

    public static Flight Find(int idToFind)
    {
      SqlConnection conn = DB.Connection();
      conn.Open();

      SqlCommand cmd = new SqlCommand("SELECT * FROM flights WHERE id = @FlightId", conn);
      cmd.Parameters.Add(new SqlParameter("@FlightId", idToFind));

      SqlDataReader rdr = cmd.ExecuteReader();

      int id = 0;
      DateTime departureTime = default(DateTime);
      string status = null;
      while(rdr.Read())
      {
        id = rdr.GetInt32(0);
        departureTime = rdr.GetDateTime(1);
        status = rdr.GetString(2);
      }
      Flight foundFlight = new Flight(departureTime, status, id);

      if(rdr != null)
      {
        rdr.Close();
      }
      if(conn != null)
      {
        conn.Close();
      }
      return foundFlight;
    }


    public void CreateFlightPlan(City departureCity, City arrivalCity)
    {
      SqlConnection conn = DB.Connection();
      conn.Open();

      SqlCommand cmd = new SqlCommand("INSERT INTO cities_flights (city_departure_id, city_arrival_id, flight_id) OUTPUT INSERTED.id VALUES (@CityDepartureId, @CityArrivalId, @FlightId)", conn);

      SqlParameter departureCityParam = new SqlParameter("@CityDepartureId", departureCity.GetId());
      SqlParameter arrivalCityParam = new SqlParameter("@CityArrivalId", arrivalCity.GetId());
      Console.WriteLine(arrivalCity.GetId());
      SqlParameter flightIdParam = new SqlParameter("@FlightId", this.GetId());
      cmd.Parameters.Add(departureCityParam);
      cmd.Parameters.Add(arrivalCityParam);
      cmd.Parameters.Add(flightIdParam);

      SqlDataReader rdr = cmd.ExecuteReader();

      while(rdr.Read())
      {
        int test  = rdr.GetInt32(0);
      }

      if(rdr != null)
      {
        rdr.Close();
      }
      if(conn != null)
      {
        conn.Close();
      }
    }

    public List<Dictionary<string, City>> GetAllFlightPlans()
    {
      SqlConnection conn = DB.Connection();
      conn.Open();

      SqlCommand cmd = new SqlCommand("SELECT * FROM cities_flights WHERE flight_id = @FlightId", conn);
      SqlParameter FlightIdParam = new SqlParameter("@FlightId", this.GetId());
      cmd.Parameters.Add(FlightIdParam);

      SqlDataReader rdr = cmd.ExecuteReader();
      List<Dictionary<string, City>> flightPlans = new List<Dictionary<string, City>>{};
      while(rdr.Read())
      {
        Dictionary <string, City> flightPlan = new Dictionary<string, City>{};
        flightPlan.Add("departure-city", City.Find(rdr.GetInt32(1)));
        flightPlan.Add("arrival-city", City.Find(rdr.GetInt32(2)));
        // flightPlan.Add("flight-id", rdr.GetInt32(3));
        flightPlans.Add(flightPlan);
      }

      if(rdr != null)
      {
        rdr.Close();
      }
      if(conn != null)
      {
        conn.Close();
      }
      return flightPlans;
    }
  }
}
