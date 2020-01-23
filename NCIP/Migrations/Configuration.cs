namespace NCIP.Migrations
{
    using NCIP.Models;
    using System;
    using System.Collections.Generic;
    using System.Data.Entity;
    using System.Data.Entity.Migrations;
    using System.Linq;

    internal sealed class Configuration : DbMigrationsConfiguration<NCIP.Models.ApplicationDbContext>
    {
        public Configuration()
        {
            AutomaticMigrationsEnabled = false;
        }

        protected override void Seed(NCIP.Models.ApplicationDbContext context)
        {
            //  This method will be called after migrating to the latest version.

            //  You can use the DbSet<T>.AddOrUpdate() helper extension method 
            //  to avoid creating duplicate seed data.

            var stages = new List<Stage>
            {
                new Stage{StageName="Endorsement of Cp Application to NCIP"},
                new Stage{StageName="Conduct of FBI & Preparation of Report"},
                new Stage{StageName="CNO"},
                new Stage{StageName="Pre-FPIC"},
                new Stage{StageName="First Community Assembly"},
                new Stage{StageName="Second Community Assembly"},
                new Stage{StageName="Concensus Building / Decision Making"},
                new Stage{StageName="Negotiation; MOA Drafting, Validation, &Signing; Resolution of Consent"},
                new Stage{StageName="Preparation of FPIC Report"},
                new Stage{StageName="RD-RRT / ADO-LAO / CEB-Deliberation; CP"},
                new Stage{StageName="Resolution of Non-Consent"},
                new Stage{StageName="Request for Reconsideration"},
                new Stage{StageName="Community Consultation / Assembly"},
                new Stage{StageName="Inform FPIC Team - RD - Applicant"},
            };
            stages.ForEach(s => context.Stages.AddOrUpdate(p => p.StageName, s));
            context.SaveChanges();



            ///optional?
            var enthnicGroups = new List<EthnicGroup> {
                new EthnicGroup{Name="Ibaloi"},
                new EthnicGroup{Name="Kankanaey"},
                new EthnicGroup{Name="Kalanguya"},
                new EthnicGroup{Name="Bontok"},
                new EthnicGroup{Name="Kiangan"},
                new EthnicGroup{Name="Itneg"},
                new EthnicGroup{Name="Non_IP"}
            };
            enthnicGroups.ForEach(s => context.EthnicGroups.AddOrUpdate(p => p.Name, s));
            context.SaveChanges();
        }
    }
}
