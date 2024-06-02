using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Persistence.Migrations
{
    /// <inheritdoc />
    public partial class mig2 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "IntervalEnd",
                table: "AppointmentIntervals");

            migrationBuilder.RenameColumn(
                name: "IntervalStart",
                table: "AppointmentIntervals",
                newName: "Interval");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "Interval",
                table: "AppointmentIntervals",
                newName: "IntervalStart");

            migrationBuilder.AddColumn<TimeSpan>(
                name: "IntervalEnd",
                table: "AppointmentIntervals",
                type: "time",
                nullable: false,
                defaultValue: new TimeSpan(0, 0, 0, 0, 0));
        }
    }
}
