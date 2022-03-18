
//  Monument

// An Open-Source, Convention Based Dependency Injection Auto-Registration Platform



// Goals
// Guide towards better DI practices, with less code (no need for IocConfig or Registrations.cs)
// Allow stronger component cohesion and reduce coupling
// Encourage use of Design Patterns (Decorator, Composite, Adapter, Factory)
// Help code writers and reviewers with Threading issues









//  Proper DI, but unnecessarey
//            container.Register<IFeatureGatesService, FeatureGatesService>();

//  Improper DI, 
//            container.Register<IContactService>(
//                () =>
//                {
//                    var config = container.GetInstance<IApiServiceConfig>();
//                    return new ContactService(
//                        config.EnvironmentName,
//                        container.GetInstance<IFeatureGatesService>(),
//                        container.GetInstance<ITenantShardResolver>(),
//                        container.GetInstance<TelemetryClient>()
//                        );
//                },
//                Lifestyle.Singleton);


// Advanced 
// Captive Dependency Resolution (via IFactory<>)
// Constrained and Varient Generics
// Generic Composites
// Much more!




//            //
//            // end persistence bindings
//            ////////////////////////////////////////////////////////////////////
//            ///

//            // register Unified Data Sharing Objects
//            container.Register<IUnifiedDataSharingRepository>(() => new UnifiedDataSharingRepository(getDataWarehouseSqlConnectionDelegate), Lifestyle.Singleton);

//            container.Register<IUnifiedDataSharingService>(
//                () =>
//                {
//                    return new UnifiedDataSharingService(
//                        container.GetInstance<IUnifiedDataSharingRepository>(),
//                        container.GetInstance<TelemetryClient>());
//                },
//                Lifestyle.Singleton);

//            container.Register<IDataInsightsService>(
//                () =>
//                {
//                    return new DataInsightsService(
//                        container.GetInstance<IDataWarehouseRepository>(),
//                        container.GetInstance<ITenantShardResolver>(),
//                        container.GetInstance<TelemetryClient>()
//                        );
//                },
//                Lifestyle.Singleton);

//            container.Register<IBatteryDataInsightsService>(
//                () =>
//                {
//                    return new BatteryDataInsightsService(
//                        container.GetInstance<IDataWarehouseRepository>(),
//                        container.GetInstance<ITenantShardResolver>(),
//                        container.GetInstance<TelemetryClient>()
//                        );
//                },
//                Lifestyle.Singleton);

