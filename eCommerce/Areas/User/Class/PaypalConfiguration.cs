using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using PayPal.Api;

namespace eCommerce.Areas.User.Config
{
    public static class PaypalConfiguration
    {
        //Variables for storing the clientID and clientSecret key  
        public readonly static string ClientId;
        public readonly static string ClientSecret;
        //Constructor  
        static PaypalConfiguration()
        {
            var config = GetConfig();
            ClientId = "AQ90eYOMMS0_hmIn3dr5ZEFtSxiCMiQERySnw7PyDPIjcPcQOKJHv34LM1msdCHz-xUm9hFyrnOLxzZS";
            ClientSecret = "EG-zHMSEO9OkHA7hq6hnp2k81CCypA9WZ5zW_AdE7LtWiR6y3pMgso9HWKGGDWygT-m_2lOqewDAzfOX";
        }
        // getting properties from the web.config  
        public static Dictionary<string, string> GetConfig()
        {
            return PayPal.Api.ConfigManager.Instance.GetProperties();
        }
        private static string GetAccessToken()
        {
            // getting accesstocken from paypal  
            string accessToken = new OAuthTokenCredential(ClientId, ClientSecret, GetConfig()).GetAccessToken();
            return accessToken;
        }

        public static APIContext GetAPIContext()
        {
            // return apicontext object by invoking it with the accesstoken  
            APIContext apiContext = new APIContext(GetAccessToken());
            apiContext.Config = GetConfig();
            return apiContext;
        }

    }
}