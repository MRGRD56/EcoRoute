using EcoRoute.Common.Attributes;

namespace EcoRoute.MapBox.Interop.Models
{
    public enum RoutingProfile
    {
        /// <summary>
        /// mapbox/driving-traffic
        /// </summary>
        [StringValue("mapbox/driving-traffic")]
        DrivingTraffic,
        
        /// <summary>
        /// mapbox/driving
        /// </summary>
        [StringValue("mapbox/driving")]
        Driving,
        
        /// <summary>
        /// mapbox/walking
        /// </summary>
        [StringValue("mapbox/walking")]
        Walking,
        
        /// <summary>
        /// mapbox/cycling
        /// </summary>
        [StringValue("mapbox/cycling")]
        Cycling
    }
}