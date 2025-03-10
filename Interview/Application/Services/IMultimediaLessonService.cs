using Domain;

namespace Application.Services;

public interface IMultimediaLessonService
{
    Task<MultimediaLesson> GetById(string id);
    Task<IList<MultimediaLesson>> Get();
    Task Create(MultimediaLesson lesson, string userId);
    Task Update(MultimediaLesson lesson, string userId);
    Task Delete(MultimediaLesson lesson);
}