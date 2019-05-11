using MostValuableVrameMoerk;
using System.Threading.Tasks;

namespace MVVMaker
{
    public class MainWindowViewModel : ViewModelBase
    {
        public string Test => "TTT";
        public AsyncProperty<string> Test1 { get; set; } = new AsyncProperty<string>("Hallo");
        public AsyncProperty<int> Test2 { get; set; } = new AsyncProperty<int>(222);

        public MainWindowViewModel()
        {
            this.SetPropertyChangeForAll();
        }

        public async Task Click1()
        {
            await Task.Delay(3000);
        }
        public async Task Click2()
        {
            await Task.Delay(3000);
            await Test1.SetAsync("Thehehest");
        }
        public async Task Click3()
        {
            await Task.Delay(3000);
            await Test2.SetAsync(123);
        }
    }
}
