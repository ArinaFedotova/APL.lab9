// пользовательский интерфейс и работа с фасадом

using Core;
using DatabaseContext;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;

namespace Presentation
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var facade = new Facade();
            facade.Run();
        }
    } 
}