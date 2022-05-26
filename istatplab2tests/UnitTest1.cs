using System;
using System.Threading.Tasks;
using istatplab2.Controllers;
using istatplab2.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Xunit;

namespace istatplab2tests;

public class UnitTest1
{
    private static MusicAPIContext CreateContext()
    {
        // створюємо підроброблений (мок) контекст для передавання в контроллер
        var options = new DbContextOptionsBuilder<MusicAPIContext>().UseInMemoryDatabase(databaseName: "MusicApi")
            .Options;
        MusicAPIContext a = new MusicAPIContext(options);
        return a;
    }
    [Fact]
    public async Task CreateGenres()
    {
        //ARRANGE
        using var context = CreateContext();
        GenresController genresController = new GenresController(context);

        //ACT
        Genres genre = new Genres()
        {
            Name = "Rock"
        };
        var result  = await genresController.PostGenres(genre);

        //ASSERT
        Assert.True(result.Result is CreatedAtActionResult);
    }
    [Fact]
    public async Task GenresValidation()
    {
        // перевіряє, чи буде помилка валідації, якщо передамо неправильний об'єкт до POST методу
        //ARRANGE
        using var context = CreateContext();
        GenresController genresController = new GenresController(context);

        //ACT
        Genres genre = new Genres();

        //ASSERT
        await Assert.ThrowsAnyAsync<Exception>(async () => await genresController.PostGenres(genre));
    }
}