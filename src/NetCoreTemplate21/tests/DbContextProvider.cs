using $ext_safeprojectname$.Data.EntityFramework;
using Microsoft.EntityFrameworkCore;

namespace $safeprojectname$
{
    public static class DbContextProvider
    {
        public static ApplicationDbContext GetInMemoryDbContext() =>
            new ApplicationDbContext(new DbContextOptionsBuilder<ApplicationDbContext>()
                .UseInMemoryDatabase("Business.Tests").Options);
    }
}
