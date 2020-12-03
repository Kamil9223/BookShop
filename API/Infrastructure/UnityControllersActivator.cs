using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Controllers;
using Unity;

namespace API.Infrastructure
{
    public class UnityControllersActivator : IControllerActivator
    {
        private readonly IUnityContainer unityContainer;

        public UnityControllersActivator(IUnityContainer unityContainer)
        {
            this.unityContainer = unityContainer;
        }

        public object Create(ControllerContext context)
        {
            return unityContainer.Resolve(context.ActionDescriptor.ControllerTypeInfo.AsType());
        }

        public void Release(ControllerContext context, object controller)
        {
            
        }
    }
}