//            container.Register<IAppServiceFactory>(
//                () =>
//                {
//                    var config = container.GetInstance<IApiServiceConfig>();
//                    return new AppServiceFactory(
//                        config.GraphHostUri,
//                        config.GraphBetaApiVersion,
//                        container.GetInstance<IGraphGatewayFactory>(),
//                        container.GetInstance<IAzureQueueStorageManager>(),
//                        container.GetInstance<TelemetryClient>());
//                },
//                Lifestyle.Singleton);
//            container.Register<IAccountServiceFactory>(
//                () =>
//                {
//                    var config = container.GetInstance<IApiServiceConfig>();
//                    return new AccountServiceFactory(
//                        config.GraphHostUri,
//                        config.GraphBetaApiVersion,
//                        container.GetInstance<IGraphGatewayFactory>(),
//                        container.GetInstance<ITenantShardResolver>(),
//                        container.GetInstance<TelemetryClient>());
//                },
//                Lifestyle.Singleton);
//            container.Register<IUpdatePolicyServiceFactory>(
//                () =>
//                {
//                    var config = container.GetInstance<IApiServiceConfig>();
//                    return new UpdatePolicyServiceFactory(
//                        config.GraphHostUri,
//                        config.GraphBetaApiVersion,
//                        container.GetInstance<IGraphGatewayFactory>(),
//                        container.GetInstance<TelemetryClient>(),
//                        container.GetInstance<IUpdatesRepository>(),
//                        container.GetInstance<ITenantDataService>(),
//                        container.GetInstance<ITenantShardResolver>()
//                        );
//                },
//                Lifestyle.Singleton);
//            container.Register<IGroupServiceFactory>(
//                () =>
//                {
//                    var config = container.GetInstance<IApiServiceConfig>();
//                    return new GroupServiceFactory(
//                        config.GraphHostUri,
//                        config.GraphApiVersion,
//                        container.GetInstance<IGraphGatewayFactory>(),
//                        container.GetInstance<ITenantShardResolver>(),
//                        container.GetInstance<TelemetryClient>());
//                },
//                Lifestyle.Singleton);
//            container.Register<IAutopilotServiceFactory>(
//            () =>
//            {
//                var config = container.GetInstance<IApiServiceConfig>();
//                return new AutopilotServiceFactory(
//                    config.GraphHostUri,
//                    config.GraphBetaApiVersion,
//                    container.GetInstance<IGraphGatewayFactory>(),
//                    container.GetInstance<ITenantShardResolver>(),
//                    container.GetInstance<TelemetryClient>());
//            },
//            Lifestyle.Singleton);
//            container.Register<IMdmServiceFactory>(
//                () =>
//                {
//                    var config = container.GetInstance<IApiServiceConfig>();
//                    return new IntuneMdmServiceFactory(
//                        config.GraphHostUri,
//                        config.GraphBetaApiVersion,
//                        container.GetInstance<IGraphGatewayFactory>(),
//                        config.AdGraphHostUri,
//                        config.AdGraphVersion,
//                        container.GetInstance<IAdGraphGatewayFactory>(),
//                        container.GetInstance<ITenantShardResolver>(),
//                        container.GetInstance<TelemetryClient>(),
//                        container.GetInstance<IAppServiceFactory>(),
//                        container.GetInstance<IFeatureGatesService>(),
//                        config);
//                },
//                Lifestyle.Singleton);
//            container.Register<IPasswordService, PasswordService>(Lifestyle.Singleton);
//            container.Register<PasswordGenerator>(Lifestyle.Singleton);
//            container.Register<IGraphGatewayFactory, GraphGatewayFactory>(Lifestyle.Singleton);
//            container.Register<IGraphProxyFactory, GraphProxyFactory>(Lifestyle.Singleton);
//            container.Register<IAdGraphGatewayFactory, AdGraphGatewayFactory>(Lifestyle.Singleton);
//            container.Register<IIsolatedManagementServiceFactory>(
//                () =>
//                {
//                    var config = container.GetInstance<IApiServiceConfig>();
//                    return new IsolatedManagementServiceFactory(
//                        config.GraphHostUri,
//                        config.GraphBetaApiVersion,
//                        container.GetInstance<IGraphGatewayFactory>(),
//                        container.GetInstance<ITenantShardResolver>(),
//                        container.GetInstance<TelemetryClient>());
//                },
//                Lifestyle.Singleton);
//            container.Register<IClock, WindowsClock>(Lifestyle.Singleton);
//            container.Register<IEmailServiceFactory>(
//                () =>
//                {
//                    var config = container.GetInstance<IApiServiceConfig>();
//                    var authenticator = container.GetInstance<IAdalAuthenticator>();
//                    return new EmailServiceFactory(
//                        authenticator,
//                        config.GraphHostUri,
//                        config.MicrosoftTenantId,
//                        config.GraphApiVersion,
//                        config.NativeAppClientId,
//                        config.Authority,
//                        config.EmailUsername,
//                        config.EnvironmentName,
//                        async () =>
//                        {
//                            var secretStore = container.GetInstance<ISecretStore>();
//                            return await secretStore.GetPartnerDatabaseEmailPasswordAsync();
//                        },
//                        container.GetInstance<IGraphGatewayFactory>(),
//                        container.GetInstance<IContactService>(),
//                        container.GetInstance<IFeatureGatesService>(),
//                        container.GetInstance<TelemetryClient>());
//                },
//                Lifestyle.Singleton);
//            container.Register<INotificationsService>(
//            () =>
//            {
//                return new NotificationsService(
//                    container.GetInstance<IAdminTenantRepository>(),
//                    container.GetInstance<ITenantShardResolver>(),
//                    container.GetInstance<TelemetryClient>()
//                    );
//            },
//            Lifestyle.Singleton);
//            container.Register<IMessageService>(
//                () =>
//                {
//                    var config = container.GetInstance<IApiServiceConfig>();
//                    return new MessageService(
//                        container.GetInstance<IEmailServiceFactory>(),
//                        container.GetInstance<ITenantShardResolver>(),
//                        container.GetInstance<INotificationsService>(),
//                        container.GetInstance<IContactService>(),
//                        container.GetInstance<TelemetryClient>());
//                });
//            container.Register<IUserPermissionsService, UserPermissionsService>();
//            container.Register<ITenantCredentialService>(
//                () =>
//                {
//                    var authenticator = container.GetInstance<IAdalAuthenticator>();
//                    var config = container.GetInstance<IApiServiceConfig>();
//                    return new TenantCredentialService(
//                        authenticator,
//                        container.GetInstance<IAuditEntriesRepository>(),
//                        container.GetInstance<IEnrollmentConfigurationRepository>(),
//                        container.GetInstance<IGraphGatewayFactory>(),
//                        container.GetInstance<IKeyVaultInstanceFactory>(),
//                        container.GetInstance<ITenantShardResolver>(),
//                        container.GetInstance<IPasswordService>(),
//                        (authority, resource, scope) =>
//                        {
//                            return authenticator.AuthenticateDaemonViaCertificateAsync(
//                                resource,
//                                authority,
//                                config.CustomerSvcAppClientId,
//                                config.CustomerSvcAppAuthCertificate);
//                        },
//                        container.GetInstance<TelemetryClient>(),
//                        config.GraphHostUri,
//                        config.GraphApiVersion,
//                        config.NativeAppClientId,
//                        config.ClientAuthority,
//                        config.PasswordAccessChangeTimeMinutes,
//                        config.PasswordIdleChangeTimeDays);
//                }, Lifestyle.Singleton);
//            container.Register<IUserManagementService>(
//                () =>
//                {
//                    return new UserManagementService(
//                         container.GetInstance<ITenantShardResolver>(),
//                         container.GetInstance<TelemetryClient>());
//                });
//            container.Register<IICMService>(
//                () =>
//                {
//                    var config = container.GetInstance<IApiServiceConfig>();
//                    return new ICMService(
//                        config.ICMUri,
//                        config.ICMAzurePublicId,
//                        config.ICMMMDPublicId,
//                        config.ICMThumbprint,
//                        config.ICMEnvironment,
//                        Guid.Parse(config.ICMGuid),
//                        container.GetInstance<IAzureTableStorageManager>(),
//                        container.GetInstance<IFeatureGatesService>(),
//                        container.GetInstance<IUserPermissionsService>(),
//                        container.GetInstance<TelemetryClient>(),
//                        config.ICMCertificateSubject);
//                }
//                );
//            container.Register<ITenantSettingService>(
//                () =>
//                {
//                    return new TenantSettingService(
//                        container.GetInstance<ITenantDataService>(),
//                        container.GetInstance<ITenantShardResolver>(),
//                        container.GetInstance<TelemetryClient>());
//                }
//            );
//            container.Register<IMSEGDeviceSupportService>(
//                () =>
//                {
//                    var authenticator = container.GetInstance<IAdalAuthenticator>();
//                    var config = container.GetInstance<IApiServiceConfig>();
//                    return new MSEGDeviceSupportService(
//                        container.GetInstance<IHttpClientFactory>(),
//                        container.GetInstance<IEnrollmentConfigurationRepository>(),
//                        config.GraphHostUri,
//                        config.GraphBetaApiVersion,
//                        config.MSEGUri,
//                        config.NativeAppClientId,
//                        config.Authority,
//                        container.GetInstance<TelemetryClient>(),
//                        container.GetInstance<IGraphGatewayFactory>(),
//                        container.GetInstance<IKeyVaultInstanceFactory>(),
//                        authenticator,
//                        container.GetInstance<ITenantDataService>(),
//                        container.GetInstance<ISqlAppsRepository>(),
//                        x =>
//                        {
//                            return authenticator.AuthenticateDelegatedUserToTenantAsync(
//                                config.GraphResourceUri,
//                                config.WebAppClientId,
//                                config.Authority + "{0}",
//                                x.GetTenantId()?.ToString(),
//                                x as ClaimsPrincipal,
//                                config.AuthCertificate);
//                        },
//                        y =>
//                        {
//                            return authenticator.AuthenticateDelegatedUserToTenantAsync(
//                                config.MSEGAppId,
//                                config.WebAppClientId,
//                                config.Authority + "{0}",
//                                y.GetTenantId()?.ToString(),
//                                y as ClaimsPrincipal,
//                                config.AuthCertificate);
//                        },
//                        (authority, resource, scope) =>
//                        {
//                            return authenticator.AuthenticateDaemonViaCertificateAsync(
//                                resource,
//                                authority,
//                                config.CustomerSvcAppClientId,
//                                config.CustomerSvcAppAuthCertificate);
//                        });
//                },
//                Lifestyle.Singleton);

