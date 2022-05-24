namespace SimpleMessaging.Message.Repository.SQL.Extensions
{
    using Models;

    public static class MessageCategoryExtensions
    {
        public static void UpdateFrom(this MessageCategory target, MessageCategory source)
        {
            if (target == null) throw new ArgumentNullException(nameof(target));
            if (source == null) throw new ArgumentNullException(nameof(source));

            if (!string.IsNullOrEmpty(source.Name)) target.Name = source.Name;
            if (!string.IsNullOrEmpty(source.Description)) target.Description = source.Description;
        }
    }
}