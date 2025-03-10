using Application.Services;
using Domain;
using Infrastructure.Persistance;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;

namespace Infrastructure.Services;

public class MediaLessonService: IMultimediaLessonService
{
    private readonly ApplicationDbContext context;
    private readonly ILogger<MediaLessonService> logger;

    public MediaLessonService(ApplicationDbContext context, ILogger<MediaLessonService> logger)
    {
        this.context = context;
        this.logger = logger;
    }
    public async Task<MultimediaLesson> GetById(string id)
    {
        return await context.MultimediaLessons.Where(x=>x.Id==id).FirstOrDefaultAsync();
    }

    public async Task<IList<MultimediaLesson>> Get()
    {
        return await context.MultimediaLessons.ToListAsync();
    }

    public async Task Create(MultimediaLesson lesson, string userId)
    {
        var mediaLesson = new MultimediaLesson()
        {
            Id = new Guid().ToString(),
            Title = lesson.Title,
            ImagePath = lesson.ImagePath,
            CreatedBy = userId, // get from JWT
            CreatedAt = DateTime.UtcNow
        };
        try
        {
            await context.MultimediaLessons.AddAsync(mediaLesson);
            await context.SaveChangesAsync();
        }
        catch (Exception e)
        {
            logger.LogError(e.Message);
            throw;
        }
    }

    public async Task Update(MultimediaLesson lesson, string userId)
    {
        var mediaLesson = await GetById(lesson.Id);
        if (mediaLesson != null)
        {
            mediaLesson.Title = lesson.Title;
            mediaLesson.ImagePath = lesson.ImagePath;
            mediaLesson.UpdatedBy = userId;
        }
    }

    public async Task Delete(MultimediaLesson lesson)
    {
         context.MultimediaLessons.Remove(lesson);
         await context.SaveChangesAsync();
    }
}