//            container.Register<ISupportServiceFactory>(
//               () =>
//               {
//                   var authenticator = container.GetInstance<IAdalAuthenticator>();
//                   var config = container.GetInstance<IApiServiceConfig>();
//                   return new SupportServiceFactory(
//                       config.GraphHostUri,
//                       config.GraphApiVersion,
//                       container.GetInstance<IGraphGatewayFactory>(),
//                       container.GetInstance<ITenantDataService>(),
//                       container.GetInstance<IPasswordService>(),
//                       container.GetInstance<TelemetryClient>(),
//                       container.GetInstance<IAuditEntriesRepository>(),
//                       x =>
//                       {
//                           return authenticator.AuthenticateDelegatedUserToTenantAsync(
//                               config.GraphResourceUri,
//                               config.WebAppClientId,
//                               config.Authority + "{0}",
//                               x.GetTenantId()?.ToString(),
//                               x as ClaimsPrincipal,
//                               config.AuthCertificate);
//                       });
//               });

//            container.Register<IConfigurationService>(
//                () =>
//                {
//                    var authenticator = container.GetInstance<IAdalAuthenticator>();
//                    var config = container.GetInstance<IApiServiceConfig>();
//                    return new ConfigurationService(
//                        container.GetInstance<IHttpClientFactory>(),
//                        container.GetInstance<ISecurityBaselineRepository>(),
//                        container.GetInstance<ITenantShardResolver>(),
//                        container.GetInstance<IEnrollmentServiceFactory>(),
//                        container.GetInstance<IGroupServiceFactory>(),
//                        container.GetInstance<IUpdatesRepository>(),
//                        container.GetInstance<IAppLockerService>(),
//                        container.GetInstance<ISqlProxyExceptionsRepository>(),
//                        container.GetInstance<IDeploymentTrackerService>(),
//                        container.GetInstance<IMdmServiceFactory>(),
//                        container.GetInstance<TelemetryClient>(),
//                        x =>
//                        {
//                            return authenticator.AuthenticateDelegatedUserToTenantAsync(
//                                config.GraphResourceUri,
//                                config.WebAppClientId,
//                                config.Authority + "{0}",
//                                x.GetTenantId()?.ToString(),
//                                x as ClaimsPrincipal,
//                                config.AuthCertificate);
//                        },
//                        x =>
//                        {
//                            return authenticator.AuthenticateDelegatedUserToTenantAsync(
//                                config.AdGraphResourceUri,
//                                config.WebAppClientId,
//                                config.ClientAuthority + "{0}",
//                                x.GetTenantId()?.ToString(),
//                                x as ClaimsPrincipal,
//                                config.AuthCertificate);
//                        },
//                        container.GetInstance<IICMService>(),
//                        container.GetInstance<IEmbeddedResourceReader>(),
//                        container.GetInstance<IAppLockerBasePolicyRepository>()
//                    );
//                });

