using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices.JavaScript;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Extensions.Logging;

namespace BooksMgmt.Domain
{
    public class AuthorService
    {
        private readonly ILogger _logger;

        public AuthorService(ILogger<AuthorService> logger)
        {
            _logger = logger;
        }

        public bool ValidateAuthorAge(DateTime dob)
        {
            if(dob > DateTime.Now)
                return false;

            return true;
        }
    }
}
