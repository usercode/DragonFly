using DragonFly.Content;
using DragonFly.Storage;

namespace DragonFlyTemplate.Startup;

public class DataSeeding
{
    public DataSeeding(IDataStorage dataStorage)
    {
        DataStorage = dataStorage;
    }

    private IDataStorage DataStorage { get; }

    public async Task StartAsync()
    {
        
    }

}
