using System;
using System.IO;
using Xunit;
using CarrentalBot;
using Microsoft.Data.Sqlite;

namespace CarrentalBot.tests
{
    public class CarrentalBotTest
    {
        public CarrentalBotTest()
        {
            using (var connection = new SqliteConnection(DB.GetConnectionString()))
            {
                connection.Open();

                var commandUpdate = connection.CreateCommand();
                commandUpdate.CommandText =
                @"DELETE FROM ORDERS";
                commandUpdate.ExecuteNonQuery();
            }
        }
        [Fact]
        public void Test1()
        {

        }
        [Fact]
        public void TestWelcome()
        {
            Session oSession = new Session("12345");
            List<String> sInput = oSession.OnMessage("hello");
            Assert.True(sInput[0].Contains("Welcome"));
            Assert.True(sInput[1].Contains("Reservation"));
        }
        [Fact]
        public void TestLocation()
        {
            Session oSession = new Session("12345");
            oSession.OnMessage("hello");
            List<String> sInput = oSession.OnMessage("reservation");
            Assert.True(sInput[0].ToLower().Contains("choose locations"));
        }
        [Fact]
        public void TestPickupdate()
        {
            Session oSession = new Session("12345");
            oSession.OnMessage("hello");
            oSession.OnMessage("reservation");
            List<String> sInput = oSession.OnMessage("london");
            Assert.True(sInput[0].ToLower().Contains("pickup"));
        }
        [Fact]
        public void TestReturndate()
        {
            Session oSession = new Session("12345");
            oSession.OnMessage("hello");
            oSession.OnMessage("reservation");
            oSession.OnMessage("london");
            List<String> sInput = oSession.OnMessage(" 5 NOV ");
            Assert.True(sInput[0].ToLower().Contains("return"));
        }
        [Fact]
        public void TestAvailablecars()
        {
            Session oSession = new Session("12345");
            oSession.OnMessage("hello");
            oSession.OnMessage("reservation");
            oSession.OnMessage("london");
            oSession.OnMessage(" 5 NOV ");
            List<String> sInput = oSession.OnMessage(" 6 NOV ");
            Assert.True(sInput[0].ToLower().Contains("available"));
        }
        [Fact]
        public void TestBooking()
        {
            Session oSession = new Session("12345");
            oSession.OnMessage("hello");
            oSession.OnMessage("reservation");
            oSession.OnMessage("london");
            oSession.OnMessage(" 5 NOV ");
            oSession.OnMessage(" 6 NOV ");
            oSession.OnMessage(" BENZ ");
            List<String> sInput = oSession.OnMessage(" Yes ");
            Assert.True(sInput[0].ToLower().Contains("full name"));
        }
        [Fact]
        public void TestName()
        {
            Session oSession = new Session("12345");
            oSession.OnMessage("hello");
            oSession.OnMessage("reservation");
            oSession.OnMessage("london");
            oSession.OnMessage(" 5 NOV ");
            oSession.OnMessage(" 6 NOV ");
            oSession.OnMessage(" BENZ ");
            oSession.OnMessage(" Yes ");
            List<String> sInput = oSession.OnMessage(" TIM ");
            Assert.True(sInput[0].ToLower().Contains("phone"));
        }
        [Fact]
        public void TestPhone()
        {
            Session oSession = new Session("12345");
            oSession.OnMessage("hello");
            oSession.OnMessage("reservation");
            oSession.OnMessage("london");
            oSession.OnMessage(" 5 NOV ");
            oSession.OnMessage(" 6 NOV ");
            oSession.OnMessage(" BENZ ");
            oSession.OnMessage(" Yes ");
            oSession.OnMessage(" TIM ");
            List<String> sInput = oSession.OnMessage("9395159102");
            Assert.True(sInput[0].ToLower().Contains("successful"));
        }

    }
}
