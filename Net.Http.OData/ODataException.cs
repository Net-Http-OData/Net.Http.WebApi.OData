﻿// -----------------------------------------------------------------------
// <copyright file="ODataException.cs" company="Project Contributors">
// Copyright 2012 - 2020 Project Contributors
//
// Licensed under the Apache License, Version 2.0 (the "License");
// you may not use this file except in compliance with the License.
// You may obtain a copy of the License at
//
//    http://www.apache.org/licenses/LICENSE-2.0
//
// </copyright>
// -----------------------------------------------------------------------
namespace Net.Http.OData
{
    using System;
    using System.Net;
    using System.Runtime.Serialization;

    /// <summary>
    /// An exception which is thrown in relation to an OData request.
    /// </summary>
    [Serializable]
    public sealed class ODataException : Exception
    {
        /// <summary>
        /// Initialises a new instance of the <see cref="ODataException"/> class.
        /// </summary>
        public ODataException()
        {
        }

        /// <summary>
        /// Initialises a new instance of the <see cref="ODataException"/> class.
        /// </summary>
        /// <param name="message">The message that describes the error.</param>
        public ODataException(string message)
            : base(message)
        {
        }

        /// <summary>
        /// Initialises a new instance of the <see cref="ODataException"/> class.
        /// </summary>
        /// <param name="message">The message that describes the error.</param>
        /// <param name="innerException">The exception that is the cause of the current exception, or a null reference (Nothing in Visual Basic) if no inner exception is specified.</param>
        public ODataException(string message, Exception innerException)
            : base(message, innerException)
        {
        }

        /// <summary>
        /// Initialises a new instance of the <see cref="ODataException"/> class.
        /// </summary>
        /// <param name="statusCode">The HTTP status code that describes the error.</param>
        /// <param name="message">The message that describes the error.</param>
        public ODataException(HttpStatusCode statusCode, string message)
            : this(statusCode, message, null)
        {
        }

        /// <summary>
        /// Initialises a new instance of the <see cref="ODataException" /> class.
        /// </summary>
        /// <param name="statusCode">The HTTP status code that describes the error.</param>
        /// <param name="message">The message that describes the error.</param>
        /// <param name="target">The target of the exception.</param>
        public ODataException(HttpStatusCode statusCode, string message, string target)
            : base(message)
        {
            this.StatusCode = statusCode;
            this.Target = target;
        }

        private ODataException(SerializationInfo info, StreamingContext context)
            : base(info, context)
        {
            this.StatusCode = (HttpStatusCode)Enum.Parse(typeof(HttpStatusCode), info.GetString("StatusCode"));
            this.Target = info.GetString("Target");
        }

        /// <summary>
        /// Gets or sets the HTTP status code that describes the error.
        /// </summary>
        public HttpStatusCode StatusCode { get; set; } = HttpStatusCode.InternalServerError;

        /// <summary>
        /// Gets or sets the target of the exception.
        /// </summary>
        public string Target { get; set; }

        /// <summary>
        /// sets the System.Runtime.Serialization.SerializationInfo with information about the exception.
        /// </summary>
        /// <param name="info">The System.Runtime.Serialization.SerializationInfo that holds the serialized object data about the exception being thrown.</param>
        /// <param name="context">The System.Runtime.Serialization.StreamingContext that contains contextual information about the source or destination.</param>
        public override void GetObjectData(SerializationInfo info, StreamingContext context)
        {
            if (info != null)
            {
                info.AddValue("StatusCode", this.StatusCode.ToString());
                info.AddValue("Target", this.Target);
            }

            base.GetObjectData(info, context);
        }
    }
}