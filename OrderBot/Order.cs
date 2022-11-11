using Microsoft.Data.Sqlite;

namespace CarrentalBot
{
    public class Carrental : ISQLModel
    {
        private string _location = String.Empty;
        private string _pickupdate = String.Empty;
        private string _returndate = String.Empty;
        private string _availablecars = String.Empty;
        private string _booking = String.Empty;
        private string _payment = String.Empty;

        public string Location{
            get => _location;
            set => _location = value;
        }

        public string Pickupdate{
            get => _pickupdate;
            set => _pickupdate = value;
        }

        public string Returndate{
            get => _returndate;
            set => _returndate = value;
        }
        public string Availablecars{
            get => _availablecars;
            set => _availablecars = value;
        }

        public string Booking{
            get => _booking;
            set => _booking = value;
        }
        public string Payment{
            get => _payment;
            set => _payment = value;
        }

        public void Save(){
           using (var connection = new SqliteConnection(DB.GetConnectionString()))
            {
                connection.Open();

                var commandUpdate = connection.CreateCommand();
                commandUpdate.CommandText =
                @"
        UPDATE 
        SET size = $size
        WHERE phone = $phone
    ";
                commandUpdate.Parameters.AddWithValue("$size", Size);
                commandUpdate.Parameters.AddWithValue("$phone", Phone);
                int nRows = commandUpdate.ExecuteNonQuery();
                if(nRows == 0){
                    var commandInsert = connection.CreateCommand();
                    commandInsert.CommandText =
                    @"
            INSERT INTO orders(size, phone)
            VALUES($size, $phone)
        ";
                    commandInsert.Parameters.AddWithValue("$size", Size);
                    commandInsert.Parameters.AddWithValue("$phone", Phone);
                    int nRowsInserted = commandInsert.ExecuteNonQuery();

                }
            }

        }
    }
}
