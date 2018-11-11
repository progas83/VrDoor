using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data.Entity;
using DoorDataModel;
using WebDoor.Models;

namespace WebDoor.Logic
{
    public class ReservationManager
    {
        private int defaultCountOfFreeWorkStations = 40;
        internal List<CalendarWeek> CalculateReservations(int year, int month)
        {
            DateLogicHelper dateLogicHelper = new DateLogicHelper();
            List<CalendarWeek> monthReservedInfo = dateLogicHelper.GetWeeksOfMonth(year, month);
            using (var db = new DoorDbContext())
            {
                var reservedDaysOfMonth = db.Reservations.Include(r => r.HelmetWorkStations).Where(r => r.StartReservation.Month == month).OrderBy(r => r.StartReservation.Day).ToList();
                foreach (var week in monthReservedInfo)
                {
                    foreach (var day in week.DaysOfWeek)
                    {
                        if (day != null)
                        {
                            var reservationsInDay = reservedDaysOfMonth.Where(r => r.StartReservation.Day == day.DayOfMonth).ToList();
                            int freeWorkStations = this.defaultCountOfFreeWorkStations;
                            if (reservationsInDay != null)
                            {
                                int countOfReservedWorkStations = reservationsInDay.Select(r => r.HelmetWorkStations).Count();
                                freeWorkStations = freeWorkStations - countOfReservedWorkStations;

                                //foreach (var reservation in reservationsInDay)
                                //{
                                //    reservation.HelmetWorkStations.Count
                                //}

                            }
                            day.AvailableWorkStations = freeWorkStations;
                        }
                    }
                }
            }
            return monthReservedInfo;
        }
    }
}