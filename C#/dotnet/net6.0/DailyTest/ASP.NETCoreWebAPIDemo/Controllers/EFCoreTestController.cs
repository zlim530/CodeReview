using EntityFrameworkCoreModel;
using Microsoft.AspNetCore.Mvc;

namespace ASP.NETCoreWebAPIDemo;

[Route("api/[controller]/[action]")]
[ApiController]
public class EFCoreTestController : ControllerBase
{
    private readonly MyMultiDBContext _myDbContext;
	private readonly MyMovieDBContext _movieDbContext;

	public EFCoreTestController(MyMultiDBContext myMultiDBContext,
		MyMovieDBContext myMovieDBContext)
	{
		_myDbContext= myMultiDBContext;
		_movieDbContext= myMovieDBContext;
	}

	[HttpGet]
	public string GetBookCount()
	{
		return "Book Count = " + _myDbContext.Books.Count();
	}

    [HttpGet]
    public string GetMovieCount()
    {
        return "Movie Count = " + _movieDbContext.Movies.Count();
    }
}