using AutoMapper;
using Microsoft.AspNet.Identity.EntityFramework;
using SportsTech.Data;
using SportsTech.Data.Entity;
using SportsTech.Web.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace SportsTech.Web
{
    public static class SiteAutoMapperConfig
    {
        public static void Initialize()
        {
            Mapper.CreateMap<RegisterViewModel, ApplicationUser>()
                .ForMember(dest => dest.UserProfile, opt => opt.MapFrom(v => Mapper.Map<RegisterViewModel, Data.Model.UserProfile>(v)));

            Mapper.CreateMap<RegisterViewModel, Data.Model.UserProfile>()
                .ForMember(dest => dest.FirstName, opt => opt.MapFrom(v => v.FirstName.Trim()))
                .ForMember(dest => dest.LastName, opt => opt.MapFrom(v => v.LastName.Trim()))
                .ForMember(dest => dest.EmailAddress, opt => opt.MapFrom(v => v.EmailAddress.Trim()))
                .ForMember(dest => dest.TimeZone, opt => opt.UseValue("en-NZ"))
                .ForMember(dest => dest.DateFormat, opt => opt.UseValue("dd-MM-yyyy"));

            Mapper.CreateMap<Areas.Clubs.ViewModels.Club.CreateViewModel, Data.Model.Club>();

            Mapper.CreateMap<Areas.Clubs.ViewModels.Team.CreateViewModel, Data.Model.Team>()
                .ForMember(dest => dest.Name, opt => opt.MapFrom(v => v.Name.Trim()));
            Mapper.CreateMap<Data.Model.Team, Areas.Clubs.ViewModels.Team.CreateViewModel>()
                .ForMember(dest => dest.Id, opt => opt.MapFrom(v => v.Id))
                .ForMember(dest => dest.Name, opt => opt.MapFrom(v => v.Name));
        }
    }
}