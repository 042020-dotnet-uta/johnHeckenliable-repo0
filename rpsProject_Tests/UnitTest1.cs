using Microsoft.EntityFrameworkCore;
using rpsProject;
using System;
using Xunit;

namespace rpsProject_Tests
{
    public class UnitTest1
    {
        [Fact]
        public void Test1()
        {
            var options = new DbContextOptionsBuilder<RPS_DbContextClass>()
                .UseInMemoryDatabase(databaseName: "Test DB")
                .Options;

            using (var context = new RPS_DbContextClass(options)) 
            {
                GamePlay gamePlay = new GamePlay();
            }
        }
    }
}
