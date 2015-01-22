using Castle.MicroKernel.Registration;
using Castle.Windsor;
using EndlessLobster.Domain;
using EndlessLobster.Domain.Repository;
using EndlessLobster.Domain.Repository.RepositoryInstallers;

namespace EndlessLobster.Application
{
    public class Bootstrapper
    {

        public IWindsorContainer Container { get; private set; }

        public void Init(IWindsorContainer container)
        {
            container.Install(new ArtistInstaller());
            Container = container;
        }
    }
}