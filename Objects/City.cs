using System.Collections.Generic;
using System.Data.SqlClient;
using System;

namespace AirlinePlanner.Objects
{
  public class City
  {
    private int _id;
    private string _name;

    public City(string name, int id = 0)
    {
      _name = name;
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
    public string GetName()
    {
      return _name;
    }
    public void SetName(string newName)
    {
      _name = newName;
    }

    public static List<City> GetAll()
    {
      SqlConnection conn = DB.Connection();
      conn.Open();

      SqlCommand cmd = new SqlCommand("SELECT * FROM cities", conn);

      SqlDataReader rdr = cmd.ExecuteReader();

      List<City> cities = new List<City>{};

      while(rdr.Read())
      {
        int id = rdr.GetInt32(0);
        string name = rdr.GetString(1);
        City newCity = new City(name, id);
        cities.Add(newCity);
      }

      if(rdr != null)
      {
        rdr.Close();
      }
      if(conn != null)
      {
        conn.Close();
      }

      return cities;
    }
  }
}
