using Microsoft.AspNetCore.Components;
using Models;
using NBP.Data;
using NBPApi;
using static MudBlazor.CategoryTypes;

namespace NBP.Pages
{
    public partial class Index
    {
        [Inject] INBPClient _nbpClient { get; set; }
        [Inject] INBPRepository _repository { get; set; }
        string type;
        bool load;
        private NBPTable NBPTable = new NBPTable();
        protected override async Task OnAfterRenderAsync(bool firstRender)
        {
            if (firstRender)
            {
                //await GetTable();
                //StateHasChanged();
            }
        }
        public async Task GetTable()
        {
            load = true;
            NBPTable = await _nbpClient.GetCurrency(type);
            await _repository.SetNBPTable(NBPTable);
            load = false;
        }

        private async Task Change(string selectedType)
        {
            type = selectedType;
            await GetTable();
        }

        private string searchString = "";
        private Func<Rate, bool> _quickFilter => x =>
        {
            if (string.IsNullOrWhiteSpace(searchString))
                return true;
            if (x.Currency.Contains(searchString, StringComparison.OrdinalIgnoreCase))
                return true;
            if (x.Code.Contains(searchString, StringComparison.OrdinalIgnoreCase))
                return true;
            if (x.Mid.Equals(searchString))
                return true;

            return false;
        };
    }
}
