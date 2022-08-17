﻿using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace VShop.ProductApi.Migrations
{
    public partial class SeedProducts : Migration
    {
        protected override void Up(MigrationBuilder mb)
        {
            mb.Sql(
                "INSERT INTO Products(Name,Price,Description, Stock, ImageURL, CategoryId) "
                    + "Values('Caderno', 7.55,'Caderno',10,'caderno1.jpg',1)"
            );
            mb.Sql(
                "INSERT INTO Products(Name,Price,Description, Stock, ImageURL, CategoryId) "
                    + "Values('Lapis', 3.45,'Lapis',20,'Lapis1.jpg',1)"
            );
            mb.Sql(
                "INSERT INTO Products(Name,Price,Description, Stock, ImageURL, CategoryId) "
                    + "Values('Clips', 5.33,'Clips',50,'Clips1.jpg',1)"
            );
        }

        protected override void Down(MigrationBuilder mb)
        {
            mb.Sql("DELETE FROM Products");
        }
    }
}
