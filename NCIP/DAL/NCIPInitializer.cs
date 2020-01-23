using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data.Entity;
using NCIP.Models;

namespace NCIP.DAL
{
    public class NCIPInitializer : DropCreateDatabaseAlways<NcipDBContext>
    {
        protected override void Seed(NcipDBContext context)
        {
            var Barangays = new List<Barangay>{
                new Barangay{BarangayName="Alapang",Classification="Rural"},
                new Barangay{BarangayName="Alno",Classification="Rural"},
                new Barangay{BarangayName="Ambiong",Classification="Urban"},
                new Barangay{BarangayName="Bahong",Classification="Urban"},
                new Barangay{BarangayName="Balili",Classification="Urban"},
                new Barangay{BarangayName="Beckel",Classification="Urban"},
                new Barangay{BarangayName="Bineng",Classification="Rural"},
                new Barangay{BarangayName="Betag",Classification="Urban"},
                new Barangay{BarangayName="Cruz",Classification="Urban"},
                new Barangay{BarangayName="Lubas",Classification="Urban"},
                new Barangay{BarangayName="Pico",Classification="Urban"},
                new Barangay{BarangayName="Poblacion",Classification="Urban"},
                new Barangay{BarangayName="Puguis",Classification="Rural"},
                new Barangay{BarangayName="Shilan",Classification="Urban"},
                new Barangay{BarangayName="Tawang",Classification="Urban"},
                new Barangay{BarangayName="Wangal",Classification="Rural"}
            };
            Barangays.ForEach(s => context.Barangays.Add(s));
            context.SaveChanges();

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
            stages.ForEach(s => context.Stages.Add(s));
            context.SaveChanges();

            var community = new List<Community> {
                new Community{Sitio="Alapansg Proper", Population=100, Representative="n/a", BarangayID =1},
                new Community{Sitio="Camp Dangwa", Population=100, Representative="n/a", BarangayID=1},
                new Community{Sitio="Dapiting", Population=100, Representative="n/a", BarangayID = 1},
                new Community{Sitio="Ettong", Population=100, Representative="n/a", BarangayID = 1},
                new Community{Sitio="Samoyao", Population=100, Representative="n/a", BarangayID = 1}
            };
            community.ForEach(s => context.Communities.Add(s));
            context.SaveChanges();

            var company = new List<Company>{
                new Company{Name="Hedcor Incorporated", Address="214 Ambuclao Rd. Obulan, Beckel, La Trinidad, Benguet", Landline="424 4762", Mobile="n/a", Businesstype="Power Generation"},
                new Company{Name="Texas Instruments", Address="271 Magsaysay Rd, Brgy. Loakan Proper, Baguio, 2600 Benguet", Landline="305 2151", Mobile="n/a", Businesstype="Semi Conductor"}
            };
            company.ForEach(s => context.Companies.Add(s));
            context.SaveChanges();

            var person = new List<Person>
            {
                new Person{Lastname="Gazmen",Firstname="Prinz Juno Gian carlo",Address="Baguio City", landline="111-222", Mobile="09061234567",CompanyID=1},
                new Person{Lastname="Doe",Firstname="John",Address="La Trinidad", landline="222-5556", Mobile="01917894561",CompanyID=2}
            };
            person.ForEach(s => context.Persons.Add(s));
            context.SaveChanges();

            var project = new List<Project>{
                new Project{projectTitle="La Trinidad hydro Plant", projectType="Power Generation", reference="BENG-001",CompanyID=1, timeStamp=DateTime.Today, StageID=1,PersonID=1},
                new Project{projectTitle="Hedcor Benguet Office", projectType="Corporate", reference="BENG-002",CompanyID=1, timeStamp=DateTime.Today, StageID=1,PersonID=1},
                new Project{projectTitle="Semi Conductor Factory", projectType="Electronics", reference="BENG-003",CompanyID=2,timeStamp=DateTime.Today, StageID=1,PersonID=2}
            };
            project.ForEach(s => context.Projects.Add(s));
            context.SaveChanges();

            var enthnicGroups = new List<EthnicGroup> {
                new EthnicGroup{Name="Ibaloi"},
                new EthnicGroup{Name="Kankanaey"},
                new EthnicGroup{Name="Kalanguya"},
                new EthnicGroup{Name="Bontok"},
                new EthnicGroup{Name="Kiangan"},
                new EthnicGroup{Name="Itneg"},
                new EthnicGroup{Name="Non_IP"}
            };
            enthnicGroups.ForEach(s => context.EthnicGroups.Add(s));
            context.SaveChanges();

            var meetings = new List<Meeting> {
                new Meeting{
                    Name ="Birthday party ni boss",
                    DateHeld =new DateTime(2012,12,12),
                    ProjectID=2,
                    StageID =1
                },
                new Meeting{
                    Name ="After party ni boss",
                    DateHeld =new DateTime(2012,12,13),
                    ProjectID=2,
                    StageID =2
                },
                new Meeting{
                    Name ="Lamay ni boss",
                    DateHeld =new DateTime(2012,12,14),
                    ProjectID=2,
                    StageID =3
                }

            };
            meetings.ForEach(s => context.Meetings.Add(s));
            context.SaveChanges();

            var atendees = new List<Attendance>
            {
                new Attendance{
                    Lastname="Marsden",
                    Firstname="Ivan",
                    MeetingID=1,
                    CommunityID=1,
                    EthnicGroupID=1,
                },
                new Attendance{
                    Lastname="Mayo",
                    Firstname="Loren",
                    MeetingID=1,
                    CommunityID=4,
                    EthnicGroupID=2,
                },
                new Attendance{
                    Lastname="Battle",
                    Firstname="Sasha",
                    MeetingID=1,
                    CommunityID=5,
                    EthnicGroupID=4,
                },
                new Attendance{
                    Lastname="Gibbs",
                    Firstname="Velma",
                    MeetingID=1,
                    CommunityID=4,
                    EthnicGroupID=6,
                },
                new Attendance{
                    Lastname="Marsden",
                    Firstname="Ivan",
                    MeetingID=2,
                    CommunityID=1,
                    EthnicGroupID=1,
                },
                new Attendance{
                    Lastname="Mayo",
                    Firstname="Loren",
                    MeetingID=2,
                    CommunityID=4,
                    EthnicGroupID=2,
                },
                new Attendance{
                    Lastname="Battle",
                    Firstname="Sasha",
                    MeetingID=2,
                    CommunityID=5,
                    EthnicGroupID=4,
                },
                new Attendance{
                    Lastname="Mayo",
                    Firstname="Loren",
                    MeetingID=3,
                    CommunityID=4,
                    EthnicGroupID=2,
                },
            };
            atendees.ForEach(s => context.Attendance.Add(s));
            context.SaveChanges();



            var affected = new List<AffectedCommunity> {
                new AffectedCommunity{ProjectID=1,CommunityID=1},
                new AffectedCommunity{ProjectID=1,CommunityID=2},
                new AffectedCommunity{ProjectID=1,CommunityID=3},
                new AffectedCommunity{ProjectID=3,CommunityID=4},
                new AffectedCommunity{ProjectID=3,CommunityID=3},
                new AffectedCommunity{ProjectID=3,CommunityID=2},
                new AffectedCommunity{ProjectID=2,CommunityID=1},
                new AffectedCommunity{ProjectID=2,CommunityID=5},
                new AffectedCommunity{ProjectID=2,CommunityID=4},
            };
            affected.ForEach(s => context.AffectedCommunities.Add(s));
            context.SaveChanges();

        }
    }
}