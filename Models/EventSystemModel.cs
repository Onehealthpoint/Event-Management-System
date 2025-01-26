using System.ComponentModel.DataAnnotations;
using System.Data;
using MySql.Data.MySqlClient;

namespace Event_Management_System.Models
{
    public class EventSystemModel
    {
        [MaxLength(20)] public required string EventId { get; set; }
        [MaxLength(30)] public string? EventName { get; set; }
        [MaxLength(50)] public string? EventDescription { get; set; }
        public DateTime? EventDateTime { get; set; }
        [MaxLength(20)] public string? EventVenue { get; set; }

        private const string connectionString = "SERVER=sql12.freesqldatabase.com;DATABASE=sql12759678;UID=sql12759678;PASSWORD=UzELmxgsAG;PORT=3306;";
        private const string connectionString = "SERVER=localhost;DATABASE=mydb;UID=root;PASSWORD=;";

        public void Read(string EventId)
        {
            using (MySqlConnection conn = new MySqlConnection(connectionString))
            {
                string query = "SELECT * FROM Events WHERE EventId = @EventId";

                conn.Open();
                using var command = new MySqlCommand(query, conn);
                command.Parameters.AddWithValue("@EventId", EventId);

                using var reader = command.ExecuteReader();
                if (reader.Read())
                {
                    this.EventId = reader.GetString("EventId");
                    this.EventName = reader.GetString("EventName");
                    this.EventDescription = reader.IsDBNull(reader.GetOrdinal("EventDescription")) ? null : reader.GetString("EventDescription");
                    this.EventDateTime = reader.GetDateTime("EventDateTime");
                    this.EventVenue = reader.GetString("EventVenue");
                }
            }
        }

        public int Create()
        {
            using (MySqlConnection conn = new MySqlConnection(connectionString))
            {
                string query = @"INSERT INTO Events (EventId, EventName, EventDescription, EventDateTime, EventVenue) 
                           VALUES (@EventId, @EventName, @EventDescription, @EventDateTime, @EventVenue)";

                conn.Open();
                using var command = new MySqlCommand(query, conn);
                command.Parameters.AddWithValue("@EventId", EventId);
                command.Parameters.AddWithValue("@EventName", EventName);
                command.Parameters.AddWithValue("@EventDescription", EventDescription ?? (object)DBNull.Value);
                command.Parameters.AddWithValue("@EventDateTime", EventDateTime);
                command.Parameters.AddWithValue("@EventVenue", EventVenue);

                return command.ExecuteNonQuery();
            }
        }

        public int Update()
        {
            using (MySqlConnection conn = new MySqlConnection(connectionString))
            {
                string query = @"UPDATE Events 
                           SET EventName = @EventName, 
                               EventDescription = @EventDescription, 
                               EventDateTime = @EventDateTime, 
                               EventVenue = @EventVenue 
                           WHERE EventId = @EventId";

                conn.Open();
                using var command = new MySqlCommand(query, conn);
                command.Parameters.AddWithValue("@EventId", EventId);
                command.Parameters.AddWithValue("@EventName", EventName);
                command.Parameters.AddWithValue("@EventDescription", EventDescription ?? (object)DBNull.Value);
                command.Parameters.AddWithValue("@EventDateTime", EventDateTime);
                command.Parameters.AddWithValue("@EventVenue", EventVenue);

                return command.ExecuteNonQuery();
            }
        }

        public int Delete()
        {
            using (MySqlConnection conn = new MySqlConnection(connectionString))
            {
                string query = "DELETE FROM Events WHERE EventId = @EventId";

                conn.Open();
                using var command = new MySqlCommand(query, conn);
                command.Parameters.AddWithValue("@EventId", EventId);

                return command.ExecuteNonQuery();
            }
        }

        public static List<EventSystemModel> GetAll()
        {
            var events = new List<EventSystemModel>();

            using (MySqlConnection conn = new MySqlConnection(connectionString))
            {
                string query = "SELECT * FROM Events";

                conn.Open();
                using var command = new MySqlCommand(query, conn);
                using var reader = command.ExecuteReader();

                while (reader.Read())
                {
                    events.Add(new EventSystemModel
                    {
                        EventId = reader.GetString("EventId"),
                        EventName = reader.GetString("EventName"),
                        EventDescription = reader.IsDBNull(reader.GetOrdinal("EventDescription")) ? null : reader.GetString("EventDescription"),
                        EventDateTime = reader.GetDateTime("EventDateTime"),
                        EventVenue = reader.GetString("EventVenue")
                    });
                }
            }

            return events;
        }
    }

