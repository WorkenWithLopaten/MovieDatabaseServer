using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace WebAPI.Models.Cinema
{
    public class CinemaToAddModel
    {
        public CinemaToAddModel()
        {
            this.CinemaCities = new HashSet<CinemaCityModel>();
        }
        public string Name { get; set; }
        public ICollection<CinemaCityModel> CinemaCities{get;set;}
    }
}