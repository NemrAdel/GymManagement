using GymManagementBLL.View_Models.EmailViewModel;

namespace GymManagementBLL.EmailService
{
    public interface IEmailService
    {
        Task SendEmail(EmailVM email);
    }
}
