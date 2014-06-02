using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data;
using System.Data.SqlClient;

/// <summary>
/// EventDAO class is the main class which interacts with the database. SQL Server express edition
/// has been used.
/// the event information is stored in a table named 'event' in the database.
///
/// Here is the table format:
/// event(event_id int, title varchar(100), description varchar(200),event_start datetime, event_end datetime)
/// event_id is the primary key
/// </summary>
public class EventDAO
{
    //change the connection string as per your database connection.
    //private static string connectionString = "Data Source=208.91.198.59;Initial Catalog=TestDB3;User ID=sarang; Password=admin12!@";
    //private static string connectionString = "Data Source=localhost;Initial Catalog=TestDB3;Integrated Security=True";
    private static string connectionString = System.Configuration.ConfigurationManager.ConnectionStrings["ApplicationServices"].ConnectionString;

    //this method retrieves all events within range start-end
    public static List<CalendarEvent> getEvents(DateTime start, DateTime end)
    {

        List<CalendarEvent> events = new List<CalendarEvent>();
        SqlConnection con = new SqlConnection(connectionString);
        SqlCommand cmd = new SqlCommand("SELECT event_id, description, title, event_start, event_end,purpose FROM event where event_start>=@start AND event_end<=@end", con);
        cmd.Parameters.AddWithValue("@start", start);
        cmd.Parameters.AddWithValue("@end", end);

        using (con)
        {
            con.Open();
            SqlDataReader reader = cmd.ExecuteReader();
            while (reader.Read())
            {
                CalendarEvent cevent = new CalendarEvent();
                cevent.id = (int)reader["event_id"];
                cevent.title = (string)reader["title"];
                cevent.description = (string)reader["description"];
                cevent.start = (DateTime)reader["event_start"];
                cevent.end = (DateTime)reader["event_end"];
                cevent.purpose = (string)reader["purpose"];
                events.Add(cevent);
            }

            reader.Close();
             events = new List<CalendarEvent>();
            cmd = new SqlCommand("select ID,CPAID,ISNULL(CustomerID,0)CustomerID,ISNULL(CustomerName,'')CustomerName,ISNULL(PurposeOfVisit,'')PurposeOfVisit,ISNULL(ContactNumber,'')ContactNumber,StartTime,EndTime,ISNULL(Note,'')Note,IsOpen from CPAAppointment where StartTime>=@start AND EndTime<=@end", con);
            cmd.Parameters.AddWithValue("@start", start);
            cmd.Parameters.AddWithValue("@end", end);

            reader = cmd.ExecuteReader();
            while (reader.Read())
            {
                CalendarEvent cevent = new CalendarEvent();
                cevent.id = (int)reader["ID"];
                cevent.title = (string)reader["CustomerName"];
                cevent.description = (string)reader["Note"];
                cevent.start = (DateTime)reader["StartTime"];
                cevent.end = (DateTime)reader["EndTime"];
                cevent.purpose = (string)reader["PurposeOfVisit"];
                cevent.contactNumber = (string)reader["ContactNumber"];
                events.Add(cevent);
            }
        }
        return events;
        //side note: if you want to show events only related to particular users,
        //if user id of that user is stored in session as Session["userid"]
        //the event table also contains a extra field named 'user_id' to mark the event for that particular user
        //then you can modify the SQL as:
        //SELECT event_id, description, title, event_start, event_end FROM event where user_id=@user_id AND event_start>=@start AND event_end<=@end
        //then add paramter as:cmd.Parameters.AddWithValue("@user_id", HttpContext.Current.Session["userid"]);
    }

