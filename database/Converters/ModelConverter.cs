//using domain.Classes;
//using database.Models;

//namespace database.Converters;

//public static class ModelConverter
//{
//    public static T? ToDomain<T, M>(this IModel model)
//        where T : class
//        where M : class, IModel
//    {
//        return ModelConverter.ToDomain(model);
//    }

//    private static object ToDomain<M>(M model) where M : class, IModel
//    {
//        return AppointmentModelConverter.ToDomain<M>(model);
//    }

//    public static Appointment? ToDomain(this AppointmentModel model)
//    {
//        return AppointmentModelConverter.ToDomain(model);
//    }

//    public static Doctor? ToDomain(this DoctorModel model)
//    {
//        return DoctorModelConverter.ToDomain(model);
//    }

//    public static Schedule? ToDomain(this ScheduleModel model)
//    {
//        return ScheduleModelConverter.ToDomain(model);
//    }

//    public static Specialty? ToDomain(this SpecialtyModel model)
//    {
//        return SpecialtyModelConverter.ToDomain(model);
//    }

//    public static User? ToDomain(this UserModel model)
//    {
//        return UserModelConverter.ToDomain(model);
//    }


//    //public static T? ToModel<T, M>(this M model) where T : class where M : class
//    //{
//    //    return ModelConverter.ToModel(model);
//    //}

//    public static AppointmentModel? ToModel(this Appointment model)
//    {
//        return AppointmentModelConverter.ToModel(model);
//    }

//    public static DoctorModel? ToModel(this Doctor model)
//    {
//        return DoctorModelConverter.ToModel(model);
//    }

//    public static ScheduleModel? ToModel(this Schedule model)
//    {
//        return ScheduleModelConverter.ToModel(model);
//    }

//    public static SpecialtyModel? ToModel(this Specialty model)
//    {
//        return SpecialtyModelConverter.ToModel(model);
//    }

//    public static UserModel? ToModel(this User model)
//    {
//        return UserModelConverter.ToModel(model);
//    }
//}