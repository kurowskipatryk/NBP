using Microsoft.AspNetCore.Components;
using NBP.Data;
using NBPApi;

namespace NBP.Pages
{
    public partial class Index
    {
        [Inject] INBPClient _nbpClient { get; set; }
        [Inject] INBPRepository _repository { get; set; }
        string type = "A";
        protected override async Task OnAfterRenderAsync(bool firstRender)
        {
            if (firstRender)
            {

            }
        }

        public async Task GetTable()
        {

            var values = await _nbpClient.GetCurrency(type);

            await _repository.SetNBPTable(values);
        }

        private async Task Change(string selectedType)
        {
            type = selectedType;
            await GetTable();
        }
    }
}
