namespace Sitecore.Support.Cintel.Client.Transformers.Contact
{
    using System.Reflection;
    using Sitecore.Cintel.Client;
    public class ContactDetailsTransformer : Sitecore.Cintel.Client.Transformers.Contact.ContactDetailsTransformer
    {
        private static readonly FieldInfo timeConverterFieldInfo =
            typeof(Sitecore.Cintel.Client.Transformers.Contact.ContactDetailsTransformer).GetField(
                "timeConverter", BindingFlags.Instance | BindingFlags.NonPublic);
        public ContactDetailsTransformer()
        {
            timeConverterFieldInfo.SetValue(this, new Sitecore.Support.Cintel.Client.Transformers.TimeConverter(ClientFactory.Instance.GetRepository(), ClientFactory.Instance.GetContextUtil()));
        }
    }
}