using FluentMigrator;

namespace MeterReadingRepositoryMigrations.DataMigrations
{
    [Migration(202310010001)]
    public class InitialTables_202310110001 : Migration
    {
        public override void Up()
        {
            Create.Table("Accounts")
                .WithColumn("AccountId").AsInt64().NotNullable().PrimaryKey()
                .WithColumn("FirstName").AsString(60).NotNullable()
                .WithColumn("LastName").AsString(60).NotNullable();

            Create.Table("MeterReadings")
                .WithColumn("Id").AsInt64().NotNullable().PrimaryKey().Identity()
                .WithColumn("AccountId").AsInt64().NotNullable().ForeignKey("Accounts", "AccountId")
                .WithColumn("MeterReadingDateTime").AsDateTime2().NotNullable()
                .WithColumn("MeterReadValue").AsInt64().NotNullable();
        }

        public override void Down()
        {
            Delete.Table("Accounts");
            Delete.Table("MeterReadings");
        }
    }
}
