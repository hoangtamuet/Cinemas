using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Http;

namespace Cinemas.Modules.MFilm
{
    [RoutePrefix("api/Films")]
    public class FilmController : CommonController
    { 
        private IFilmService FilmService;
        public FilmController(IFilmService FilmService)
        {
             this.FilmService = FilmService;
        }

        [HttpGet, Route("Count")]
        public int Count([FromUri] SearchFilmEntity SearchFilmEntity)
        {
             return FilmService.Count(UserEntity, SearchFilmEntity);
        }

        [HttpGet, Route("")]
        public List<FilmEntity> Gets([FromUri] SearchFilmEntity SearchFilmEntity)
        {
            return FilmService.Gets(UserEntity, SearchFilmEntity);
        }

        [HttpGet, Route("{FilmId}")]
        public FilmEntity GetId([FromUri] int FilmId)
        {
             return FilmService.GetId(UserEntity, FilmId);
        }


         [HttpPost, Route("")]
         public FilmEntity Create([FromBody] FilmEntity FilmEntity)
         {
             return FilmService.Create(UserEntity, FilmEntity);
         }

         [HttpPut, Route("{FilmId}")]
         public FilmEntity Update([FromUri] int FilmId, [FromBody] FilmEntity FilmEntity)
         {
             return FilmService.Update(UserEntity, FilmId, FilmEntity);
         }

         [HttpDelete, Route("{FilmId}")]
         public bool Delete([FromUri] int FilmId)
         {
             return FilmService.Delete(UserEntity, FilmId);
         }
    }
}