using System.Security.Claims;
using Backend.Data.Entities;
using Elastic.Clients.Elasticsearch;
using Microsoft.AspNetCore.Identity;

namespace Backend.Helpers
{
    public static class UserHelper
    {
        public static int? Id(this ClaimsPrincipal claimsPrincipal)
        {
            var claim = claimsPrincipal.FindFirstValue(ClaimTypes.NameIdentifier);
            
            return claim != null ? int.Parse(claim) : null;
        }
    }
}