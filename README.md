[![Build Status](https://travis-ci.com/raulcanales/FeatuR.svg?branch=master)](https://travis-ci.com/raulcanales/FeatuR)
[![Downloads](https://img.shields.io/nuget/dt/FeatuR?style=plastic)](https://img.shields.io/nuget/dt/FeatuR?style=plastic)
[![Nuget](https://img.shields.io/nuget/v/FeatuR)](https://img.shields.io/nuget/v/FeatuR)

# FeatuR
A simple feature toggle system, to manage experimental features with customizable activation strategies.

Currently, the core library is out, and can be used on projects as well as extend it to your needs, create your custom activation strategies, etc.

The future plans, will include a standalone API service with a small database, that could be used on a distributed system, as well as rest clients to consume such API (or your own implementation of it).

### Roadmap

- [x] Core FeatuR library
- [ ] Write documentation
- [ ] Create and publish Rest client implementation for IFeatureService
- [ ] Create and publish SQL implementation for IFeatureStore
- [ ] Add extra activation strategies
- [ ] Add more samples
