using System.Threading.Tasks;
using Addapptables.Boilerplate.Configuration.Dto;

namespace Addapptables.Boilerplate.Configuration
{
    public interface IConfigurationAppService
    {
        Task ChangeUiTheme(ChangeUiThemeInput input);

        Task UpdateEmailSettings(EmailSettingsDto settings);

        Task<EmailSettingsDto> GetEmailSettings();
    }
}
