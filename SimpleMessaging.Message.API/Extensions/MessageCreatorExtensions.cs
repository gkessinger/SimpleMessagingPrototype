namespace SimpleMessaging.Message.API.Extensions
{
    public static class MessageCreatorExtensions
    {
        public static Repository.Models.Message ToRespositoryModel(this API.Models.Message source)
        {
            Repository.Models.Message target = new()
            {
                CreatorId = source.CreatorId,
                CategoryId = source.CategoryId,
                Body = source.Body,
                PostDate = source.PostDate
            };

            return target;
        }

        public static API.Models.Message ToAPIModel(this Repository.Models.Message source)
        {
            API.Models.Message target = new()
            {
                CreatorId = source.CreatorId,
                CategoryId = source.CategoryId,
                Body = source.Body,
                PostDate = source.PostDate
            };

            return target;
        }
    }
}