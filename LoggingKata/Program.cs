﻿using System;
using System.Linq;
using System.IO;
using GeoCoordinatePortable;

namespace LoggingKata
{
    class Program
    {
        static readonly ILog logger = new TacoLogger();
        const string csvPath = "TacoBell-US-AL.csv";

        static void Main(string[] args)
        {
            // TODO:  Find the two Taco Bells that are the furthest from one another.
            // HINT:  You'll need two nested forloops ---------------------------          


            logger.LogInfo("Log initialized");

            // use File.ReadAllLines(path) to grab all the lines from your csv file
            // Log and error if you get 0 lines and a warning if you get 1 line
            var lines = File.ReadAllLines(csvPath);

            logger.LogInfo($"Lines: {lines[0]}");
            logger.LogWarning($"Lines: {lines[1]}");

            // Create a new instance of your TacoParser class
            var parser = new TacoParser();

            // Grab an IEnumerable of locations using the Select command: var locations = lines.Select(parser.Parse);
            var locations = lines.Select(parser.Parse).ToArray();

            // DON'T FORGET TO LOG YOUR STEPS

            // Now that your Parse method is completed, START BELOW ----------

            // TODO: Create two `ITrackable` variables with initial values of `null`. These will be used to store your two taco bells that are the farthest from each other.
            // Create a `double` variable to store the distance            

            ITrackable tacoBell1 = null;
            ITrackable tacoBell2 = null;
            double distance = 0.00;

            // Include the Geolocation toolbox, so you can compare locations: `using GeoCoordinatePortable;`


            //HINT NESTED LOOPS SECTION---------------------

            for (int i = 0; i < locations.Length; i++)      // Do a loop for your locations to grab each location as the origin (perhaps: `locA`)
            {
                var locA = locations[i];        // Create a new corA Coordinate with your locA's lat and long

                var corA = new GeoCoordinate();

                corA.Latitude = locA.Location.Latitude;

                corA.Longitude = locA.Location.Longitude;

                for (int j = i + 1; j < locations.Length; j++)     // Now, do another loop on the locations with the scope of your first loop, so you can grab
                                                                   // the "destination" location (perhaps: `locB`)
                {                 

                    var locB = locations[j];

                    var corB = new GeoCoordinate();         // Create a new Coordinate with your locB's lat and long

                    corB.Latitude = locB.Location.Latitude;

                    corB.Longitude = locB.Location.Longitude;

                    if (corA.GetDistanceTo(corB) > distance)        // Now, compare the two using `.GetDistanceTo()`, which returns a double
                    {
                        distance = corA.GetDistanceTo(corB);         // If the distance is greater than the currently saved distance, update the distance and
                                                                     // the two `ITrackable` variables you set above
                        tacoBell1 = locA;
                        tacoBell2 = locB;

                    }

                }                                                // Once you've looped through everything, you've found the two Taco Bells farthest away from each other.

            }
            Console.WriteLine($"The longest distance is from {tacoBell1.Name} to {tacoBell2.Name} with a distance of {distance} meters");
        }

    }












}