//            container.Register<IConditionalAccessService>(
//                () =>
//                {
//                    var authenticator = container.GetInstance<IAdalAuthenticator>();
//                    var config = container.GetInstance<IApiServiceConfig>();
//                    return new ConditionalAccessService(
//                        container.GetInstance<IMdmServiceFactory>(),
//                        container.GetInstance<TelemetryClient>(),
//                        x =>
//                        {
//                            return authenticator.AuthenticateDelegatedUserToTenantAsync(
//                                config.GraphResourceUri,
//                                config.WebAppClientId,
//                                config.ClientAuthority + "{0}",
//                                x.GetTenantId()?.ToString(),
//                                x as ClaimsPrincipal,
//                                config.AuthCertificate);
//                        });
//                });
//            container.Register<IEnrollmentServiceFactory>(
//                () =>
//                {
//                    var authenticator = container.GetInstance<IAdalAuthenticator>();
//                    var config = container.GetInstance<IApiServiceConfig>();
//                    return new EnrollmentServiceFactory(
//                        container.GetInstance<IEnrollmentConfigurationRepository>(),
//                        container.GetInstance<ISecurityBaselineRepository>(),
//                        container.GetInstance<IAccountServiceFactory>(),
//                        container.GetInstance<IGroupServiceFactory>(),
//                        container.GetInstance<IMdmServiceFactory>(),
//                        container.GetInstance<IPasswordService>(),
//                        container.GetInstance<IAutopilotServiceFactory>(),
//                        authenticator,
//                        container.GetInstance<TelemetryClient>(),
//                        container.GetInstance<ITenantShardResolver>(),
//                        container.GetInstance<IKeyVaultInstanceFactory>(),
//                        container.GetInstance<IICMService>(),
//                        container.GetInstance<IAppServiceFactory>(),
//                        container.GetInstance<IIsolatedManagementServiceFactory>(),
//                        container.GetInstance<ITenantSettingService>(),
//                        container.GetInstance<IAzureQueueStorageManager>(),

//                        x =>
//                        {
//                            return authenticator.AuthenticateDelegatedUserToTenantAsync(
//                                config.GraphResourceUri,
//                                config.WebAppClientId,
//                                config.ClientAuthority + "{0}",
//                                x.GetTenantId()?.ToString(),
//                                x as ClaimsPrincipal,
//                                config.AuthCertificate);
//                        },
//                        x =>
//                        {
//                            return authenticator.AuthenticateDelegatedUserToTenantAsync(
//                                config.AdGraphResourceUri,
//                                config.WebAppClientId,
//                                config.ClientAuthority + "{0}",
//                                x.GetTenantId()?.ToString(),
//                                x as ClaimsPrincipal,
//                                config.AuthCertificate);
//                        },
//                        (authority, resource, scope) =>
//                        {
//                            return authenticator.AuthenticateDaemonViaCertificateAsync(
//                                resource,
//                                authority,
//                                config.CustomerSvcAppClientId,
//                                config.CustomerSvcAppAuthCertificate);
//                        },
//                        () =>
//                        {
//                            return authenticator.AuthenticateDaemonViaCertificateAsync(
//                                config.M365EventAuthoringResourceUri,
//                                config.Authority + config.MicrosoftTenantId,
//                                config.CustomerSvcAppClientId,
//                                config.CustomerSvcAppAuthCertificate);
//                        },
//                        container.GetInstance<IM365EventAuthoringGatewayFactory>(),
//                        container.GetInstance<IOnboardToM365HealthDashboardServiceFactory>(),
//                        container.GetInstance<ITenantDataService>(),
//                        container.GetInstance<IReregistrationWithDDSAutopilotService>(),
//                        container.GetInstance<IUpdatesRepository>(),
//                        container.GetInstance<IWin32AppServiceFactory>(),
//                        container.GetInstance<IProfilesFacade>(),
//                        container.GetInstance<IEvolvedFacade>(),
//                        container.GetInstance<IConditionalAccessService>(),
//                        container.GetInstance<IDeviceInformationResolver>(),
//                        container.GetInstance<IUpdatePolicyServiceFactory>(),
//                        config,
//                        container.GetInstance<IDynamicsGateway>(),
//                        container.GetInstance<IWindowsHealthMonitoringService>(),
//                        container.GetInstance<IFeatureGatesService>(),
//                        container.GetInstance<IExpectedStateService>(),
//                        container.GetInstance<ITenantManagementGateway>()
//                        );
//                },
//                Lifestyle.Singleton);

