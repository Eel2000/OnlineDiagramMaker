
using Microsoft.AspNetCore.Components;

namespace OnlineDiagramMaker.Client.Pages
{
    public partial class Index : ComponentBase
    {
        [Inject]
        NavigationManager? NavigationManager { get; set; }

        private void StartOverAsync()
        {
            //navigat to a new page
            NavigationManager!.NavigateTo("/work-space");
        }
    }
}