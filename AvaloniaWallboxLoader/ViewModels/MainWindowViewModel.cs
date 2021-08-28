using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Net.Mime;
using System.Threading.Tasks;
using AvaloniaWallboxLoader.Models;
using WallBox;
using ReactiveUI;

namespace AvaloniaWallboxLoader.ViewModels
{
    public class MainWindowViewModel : ViewModelBase
    {
        private int _SelectedCategory = -1;
        public ObservableCollection<WallBox.DataModel.CategoryModel> Categories { get; set; } = new();
        public ObservableCollection<ImageModel> ImageModels{ get; set; } = new();

        public int SelectedCategory
        {
            get => _SelectedCategory;
            set
            {
                if (value > -1)
                {
                    this.RaiseAndSetIfChanged(ref _SelectedCategory, value);
                    LoadData(value);
                }
            }
        }
        
        public MainWindowViewModel() => GetCategories();
        private void GetCategories()
        {
            var resAwaiter = WallBoxApi.GetCategoriesAsync().GetAwaiter();
            resAwaiter.OnCompleted(() =>
            {
                var res = resAwaiter.GetResult();
                res.ForEach(x => Categories.Add(x));
                SelectedCategory = 0;
            });
        }

        private void LoadData(int index)
        {
            var CategoryGetAwaiter = WallBoxApi.GetCategoryPageData(Categories[index].Url).GetAwaiter();
            CategoryGetAwaiter.OnCompleted(() =>
            {
                var items = CategoryGetAwaiter.GetResult().Item1;
                
                items.ForEach(x=>ImageModels.Add(new(x)));
                Task.Run(() =>ImageModels.ToList().ForEach(x => x.LoadBItmap()));
            });
        }
    }
}