//            container.Register<IAzureBlobStorageManager>(
//                () =>
//                {
//                    Dictionary<StorageAccountName, string> storageAccountConnectionStringDict = new Dictionary<StorageAccountName, string>();
//                    storageAccountConnectionStringDict.Add(StorageAccountName.DEFAULT_STORAGE_ACCOUNT, container.GetInstance<ISecretStore>().GetStorageAccountConnectionStringAsync().Result);
//                    storageAccountConnectionStringDict.Add(StorageAccountName.LOG_STORAGE_ACCOUNT, container.GetInstance<ISecretStore>().GetLogCollectionStorageAccountConnectionStringAsync().Result);
//                    storageAccountConnectionStringDict.Add(StorageAccountName.CUST_STORAGE_ACCOUNT, container.GetInstance<ISecretStore>().GetCustomerFacingStorageAccountConnectionStringAsync().Result);
//                    return new AzureBlobStorageManager(storageAccountConnectionStringDict);
//                }, Lifestyle.Singleton);

//            container.Register<IAzureCustomerBlobStorageManager>(
//                () =>
//                {
//                    var config = container.GetInstance<IApiServiceConfig>();
//                    BlobServiceClient blobServiceClient;
//                    if (config.IsDebugEnvironment)
//                    {
//                        blobServiceClient = new BlobServiceClient(config.CustomerStorageAccountName);
//                    }
//                    else
//                    {
//                        var credential = new DefaultAzureCredential(new DefaultAzureCredentialOptions { ManagedIdentityClientId = config.ManagedIdentitiesForBlobAuth });
//                        var uri = String.Format("https://{0}.blob.core.windows.net", config.CustomerStorageAccountName);
//                        blobServiceClient = new BlobServiceClient(new Uri(uri), credential);
//                    }
//                    return new AzureCustomerBlobStorageManager(blobServiceClient);
//                }, Lifestyle.Singleton);
//            container.Register<IGuidGenerator, DotNetGuidGenerator>(Lifestyle.Singleton);
//            container.Register<IKeyVaultInstance>(
//                () =>
//                {
//                    var authenticator = container.GetInstance<IAdalAuthenticator>();
//                    var config = container.GetInstance<IApiServiceConfig>();
//                    var factory = container.GetInstance<IKeyVaultInstanceFactory>();
//                    var keyVault = factory.Create(
//                        (authority, resource, scope) => authenticator.AuthenticateDaemonViaCertificateAsync(
//                            resource,
//                            authority,
//                            config.CustomerSvcAppClientId,
//                            config.CustomerSvcAppAuthCertificate),
//                        new Uri(config.KeyVaultUri));
//                    return keyVault;
//                },
//                Lifestyle.Singleton);
//            container.Register<IKeyVaultInstanceFactory, KeyVaultInstanceFactory>(Lifestyle.Singleton);
//            container.Register<IDeploymentTrackerService, DeploymentTrackerService>(Lifestyle.Singleton);
//            container.Register<IAppManagementService>(
//                () =>
//                {
//                    var authenticator = container.GetInstance<IAdalAuthenticator>();
//                    var config = container.GetInstance<IApiServiceConfig>();
//                    return new AppManagementService(
//                        container.GetInstance<IAppServiceFactory>(),
//                        authenticator,
//                        container.GetInstance<TelemetryClient>(),
//                        x =>
//                        {
//                            return authenticator.AuthenticateDelegatedUserToTenantAsync(
//                                config.GraphResourceUri,
//                                config.WebAppClientId,
//                                config.Authority + "{0}",
//                                x.GetTenantId()?.ToString(),
//                                x as ClaimsPrincipal,
//                                config.AuthCertificate);
//                        });
//                },
//                Lifestyle.Singleton);

