# FeatuR
A simple feature toggle system, to manage experimental features with customizable activation strategies.

Currently, the core library is out, and can be used on projects as well as extend it to your needs, create your custom activation strategies, etc.

The future plans, will include a standalone API service with a small database, that could be used on a distributed system, as well as rest clients to consume such API (or your own implementation of it).

|                   |  |
|-------------------|--------|
| &nbsp;&nbsp;**Build**            |    ![CI](https://github.com/raulcanales/FeatuR/workflows/CI/badge.svg)   |
| &nbsp;&nbsp;**Quality** |    [![Sonar Quality Gate](https://sonarcloud.io/api/project_badges/measure?project=raulcanales_FeatuR&metric=alert_status)](https://sonarcloud.io/project/issues?id=raulcanales_FeatuR) [![Codefactor](https://www.codefactor.io/repository/github/raulcanales/FeatuR/badge)](https://www.codefactor.io/repository/github/raulcanales/FeatuR/badge)   |
| &nbsp;&nbsp;**Sonar Bugs** | [![Sonar Bugs](https://sonarcloud.io/api/project_badges/measure?project=raulcanales_FeatuR&metric=bugs)](https://sonarcloud.io/project/issues?id=raulcanales_FeatuR&resolved=false&types=BUG) [![Sonar Code Smells](https://sonarcloud.io/api/project_badges/measure?project=raulcanales_FeatuR&metric=code_smells)](https://sonarcloud.io/project/issues?id=raulcanales_FeatuR&resolved=false&types=CODE_SMELL) |

|                   | Official | Preview |
|-------------------|:--------:|:-------:|
| &nbsp;&nbsp;**FeatuR**            |    [![NuGet Badge](https://buildstats.info/nuget/FeatuR)](https://www.nuget.org/packages/FeatuR)   |   [![MyGet Badge](https://buildstats.info/myget/featur/FeatuR)](https://www.myget.org/feed/featur/package/nuget/FeatuR)   |
| &nbsp;&nbsp;**FeatuR.RestClient** |    Coming soon   |   Coming soon   |
| &nbsp;&nbsp;**FeatuR.Sql**        |    Coming soon   |   Coming soon   |

### Roadmap

- [x] Core FeatuR library
- [ ] Write documentation
- [ ] Create and publish Rest client implementation for IFeatureService
- [ ] Create and publish SQL implementation for IFeatureStore
- [ ] Add extra activation strategies
- [ ] Add more samples
