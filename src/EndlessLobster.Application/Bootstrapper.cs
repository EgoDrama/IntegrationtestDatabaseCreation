using Castle.Windsor;
using EndlessLobster.Domain.Repository.RepositoryInstallers;

namespace EndlessLobster.Application
{
    public class Bootstrapper
    {
        public void Init(IWindsorContainer container)
        {
            container.Install(new ArtistInstaller());
        }
    }
}