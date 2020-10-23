using BookService.Repositories;
using BookService.Repositories.AdminRepositories;
using BookService.Services.Interfaces;
using Core.AdminRepositories;
using Core.Repositories;
using Unity;

namespace BookService.Infrastructure
{
    public class BookServiceStartup
    {
        public static void RegisterServices(IUnityContainer unityContainer)
        {
            unityContainer.RegisterType<IBookRepository, BookRepository>();
            unityContainer.RegisterType<IAdminBookRepository, AdminBookRepository>();
            unityContainer.RegisterType<ICategoryRepository, CategoryRepository>();
            unityContainer.RegisterType<IBookService, Services.Implementations.BookService>();
        }
    }
}
