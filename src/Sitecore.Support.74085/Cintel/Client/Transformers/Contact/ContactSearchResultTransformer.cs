namespace Sitecore.Support.Cintel.Client.Transformers.Contact
{
    using System.Reflection;
    using Sitecore.Cintel.Client;
    public class ContactSearchResultTransformer : Sitecore.Cintel.Client.Transformers.Contact.ContactSearchResultTransformer
    {
        private static readonly FieldInfo timeConverterFieldInfo =
            typeof(Sitecore.Cintel.Client.Transformers.Contact.ContactSearchResultTransformer).GetField(
                "timeConverter", BindingFlags.Instance | BindingFlags.NonPublic);

        public ContactSearchResultTransformer()
        {
            timeConverterFieldInfo.SetValue(this, new Sitecore.Support.Cintel.Client.Transformers.TimeConverter(ClientFactory.Instance.GetRepository(), ClientFactory.Instance.GetContextUtil()));
        }
    }
}