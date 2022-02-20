# TrustedConnectionWithApplicationRoleSample

This sample shows how to use a *Trusted Connection* to establish a connection in the name of the executing user and the using an *Application Role* to enhance the permission for the application.

Establishing a *Trusted Connection* should be possible with all state of the art *SQL server clients* available for *Microsoft Windows*.
In addition, as activating the *Application Role* is just executing a *Stored Procedure*, this should also be possible with all those clients.


## Dependencies

In this sample I used the new, open source library `Microsoft.Data.SqlClient` which can be found and installed as *NuGet* package.  
The complete source code can be found at [GitHub dotnet/SqlClient](https://github.com/dotnet/SqlClient).
