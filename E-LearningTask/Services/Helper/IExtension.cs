namespace E_LearningTask.Services.Helper
{
    public interface IExtension
    {
        string UploudFile(string rootpath, string folderName, IFormFile image);

        bool SendEmail(string receiver, string subject, string body);
        //  ClaimsIdentity getClaimsIdentity(int uid, string userName);
    }
}
