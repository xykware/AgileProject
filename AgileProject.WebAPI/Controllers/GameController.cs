using AgileProject.Models;
using AgileProject.Services;
using Microsoft.AspNet.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace AgileProject.WebAPI.Controllers
{
    public class GameController : ApiController
    {

        // C - POST Game
        [HttpPost]
        public IHttpActionResult Post(GameCreate game)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var service = CreateGameService();

            if (!service.EnterGameIntoDatabase(game))
            {
                return InternalServerError();
            }
            return Ok();
        }

        // R - GET ALL
        [HttpGet]
        public IHttpActionResult Get()
        {
            GameService gameService = CreateGameService();
            var games = gameService.GetGames();
            return Ok(games);
        }

        // R- GET One By Title
        [HttpGet]
        public IHttpActionResult GetGameByTitle(string title)
        {
            GameService gameService = CreateGameService();
            var game = gameService.GetGameByTitle(title);
            return Ok(game);
        }

        // R - GET One By GameId
        [HttpGet]
        public IHttpActionResult GetGameByGameId(string title)
        {
            GameService gameService = CreateGameService();
            var game = gameService.GetGameByTitle(title);
            return Ok(game);
        }

        // U - PUT One By GameId
        [HttpPut]
        public IHttpActionResult Put(GameEdit game)
        {
            if(!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var service = CreateGameService();

            if(!service.UpdateGame(game))
            {
                return InternalServerError();
            }
            return Ok();
        }

        // D - Delete One By GameId
        [HttpDelete]
        public IHttpActionResult Delete(int id)
        {
            var service = CreateGameService();

            if(service.DeleteGame(id))
            {
                return InternalServerError();
            }
            return Ok();
        }

        //Helper Method
        private GameService CreateGameService()
        {
            var userId = Guid.Parse(User.Identity.GetUserId());
            var gameService = new GameService(userId);
            return gameService;
        }
    }
}
