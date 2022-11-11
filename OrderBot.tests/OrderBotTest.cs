using System;
using System.IO;
using Xunit;
using CarrentalBot;

namespace CarrentalBot.tests
{
    public class CarrentalBotTest
    {
        [Fact]
        public void Test1()
        {

        }
        [Fact]
        public void TestWelcome()
        {
            Session oSession = new Session("12345");
            String sInput = oSession.OnMessage("hello")[0];
            Assert.True(sInput.Contains("Welcome"));
        }
        [Fact]
        public void TestShawarama()
        {
            Session oSession = new Session("12345");
            String sInput = oSession.OnMessage("hello")[0];
            Assert.True(sInput.ToLower().Contains("shawarama"));
        }
        [Fact]
        public void TestSize()
        {
            Session oSession = new Session("12345");
            String sInput = oSession.OnMessage("hello")[1];
            Assert.True(sInput.ToLower().Contains("size"));
        }
        [Fact]
        public void TestLarge()
        {
            Session oSession = new Session("12345");
            oSession.OnMessage("hello");
            String sInput = oSession.OnMessage("large")[0];
            Assert.True(sInput.ToLower().Contains("protein"));
            Assert.True(sInput.ToLower().Contains("large"));
        }
        [Fact]
        public void TestChicken()
        {
            string sPath = DB.GetConnectionString();
            Session oSession = new Session("12345");
            oSession.OnMessage("hello");
            oSession.OnMessage("large");
            String sInput = oSession.OnMessage("chicken")[0];
            Assert.True(sInput.ToLower().Contains("toppings"));
            Assert.True(sInput.ToLower().Contains("large"));
            Assert.True(sInput.ToLower().Contains("chicken"));
        }
    }
}
