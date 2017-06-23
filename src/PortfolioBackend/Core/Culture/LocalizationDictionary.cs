using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using PortfolioBackend.Globalization;
using PortfolioBackend.Core.DAL;
using PortfolioBackend;
using System.Configuration;
using System.Collections.Concurrent;
using Autofac;

namespace PortfolioBackend.Core.Cultures
{
    public interface ILocalizationDictionary : IDisposable
    {
        string this[string key] { get; }
        string Get(string key);
        string Get(string key, int cultureId);
        void Set(string key, int cultureId, string value);
        IList<KeyValuePair<string, int>> GetEmptyStrings();
        void SyncEmptyStringsWithDB();
        //void OnApplicationEnd();
    }

    public class LocalizationDictionary : ILocalizationDictionary
    {
        #region Instance
        //private readonly ApplicationDbContext _context;
        
        private static ILocalizationDictionary _current;
        public static ILocalizationDictionary Current
        {
            get
            {
                return _current;
            }
        }

        #endregion

        #region Fields

        private ConcurrentDictionary<int, ConcurrentDictionary<string, string>> _dic = new ConcurrentDictionary<int, ConcurrentDictionary<string, string>>();
        private object _dicSyncObj = new Object();

        #endregion

        #region Ctor

        static LocalizationDictionary()
        {
            _current = Startup.ApplicationContainer.Resolve<ILocalizationDictionary>();
        }
        public LocalizationDictionary()//ApplicationDbContext context
        {
            //_context = context;
            Configure();
            Load();
        }

        #endregion

        #region Properties

        public string this[string key] { get { return Get(key); } }

        public int CurrentCultureId
        {
            get
            {
                return CultureHelper.CurrentCultureId;
            }
        }

        #endregion

        #region Methods

        private void Configure()
        {
            // Loading cultures
            foreach (var culture in CultureHelper.Cultures)
                _dic.AddOrUpdate(culture.Id, new ConcurrentDictionary<string, string>(), (k, v) => new ConcurrentDictionary<string, string>());

            // Defining Resolve function
            //var addLocalizedStringsInRuntime = ConfigurationManager.AppSettings["AddLocalizedStringsInRuntime"].AsBoolean();
            //if (addLocalizedStringsInRuntime.GetValueOrDefault())
            //    Resolve = ResolveWithAddInRuntime;
            //else
                Resolve = ResolveWithAddInCache;
        }

        private void Load()
        {
            //using (var dbContext = new ApplicationDbContext())
            //{
                //foreach (var item in _context.LocalizedStrings)
                //    if (!_dic[item.CultureId].ContainsKey(item.Key))
                //        _dic[item.CultureId].AddOrUpdate(item.Key, item.Value == string.Empty ? null : item.Value, (k, v) => item.Value);
            //}
        }

        public string Get(string key)
        {
            if (_dic[CurrentCultureId].ContainsKey(key))
            {
                string value = _dic[CurrentCultureId][key];
                return string.IsNullOrEmpty(value) ? key : value;
            }
            else
                return Resolve(key, CurrentCultureId);
        }
        public string Get(string key, int cultureId)
        {
            if (_dic[cultureId].ContainsKey(key))
            {
                string value = _dic[cultureId][key];
                return string.IsNullOrEmpty(value) ? key : value;
            }
            else
                return Resolve(key, cultureId);
        }
        public void Set(string key, int cultureId, string value)
        {
            if (_dic[cultureId].ContainsKey(key))
                _dic[cultureId][key] = value;
            else
                _dic[cultureId].AddOrUpdate(key, value, (k, v) => value);
        }

        private Func<string, int, string> Resolve;

        private string ResolveWithAddInRuntime(string key, int cultureId)
        {
            throw new NotImplementedException();
        }

        private string ResolveWithAddInCache(string key, int cultureId)
        {
            _dic[cultureId].AddOrUpdate(key, addValue: string.Empty, updateValueFactory: (k, v) => null);

            return key;
        }

        public IList<KeyValuePair<string, int>> GetEmptyStrings()
        {
            return GetEmptyStrings(isOnlyNotSavedToDB: false);
        }

        private IList<KeyValuePair<string, int>> GetEmptyStrings(bool isOnlyNotSavedToDB)
        {
            var list = new List<KeyValuePair<string, int>>();

            lock (_dicSyncObj)
            {
                if (isOnlyNotSavedToDB)
                    foreach (var strings in _dic)
                        list.AddRange(
                            strings.Value.Where(a => a.Value == string.Empty)
                                         .Select(a => new KeyValuePair<string, int>(a.Key, strings.Key)));
                else
                    foreach (var strings in _dic)
                        list.AddRange(
                            strings.Value.Where(a => string.IsNullOrEmpty(a.Value))
                                         .Select(a => new KeyValuePair<string, int>(a.Key, strings.Key)));
            }

            return list;
        }

        /// <summary>
        /// Saves empty that is untranslated entries in the database
        /// </summary>
        /// <param name="isResetCache">If you call this function from Application_End you can set false otherwise must be true</param>
        private void SaveEmptyStringsToDB(bool isResetCache)
        {
            //using (var dbContext = new ApplicationDbContext())
            //{
            //    foreach (var item in GetEmptyStrings(isOnlyNotSavedToDB: true))
            //    {
            //        dbContext.LocalizedStrings.Add(new LocalizedString
            //        {
            //            CultureId = item.Value,
            //            Key = item.Key,
            //            Value = null
            //        });
            //    }
            //    dbContext.SaveChanges();
            //}
            //if (isResetCache)
            //{
            //    lock (_dicSyncObj)
            //    {
            //        foreach (var d in _dic)
            //            d.Value.Clear();
            //        Load();
            //    }
            //}
        }

        public void SyncEmptyStringsWithDB()
        {
            SaveEmptyStringsToDB(isResetCache: true);
        }

        #endregion

        #region IDisposable implementation
        bool _disposed = false;
        public void Dispose(bool disposing)
        {
            if (!_disposed)
            {
                if (disposing)
                {
                    foreach (var d in _dic)
                        d.Value.Clear();
                    _dic.Clear();
                    _dic = null;
                }

                _disposed = true;
            }
        }

        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }

        ~LocalizationDictionary()
        {
            Dispose(false);
        }

        #endregion
    }
}