using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
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
            [Required] string to)
        {
            try
            {
                var isCoordinates = Coordinates.IsCoordinates(from) && 
                                    Coordinates.IsCoordinates(to);
                
                string coordinates;
                if (isCoordinates)
                {
                    coordinates = string.Join(";", from, to);
                }
                else
                {
                    var fromPlace = await _openStreetMapClient.SearchFirst(query: from);
                    var toPlace = await _openStreetMapClient.SearchFirst(query: to);
                    coordinates = string.Join(";",
                        new Coordinates(fromPlace.Latitude, fromPlace.Longitude),
                        new Coordinates(toPlace.Latitude, toPlace.Longitude));
                }

                return Ok(await _mapBoxClient.GetDirectionsAsync("mapbox/" + profile, coordinates));
            }
            catch (HttpException exception)
            {
                return BadRequest(await exception.Response.Content.ReadAsStringAsync());
            }
        }
    }
}