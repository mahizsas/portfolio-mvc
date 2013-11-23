﻿using System.Collections.Generic;
using System.Linq;
using System.Web.Mvc;
using Portfolio.Lib.Caching;
using Portfolio.Lib.Data;
using Portfolio.Lib.Services;
using Portfolio.Models;

namespace Portfolio.ViewModels
{
    public static class TagSelectList
    {
        public const string CACHE_KEY = "all_tags";
        public const string TEXT_PROPERTY = "Text";
        public const string VALUE_PROPERTY = "Value";

        public static bool IsInitialized { get; private set; }

        public static IEnumerable<SelectListItem> Tags
        {
            get
            {
                var cachedTags = Cache.Instance.Get(CACHE_KEY);
                return (IEnumerable<SelectListItem>)cachedTags;
            }
        }

        public static void Initialize(IRepository repository = null)
        {
            if (repository == null)
                repository = ServiceLocator.Instance.GetService<IRepository>();

            var categories = repository.Find<Tag>(c => c.IsActive).OrderBy(c => c.Description).ToArray();
            var models = categories.Select(c => new SelectListItem { Value = c.Id.ToString(), Text = c.Description });
            Cache.Instance.Add(CACHE_KEY, models);
            IsInitialized = true;
        }

        public static SelectList SelectList(int? selected = null)
        {
            return new SelectList(Tags, VALUE_PROPERTY, TEXT_PROPERTY, selected);
        }
    }
}