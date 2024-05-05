using ActorsInformation.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using HtmlAgilityPack;

namespace ActorsInformation.Services
{
    public abstract class MoviesScraperBase : IMoviesScraper
    {
        protected string Url { get; private set; }
        public HtmlDocument Document { get; private set; }
        public string MoviesElId { get; }
        public string NameElId { get; }
        public string RankElId { get; }
        public string TypeElId { get; }
        public string DetailsElId { get; }
        public string Source { get; }

        public MoviesScraperBase(string url)
        {
            Url = url;
        }

        public MoviesScraperBase(string url, string moviesElId, string nameElId, string rankElId, string typeElId, string detailsElId, string source) : this(url)
        {
            MoviesElId = moviesElId;
            NameElId = nameElId;
            RankElId = rankElId;
            TypeElId = typeElId;
            DetailsElId = detailsElId;
            Source = source;
        }

        protected void LoadHtmlWeb()
        {
            HtmlWeb web = new HtmlWeb();
            Document = web.Load(this.Url);
        }

  //      public abstract List<Actor> LoadActors();
        public virtual List<Actor> LoadActors()
        {
            LoadHtmlWeb();

            var nodes = Document.DocumentNode.SelectNodes(this.MoviesElId);
            List<Actor> actors = new List<Actor>();
            if (nodes != null)
            {
                foreach (var node in nodes)
                {
                    var rankNode = node.SelectSingleNode(this.RankElId);
                    var actor = new Actor
                    {
                        Name = node.SelectSingleNode(this.NameElId).InnerText.Trim(),
                        Rank = int.Parse(rankNode.InnerText.Trim().TrimEnd('.')),
                        Type = node.SelectSingleNode(this.TypeElId).InnerText.Trim().Split(new[] { '|' }, 2)[0].Trim(),
                        Details = node.SelectSingleNode(this.DetailsElId).InnerText.Trim(),
                        Source = this.Source
                    };
                    actors.Add(actor);
                }

            }

            return actors;
        }
    }
}
