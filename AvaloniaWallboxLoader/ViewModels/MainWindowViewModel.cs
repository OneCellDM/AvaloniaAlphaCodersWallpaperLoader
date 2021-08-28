using System;
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
        private int _CurrentPage = 1;
        private int _PageCount = 0;
        private string _Title;
        
        private int _SelectedMenuItem = -1;
        private bool _CategoriesIsVisible = false;

        private bool _ControlIsVisible = true;
        

        private List<int> Pages;
        
        public bool CategoriesIsVisible 
        {
            get => _CategoriesIsVisible ;
            set => this.RaiseAndSetIfChanged(ref _CategoriesIsVisible, value);
        }
        
        public bool ControlIsVisible
        {
            get => _ControlIsVisible ;
            set => this.RaiseAndSetIfChanged(ref _ControlIsVisible, value);
        }

        
        public int CurrentPage
        {
            get => _CurrentPage;
            set => this.RaiseAndSetIfChanged(ref _CurrentPage, value);
        }

        public int PageCount
        {
            get => _PageCount;
            set => this.RaiseAndSetIfChanged(ref _PageCount, value);
        }

        public string Title
        {
            get => _Title;
            set => this.RaiseAndSetIfChanged(ref _Title, value);
        }

        public IReactiveCommand CloseCategoriesCommand
        {
            get;
            set;
        }
        
        
      
        public  string Category { get; set; }

       
        public ObservableCollection<WallBox.DataModel.CategoryModel> Categories { get; set; } = new();
        public ObservableCollection<ImageModel> ImageModels { get; set; } = new();

        public int SelectedMenuItem
        {
            get => _SelectedMenuItem;
            set
            {
                if (value > -1)
                {
                    this.RaiseAndSetIfChanged(ref _SelectedMenuItem, value);
                    switch (value)
                    {
                        case 0:
                        {
                            ControlIsVisible = true;
                            Title = "Все";
                            LoadData("/",CurrentPage);
                            break;
                            
                        }
                        case 2:
                        {
                            ControlIsVisible = false;
                            CategoriesIsVisible = true;
                            SelectedMenuItem = -1;
                            break;
                            
                        }
                    }
                    
                }
            }
        }

        public MainWindowViewModel()
        {
            GetCategories();
            CloseCategoriesCommand = ReactiveCommand.Create((object ob) => CategoriesIsVisible = false);
        }

        private void GetCategories()
        {
            var resAwaiter = WallBoxApi.GetCategoriesAsync().GetAwaiter();
            resAwaiter.OnCompleted(() =>
            {
                var res = resAwaiter.GetResult();
                res.ForEach(x => Categories.Add(x));
                SelectedMenuItem = 0;
            });
        }

        private void LoadData(string Category, int index)
        {
            var CategoryGetAwaiter = WallBoxApi.GetCategoryPageData(Category,CurrentPage).GetAwaiter();
            CategoryGetAwaiter.OnCompleted(() =>
            {
                var items = CategoryGetAwaiter.GetResult().Item1;
                Pages = CategoryGetAwaiter.GetResult().Item2;
                PageCount = Pages.Last();
                items.ForEach(x => ImageModels.Add(new(x)));
                Task.Run(() => ImageModels.ToList().ForEach(x => x.LoadBItmap()));
            });
        }
    }
}