//            container.Register<IAzureQueueStorageManager>(
//                () =>
//                {
//                    return new AzureQueueStorageManager(
//                        async () =>
//                        {
//                            var secretStore = container.GetInstance<ISecretStore>();
//                            return await secretStore.GetQueueStorageAccountConnectionStringAsync();
//                        });
//                },
//                Lifestyle.Singleton);

//            //container.Register<IWin32AppService, Win32AppService>();
//            container.Register<IWin32AppServiceFactory>(
//               () =>
//               {
//                   var config = container.GetInstance<IApiServiceConfig>();
//                   return new Win32AppServiceFactory(
//                        config.GraphHostUri,
//                        config.GraphBetaApiVersion,
//                        container.GetInstance<IGraphGatewayFactory>(),
//                        container.GetInstance<TelemetryClient>(),
//                        container.GetInstance<IAzureQueueStorageManager>(),
//                        container.GetInstance<IAzureBlobStorageManager>(),
//                        container.GetInstance<IWin32AppRepository>(),
//                        container.GetInstance<IAppServiceFactory>(),
//                        container.GetInstance<ITenantCredentialService>());
//               },
//               Lifestyle.Singleton);

//            container.Register<IDeviceRegistrationService>(
//                () =>
//                {
//                    var config = container.GetInstance<IApiServiceConfig>();
//                    var authenticator = container.GetInstance<IAdalAuthenticator>();

//                    return new DeviceRegistrationService(
//                        container.GetInstance<ITenantShardResolver>(),
//                        container.GetInstance<ITenantCredentialService>(),
//                        authenticator,
//                        container.GetInstance<IAdminTenantRepository>(),
//                        config.GraphHostUri,
//                        config.WebAppClientId,
//                        config.Authority + "{0}",
//                        config.AuthCertificate,
//                        config.GraphBetaApiVersion,
//                        container.GetInstance<IGraphGatewayFactory>(),
//                        x =>
//                        {
//                            return authenticator.AuthenticateDelegatedUserToTenantAsync(
//                                config.GraphResourceUri,
//                                config.WebAppClientId,
//                                config.Authority + "{0}",
//                                x.GetTenantId()?.ToString(),
//                                x as ClaimsPrincipal,
//                                config.AuthCertificate);
//                        },
//                        container.GetInstance<IAzureQueueStorageManager>(),
//                        container.GetInstance<IAzureBlobStorageManager>(),
//                        container.GetInstance<IFeatureGatesService>(),
//                        container.GetInstance<TelemetryClient>());
//                },
//                Lifestyle.Singleton);

//            container.Register<IUnenrollmentService, UnenrollmentService>(Lifestyle.Transient);
//            container.Register<ITenantDataService, TenantDataService>(Lifestyle.Singleton);
//            container.Register<IAppPackagingService, AppPackagingService>(Lifestyle.Singleton);
//            container.Register<IAppDiagnosticsService, AppDiagnosticsService>(Lifestyle.Singleton);
//            container.Register<ILogAnalyticsGatewayFactory>(
//               () =>
//               {
//                   var authenticator = container.GetInstance<IAdalAuthenticator>();
//                   var config = container.GetInstance<IApiServiceConfig>();
//                   return new LogAnalyticsGatewayFactory(
//                       container.GetInstance<IHttpClientFactory>(),
//                       config,
//                       x =>
//                       {
//                           return authenticator.AuthenticateDelegatedUserToTenantAsync(
//                               config.LogAnalyticsResourceUri,
//                               config.WebAppClientId,
//                               config.Authority + "{0}",
//                               x.GetTenantId()?.ToString(),
//                               x as ClaimsPrincipal,
//                               config.AuthCertificate);
//                       }
//                   );
//               },
//               Lifestyle.Singleton);
//            container.Register<IWindowsEventService, BasicWindowsEventService>(Lifestyle.Singleton);

//            container.Register<IPublisherRulesRepository, PublisherRulesRepository>(Lifestyle.Singleton);
//            container.Register<IPathRulesRepository, PathRulesRepository>(Lifestyle.Singleton);
//            container.Register<IHashRulesRepository, HashRulesRepository>(Lifestyle.Singleton);
//            container.Register<IAppLockerCustomRulesHandler, AppLockerCustomRulesHandler>();
//            container.Register<IAppLockerService, AppLockerService>();
//            container.Register<IAppLockerBasePolicyRepository, AppLockerBasePolicyRepository>();
//            container.Register<IAppLockerXmlHandler, AppLockerXmlHandler>();
//            container.Register<IAppControlRepository, AppControlRepository>();
//            container.Register<IProfileAssignmentsRepository, ProfileAssignmentsRepository>();
//            container.Register<IDeviceHardwareInventoryService, DeviceHardwareInventoryService>();

