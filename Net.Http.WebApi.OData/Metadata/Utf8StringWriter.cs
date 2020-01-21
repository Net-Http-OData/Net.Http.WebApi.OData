// -----------------------------------------------------------------------
// <copyright file="Utf8StringWriter.cs" company="Project Contributors">
// Copyright Project Contributors
//
// Licensed under the Apache License, Version 2.0 (the "License");
// you may not use this file except in compliance with the License.
// You may obtain a copy of the License at
//
//    http://www.apache.org/licenses/LICENSE-2.0
//
// </copyright>
// -----------------------------------------------------------------------
using System.IO;
using System.Text;

namespace Net.Http.WebApi.OData.Metadata
{
    internal sealed class Utf8StringWriter : StringWriter
    {
        public override Encoding Encoding { get; } = Encoding.UTF8;
    }
}
