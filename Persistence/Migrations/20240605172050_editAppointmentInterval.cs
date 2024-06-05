using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Persistence.Migrations
{
    /// <inheritdoc />
    public partial class editAppointmentInterval : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Interval",
                table: "AppointmentIntervals");

            migrationBuilder.RenameColumn(
                name: "Day",
                table: "AppointmentIntervals",
                newName: "IntervalDate");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "IntervalDate",
                table: "AppointmentIntervals",
                newName: "Day");

            migrationBuilder.AddColumn<TimeSpan>(
                name: "Interval",
                table: "AppointmentIntervals",
                type: "time",
                nullable: false,
                defaultValue: new TimeSpan(0, 0, 0, 0, 0));
        }
    }
}
