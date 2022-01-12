
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Threading.Tasks;

namespace MCComputersApp.Model
{
    public static class SystemConstant
    {
        public static TextInfo TextInfo = new CultureInfo("en-US", false).TextInfo;

        public static string DeleteSuccess = "Delete successful";
        public static string UpdateSuccess = "Item has been successfully updated";
        public static string RequestSuccess = "Request has been Successfully Completed";
        public static string CurrencyType = "Rs";
        public static string CurrencyCode = "LKR";
        public static string DeleteError = "Error while Deleting";
        public static string RequestError = "Error occurred while processing your request";
        public static string DeleteDependantError = "Items cannot be deleted while dependant items exist.";
        public static string DefaultServiceItemImage = "Items cannot be deleted while dependant items exist.";
    }
}
