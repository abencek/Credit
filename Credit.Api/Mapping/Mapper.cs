using AutoMapper;
using System;


namespace Credit.Api.Mapping
{
    /// <summary>
    /// AutoMapper configuration
    /// AutoMapper is used to convert Models into Dtos and Dtos into Models
    /// This creates singleton static mapper instance
    /// </summary>
    public static class Mapper
    {
        private static readonly Lazy<IMapper> Lazy = new(() =>
        {
            var config = new MapperConfiguration(
                    cfg => cfg.AddProfile<MapperProfile>()
                );
            var mapper = config.CreateMapper();

            return mapper;

        });

        public static IMapper Instance => Lazy.Value;
    }
}
