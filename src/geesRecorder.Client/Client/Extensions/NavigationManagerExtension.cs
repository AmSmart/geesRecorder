﻿using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.WebUtilities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace geesRecorder.Client.Client.Extensions
{
    public static class NavigationManagerExtension
    {
        public static T ExtractQueryStringByKey<T>(this NavigationManager navManager, string key)
        {
            var uri = navManager.ToAbsoluteUri(navManager.Uri);
            QueryHelpers.ParseQuery(uri.Query)
                .TryGetValue(key, out var queryValue);

            if (typeof(T).Equals(typeof(int)))
            {
                int.TryParse(queryValue, out int result);
                return (T)(object)result;
            }

            if (typeof(T).Equals(typeof(string)))
                return (T)(object)queryValue.ToString();

            return default;
        }
    }
}
