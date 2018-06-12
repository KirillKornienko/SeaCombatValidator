using System;

namespace SeaCombatValidator
{
    class Program
    {
        static void Main(string[] args)
        {
            int[,] field = {{1, 0, 0, 0, 0, 1, 1, 0, 0, 0},
                            {1, 0, 1, 0, 0, 0, 0, 0, 1, 0},
                            {1, 0, 1, 0, 1, 1, 1, 0, 1, 0},
                            {1, 0, 0, 0, 0, 0, 0, 0, 0, 0},
                            {0, 0, 0, 0, 0, 0, 0, 0, 1, 0},
                            {0, 0, 0, 0, 1, 1, 1, 0, 0, 0},
                            {0, 0, 0, 0, 0, 0, 0, 0, 1, 0},
                            {0, 0, 0, 1, 0, 0, 0, 0, 0, 0},
                            {0, 0, 0, 0, 0, 0, 0, 1, 0, 0},
                            {0, 0, 0, 0, 0, 0, 0, 0, 0, 0} };

            Console.WriteLine(
                new SeaCombatValidator().Validator(field));
        }
    }
    class SeaCombatValidator
    {
        /// <summary>
        /// Запускает валидацию игрового поля, возвращает результат валидации
        /// </summary>
        /// <param name="field">Игровое поле</param>
        public bool Validator(int[,] field)
        {
            game_field = new int[10, 10];
            Array.Copy(field, game_field, field.Length);

            for (int x = 0; x < field.GetLength(0); x++)
            {
                for (int y = 0; y < field.GetLength(1); y++)
                {
                    if (field[y, x] == 1)
                    {
                        if (GetCell(x + 1, y) == 1)
                        {
                            if (!HorizontalValidator(y, x, x + GetEndPosition(x, y, field, false) - 1))
                                return false;
                        }
                        else if (GetCell(x, y + 1) == 1)
                        {
                            if (!VerticalValidator(x, y, y + GetEndPosition(x, y, field, true) - 1))
                                return false;
                        }
                        else
                        {
                            if (!HorizontalValidator(y, x, x))
                                return false;
                        }
                    }
                }
            }

            if (number_ships[0] != 0 ||
                number_ships[1] != 0 ||
                number_ships[2] != 0 ||
                number_ships[3] != 0)
                return false;

            return true;
        }

        int GetEndPosition(int x, int y, int[,] field, bool vertical)
        {
            int i = 1;
            field[y, x] = 0;

            if (GetCell(x + 1, y) == 1 && !vertical)
            {
                i += GetEndPosition(x + 1, y, field, vertical);
            }
            else if (GetCell(x, y + 1) == 1 && vertical)
            {
                i += GetEndPosition(x, y + 1, field, vertical);
            }

            return i;
        }

        int[] number_ships = new int[] { 4, 3, 2, 1 };
        int[,] game_field;

        bool HorizontalValidator(int y, int xStart, int xEnd)
        {
            int x_length = xEnd - xStart;

            if (x_length > 3)
                return false;

            if (--number_ships[x_length] < 0) return false;

            for (int x = xStart; x <= xEnd; x++)
            {
                if (x == xStart)
                {
                    if (GetCell(x - 1, y - 1) == 1 || GetCell(x - 1, y) == 1 || GetCell(x - 1, y + 1) == 1)
                        return false;
                }

                if (GetCell(x, y - 1) == 1 || GetCell(x, y + 1) == 1)
                    return false;

                if (x == xEnd)
                {
                    if (GetCell(x + 1, y - 1) == 1 || GetCell(x + 1, y) == 1 || GetCell(x + 1, y + 1) == 1)
                        return false;
                }
            }

            return true;
        }

        bool VerticalValidator(int x, int yStart, int yEnd)
        {
            int y_length = yEnd - yStart;

            if (y_length > 3)
                return false;

            if (--number_ships[y_length] < 0) return false;

            for (int y = yStart; y <= yEnd; y++)
            {
                if (y == yStart)
                {
                    if (GetCell(x - 1, y - 1) == 1 || GetCell(x, y - 1) == 1 || GetCell(x + 1, y - 1) == 1)
                        return false;
                }

                if (GetCell(x - 1, y) == 1 || GetCell(x + 1, y) == 1)
                    return false;

                if (y == yEnd)
                {
                    if (GetCell(x - 1, y + 1) == 1 || GetCell(x, y + 1) == 1 || GetCell(x + 1, y + 1) == 1)
                        return false;
                }
            }

            return true;
        }

        int GetCell(int x, int y)
        {
            if (x < 0 || y < 0 || x >= game_field.GetLength(0) || y >= game_field.GetLength(1))
                return 0;

            return game_field[y, x];
        }
    }
}
