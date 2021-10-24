using JocsDeGuerra.Models;
using System.Collections.Generic;

namespace JocsDeGuerra.Interfaces.Services
{
    public interface IMapLocationService
    {
        List<MapLocation> GetAllLocations();
    }
}
