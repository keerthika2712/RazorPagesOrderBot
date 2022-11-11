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
        private string _name = String.Empty;
        private string _phonenumber = String.Empty;
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

        public string Name{
            get => _name;
            set => _name = value;
        }

        public string Phonenumber{
            get => _phonenumber;
            set => _phonenumber = value;
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
        SET location = $location, pickupdate = $pickupdate, returndate = $returndate, availablecars = $availablecars, booking = $booking, payment = $payment
        WHERE name = $name, phone = $phone
    ";
                commandUpdate.Parameters.AddWithValue("$location", Location);
                commandUpdate.Parameters.AddWithValue("$pickupdate", Pickupdate);
                commandUpdate.Parameters.AddWithValue("$returndate", Returndate);
                commandUpdate.Parameters.AddWithValue("$availablecars", Availablecars);
                commandUpdate.Parameters.AddWithValue("$booking", Booking);
                commandUpdate.Parameters.AddWithValue("$payment", Payment);
                commandUpdate.Parameters.AddWithValue("$name", Name);
                commandUpdate.Parameters.AddWithValue("$phonenumber", Phonenumber);
                int nRows = commandUpdate.ExecuteNonQuery();
                if(nRows == 0){
                    var commandInsert = connection.CreateCommand();
                    commandInsert.CommandText =
                    @"
            INSERT INTO orders(location, pickupdate, returndate, availablecars, booking, payment, name, phonenumber)
            VALUES($location, $pickupdate, $returndate, $availablecars, $bboking, $payment, $name, $phonenumber)
        ";
                    commandInsert.Parameters.AddWithValue("$location", Location);
                    commandInsert.Parameters.AddWithValue("$pickupdate", Pickupdate);
                    commandInsert.Parameters.AddWithValue("$returndate", Returndate);
                    commandInsert.Parameters.AddWithValue("$availablecars", Availablecars);
                    commandInsert.Parameters.AddWithValue("$booking", Booking);
                    commandInsert.Parameters.AddWithValue("$payment", Payment);
                    commandInsert.Parameters.AddWithValue("$name", Name);
                    commandInsert.Parameters.AddWithValue("$phonenumber", Phonenumber);
                    int nRowsInserted = commandInsert.ExecuteNonQuery();

                }
            }

        }
    }
}
