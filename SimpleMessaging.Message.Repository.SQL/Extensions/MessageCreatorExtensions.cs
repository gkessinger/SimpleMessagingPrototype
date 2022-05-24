namespace SimpleMessaging.Message.Repository.SQL.Extensions
{
    using Models;

    public static class MessageCreatorExtensions
    {
        public static void UpdateFrom(this MessageCreator target, MessageCreator source)
        {
            if (target == null) throw new ArgumentNullException("target");
            if (source == null) throw new ArgumentNullException("source");

            if (!string.IsNullOrEmpty(source.FirstName)) target.FirstName = source.FirstName;
            if (!string.IsNullOrEmpty(source.LastName)) target.LastName = source.LastName;
        }
    }
}