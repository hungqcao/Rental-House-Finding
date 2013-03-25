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
        IEnumerable<Posts> GetAllPosts();
        IEnumerable<EmailTemplate> GetAllEmailTemplate();
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
                data = DataContext.Districts.OrderBy(v => v.Name).ToList();

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
                data = DataContext.Categories.OrderBy(v => v.Name).ToList();

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
                data = DataContext.Provinces.ToList();

                if (data.Any())
                {
                    // Put this data into the cache for 30 minutes
                    Cache.Set("provinces", data, int.Parse(ConfigurationManager.AppSettings["TimeRefreshCache"]));
                }
            }

            return data;
        }

        public IEnumerable<Posts> GetAllPosts()
        {
            // First, check the cache
            IEnumerable<Posts> data = Cache.Get("posts") as IEnumerable<Posts>;

            // If it's not in the cache, we need to read it from the repository
            if (data == null)
            {
                // Get the repository data
                data = DataContext.Posts.ToList();

                if (data.Any())
                {
                    // Put this data into the cache for 30 minutes
                    Cache.Set("posts", data, int.Parse(ConfigurationManager.AppSettings["TimeRefreshCache"]));
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

        public void ClearCache()
        {
            Cache.Invalidate("districts");
            Cache.Invalidate("configurations");
            Cache.Invalidate("provinces");
            Cache.Invalidate("categories");
            Cache.Invalidate("posts");
            Cache.Invalidate("EmailTemplate");
        }
    }
}