//            container.Register<IAzureTableStorageManager>(
//                () =>
//                {
//                    return new AzureTableStorageManager(
//                        async () =>
//                        {
//                            var secretStore = container.GetInstance<ISecretStore>();
//                            return await secretStore.GetStorageAccountConnectionStringAsync();
//                        });
//                },
//                Lifestyle.Singleton);

//            container.Register<IReregistrationWithDDSAutopilotService>(
//                () =>
//                {
//                    return new ReregistrationWithDDSAutopilotService(
//                        container.GetInstance<ITenantDataService>(),
//                        container.GetInstance<IAzureTableStorageManager>(),
//                        container.GetInstance<IAzureQueueStorageManager>(),
//                        container.GetInstance<TelemetryClient>());
//                },
//                Lifestyle.Singleton);

//            container.Register<IM365EventAuthoringGatewayFactory>(
//                () =>
//                {
//                    var config = container.GetInstance<IApiServiceConfig>();
//                    return new M365EventAuthoringGatewayFactory(
//                        container.GetInstance<IHttpClientFactory>(),
//                        config.M365EventAuthoringHostUri,
//                        config.M365EventAuthoringApiVersion
//                        );
//                },
//                Lifestyle.Singleton);
//            container.Register<IOnboardToM365HealthDashboardServiceFactory>(
//                () =>
//                {
//                    var config = container.GetInstance<IApiServiceConfig>();
//                    return new OnboardToM365HealthDashboardServiceFactory(
//                        new M365ConfigParameters
//                        {
//                            WorkloadName = config.M365EventAuthoringWorkloadName
//                        });
//                },
//                Lifestyle.Singleton);
//            container.Register<IHealthCheckService, HealthCheckService>();
//            container.Register<IEmbeddedResourceReader, EmbeddedResourceReader>();
//            container.Register<IDocumentStorageManager>(
//                () =>
//                {
//                    var secretStore = container.GetInstance<ISecretStore>();
//                    return new CosmosDbStorageManager(container.GetInstance<IEmbeddedResourceReader>(), secretStore.GetCosmosDbAccountConnectionString, container.GetInstance<TelemetryClient>());
//                },
//                Lifestyle.Singleton);
//            container.Register<IProfilesRepository, ProfilesRepository>();
//            container.Register<ITenantProfilesRepository, TenantProfilesRepositoryResolver>();
//            container.Register<IDeviceProfileAssignmentRepository, DeviceProfileAssignmentRepositoryResolver>();
//            container.Register<ITenantProfileManager, TenantProfileManager>();
//            container.Register<IGroupTagParser, GroupTagParser>(Lifestyle.Transient);
//            container.Register<IDeviceProfileResolver, DeviceProfileResolver>();
//            container.Register<IPersonaGroupExcluder, PersonaGroupExcluder>();
//            container.Register<IProfilesFacade, ProfilesFacade>();
//            container.Register<IProfilesOperationsReader, ProfilesOperationsReader>();
//            container.Register<IProfilesOperationsRepository, ProfilesOperationsRepository>();
//            container.Register<IBulkAssignmentRequestRepository, BulkAssignmentRequestRepositoryResolver>();
//            container.Register<IDeviceProfileBulkAssigner, DeviceProfileBulkAssigner>();
//            container.Register<IDeviceProfileBulkAssignmentFacade, DeviceProfileBulkAssignmentFacade>();
//            container.Register<IWin32AppRepository, Win32AppRepository>();

//            container.Register<IDeviceConfigurationValidationService, DeviceConfigurationValidationService>();
//            container.Register<IDeviceConfigurationValidationRepository, DeviceConfigurationValidationRepository>();
//            container.Register<IWorkloadGroupCreator, WorkloadGroupCreator>();
//            container.Register<IWorkloadGroupsRepository, WorkloadGroupsRepository>();
//            container.Register<IEvolvedDevicesRepository, EvolvedDevicesRepository>();
//            container.Register<IEvolvedFacade, EvolvedFacade>();
//            container.Register<ITenantProfileService, TenantProfileService>();

//            container.Register<IDevicePerformanceService, DevicePerformanceService>();
//            container.Register<IPerformanceEventRepository, PerformanceEventRepository>();
//            container.Register<IKustoQuerier, KustoQuerier>();
//            container.Register<IKustoIngestor, KustoIngestor>();
//            container.Register<IKustoCredentialResolver, KustoCredentialResolver>();
//            container.Register<IKustoICslQueryProviderFactory, KustoICslQueryProviderFactory>();
//            container.Register<IKustoIngestionClientFactory, KustoIngestionClientFactory>();

//            container.Register<IDeviceStatusMapper, DeviceStatusMapper>();

