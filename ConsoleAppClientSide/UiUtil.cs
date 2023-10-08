namespace ConsoleAppClientSide;

public static class UiUtil
{
    public static int InputInt(string msg)
    {
        bool inputResult;
        int number;

        do
        {
            Console.Write(msg);

            inputResult = int.TryParse(Console.ReadLine(), out number);
        } while (!inputResult);

        return number;
    }

    public static int InputIntWithBounds(string msg, int min, int max)
    {
        bool inputResult;
        int number;

        do
        {
            Console.Write(msg);

            inputResult = int.TryParse(Console.ReadLine(), out number);

            if (inputResult && (number < min || number > max))
            {
                inputResult = false;
            }
        } while (!inputResult);

        return number;
    }

    public static string InputString(string msg)
    {
        Console.Write(msg);
        return Console.ReadLine();
    }

    public static void PrintlnString(string msg)
    {
        Console.WriteLine(msg);
    }

    public static void ClearConsole()
    {
        Console.Clear();
    }

    public static void WaitPressAnyKey()
    {
        Console.ReadKey();
    }
}