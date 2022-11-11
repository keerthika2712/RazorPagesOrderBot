using System;

namespace OrderBot
{
    public class Session
    {
        private enum State
        {
            WELCOMING, LOCATION, PICKUPDATE, RETURNDATE, AVAILABLECARS, BOOKING , PAYMENT
        }

        private State nCur = State.WELCOMING;
        private Order oOrder;

        public Session(string sPhone)
        {
            this.oOrder = new Order();
            this.oOrder.Phone = sPhone;
        }

        public List<String> OnMessage(String sInMessage)
        {
            List<String> aMessages = new List<string>();
            switch (this.nCur)
            {
                case State.WELCOMING:
                    aMessages.Add("Welcome to PicknGo Car Rentals!");
                    aMessages.Add("Make a Reservation");
                    this.nCur = State.LOCATION;
                    break;
                case State.LOCATION:
                    this.oOrder.Size = sInMessage;
                    this.oOrder.Save();
                    aMessages.Add("choose locations (1. London 2. Waterloo 3. Kitchener)");
                    this.nCur = State.PICKUPDATE;
                    break;
                case State.PICKUPDATE:
                    string sPickupdate = sInMessage;
                    aMessages.Add("Choose pickup Date and Time (1. Nov 5)");
                    this.nCur = State.RETURNDATE;
                    break;
                case State.RETURNDATE:
                    string sReturndate = sInMessage;
                    aMessages.Add("Choose return Date and Time" );
                    this.nCur = State.AVAILABLECARS;
                    break;
                case State.AVAILABLECARS:
                    string sAvailablecars = sInMessage;
                    aMessages.Add("Choose Available Cars (1. BENZ 2. KIA 3. SKODA 4. TOYOTA ");
                    this.nCur = State.BOOKING;
                    break;
                case State.BOOKING:
                    string sBooking = sInMessage;
                    aMessages.Add("Confirm Booking ");
                    this.nCur = State.PAYMENT;
                    break;
                case State.PAYMENT:
                    string sPayment = sInMessage;
                    aMessages.Add("Make Payment ");
                    break;
            }
            aMessages.ForEach(delegate (String sMessage)
            {
                System.Diagnostics.Debug.WriteLine(sMessage);
            });
            return aMessages;
        }

    }
}
