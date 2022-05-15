using System;


namespace Credit.Api.Infrastructure
{
    /// <summary>
    /// DateTime extension method
    /// </summary>   
    public static class DateTimeExtention
    {
        public static string ValueOrNullToString(this DateTime? dateTimeValue, string format){
            if (dateTimeValue == null) 
            {
                return null; 
            } 
            else
            {
                return ((DateTime)dateTimeValue).ToString(format);
            }    
        }
    }

}
