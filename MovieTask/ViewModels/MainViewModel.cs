using MovieTask.Commands;
using MovieTask.Helpers;
using MovieTask.Models;
using MovieTask.Services;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;

namespace MovieTask.ViewModels
{
    public class MainViewModel:BaseViewModel
    {
        private string recentMovie;

        public string RecentMovie
        {
            get { return recentMovie; }
            set { recentMovie = value;OnPropertyChanged(); }
        }

        private ObservableCollection<Movie> allMovies;

        public ObservableCollection<Movie> AllMovies
        {
            get { return allMovies; }
            set { allMovies = value; OnPropertyChanged(); }
        }
        private PageNo selectedPageNo;

        public PageNo SelectedPageNo
        {
            get { return selectedPageNo; }
            set { selectedPageNo = value; }
        }
        private ObservableCollection<PageNo> allPages;

        public ObservableCollection<PageNo> AllPages
        {
            get { return allPages; }
            set { allPages = value; OnPropertyChanged(); }
        }
        public RelayCommand SelectPageCommand { get; set; }

        List<Movie> result = null;
        public async void LoadData()
        {
            var service = new MovieApiService();


            result = await MovieApiService.GetMoviesAsync(RecentMovie);
            AllPages = new ObservableCollection<PageNo>();
            var pageSize = 2;
            var pageCount = decimal.Parse(result.Count().ToString()) / pageSize;
            var count = Math.Ceiling(pageCount);
            for (int i = 0; i < count; i++)
            {
                AllPages.Add(new PageNo
                {
                    No = i + 1
                });
            }

            AllMovies = new ObservableCollection<Movie>(result.Skip(0).Take(pageSize));
        }
        public RelayCommand SearchCommand { get; set; }
        public MainViewModel()
        {
            SearchCommand = new RelayCommand((obj) =>
            {
                LoadData();
            });


            SelectPageCommand = new RelayCommand((obj) =>
            {
                var no = SelectedPageNo.No;
                AllMovies = new ObservableCollection<Movie>(result.Skip((no - 1) * 2).Take(2));
            });
        }
    }
}
