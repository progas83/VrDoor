using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;
using Door.StaticData;

namespace DoorDataModel
{
    public class DoorDbInitializer : CreateDatabaseIfNotExists<DoorDbContext>
    {
        protected override void Seed(DoorDbContext context)
        {

            this.UpdateHelmetTypes(context);

            this.UpdateHelmetPrice(context);

            

            base.Seed(context);
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