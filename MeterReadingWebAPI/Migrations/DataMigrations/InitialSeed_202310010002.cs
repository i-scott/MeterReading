using System;
using FluentMigrator;
using FluentMigrator.SqlServer;
using MeterReadingModel;

namespace MeterReadingWebAPI.Migrations.DataMigrations
{
    [Migration(202310010002)]
    public class InitialSeed_202310010002 : Migration
    {
        public override void Up()
        {
            Insert.IntoTable("Accounts")
                .Row(new Account { AccountId = 2344, FirstName = "Tommy", LastName = "Test" })
                .Row(new Account { AccountId = 2233, FirstName = "Barry", LastName = "Test" })
                .Row(new Account { AccountId = 8766, FirstName = "Sally", LastName = "Test" })
                .Row(new Account { AccountId = 2345, FirstName = "Jerry", LastName = "Test" })
                .Row(new Account { AccountId = 2346, FirstName = "Ollie", LastName = "Test" })
                .Row(new Account { AccountId = 2347, FirstName = "Tara", LastName = "Test" })
                .Row(new Account { AccountId = 2348, FirstName = "Tammy", LastName = "Test" })
                .Row(new Account { AccountId = 2349, FirstName = "Simon", LastName = "Test" })
                .Row(new Account { AccountId = 2350, FirstName = "Colin", LastName = "Test" })
                .Row(new Account { AccountId = 2351, FirstName = "Gladys", LastName = "Test" })
                .Row(new Account { AccountId = 2352, FirstName = "Greg", LastName = "Test" })
                .Row(new Account { AccountId = 2353, FirstName = "Tony", LastName = "Test" })
                .Row(new Account { AccountId = 2355, FirstName = "Arthur", LastName = "Test" })
                .Row(new Account { AccountId = 2356, FirstName = "Craig", LastName = "Test" })
                .Row(new Account { AccountId = 6776, FirstName = "Laura", LastName = "Test" })
                .Row(new Account { AccountId = 4534, FirstName = "JOSH", LastName = "TEST" })
                .Row(new Account { AccountId = 1234, FirstName = "Freya", LastName = "Test" })
                .Row(new Account { AccountId = 1239, FirstName = "Noddy", LastName = "Test" })
                .Row(new Account { AccountId = 1240, FirstName = "Archie", LastName = "Test" })
                .Row(new Account { AccountId = 1241, FirstName = "Lara", LastName = "Test" })
                .Row(new Account { AccountId = 1242, FirstName = "Tim", LastName = "Test" })
                .Row(new Account { AccountId = 1243, FirstName = "Graham", LastName = "Test" })
                .Row(new Account { AccountId = 1244, FirstName = "Tony", LastName = "Test" })
                .Row(new Account { AccountId = 1245, FirstName = "Neville", LastName = "Test" })
                .Row(new Account { AccountId = 1246, FirstName = "Jo", LastName = "Test" })
                .Row(new Account { AccountId = 1247, FirstName = "Jim", LastName = "Test" })
                .Row(new Account { AccountId = 1248, FirstName = "Pam", LastName = "Test" });
        }

        public override void Down()
        {
            Delete.FromTable("Accounts").AllRows();
        }
    }
}
