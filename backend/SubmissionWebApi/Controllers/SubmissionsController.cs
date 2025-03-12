using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Caching.Memory;


namespace SubmissionWebApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class SubmissionsController : ControllerBase
    {
        private readonly IMemoryCache _cache;
        private const string CacheKey = "Submissions";

        public SubmissionsController(IMemoryCache cache)
        {
            _cache = cache;

            // Ensure the cache is initialized
            if (!_cache.TryGetValue(CacheKey, out List<Dictionary<string, object>> submissions))
            {
                submissions = new List<Dictionary<string, object>>();
                _cache.Set(CacheKey, submissions);
            }
        }

        [HttpPost]
        public IActionResult Submit([FromBody] Dictionary<string, object> formData)
        {
            if (formData == null || formData.Count == 0)
            {
                return BadRequest("Form data cannot be empty.");
            }

            // Retrieve cached submissions
            var submissions = _cache.Get<List<Dictionary<string, object>>>(CacheKey) ?? new List<Dictionary<string, object>>();

            formData["id"] = submissions.Count + 1;

            // Add to the list
            submissions.Add(formData);
            _cache.Set(CacheKey, submissions);

            return Ok(new { message = "Form submitted successfully", submission = formData });
        }

        [HttpGet]
        public IActionResult GetSubmissions([FromQuery] string search = "", [FromQuery] int page = 1, [FromQuery] int pageSize = 10)
        {
            var submissions = _cache.Get<List<Dictionary<string, object>>>(CacheKey) ?? new List<Dictionary<string, object>>();

            // Search filter (if provided)
            if (!string.IsNullOrWhiteSpace(search))
            {
                submissions = submissions
                    .Where(s => s.Values.Any(v => v.ToString().Contains(search, StringComparison.OrdinalIgnoreCase)))
                    .ToList();
            }   

            // Pagination
            var paginatedSubmissions = submissions.Skip((page - 1) * pageSize).Take(pageSize).ToList();

            return Ok(new
            {
                total = submissions.Count,
                page,
                pageSize,
                data = paginatedSubmissions
            });
        }
    }
}
