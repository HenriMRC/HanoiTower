using HanoiTowerSolver;

namespace HanoiTowerTest;

public class Tests
{
    private const int DISK_COUNT = 5;
    private const int POLES_COUNT = 3;

    [Test]
    public void Test()
    {
        uint[] firstPole = new uint[DISK_COUNT];
        for(uint i = 0; i < DISK_COUNT; )
            firstPole[i] = ++i;

        uint[][] board = new uint[POLES_COUNT][];
        board[0] = firstPole;
        for (int i = 1; i < POLES_COUNT; i++)
            board[i] = new uint[DISK_COUNT];

        Step[] steps = HanoiTower.Solve(DISK_COUNT);
        for (int s = 0; s < steps.Length; s++)
        {
            Step step = steps[s];

            uint number = 0;
            uint[] sourcePole = board[step.Source];
            for (int i = 0; i < sourcePole.Length; i++) 
                if(sourcePole[i] != 0)
                {
                    number = sourcePole[i];
                    sourcePole[i] = 0;
                    break;
                }
            Assert.That(number, Is.Not.EqualTo(0));

            uint[] targetPole = board[step.Destination];
            for (int i = targetPole.Length - 1; i > -1; i--)
                if (targetPole[i] == 0)
                {
                    targetPole[i] = number;
                    number = 0;
                    break;
                }
            Assert.That(number, Is.EqualTo(0));

            foreach (uint[] pole in board)
            {
                uint previous = 0;
                foreach (uint current in pole) 
                {
                    if (previous != 0)
                        Assert.That(previous, Is.LessThan(current));

                    previous = current;
                }
            }
        }
    }

    [Test, Repeat(1_000_000)]
    public void Time()
    {
        Step[] steps = HanoiTower.Solve(DISK_COUNT);
    }
}