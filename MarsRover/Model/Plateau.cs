﻿using MarsRover.Data;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using LanguageExt;
using static LanguageExt.Prelude;

namespace MarsRover.Model;

internal class Plateau(PlateauSize plateauSize)
{
    public PlateauSize PlateauSize { get; set; } = plateauSize;

    public int MaxX { get => PlateauSize.X; }
    public int MaxY { get => PlateauSize.Y; }

    public static Plateau FromInts(int x, int y)
    {
        var size = new PlateauSize(x, y);
        return new Plateau(size);
    }

    public static Either<string, Plateau> From(int x, int y)
    {
        return Right(FromInts(x, y));
    }

    public static Either<string, Plateau> FromPlateauSize(PlateauSize plateauSize)
    {
        return Right(new Plateau(plateauSize));
    }

}
