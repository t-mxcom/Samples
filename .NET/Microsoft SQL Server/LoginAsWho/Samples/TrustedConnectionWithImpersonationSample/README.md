# TrustedConnectionWithImpersonationSample

This sample shows how to use a *Trusted Connection* to establish a connection in the name of a user, which will be logged on and impersonated before.

Establishing a *Trusted Connection* should be possible with all state of the art *SQL server clients* available for *Microsoft Windows*.
Logging on and impersonating another user will be done using *Win32 API calls*.

## Dependencies

In this sample I used the new, open source library `Microsoft.Data.SqlClient` which can be found and installed as *NuGet* package.  
The complete source code can be found at [GitHub dotnet/SqlClient](https://github.com/dotnet/SqlClient).
