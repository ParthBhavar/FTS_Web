namespace CacheManager
{
    /// <summary>
    /// Represents default values related to caching
    /// </summary>
    public static partial class CachingDefaults
    {
        /// <summary>
        /// Gets the default cache time in minutes
        /// </summary>
        public static int CacheTime => 60;

        /// <summary>
        /// Gets the key used to store the protection key list to Redis (used with the PersistDataProtectionKeysToRedis option enabled)
        /// </summary>
        public static string RedisDataProtectionKey => "Demo.DataProtectionKeys";

        public static class Keys
        {
            public static string BuildUserPreviledgeKey(string userId, string roleName) {
                return $"UserPriviledge_{userId}_{roleName}";
            }

            public static string BuildDocumentProcessKeyKey(string userId)
            {
                return $"Document_PageSequence_{userId}";
            }

            public static string BuildGoogleAccessTokenKey(string userId)
            {
                return $"GoogleAccessToken_{userId}";
            }

            public static string BuildDocumentUploadKey(string userId, string documentId)
            {
                return $"{userId}_{documentId}";
            }

            public static string BuildLoginProviderKey(string userId)
            {
                return $"Web_{userId}";
            }
            public static string NotificationKey(string userId)
            {
               // return $"{Constants.Notification_}{userId}";
                const string Equo = "Equo";
                return $"{Equo}{userId}";
            }

            public static string BuildOffice365AccessTokenKey(string userId)
            {
                return $"Office365AccessToken_{userId}";
            }

            public static string BuildSalesForceAccessTokenKey(string userId)
            {
                return $"SalesforceAccessToken_{userId}";
            }

            public static string BuildSalesForceAuthInstUrlKey(string userId)
            {
                return $"SalesforceAuthInstUrl_{userId}";
            }

            public static string BuildZohoAccessTokenKey(string userId)
            {
                return $"ZohoAccessToken_{userId}";
            }

            public static string BuildGoogleScopeKey(string userId)
            {
                return $"GoogleAccessScope_{userId}";
            }

            public static string BuildGoogleRefreshTokenKey(string userId)
            {
                return $"GoogleRefreshToken_{userId}";
            }
        }
    }
}