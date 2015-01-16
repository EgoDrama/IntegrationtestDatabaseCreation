using Castle.Windsor;
using EndlessLobster.Domain.Repository;
using EndlessLobster.Domain.Repository.RepositoryInstallers;

namespace EndlessLobster.Application
{
    public class Bootstrapper
    {
        private readonly IDatabaseFactory _databaseFactory;
        
        public Bootstrapper(IDatabaseFactory databaseFactory)
        {
            _databaseFactory = databaseFactory;
        }

        public IWindsorContainer Container { get; private set; }

        public void Init(IWindsorContainer container)
        {
            var artistInstaller = new ArtistInstaller(_databaseFactory);
            container.Install(artistInstaller);
            Container = container;
        }
    }
}