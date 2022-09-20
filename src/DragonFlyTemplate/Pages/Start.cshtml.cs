using DragonFly.Storage;
using DragonFlyTemplate.Models;

namespace DragonFlyTemplate.Pages;

public class StartPage : BasePageModel
{
    public StartPage(IContentStorage contentStorage)
    {
        ContentStorage = contentStorage;

        PageTitle = "Start";
    }

    public IContentStorage ContentStorage { get; }

    public StandardPageModel Result { get; set; }

    //public async Task OnGetAsync()
    //{
    //    var query = new ContentItemQuery() {  Top = 1 };
        
    //    var result = await ContentStorage.QueryAsync<StandardPageModel>(query);

    //    Result = result.Items[0];

    //}
}
