using BlazorBootstrap;
using Microsoft.AspNetCore.Components;
using Microsoft.Extensions.Options;
using Models;
using MudBlazor;
using NBPApi;
using System.Globalization;

namespace NBP.Pages
{
    public partial class Chart
    {

        [Inject] INBPClient _nbpClient { get; set; }
        string type ;
        private NBPTable NBPTable = new NBPTable();
        private LineChart lineChart = default!;
        private LineChartOptions lineChartOptions = default!;
        private ChartData chartData = default!;
        private DateRange _dateRange = new DateRange(DateTime.Now.Date.AddMonths(-1), DateTime.Now.Date);
        private MudDateRangePicker _picker;

        //protected override async Task OnInitializedAsync()
        //{
        //    await SelectedDate(_dateRange);
        //}
        public async Task GetTable()
        {
            NBPTable = await _nbpClient.GetCurrency(type);
        }

        private async Task Change(string selectedType)
        {
            type = selectedType;
            await GetTable();
            if (NBPTable.Table != null && code != null)
            {
                await FillChart(code);
            }
        }

        string code;
        private async Task ChangeCur(string selectedCode)
        {
            code = selectedCode;
            await FillChart(code);
        }
        private async Task FillChart(string code)
        {
            var res = await _nbpClient.GetLastCurrencies(type, code, _dateRange.Start.Value, _dateRange.End.Value);


            List<double> bid = new List<double>();
            List<double> ask = new List<double>();
            List<double> mid = new List<double>();
            List<string> dates = new List<string>();

            foreach (var item in res)
            {
                bid.Add(item.Bid);
                ask.Add(item.Ask);
                mid.Add(item.Mid);
                dates.Add(item.EffectiveDate.ToString("yyyy-MM-dd"));
            }

            var colors = ColorBuilder.CategoricalTwelveColors;
            var datasets = new List<IChartDataset>();

            if (type == "C")
            {

                var dataset1 = new LineChartDataset
                {
                    Label = $"{code} BID",
                    Data = bid,
                    BackgroundColor = new List<string> { colors[0] },
                    BorderColor = new List<string> { colors[0] },
                    BorderWidth = new List<double> { 2 },
                    HoverBorderWidth = new List<double> { 4 },
                    PointBackgroundColor = new List<string> { colors[0] },
                    //PointRadius = new List<int> { 0 }, // hide points
                    PointHoverRadius = new List<int> { 4 }
                };
                datasets.Add(dataset1);

                var dataset2 = new LineChartDataset
                {
                    Label = $"{code} ASK",
                    Data = ask,
                    BackgroundColor = new List<string> { colors[1] },
                    BorderColor = new List<string> { colors[1] },
                    BorderWidth = new List<double> { 2 },
                    HoverBorderWidth = new List<double> { 4 },
                    PointBackgroundColor = new List<string> { colors[1] },
                    //PointRadius = new List<int> { 0 }, // hide points
                    PointHoverRadius = new List<int> { 4 }
                };
                datasets.Add(dataset2);
            }
            else
            {
                var dataset1 = new LineChartDataset
                {
                    Label = code,
                    Data = mid,
                    BackgroundColor = new List<string> { colors[0] },
                    BorderColor = new List<string> { colors[0] },
                    BorderWidth = new List<double> { 2 },
                    HoverBorderWidth = new List<double> { 4 },
                    PointBackgroundColor = new List<string> { colors[0] },
                    //PointRadius = new List<int> { 0 }, // hide points
                    PointHoverRadius = new List<int> { 4 }
                };
                datasets.Add(dataset1);
            }

            chartData = new ChartData
            {
                Labels = dates,
                Datasets = datasets
            };

            lineChartOptions = new();
            lineChartOptions.Responsive = true;
            lineChartOptions.Interaction = new Interaction { Mode = InteractionMode.Index };

            lineChartOptions.Scales.Y.Title.Text = "Price";
            lineChartOptions.Scales.Y.Title.Display = true;

            await lineChart.UpdateAsync(chartData, lineChartOptions);

            StateHasChanged();
        }


        private async Task SelectedDate(string value)
        {
            var a = value.Split(";");
            _dateRange.Start =DateTime.Parse( a[0].Substring(1));
            _dateRange.End =DateTime.Parse( a[1].Substring(0, a[1].Length-1));
            if (NBPTable.Table != null && code != null)
            {
                await FillChart(code);
            }
        }

        private async Task AddCur()
        {

        }
    }
}
