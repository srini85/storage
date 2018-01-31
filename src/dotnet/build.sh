#!/usr/bin/env bash

set -e

# Build
dotnet build Metaparticle.Storage

# Run tests
dotnet test Metaparticle.Storage.Tests
