using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data.Entity;
using DoorDataModel;
using WebDoor.Models;
using Door.StaticData;

namespace WebDoor.Logic
{
    public class ReservationManager
    {
        private int defaultCountOfFreeWorkStations = 40;
        private  List<CalendarWeek> CalculateReservations(int year, int month)
        {
            DateLogicHelper dateLogicHelper = new DateLogicHelper();
            List<CalendarWeek> monthReservedInfo = dateLogicHelper.GetWeeksOfMonth(year, month);
            using (var db = new DoorDbContext())
            {
                var reservedDaysOfMonth = db.Reservations.Include(r => r.HourReservations).Where(r => r.DateReservation.HasValue && r.DateReservation.Value.Month == month).OrderBy(r => r.DateReservation.Value.Day).ToList();
                foreach (var week in monthReservedInfo)
                {
                    foreach (var day in week.DaysOfWeek)
                    {
                        if (day != null)
                        {
                            var reservationsInDay = reservedDaysOfMonth.FirstOrDefault(r =>r.DateReservation.HasValue &&  r.DateReservation.Value.Day == day.DayOfMonth);
                            int freeWorkStations = this.defaultCountOfFreeWorkStations;
                            if (reservationsInDay != null)
                            {
                                int countOfReservedWorkStations = 45;
                                freeWorkStations = freeWorkStations - countOfReservedWorkStations;
                            }
                            day.AvailableWorkStations = freeWorkStations;
                        }
                    }
                }
            }
            return monthReservedInfo;
        }


        private List<TimeReservation> GetDayReservations(int year, int month, int day)
        {
            DateTime dt = new DateTime(year, month, day);


           
            List<TimeReservation> reservationsOnCurrentDay = new List<TimeReservation>();
            using (var db = new DoorDbContext())
            {
               

                var dayReservation = db.Reservations.Include(r => r.HourReservations).FirstOrDefault(r => r.DateReservation.HasValue && r.DateReservation.Value.Year == year && r.DateReservation.Value.Month == month && r.DateReservation.Value.Day == day);
                if(dayReservation==null)
                {
                    dayReservation = AddDayReservation(db, dt);
                    db.SaveChanges();
                    
                }

                int activeHelmetsCount = db.HelmetWorkStations.Where(h => h.IsActive).ToList().Count;
                foreach (var hourReservation in dayReservation.HourReservations)
                {
                    var reservationData = new TimeReservation(hourReservation, activeHelmetsCount);
                    reservationsOnCurrentDay.Add(reservationData);
                }

            }
                return reservationsOnCurrentDay;
        }


        private Reservation CalculateDayReservation(WorkingPlan dayWorkingPlan, DateTime date)
        {
            Reservation reservation = new Reservation();
            reservation.DateReservation = date;// new DateTime(year, month, day);
            reservation.HourReservations = new List<HourReservation>();
            for (int startHour = dayWorkingPlan.StartWorking.Hours; startHour <= dayWorkingPlan.EndWorking.Hours; startHour++)
            {
                HourReservation hourReservation = new HourReservation();
                hourReservation.StartReservation = new TimeSpan(startHour, 0, 0);
                hourReservation.EndReservation = new TimeSpan(startHour + 1, 0, 0);
                //hourReservation.HelmetWorkStations = db.HelmetWorkStations.Where(h => h.IsActive).ToList();
                reservation.HourReservations.Add(hourReservation);
            }

            return reservation;
        }

        private Reservation AddDayReservation(DoorDbContext db, DateTime date)
        {
            Reservation reservation = new Reservation();
            reservation.DateReservation = date;// new DateTime(year, month, day);
            reservation.HourReservations = new List<HourReservation>();
            WorkingPlan dayWorkingPlan = this.GetDayWorkingPlan(db, date);
            for (int startHour = dayWorkingPlan.StartWorking.Hours; startHour <= dayWorkingPlan.EndWorking.Hours; startHour++)
            {
                HourReservation hourReservation = new HourReservation();
                hourReservation.StartReservation = new TimeSpan(startHour, 0, 0);
                hourReservation.EndReservation = new TimeSpan(startHour + 1, 0, 0);
                //hourReservation.HelmetWorkStations = db.HelmetWorkStations.Where(h => h.IsActive).ToList();
                reservation.HourReservations.Add(hourReservation);
            }
            Reservation result = db.Reservations.Add(reservation);
            return result;
        }

