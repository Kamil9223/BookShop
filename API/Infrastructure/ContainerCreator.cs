using Unity;

namespace API.Infrastructure
{
    public class ContainerCreator
    {
        private static IUnityContainer mainContainer;

        private static object syncLock = new object();

        public static IUnityContainer Instance()
        {
            lock (syncLock)
            {
                if (mainContainer == null)
                    mainContainer = new UnityContainer();

                return mainContainer;
            }
        }
    }
}
