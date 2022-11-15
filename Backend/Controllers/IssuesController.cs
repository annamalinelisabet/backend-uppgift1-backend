using Backend.Data;
using Backend.Models;
using Backend.Models.Entities;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Diagnostics;

namespace Backend.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class IssuesController : ControllerBase
    {

        private readonly SqlDataContext _context;

        public IssuesController(SqlDataContext context)
        {
            _context = context;
        }

        [HttpPost]
        public async Task<IActionResult> Create(IssueRequest req)
        {
            try
            {
                var datetime = DateTime.Now;
                var issueEntity = new IssueEntity
                {
                    Title = req.Title,
                    Description = req.Description,
                    CustomerId = req.CustomerId,
                    Created = datetime,
                    Updated = datetime,
                    StatusId = 1
                };

                _context.Add(issueEntity);
                await _context.SaveChangesAsync();

                return new OkObjectResult(issueEntity);
            }
            catch (Exception ex) { Debug.WriteLine(ex.Message); }
            return new BadRequestResult();
        }

        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            try
            {
                var issues = new List<IssueResponse>();
                foreach (var issueEntity in await _context.Issues.Include(x => x.Status).Include(x => x.Customer).ToListAsync())
                    issues.Add(new IssueResponse 
                    {
                        Id = issueEntity.Id,
                        Title = issueEntity.Title,
                        Description= issueEntity.Description,
                        Created = issueEntity.Created,
                        Updated = issueEntity.Updated,
                        Status = new StatusResponse
                        {
                            Id = issueEntity.Status.Id,
                            Status = issueEntity.Status.Status
                        },
                        Customer = new CustomerResponse
                        {
                            Id = issueEntity.Customer.Id,
                            FirstName = issueEntity.Customer.FirstName,
                            LastName = issueEntity.Customer.LastName,
                            Email = issueEntity.Customer.Email,
                            Phone = issueEntity.Customer.Phone
                        }
                    });

                return new OkObjectResult(issues);
            }
            catch (Exception ex) { Debug.WriteLine(ex.Message); }
            return new BadRequestResult();
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> Get(int id)
        {
            try
            {
                var issueEntity = await _context.Issues.Include(x => x.Status).Include(x => x.Customer).Include(x => x.Comments).FirstOrDefaultAsync(x => x.Id == id);
                if (issueEntity != null)
                {
                    var comments = new List<CommentResponse>();
                    foreach (var comment in issueEntity.Comments)
                        comments.Add(new CommentResponse
                        {
                            Id = comment.Id,
                            Comment = comment.Comment,
                            Created = comment.Created,
                            CustomerId = comment.CustomerId
                        });
                    
                    return new OkObjectResult(new IssueResponse
                    {
                        Id = issueEntity.Id,
                        Title = issueEntity.Title,
                        Description = issueEntity.Description,
                        Created = issueEntity.Created,
                        Updated = issueEntity.Updated,
                        Status = new StatusResponse
                        {
                            Id = issueEntity.Status.Id,
                            Status = issueEntity.Status.Status
                        },
                        Customer = new CustomerResponse
                        {
                            Id = issueEntity.Customer.Id,
                            FirstName = issueEntity.Customer.FirstName,
                            LastName = issueEntity.Customer.LastName,
                            Email = issueEntity.Customer.Email,
                            Phone = issueEntity.Customer.Phone
                        },
                        Comments = comments
                    });

                }

            }
            catch (Exception ex) { Debug.WriteLine(ex.Message); }
            return new BadRequestResult();
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> Update(int id, IssueUpdateRequest model)
        {
            try
            {
                var issueEntity = await _context.Issues.FindAsync(id);

                if (issueEntity != null)
                    issueEntity.StatusId = model.StatusId;
                    issueEntity.Updated = DateTime.Now;

                _context.Entry(issueEntity).State = EntityState.Modified;
                await _context.SaveChangesAsync();

                return new OkObjectResult(issueEntity);
            }
            catch (Exception ex) { Debug.WriteLine(ex.Message); }
            return new BadRequestResult();
        }


    }
}
