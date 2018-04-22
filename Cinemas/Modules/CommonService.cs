using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Cinemas.Models;
namespace Cinemas.Modules
{
    public class CommonService
    {
        protected readonly CinemasEntities CinemasEntities;

        public CommonService()
        {
            CinemasEntities = new CinemasEntities();
        }
    }
}