using Application.Services;
using Domain;
using Microsoft.AspNetCore.Mvc;

namespace Interview.Controllers;
[ApiController]
[Route("[controller]")]
public class MediaLessonsController : ControllerBase
{
    private readonly IMultimediaLessonService service;

    public MediaLessonsController(IMultimediaLessonService service)
    {
        this.service = service;
    }

    [HttpGet]
    public async Task<ActionResult<IList<MultimediaLesson>>> Get()
    {
        return Ok(await service.Get());
    }
    
    [HttpGet("{id}")]
    public async Task<ActionResult<IList<MultimediaLesson>>> GetById(string id)
    {
        return Ok(await service.GetById(id));
    }
    
    [HttpPost()]
    public async Task<ActionResult<IList<MultimediaLesson>>> Post(MultimediaLesson lesson)
    {
        var userId = "token";
        await service.Create(lesson, userId);
        return Ok();
    }
    
    [HttpPut]
    public async Task<ActionResult<IList<MultimediaLesson>>> Updqte(MultimediaLesson lesson)
    {
        var userId = "token";
        await service.Update(lesson, userId);
        return Ok();
    }
    [HttpDelete("{id}")]
    public async Task<ActionResult> Delete(string id)
    {
        var lesson = await service.GetById(id);
        if (lesson == null)
        {
            return StatusCode(404, "result not found");
        }

        try
        {
            await service.Delete(lesson);
            return NoContent();
        }
        catch (Exception e)
        {
            Console.WriteLine(e);
            throw;
        }
    }
}