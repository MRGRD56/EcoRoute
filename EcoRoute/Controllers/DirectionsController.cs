using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;
using EcoRoute.Common.Models;
using EcoRoute.MapBox.Interop;
using Microsoft.AspNetCore.Mvc;
using Mrgrd56.JsonHttpClient.Exceptions;
using OpenStreetMap.Interop;

namespace EcoRoute.Controllers
{
    [Route("api/[controller]")]
    public class DirectionsController : ControllerBase
    {
        private readonly IMapBoxClient _mapBoxClient;
        private readonly OpenStreetMapClient _openStreetMapClient;

        public DirectionsController(
            IMapBoxClient mapBoxClient,
            OpenStreetMapClient openStreetMapClient)
        {
            _mapBoxClient = mapBoxClient;
            _openStreetMapClient = openStreetMapClient;
        }
        
        public async Task<IActionResult> Get(
            [Required] string profile, 
            [Required] string from,
            [Required] string to,
            string detours)
        {
            try
            {
                var isCoordinates = Coordinates.IsCoordinates(from) && 
                                    Coordinates.IsCoordinates(to);
                
                Coordinates[] coordinates;
                if (isCoordinates)
                {
                    coordinates = new[]
                    {
                        Coordinates.Parse(from), 
                        Coordinates.Parse(to)
                    };
                }
                else
                {
                    var fromPlace = await _openStreetMapClient.SearchFirst(query: from);

                    if (fromPlace == null)
                    {
                        return BadRequest($"Место не найдено: {from}");
                    }
                    
                    var toPlace = await _openStreetMapClient.SearchFirst(query: to);
                    
                    if (toPlace == null)
                    {
                        return BadRequest($"Место не найдено: {to}");
                    }
                    
                    coordinates = new[] 
                    {
                        new Coordinates(fromPlace.Latitude, fromPlace.Longitude),
                        new Coordinates(toPlace.Latitude, toPlace.Longitude)
                    };
                }

                var coordinatesList = coordinates.Select(c => c.ToString()).ToList();
                if (!string.IsNullOrWhiteSpace(detours))
                {
                    coordinatesList.Insert(1, detours);
                }
                var coordinatesString = string.Join(";", coordinatesList);

                var directions = await _mapBoxClient.GetDirectionsAsync("mapbox/" + profile, coordinatesString);
                var result = new
                {
                    From = coordinates[0].AsNumbersArray(),
                    To = coordinates[1].AsNumbersArray(),
                    Duration = directions["routes"]?[0]?.Value<double>("duration"),
                    Distance = directions["routes"]?[0]?.Value<double>("distance"),
                    Coordinates = directions["routes"]?[0]?["geometry"]?["coordinates"]
                };
                return Ok(result);
            }
            catch (Exception exception)
            {
                switch (exception)
                {
                    case HttpException httpException:
                        return BadRequest(await httpException.Response.Content.ReadAsStringAsync());
                    case FormatException:
                        return BadRequest("Invalid coordinates");
                    default:
                        throw;
                }
            }
        }
    }
}