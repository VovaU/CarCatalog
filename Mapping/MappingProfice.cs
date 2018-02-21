using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using CarCatalog.Controllers.Resources;
using CarCatalog.Models;

namespace CarCatalog.Mapping
{
    public class MappingProfice:Profile
    {
        public MappingProfice()
        {
            CreateMap<Make, MakeResource>();
            CreateMap<Model, ModelResource>();
            CreateMap<Feature, FeatureResource>();

        }
    }
}
