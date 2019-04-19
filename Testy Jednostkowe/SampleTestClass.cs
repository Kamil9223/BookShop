using Ksiegarnia.Models;
using System;
using System.Collections.Generic;
using System.Text;
using Xunit;

namespace TestyJednostkowe
{
    public class SampleTestClass
    {
        [Fact]
        public void SampleTest()
        {
            User user = new User("user", "user2@gmail.com", "pass", "salt");

            Assert.Equal("user", user.Login);
        }
    }
}
