using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using CarCatalog.Controllers.Resources;
using CarCatalog.Core.Models;

namespace CarCatalog.Mapping
{
    public class MappingProfice:Profile
    {
        public MappingProfice()
        {
            //domain to api resource
            CreateMap<Make, MakeResource>();
            CreateMap<Make, KeyValuePairResource>();
            CreateMap<Model, KeyValuePairResource>();
            CreateMap<Feature, KeyValuePairResource>();
            CreateMap<Vehicle, SaveVehicleResource>()
                .ForMember(vr => vr.Contact,
                    opt => opt.MapFrom(v =>
                        new ContactResource {Name = v.ContactName, Email = v.ContactEmail, Phone = v.ContactPhone}))
                .ForMember(vr => vr.Features, opt => opt.MapFrom(v => v.Features.Select(vf => vf.FeatureId)));
            CreateMap<Vehicle, VehicleResource>()
                .ForMember(vr => vr.Contact,
                    opt => opt.MapFrom(v =>
                        new ContactResource {Name = v.ContactName, Email = v.ContactEmail, Phone = v.ContactPhone}))
                .ForMember(vr => vr.Features,
                    opt => opt.MapFrom(v =>
                        v.Features.Select(vf => new KeyValuePairResource { Id = vf.Feature.Id, Name = vf.Feature.Name})))
                .ForMember(vr => vr.Make, opt => opt.MapFrom(v => v.Model.Make));
            //api resource to domain
            CreateMap<SaveVehicleResource, Vehicle>()
                .ForMember(v => v.Id, opt => opt.Ignore())
                .ForMember(v => v.ContactName, opt => opt.MapFrom(vr => vr.Contact.Name))
                .ForMember(v => v.ContactEmail, opt => opt.MapFrom(vr => vr.Contact.Email))
                .ForMember(v => v.ContactPhone, opt => opt.MapFrom(vr => vr.Contact.Phone))
                .ForMember(v => v.Features,
                    opt => opt.Ignore())
                .AfterMap((vr, v) =>
                {
                    //remove unselected
                    var removedFeatures = new List<VehicleFeature>();
                    foreach (var VARIABLE in v.Features)
                    {
                        if (!vr.Features.Contains(VARIABLE.FeatureId))
                        {
                            removedFeatures.Add(VARIABLE);

                        }
                    }

                    foreach (var VARIABLE in removedFeatures)
                    {
                        v.Features.Remove(VARIABLE);
                    }
                    // add new f
                    foreach (var VARIABLE in vr.Features)
                    {
                        if (!v.Features.Any(f => f.FeatureId == VARIABLE))
                        {
                            v.Features.Add(new VehicleFeature{FeatureId = VARIABLE});
                        }
                    }

                });




        }
    }
}
