using Microsoft.AspNetCore.Builder;

namespace AuthLivestockPoc.Test.Config;

public class EnvironmentTest
{

   [Fact]
   public void IsNotDevModeByDefault()
   { 
       var builder = WebApplication.CreateEmptyBuilder(new WebApplicationOptions());
       var isDev = AuthLivestockPoc.Config.Environment.IsDevMode(builder);
       Assert.False(isDev);
   }
}
