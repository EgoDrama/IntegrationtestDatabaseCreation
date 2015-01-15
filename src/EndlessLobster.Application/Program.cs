using System;
using Castle.Windsor;
using EndlessLobster.Domain;
using EndlessLobster.Domain.Repository;

namespace EndlessLobster.Application
{
    public class Program
    {
        static void Main(string[] args)
        {
            using (var container = new WindsorContainer())
            {
                var bootstrapper = new Bootstrapper();
                bootstrapper.Init(container);

                var artistRepository = container.Resolve<IRepository<Artist>>();
                int artistId = 1;
                var artist = artistRepository.Get(artistId);

                Console.WriteLine("Artist name: {0}", artist.Name);
                Console.ReadLine();
            }
        }
    }
}