        private WorkingPlan GetDayWorkingPlan(DoorDbContext db, DateTime date)
        {
            //string dayName = date.DayOfWeek.ToString();
            //PARAM dayParam = db.PARAMS.FirstOrDefault(d => d.Domain == WorkingDays.Domain && d.Label == dayName);
            //var dayWorkingPlan = db.WorkingPlans.FirstOrDefault(wp => wp.WorkingDayId == dayParam.Id);
            //return dayWorkingPlan;
            return WorkingPlanCaсhe.GetDayWorkingPlan(date.DayOfWeek);
        }





        private static readonly int activeHelmets = 4;




        /// <summary>
        /// Get free helmets for day
        /// </summary>
        /// <param name="year">Y</param>
        /// <param name="month">M</param>
        /// <param name="day">D</param>
        /// <returns>Free hemlmets in current day</returns>
        public DayReservationInfo GetDayReservation(int year, int month, int day)
        {
            DateTime dateReservation = new DateTime(year, month, day);
            DayReservationInfo result = new DayReservationInfo(dateReservation);
            result.FreeHelmets = new List<HourReservationInfo>();

            using (var db = new DoorDbContext())
            {
                Reservation dayReservation = db.Reservations.Include(r => r.HourReservations).FirstOrDefault(r => r.DateReservation.HasValue && r.DateReservation.Value.Year == year && r.DateReservation.Value.Month == month && r.DateReservation.Value.Day == day);

                WorkingPlan workingPlan = WorkingPlanCaсhe.GetDayWorkingPlan(dateReservation.DayOfWeek);

               
                for (int i = workingPlan.StartWorking.Hours; i <= workingPlan.EndWorking.Hours; i++)
                {
                    result.FreeHelmets.Add(new HourReservationInfo(i, activeHelmets));
                }

               if(dayReservation!=null && dayReservation.HourReservations!=null && dayReservation.HourReservations.Count>0)
                {
                    foreach(var reservation in dayReservation.HourReservations)
                    {
                       HourReservationInfo hourInfo = result.FreeHelmets.First(r => r.StartHour == reservation.StartReservation.Hours);
                       hourInfo.FreeHelmets = hourInfo.FreeHelmets - reservation.HelmetWorkStations.Count;
                    }
                }

            }

            return result;

        }

        /// <summary>
        /// Get quiq data for free placec in month
        /// </summary>
        /// <param name="year">year</param>
        /// <param name="month">Month</param>
        /// <returns>Free placec</returns>
        public int[] GetMonthFreeHelmetsDefault(int year, int month)
        {
            int daysInMoth = DateLogicHelper.GetDaysInMonth(year, month);
            int[] result = new int[daysInMoth];
            for (int i = 0; i < daysInMoth; i++)
            {
                DateTime dt = new DateTime(year, month, i + 1);
                result[i] = WorkingPlanCaсhe.GetWorkingHours(dt.DayOfWeek) * activeHelmets;
            }

            return result;
        }

        /// <summary>
        /// Get accurate data
        /// </summary>
        /// <param name="year">Year</param>
        /// <param name="month">Month</param>
        /// <param name="day">Day</param>
        /// <returns>Free placec</returns>
        public int GetFreeHelmetCount(int year, int month, int day)
        {
            DateTime dt = new DateTime(year, month, day);
            int freeHelmets = WorkingPlanCaсhe.GetWorkingHours(dt.DayOfWeek) * activeHelmets;
            using (var db = new DoorDbContext())
            {
                var dayReservation = db.Reservations.Include(r => r.HourReservations.Select(hr => hr.HelmetWorkStations)).FirstOrDefault(r => r.DateReservation.HasValue && r.DateReservation.Value == dt);///.Year == year && r.DateReservation.Value.Month == month && r.DateReservation.Value.Day == day);
                if (dayReservation != null)
                {
                    var busyHelmetsHours = dayReservation.HourReservations.SelectMany(hr => hr.HelmetWorkStations).Count();
                    freeHelmets = freeHelmets - busyHelmetsHours;
                }
            }
            return freeHelmets;
        }
    }
}