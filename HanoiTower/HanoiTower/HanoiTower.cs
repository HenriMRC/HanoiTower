namespace HanoiTowerSolver;

public static class HanoiTower
{
    public const int POLES_COUNT = 3;

    /// <summary>
    /// Solves Tower of Hanoi.
    /// </summary>
    /// <param name="diskCount">Number of disks, cannot be zero.</param>
    /// <param name="originPole">The index of the pole where the disks start. [0,1,2]</param>
    /// <param name="destinationPole">The index of the pole where the disk should end up. [0,1,2]</param>
    /// <returns>2D int array that represents the steps to solve the problem. output[movementIndex, p],
    /// p = 0 is the origin pole and p = 1 is the destination pole.</returns>
    public static Step[] Solve(uint diskCount, uint originPole = 0, uint destinationPole = 2)
    {
        if (diskCount == 0)
            throw new ArgumentOutOfRangeException(nameof(diskCount));
        else if (originPole >= POLES_COUNT)
            throw new ArgumentOutOfRangeException(nameof(originPole));
        else if (destinationPole >= POLES_COUNT)
            throw new ArgumentOutOfRangeException(nameof(destinationPole));

        uint steps = 1;

        //Solves in (2^diskCount) - 1
        for (uint i = 0; i < diskCount; i++)
            steps *= 2;
        steps--;

        Step[] output = new Step[steps];
        _ = InternalSolve(diskCount, originPole, destinationPole, output, 0);
        return output;
    }

    private static int InternalSolve(uint diskCount, uint originPole, uint destinationPole, Step[] result, int index)
    {
        if (originPole != destinationPole)
        {
            uint remainingPole = POLES_COUNT - originPole - destinationPole;

            if (diskCount != 1)
                index = InternalSolve(diskCount - 1, originPole, remainingPole, result, index);

            result[index++] = new Step(originPole, destinationPole);

            if (diskCount != 1)
                index = InternalSolve(diskCount - 1, remainingPole, destinationPole, result, index);
        }

        return index;
    }
}