using BookService.Repositories;
using BookService.Services.Interfaces;
using Core.IRepositories;
using Unity;

namespace BookService.Infrastructure
{
    public class BookServiceStartup
    {
        public static void RegisterServices(IUnityContainer unityContainer)
        {
            unityContainer.RegisterType<IBookRepository, BookRepository>();
            unityContainer.RegisterType<ICategoryRepository, CategoryRepository>();
            unityContainer.RegisterType<IBookService, Services.Implementations.BookService>();
        }
    }
}
