using Castle.MicroKernel.Registration;
using Castle.MicroKernel.SubSystems.Configuration;
using Castle.Windsor;

namespace EndlessLobster.Domain.Repository.RepositoryInstallers
{
    public class ArtistInstaller : IWindsorInstaller
    {
        public void Install(IWindsorContainer container, IConfigurationStore store)
        {
            container.Register(
                Component.For<IDatabaseFactory>()
                    .ImplementedBy<DatabaseFactory>()
                    .LifestyleTransient(),

                Component.For<IRepository<Artist>>()
                    .ImplementedBy<ArtistRepository>()
                    .LifestyleTransient()
                );
        }
    }
}