//            container.Register<IDeviceServicingRepository, DeviceServicingRepository>();
//            // Add named http handler with retry policy
//            IServiceCollection services = new ServiceCollection();
//            services.AddHttpClient(Constants.NameForHttpHandlerWithRetries)
//                    .AddPolicyHandler(UtilityMethods.GetHttpRetryPolicy());
//            services.AddHttpClient(Constants.NameForHttptHandlerWithRetriesAndThrottling)
//                    .AddPolicyHandler(UtilityMethods.GetHttpRetryPolicyForThrottling());

//            // Register HttpClientFactory that pools port connections correctly to avoid 
//            // SNAT port exhaustion.
//            container.Register<IHttpClientFactory>(
//                () => services.BuildServiceProvider().GetRequiredService<IHttpClientFactory>(), Lifestyle.Singleton);

//            // Register HttpClient for TenantResolverV1Controller and its unit test
//            container.Register(() => container.GetInstance<IHttpClientFactory>().CreateClient(), Lifestyle.Singleton);

//            container.Register<IDynamicsGateway, DynamicsGateway>();
//            container.Register<IAuthenticatedHttpClientFactory, AuthenticatedHttpClientFactory>();
//            container.Register<ITenantManagementGateway, TenantManagementGateway>();

//            container.Register<IWindowsHealthMonitoringService>(
//               () =>
//               {
//                   var config = container.GetInstance<IApiServiceConfig>();
//                   var authenticator = container.GetInstance<IAdalAuthenticator>();

//                   return new WindowsHealthMonitoringService(
//                       container.GetInstance<IGraphGatewayFactory>(),
//                       container.GetInstance<TelemetryClient>(),
//                       container.GetInstance<ITenantCredentialService>(),
//                       container.GetInstance<IAdminTenantRepository>(),
//                       x =>
//                       {
//                           return authenticator.AuthenticateDelegatedUserToTenantAsync(
//                               config.GraphResourceUri,
//                               config.WebAppClientId,
//                               config.ClientAuthority + "{0}",
//                               x.GetTenantId()?.ToString(),
//                               x as ClaimsPrincipal,
//                               config.AuthCertificate);
//                       },
//                       config.GraphHostUri,
//                       config.GraphApiVersion);
//               }, Lifestyle.Transient);

//            container.Register<IGraphFilterEscaper, GraphFilterEscaper>(Lifestyle.Singleton);
//        }

//        private static void RegisterElevatedActionsServices(Container container)
//        {
//            container.Register<IElevatedActionVersionPathResolver, ElevatedActionVersionPathResolver>();
//            container.Register<IElevatedActionBlobStorageSasUrlFactory, ElevatedActionBlobStorageSasUrlFactory>();
//            container.Register<IElevatedActionRepository, ElevatedActionRepository>();
//            container.Register<IElevatedActionVersionRepository, ElevatedActionVersionRepository>();
//        }

//        private static void RegisterLogCollectionServices(Container container)
//        {
//            container.Register<ILogCollectionRequestRepository, SqlLogCollectionRequestRepository>();
//            container.Register<ILogCollectionTemplateRepository, SqlLogCollectionTemplateRepository>();
//            container.Register<ILogCollectionBlobStorageUrlFactory, LogCollectionBlobStorageUrlFactory>();
//            container.Register<ILogCollectionService, LogCollectionService>();
//        }

//        private static void RegisterElevatedActionRequestsServices(Container container)
//        {
//            container.Register<IElevatedActionRequester, ElevatedActionRequester>();
//            container.Register<IActionRequestBlobStorageUrlFactory, ActionRequestBlobStorageUrlFactory>();
//            container.Register<IElevatedActionRequestRepository, SqlElevatedActionRequestRepository>();
//            container.Register<IElevatedActionResultsLocationResolver, ElevatedActionResultsLocationResolver>();
//            container.RegisterDecorator<IElevatedActionRequestRepository, HashElevatedActionRequestRepositoryDecorator>();
//        }

//        private static void RegisterLocalizationDataServices(Container container)
//        {
//            container.Register<ILocalizationDataBlobStorageUrlFactory, LocalizationDataBlobStorageUrlFactory>();
//        }

//        private static void RegisterMmdDeviceServices(Container container)
//        {
//            container.Register<IMmdDeviceRepository, SqlMmdDeviceRepository>();
//        }

//        private static void RegisterExpectedStateServices(Container container)
//        {
//            container.Register<IExpectedStateUpdateSender>(() =>
//            {
//                var config = container.GetInstance<IApiServiceConfig>();

//                return new ExpectedStateUpdateSender(config.ExpectedStateEventHubNamespace, config.ExpectedStateEventHubName, config.ManagedIdentityForMonolith);

//            });
//            container.Register<IExpectedStateService>(() =>
//            {
//                return new ExpectedStateService(
//                    container.GetInstance<TelemetryClient>(),
//                    container.GetInstance<IExpectedStateUpdateSender>());
//            });
//        }
//    }
//}