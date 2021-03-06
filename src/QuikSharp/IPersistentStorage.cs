// Copyright (C) 2015 Victor Baybekov

using System.Runtime.Caching;
using Microsoft.Isam.Esent.Collections.Generic;

namespace QuikSharp {
    /// <summary>
    /// 
    /// </summary>
    public interface IPersistentStorage {
        /// <summary>
        /// 
        /// </summary>
        void Set<T>(string key, T value);
        /// <summary>
        /// 
        /// </summary>
        T Get<T>(string key);
        /// <summary>
        /// 
        /// </summary>
        bool Contains(string key);
        /// <summary>
        /// 
        /// </summary>
        bool Remove(string key);
    }

    /// <summary>
    /// Thread-unsafe
    /// </summary>
    public class EsentStorage : IPersistentStorage {
        private static readonly PersistentDictionary<string, PersistentBlob> EsentDic
            = new PersistentDictionary<string, PersistentBlob>("./PersistentStorage");

        private static readonly MemoryCache Cache = MemoryCache.Default;

        private object syncRoot = new object();

        /// <summary>
        /// Useful for more advanced manipulation than IPersistentStorage
        /// QuikSharp depends only on IPersistentStorage
        /// </summary>
        public static PersistentDictionary<string, PersistentBlob> Storage { get { return EsentDic; } }

        public void Set<T>(string key, T value) {
            lock (syncRoot)
            {
                Cache[key] = value;
                EsentDic[key] = value.ToBlob();
            }
        }

        public T Get<T>(string key) {
            lock (syncRoot)
            {
                var v = Cache[key];
                if (v != null)
                {
                    return (T) v;
                }
                v = EsentDic[key].FromBlob<T>();
                Cache[key] = v;
                return (T) v;
            }
        }

        public bool Contains(string key) {
            lock (syncRoot)
            {
                if (Cache.Contains(key))
                {
                    return true;
                }
                if (EsentDic.ContainsKey(key))
                {
                    return true;
                }
                return false;
            }
        }

        public bool Remove(string key) {
            lock (syncRoot)
            {
                Cache.Remove(key);
                var s = EsentDic.Remove(key);
                return s;
            }
        }
    }
}