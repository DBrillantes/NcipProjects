using NCIP.Models;
using NCIP.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace NCIP.App_Start
{
    public class MappingConfig
    {

        public static void RegisterMaps() {
            AutoMapper.Mapper.Initialize(c=> {
                c.CreateMap<Project, ProjectTableViewModel>();
                c.CreateMap<Project, ProjectDetailsViewModel>();
                c.CreateMap<Project, ProjectEditViewModel>();
                c.CreateMap<AffectedCommunity, AffectedCommunityTableViewModel>();
                c.CreateMap<Community, CommunityViewModel>();
                c.CreateMap<Meeting, MeetingTableViewModel>();
                c.CreateMap<MeetingCreationViewModel, Meeting>();
               
            });
        }
    }
}