    //this method updates the event title and description
    public static void updateEvent(int id, String title, String description, string purpose, string contactNumber)
    {
        SqlConnection con = new SqlConnection(connectionString);
        //SqlCommand cmd = new SqlCommand("UPDATE event SET title=@title, description=@description, purpose=@purpose WHERE event_id=@event_id", con);
        //cmd.Parameters.AddWithValue("@title", title);
        //cmd.Parameters.AddWithValue("@purpose", purpose);
        //cmd.Parameters.AddWithValue("@description", description);
        //cmd.Parameters.AddWithValue("@event_id", id);

        SqlCommand cmd = new SqlCommand("UPDATE CPAAppointment SET CustomerName=@customerName, Note=@note, PurposeOfVisit=@purpose, ContactNumber=@contactNumber WHERE ID=@event_id", con);
        cmd.Parameters.AddWithValue("@customerName", title);
        cmd.Parameters.AddWithValue("@purpose", purpose);
        cmd.Parameters.AddWithValue("@note", description);
        cmd.Parameters.AddWithValue("@contactNumber", contactNumber);
        cmd.Parameters.AddWithValue("@event_id", id);
        using (con)
        {
            con.Open();
            cmd.ExecuteNonQuery();
        }


    }

    //this method updates the event start and end time
    public static void updateEventTime(int id, DateTime start, DateTime end)
    {
        SqlConnection con = new SqlConnection(connectionString);
        SqlCommand cmd = new SqlCommand("UPDATE event SET event_start=@event_start, event_end=@event_end WHERE event_id=@event_id", con);
        cmd.Parameters.AddWithValue("@event_start", start);
        cmd.Parameters.AddWithValue("@event_end", end);
        cmd.Parameters.AddWithValue("@event_id", id);
        using (con)
        {
            con.Open();
            cmd.ExecuteNonQuery();
        }
    }

    //this mehtod deletes event with the id passed in.
    public static void deleteEvent(int id)
    {
        SqlConnection con = new SqlConnection(connectionString);
        //SqlCommand cmd = new SqlCommand("DELETE FROM event WHERE (event_id = @event_id)", con);
        SqlCommand cmd = new SqlCommand("DELETE FROM CPAAppointment WHERE (ID = @event_id)", con);
        cmd.Parameters.AddWithValue("@event_id", id);
        using (con)
        {
            con.Open();
            cmd.ExecuteNonQuery();
        }
    }

    //this method adds events to the database
    public static int addEvent(CalendarEvent cevent)
    {
        //add event to the database and return the primary key of the added event row

        //insert
        SqlConnection con = new SqlConnection(connectionString);
        SqlCommand cmd = new SqlCommand("INSERT INTO event(title, description, event_start, event_end,purpose) VALUES(@title, @description, @event_start, @event_end,@purpose)", con);
        cmd.Parameters.AddWithValue("@title", cevent.title);
        cmd.Parameters.AddWithValue("@purpose", cevent.purpose);
        cmd.Parameters.AddWithValue("@description", cevent.description);
        cmd.Parameters.AddWithValue("@event_start", cevent.start);
        cmd.Parameters.AddWithValue("@event_end", cevent.end);

        int key = 0;
        using (con)
        {
            con.Open();
            cmd.ExecuteNonQuery();

            //get primary key of inserted row
            cmd = new SqlCommand("SELECT max(event_id) FROM event where title=@title AND description=@description AND event_start=@event_start AND event_end=@event_end", con);
            cmd.Parameters.AddWithValue("@title", cevent.title);
            cmd.Parameters.AddWithValue("@description", cevent.description);
            cmd.Parameters.AddWithValue("@event_start", cevent.start);
            cmd.Parameters.AddWithValue("@event_end", cevent.end);

            key = (int)cmd.ExecuteScalar();

            cmd = new SqlCommand();
            cmd.Connection = con;
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.CommandText = "[OpenAppointments]";
            cmd.Parameters.AddWithValue("@start", cevent.start);
            cmd.Parameters.AddWithValue("@end", cevent.end);
            cmd.Parameters.AddWithValue("@interval", cevent.interval);
            cmd.Parameters.AddWithValue("@CPAID", cevent.CPAID);
            cmd.Parameters.AddWithValue("@customerName", cevent.title);
            cmd.Parameters.AddWithValue("@purposeOfVisit", cevent.purpose);
            cmd.Parameters.AddWithValue("@contactNumber", cevent.contactNumber);
            cmd.Parameters.AddWithValue("@note", cevent.description);
            cmd.Parameters.AddWithValue("@isOpen", string.IsNullOrEmpty(cevent.title));
            cmd.ExecuteNonQuery();

        }

        return key;

    }



}
