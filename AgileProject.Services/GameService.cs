using AgileProject.Data;
using AgileProject.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AgileProject.Services
{
    public class GameService
    {
        private readonly Guid _userId;

        public GameService(Guid userId)
        {
            _userId = userId;
        }

        public bool EnterGameIntoDatabase(GameCreate game)
        {
            var newGame = new Game()
            {
                OwnerId = _userId,
                Title = game.Title,
                Comment = game.Comment,
                CreatedUtc = DateTimeOffset.Now
            };
            using (var ctx = new ApplicationDbContext())
            {
                ctx.Games.Add(newGame);
                return ctx.SaveChanges() == 1;
            }
        }

        public IEnumerable<GameListItem> GetGames()
        {
            using (var ctx = new ApplicationDbContext())
            {
                var query =
                    ctx
                    .Games
                    .Where(e => e.OwnerId == _userId)
                    .Select(
                        e =>
                            new GameListItem
                            {
                                GameId = e.GameId,
                                Title = e.Title,
                                CreatedUtc = e.CreatedUtc
                            }
                          );
                return query.ToArray();
            }

        }

        public GameDetail GetGameByTitle(string title)
        {
            using(var ctx = new ApplicationDbContext())
            {
                var game =
                    ctx
                    .Games
                    .Where(g => g.OwnerId == _userId)
                    .Single(g => g.Title == title);
                return
                    new GameDetail
                    {
                        GameId = game.GameId,
                        Title = game.Title,
                        Comment = game.Comment,
                        CreatedUtc = game.CreatedUtc,
                        ModifiedUtc = game.ModifiedUtc
                    };
            }
        }

        public bool UpdateGameByTitle(GameEdit game)
        {
            using(var ctx = new ApplicationDbContext())
            {
                var entity =
                    ctx
                    .Games
                    .Where(e => e.OwnerId == _userId)
                    .Single(e => e.Title == game.Title);

                entity.Title = game.Title;
                entity.Comment = game.Comment;
                entity.ModifiedUtc = DateTimeOffset.Now;

                return ctx.SaveChanges() == 1;
            }
        }

        public bool DeleteNoteByTitle(string title)
        {
            using (var ctx = new ApplicationDbContext())
            {
                var entity =
                    ctx
                    .Games
                    .Where(e => e.OwnerId == _userId)
                    .Single(e => e.Title == title);
                ctx.Games.Remove(entity);

                return ctx.SaveChanges() == 1;
            }
        }
    }
}
