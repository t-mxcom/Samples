# LoginAsWho

This solution contains blueprints on several authentication scenarios.

## LoginAsWhoSamplesDatabase

Contains the objects to setup the database to be used with the following sample applications.
You'll need to publish this database project first to be able to try the samples.
Additional setup steps are provided with each sample.

## SqlServerUserSample

Shows how to establish a connection based on a *SQL Server User*.
Additional Information is provided in the [SqlServerUserSample readme](Samples/SqlServerUserSample/README.md).

## TrustedConnectionWithApplicationRoleSample

Shows how to establish a trusted connection in the context of the current user and using an *Application Role* to enable additional permissions.
Additional Information is provided in the [TrustedConnectionWithApplicationRoleSample readme](Samples/TrustedConnectionWithApplicationRoleSample/README.md).

## TrustedConnectionWithImpersonationSample

Shows how to establish a trusted connection in the context of an impersonated user.
Additional Information is provided in the [TrustedConnectionWithImpersonationSample readme](Samples/TrustedConnectionWithImpersonationSample/README.md).

## AzureManagedIdentitySample

Shows how to establish a connection based on an *Azure Managed Identity*.
