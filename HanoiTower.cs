using System;
using System.Collections.Generic;
using System.Text;

namespace TowerOfHanoi
{
    public static class HanoiTower
    {
        private static uint[][] output = null;
        private static uint moveCount = 0;

        /// <summary>
        /// Solves Tower of Hanoi.
        /// </summary>
        /// <param name="diskCount">Number of disks, cannot be zero.</param>
        /// <param name="originPole">The index of the pole where the disks start. [0,1,2]</param>
        /// <param name="destinationPole">The index of the pole where the disk should end up. [0,1,2]</param>
        /// <returns>2D int array that represents the steps to solve the problem. output[movementIndex, p],
        /// p = 0 is the origin pole and p = 1 is the destination pole.</returns>
        public static uint[][] Solve(uint diskCount, uint originPole = 0, uint destinationPole = 2)
        {
            if (diskCount == 0)
                throw new ArgumentOutOfRangeException("diskCount");
            else if (originPole > 2)
                throw new ArgumentOutOfRangeException("originPole");
            else if (destinationPole > 2)
                throw new ArgumentOutOfRangeException("destinationPole");

            uint steps = 1;
            
            //Solves in (2^diskCount) - 1
            for (uint i = 0; i < diskCount; i++)
                steps *= 2;

            steps--;

            output = new uint[steps][];

            InternalSolve(diskCount, originPole, destinationPole);

            return Finish();
        }

        private static void InternalSolve(uint diskCount, uint originPole, uint destinationPole)
        {
            if (originPole == destinationPole)
                return;

            uint remainingPole = 3 - originPole - destinationPole;

            if (diskCount != 1)
                InternalSolve(diskCount - 1, originPole, remainingPole);

            AddMove(originPole, destinationPole);

            if (diskCount != 1)
                InternalSolve(diskCount - 1, remainingPole, destinationPole);
        }

        private static void AddMove(uint originPole, uint destinationPole)
        {
            output[moveCount++] = new uint[] { originPole, destinationPole };
        }

        private static uint[][] Finish()
        {
            uint[][] result = output;
            output = null;
            moveCount = 0;
            return result;
        }
    }
}
