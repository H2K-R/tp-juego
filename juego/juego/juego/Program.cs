using System;

namespace juego
{
    class Program
    {
        static void Main()
        {
            const int gridSize = 10;
            int numTreasures = 3;
            const int numSonars = 16;

            Random random = new Random();

            (int, int)[] treasurePositions = new (int, int)[numTreasures];
            for (int i = 0; i < numTreasures; i++)
            {
                treasurePositions[i] = (random.Next(0, gridSize), random.Next(0, gridSize));
            }

            Console.WriteLine("¡Bienvenido al juego de búsqueda de tesoros!");
            Console.WriteLine($"Hay {numTreasures} cofres de tesoros escondidos en un área de {gridSize}x{gridSize}.");

            int sonarUsed = 0;
            while (sonarUsed < numSonars)
            {
                DisplayOcean(gridSize, treasurePositions);

                Console.WriteLine($"\nDispositivo de sonar utilizado: {sonarUsed + 1}/{numSonars}");
                Console.Write("Ingrese la coordenada X del sonar (0-9): ");
                int x = Convert.ToInt32(Console.ReadLine());

                Console.Write("Ingrese la coordenada Y del sonar (0-9): ");
                int y = Convert.ToInt32(Console.ReadLine());

                sonarUsed++;

                int closestDistance = int.MaxValue;
                foreach ((int tx, int ty) in treasurePositions)
                {
                    int distance = Math.Abs(tx - x) + Math.Abs(ty - y);
                    closestDistance = Math.Min(closestDistance, distance);
                }

                Console.WriteLine($"El dispositivo de sonar indica que el tesoro más cercano está a una distancia de {closestDistance} unidades.\n");

                if (closestDistance == 0)
                {
                    Console.WriteLine("¡Felicidades! Has encontrado un cofre de tesoro.");
                    numTreasures--;
                    if (numTreasures == 0)
                    {
                        Console.WriteLine("¡Has encontrado todos los cofres de tesoros! ¡Felicidades!");
                        break;
                    }
                }
                else if (sonarUsed == numSonars)
                {
                    Console.WriteLine("Has usado todos tus dispositivos de sonar. ¡Game Over!");
                    break;
                }
            }

            Console.WriteLine("\n¡Gracias por jugar!");
        }

        static void DisplayOcean(int gridSize, (int, int)[] treasurePositions)
        {
            Console.Clear();
            Console.WriteLine("    0 1 2 3 4 5 6 7 8 9");
            Console.WriteLine("   ---------------------");
            for (int y = 0; y < gridSize; y++)
            {
                Console.Write($"{y} | ");
                for (int x = 0; x < gridSize; x++)
                {
                    if (Array.Exists(treasurePositions, pos => pos.Item1 == x && pos.Item2 == y))
                    {
                        Console.Write("T ");
                    }
                    else
                    {
                        Console.Write((y + x) % 2 == 0 ? "~ " : "` ");
                    }
                }
                Console.WriteLine();
            }
        }
    }
}
