using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Repositories.Entities;
using Repositories.Database;

namespace Repositories
{
    public interface ISettingsRepository
    {
        Settings GetByID(String key);
        Settings InsertOrUpdate(Settings item);
        void Dispose();
    }
    public class SettingsRepository : ISettingsRepository
    {
        private readonly DataContext dataContext;
        public SettingsRepository(DataContext context)
        {
            dataContext = context;
        }
        public SettingsRepository()
        {
            dataContext = new DataContext();
        }

        public Settings GetByID(String key)
        {            
            return dataContext.Settings.Find(key);
        }

        public Settings InsertOrUpdate(Settings item)
        {
            if (item == null)
                throw new ArgumentNullException("item");

            Settings setting = GetByID(item.Key);
            if (setting != null)
            {
                setting.Value = item.Value;
                setting.DateInserted = item.DateInserted;
                setting.DateUpdated = item.DateUpdated;
                dataContext.Entry(setting).State = System.Data.Entity.EntityState.Modified;
            }
            else
            {
                item.DateInserted = DateTime.Now;
                item.DateUpdated = item.DateInserted;
                dataContext.Settings.Add(item);
            }
            dataContext.SaveChanges();
            //TODO delete this next line, it's there to make it work.
            //setting = new Settings();
            dataContext.Entry(setting).Reload();
            return setting;
        }
        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }
        protected void Dispose(bool disposing)
        {
            if (this.dataContext != null)
            {
                this.dataContext.Dispose();
            }
        }
    }
}
