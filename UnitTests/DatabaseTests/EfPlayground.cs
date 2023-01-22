//using database;
//using database.Models;
//using database.Repositories;
//using database.Converters;
//using Microsoft.EntityFrameworkCore;

//namespace UnitTests.Database;

///// <summary>
///// ВНИМАНИЕ! Это не тест в адекватном смысле. Мы засунули этот класс в проект теста,
///// чтобы поиграться с ApplicationContext и посмотреть, нормально ли работает подключение к бд.
/////
///// СОВЕТУЮ это все либо закомментить, либо удалить, когда наиграетесь, чтобы при запуске всех тестов,
///// чтобы эта фигня не зааффектила вашу бд. Или можете воздать вторую (тестовую) бд для таких делишек
///// </summary>
//public class EfPlayground
//{
//    private readonly DbContextOptionsBuilder<ApplicationContext> _optionsBuilder;

//    public EfPlayground()
//    {
//        var optionsBuilder = new DbContextOptionsBuilder<ApplicationContext>();
//        optionsBuilder.UseNpgsql(
//            $"Host=localhost;Port=5432;Database=hospital;Username=hospital_user;Password=hospital_user_password");
//        _optionsBuilder = optionsBuilder;
//    }

//    [Fact]
//    public void PlaygroundMethod4()
//    {
//        using var context = new ApplicationContext(_optionsBuilder.Options);
//        var specialtyRep = new SpecialtyRepository(context);

//        specialtyRep.Create(new Specialty(1, "Therapist"));
//        specialtyRep.Create(new Specialty(2, "Ophthalmologist"));
//        specialtyRep.Create(new Specialty(3, "Dentist"));

//        var check = specialtyRep.GetItemsList();

//        List<Specialty> specialties = new()
//        {
//            new Specialty(1, "Therapist"),
//            new Specialty(2, "Ophthalmologist"),
//            new Specialty(3, "Dentist")
//        };

//        Assert.Contains(check, it => it.Id == specialties[0].Id);
//    }

//    [Fact]
//    public void PlaygroundMethod5()
//    {
//        using var context = new ApplicationContext(_optionsBuilder.Options);
//        var doctorRep = new DoctorRepository(context);

//        doctorRep.Create(new Doctor(1, "FIO", new Specialty(3, "Dentist")));

//        var check = doctorRep.GetItem(1);

//        Assert.Equal(check, new Doctor(1, "FIO", new Specialty(3, "Dentist")));
//    }

//    /// <summary>
//    /// Просто реально добавили запись в БД и проверили, добавилось ли
//    /// </summary>
//    [Fact]
//    public void PlaygroundMethod1()
//    {
//        using var context = new ApplicationContext(_optionsBuilder.Options);
//        context.Users.Add(new UserModel
//        {
//            Id = 2,
//            PhoneNumber = "79",
//            FullName = "FIO",
//            Role = Role.Patient,
//            Username = "TEST",
//            Password = "afaf"
//        });
//        context.SaveChanges(); // сохранили в БД

//        Assert.True(context.Users.Any(u => u.Username == "TEST")); // проверим, нашло ли в нашей бд

//        // Можно например написать такой тест, где мы просто сохранили запись,
//        // а потом пойти руками в СУБД посмотреть и убедиться самим, что она там есть 
//    }

//    /// <summary>
//    /// Просто реально удалили запись в БД и проверили, удалилось ли
//    /// </summary>
//    [Fact]
//    public void PlaygroundMethod2()
//    {
//        using var context = new ApplicationContext(_optionsBuilder.Options);
//        var u = context.Users.FirstOrDefault(u => u.Id == 2);
//        context.Users.Remove(u);
//        context.SaveChanges(); // удалили в БД

//        Assert.True(!context.Users.Any(u => u.Id == 2));
//    }

//    /// <summary>
//    /// А вот тут можно приблизительно показать, как у нас будет работать реальный код
//    /// </summary>
//    [Fact]
//    public void PlaygroundMethod3()
//    {
//        # region подготовили сервис

//        using var context = new ApplicationContext(_optionsBuilder.Options);
//        var userRepository = new UserRepository(context);
//        var userService = new UserService(userRepository);

//        # endregion

//        // Подгтовили сервис, которому дали репозиторий, который юзает контекст
//        var res = userService.GetUserByLogin("TEST");

//        Assert.NotNull(res.Value);
//        Assert.Equal(2, res.Value.Id);
//    }
//}