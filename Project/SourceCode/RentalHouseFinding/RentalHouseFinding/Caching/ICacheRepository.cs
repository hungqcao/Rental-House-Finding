using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using RentalHouseFinding.Models;
using RentalHouseFinding.Caching;
using System.Configuration;

namespace RentalHouseFinding.Caching
{
    public interface ICacheRepository
    {
        void ClearCache();
        IEnumerable<Districts> GetAllDistricts();
        IEnumerable<ConfigurationRHF> GetAllConfiguration();
        IEnumerable<Provinces> GetAllProvinces();
        IEnumerable<Categories> GetAllCategories();
        IEnumerable<EmailTemplate> GetAllEmailTemplate();
        IEnumerable<PostStatuses> GetAllPostStatus();
        IEnumerable<AdvanceSearchScore> GetAllAdvanceSearchScore();
        IEnumerable<BadWords> GetAllBadWords();
    }

    public class CacheRepository : ICacheRepository
    {
        protected RentalHouseFindingEntities DataContext { get; private set; }

        public ICacheProvider Cache { get; set; }

        public CacheRepository()
            : this(new DefaultCacheProvider())
        {
        }

        public CacheRepository(ICacheProvider cacheProvider)
        {
            this.DataContext = new RentalHouseFindingEntities();
            this.Cache = cacheProvider;
        }

        public IEnumerable<Districts> GetAllDistricts()
        {
            // First, check the cache
            IEnumerable<Districts> data = Cache.Get("districts") as IEnumerable<Districts>;

            // If it's not in the cache, we need to read it from the repository
            if (data == null)
            {
                // Get the repository data
                data = DataContext.Districts.Where(d => d.IsDeleted == false).OrderBy(v => v.Name).ToList();

                if (data.Any())
                {
                    // Put this data into the cache for 30 minutes
                    Cache.Set("districts", data, int.Parse(ConfigurationManager.AppSettings["TimeRefreshCache"]));
                }
            }

            return data;
        }

        public IEnumerable<Categories> GetAllCategories()
        {
            // First, check the cache
            IEnumerable<Categories> data = Cache.Get("categories") as IEnumerable<Categories>;

            // If it's not in the cache, we need to read it from the repository
            if (data == null)
            {
                // Get the repository data
                data = DataContext.Categories.Where(d => d.IsDeleted == false).OrderBy(v => v.Name).ToList();

                if (data.Any())
                {
                    // Put this data into the cache for 30 minutes
                    Cache.Set("categories", data, int.Parse(ConfigurationManager.AppSettings["TimeRefreshCache"]));
                }
            }

            return data;
        }

        public IEnumerable<ConfigurationRHF> GetAllConfiguration()
        {
            // First, check the cache
            IEnumerable<ConfigurationRHF> data = Cache.Get("configurations") as IEnumerable<ConfigurationRHF>;

            // If it's not in the cache, we need to read it from the repository
            if (data == null)
            {
                // Get the repository data
                data = DataContext.ConfigurationRHFs.ToList();

                if (data.Any())
                {
                    // Put this data into the cache for 30 minutes
                    Cache.Set("configurations", data, int.Parse(ConfigurationManager.AppSettings["TimeRefreshCache"]));
                }
            }

            return data;
        }

        public IEnumerable<Provinces> GetAllProvinces()
        {
            // First, check the cache
            IEnumerable<Provinces> data = Cache.Get("provinces") as IEnumerable<Provinces>;

            // If it's not in the cache, we need to read it from the repository
            if (data == null)
            {
                // Get the repository data
                data = DataContext.Provinces.Where(d => d.IsDeleted == false).ToList();

                if (data.Any())
                {
                    // Put this data into the cache for 30 minutes
                    Cache.Set("provinces", data, int.Parse(ConfigurationManager.AppSettings["TimeRefreshCache"]));
                }
            }

            return data;
        }

        public IEnumerable<EmailTemplate> GetAllEmailTemplate()
        {
            // First, check the cache
            IEnumerable<EmailTemplate> data = Cache.Get("EmailTemplate") as IEnumerable<EmailTemplate>;

            // If it's not in the cache, we need to read it from the repository
            if (data == null)
            {
                // Get the repository data
                data = DataContext.EmailTemplates.ToList();

                if (data.Any())
                {
                    // Put this data into the cache for 30 minutes
                    Cache.Set("EmailTemplate", data, int.Parse(ConfigurationManager.AppSettings["TimeRefreshCache"]));
                }
            }

            return data;
        }

        public IEnumerable<PostStatuses> GetAllPostStatus()
        {
            // First, check the cache
            IEnumerable<PostStatuses> data = Cache.Get("PostStatus") as IEnumerable<PostStatuses>;

            // If it's not in the cache, we need to read it from the repository
            if (data == null)
            {
                // Get the repository data
                data = DataContext.PostStatuses.Where(d => d.IsDeleted == false).ToList();

                if (data.Any())
                {
                    // Put this data into the cache for 30 minutes
                    Cache.Set("PostStatus", data, int.Parse(ConfigurationManager.AppSettings["TimeRefreshCache"]));
                }
            }

            return data;
        }

        public IEnumerable<AdvanceSearchScore> GetAllAdvanceSearchScore()
        {
            // First, check the cache
            IEnumerable<AdvanceSearchScore> data = Cache.Get("AdvanceSearchScore") as IEnumerable<AdvanceSearchScore>;

            // If it's not in the cache, we need to read it from the repository
            if (data == null)
            {
                // Get the repository data
                data = DataContext.AdvanceSearchScores.ToList();

                if (data.Any())
                {
                    // Put this data into the cache for 30 minutes
                    Cache.Set("AdvanceSearchScore", data, int.Parse(ConfigurationManager.AppSettings["TimeRefreshCache"]));
                }
            }

            return data;
        }

        public IEnumerable<BadWords> GetAllBadWords()
        {
            // First, check the cache
            IEnumerable<BadWords> data = Cache.Get("BadWords") as IEnumerable<BadWords>;

            // If it's not in the cache, we need to read it from the repository
            if (data == null)
            {
                // Get the repository data
                data = DataContext.BadWords.ToList();

                if (data.Any())
                {
                    // Put this data into the cache for 30 minutes
                    Cache.Set("BadWords", data, int.Parse(ConfigurationManager.AppSettings["TimeRefreshCache"]));
                }
            }

            return data;
        }

        public void ClearCache()
        {
            Cache.Invalidate("districts");
            Cache.Invalidate("configurations");
            Cache.Invalidate("provinces");
            Cache.Invalidate("categories");
            Cache.Invalidate("EmailTemplate");
            Cache.Invalidate("PostStatus");
            Cache.Invalidate("AdvanceSearchScore");
            Cache.Invalidate("BadWords");
        }
    }
}