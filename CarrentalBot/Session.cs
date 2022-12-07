using System;

namespace CarrentalBot
{
    public class Session
    {
        private enum State
        {
            WELCOMING, LOCATION, PICKUPDATE, RETURNDATE, AVAILABLECARS, BOOKING, SUCCESSFUL, NAME, PHONE
        }

        private State nCur = State.WELCOMING;
        private Carrental oCarrental;

        public Session(string sLocation)
        {
            this.oCarrental = new Carrental();
            this.oCarrental.Location = sLocation;
        }

        public List<String> OnMessage(String sInMessage)
        {
            List<String> aMessages = new List<string>();
            switch (this.nCur)
            {
                case State.WELCOMING:
                    aMessages.Add("Welcome to PicknGo Car Rentals!");
                    aMessages.Add("Make a Reservation (1. Yes 2. No) ");
                    this.nCur = State.LOCATION;
                    break;
                case State.LOCATION:
                    this.oCarrental.Location = sInMessage;
                    this.oCarrental.Save();
                    string sLocation = sInMessage;
                    if (sLocation == "1")
                    {
                        aMessages.Add("choose locations (1. London 2. Waterloo 3. Kitchener)");
                        this.nCur = State.PICKUPDATE;
                    }
                    else
                    {
                        aMessages.Add("Thank You! Visit Again");
                        this.nCur = State.WELCOMING;
                    }
                    break;
                case State.PICKUPDATE:
                    string sPickupdate = sInMessage;
                    aMessages.Add("Choose pickup Date and Time");
                    this.nCur = State.RETURNDATE;
                    break;
                case State.RETURNDATE:
                    string sReturndate = sInMessage;
                    aMessages.Add("Choose return Date and Time");
                    this.nCur = State.AVAILABLECARS;
                    break;
                case State.AVAILABLECARS:
                    string sAvailablecars = sInMessage;
                    aMessages.Add("Choose Available Cars (1. BENZ 2. KIA 3. SKODA 4. TOYOTA ");
                    this.nCur = State.BOOKING;
                    break;
                case State.BOOKING:
                    string sBooking = sInMessage;
                    aMessages.Add("Confirm Booking (1. YES 2. No) ");
                    this.nCur = State.NAME;
                    break;
                case State.NAME:
                    string sName = sInMessage;
                    if (sName == "1")
                    {
                        aMessages.Add("FULL NAME ");
                        this.nCur = State.PHONE;
                    }
                    else
                    {
                        aMessages.Add("Thank You! Visit Again");
                        this.nCur = State.WELCOMING;
                    }
                    break;
                case State.PHONE:
                    string sPhone = sInMessage;
                    aMessages.Add("PHONE NUMBER ");
                    this.nCur = State.SUCCESSFUL;
                    break;
                case State.SUCCESSFUL:
                    string sPayment = sInMessage;
                    aMessages.Add("Booking has been sucessful");
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
