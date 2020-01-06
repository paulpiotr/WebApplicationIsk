using System;
using System.Collections.Generic;
using WebApplicationIsk.Data;

public class AuthorizationService
{

    private readonly UserContext _context;
    public AuthorizationService(UserContext context)
    {
        _context = context;
    }
    public AuthorizationService()
    {
    }

}

