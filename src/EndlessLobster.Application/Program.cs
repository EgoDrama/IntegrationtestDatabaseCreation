﻿
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
            var databaseFactory = new DatabaseFactory();
            var bootstrapper = new Bootstrapper(databaseFactory);

            using (var container = new WindsorContainer())
            {
                bootstrapper.Init(container);
                var artistRepository = bootstrapper.Container.Resolve<IRepository<Artist>>();
                var artistId = 1;

                var artistModifier = new ArtistModifier(artistRepository);
                var artist = artistModifier.ModifyArtistName(artistId, " - AD/HD");

                Console.WriteLine("Artist name: {0}", artist.Name);
                Console.ReadLine();
            }
        }
    }
}
