using MarsRover.Data;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MarsRover.Model;

internal class Plateau(PlateauSize plateauSize)
{
    public PlateauSize PlateauSize { get; set; } = plateauSize;

    public static Plateau From(int x, int y)
    {
        var size = new PlateauSize(x, y);
        return new Plateau(size);
    }

}
