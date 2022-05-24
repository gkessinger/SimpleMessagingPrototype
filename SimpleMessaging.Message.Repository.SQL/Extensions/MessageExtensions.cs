namespace SimpleMessaging.Message.Repository.SQL.Extensions
{
    using Models;

    public static class MessageExtensions
    {
        public static void UpdateFrom(this Message target, Message source)
        {
            target.PostDate = source.PostDate;
            target.Category = source.Category;
            target.Creator = source.Creator;
            if (!string.IsNullOrEmpty(source.Body)) target.Body = source.Body;
        }
    }
}