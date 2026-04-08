using RestDemo.BLL.IServices;
using RestDemo.Data;

namespace RestDemo.BLL.Services
{
    public class UserJobService : IUserJobService
    {
        private readonly ILogger<UserJobService> _logger;
        private readonly AppDbContext _appDbContext;

        public UserJobService(ILogger<UserJobService> logger, AppDbContext appDbContext)
        {
            _logger = logger;
            _appDbContext = appDbContext;
        }

        public void UpdateUserToUnActive()
        {
            try
            {
                _logger.LogInformation("Starting job to update user to unactive at {Time}", DateTime.Now);
                // Logic to update user to unactive based on some criteria
                // For example, you can check if the user's last login date is older than a certain threshold
                // and then update their status to unactive in the database.
                var users = _appDbContext.Users.Where(x => x.IsActive).ToList();
                if (users.Count > 0)
                {
                    foreach (var item in users)
                    {
                        item.IsActive = false;
                    }
                    _appDbContext.SaveChanges();
                    _appDbContext.SaveChanges();
                }
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error occurred while updating user to unactive at {Time}", DateTime.Now);
            }
            finally
            {
                _logger.LogInformation("Finished job to update user to unactive at {Time}", DateTime.Now);
            }
        }
    }
}