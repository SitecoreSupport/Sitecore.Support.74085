using System;
using System.Reflection;
using Sitecore.Cintel.Client;

namespace Sitecore.Support.Cintel.Client.Transformers
{
    public class TimeConverter : Sitecore.Cintel.Client.Transformers.TimeConverter
    {
        private static readonly MethodInfo GetCustomizedTimespanStringMethodInfo =
            typeof(Sitecore.Cintel.Client.Transformers.TimeConverter).GetMethod("GetCustomizedTimespanString",
                BindingFlags.Instance | BindingFlags.NonPublic);
        private static readonly FieldInfo contextUtilFieldInfo =
            typeof(Sitecore.Cintel.Client.Transformers.TimeConverter).GetField("contextUtil",
                BindingFlags.Instance | BindingFlags.NonPublic);
        private static readonly MethodInfo GetZeroRecencyMethodInfo =
            typeof(Sitecore.Cintel.Client.Transformers.TimeConverter).GetMethod("GetZeroRecency",
                BindingFlags.Instance | BindingFlags.NonPublic);
        private static readonly FieldInfo recencyTimePartFuncsFieldInfo =
            typeof(Sitecore.Cintel.Client.Transformers.TimeConverter).GetField("recencyTimePartFuncs",
                BindingFlags.Instance | BindingFlags.NonPublic);
        private static readonly FieldInfo recencyDescriptionsFieldInfo =
            typeof(Sitecore.Cintel.Client.Transformers.TimeConverter).GetField("recencyDescriptions",
                BindingFlags.Instance | BindingFlags.NonPublic);
        private ContextUtil contextUtil
        {
            get
            {
                if (contextUtilFieldInfo != null)
                {
                    return (ContextUtil)contextUtilFieldInfo.GetValue(this);
                }
                return default(ContextUtil);
            }
        }
        private Func<DateTime, DateTime, int>[] recencyTimePartFuncs
        {
            get
            {
                if (recencyTimePartFuncsFieldInfo != null)
                {
                    return (Func<DateTime, DateTime, int>[])recencyTimePartFuncsFieldInfo.GetValue(this);
                }
                return default(Func<DateTime, DateTime, int>[]);
            }
        }
        private string[] recencyDescriptions
        {
            get
            {
                if (recencyDescriptionsFieldInfo != null)
                {
                    return (string[])recencyDescriptionsFieldInfo.GetValue(this);
                }
                return default(string[]);
            }
        }

        public TimeConverter(Repository repository, ContextUtil contextUtil) : base(repository, contextUtil)
        {
        }

        public override string FormatDateTime(DateTime time, string format)
        {
            string formattedTime = string.Empty;
            if (time != DateTime.MinValue)
            {
                formattedTime = time.AddMinutes(this.contextUtil.GetTimeZoneOffset()).ToString(format);
            }

            return formattedTime;
        }

        public override string GetRecency(DateTime eventTime, DateTime nowTime)
        {
            string recency = string.Empty;
            if (eventTime != DateTime.MinValue)
            {
                recency = GetCustomizedTimespanString(eventTime, nowTime, recencyTimePartFuncs, recencyDescriptions,
                    GetZeroRecency);
            }

            return recency;
        }

        private string GetCustomizedTimespanString(DateTime eventTime, DateTime nowTime,
            Func<DateTime, DateTime, int>[] timePartFuncs, string[] timePartDescriptions, Func<string> zeroDateFunc)
        {
            if (GetCustomizedTimespanStringMethodInfo != null)
            {
                return (string)GetCustomizedTimespanStringMethodInfo.Invoke(this,
                    new object[] { eventTime, nowTime, timePartFuncs, timePartDescriptions, zeroDateFunc });
            }
            return zeroDateFunc.Invoke();
        }

        private string GetZeroRecency()
        {
            return (string)GetZeroRecencyMethodInfo.Invoke(this, new object[] { });
        }
    }
}