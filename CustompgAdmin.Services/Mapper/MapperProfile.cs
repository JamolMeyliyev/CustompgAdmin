using AutoMapper;
using CustompgAdmin.DataAccess.Entities;
using CustompgAdmin.Services.DTOs.Column;
using CustompgAdmin.Services.DTOs.Connection;
using CustompgAdmin.Services.DTOs.Database;
using CustompgAdmin.Services.DTOs.Table;
using Table = CustompgAdmin.DataAccess.Entities.Table;
namespace CustompgAdmin.Services.Mapper;

public class MapperProfile:Profile
{
    public MapperProfile()
    {
        CreateMap<CreateDatabase,DatabaseEntity>();
        CreateMap<DatabaseEntity, DatabaseDto>().ForMember(s => s.TableDtos, p => p.MapFrom(s => s.Tables.Select(s =>
        new TableDto()
        {
            Name = s.Name,
            ColumnDtos = s.Columns.Select(t=> new ColumnReturnDto()
            {
                Name = t.Name
            }).ToList(),
        }).ToList()
        )); ;

        CreateMap<CreateConnectionDB, ConnectionEntity>();
        CreateMap<ConnectionEntity,ConnectionDto>();
        CreateMap<CreateTable, Table>();
        CreateMap<Table, TableDto>().ForMember(s => s.ColumnDtos,p => p.MapFrom(s => s.Columns.Select(s =>
        new ColumnReturnDto()
        {
            Name = s.Name,
            
        }).ToList()
        ));
     
        CreateMap<Column,ColumnReturnDto>().ForMember(s => s.Name,p => p.MapFrom(a => a.Name));
    }
}