    public class OrganizationMembers
    {
        [MaxLength(20)] public required string MemberId { get; set; }
        [MaxLength(20)] public string? MemberName { get; set; }
        public DateTime? MemberDateOfBirth { get; set; }
        [MaxLength(50)][EmailAddress] public string? MemberEmail { get; set; }
        [MaxLength(10)] public string? MemberPhone { get; set; }

        private const string connectionString = "SERVER=sql12.freesqldatabase.com;DATABASE=sql12759678;UID=sql12759678;PASSWORD=UzELmxgsAG;PORT=3306;";
        private const string connectionString = "SERVER=localhost;DATABASE=mydb;UID=root;PASSWORD=;";

        public void Read(string MemberId)
        {
            using (MySqlConnection conn = new MySqlConnection(connectionString))
            {
                string query = "SELECT * FROM Members WHERE MemberId = @MemberId";

                conn.Open();
                using var command = new MySqlCommand(query, conn);
                command.Parameters.AddWithValue("@MemberId", MemberId);

                using var reader = command.ExecuteReader();
                if (reader.Read())
                {
                    this.MemberId = reader.GetString("MemberId");
                    this.MemberName = reader.GetString("MemberName");
                    this.MemberDateOfBirth = reader.IsDBNull(reader.GetOrdinal("MemberDateOfBirth")) ? null : reader.GetDateTime("MemberDateOfBirth");
                    this.MemberEmail = reader.GetString("MemberEmail");
                    this.MemberPhone = reader.IsDBNull(reader.GetOrdinal("MemberPhone")) ? null : reader.GetString("MemberPhone");
                }
            }
        }

        public int Create()
        {
            using (MySqlConnection conn = new MySqlConnection(connectionString))
            {
                string query = @"INSERT INTO Members (MemberId, MemberName, MemberDateOfBirth, MemberEmail, MemberPhone) 
                           VALUES (@MemberId, @MemberName, @MemberDateOfBirth, @MemberEmail, @MemberPhone)";

                conn.Open();
                using var command = new MySqlCommand(query, conn);
                command.Parameters.AddWithValue("@MemberId", MemberId);
                command.Parameters.AddWithValue("@MemberName", MemberName);
                command.Parameters.AddWithValue("@MemberDateOfBirth", MemberDateOfBirth ?? (object)DBNull.Value);
                command.Parameters.AddWithValue("@MemberEmail", MemberEmail);
                command.Parameters.AddWithValue("@MemberPhone", MemberPhone ?? (object)DBNull.Value);

                return command.ExecuteNonQuery();
            }
        }

        public int Update()
        {
            using (MySqlConnection conn = new MySqlConnection(connectionString))
            {
                string query = @"UPDATE Members 
                           SET MemberName = @MemberName, 
                               MemberDateOfBirth = @MemberDateOfBirth, 
                               MemberEmail = @MemberEmail, 
                               MemberPhone = @MemberPhone 
                           WHERE MemberId = @MemberId";

                conn.Open();
                using var command = new MySqlCommand(query, conn);
                command.Parameters.AddWithValue("@MemberId", MemberId);
                command.Parameters.AddWithValue("@MemberName", MemberName);
                command.Parameters.AddWithValue("@MemberDateOfBirth", MemberDateOfBirth ?? (object)DBNull.Value);
                command.Parameters.AddWithValue("@MemberEmail", MemberEmail);
                command.Parameters.AddWithValue("@MemberPhone", MemberPhone ?? (object)DBNull.Value);

                return command.ExecuteNonQuery();
            }
        }

        public int Delete()
        {
            using (MySqlConnection conn = new MySqlConnection(connectionString))
            {
                string query = "DELETE FROM Members WHERE MemberId = @MemberId";

                conn.Open();
                using var command = new MySqlCommand(query, conn);
                command.Parameters.AddWithValue("@MemberId", MemberId);

                return command.ExecuteNonQuery();
            }
        }

