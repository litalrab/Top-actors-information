using ActorsInformation.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ActorsInformation.Services
{
    public class ImdbMovieScraper : MoviesScraperBase
    {
        private static readonly string IMDB_URL = "https://www.imdb.com/list/ls054840033/";
        private static readonly string SOURCE = "imdb";

        private static readonly string MOVIES_EL_ID = "//div[contains(@class, 'lister-item mode-detail')]";
        private static readonly string NAME_EL_ID = ".//h3/a";
        private static readonly string RANK_EL_ID = ".//span[@class='lister-item-index unbold text-primary']";
        private static readonly string TYPE_EL_ID = ".//p";
        private static readonly string DETAILS_EL_ID = ".//p[last()]";

        public ImdbMovieScraper() : base(IMDB_URL, MOVIES_EL_ID,NAME_EL_ID,RANK_EL_ID, TYPE_EL_ID, DETAILS_EL_ID, SOURCE)
        {
        }

        //public override List<Actor> LoadActors()
        //{
        //    LoadHtmlWeb();

        //    var nodes = Document.DocumentNode.SelectNodes("//div[contains(@class, 'lister-item mode-detail')]");
        //    List<Actor> actors = new List<Actor>();
        //    if (nodes != null)
        //    {
        //        foreach (var node in nodes)
        //        {
        //            var rankNode = node.SelectSingleNode(".//span[@class='lister-item-index unbold text-primary']");
        //            var actor = new Actor
        //            {
        //                Name = node.SelectSingleNode(".//h3/a").InnerText.Trim(),
        //                Rank = int.Parse(rankNode.InnerText.Trim().TrimEnd('.')),
        //                Type = node.SelectSingleNode(".//p").InnerText.Trim().Split(new[] { '|' }, 2)[0].Trim(),
        //                Details = node.SelectSingleNode(".//p[last()]").InnerText.Trim(),
        //                Source = SOURCE
        //            };
        //            actors.Add(actor);
        //        }

        //    }

        //    return actors;
        //}
    }
}
