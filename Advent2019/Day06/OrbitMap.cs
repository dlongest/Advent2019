using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Advent2019.Day06
{
    public class OrbitMap
    {
        private string centerOfMass = "COM";
        private IDictionary<string, string> satelliteToBodyOrbits = new Dictionary<string, string>();
        private IDictionary<string, List<string>> bodyToSatelliteOrbits = new Dictionary<string, List<string>>();

        public OrbitMap AddOrbit(string body, string satellite)
        {
            this.satelliteToBodyOrbits.Add(satellite, body);

            if (!this.bodyToSatelliteOrbits.ContainsKey(body))
            {
                this.bodyToSatelliteOrbits.Add(body, new List<string>());
            }

            this.bodyToSatelliteOrbits[body].Add(satellite);

            return this;
        }


        public int Checksum(string body)
        {
            return DistanceTo(body, this.centerOfMass);
        }

        private int DistanceTo(string body, string target)
        {
            var checkSum = 0;
            var inspectBody = body;

            while (!inspectBody.Equals(target, StringComparison.OrdinalIgnoreCase))
            {
                checkSum++;

                inspectBody = satelliteToBodyOrbits[inspectBody];
            }

            return checkSum;
        }

        public int Checksum()
        {
            return this.satelliteToBodyOrbits.Keys.Sum(k => this.Checksum(k));
        }

        public string NearestCommonBody(string satellite1, string satellite2)
        {
            var (longest, shortest) = Orbits(satellite1, satellite2);

            foreach (var body in longest)
            {
                if (shortest.Contains(body))
                {
                    return body;
                }
            }

            return this.centerOfMass;
        }

        public int Transfers(string satellite1, string satellite2)
        {
            var commonBody = this.NearestCommonBody(satellite1, satellite2);

            var distance1 = DistanceTo(satellite1, commonBody);
            var distance2 = DistanceTo(satellite2, commonBody);

            return distance1 + distance2 - 2;
        }

        private Tuple<string[], string[]> Orbits(string satellite1, string satellite2)
        {
            var orbits1 = this.Orbits(satellite1).ToArray();
            var orbits2 = this.Orbits(satellite2).ToArray();

            return (orbits1.Length >= orbits2.Length) ? Tuple.Create(orbits1, orbits2) : Tuple.Create(orbits2, orbits1);
        }

        public IEnumerable<string> Orbits(string satellite)
        {
            if (satellite.Equals(this.centerOfMass, StringComparison.OrdinalIgnoreCase))
            {
                return Enumerable.Empty<string>();
            }

            var orbits = new List<string>();
            var inspectBody = satellite;
          
            while (!inspectBody.Equals(this.centerOfMass, StringComparison.OrdinalIgnoreCase))
            {                
                inspectBody = satelliteToBodyOrbits[inspectBody];
                orbits.Add(inspectBody);
            }

            return orbits;
        }
    }
}
