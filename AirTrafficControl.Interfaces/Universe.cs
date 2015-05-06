﻿using Microsoft;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Collections.ObjectModel;

namespace AirTrafficControl.Interfaces
{
    public class Universe
    {
        public static Universe Current = new Universe();

        public ReadOnlyCollection<Airport> Airports { get; private set; }
        public ReadOnlyCollection<Route> Routes { get; private set; }

        public Universe()
        {
            Initialize();
        }

        public Route GetRouteBetween(Fix from, Fix to)
        {
            Requires.NotNull(from, "from");
            Requires.NotNull(to, "to");

            Route connectingRoute = Routes.Where(r => r.Fixes.Contains(from) && r.Fixes.Contains(to)).FirstOrDefault();
            return connectingRoute;
        }

        private void Initialize()
        {

            var ksea = new Airport("KSEA", "Seattle-Tacoma International", Direction.West);
            var kgeg = new Airport("KGEG", "Spokane International", Direction.North);
            var kpdx = new Airport("KPDX", "Portland International", Direction.Southeast);

            var eph = new Fix("EPH", "Ephrata VORTAC");
            var mwh = new Fix("MWH", "Moses Lake VOR-DME");
            var ykm = new Fix("YKM", "Yakima VORTAC");
            var malay = new Fix("MALAY", "MALAY intersection");

            var v120 = new Route("V120");
            v120.Fixes = new ReadOnlyCollection<Fix>(new Fix[] { ksea, eph, kgeg });

            var v448 = new Route("V448");
            v448.Fixes = new ReadOnlyCollection<Fix>(new Fix[] { kgeg, mwh, ykm, kpdx });

            var v23 = new Route("V23");
            v23.Fixes = new ReadOnlyCollection<Fix>(new Fix[] { ksea, malay, kpdx });

            this.Airports = new ReadOnlyCollection<Airport>(new Airport[] { ksea, kgeg, kpdx });
            this.Routes = new ReadOnlyCollection<Route>(new Route[] { v120, v23, v448 });
        }
    }
}