        public static List<OrganizationMembers> GetAll()
        {
            var members = new List<OrganizationMembers>();

            using (MySqlConnection conn = new MySqlConnection(connectionString))
            {
                string query = "SELECT * FROM Members";

                conn.Open();
                using var command = new MySqlCommand(query, conn);
                using var reader = command.ExecuteReader();

                while (reader.Read())
                {
                    members.Add(new OrganizationMembers
                    {
                        MemberId = reader.GetString("MemberId"),
                        MemberName = reader.GetString("MemberName"),
                        MemberDateOfBirth = reader.IsDBNull(reader.GetOrdinal("MemberDateOfBirth")) ? null : reader.GetDateTime("MemberDateOfBirth"),
                        MemberEmail = reader.GetString("MemberEmail"),
                        MemberPhone = reader.IsDBNull(reader.GetOrdinal("MemberPhone")) ? null : reader.GetString("MemberPhone")
                    });
                }
            }

            return members;
        }
    }
     
    public class EventAttendees
    {
        public required string EventId { get; set; }
        public required string MemberId { get; set; }

        private const string connectionString = "SERVER=sql12.freesqldatabase.com;DATABASE=sql12759678;UID=sql12759678;PASSWORD=UzELmxgsAG;PORT=3306;"

        public void Read(string EventId, string MemberId)
        {
            using (MySqlConnection conn = new MySqlConnection(connectionString))
            {
                string query = "SELECT * FROM EventAttendees WHERE EventId = @EventId AND MemberId = @MemberId";

                conn.Open();
                using var command = new MySqlCommand(query, conn);
                command.Parameters.AddWithValue("@EventId", EventId);
                command.Parameters.AddWithValue("@MemberId", MemberId);

                using var reader = command.ExecuteReader();
                if (reader.Read())
                {
                    this.EventId = reader.GetString("EventId");
                    this.MemberId = reader.GetString("MemberId");
                }
            }
        }

        public int Create()
        {
            using (MySqlConnection conn = new MySqlConnection(connectionString))
            {
                string query = "INSERT INTO EventAttendees (EventId, MemberId) VALUES (@EventId, @MemberId)";

                conn.Open();
                using var command = new MySqlCommand(query, conn);
                command.Parameters.AddWithValue("@EventId", EventId);
                command.Parameters.AddWithValue("@MemberId", MemberId);

                return command.ExecuteNonQuery();
            }
        }

        public int Delete()
        {
            using (MySqlConnection conn = new MySqlConnection(connectionString))
            {
                string query = "DELETE FROM EventAttendees WHERE EventId = @EventId AND MemberId = @MemberId";

                conn.Open();
                using var command = new MySqlCommand(query, conn);
                command.Parameters.AddWithValue("@EventId", EventId);
                command.Parameters.AddWithValue("@MemberId", MemberId);

                return command.ExecuteNonQuery();
            }
        }

        public static List<EventAttendees> GetAllByEvent(string EventId)
        {
            var attendees = new List<EventAttendees>();

            using (MySqlConnection conn = new MySqlConnection(connectionString))
            {
                string query = "SELECT * FROM EventAttendees WHERE EventId = @EventId";

                conn.Open();
                using var command = new MySqlCommand(query, conn);
                command.Parameters.AddWithValue("@EventId", EventId);

                using var reader = command.ExecuteReader();
                while (reader.Read())
                {
                    attendees.Add(new EventAttendees
                    {
                        EventId = reader.GetString("EventId"),
                        MemberId = reader.GetString("MemberId")
                    });
                }
            }

            return attendees;
        }

        public static List<EventAttendees> GetAllByMember(string MemberId)
        {
            var attendees = new List<EventAttendees>();

            using (MySqlConnection conn = new MySqlConnection(connectionString))
            {
                string query = "SELECT * FROM EventAttendees WHERE MemberId = @MemberId";

                conn.Open();
                using var command = new MySqlCommand(query, conn);
                command.Parameters.AddWithValue("@MemberId", MemberId);

                using var reader = command.ExecuteReader();
                while (reader.Read())
                {
                    attendees.Add(new EventAttendees()
                    {
                        EventId = reader.GetString("EventId"),
                        MemberId = reader.GetString("MemberId")
                    });
                }
            }

            return attendees;
        }
    }
}