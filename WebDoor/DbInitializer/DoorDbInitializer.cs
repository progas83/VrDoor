using System;
using System.Data.Entity;
using Door.StaticData;
using DoorDataModel;

namespace WebDoor
{
    public class DoorDbInitializer : CreateDatabaseIfNotExists<DoorDbContext>
    {
        protected override void Seed(DoorDbContext context)
        {

            ///this.UpdateHelmetTypes(context);

            this.UpdateHelmetPrice(context);
            context.SaveChanges();

            this.UpdateWorkingHours(context);
            context.SaveChanges();

            this.UpdateHelmetWorkStations(context);
            context.SaveChanges();




            base.Seed(context);
        }

        private void UpdateHelmetWorkStations(DoorDbContext context)
        {
            PARAM helmetParam1 = new PARAM();
            helmetParam1.Domain = HelmetTypes.Domain;
            helmetParam1.Label = HelmetTypes.StandartType;
            helmetParam1.Value = HelmetTypes.StandartTypeValue;

            HelmetWorkStation station1 = new HelmetWorkStation();
            station1.HelmetType = helmetParam1;
            station1.WorkStationNumber = 1;
            

            context.HelmetWorkStations.Add(station1);

            HelmetWorkStation station3 = new HelmetWorkStation();
            station3.HelmetType = helmetParam1;
            station3.WorkStationNumber = 3;

            context.HelmetWorkStations.Add(station3);

            PARAM helmetParam2 = new PARAM();
            helmetParam2.Domain = HelmetTypes.Domain;
            helmetParam2.Label = HelmetTypes.PremiumType;
            helmetParam2.Value = HelmetTypes.PremiumTypeValue;

            HelmetWorkStation station2 = new HelmetWorkStation();
            station2.HelmetType = helmetParam2;
            station2.WorkStationNumber = 2;
            context.HelmetWorkStations.Add(station2);

            HelmetWorkStation station4 = new HelmetWorkStation();
            station4.HelmetType = helmetParam2;
            station4.WorkStationNumber = 4;

            context.HelmetWorkStations.Add(station4);

        }

        private void UpdateWorkingHours(DoorDbContext context)
        {
            PARAM monday = new PARAM();
            monday.Domain = WorkingDays.Domain;
            monday.Label = WorkingDays.Monday;
            monday.Value = WorkingDays.MondayValue;

            WorkingPlan mondayPlan = new WorkingPlan();
            mondayPlan.StartWorking = new TimeSpan(14, 0, 0);
            mondayPlan.EndWorking = new TimeSpan(22, 0, 0);
            mondayPlan.WorkingDay = monday;
            context.WorkingPlans.Add(mondayPlan);

            PARAM tuesday = new PARAM();
            tuesday.Domain = WorkingDays.Domain;
            tuesday.Label = WorkingDays.Tuesday;
            tuesday.Value = WorkingDays.TuesdayValue;

            WorkingPlan tuesdayPlan = new WorkingPlan();
            tuesdayPlan.StartWorking = new TimeSpan(14, 0, 0);
            tuesdayPlan.EndWorking = new TimeSpan(22, 0, 0);
            tuesdayPlan.WorkingDay = tuesday;

            context.WorkingPlans.Add(tuesdayPlan);

            PARAM wednesday = new PARAM();
            wednesday.Domain = WorkingDays.Domain;
            wednesday.Label = WorkingDays.Wednesday;
            wednesday.Value = WorkingDays.WednesdayValue;

            WorkingPlan wednesdayPlan = new WorkingPlan();
            wednesdayPlan.StartWorking = new TimeSpan(14, 0, 0);
            wednesdayPlan.EndWorking = new TimeSpan(22, 0, 0);
            wednesdayPlan.WorkingDay = wednesday;
            context.WorkingPlans.Add(wednesdayPlan);


            PARAM thursday  = new PARAM();
            thursday.Domain = WorkingDays.Domain;
            thursday.Label = WorkingDays.Thursday;
            thursday.Value = WorkingDays.ThursdayValue;

            WorkingPlan thursdayPlan = new WorkingPlan();
            thursdayPlan.StartWorking = new TimeSpan(14, 0, 0);
            thursdayPlan.EndWorking = new TimeSpan(22, 0, 0);
            thursdayPlan.WorkingDay = thursday;
            context.WorkingPlans.Add(thursdayPlan);

            PARAM friday = new PARAM();
            friday.Domain = WorkingDays.Domain;
            friday.Label = WorkingDays.Friday;
            friday.Value = WorkingDays.FridayValue;

            WorkingPlan fridayPlan = new WorkingPlan();
            fridayPlan.StartWorking = new TimeSpan(14, 0, 0);
            fridayPlan.EndWorking = new TimeSpan(22, 0, 0);
            fridayPlan.WorkingDay = friday;
            context.WorkingPlans.Add(fridayPlan);

            PARAM saturday = new PARAM();
            saturday.Domain = WorkingDays.Domain;
            saturday.Label = WorkingDays.Saturday;
            saturday.Value = WorkingDays.SaturdayValue;

            WorkingPlan saturdayPlan = new WorkingPlan();
            saturdayPlan.StartWorking = new TimeSpan(10, 0, 0);
            saturdayPlan.EndWorking = new TimeSpan(23, 0, 0);
            saturdayPlan.WorkingDay = saturday;
            context.WorkingPlans.Add(saturdayPlan);

            PARAM sunday = new PARAM();
            sunday.Domain = WorkingDays.Domain;
            sunday.Label = WorkingDays.Sunday;
            sunday.Value = WorkingDays.SundayValue;

            WorkingPlan sundayPlan = new WorkingPlan();
            sundayPlan.StartWorking = new TimeSpan(10, 0, 0);
            sundayPlan.EndWorking = new TimeSpan(23, 0, 0);
            sundayPlan.WorkingDay = sunday;
            context.WorkingPlans.Add(sundayPlan);
        }

        private void UpdateHelmetPrice(DoorDbContext context)
        {

            PARAM standrtPrice = new PARAM();
            standrtPrice.Domain = HelmetPrices.Domain;
            standrtPrice.Label = HelmetPrices.StandartPrice;
            standrtPrice.Value = HelmetPrices.StandartPriceValue;

            PARAM premiumPrice = new PARAM();
            premiumPrice.Domain = HelmetPrices.Domain;
            premiumPrice.Label = HelmetPrices.PremiumPrice;
            premiumPrice.Value = HelmetPrices.PremiumPriceValue;

            context.PARAMS.Add(standrtPrice);
            context.PARAMS.Add(premiumPrice);
        }

        private void UpdateHelmetTypes(DoorDbContext db)
        {
            PARAM helmetParam1 = new PARAM();
            helmetParam1.Domain = HelmetTypes.Domain;
            helmetParam1.Label = HelmetTypes.StandartType;
            helmetParam1.Value = HelmetTypes.StandartTypeValue;


            PARAM helmetParam2 = new PARAM();
            helmetParam2.Domain = HelmetTypes.Domain;
            helmetParam2.Label = HelmetTypes.PremiumType;
            helmetParam2.Value = HelmetTypes.PremiumTypeValue;

            db.PARAMS.Add(helmetParam1);
            db.PARAMS.Add(helmetParam2);
        }
    }
}