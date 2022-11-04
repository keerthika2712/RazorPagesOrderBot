using System;

namespace OrderBot
{
    public class Session
    {
        private enum State
        {
            WELCOMING, LOCATION, PICKUPDATE
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
                    aMessages.Add("choose locations (1. London 2. Waterloo 3. Kitchener)" + this.oOrder.Size + " London?");
                    this.nCur = State.PICKUPDATE;
                    break;
                case State.PICKUPDATE:
                    string sProtein = sInMessage;
                    aMessages.Add("Choose pickup Date and Time" + this.oOrder.Size + " " + sProtein + " NOV 5th, 8:30 PM ?");
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
