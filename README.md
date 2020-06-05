![Logo](https://github.com/raulcanales/FeatuR/blob/master/assets/logo/on_64.png "Logo")
# FeatuR
A simple feature toggle system, to manage experimental features with customizable activation strategies.

Currently, the core library is out, and can be used on projects as well as extend it to your needs, create your custom activation strategies, etc.

The future plans, will include a standalone API service with a small database, that could be used on a distributed system, as well as rest clients to consume such API (or your own implementation of it).

|                   |  |
|-------------------|--------|
| &nbsp;&nbsp;**Build**            |    ![CI](https://github.com/raulcanales/FeatuR/workflows/CI/badge.svg)   |
| &nbsp;&nbsp;**Quality** |    [![Codacy Badge](https://app.codacy.com/project/badge/Grade/58bce6d90c90441da814ca4349bc9d6f)](https://www.codacy.com/manual/raulcanales/FeatuR?utm_source=github.com&amp;utm_medium=referral&amp;utm_content=raulcanales/FeatuR&amp;utm_campaign=Badge_Grade) [![Codefactor](https://www.codefactor.io/repository/github/raulcanales/FeatuR/badge)](https://www.codefactor.io/repository/github/raulcanales/FeatuR/badge)   |
| &nbsp;&nbsp;**Test Coverage** | [![Codacy Badge](https://app.codacy.com/project/badge/Coverage/58bce6d90c90441da814ca4349bc9d6f)](https://www.codacy.com/manual/raulcanales/FeatuR?utm_source=github.com&utm_medium=referral&utm_content=raulcanales/FeatuR&utm_campaign=Badge_Coverage) |


|                   | Official | Preview |
|-------------------|:--------:|:-------:|
| &nbsp;&nbsp;**FeatuR**            |    [![NuGet Badge](https://buildstats.info/nuget/FeatuR)](https://www.nuget.org/packages/FeatuR)   |   [![MyGet Badge](https://buildstats.info/myget/featur/FeatuR)](https://www.myget.org/feed/featur/package/nuget/FeatuR)   |
| &nbsp;&nbsp;**FeatuR.RestClient** |    Coming soon   |   [![MyGet Badge](https://buildstats.info/myget/featur/FeatuR.RestClient)](https://www.myget.org/feed/featur/package/nuget/FeatuR.RestClient)   |
| &nbsp;&nbsp;**FeatuR.EntityFramework.MySQL**        |    Coming soon   |   [![MyGet Badge](https://buildstats.info/myget/featur/FeatuR.EntityFramework.MySQL)](https://www.myget.org/feed/featur/package/nuget/FeatuR.EntityFramework.MySQL)   |
| &nbsp;&nbsp;**FeatuR.EntityFramework.MySQL.DependencyInjection**        |    Coming soon   |   [![MyGet Badge](https://buildstats.info/myget/featur/FeatuR.EntityFramework.MySQL.DependencyInjection)](https://www.myget.org/feed/featur/package/nuget/FeatuR.EntityFramework.MySQL.DependencyInjection)   |
| &nbsp;&nbsp;**FeatuR.EntityFramework.SqlServer**        |    Coming soon   |   [![MyGet Badge](https://buildstats.info/myget/featur/FeatuR.EntityFramework.SqlServer)](https://www.myget.org/feed/featur/package/nuget/FeatuR.EntityFramework.SqlServer)   |
| &nbsp;&nbsp;**FeatuR.EntityFramework.SqlServer.DependencyInjection**        |    Coming soon   |   [![MyGet Badge](https://buildstats.info/myget/featur/FeatuR.EntityFramework.SqlServer.DependencyInjection)](https://www.myget.org/feed/featur/package/nuget/FeatuR.EntityFramework.SqlServer.DependencyInjection)   |

### Roadmap

- [x] Core FeatuR library
- [ ] Write documentation
- [ ] Create and publish Rest client implementation for IFeatureService
- [ ] Create and publish SQL implementation for IFeatureStore
- [ ] Add extra activation strategies
- [ ] Add more samples
