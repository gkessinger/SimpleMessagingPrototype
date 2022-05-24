using Microsoft.AspNetCore.Mvc;
using System.Net;

namespace SimpleMessaging.Message.API.Controllers
{
    using Repository.Models;
    using Repository.Interfaces;
    using Microsoft.AspNetCore.Routing;

    [ApiController]
    [Route("[controller]")]
    //[Produces("application/json"), Consumes("application/json")]
    public class MessagesController : ControllerBase
    {
        private readonly IMesssageRepository _repository;

        public MessagesController(IMesssageRepository repository)
        {
            _repository = repository;
        }

        /// <summary>
        /// Determine base resource path for responses.
        /// </summary>
        /// <remarks>
        /// Determine the base resource path here for url responses from controller methods to account
        /// for issue with formatting when using object responses (location header) involving path
        /// separators.
        /// </remarks>
        public string BaseResourcePath
        {
            get
            {
                return Request != null ? $"{Request.Scheme}://{Request.Host.ToString().Trim('/')}/{Request.Path.ToString().Trim('/')}" : string.Empty;
            }
        }

        #region Create (POST)

        [HttpPost]
        [ProducesResponseType(201)]
        [ProducesResponseType(409)]
        [ProducesResponseType(500)]
        public async Task<IActionResult> Create([FromBody] Message source)
        {
            var (result, status) = await _repository.CreateMessageAsync(source);

            switch (status)
            {
                case RepositoryStatusCode.Ok:
                case RepositoryStatusCode.Created:
                    var route = $"{BaseResourcePath}/{result.Id}";
                    return Created(route, null);

                case RepositoryStatusCode.Invalid:
                    return BadRequest("Invalid request.");

                case RepositoryStatusCode.Conflict:
                    return Conflict("Resource already exists.");

                default:
                    return StatusCode((int)HttpStatusCode.InternalServerError);
            }
        }

        #endregion

        #region Read (GET)

        [HttpGet(Name = nameof(GetAllMessages))]
        [ProducesResponseType(typeof(IEnumerable<Message>), 200)]
        public async Task<IActionResult> GetAllMessages()
        {
            var (result, _) = await _repository.GetMessagesAsync();
            return Ok(result);
        }


        [HttpGet("{id:int}")]
        [ProducesResponseType(typeof(Message), 200)]
        [ProducesResponseType(typeof(void), 404)]
        public async Task<IActionResult> GetMessageById([FromRoute] int id)
        {
            var (result, status) = await _repository.GetMessageByIdAsync(id);
            return status == RepositoryStatusCode.Ok ? Ok(result) : NotFound();
        }

        #endregion

        #region Update (PUT)

        #endregion

        #region Delete (DELETE)

        #endregion
    }
}