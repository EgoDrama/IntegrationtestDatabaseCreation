using System;
using Castle.Windsor;
using EndlessLobster.Domain;

namespace EndlessLobster.Application
{
    public class Program
    {
        static void Main(string[] args)
        {
            var bootstrapper = new Bootstrapper();

            using (var container = new WindsorContainer())
            {
                bootstrapper.Init(container);
                var artistId = 1;

                var artistModifier = container.Resolve<IArtistModifier>();
                var artist = artistModifier.ModifyArtistName(artistId, "AD/HD");

                Console.WriteLine("Artist name: {0}", artist.Name);
                Console.ReadLine();
            }
        }
    }
}
