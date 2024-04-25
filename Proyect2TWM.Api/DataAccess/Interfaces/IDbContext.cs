using System.Data.Common;

namespace Proyect2TWM.Api.DataAccess.Interfaces;

public interface IDbContext
{
    //llamar la conexion
    DbConnection Connection { get; }
    
}