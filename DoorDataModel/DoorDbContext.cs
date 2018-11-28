using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Data.Entity.ModelConfiguration.Conventions;
using System.Linq;
using System.Web;

namespace DoorDataModel
{
    public class DoorDbContext : DbContext
    {
        public DoorDbContext():base("name = DoorDbEntities")
        {

        }
        public DbSet<PARAM> PARAMS { get; set; }


        public DbSet<WorkingPlan> WorkingPlans { get; set; }

        public DbSet<HelmetWorkStation> HelmetWorkStations { get; set; }

        public DbSet<Visitor> Visitors { get; set; }

        public DbSet<Reservation> Reservations { get; set; }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {

            modelBuilder.Conventions.Remove<ManyToManyCascadeDeleteConvention>();
            //modelBuilder.Entity<Reservation>()
            //    .HasMany(r => r.HelmetWorkStations)
            //    .WithMany().WillCascadeOnDelete(false);

            //hs => hs.Reservations)
                
            //    .Map(m =>
            //    {
            //        m.ToTable("WorkStationsReservations");
            //        m.MapLeftKey("WordStationId");
            //        m.MapRightKey("ReservationId");
                    
            //    });
            base.OnModelCreating(modelBuilder);
        }
    }
}