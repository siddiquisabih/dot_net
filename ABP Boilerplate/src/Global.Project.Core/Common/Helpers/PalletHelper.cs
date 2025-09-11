using System;
using System.Collections.Generic;

namespace Global.Project.Common.Helpers
{

    public static class PalletHelper
    {
        private static readonly Dictionary<string, double> UnitToInch = new Dictionary<string, double>(StringComparer.OrdinalIgnoreCase)
        {
            { "mm", 0.0393701 },
            { "cm", 0.393701 },
            { "m", 39.3701 },
            { "in", 1 },
            { "ft", 12 }
        };
        public static int CalculatePallets(
            double masterBoxLength,
            double masterBoxWidth,
            double masterBoxHeight,
            string sizeUnit,
            double palletLengthInches,
            double palletWidthInches,
            double shelfHeightInches,
            int totalBoxes)
        {
            double boxLengthInches = ConvertToInches(masterBoxLength, sizeUnit);
            double boxWidthInches = ConvertToInches(masterBoxWidth, sizeUnit);
            double boxHeightInches = ConvertToInches(masterBoxHeight, sizeUnit);

            if (boxLengthInches <= 0 || boxWidthInches <= 0 || boxHeightInches <= 0)
                throw new ArgumentException("Master box dimensions must be greater than zero.");

            if (palletLengthInches <= 0 || palletWidthInches <= 0)
                throw new ArgumentException("Pallet dimensions must be greater than zero.");

            if (shelfHeightInches <= 0)
                throw new ArgumentException("Shelf height must be greater than zero.");

            if (totalBoxes <= 0)
                throw new ArgumentException("Total boxes must be greater than zero.");

            if (boxLengthInches > palletLengthInches || boxWidthInches > palletWidthInches)
            {
                int palletsPerBox = (int)Math.Ceiling(boxLengthInches / palletLengthInches) *
                                    (int)Math.Ceiling(boxWidthInches / palletWidthInches);

                int layersPerFootprint = (int)Math.Floor(shelfHeightInches / boxHeightInches);
                if (layersPerFootprint < 1) layersPerFootprint = 1;

                int boxesPerFootprint = layersPerFootprint;

                int footprintsNeeded = (int)Math.Ceiling((double)totalBoxes / boxesPerFootprint);

                return footprintsNeeded * palletsPerBox;
            }

            int boxesLengthwise = (int)(palletLengthInches / boxLengthInches);
            int boxesWidthwise = (int)(palletWidthInches / boxWidthInches);
            int boxesPerLayer = boxesLengthwise * boxesWidthwise;

            if (boxesPerLayer == 0)
                throw new InvalidOperationException("Box is too large to fit on the pallet.");

            int layersPerPallet = (int)(shelfHeightInches / boxHeightInches);
            if (layersPerPallet < 1) layersPerPallet = 1;

            int boxesPerPallet = boxesPerLayer * layersPerPallet;

            return (int)Math.Ceiling((double)totalBoxes / boxesPerPallet);
        }





        private static double ConvertToInches(double value, string unit)
        {
            if (!UnitToInch.ContainsKey(unit))
                throw new ArgumentException($"Unsupported unit: {unit}");

            return value * UnitToInch[unit];
        }
    }
}