using System.Runtime.InteropServices;

namespace HanoiTowerSolver;

[StructLayout(LayoutKind.Sequential)]
public readonly struct Step
{
    public readonly uint Source;
    public readonly uint Destination;

    public Step(uint source, uint destination)
    {
        Source = source;
        Destination = destination;
    }
}
