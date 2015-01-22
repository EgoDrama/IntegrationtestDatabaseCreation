using Castle.MicroKernel.Registration;
using Castle.Windsor;
using EndlessLobster.Domain;
using EndlessLobster.Domain.Repository;
using EndlessLobster.Domain.Repository.RepositoryInstallers;

namespace EndlessLobster.Application
{
    public class Bootstrapper
    {
        private readonly IDatabaseFactory _databaseFactor;

        public Bootstrapper(IDatabaseFactory databaseFactor)
        {
            _databaseFactor = databaseFactor;
        }

        public IWindsorContainer Container { get; private set; }

        public void Init(IWindsorContainer container)
        {
            container.Install(new ArtistInstaller(_databaseFactor));
            Container = container;
        }
    }
}