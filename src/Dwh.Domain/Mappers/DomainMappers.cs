using AutoMapper;
using Dwh.Domain.Commands;
using Dwh.Domain.Models;
using Dwh.Domain.Objects;
using System;

namespace Dwh.Domain.Mappers
{
    public class DomainMappers : Profile
    {
        public DomainMappers()
        {
            CreateMap<Matrix, ViewMatrixModel>().ForMember(x => x.Dimensions, y => y.Ignore())
                                                .ForMember(x => x.Facts, y => y.Ignore());

            CreateMap<MatrixItem, ViewMatrixItemModel>();

            CreateMap<Fact, ViewFactModel>();
            CreateMap<CreateFactCommand, Fact>().AfterMap((src, dest) =>
            {
                dest.Id = Guid.NewGuid();
            });

            CreateMap<Dimension, ViewDimensionModel>();
            CreateMap<CreateDimensionCommand, Dimension>().AfterMap((src, dest) =>
            {
                dest.Id = Guid.NewGuid();
            });
        }
    }
}
