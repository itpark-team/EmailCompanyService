namespace ConsoleAppClientSide;

public static class DataStorage
{
    private static Dictionary<string, object> _data = new Dictionary<string, object>();

    public static void AddOrUpdate(string key, object value)
    {
        _data[key] = value;
    }

    public static void Delete(string key)
    {
        _data.Remove(key);
    }

    public static void Clear()
    {
        _data.Clear();
    }

    public static object Get(string key)
    {
        return _data[key];
    }

    public static T GetWithType<T>(string key)
    {
        return (T)_data[key];
    }

    public static bool ContainsKey(string key)
    {
        return _data.ContainsKey(key);
    }
}