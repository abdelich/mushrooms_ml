using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using MushroomsCRUD.Data;
using SQLitePCL;
using System.IO;
using System.Windows;

namespace MushroomsCRUD
{
    /// <summary>
    /// Interaction logic for App.xaml
    /// </summary>
    public partial class App : Application
    {
        public App()
        {
            DatabaseTest.TestConnection();
        }
    }
}
