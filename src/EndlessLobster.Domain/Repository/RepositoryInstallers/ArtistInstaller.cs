using Castle.MicroKernel.Registration;
using Castle.MicroKernel.SubSystems.Configuration;
using Castle.Windsor;

namespace EndlessLobster.Domain.Repository.RepositoryInstallers
{
    public class ArtistInstaller : IWindsorInstaller
    {
        private readonly IDatabaseFactory _databaseFactory;

        public ArtistInstaller(IDatabaseFactory databaseFactory)
        {
            _databaseFactory = databaseFactory;
        }

        public void Install(IWindsorContainer container, IConfigurationStore store)
        {
            container.Register(
                Component.For<IDatabaseFactory>()
                    .UsingFactoryMethod(() => _databaseFactory)
                    .LifestyleTransient(),

                Component.For<IRepository<Artist>>()
                    .ImplementedBy<ArtistRepository>()
                    .LifestyleTransient()
                );
        }
    }
}